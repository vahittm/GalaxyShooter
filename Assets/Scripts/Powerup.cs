using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -4.5f)
        {
            Destroy(gameObject);
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
