using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;
    public float damage = 3f;
    public GameObject target;
    private float step = 0.2f;
    private bool playerInRange = false;
    private Vector3 offset = new Vector3(0f, -0.1f, 0);
    public SlimeAttack slimeAttack;
    private Vector3 startingPosition;
    private float patrolDistance = 0.3f;

    private Vector3 point1;
    private Vector3 point2;
    private Vector3 point3;

    bool point1Touched = false;
    bool point2Touched = false;
    bool point3Touched = false;

    bool canMove = true;

    public Flash flash;

    public GameObject lootCoin;

    public float Health
    {
        set
        {
            health = value;
            if(health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    public float health = 1f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        point1 = new Vector3(Random.Range(startingPosition.x - patrolDistance, startingPosition.x + patrolDistance), Random.Range(startingPosition.y - patrolDistance, startingPosition.y + patrolDistance), 0);
        point2 = new Vector3(Random.Range(startingPosition.x - patrolDistance, startingPosition.x + patrolDistance), Random.Range(startingPosition.y - patrolDistance, startingPosition.y + patrolDistance), 0);
        point3 = new Vector3(Random.Range(startingPosition.x - patrolDistance, startingPosition.x + patrolDistance), Random.Range(startingPosition.y - patrolDistance, startingPosition.y + patrolDistance), 0);
    }


    public void Defeated()
    {
        animator.SetTrigger("Defeated");

        
    }

    private void DropLoot()
    {
        //Instantiate chest prefab where slime died
        Instantiate(lootCoin, transform.position, transform.rotation);
    }

    public void RemoveEnemy()
    {
        //drop loot
        DropLoot();
        Destroy(gameObject);
    }

    //Damage Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Attack Number: " + slimeAttack.attackNumber);
        
        if(collision.tag == "Player" && slimeAttack.attackNumber >= 1)
        {
            // Deal damage to enemy
            StartCoroutine(Attack2Seconds());
        }
    }

    IEnumerator Attack2Seconds()
    {
        yield return new WaitForSeconds(2);
        
        if (target != null)
        {
            target.GetComponent<PlayerController>().Health -= damage;
            //Flash damage image
            flash.playerDamaged = true;
            print("Damaged Player " + target.GetComponent<PlayerController>().Health);
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            //move towards player
            if (canMove)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position + offset, step * Time.deltaTime);
            }
            
        }
        else
        {
            Patrol();
        }
    }

    //Move towards player
    public void AttackPlayer()
    {
        //start animation
        animator.SetBool("Moving", true);
        canMove = true;
        //set player is in range
        playerInRange = true;
        animator.SetBool("Damaged", false);
    }

    public void StopAttack()
    {
        //start idle animation
        animator.SetBool("Moving", false);
        //stop going towards player, maybe go back to starting spot
        playerInRange = false;
    }


   public void Damaged()
    {
        canMove = false;
        animator.SetTrigger("Damaged");
    }

    
    private void Patrol()
    {

        if(transform.position != point1 && !point1Touched)
        {
            transform.position = Vector2.MoveTowards(transform.position, point1, step * Time.deltaTime);
            if(transform.position == point1)
            {
                point1Touched = true;
                point2Touched = false;
            }
        }
        else if(transform.position != point2 && !point2Touched)
        {
            transform.position = Vector2.MoveTowards(transform.position, point2, step * Time.deltaTime);
            if (transform.position == point2)
            {
                point2Touched = true;
                point3Touched = false;
            }
        }
        else if(transform.position != point3 && !point3Touched)
        {
            transform.position = Vector2.MoveTowards(transform.position, point3, step * Time.deltaTime);
            if (transform.position == point3)
            {
                point3Touched = true;
                point1Touched = false;
            }
        }
    }
}
