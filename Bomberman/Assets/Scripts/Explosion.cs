using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {


	void Start () {	
		
	}

	void OnTriggerEnter2D(Collider2D other){				
		//Debug.Log(other.gameObject.name);
		if (other.gameObject.CompareTag("Destroyable")) {
			Destroy (other.gameObject);
		}

	}

	public void DestroyMe(){
		Destroy (gameObject);
	}

}
