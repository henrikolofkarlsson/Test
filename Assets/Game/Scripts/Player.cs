using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] // Allows designer to change speed, while keeping it private to player (no other script can interfere with it)
	private float speed = 5.0f;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
	}

	// How the user control player movement
	private void Movement () {
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
		transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

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
}
