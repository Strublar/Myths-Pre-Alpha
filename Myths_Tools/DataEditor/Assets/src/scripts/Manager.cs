using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        Debug.Log("Loading Myths...");
        Myth[] myths = Resources.LoadAll<Myth>("Data/Myths/");
        Debug.Log(myths.Length + " myths loaded");
        string mythsJson="";
        Myth.currentId = 0;
        foreach(Myth myth in myths)
        {
            myth.id = Myth.GetNextId();
            Debug.Log("Serializing "+myth.mythName + "");
            mythsJson = myth.ToJson();
            //Debug.Log(mythsJson);
            File.AppendAllText(Application.dataPath + "/json/Myths/"+myth.id+".json", mythsJson);
            Debug.Log("Json file updated");
        }
        Debug.Log("All Myths serialized");
        
    }


}
