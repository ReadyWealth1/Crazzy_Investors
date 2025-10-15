using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankBar : MonoBehaviour
{
    public Slider healthBar;
    //public TextMeshProUGUI cashflow;
    public GameManager gameManager;


    void Update()
    {
        healthBar.value = GameManager.numberOfCoins;
        //healthBar.value = GameManager.Casflow_Percentage;
        //Debug.Log("" + healthBar.value);
        //Debug.Log((float.TryParse(cashflow.text, out float cashflowValue)));
        /*if (float.TryParse(cashflow.text, out float cashflowValue))
        {
           
            healthBar.value = cashflowValue;
        }*/
    }
}