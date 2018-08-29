using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

	// this script makes sure explosion effects are cleaned out of memory after they are done
	void Start () {
        Destroy(this.gameObject, 4.0f); // the additional arg is a time delay of 4 s
	}
	
}
