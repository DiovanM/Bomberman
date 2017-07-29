using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public float speed = 6;
	public static int bombPower; //Variável estática que guarda a força da bomba para ser usada na explosão
	public int bombPowerLimit = 5; //Limite de blocos de 

	void Start(){
		bombPower = 1;
	}


	void FixedUpdate(){
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");	
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (h, v) * speed;

		GetComponent<Animator> ().SetInteger ("x", (int)h);
		GetComponent<Animator> ().SetInteger ("y", (int)v);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "BombUp" ) {
			if(bombPower < bombPowerLimit)
			bombPower++;
		}
	}

}
