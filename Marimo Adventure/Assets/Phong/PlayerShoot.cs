using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint; // Điểm xuất phát của mũi tên
    public GameObject arrowPrefab;
    private bool facingRight = true;
    public Animator anim;
    public float cooldownTime = 1f;
    private float nextFireTime = 0f;
    private PlayerAudio playerAudio;
    private Animator animator;
    public int arrowDamage = 1; // Thêm biến damage cho mũi tên

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<PlayerAudio>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + cooldownTime; // Đặt thời gian tiếp theo có thể bắn
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            facingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            facingRight = true;
        }
        UpdateFirePointDirection();

    }

    void Shoot()
    {
        // Tạo mũi tên tại vị trí và hướng của firePoint
        anim.SetTrigger("attack");
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        arrow.GetComponent<Arrow>().damage = arrowDamage; // Truyền damage cho mũi tên
        PlayerAudio.instance.PlaySFX("Shoot");
    }

    void UpdateFirePointDirection()
    {
        // Xoay firePoint theo hướng của người chơi
        if (facingRight)
        {
            firePoint.rotation = Quaternion.Euler(0, 0, 0); // Hướng về phía bên phải
        }
        else
        {
            firePoint.rotation = Quaternion.Euler(0, 180, 0); // Hướng về phía bên trái
        }
    }
}
