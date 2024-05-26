using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("Data storage config")]
    [SerializeField]
    string FileName = "SaveGame";

    //Create a static entry point to get an object with context
    public static DataPersistanceManager Instance {  get; private set; }

    private PersistentData GameData;
    private FileDataHandler fileDataHandler;
    private void Awake()
    {
        //Ensure that our instance is unique
        if(Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        //Ensure singleton persistance
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, FileName);
        //On game start we load the data into the game
        //(we do that on start because here all entities and components already exists)
        LoadData();
    }

    //Save game before application closes
    private void OnApplicationQuit()
    {
        SaveData();    
    }

    //This method is a brute force approach to find all data handlers, there are better solutions!
    private List<IDataHandler> GetSceneDataHandlers()
    {
        IEnumerable<IDataHandler> dataHandlers = 
            FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IDataHandler>();
        return new List<IDataHandler>(dataHandlers);
    }

    public void NewGame()
    {
        GameData = new PersistentData();
    }
    public void SaveData()
    {
        //Pass the data object to any component that needs to update it
        foreach(IDataHandler handler in GetSceneDataHandlers())
        {
            handler.SaveData(ref GameData);
        }

        //Save that data into a file
        fileDataHandler.Save(GameData);
    }

    public void LoadData() 
    {
        //First we try to load data from a file
        GameData = fileDataHandler.Load();
        //if that didn't work (it's the first time opening the game for example) we load the default data
        if (GameData == null)
        {
            Debug.LogWarning("No Persistent Data is found. Creating a default one!");
            NewGame();
        }

        //We pass the loaded data to any component that needs it
        foreach(IDataHandler handler in GetSceneDataHandlers())
        {
            handler.LoadData(GameData);
        }
    }
}
