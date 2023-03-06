using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mentor.Game;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PersistanceManager : MonoBehaviour
{
    public const string HP_STAT_KEY = "HP";
    public const string SAVE_FILE_NAME = @"\SaveGame.Dat";
    public static int currentLevel = 3;
    public PlayerController playerController;
    public KeyCode saveButton, loadButton;

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(saveButton))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(loadButton))
        {
            LoadGame();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }  

    private void SaveGame()
    {
        //Player Prefs
        //  PlayerPrefs.SetInt(HP_STAT_KEY, playerController.stats.HP);


        //Json Save
        //    string characterStatsJson = JsonUtility.ToJson(playerController.transform.po, true);
        //TextWriter writer = null;
        //    writer = new StreamWriter(Application.persistentDataPath + SAVE_FILE_NAME);
        //    writer.Write(characterStatsJson);
        //    if (writer != null)
        //        writer.Close();

        SerializedSaveGame serializedSaveGame = new SerializedSaveGame();
        serializedSaveGame.characterStats = playerController.stats;
        serializedSaveGame.characterStats.HP += 10;
        serializedSaveGame.enemiesInfos = new List<EnemiesInfo>() { 
            new EnemiesInfo { position = (SerializedVector3) new Vector2(1,1), type = 2},
        new EnemiesInfo{ position = (SerializedVector3)new Vector2(8,32), type = 1}};
        serializedSaveGame.completedQuests = new List<string>() { "The Mighty Quest For Epic Loot",
             " Kill the alien"};
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Open(Application.persistentDataPath  + SAVE_FILE_NAME, FileMode.OpenOrCreate);
        bf.Serialize(fileStream, serializedSaveGame);
        fileStream.Close();

        Debug.Log("Saved!");

    }


    private void LoadGame()
    {
        //TextReader reader = null;

        //reader = new StreamReader(Application.persistentDataPath + SAVE_FILE_NAME);
        //string fileContents = reader.ReadToEnd();

        //playerController.stats = JsonUtility.FromJson<CharacterStats>(fileContents);


        //if (reader != null)
        //    reader.Close();

        FileStream fileStream = File.Open(Application.persistentDataPath + SAVE_FILE_NAME,
            FileMode.Open, FileAccess.Read);

        if (fileStream != null)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SerializedSaveGame saveGame = (SerializedSaveGame)binaryFormatter.Deserialize(fileStream);
            playerController.stats = saveGame.characterStats;
            fileStream.Close();
        }

    }

}

[Serializable]
public class SerializedVector3
{
    public float x, y, z = 5f;

    public static explicit operator SerializedVector3(Vector3 fromVector)
    {
        return new SerializedVector3
        {
            x = fromVector.x,
            y = fromVector.y,
            z = fromVector.z
        };
    }

    public static explicit operator SerializedVector3(Vector2 fromVector)
    {
        return new SerializedVector3
        {
            x = fromVector.x,
            y = fromVector.y
        };
    }


    public static SerializedVector3 operator +(SerializedVector3 firstVector,
        SerializedVector3 secondVector)
    {
        return new SerializedVector3
        {
            x = firstVector.x + secondVector.x,
            y = firstVector.y + secondVector.y,
            z = firstVector.z + secondVector.z
        };
    }

    public static SerializedVector3 operator +(SerializedVector3 firstVector,
       Vector2 secondVector)
    {
        return new SerializedVector3
        {
            x = firstVector.x + secondVector.x,
            y = firstVector.y + secondVector.y,
        };
    }
}
