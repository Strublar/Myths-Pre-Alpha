using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageBubbleBehaviour : MonoBehaviour
{
    public TextMeshPro bubble;
    public bool isActive = false;
    public float timer = 0;
    public int bar;//0 for hp, 1 for armor, 2 for barrier

    public void Init(int damage)
    {
        bubble.text = damage.ToString();
        if(damage > 0)
        {
            if(bar == 0)
            {
                bubble.color = new Color32(0, 255, 0, 255);
            }
            
            gameObject.SetActive(true);
        }
        else if(damage < 0)
        {
            if (bar == 0)
            {
                bubble.color = new Color32(255, 0, 0, 255);
            }
            gameObject.SetActive(true);
        }
    }

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
