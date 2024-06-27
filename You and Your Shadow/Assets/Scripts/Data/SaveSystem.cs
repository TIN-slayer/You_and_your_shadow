using Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Data
{
    public static class SaveSystem
    {
        private static string path = Application.persistentDataPath + "/levels.data";
        private static BinaryFormatter bf = new BinaryFormatter();

        public static void SaveProgress(LevelsData levelsData)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            bf.Serialize(stream, levelsData);
            stream.Close();
        }

        public static LevelsData LoadProgress()
        {
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                LevelsData levelsData = bf.Deserialize(stream) as LevelsData;
                stream.Close();
                return levelsData;
            }
            else
            {
                LevelsData levelsData = new LevelsData();
                SaveProgress(levelsData);
                return levelsData;
            }
        }

        public static void DeleteSaveFile()
        {
            File.Delete(path);
        }
    }
}
