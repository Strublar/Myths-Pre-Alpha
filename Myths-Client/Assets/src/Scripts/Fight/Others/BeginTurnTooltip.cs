using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginTurnTooltip : MonoBehaviour
{
    private float timer;
    public void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
