using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEaterBehaviour : MonoBehaviour
{
    Vector2 pos;
    public float speed = 1;
    public LayerMask checkOnlyThis;
    private int randomIndex, previousIndex, bombIndex;
    Vector2[] rayDirection = new Vector2[4];
    private Vector2 facingRayInitial, nextPosition;
    private bool orientation, isValid, bombClose;   

    public static int life;
    public static float time;
    public static bool hittable = true; //Variável para saber se o inimigo está na animação de dano ou não
    public static Rigidbody2D rig;

    void Start()
    {
        life = 3;
        rig = GetComponent<Rigidbody2D>();

        rayDirection[0] = new Vector2(0, 1);
        rayDirection[1] = new Vector2(0, -1);
        rayDirection[2] = new Vector2(-1, 0);
        rayDirection[3] = new Vector2(1, 0);
        bombClose = false;
        pos = transform.position;
        InitialDirection();
    }

    void OnDestroy()
    {
        MenusManager.enemyAmount -= 1;
    }

    void FixedUpdate()
    {       
        GotHit();

        if (life <= 0) {           
            Destroy(gameObject);
        }

         if (transform.position.x >= pos.x + 1 || transform.position.y >= pos.y + 1
                || transform.position.x <= pos.x - 1 || transform.position.y <= pos.y - 1)
         {
            bombClose = false;         
            pos = transform.position;           
         }
       

        if (hittable && bombClose)
        {            
            Move(randomIndex);  
        }
        else if (hittable && !bombClose)
        {            
            Move(randomIndex);
            facingRayInitial = new Vector2(transform.position.x, transform.position.y - 0.3f);
            RaycastHit2D facing = Physics2D.Raycast(facingRayInitial, rayDirection[randomIndex], 0.3f, checkOnlyThis);
            if (facing.collider != null)
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

           if (transform.position.x >= pos.x + 1 || transform.position.y >= pos.y + 1
               || transform.position.x <= pos.x - 1 || transform.position.y <= pos.y - 1)
           {
                if ((transform.position.x / Mathf.Round(transform.position.x) != 1 || transform.position.y / Mathf.Round(transform.position.y) != 1))
                {
                    transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
                }               
                ChangeDirection();
                pos = transform.position;
                TestBomb();
            }
        }       
    }    

    void ChangeDirection() //Função para mudar a direção do inimigo
    {       
        previousIndex = randomIndex;       
        if (orientation == true)
        {
            randomIndex = Random.Range(0, 2);
        }
        else
        {
            randomIndex = Random.Range(2, 4);
        }
        if (Raycasting(randomIndex, 1))
        {
            randomIndex = previousIndex;
        }
    } 

    void Move(int direction) //Função para mover o inimigo dependendo do raio escolhido
    {        
        switch (direction)
        {
            case 0: //Up              
                nextPosition = new Vector2(transform.position.x, transform.position.y + 1);
                transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
                GetComponent<Animator>().SetInteger("y", 1);
                GetComponent<Animator>().SetInteger("x", 0);
                orientation = false;
                break;
            case 1: //Down
                nextPosition = new Vector2(transform.position.x, transform.position.y - 1);
                transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
                GetComponent<Animator>().SetInteger("y", -1);
                GetComponent<Animator>().SetInteger("x", 0);
                orientation = false;
                break;
            case 2: //Left
                GetComponent<SpriteRenderer>().flipX = false;
                nextPosition = new Vector2(transform.position.x - 1, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
                GetComponent<Animator>().SetInteger("x", -1);
                GetComponent<Animator>().SetInteger("y", 0);
                orientation = true;
                break;
            case 3: //Right
                GetComponent<SpriteRenderer>().flipX = true;
                nextPosition = new Vector2(transform.position.x + 1, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
                GetComponent<Animator>().SetInteger("x", 1);
                GetComponent<Animator>().SetInteger("y", 0);
                orientation = true;
                break;
        }
    }   

    void InitialDirection() //Função para escolher a direção inicial
    {
        do
        {
            isValid = false;
            randomIndex = Random.Range(0, 4);
            if (Raycasting(randomIndex, 1) == null)
            {
                isValid = true;
            }
        } while (isValid == false);
        if (randomIndex == 0 || randomIndex == 1) 
        {
            orientation = true;
        }
        else
        {
            orientation = false;
        }
    }

    void TestBomb() //Função para testar se há bomba perto
    {
        for (int i = 0; i < 4; i++)
        {
            var rayCast = Raycasting(i, 5);
            if (rayCast && rayCast.CompareTag("Bomba"))
            {                
                isValid = true;
                bombClose = true;
                randomIndex = i;
                if (i == 0 || i == 1)
                {
                    orientation = true;                   
                }
                else
                {
                    orientation = false;                    
                }
                isValid = false;
                return;
            }
            else
            {
                bombClose = false;
            }         
        }
    }

    Collider2D Raycasting(int index, float size) //Função para testar a direção aleatória do raio
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection[index], size, checkOnlyThis);
        return hit.collider;
    }


    void OnTriggerEnter2D(Collider2D other) //Função para matar o player
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerBehaviour.live = false;
        }
        if (other.gameObject.CompareTag("Bomba")) {
            Destroy(other.gameObject);            
        }
    }

    private void GotHit() //Função para animação de dano do inimigo
    {
        if (hittable == false)
        {
            transform.position = transform.position;
            GetComponent<Animator>().SetBool("isHit", true);
            time += Time.deltaTime;
            if (time >= 0.7)
            {               
                GetComponent<Rigidbody2D>().isKinematic = false;
                life -= 1;               
                hittable = true;
                time = 0;
                GetComponent<Animator>().SetBool("isHit", false);
            }
        }
    }
}
