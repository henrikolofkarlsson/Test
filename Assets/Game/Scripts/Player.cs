using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
	private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleLaserPrefab;

    [SerializeField]
    private GameObject _playerExplosionPrefab;

    [SerializeField]
    private GameObject _ShieldGameObject;

    [SerializeField]
    private GameObject[] _engine;

    [SerializeField]
    private float _fireRate = 0.2f;
    [SerializeField] // Lets us change variable in Unity, but no other script can interfere with it
    private float _speed = 5.0f;

    private float _canFire = 0;
    public bool canTripleShoot = false;
    public bool hasShield = false;
    public int lives = 3;

    private UIManager _UIManager; // To hold connection to uimanager script
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audiosource;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, 0, 0);

        // Find the object that holds the UIManager (canvas) and get access to
        // its UIManager script. Etc.
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        _audiosource = GetComponent<AudioSource>();

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (_UIManager != null) // Always null check, to not crash of unable to find object
        {
            _UIManager.UpdateLives(lives);
        }

        if (_spawnManager != null) 
        {
            _spawnManager.startSpawning();
        }

	}
	
	// Update is called once per frame
	void Update () {
		Movement();

		if (Input.GetKeyDown(KeyCode.Space))
		{
            if (canTripleShoot)
                tripleShoot();
            else
                Shoot();
		}
	}

    //Spawn lasers 
    private void Shoot () {
        if (Time.time > _canFire)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.84f, 0), Quaternion.identity);
            _audiosource.Play();
            _canFire = Time.time + _fireRate;
        }
    }

    //Spawn three lasers
    private void tripleShoot () {
        if (Time.time > _canFire)
        {
            Instantiate(_tripleLaserPrefab, transform.position + new Vector3(-0.55f, 0, 0), Quaternion.identity);
            _audiosource.Play();
            _canFire = Time.time + _fireRate;
        }
    }

    // Turn on powerup and start coroutine to turn it off in 5 s
    public void PowerUpOn(int powerupID)
    {
        if (powerupID == 0)
        {
            canTripleShoot = true;
            StartCoroutine(PowerDownRoutine(powerupID));
        }
        else if (powerupID == 1)
        {
            _speed = 8.0f;
            StartCoroutine(PowerDownRoutine(powerupID));
        }
        else if (powerupID == 2)
        {
            hasShield = true;
            _ShieldGameObject.SetActive(true);
            StartCoroutine(PowerDownRoutine(powerupID));
        }

    }

    // Turn of powerup after 5 seconds. IEnumerator and yield return are always
    // needed in coroutines. The routine gets started elsewhere (in powerupon)
    IEnumerator PowerDownRoutine(int powerupID)
    {
        yield return new WaitForSeconds(5.0f);

        if (powerupID == 0)
        {
            canTripleShoot = false;
        }
        else if (powerupID == 1)
        {
            _speed = 5.0f;
        }
        else if (powerupID == 2)
        {
            hasShield = false;
            _ShieldGameObject.SetActive(false);
        }

    }

    // How the user control player movement

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 10.6)
        {
            transform.position = new Vector3(-10.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -10.6)
        {
            transform.position = new Vector3(10.5f, transform.position.y, 0);
        }
    }

    public void Damage () {
        if (hasShield) {
            hasShield = false;
            _ShieldGameObject.SetActive(false);
            return;
        }

        lives--;
        _UIManager.UpdateLives(lives); // update live count on screen

        if (lives == 2) {
            _engine[0].SetActive(true);
        }
        else if (lives == 1) {
            _engine[1].SetActive(true);
        }
        else if (lives < 1)
        {
            if (_gameManager != null)
            {
                _gameManager.gameOver = true;
            }

            Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity); // explode player
            Destroy(this.gameObject);
        }
    }
}
