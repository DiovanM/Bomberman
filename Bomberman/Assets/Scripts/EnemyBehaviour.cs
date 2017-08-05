using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	Vector2 pos;
	public float speed;
	public LayerMask ExplosionStop;
	private int[] direction = new int[4];

	void Start () {
		pos = transform.position;
		VerifyDirections ();
		Move(naosei (direction));
	}	

	void FixedUpdate () {		 
		if (pos.x + 1 >= transform.position.x || pos.y + 1 >= transform.position.y
		||	pos.x - 1 <= transform.position.x || pos.y - 1 <= transform.position.y) {
			VerifyDirections ();
			Move(naosei (direction));
		} 
	}

	void VerifyDirections(){
		RaycastHit2D hitUp = Physics2D.Raycast (transform.position, Vector2.up, ExplosionStop);
		RaycastHit2D hitDown = Physics2D.Raycast (transform.position, Vector2.down, ExplosionStop);
		RaycastHit2D hitLeft = Physics2D.Raycast (transform.position, Vector2.left, ExplosionStop);
		RaycastHit2D hitRight = Physics2D.Raycast (transform.position, Vector2.right, ExplosionStop);

		if (hitUp.collider == null) {
			direction[0] = 1;           
		} else
			direction[0] = 0;

		if (hitDown.collider == null) {
			direction[1] = 1;          
		} else
			direction[1] = 0;

		if (hitLeft.collider == null) {
			direction[2] = 1;           
		} else
			direction[2] = 0;

		if (hitRight.collider == null) {
			direction[3] = 1;           
		} else
			direction[3] = 0;
		
	}

	int naosei(int[] directions){
		int randomIndex;
		do{			
			randomIndex = Random.Range (0, 4);
		}while (directions[randomIndex] == 0);
		return randomIndex;
	}


	void Move(int direction){
		switch (direction) {
		case 0: //Up 
			GetComponent<Rigidbody2D> ().velocity =  new Vector2 (0, 1) *speed;
			GetComponent<Animator> ().SetInteger ("y", 1);
			break;
		case 1: //Down
			GetComponent<Rigidbody2D> ().velocity = new Vector2(0,-1)*speed;
			GetComponent<Animator> ().SetInteger ("y", -1);
			break;
		case 2: //Left
			GetComponent<Rigidbody2D> ().velocity = new Vector2(-1,0)*speed;
			GetComponent<Animator> ().SetInteger ("x", -1);
			break;
		case 3: //Right
			GetComponent<Rigidbody2D> ().velocity = new Vector2(1,0)*speed;
			GetComponent<Animator> ().SetInteger ("x", 1);
			break;
		}
	}


}
