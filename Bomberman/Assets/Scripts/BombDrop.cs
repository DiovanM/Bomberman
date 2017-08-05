using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDrop : MonoBehaviour {

	public GameObject bombPrefab;
	GameObject[] bombs;
	private bool canDrop = true;

	// Update is called once per frame
	void Update () {
		bombs = GameObject.FindGameObjectsWithTag("Bomba");
		if (bombs.Length < PlayerBehaviour.bombAmount && canDrop) {
			bombDrop ();
		}
	}

	void OnTriggerStay2D(Collider2D other){ // Verifica quando o Player está de cima da bomba
		if (other.CompareTag ("Bomba")) {
			canDrop = false;
		}
	}
	void OnTriggerExit2D(Collider2D other){ // Verifica quando o Player sai de cima da bomba
		if (other.CompareTag ("Bomba")) {
			canDrop = true;
		}
	}

	void bombDrop(){
		if (Input.GetKeyDown ("space") && PlayerBehaviour.live == true) {
			Vector2 pos = transform.position;
			pos.x = (Mathf.Round (pos.x));
			pos.y = (Mathf.Round (pos.y));
			Instantiate (bombPrefab, pos, Quaternion.identity);
		}
	}

}


