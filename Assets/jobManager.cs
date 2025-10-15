using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jobManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject job1;
    public GameObject job2;
    public GameObject job3;
    public static int jobmissed;
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            // Debug.Log("Coins:"+GameManager.numberOfCoins);
            job1.SetActive(false);
            jobmissed++;
            
            // Instantiate(PickUpEffect, transform.position, transform.rotation);
            // Debug.Log("Coinscript firstzero=false");
        }
    }
}
