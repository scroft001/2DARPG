using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //player wallet
    public float playerWallet;
    //Wallet Text
    public Text copperText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        copperText.text = getWallet().ToString();
    }

    //setters for wallet
    public void setWallet(float amount){
        playerWallet += amount;
    }

    //getters for wallet
    public float getWallet(){
        return playerWallet;
    }
}
