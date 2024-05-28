using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint; // Điểm xuất phát của mũi tên
    public GameObject arrowPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        // Tạo mũi tên tại vị trí và hướng của firePoint
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }
}
