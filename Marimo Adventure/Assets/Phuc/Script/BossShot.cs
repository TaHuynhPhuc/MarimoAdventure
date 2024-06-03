using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        //vi tri player nó sẽ bắn

        float distance = Vector2.Distance(transform.position, player.transform.position);

        Debug.Log(distance);
        //neu vi tri ban xa
        if(distance < 5)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }

        
    }

     void shoot()
    {
        Instantiate(bullet, bulletPos.position,Quaternion.identity);
    }
}
