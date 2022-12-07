using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickup : MonoBehaviour
{

    //When I collide with the player, add amount to wallet, and disappear
    //Future: add particle effect and sound to pickup

    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "LootPickup"){
            addMoney();
            Destroy(gameObject);
        }
    }

    private void addMoney(){
        gameManager.setWallet(10f);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
