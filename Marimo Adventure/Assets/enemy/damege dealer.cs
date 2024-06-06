using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damegedealer : MonoBehaviour
{
    [SerializeField] private int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>() != null)
                other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
        
}
