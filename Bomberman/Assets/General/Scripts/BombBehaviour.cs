using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {

	private int bombRange;
    private bool exploded = false;
	public float explosionRangeUp, explosionRangeDown, explosionRangeLeft, explosionRangeRight;
	public LayerMask ExplosionStop;
	[Header("Explosion Prefabs")]
	public GameObject Horizontal;
	public GameObject HorizLeft;
	public GameObject HorizRight;
	public GameObject Middle;
	public GameObject Vertical;
	public GameObject VertDown;
	public GameObject VertUp;

	void Start () {				
		bombRange = PlayerBehaviour.bombPower;        
	}

    public void Exploded() {
        exploded = true;
    } 

	void OnDestroy(){
        { if (exploded)
            {
                //Instancia a explosão com os parâmetros de limite para cada lado
                InstantiateExplosion((int)explosionRangeUp, (int)explosionRangeDown, (int)explosionRangeLeft, (int)explosionRangeRight);
            }
        }
	}

	void FixedUpdate () {			
		//Verifica a distancia até os blocos que colidirá(ou não) em todas as direções

		Vector2 baseUp = new Vector2(transform.position.x,transform.position.y +0.4f);  //Começa o raycast 0.4 de distância do centro da bomba, para nao colidir com a própria
		Vector2 baseDown = new Vector2(transform.position.x,transform.position.y -0.4f);
		Vector2 baseLeft = new Vector2(transform.position.x-0.4f,transform.position.y);
		Vector2 baseRight = new Vector2(transform.position.x+0.4f,transform.position.y);

		RaycastHit2D hitUp = Physics2D.Raycast (baseUp, Vector2.up, bombRange, ExplosionStop);
		RaycastHit2D hitDown = Physics2D.Raycast (baseDown, Vector2.down, bombRange, ExplosionStop);
		RaycastHit2D hitLeft = Physics2D.Raycast (baseLeft, Vector2.left, bombRange, ExplosionStop);
		RaycastHit2D hitRight = Physics2D.Raycast (baseRight, Vector2.right, bombRange, ExplosionStop);
        
		if (hitUp.collider != null) {
            explosionRangeUp = Mathf.Round(hitUp.collider.transform.position.y - transform.position.y);           
        }
        else
        { 
            explosionRangeUp = bombRange;
		}
		if (hitDown.collider != null) {						
			explosionRangeDown = Mathf.Round(transform.position.y - hitDown.collider.transform.position.y );
		}
        else
        {
			explosionRangeDown = bombRange;
        }
		if (hitLeft.collider != null) {			
			explosionRangeLeft = Mathf.Round(transform.position.x - hitLeft.collider.transform.position.x );						
		}
        else
        {
			explosionRangeLeft = bombRange;
        }
		if (hitRight.collider != null) {						
			explosionRangeRight = Mathf.Round(hitRight.collider.transform.position.x -transform.position.x );
		}
        else
        {
			explosionRangeRight = bombRange;
        }
	}
    
    void InstantiateExplosion(int up, int down, int left, int right) {

        Vector3 posUp = new Vector3(transform.position.x, transform.position.y + up, transform.position.z); //Declara a posição de cada "ponta" da explosão
		Vector3 posDown = new Vector3 (transform.position.x, transform.position.y - down, transform.position.z);
		Vector3 posLeft = new Vector3 (transform.position.x - left, transform.position.y, transform.position.z);
		Vector3 posRight = new Vector3 (transform.position.x +right, transform.position.y, transform.position.z);

		Instantiate (Middle, transform.position, transform.rotation); //Instancia o meio pois é padrão em toda explosão

        //Cada "for" instancia as animações do meio até cada ponta		  
		for(int i = 1; i < up; i++)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + i, transform.position.z);
            Instantiate(Vertical, pos, transform.rotation);
        }
        for(int i = 1; i < down; i++)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y - i, transform.position.z);
            Instantiate(Vertical, pos, transform.rotation);
        }

        for(int i = 1; i < left; i++)
        {
            Vector3 pos = new Vector3(transform.position.x - i, transform.position.y, transform.position.z);
            Instantiate(Horizontal, pos, transform.rotation);
        }
        for (int i = 1; i < right; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + i, transform.position.y, transform.position.z);
            Instantiate(Horizontal, pos, transform.rotation);
        }

		//Instancia as pontas
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
