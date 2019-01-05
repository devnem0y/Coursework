using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.IO;

public class LevelManager
{
    private string path; // создать путь для каждого уровня

    public List<SaveabelObject> levelElements = new List<SaveabelObject>();

    public LevelManager()
    {
        // файл по дефолту (первый уровень)
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "testLevel.xml");
#else
        path = Path.Combine(Application.dataPath, "testLevel.xml");
#endif
    }

    public void Save()
    {
        XElement root = new XElement("root");

        levelElements.ForEach(el => root.Add(el.GetElement()));

        XDocument saveDoc = new XDocument(root);
        File.WriteAllText(path, saveDoc.ToString());
    }

    public void Load(string levelName)
    {

    }
}
