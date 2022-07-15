using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Animator animator;
    public float damage = 3f;
    public GameObject target;
    private float step = 0.2f;
    private bool playerInRange = false;
    private Vector3 offset = new Vector3(0f, -0.1f, 0);
    public SlimeAttack slimeAttack;
    private Vector3 startingPosition;
    private float patrolDistance = 1.3f;

    [SerializeField]
    private Vector3 point1;
    [SerializeField]
    private Vector3 point2;
    [SerializeField]
    private Vector3 point3;

    [SerializeField]
    bool point1Touched = false;
    [SerializeField]
    bool point2Touched = false;
    [SerializeField]
    bool point3Touched = false;

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
        startingPosition = transform.position;
        point1 = new Vector3(Random.Range(startingPosition.x - patrolDistance, startingPosition.x + patrolDistance), Random.Range(startingPosition.y - patrolDistance, startingPosition.y + patrolDistance), 0);
        point2 = new Vector3(Random.Range(startingPosition.x - patrolDistance, startingPosition.x + patrolDistance), Random.Range(startingPosition.y - patrolDistance, startingPosition.y + patrolDistance), 0);
        point3 = new Vector3(Random.Range(startingPosition.x - patrolDistance, startingPosition.x + patrolDistance), Random.Range(startingPosition.y - patrolDistance, startingPosition.y + patrolDistance), 0);
    }


    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
    {
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
            print("Damaged Player " + target.GetComponent<PlayerController>().Health);
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            //move towards player
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position + offset, step * Time.deltaTime);
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
        //set player is in range
        playerInRange = true;
    }

    public void StopAttack()
    {
        //start idle animation
        animator.SetBool("Moving", false);
        //stop going towards player, maybe go back to starting spot
        playerInRange = false;
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
