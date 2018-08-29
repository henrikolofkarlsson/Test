using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    [SerializeField]
    private int powerupID; // triple shot = 0, speed = 1, shield = 2

    [SerializeField]
    private float speed = 3.0f;


    [SerializeField]
    private AudioClip _clip;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -10) 
        {
            Destroy(this.gameObject);
        }
	}

    // Called on collison with other
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {

            //Access player
            Player player = other.GetComponent<Player>();

            // Null check and enable triple shot
            if (player != null)
            {
                player.PowerUpOn(powerupID);
            }

            // play power up sound by main camera (necessary to use playclipatpoint since we're
            // destroying this gameobject right after). camera position just for loudness.
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            //Destroy self
            Destroy(this.gameObject);
        }
    }
}
