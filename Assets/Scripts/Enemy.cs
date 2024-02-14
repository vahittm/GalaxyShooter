using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player palyer;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 5.8f, 0);

        palyer = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        if (transform.position.y < -5f)
        {
            float randomx = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomx, 7, 0);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if (palyer != null)
            {
                palyer.Addscore(10);
            }
            Destroy(this.gameObject);
        }
    }
}

