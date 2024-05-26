using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirectoryPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirectoryPath, string dataFileName)
    {
        this.dataDirectoryPath = dataDirectoryPath;
        this.dataFileName = dataFileName;
    }

    public PersistentData Load()
    {
        PersistentData loadedData = null;

        //Here we generaate the full path of the saved file. We use that method so it works in different OS
        string fullPath = Path.Combine(dataDirectoryPath, dataFileName);

        if (File.Exists(fullPath))
        { 
            //Prevent a crash in case we get some error while opening the file and reading it
            try
            {
                //Here we will store what we find in the file
                string dataToLoad = "";

                //Open the file
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
                {
                    //Read the file
                    using(StreamReader reader =  new StreamReader(fileStream)) 
                    {
                        //Save readed data into the string
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //Deserialize string data into our class
                loadedData = JsonUtility.FromJson<PersistentData>(dataToLoad);
            }
            catch (Exception exception)
            {
                Debug.LogError("Unable to load from file: " + fullPath + ". \n Exception thrown: " + exception);
            }
        }
        return loadedData;
    }

    public void Save(PersistentData data) 
    {
        //Here we generaate the full path of the saved file. We use that method so it works in different OS
        string fullPath = Path.Combine(dataDirectoryPath, dataFileName);

        //Prevent a crash in case we get some error while opening the file and saving it
        try
        {
            //First we create the file (in case it doesn't exist)
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Then we serialize our data to a Json. This will convers any System.Serializable into a Json field
            string SerializedData = JsonUtility.ToJson(data, true);

            //Then we write the file
            //First we open a fileStream (inside this using we have acces to the file)
            using(FileStream fileStream = new FileStream(fullPath, FileMode.Create)) 
            {
                //Then we open a write stream (a stream of data tha will be written into the file)
                using(StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.Write(SerializedData);
                }
            }
        }
        catch(Exception exception) 
        {
            Debug.LogError("Unable to save to file: " + fullPath + ". \n Exception thrown: " + exception);
        }
    }
}
