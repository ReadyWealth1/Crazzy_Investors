using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager;
    private int add_coins = 500;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.numberOfCoins += add_coins;

            // Play the coin pickup sound
            if (AudioManager.instance != null && AudioManager.instance.coin_Pickup != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.coin_Pickup);
            }

            Destroy(gameObject);
            GameManager.First_Zero = false;
        }
    }
}
