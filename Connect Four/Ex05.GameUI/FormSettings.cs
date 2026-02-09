using System;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05
{
    public class FormSettings : Form
    {
        private Label m_LabelPlayers;
        private Label m_LabelPlayer1;
        private Label m_LabelPlayer2;
        private Label m_LabelBoardSize;
        private Label m_LabelRows;
        private Label m_LabelCols;
        private TextBox m_TextBoxPlayer1;
        private TextBox m_TextBoxPlayer2;
        private CheckBox m_CheckBoxPlayer2;
        private NumericUpDown m_NumericUpDownRows;
        private NumericUpDown m_NumericUpDownCols;
        private Button m_ButtonStart;

        public string Player1Name
        {
            get
            {
                string name = m_TextBoxPlayer1.Text;

                if (string.IsNullOrEmpty(name))
                {
                    name = "Player 1";
                }

                return name;
            }
        }

        public string Player2Name
        {
            get
            {
                string name = "Computer";

                if (m_CheckBoxPlayer2.Checked)
                {
                    name = m_TextBoxPlayer2.Text;
                    
                    if (string.IsNullOrEmpty(name))
                    {
                        name = "Player 2";
                    }
                }

                return name;
            }
        }

        public int Rows
        {
            get
            {
                return (int)m_NumericUpDownRows.Value;
            }
        }

        public int Cols
        {
            get
            {
                return (int)m_NumericUpDownCols.Value;
            }
        }

        public bool IsAgainstComputer
        {
            get
            {
                return !m_CheckBoxPlayer2.Checked;
            }
        }

        public FormSettings()
        {
            initializeUI();
        }

        private void initializeUI()
        {
            this.Text = "Game Settings";
            this.ClientSize = new Size(280, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            m_LabelPlayers = new Label();
            m_LabelPlayers.Text = "Players:";
            m_LabelPlayers.Location = new Point(10, 15);
            m_LabelPlayers.AutoSize = true;

            m_LabelPlayer1 = new Label();
            m_LabelPlayer1.Text = "Player 1:";
            m_LabelPlayer1.Location = new Point(25, 45);
            m_LabelPlayer1.AutoSize = true;

            m_TextBoxPlayer1 = new TextBox();
            m_TextBoxPlayer1.Location = new Point(130, 42);
            m_TextBoxPlayer1.Size = new Size(120, 20);

            m_CheckBoxPlayer2 = new CheckBox();
            m_CheckBoxPlayer2.Text = "Player 2:";
            m_CheckBoxPlayer2.Location = new Point(25, 75);
            m_CheckBoxPlayer2.AutoSize = true;
            m_CheckBoxPlayer2.CheckedChanged += checkBoxPlayer2_CheckedChanged;

            m_TextBoxPlayer2 = new TextBox();
            m_TextBoxPlayer2.Location = new Point(130, 72);
            m_TextBoxPlayer2.Size = new Size(120, 20);
            m_TextBoxPlayer2.Enabled = false;
            m_TextBoxPlayer2.Text = "[Computer]";

            m_LabelBoardSize = new Label();
            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.Location = new Point(10, 120);
            m_LabelBoardSize.AutoSize = true;

            m_LabelRows = new Label();
            m_LabelRows.Text = "Rows:";
            m_LabelRows.Location = new Point(25, 155);
            m_LabelRows.AutoSize = true;

            m_NumericUpDownRows = new NumericUpDown();
            m_NumericUpDownRows.Location = new Point(75, 153);
            m_NumericUpDownRows.Size = new Size(40, 20);
            m_NumericUpDownRows.Minimum = 4;
            m_NumericUpDownRows.Maximum = 10;

            m_LabelCols = new Label();
            m_LabelCols.Text = "Cols:";
            m_LabelCols.Location = new Point(155, 155);
            m_LabelCols.AutoSize = true;

            m_NumericUpDownCols = new NumericUpDown();
            m_NumericUpDownCols.Location = new Point(205, 153);
            m_NumericUpDownCols.Size = new Size(40, 20);
            m_NumericUpDownCols.Minimum = 4;
            m_NumericUpDownCols.Maximum = 10;

            m_ButtonStart = new Button();
            m_ButtonStart.Text = "Start!";
            m_ButtonStart.Size = new Size(240, 30);
            m_ButtonStart.Location = new Point(((this.ClientSize.Width - m_ButtonStart.Width) / 2), 200);
            m_ButtonStart.Click += buttonStart_Click;

            this.Controls.Add(m_LabelPlayers);
            this.Controls.Add(m_LabelPlayer1);
            this.Controls.Add(m_TextBoxPlayer1);
            this.Controls.Add(m_CheckBoxPlayer2);
            this.Controls.Add(m_TextBoxPlayer2);
            this.Controls.Add(m_LabelBoardSize);
            this.Controls.Add(m_LabelRows);
            this.Controls.Add(m_NumericUpDownRows);
            this.Controls.Add(m_LabelCols);
            this.Controls.Add(m_NumericUpDownCols);
            this.Controls.Add(m_ButtonStart);
        }

        private void checkBoxPlayer2_CheckedChanged(object i_Sender, EventArgs i_EventArgs)
        {
            if (m_CheckBoxPlayer2.Checked)
            {
                m_TextBoxPlayer2.Enabled = true;
                m_TextBoxPlayer2.Text = string.Empty;
            }
            else
            {
                m_TextBoxPlayer2.Enabled = false;
                m_TextBoxPlayer2.Text = "[Computer]";
            }
        }

        private void buttonStart_Click(object i_Sender, EventArgs i_EventArgs)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}