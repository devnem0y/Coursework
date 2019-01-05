using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class SaveabelObject : MonoBehaviour // Скрипт на объект, который нужно сохранять
{
    private LevelManager ls;
    public string elementName;

    private void Awake()
    {
        ls = FindObjectOfType<LevelScreen>().GetLevelManager();
    }

    private void Start()
    {
        ls.levelElements.Add(this);
    }

    private void OnDestroy()
    {
        ls.levelElements.Remove(this);
    }

    public XElement GetElement()
    {
        XAttribute posX = new XAttribute("posX", transform.position.x);
        XAttribute posY = new XAttribute("posY", transform.position.y);
        XAttribute posZ = new XAttribute("posZ", transform.position.z);
        XAttribute rotX = new XAttribute("rotX", transform.rotation.eulerAngles.x);
        XAttribute rotY = new XAttribute("rotY", transform.rotation.eulerAngles.y);
        XAttribute rotZ = new XAttribute("rotZ", transform.rotation.eulerAngles.z);

        return new XElement("instance", elementName, posX, posY, posZ, rotX, rotY, rotZ);
    }
}