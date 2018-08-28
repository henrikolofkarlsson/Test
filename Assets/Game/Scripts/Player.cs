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
    private float _fireRate = 0.2f;

    private float _canFire = 0;

    public bool canTripleShoot = false;

	[SerializeField] // Allows designer to change speed in Unity, while keeping it private to player (no other script can interfere with it)
	private float _speed = 5.0f;

    public int lives = 3;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, 0, 0);
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
            _canFire = Time.time + _fireRate;
        }
    }

    //Spawn three lasers
    private void tripleShoot () {
        if (Time.time > _canFire)
        {
            Instantiate(_tripleLaserPrefab, transform.position + new Vector3(-0.55f, 0, 0), Quaternion.identity);
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
            // apply shield
        }

    }

    // Turn of powerup after 5 seconds
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
            // apply shield
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
        lives--;
        if (lives < 1)
        {
            Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }
}
