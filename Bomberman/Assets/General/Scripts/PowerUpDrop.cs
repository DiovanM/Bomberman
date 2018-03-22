using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDrop : MonoBehaviour {

	public GameObject InstantiatePowerUp;

	void OnDestroy(){
		Instantiate (InstantiatePowerUp, transform.position, Quaternion.identity);
	}

}
