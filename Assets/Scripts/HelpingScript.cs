using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddCoins()
    {
        GameManager.numberOfCoins= GameManager.numberOfCoins+50000;
    }
    public void SubtractCoins()
    {
        GameManager.numberOfCoins = GameManager.numberOfCoins - 50000;
    }
}
