using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    Vector2 rightAttackOffset;

    public float damage = 3f;


    private void Start()
    {
        rightAttackOffset = transform.position;
    }


    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(rightAttackOffset.x * -1, transform.position.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            // Deal damage to enemy
            print("enemy hit");
            Enemy enemy = collision.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.Health -= damage;
                //pause enemy movement or push back
                enemy.Damaged();
            }
        }
    }
}
