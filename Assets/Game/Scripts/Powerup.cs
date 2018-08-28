﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    [SerializeField]
    private int powerupID; // triple shot = 0, speed = 1, shield = 2

    [SerializeField]
    private float speed = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
	}

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
                
            //Destroy self
            Destroy(this.gameObject);
        }
    }
}
