using UnityEngine;

//Here you can see that this class implements the data handler interface and also inherits from MonoBehaviour
public class ScoreManager : MonoBehaviour, IDataHandler
{
    //Create a static entry point to get an object with context
    public static ScoreManager instance {  get; private set; }

    public int HighScore { get; private set; }
    public int CurrentScore { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
        CurrentScore = 0;
        HighScore = 0;
    }

    //For this object, the IDataHandler.LoadData(PersistentData data) it's just loading the highscore into its own var
    public void LoadData(PersistentData data)
    {
        HighScore = data.HighScore;
    }

    //For this object, the IDataHandler.SaveData(ref PersistentData data) it checks if current score is a new high score and then save it

    public void SaveData(ref PersistentData data)
    {
        if(CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
        }

        data.HighScore = HighScore;
    }

    public void AddScore()
    {
        CurrentScore += 100;
    }
}
