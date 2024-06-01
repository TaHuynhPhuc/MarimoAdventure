using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    private Transform target;
    private float maxhp = 100;  
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _leftBound;
    [SerializeField] private float _rightBound;
    [SerializeField] private float scalex;
    [SerializeField] private float scaley;
    private bool _isMovingLeft = true;
    private bool _isMoving = true;
    public bool playerIsClose;
    [SerializeField] public GameObject danger;
    void Start()
    {
        maxhp = 100;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving && playerIsClose == false)
        {
            _speed = 2;
            danger.SetActive(false);
            var curPosition = transform.localPosition;
            if (curPosition.x > _rightBound)
            {
                _isMovingLeft = true;
            }
            if (curPosition.x < _leftBound)
            {
                _isMovingLeft = false;
            }

            var direction = Vector3.left;
            if (!_isMovingLeft)
            {
                direction = Vector3.right;
            }
            transform.Translate(direction * _speed * Time.deltaTime);
            var localScale = transform.localScale;
            localScale.x = _isMovingLeft ? scalex : scaley;
            transform.localScale = localScale;
        }
        if (playerIsClose)
        {
            danger.SetActive(true);
            _speed = 4f;
            transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
        //if (other.gameObject.CompareTag("Bullet"))
        //{
        //    Destroy(other.gameObject);
        //    maxhp -= 50f;
        //    heathbar.value = maxhp;
        //    if (maxhp <= 0)
        //    {
        //        danger.SetActive(false);
        //        Destroy(gameObject);
        //    }
        //}
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
        //if (other.gameObject.CompareTag("Bullet"))
        //{
        //    Destroy(other.gameObject);
        //    maxhp -= 50f;
        //    heathbar.value = maxhp;
        //    if (maxhp <= 0)
        //    {
        //        danger.SetActive(false);
        //        Destroy(gameObject);
        //    }
        //}
    }

}
