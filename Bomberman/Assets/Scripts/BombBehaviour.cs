using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

	public float time = 3;
	public int bombRange;
	public static float explosionRangeUp, expRangeDown, expRangeLeft, expRangeRight;
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
		InstantiateExplosion ((int)explosionRangeUp,(int)expRangeDown,(int)expRangeLeft,(int)expRangeRight);
	}

	void FixedUpdate () {			
		#region Verifica a distancia até os blocos que colidirá(ou não) em todas as direções

		RaycastHit2D hitUp = Physics2D.Raycast (transform.position, Vector2.up, bombRange, 1 << 8);
		RaycastHit2D hitDown = Physics2D.Raycast (transform.position, Vector2.down, bombRange, 1 << 8);
		RaycastHit2D hitLeft = Physics2D.Raycast (transform.position, Vector2.left, bombRange, 1 << 8);
		RaycastHit2D hitRight = Physics2D.Raycast (transform.position, Vector2.right, bombRange, 1 << 8);
        if (hitUp.collider != null) {
            explosionRangeUp = Mathf.Round(hitUp.collider.transform.position.y - transform.position.y);
            Debug.Log("Up " + explosionRangeUp);
        }
        else
        { 
            explosionRangeUp = bombRange;
		}
		if (hitDown.collider != null) {			
			//if (hitDown.collider.CompareTag ("Destroyable")) {
				expRangeDown = Mathf.Round(transform.position.y - hitDown.collider.transform.position.y );
				Debug.Log ("Down "+expRangeDown);
			//}else if (hitDown.collider.CompareTag("Bomba")){
			
		//	}
		}
        else
        {
            expRangeDown = bombRange;
        }
		if (hitLeft.collider != null) {			
		//	if (hitLeft.collider.CompareTag ("Destroyable")) {
				expRangeLeft = Mathf.Round(transform.position.x - hitLeft.collider.transform.position.x );
				Debug.Log ("Left "+expRangeLeft);		
		//	} else if (hitLeft.collider.CompareTag ("Bomba")) {

		//	}
		}
        else
        {
            expRangeLeft = bombRange;
        }
		if (hitRight.collider != null) {			
			//if (hitRight.collider.CompareTag ("Destroyable")) {
				expRangeRight = Mathf.Round(hitRight.collider.transform.position.x -transform.position.x );
				Debug.Log ("Right "+expRangeRight);
			//} else if (hitRight.collider.CompareTag ("Bomba")) {

			//}
		}
        else
        {
            expRangeRight = bombRange;
        }
		#endregion
	}

    void InstantiateExplosion(int up, int down, int left, int right) {

        Vector3 posUp = new Vector3(transform.position.x, transform.position.y + up, transform.position.z); //Declara a posição padrão de cada "ponta" da explosão
		Vector3 posDown = new Vector3 (transform.position.x, transform.position.y - down, transform.position.z);
		Vector3 posLeft = new Vector3 (transform.position.x - left, transform.position.y, transform.position.z);
		Vector3 posRight = new Vector3 (transform.position.x +right, transform.position.y, transform.position.z);
		Instantiate (Middle, transform.position, transform.rotation); //Instancia o meio pois é padrão em toda explosão

        ///Cada for instancia a animação do meio até o fim.

        for(int i = 1; i < up; i++)
        {
            Vector3 posMiddle = new Vector3(transform.position.x, transform.position.y + i, transform.position.z);
            Instantiate(Vertical, posMiddle, transform.rotation);
        }
        for(int i = 1; i < down; i++)
        {
            Vector3 posMiddle = new Vector3(transform.position.x, transform.position.y - i, transform.position.z);
            Instantiate(Vertical, posMiddle, transform.rotation);
        }

        for(int i = 1; i < left; i++)
        {
            Vector3 posMiddle = new Vector3(transform.position.x - i, transform.position.y, transform.position.z);
            Instantiate(Horizontal, posMiddle, transform.rotation);
        }
        for (int i = 1; i < right; i++)
        {
            Vector3 posMiddle = new Vector3(transform.position.x+i, transform.position.y, transform.position.z);
            Instantiate(Horizontal, posMiddle, transform.rotation);
        }
        Instantiate(VertUp, posUp, transform.rotation);
        Instantiate(VertDown, posDown, transform.rotation);
        Instantiate(HorizLeft, posLeft, transform.rotation);
        Instantiate(HorizRight, posRight, transform.rotation);
    }

    void OnTriggerExit2D(Collider2D other){ // Verifica quando o Player sai de cima da bomba 
		if(other.CompareTag("Player")){
			this.gameObject.GetComponent<BoxCollider2D> ().isTrigger = false;
		}
	}
}
