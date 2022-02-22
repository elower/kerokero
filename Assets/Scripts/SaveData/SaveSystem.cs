using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

public static class SaveSystem
{
    public static void save(Data data) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveData.kero";

        //open file stream
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(data);

        //insert into filestream and close
        formatter.Serialize(stream, playerData);
        stream.Close();
    }
    public static void saveWithoutTime(Data data) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveData.kero";

        //open file stream
        FileStream stream = new FileStream(path, FileMode.Create);
        DateTime lastLoggedTime = data.lastLoggedTime;

        PlayerData playerData = new PlayerData(data);
        playerData.lastLoggedTime = lastLoggedTime;


        //insert into filestream and close
        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData load() {
        string path = Application.persistentDataPath + "/saveData.kero";

        //open file stream
        FileStream stream = new FileStream(path, FileMode.Open);
        if(File.Exists(path) && stream.Length > 0) {
            BinaryFormatter formatter = new BinaryFormatter();


            // Fetch data and close
            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return playerData;
        } else {
            Debug.LogError("Save File Not Found!");
            return null;
        }


    }
}
