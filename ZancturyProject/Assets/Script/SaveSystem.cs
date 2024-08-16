using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveZanctuary()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        ZanctuaryData data = new ZanctuaryData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ZanctuaryData LoadZanctuary()
    {
        string path = Application.persistentDataPath + "/save.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ZanctuaryData data = formatter.Deserialize(stream) as ZanctuaryData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }


}
