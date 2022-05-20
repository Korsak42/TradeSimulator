using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HexMapEditor : MonoBehaviour
{
    public void Save()
    {
        string path = System.IO.Path.Combine(Application.persistentDataPath, "test.map");
        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            

        }
    }

    public void Load()
    {
        string path = System.IO.Path.Combine(Application.persistentDataPath, "test.map");
        using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
        {
            Debug.Log(reader.ReadInt32());
        }
    }


}
