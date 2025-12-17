using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace akistd
{
    public static class SaveLoad
    {
        public static void SaveData(List<LevelData> level)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/Finisher.akistd";

            FileStream stream = new FileStream(path, FileMode.Create);

            LevelDataList charData = new LevelDataList(level);
            foreach (var item in charData.lvDataList)
            {
                Debug.LogError("Saved " + item.sceneCode + item.unlocked);
            }

            formatter.Serialize(stream, charData);
            stream.Close();

            
        }

        public static LevelDataList LoadData()
        {
            string path = Application.persistentDataPath + "/Finisher.akistd";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                LevelDataList data = formatter.Deserialize(stream) as LevelDataList;

                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Error: Save file not found in " + path);

                return null;
            }
        }

        public static void SaveGameSettings(string settings)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/Finisher_settings.akistd";

            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, settings);
            stream.Close();

            Debug.Log("Saved Settings");
        }

        public static void SaveGameSettingsFirstTime(string settings)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/Finisher_settings.akistd";

            FileStream stream = new FileStream(path, FileMode.Append);

            formatter.Serialize(stream, settings);
            stream.Close();


            Debug.Log("Saved Settings");
        }

        public static string LoadGameSettings()
        {

            string path = Application.persistentDataPath + "/Finisher_settings.akistd";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                string data = formatter.Deserialize(stream) as string;

                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Error: Save file not found in " + path);

                return null;
            }
        }

        /*public static void SaveSettingData(List<LevelData> level)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/Finisher.akistd";

            FileStream stream = new FileStream(path, FileMode.Create);

            LevelDataList charData = new LevelDataList(level);

            formatter.Serialize(stream, charData);
            stream.Close();
        }*/

    }
}
