//This class will store the information that we want to save to the disk

//making it serializable means that the system will know how to convert this class into a stream that can be saved
//in different formats. In our case I will use a Json file type.
[System.Serializable]
public class PersistentData
{
    public int HighScore = 0;

    //We add a constructor just to ensure we get defaul values before we load any actual data
    //That way system wont broke if we try to load an inexistent file
    public PersistentData()
    {
        HighScore = 0;
    }
}
