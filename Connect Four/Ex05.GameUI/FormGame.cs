using System;
using System.Drawing;
using System.Windows.Forms;
using Ex05; // Use the Logic Namespace

namespace Ex05_Eylon_203235478_Michael_208233890
{
    public class FormGame : Form
    {
        private readonly GameManager r_GameManager;
        private Button[] m_ColButtons;
        private Button[,] m_BoardButtons;
        private Label m_LabelP1Score;
        private Label m_LabelP2Score;

        public FormGame(int i_Rows, int i_Cols, string i_P1Name, string i_P2Name, bool i_IsComputer)
        {
            // 1. Initialize Manager
            r_GameManager = new GameManager(i_Rows, i_Cols, i_IsComputer);

            // 2. Subscribe to Events
            r_GameManager.BoardChanged += board_Changed;
            r_GameManager.GameEnded += game_Ended;
            r_GameManager.TurnChanged += turn_Changed;

            // 3. Set up UI
            initializeUI(i_Rows, i_Cols, i_P1Name, i_P2Name);
        }

        private void initializeUI(int rows, int cols, string p1Name, string p2Name)
        {
            this.Text = "4 in a Row !!";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            int startX = 10;
            int startY = 10;
            int btnSize = 50; // Size of buttons

            // Create Top Buttons (1, 2, 3...)
            m_ColButtons = new Button[cols];
            for (int c = 0; c < cols; c++)
            {
                m_ColButtons[c] = new Button();
                m_ColButtons[c].Text = (c + 1).ToString();
                m_ColButtons[c].Size = new Size(btnSize, 25);
                m_ColButtons[c].Location = new Point(startX + c * (btnSize + 5), startY);
                m_ColButtons[c].Tag = c; // Remember the column index
                m_ColButtons[c].Click += colButton_Click;
                this.Controls.Add(m_ColButtons[c]);
            }

            // Create Board Grid (Disabled Buttons)
            m_BoardButtons = new Button[rows, cols];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(btnSize, btnSize);
                    btn.Location = new Point(startX + c * (btnSize + 5), startY + 30 + r * (btnSize + 5));
                    btn.Enabled = false; // User cannot click these
                    btn.BackColor = Color.LightGray;
                    btn.Font = new Font("Arial", 20, FontStyle.Bold);
                    m_BoardButtons[r, c] = btn;
                    this.Controls.Add(btn);
                }
            }

            // Labels for Score
            int bottomY = startY + 30 + rows * (btnSize + 5) + 10;

            m_LabelP1Score = new Label();
            m_LabelP1Score.Text = string.Format("{0}: 0", p1Name);
            m_LabelP1Score.AutoSize = true;
            m_LabelP1Score.Location = new Point(startX + 20, bottomY);
            this.Controls.Add(m_LabelP1Score);

            m_LabelP2Score = new Label();
            m_LabelP2Score.Text = string.Format("{0}: 0", p2Name);
            m_LabelP2Score.AutoSize = true;
            m_LabelP2Score.Location = new Point(startX + 150, bottomY);
            this.Controls.Add(m_LabelP2Score);

            // Resize Form to fit everything
            this.ClientSize = new Size(startX + cols * (btnSize + 5) + 10, bottomY + 40);
        }

        // === Event Handlers ===

        private void colButton_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            int colIndex = (int)clickedBtn.Tag;

            // Tell Logic to play this move
            r_GameManager.PlayMove(colIndex);
        }

        private void board_Changed(int row, int col, eCoin coin)
        {
            string text = "";
            if (coin == eCoin.Player1) text = "X";
            else if (coin == eCoin.Player2) text = "O";

            m_BoardButtons[row, col].Text = text;
        }


        private void turn_Changed(int playerNum)
        {
            // Optional: You could bold the active player's label here
        }

        private void game_Ended(string message)
        {
            // Update Scores
            string[] names = m_LabelP1Score.Text.Split(':');
            m_LabelP1Score.Text = string.Format("{0}: {1}", names[0], r_GameManager.Player1Score);

            names = m_LabelP2Score.Text.Split(':');
            m_LabelP2Score.Text = string.Format("{0}: {1}", names[0], r_GameManager.Player2Score);

            // Show Message Box
            DialogResult result = MessageBox.Show(
                string.Format("{0}{1}Another Round?", message, Environment.NewLine),
                "Game Over",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                r_GameManager.ResetGame();
                clearBoardUI();
            }
            else
            {
                this.Close();
            }
        }

        private void clearBoardUI()
        {
            foreach (Button btn in m_BoardButtons)
            {
                btn.Text = "";
            }
        }
    }
}