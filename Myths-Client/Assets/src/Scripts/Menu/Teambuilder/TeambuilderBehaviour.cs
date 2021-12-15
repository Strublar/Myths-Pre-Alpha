using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeambuilderBehaviour : MonoBehaviour
{
    public TeambuilderMythBehaviour[] myths;
    public SelectedMythPanelBehaviour selectedMyth;
    public SelectedTeamBehaviour team;
    public GameObject leaveButton;

    /*public void InitTeambuilder()
    {
        int index = 0;
        for(int i =0;i<14;i++)
        {
            myths[index].LinkedMyth = Myth.ParseMyth(i);
            myths[index].UpdateMyth();
            index++;
        }
        selectedMyth.EnterTeambuilding();
        UpdateLeaveButton();

    }*/

    public void ExitTeambuilder()
    {
        selectedMyth.ExitTeambuilding();
    }

    public void UpdateLeaveButton()
    {
        bool valid = true;
        foreach(TeambuilderMythBehaviour myth in team.myths)
        {
            if(myth.LinkedMyth == null)
            {
                valid = false;
                break;
            }
        }

        leaveButton.SetActive(valid);
    }

   
}
