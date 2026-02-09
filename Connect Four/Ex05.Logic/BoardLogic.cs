using Ex05;
using System;

public class BoardLogic
{
    private readonly eCoin[,] m_Board;
    private readonly int r_Rows;
    private readonly int r_Cols;
    private const int k_NumToWin = 4;

    public event Action<int, int, eCoin> CellChanged;

    public int Rows
    {
        get
        { 
            return r_Rows; 
        }
    }

    public int Cols
    {
        get
        { 
            return r_Cols; 
        }
    }

    public BoardLogic(int i_Rows, int i_Cols)
    {
        r_Rows = i_Rows;
        r_Cols = i_Cols;
        m_Board = new eCoin[r_Rows, r_Cols];
        initializeBoard();
    }

    private void initializeBoard()
    {
        for (int i = 0; i < r_Rows; i++)
        {
            for (int j = 0; j < r_Cols; j++)
            {
                m_Board[i, j] = eCoin.Empty;
            }
        }
    }

    public bool PlaceCoin(int i_Col, eCoin i_Mark)
    {
        bool isPlaced = false;

        if (i_Col >= 0 && i_Col < r_Cols)
        {
            for (int row = r_Rows - 1; row >= 0; row--)
            {
                if (m_Board[row, i_Col] == eCoin.Empty)
                {
                    m_Board[row, i_Col] = i_Mark;
                    isPlaced = true;
                    OnCellChanged(row, i_Col, i_Mark);
                    break;
                }
            }
        }

        return isPlaced;
    }

    protected virtual void OnCellChanged(int i_Row, int i_Col, eCoin i_Mark)
    {
        CellChanged?.Invoke(i_Row, i_Col, i_Mark);
    }

    public eCoin GetCellContent(int i_Row, int i_Col)
    {
        return m_Board[i_Row, i_Col];
    }

    public bool IsCellEmpty(int i_Row, int i_Col)
    {
        return m_Board[i_Row, i_Col] == eCoin.Empty;
    }

    public bool IsBoardFull()
    {
        bool isFull = true;

        for (int col = 0; col < r_Cols; col++)
        {
            if (m_Board[0, col] == eCoin.Empty)
            {
                isFull = false;
                break;
            }
        }

        return isFull;
    }

    public bool CheckWinner(eCoin i_Mark)
    {
        return checkHorizontal(i_Mark) ||
               checkVertical(i_Mark) ||
               checkDiagonalDown(i_Mark) ||
               checkDiagonalUp(i_Mark);
    }

    private bool checkHorizontal(eCoin i_Mark)
    {
        bool isWin = false;
        for (int row = 0; row < r_Rows && !isWin; row++)
        {
            for (int col = 0; col <= r_Cols - k_NumToWin; col++)
            {
                if (m_Board[row, col] == i_Mark &&
                    m_Board[row, col + 1] == i_Mark &&
                    m_Board[row, col + 2] == i_Mark &&
                    m_Board[row, col + 3] == i_Mark)
                {
                    isWin = true;
                    break;
                }
            }
        }

        return isWin;
    }

    private bool checkVertical(eCoin i_Mark)
    {
        bool isWin = false;
        for (int col = 0; col < r_Cols && !isWin; col++)
        {
            for (int row = 0; row <= r_Rows - k_NumToWin; row++)
            {
                if (m_Board[row, col] == i_Mark &&
                    m_Board[row + 1, col] == i_Mark &&
                    m_Board[row + 2, col] == i_Mark &&
                    m_Board[row + 3, col] == i_Mark)
                {
                    isWin = true;
                    break;
                }
            }
        }

        return isWin;
    }

    private bool checkDiagonalDown(eCoin i_Mark)
    {
        bool isWin = false;
        for (int row = 0; row < r_Rows - k_NumToWin && !isWin; row++)
        {
            for (int col = 0; col <= r_Cols - k_NumToWin; col++)
            {
                if (m_Board[row, col] == i_Mark &&
                    m_Board[row + 1, col + 1] == i_Mark &&
                    m_Board[row + 2, col + 2] == i_Mark &&
                    m_Board[row + 3, col + 3] == i_Mark)
                {
                    isWin = true;
                    break;
                }
            }
        }

        return isWin;
    }

    private bool checkDiagonalUp(eCoin i_Mark)
    {
        bool isWin = false;
        for (int row = k_NumToWin - 1; row < r_Rows && !isWin; row++)
        {
            for (int col = 0; col <= r_Cols - k_NumToWin; col++)
            {
                if (m_Board[row, col] == i_Mark &&
                    m_Board[row - 1, col + 1] == i_Mark &&
                    m_Board[row - 2, col + 2] == i_Mark &&
                    m_Board[row - 3, col + 3] == i_Mark)
                {
                    isWin = true;
                    break;
                }
            }
        }

        return isWin;
    }

    public void ClearBoard()
    {
        initializeBoard();
    }
}
