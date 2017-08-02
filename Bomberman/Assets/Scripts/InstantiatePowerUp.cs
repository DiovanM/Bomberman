using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePowerUp : MonoBehaviour {

	public GameObject BombAmountUp, BombPowerUp, SpeedUp;

	void OnTriggerExit2D(Collider2D other){
		//Debug.Log ("a");
		if (other.CompareTag("Explosion")) {
			Debug.Log ("a");
			instantiatePowerUp ();
			Destroy (gameObject);
		}
	}

	void instantiatePowerUp(){
		int r = Random.Range (0, 10);

		if (r == 7) {
			Instantiate (BombAmountUp, transform.position,Quaternion.identity);
		} else if (r == 8) {
			Instantiate (BombPowerUp, transform.position,Quaternion.identity);

		}
		else if (r == 9) {
			Instantiate (SpeedUp, transform.position,Quaternion.identity);
		}

	}
}
