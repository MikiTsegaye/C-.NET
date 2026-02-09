using Ex05;

public class Player
{
    private readonly string r_Name;
    private readonly eCoin r_Mark;
    private int m_Score;

    public string Name
    {
        get 
        { 
            return r_Name;
        }
    }

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

    // Updated constructor to accept eCoin
    public Player(string i_Name, eCoin i_Mark)
    {
        r_Name = i_Name;
        r_Mark = i_Mark;
        m_Score = 0;
    }
}