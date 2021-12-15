using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public UnityEngine.UI.Toggle autopass;
    public EndTurnButton endTurnButton;
    public UnityEngine.UI.Text text;
    public float timer;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        UpdateTimerText();
        if(timer <= 0  && GameManager.gm.currentPlayer == GameManager.gm.localPlayerId &&
            autopass.isOn)
        {
            endTurnButton.OnClick();
            
        }
    }

    public void ResetTimer()
    {
        timer = 120;
    }

    public void UpdateTimerText()
    {
        text.text = ((int)timer).ToString() ;
    }
}
