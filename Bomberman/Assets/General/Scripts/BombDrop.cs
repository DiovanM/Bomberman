using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDrop : MonoBehaviour {

	public GameObject bombPrefab;
	GameObject[] bombs;
	private bool canDrop = true;

	void Update () { //Verifica se a quantidade atual de bombas no cenário não ultrapassa o limite de bombas do player
		bombs = GameObject.FindGameObjectsWithTag("Bomba");
		if (bombs.Length < PlayerBehaviour.bombAmount && canDrop) {
			bombDrop ();
		}
	}

	void OnTriggerStay2D(Collider2D other){ // Verifica quando o Player está encima da bomba
		if (other.CompareTag ("Bomba") || other.CompareTag("BossBomb")) {
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


