using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private AudioClip _clip;

    private UIManager _UIManager;


	// Use this for initialization
	void Start () {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // when the enemy leaves screen, respawn above at random x
        if (transform.position.y < -7)
        {
            float randomX = Random.Range(-9.0f, 9.0f);
            transform.position = new Vector3(randomX, 8, 0);
        }
	}

    // Handles collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if enemy collides with player this gets executed
        if (other.tag == "Player") {
            Player player = other.GetComponent<Player>();

            if (player != null) // null check
            {
                player.Damage(); // if enemy collides with player, damage player
            }

            // play explosion animation and destroy enemy object
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            // play explosion sound by main camera (necessary to use playclipatpoint since we're
            // destroying this gameobject right after.
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f); 

            Destroy(this.gameObject);
        }

        // if enemy gets hit by laser beam, this gets executed.
        else if (other.tag == "Laser") {
            if (other.transform.parent != null) // if laser has a parent, destroy it not to clutter memory
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject); // destroy laser

            // play explosion, increment score and destroy enemy object
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _UIManager.UpdateScore();

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
    }
}
