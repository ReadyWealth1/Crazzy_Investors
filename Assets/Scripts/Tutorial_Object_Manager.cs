using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Object_Manager : MonoBehaviour
{
    [SerializeField] private GameObject Asset_Buy_Btn;
    [SerializeField] private GameObject Fixed_Deposit_Asset;
    [SerializeField] private GameObject MutualFunds_Asset;
    [SerializeField] private GameObject Asset_Next_Button;
    [SerializeField] private GameObject Asset_Previous_Button;
    [SerializeField] private GameObject Portfolio_Button;
    [SerializeField] private GameObject Job_Counter;
    [SerializeField] private GameObject AssetManageEmpty_Ui;
    // [SerializeField] private GameObject Score_Ui;
    [SerializeField] private GameObject Asset_Close_Button;

    public void DisableTutorialObjectsIfInTutorialMode()
    {
        if (GroundSpawnerTest.isTutorialMode)
        {
            Job_Counter.SetActive(false);
            Asset_Buy_Btn.SetActive(false);
            Fixed_Deposit_Asset.SetActive(false);
            MutualFunds_Asset.SetActive(false);
            Asset_Next_Button.SetActive(false);
            Asset_Previous_Button.SetActive(false);
            Portfolio_Button.SetActive(false);

            AssetManageEmpty_Ui.SetActive(false);
            // Score_Ui.SetActive(false);
            Asset_Close_Button.SetActive(false);
        }
    }



}