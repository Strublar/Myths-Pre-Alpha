using Myths_Library;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Manager : MonoBehaviour
{
    public static Manager m;

    private void Awake()
    {
        m = this;
    }

    public void Start()
    {
        //End Test
        Debug.Log("Loading Myths...");
        Myth[] myths = Resources.LoadAll<Myth>("Data/Myths/");
        Debug.Log(myths.Length + " myths loaded");
        XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(MythDefinition));
        Myth.currentId = 0;
        foreach(Myth myth in myths)
        {
            myth.id = Myth.GetNextId();
            Debug.Log("Serializing "+myth.mythName + "");

            MythDefinition def = myth.BuildDefinition();

            
            XmlWriter writer = XmlWriter.Create(Application.dataPath + "/xml/Myths/" + myth.id +".xml");
            serializer.Serialize(writer, def);

            Debug.Log("XML file updated");
        }
        Debug.Log("All Myths serialized");
        
    }


}
