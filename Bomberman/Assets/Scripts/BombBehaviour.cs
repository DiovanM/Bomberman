using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

	public float time = 3;
	public int bombRange;
	public float expRangeUp, expRangeDown, expRangeLeft, expRangeRight ;

	void Start () {		
		Destroy (this.gameObject, time);
		bombRange = PlayerBehaviour.bombPower;
	}	

	void FixedUpdate () {			
		RaycastHit2D hitUp = Physics2D.Raycast (transform.position, Vector2.up, bombRange, 1 << 8);
		RaycastHit2D hitDown = Physics2D.Raycast (transform.position, Vector2.down, bombRange, 1 << 8);
		RaycastHit2D hitLeft = Physics2D.Raycast (transform.position, Vector2.left, bombRange, 1 << 8);
		RaycastHit2D hitRight = Physics2D.Raycast (transform.position, Vector2.right, bombRange, 1 << 8);

		/*Debug.DrawRay (transform.position, bombRangeUp);
		Debug.DrawRay (transform.position, bombRangeDown);
		Debug.DrawRay (transform.position, bombRangeLeft);
		Debug.DrawRay (transform.position, bombRangeRight);
		*/

		if (hitUp.collider != null) {			
			if (hitUp.collider.CompareTag ("Destroyable")) {
				expRangeUp = Mathf.Round(hitUp.collider.transform.position.y - transform.position.y);
				Debug.Log ("Up "+expRangeUp);
			} else if (hitUp.collider.CompareTag ("Bomba")) {
			}
		}

		if (hitDown.collider != null) {			
			if (hitDown.collider.CompareTag ("Destroyable")) {
				expRangeDown = Mathf.Round(transform.position.y - hitDown.collider.transform.position.y );
				Debug.Log ("Down "+expRangeDown);
			}else if (hitDown.collider.CompareTag("Bomba")){
			
			}
		}

		if (hitLeft.collider != null) {			
			if (hitLeft.collider.CompareTag ("Destroyable")) {
				expRangeLeft = Mathf.Round(transform.position.x - hitLeft.collider.transform.position.x );
				Debug.Log ("Left "+expRangeLeft);		
			} else if (hitLeft.collider.CompareTag ("Bomba")) {

			}
		}

		if (hitRight.collider != null) {			
			if (hitRight.collider.CompareTag ("Destroyable")) {
				expRangeRight = Mathf.Round(hitRight.collider.transform.position.x -transform.position.x );
				Debug.Log ("Right "+expRangeRight);
			} else if (hitRight.collider.CompareTag ("Bomba")) {

			}
		}
	}

	void OnTriggerExit2D(Collider2D other){ // Verifica quando o Player sai de cima da bomba 
		if(other.CompareTag("Player")){
			this.gameObject.GetComponent<BoxCollider2D> ().isTrigger = false;
		}
	}
}
