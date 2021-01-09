using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTeamBehaviour : MonoBehaviour
{
    public TeambuilderMythBehaviour[] myths;
    public SelectedMythPanelBehaviour selectedMyth;
    // Start is called before the first frame update
    void Start()
    {
        /*myths[0].LinkedMyth = Myth.ParseMyth(4);
        myths[1].LinkedMyth = Myth.ParseMyth(0);
        myths[2].LinkedMyth = Myth.ParseMyth(1);
        myths[3].LinkedMyth = Myth.ParseMyth(3);
        myths[4].LinkedMyth = Myth.ParseMyth(9);
        myths[0].UpdateMyth();
        myths[1].UpdateMyth();
        myths[2].UpdateMyth();
        myths[3].UpdateMyth();
        myths[4].UpdateMyth();*/
    }

    public void UpdateSelectedMyth(Myth myth)
    {
        selectedMyth.gameObject.SetActive(true);
        selectedMyth.LinkedMyth = myth;
        selectedMyth.UpdatePanel();
    }

    public void AddMyth(Myth myth)
    {
        foreach(TeambuilderMythBehaviour mythBehaviour in myths)
        {
            if(mythBehaviour.LinkedMyth == null)
            {
                mythBehaviour.LinkedMyth = myth;
                mythBehaviour.UpdateMyth();
                break;
            }
        }
    }
}
