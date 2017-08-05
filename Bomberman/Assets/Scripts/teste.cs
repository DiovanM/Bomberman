using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste : MonoBehaviour {

    Vector2 pos;
    public float speed = 2;
    public LayerMask checkthis;
	//private int[] direction = new int[4];
    private int randomIndex;
	Vector2[] rayDirection = new Vector2[4];

    void Start()
    {	
		rayDirection[0] = new Vector2 (1, 0);
		rayDirection[1] = new Vector2 (-1, 0);
		rayDirection[2] = new Vector2 (0, -1);	
		rayDirection[3] = new Vector2 (0, 1);

		pos = transform.position;
		rr ();
		VerifyRandom ();
    }    


	void FixedUpdate()
    {		
		Move(randomIndex);
		Debug.DrawRay(transform.position, Vector2.up);
		Debug.DrawRay(transform.position, Vector2.down);
		Debug.DrawRay (transform.position, Vector2.left);
		Debug.DrawRay (transform.position, Vector2.right);
		if (transform.position.x >= pos.x + 1 || transform.position.y >= pos.y + 1
		    || transform.position.x <= pos.x - 1 || transform.position.y <= pos.y - 1) {
			pos = transform.position;
			VerifyRandom ();
		}	
   
    }

    void VerifyRandom()
    {
		do {
			rr ();
		} while (rr () == false);
			
		
		/*
		if (direction[randomIndex] == 0) {
			for (int i = 0; i < 4; i++) {
				if (direction [i] == 0) {
					rr ();
				}
			}					

		}*/

		/* if (hitUp.collider == null)
        {
			direction[0] = true;
        }
        else
			direction[0] = false;

        if (hitDown.collider == null)
        {
			direction[1] = true;
        }
        else
			direction[1] = false;

        if (hitLeft.collider == null)
        {
			direction[2] = true;
        }
        else
			direction[2] = false;

        if (hitRight.collider == null)
        {
			direction[3] = true;
        }
        else
			direction[3] = false;		       
       */
    }    

	 bool rr(){
		randomIndex = Random.Range(0,4);
		RaycastHit2D hit = Physics2D.Raycast (transform.position, rayDirection[randomIndex]);

		if (hit.collider.CompareTag("Destroyable")|| hit.collider.CompareTag("Undestroyable")||hit.collider.CompareTag("Bomba")) {
			Debug.Log ("temcoisa");
			return false;
		} else {
			return true;
		}
	}


     void Move(int direction)
     {
         switch (direction)
         {
             case 0: //Up 
                 GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed;
                 GetComponent<Animator>().SetInteger("y", 1);
				 GetComponent<Animator>().SetInteger("x", 0);
                 break;
             case 1: //Down
                 GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed;
                 GetComponent<Animator>().SetInteger("y", -1);
				 GetComponent<Animator>().SetInteger("x", 0);
                 break;
             case 2: //Left
			GetComponent<SpriteRenderer>().flipX = false;
                 GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * speed;
                 GetComponent<Animator>().SetInteger("x", -1);
			     GetComponent<Animator>().SetInteger("y", 0);
                 break;
             case 3: //Right
				 GetComponent<SpriteRenderer>().flipX = true;
                 GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * speed;
                 GetComponent<Animator>().SetInteger("x", 1);
	             GetComponent<Animator>().SetInteger("y", 0);
                 break;
         }
     }


}
