using Ex05;
using System;
using System.Collections.Generic;

public class ComputerBot
{
    private readonly Random r_Random = new Random();
    private readonly eCoin r_Mark;
    private int m_Score;

    public eCoin Mark
    {
        get
        { 
            return r_Mark;
        }
    }

    public int Score
    {
        get 
        {
            return m_Score; 
        }
        set 
        { 
            m_Score = value;
        }
    }

    public ComputerBot(eCoin i_Mark)
    {
        r_Mark = i_Mark;
        m_Score = 0;
    }

    public int GetMove(BoardLogic i_Board)
    {
        List<int> availableColumns = new List<int>();
        int chosenCol = -1;

        for (int col = 0; col < i_Board.Cols; col++)
        {
            if (i_Board.GetCellContent(0, col) == eCoin.Empty)
            {
                availableColumns.Add(col);
            }
        }

        if (availableColumns.Count > 0)
        {
            int randomIndex = r_Random.Next(availableColumns.Count);
            chosenCol = availableColumns[randomIndex];
        }

        return chosenCol;
    }
}