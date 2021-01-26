using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeambuilderBehaviour : MonoBehaviour
{
    public TeambuilderMythBehaviour[] myths;
    public SelectedMythPanelBehaviour selectedMyth;
    public SelectedTeamBehaviour team;
    public GameObject leaveButton;

    public void InitTeambuilder()
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

    }

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

    public void Prebuilt1()
    {
        //Fire team.
        team.myths[0].LinkedMyth = Myth.ParseMyth(4);
        team.myths[0].UpdateMyth();
        team.myths[1].LinkedMyth = Myth.ParseMyth(0);
        team.myths[1].UpdateMyth();
        team.myths[2].LinkedMyth = Myth.ParseMyth(3);
        team.myths[2].UpdateMyth();
        team.myths[3].LinkedMyth = Myth.ParseMyth(9);
        team.myths[3].UpdateMyth();
        team.myths[4].LinkedMyth = Myth.ParseMyth(1);
        team.myths[4].UpdateMyth();
        UpdateLeaveButton();
    }

    public void Prebuilt2()
    {
        //Dark Team
        team.myths[0].LinkedMyth = Myth.ParseMyth(8);
        team.myths[0].UpdateMyth();
        team.myths[1].LinkedMyth = Myth.ParseMyth(13);
        team.myths[1].UpdateMyth();
        team.myths[2].LinkedMyth = Myth.ParseMyth(12);
        team.myths[2].UpdateMyth();
        team.myths[3].LinkedMyth = Myth.ParseMyth(11);
        team.myths[3].UpdateMyth();
        team.myths[4].LinkedMyth = Myth.ParseMyth(2);
        team.myths[4].UpdateMyth();
        UpdateLeaveButton();
    }

    public void Prebuilt3()
    {
        //Balance Team
        team.myths[0].LinkedMyth = Myth.ParseMyth(4);
        team.myths[0].UpdateMyth();
        team.myths[1].LinkedMyth = Myth.ParseMyth(8);
        team.myths[1].UpdateMyth();
        team.myths[2].LinkedMyth = Myth.ParseMyth(3);
        team.myths[2].UpdateMyth();
        team.myths[3].LinkedMyth = Myth.ParseMyth(12);
        team.myths[3].UpdateMyth();
        team.myths[4].LinkedMyth = Myth.ParseMyth(1);
        team.myths[4].UpdateMyth();
        UpdateLeaveButton();
    }
}
