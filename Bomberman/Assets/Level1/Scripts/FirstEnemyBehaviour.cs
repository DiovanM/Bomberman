using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemyBehaviour : MonoBehaviour {

	Vector2 pos;
	public float speed = 1;
	public LayerMask checkOnlyThis;
	private int randomIndex, previousIndex;
	Vector2[] rayDirection = new Vector2[4];
	private Vector2 facingRayInitial, playerPosition;
	private bool orientation, isValid;

	void Start()
	{					
		rayDirection[0] = new Vector2 (0, 1);
		rayDirection[1] = new Vector2 (0, -1);
		rayDirection[2] = new Vector2 (-1, 0);
		rayDirection[3] = new Vector2 (1, 0);
		pos = transform.position;
		InitialDirection ();
	}
	void OnDestroy(){
		MenusManager.enemyAmount -= 1;
	}
	void Update()
	{
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (playerPosition.y > transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 3;
        }


        Move(randomIndex);		

		if (transform.position.x >= pos.x + 1 || transform.position.y >= pos.y + 1
			|| transform.position.x <= pos.x - 1 || transform.position.y <= pos.y - 1) {
            if (Raycasting(randomIndex) != null)
            {
                switch (randomIndex)
                {
                    case 0:
                        randomIndex = 1;
                        break;
                    case 1:
                        randomIndex = 0;
                        break;
                    case 2:
                        randomIndex = 3;
                        break;
                    case 3:
                        randomIndex = 2;
                        break;
                }
            }
            ChangeDirection();
			pos = transform.position;
		}   
	}   

	void ChangeDirection(){
		previousIndex = randomIndex;
		if (orientation == true) {
			randomIndex = Random.Range (0, 2);
		} else {
			randomIndex = Random.Range (2, 4);
		}
		if (Raycasting(randomIndex) != null) {			
			randomIndex = previousIndex;
		} 
	}
	void Move(int direction)
	{
		Vector2 nextPosition;
		switch (direction)
		{
		case 0: //Up              
			nextPosition = new Vector2 (transform.position.x, transform.position.y + 1);
			transform.position = Vector2.MoveTowards (transform.position, nextPosition, speed * Time.deltaTime);
			GetComponent<Animator> ().SetInteger ("y", 1);
			GetComponent<Animator> ().SetInteger ("x", 0);
			orientation = false;
			break;
		case 1: //Down
			nextPosition = new Vector2 (transform.position.x, transform.position.y - 1);
			transform.position = Vector2.MoveTowards (transform.position, nextPosition, speed * Time.deltaTime);
			GetComponent<Animator>().SetInteger("y", -1);
			GetComponent<Animator>().SetInteger("x", 0);
			orientation = false;
			break;
		case 2: //Left
			GetComponent<SpriteRenderer>().flipX = false;
			nextPosition = new Vector2 (transform.position.x -1, transform.position.y);
			transform.position = Vector2.MoveTowards (transform.position, nextPosition, speed * Time.deltaTime);
			GetComponent<Animator>().SetInteger("x", -1);
			GetComponent<Animator>().SetInteger("y", 0);
			orientation = true;
			break;
		case 3: //Right
			GetComponent<SpriteRenderer>().flipX = true;
			nextPosition = new Vector2 (transform.position.x + 1, transform.position.y);
			transform.position = Vector2.MoveTowards (transform.position, nextPosition, speed * Time.deltaTime);
			GetComponent<Animator>().SetInteger("x", 1);
			GetComponent<Animator>().SetInteger("y", 0);
			orientation = true;
			break;
		}
	}
	Collider2D Raycasting(int index){
		RaycastHit2D hit = Physics2D.Raycast (transform.position, rayDirection[index], 1f, checkOnlyThis);
		return hit.collider;
	}
	void InitialDirection(){
		do {
			isValid = false;
			randomIndex = Random.Range(0,4);
			if(Raycasting(randomIndex) == null){
				isValid = true;
			}
		}while (isValid == false);
		if (randomIndex == 0 || randomIndex == 1) {
			orientation = true;
		} else {
			orientation = false;
		}
	}
	void OnTriggerEnter2D(Collider2D other){				

		if (other.gameObject.CompareTag ("Player")) {
			PlayerBehaviour.live = false;
		}
	}
}
