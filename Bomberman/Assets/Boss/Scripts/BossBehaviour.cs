using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {

    Vector2 pos;
    public float BossSpeed;
    private float speed;
    public LayerMask checkOnlyThis, playerLayer;
    private int randomIndex, previousIndex;
    public int life = 3;
    Vector2[] rayDirection = new Vector2[4];
    private Vector2 facingRayInitial, playerPosition, nextPosition;
    private bool orientation, isValid, hittable, willChangeDirection;
    public static float time;

    public GameObject bombPrefab;

    void Start()
    {
        speed = BossSpeed;
        hittable = true;        
        rayDirection[0] = new Vector2(0, 1);
        rayDirection[1] = new Vector2(0, -1);
        rayDirection[2] = new Vector2(-1, 0);
        rayDirection[3] = new Vector2(1, 0);
        pos = transform.position;
        InitialDirection();        
    }
    void OnDestroy()
    {
        MenusManager.enemyAmount -= 1;
    }
    void FixedUpdate()
    {
        #region Change boss layer

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (playerPosition.y > transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 3;
        }

        #endregion
        
        if (!hittable) { //Verify if hit and change the life status
            GotHit();
        }

        Move(randomIndex);
        
        if (transform.position.x >= pos.x + 1 || transform.position.y >= pos.y + 1
           || transform.position.x <= pos.x - 1 || transform.position.y <= pos.y - 1)
        {
            if (Raycasting(randomIndex, 0) != null)
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

    void ChangeDirection()
    {
        previousIndex = randomIndex;
       
        if (Raycasting(randomIndex, 1) == null)
        {
            if (orientation)
            {
                if (playerPosition.y > transform.position.y)
                {
                    randomIndex = 0;
                }
                else
                {
                    randomIndex = 1;
                }
            }           
            else
            {
                if (playerPosition.x > transform.position.x)
                {
                    randomIndex = 3;
                }
                else
                {
                    randomIndex = 2;
                }               
            }
            if (Raycasting(randomIndex, 0) != null)
            {
                randomIndex = previousIndex;
            }

        }

    }

    void Move(int direction)
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
                nextPosition = new Vector2(transform.position.x - 1, transform.position.y);               
                transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
                GetComponent<Animator>().SetInteger("x", -1);
                GetComponent<Animator>().SetInteger("y", 0);               
                orientation = true;
                break;
            case 3: //Right                
                nextPosition = new Vector2(transform.position.x + 1, transform.position.y);               
                transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
                GetComponent<Animator>().SetInteger("x", 1);
                GetComponent<Animator>().SetInteger("y", 0);               
                orientation = true;
                break;
        }
    }

    Collider2D Raycasting(int index, int test)
    {
        RaycastHit2D hit = new RaycastHit2D();
        if (test == 0)
        {
            hit = Physics2D.Raycast(transform.position, rayDirection[index], 1f, checkOnlyThis);            
        } else 
        {
            hit = Physics2D.Raycast(transform.position, rayDirection[index], 11f, playerLayer);           
        }

        return hit.collider;
    }

    void InitialDirection()
    {

        float xOrY = Random.Range(0, 2);

        if (xOrY == 0 && playerPosition.y > transform.position.y)
            {
                randomIndex = 0;
            }
            else
            {
                randomIndex = 1;
            }

        if (xOrY == 1 && playerPosition.x > transform.position.x)
        {
            randomIndex = 3;
        }
        else
        {
            randomIndex = 2;
        }       

        if (randomIndex == 0 || randomIndex == 1)
        {
            orientation = true;
        }
        else
        {
            orientation = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerBehaviour.live = false;
        }
        if (other.gameObject.CompareTag("Explosion"))
        {
            hittable = false;
        }
    }   
    
    private void GotHit()
    {
        if (life <= 0)
        {
            speed = 0;
            GetComponent<Animator>().SetBool("Death", true);
        }
        else
        {
            speed = 0;           
            GetComponent<Animator>().SetBool("isHit", true);
            time += Time.deltaTime;
            if (time >= 0.7)
            {
                life--;
                hittable = true;
                time = 0;
                GetComponent<Animator>().SetBool("isHit", false);
                speed = BossSpeed;
            }
        }
    }

}