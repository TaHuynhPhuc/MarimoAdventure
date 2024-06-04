using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float damage;
    [SerializeField] private float maxhp;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float basespeed;
    [SerializeField] private float maxspeed;
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
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving && !playerIsClose)
        {
            _speed = basespeed;
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
            _speed = maxspeed;
            transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
        if (other.gameObject.CompareTag("Arrow"))
        {
            Destroy(other.gameObject);
            maxhp -= damage;
            if (maxhp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
