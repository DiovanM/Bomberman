using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePowerUp : MonoBehaviour {

	public GameObject BombAmountUp, BombPowerUp, SpeedUp;

	void OnTriggerExit2D(Collider2D other){
		
		if (other.CompareTag("Explosion")) { //Aguarda a explosão não existir mais para instancear o powerup				
			Invoke("instantiatePowerUp",0.1f);
			//instantiatePowerUp ();
			Invoke("DestroyThis", 0.15f);
		}
	}

	void instantiatePowerUp(){
		int r = Random.Range (0,12);

		if (r == 0) {
			Instantiate (BombAmountUp, transform.position,Quaternion.identity);
		} 
		else if (r == 1) {
			Instantiate (BombPowerUp, transform.position,Quaternion.identity);
		}
		else if (r == 2) {
			Instantiate (SpeedUp, transform.position,Quaternion.identity);
		}
	}

	private void DestroyThis(){
		Destroy (this.gameObject);
	}
}
