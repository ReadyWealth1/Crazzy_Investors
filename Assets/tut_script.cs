using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tut_script : MonoBehaviour
{
    public GameObject UiScreen1;
    public GameObject UiScreen2;
    public GameObject UiScreen3;
    public GameObject UiScreen4;
    public GameObject UiScreen5;
    public GameObject UiScreen6_BankBalance;
    public GameObject UiScreen7;
    public GameObject UiScreen8;
    public GameObject UiScreen9;
    public GameObject UiScreen9_assetbtn;
    [SerializeField]private Button assetbtn;

    public GameObject UiScreen10;
    public GameObject UiScreen11;
    public GameObject UiScreen12;
    public GameObject UiScreen13;
    public GameObject UiScreen14;
    public GameObject UiScreen15;
    public GameObject UiScreen16;
    public GameObject UiScreen17;
    public GameObject AssetCloseButton;
    public Canvas BuyAssets;
    public Canvas Portfolio;
    public GroundSpawnerTest GroundSpawnerTest;
    // private PauseMenu pausemenu;
    public bool isT1;
    public GameObject PortfolioBtn;
    public GameObject Job_Ui;
    public Canvas Lottery;
    //public GameObject ;
    // private int BuyAssetCounter = 0

    void Start()
    {
        assetbtn.onClick.AddListener(ChangePorttSorttoHigh);

        // pausemenu = GetComponent<PauseMenu>();
        // Ensure the UI screen is initially hidden
        if (UiScreen1 != null)
        {
            UiScreen1.SetActive(false);
        }
        if (UiScreen2 != null)
        {
            UiScreen2.SetActive(false);
        }
        if (UiScreen3 != null)
        {
            UiScreen3.SetActive(false);
        }
        if (UiScreen4 != null)
        {
            UiScreen4.SetActive(false);
        }
        if (UiScreen5 != null)
        {
            UiScreen5.SetActive(false);
        }
        if (UiScreen6_BankBalance != null)
        {
            // UiScreen6_BankBalance.SetActive(false);
        }
        if (UiScreen7 != null)
        {
            UiScreen7.SetActive(false);
        }
        if (UiScreen8 != null)
        {
            UiScreen8.SetActive(false);
        }
        if (UiScreen9 != null)
        {
            UiScreen9.SetActive(false);
        }
        if (UiScreen10 != null)
        {
            UiScreen10.SetActive(false);
        }
        if (UiScreen11 != null)
        {
            UiScreen11.SetActive(false);
        }
        if (UiScreen12 != null)
        {
            UiScreen12.SetActive(false);
        }
        if (UiScreen13 != null)
        {
            UiScreen13.SetActive(false);
        }
        if (UiScreen14 != null)
        {
            UiScreen14.SetActive(false);
        }
        if (UiScreen15 != null)
        {
            UiScreen15.SetActive(false);
        }
        if (UiScreen16 != null)
        {
            UiScreen16.SetActive(false);
        }
        if (UiScreen17 != null)
        {
            UiScreen17.SetActive(false);
        }

    }
    private void Update()
    {
        if (GameManager.isGameOver)
        {
            ChangePortSorttoLow();
        }
    }
    private void OnEnable()
    {
        GameManager.OnGameOver += ChangePortSorttoLow;
        GameManager.OnGameOver += DisableAssetsAndPort;
    }
    private void OnDisable()
    {
        GameManager.OnGameOver -= ChangePortSorttoLow;
        GameManager.OnGameOver -= DisableAssetsAndPort;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tut1"))
        {
            UiScreen1.SetActive(true);
            //PauseMenu.instance.PauseGame();
            //   Debug.Log($"isT1 is" + isT1);
            //   isT1 = true;


        }

        if (other.CompareTag("Tut2"))
        {
            UiScreen2.SetActive(true);
            PauseMenu.instance.PauseGame();
        }
        if (other.CompareTag("Tut3"))
        {
            UiScreen3.SetActive(true);
            PauseMenu.instance.PauseGame();
        }
        if (other.CompareTag("Tut4"))
        {
            UiScreen4.SetActive(true);
            PauseMenu.instance.PauseGame();
        }
        if (other.CompareTag("Tut5"))
        {
            UiScreen5.SetActive(true);
            PauseMenu.instance.PauseGame();
        }
        if (other.CompareTag("Tut6"))
        {
            UiScreen6_BankBalance.SetActive(true);
            //pausemenu.PauseGame();
            //PauseMenu.instance.PauseGame();
        }
        if (other.CompareTag("Tut7"))
        {
            UiScreen7.SetActive(true);
            PauseMenu.instance.PauseGame();

            PortfolioBtn.SetActive(true);


        }

        if (other.CompareTag("Tut8"))
        {
            // UiScreen8.SetActive(true);
            //pausemenu.PauseGame();

            // Set the sorting order of the BuyAssets canvas to 33

        }
        if (other.CompareTag("Tut9"))
        {
            ChangePorttSorttoHigh();
            UiScreen9.SetActive(true);
            UiScreen9_assetbtn.SetActive(true);
            PauseMenu.instance.PauseGame();
            AssetCloseButton.SetActive(true);
        }

        if (other.CompareTag("Tut10"))
        {
            ChangePortSorttoLow();
            UiScreen10.SetActive(true);
            PauseMenu.instance.PauseGame();
            Job_Ui.SetActive(true);
            ChangePortSorttoLow();
        }
        if (other.CompareTag("Tut11"))
        {
            UiScreen11.SetActive(true);
            PauseMenu.instance.PauseGame();
        }
        if (other.CompareTag("Tut12"))
        {
            UiScreen12.SetActive(true);
            PauseMenu.instance.PauseGame();
        }
        if (other.CompareTag("Tut13"))
        {
            UiScreen13.SetActive(true);
            PauseMenu.instance.PauseGame();
        }
        if (other.CompareTag("Tut14"))
        {
            UiScreen14.SetActive(true);
            PauseMenu.instance.PauseGame();
        }
        if (other.CompareTag("Tut15"))
        {
            UiScreen15.SetActive(true);
            PauseMenu.instance.PauseGame();
        }
        if (other.CompareTag("Tut16"))
        {
            UiScreen16.SetActive(true);
            ChangeLotSorttoHigh();
            PauseMenu.instance.PauseGame();
        }
        if (other.CompareTag("Tut17"))
        {
            UiScreen17.SetActive(true);
            PauseMenu.instance.PauseGame();
            ChangeLotSorttoLow();
            
        }
    }
    public void ChangeLotSorttoLow()
    {
        if (Lottery != null)
        {
            Lottery.sortingOrder = 4;
            Debug.Log("ChangeLotSorttoLow Method call");
            /* PauseMenu.instance.StartGradualResume();
             Debug.Log("PauseMenu.instance.StartGradualResume();");*/
        }
    }
    public void ChangeLotSorttoHigh()
    {
        if (Lottery != null)
        {
            Lottery.sortingOrder = 33;
        }
    }
    public void ChangeBuyAssetSorttoLow()
    {
        if (BuyAssets != null)
        {
            BuyAssets.sortingOrder = 4;
        }
    }
    public void ChangeBuyAssetSorttoHigh()
    {
        if (BuyAssets != null)
        {
            BuyAssets.sortingOrder = 33;
        }
    }
    public void ChangePortSorttoLow()
    {
        if (Portfolio != null)
        {
            Portfolio.sortingOrder = 6;
        }
    }
    public void DisableAssetsAndPort()
    {
        Image portfolioImg = PortfolioBtn.GetComponent<Image>();
        if (portfolioImg != null)
            portfolioImg.raycastTarget = false;

        Image assetImg = UiScreen9_assetbtn.GetComponent<Image>();
        if (assetImg != null)
            assetImg.raycastTarget = false;
    }
    public void ChangePorttSorttoHigh()
    {
        if (Portfolio != null)
        {
            Portfolio.sortingOrder = 33;
        }
    }
    public void ChangePorttSorttoLow()
    {
        if (Portfolio != null)
        {
            Portfolio.sortingOrder = 13;
        }
    }
    public void UpperChangeBuyAssetSort()
    {
        // BuyAssetCounter = BuyAssetCounter+1;
        if (GroundSpawnerTest)
        {
            UiScreen8.SetActive(false);
        }
    }

    public void ResumeAndMinimize()
    {
        if (isT1 == true)
        {
            isT1 = false;

            UiScreen1.SetActive(false);
            PauseMenu.instance.StartGradualResume();
            Debug.Log($"isT1 is" + isT1);

        }
    }
}