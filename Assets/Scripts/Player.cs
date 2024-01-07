using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour

{
    [FormerlySerializedAs("_speed")] [SerializeField] 
    private float speed = 3.5f;
    [FormerlySerializedAs("_laserPrefab")] [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    
    [FormerlySerializedAs("_fireRate")] [SerializeField]
    private float fireRate = 0.5f;

    private float _canFire = -1f;
    [FormerlySerializedAs("_lives")] [SerializeField]
    private int lives = 3;

    private SpawnManager _spawnManager;
    [SerializeField]

    private bool _IsTripleShotActive = false;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3.8f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
      CalculateMovement();
      if (Input.GetKeyDown(KeyCode.Space)&& Time.time> _canFire)
      {
          Firelaser();
      }

     
      
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // transform.Translate(Vector3.right * (horizontalInput * _speed * Time.deltaTime));
        //transform.Translate(Vector3.up * (verticalInput * _speed * Time.deltaTime));
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        Transform transform1;
        (transform1 = transform).Translate(direction * (speed * Time.deltaTime));

        var position = transform1.position;
        position = new Vector3(position.x, Mathf.Clamp(position.y, -3.8f, 0f),0);
        transform.position = position;

        if (transform.position.x > 11.3f)


        {
            var transform2 = transform;
            transform2.position = new Vector3(-11.3f, transform2.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            var transform2 = transform;
            transform2.position = new Vector3(11.3f, transform2.position.y, 0);
        }
    }

    void Firelaser()
    {
        _canFire = Time.time + fireRate;  
        if (_IsTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab,transform.position + new Vector3(0,1.05f,0),Quaternion.identity);

        }
        
    }

    public void Damage()

    {  
        lives -=1;
         
        if (lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }
    
}
            
                
            
        
        
    

