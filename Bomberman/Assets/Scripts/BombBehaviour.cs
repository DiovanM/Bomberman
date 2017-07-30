using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

	public float time = 3;
	public int bombRange;
	public static float expRangeUp, expRangeDown, expRangeLeft, expRangeRight;
	[Header("Explosion Prefabs")]
	public GameObject Horizontal;
	public GameObject HorizLeft;
	public GameObject HorizRight;
	public GameObject Middle;
	public GameObject Vertical;
	public GameObject VertDown;
	public GameObject VertUp;

	void Start () {		
		Destroy (this.gameObject, time);
		bombRange = PlayerBehaviour.bombPower;
	}

	void OnDestroy(){
		InstantiateExplosion ((int)expRangeUp,(int)expRangeDown,(int)expRangeLeft,(int)expRangeRight);
	}

	void FixedUpdate () {			
		#region Verifica a distancia até os blocos que colidirá(ou não) em todas as direções
		RaycastHit2D hitUp = Physics2D.Raycast (transform.position, Vector2.up, bombRange, 1 << 8);
		RaycastHit2D hitDown = Physics2D.Raycast (transform.position, Vector2.down, bombRange, 1 << 8);
		RaycastHit2D hitLeft = Physics2D.Raycast (transform.position, Vector2.left, bombRange, 1 << 8);
		RaycastHit2D hitRight = Physics2D.Raycast (transform.position, Vector2.right, bombRange, 1 << 8);
		if (hitUp.collider != null) {			
			//if (hitUp.collider.CompareTag ("Destroyable")) {
				expRangeUp = Mathf.Round(hitUp.collider.transform.position.y - transform.position.y);
				Debug.Log ("Up "+expRangeUp);
			//} else if (hitUp.collider.CompareTag ("Bomba")) {
				
			//}
		}else if (hitUp.collider == null){
			expRangeUp = bombRange;
		}
		if (hitDown.collider != null) {			
			//if (hitDown.collider.CompareTag ("Destroyable")) {
				expRangeDown = Mathf.Round(transform.position.y - hitDown.collider.transform.position.y );
				Debug.Log ("Down "+expRangeDown);
			//}else if (hitDown.collider.CompareTag("Bomba")){
			
		//	}
		}
		if (hitLeft.collider != null) {			
		//	if (hitLeft.collider.CompareTag ("Destroyable")) {
				expRangeLeft = Mathf.Round(transform.position.x - hitLeft.collider.transform.position.x );
				Debug.Log ("Left "+expRangeLeft);		
		//	} else if (hitLeft.collider.CompareTag ("Bomba")) {

		//	}
		}
		if (hitRight.collider != null) {			
			//if (hitRight.collider.CompareTag ("Destroyable")) {
				expRangeRight = Mathf.Round(hitRight.collider.transform.position.x -transform.position.x );
				Debug.Log ("Right "+expRangeRight);
			//} else if (hitRight.collider.CompareTag ("Bomba")) {

			//}
		}
		#endregion
	}

	void InstantiateExplosion(int up, int down, int left, int right){
		Vector3 posUp = new Vector3 (transform.position.x, transform.position.y + up, transform.position.z); //Declara a posição padrão de cada "ponta" da explosão
		Vector3 posDown = new Vector3 (transform.position.x, transform.position.y - down, transform.position.z);
		Vector3 posLeft = new Vector3 (transform.position.x - left, transform.position.y, transform.position.z);
		Vector3 posRight = new Vector3 (transform.position.x +right, transform.position.y, transform.position.z);
		Instantiate (Middle, transform.position, transform.rotation); //Instancia o meio pois é padrão em toda explosão
		switch (up) {
		case 1: 			
			Instantiate (VertUp, posUp, transform.rotation); 
			break;
		case 2:			
			Vector3 posMiddle = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
			Instantiate (VertUp, posUp, transform.rotation);
			Instantiate (Vertical, posMiddle ,transform.rotation);
			break;
			//Fazer os outros casos até 5(limite de força da bomba definido por mim mesmo foda-se)
		}
		//Fazer o switch(ou outra coisa quem sabe) de cada direção
	}

	void OnTriggerExit2D(Collider2D other){ // Verifica quando o Player sai de cima da bomba 
		if(other.CompareTag("Player")){
			this.gameObject.GetComponent<BoxCollider2D> ().isTrigger = false;
		}
	}
}
