using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image health1;
    public Image health2;
    public Image health3;
    private PlayerController player;

    private void Start()
    {
        player = GameObject.FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        if(player.heath == 2)
        {
            health3.color = Color.black;
        }else if (player.heath == 1)
        {
            health2.color = Color.black;
        }else if (player.heath <= 0)
        {
            health1.color = Color.black;
        }
    }
}
