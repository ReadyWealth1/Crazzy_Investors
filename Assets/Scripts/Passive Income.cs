using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CandyCoded.HapticFeedback;
public class PassiveIncome : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        //transform.Rotate(0, 60 * Time.deltaTime, 0);
    }
    /*private void LightVibration()
    {

    }

    private void MediumVibration()
    {

    }*/
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int[] validIncomeValues = { 20, 30, 40, 50, 60 };
            int randomIndex = Random.Range(0, validIncomeValues.Length);
            GameManager.PassiveIncome += validIncomeValues[randomIndex];

            // Vibrate the device
            Vibrator.Vibrate();
            Vibrator.Vibrate(100);

            Destroy(gameObject);
        }
    }*/
}
