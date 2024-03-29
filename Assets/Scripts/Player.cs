using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour

{
    [FormerlySerializedAs("_speed")] [SerializeField] 
    private float speed = 3.5f;
    private float speedMultiplier = 2;
    
    [FormerlySerializedAs("_laserPrefab")] [SerializeField]
    private GameObject laserPrefab;
    
    [SerializeField]
    private GameObject _tripleShotPrefab;
    
    [FormerlySerializedAs("_fireRate")] [SerializeField]
    private float fireRate = 0.5f;

    private float _canFire = -1f;
    [FormerlySerializedAs("_lives")] 
     
    [SerializeField]
    private int lives = 3;

    [SerializeField] 
    private int score;

    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _IsTripleShotActive ;
    private bool _isSpeedBoostActive ;
    private bool _IsShieldAcitve;
    [SerializeField]
    private GameObject _shieldVisualizer;

    private UI_Manager UIManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3.8f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        UIManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        
        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is null");
        }

        if (UIManager ==null)
            
        {
            Debug.LogError("the ui manager is null");
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
        if (_IsShieldAcitve)
        {
            _IsShieldAcitve = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        lives -=1;
        
        UIManager.updatelives(lives); 
        if (lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()

    {
        _IsTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _IsTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        speed /= speedMultiplier;
    }

    public void ShieldActive()
    {
        _IsShieldAcitve = true;
        _shieldVisualizer.SetActive(true);
    }

    public void Addscore(int points)
    {
        score += points; 
        UIManager.updatescore(score);
    }
}
    
            
                
            
        
        
    

