using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public float speed = 3;
	public float speedLimit = 8;
	public float speedUpValue = 0.2f;
	public static int bombPower =1;
	public int bombPowerLimit = 5;
	public static int bombAmount =1;
	public int bombAmountLimit = 5;
	public static bool live;

	void Start(){			
		live = true;
	}

	void FixedUpdate(){	
		if (live == true) {
			Move ();
		} else {
			GetComponent<Rigidbody2D> ().constraints = 
			RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
			GetComponent<Animator> ().SetBool("Death", true);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("BombPowerUp")) { // Aumenta o range da explosão, se o atual for menor que o limite
			if (bombPower < bombPowerLimit) {		 
				bombPower++;
			}	Destroy (other.gameObject);
		}else if (other.gameObject.CompareTag ("BombAmountUp")) { //Aumenta a quantidade de bombas, se a atual for menor que o limite
			if (bombAmount < bombAmountLimit) {
				bombAmount++;
			}	Destroy (other.gameObject);
		}else if (other.gameObject.CompareTag ("SpeedUp")) { //Aumenta a velocidade do personagem, se a atual for menor que o limite
			if (speed < speedLimit) {		
				speed += speedUpValue;
			}	Destroy (other.gameObject);
		}
	}

	void Move(){
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");	
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (h, v) * speed;

		GetComponent<Animator> ().SetInteger ("x", (int)h);
		GetComponent<Animator> ().SetInteger ("y", (int)v);
	}

	public void DestroyMe(){
		Destroy (gameObject);
	}

}
