using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{

    public Collider2D attackZone;
    public Enemy slime;
    public int attackNumber = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attackZone.enabled = false;
            slime.AttackPlayer();
            attackNumber += 1;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        slime.StopAttack();
    //    }
    //}


}
