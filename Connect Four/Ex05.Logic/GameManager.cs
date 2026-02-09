using Ex05;
using System;

public class GameManager
{
    public event Action<int, int, eCoin> BoardChanged;
    public event Action<string> GameEnded;
    public event Action<int> TurnChanged;

    private BoardLogic m_Board;
    private readonly Player r_Player1;
    private readonly Player r_Player2;
    private readonly ComputerBot r_ComputerBot;
    private readonly bool r_IsComputerOpponent;
    private bool m_IsPlayer1Turn;

    public int Rows => m_Board.Rows;
    public int Cols => m_Board.Cols;

    public int Player1Score
    {
        get; 
        private set;
    }
    public int Player2Score
    { 
        get; 
        private set;
    }

    public GameManager(int i_Rows, int i_Cols, bool i_IsComputerOpponent)
    {
        m_Board = new BoardLogic(i_Rows, i_Cols);
        r_IsComputerOpponent = i_IsComputerOpponent;
        m_IsPlayer1Turn = true;
        r_Player1 = new Player("Player 1", eCoin.Player1);

        if (r_IsComputerOpponent)
        {
            r_ComputerBot = new ComputerBot(eCoin.Player2);
        }
        else
        {
            r_Player2 = new Player("Player 2", eCoin.Player2);
        }
    }

    public void PlayMove(int i_Col)
    {
        eCoin currentMark = getCurrentMark();

        if (m_Board.PlaceCoin(i_Col, currentMark))
        {
            int landedRow = findLandedRow(i_Col);
            OnBoardChanged(landedRow, i_Col, currentMark);
            checkGameStatus(currentMark);
        }
    }

    public void ResetGame()
    {
        m_Board = new BoardLogic(m_Board.Rows, m_Board.Cols);
        m_IsPlayer1Turn = true;
        OnTurnChanged(1);
    }

    private void checkGameStatus(eCoin i_CurrentMark)
    {
        if (m_Board.CheckWinner(i_CurrentMark))
        {
            handleWin(i_CurrentMark);
        }
        else if (m_Board.IsBoardFull())
        {
            OnGameEnded("Tie!!");
        }
        else
        {
            switchTurn();
        }
    }

    private void switchTurn()
    {
        m_IsPlayer1Turn = !m_IsPlayer1Turn;
        OnTurnChanged(m_IsPlayer1Turn ? 1 : 2);

        if (!m_IsPlayer1Turn && r_IsComputerOpponent)
        {
            playComputerTurn();
        }
    }

    private void playComputerTurn()
    {
        int botMoveCol = r_ComputerBot.GetMove(m_Board);
        PlayMove(botMoveCol);
    }

    private void handleWin(eCoin i_WinnerMark)
    {
        string message;
        if (i_WinnerMark == r_Player1.Mark)
        {
            Player1Score++;
            message = "Player 1 Won!!";
        }
        else
        {
            Player2Score++;
            message = r_IsComputerOpponent ? "Computer Won!!" : "Player 2 Won!!";
        }
        OnGameEnded(message);
    }

    private eCoin getCurrentMark()
    {
        if (m_IsPlayer1Turn)
        {
            return r_Player1.Mark;
        }
        if (r_IsComputerOpponent)
        {
            return r_ComputerBot.Mark;
        }

        return r_Player2.Mark;
    }

    private int findLandedRow(int i_Col)
    {
        for (int row = 0; row < m_Board.Rows; row++)
        {
            if (!m_Board.IsCellEmpty(row, i_Col))
            {
                return row;
            }
        }

        return -1;
    }

    private void OnBoardChanged(int i_Row, int i_Column, eCoin i_Mark)
    {
        BoardChanged?.Invoke(i_Row, i_Column, i_Mark);
    }

    private void OnGameEnded(string i_Message)
    {
        GameEnded?.Invoke(i_Message);
    }

    private void OnTurnChanged(int i_PlayerNum)
    {
        TurnChanged?.Invoke(i_PlayerNum);
    }
}