using UnityEngine;

public class InternetConnectivity : MonoBehaviour
{
    public static InternetConnectivity Instance { get; private set; }

    public bool isInternet;
    public GameObject internetPanel;
    private bool shouldMonitorInternet = false; // ?? Track when to start checking

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (shouldMonitorInternet)
        {
            MonitorInternet();  // ? Check every frame only after login
        }
   
    }

    public void StartMonitoringInternet()
    {
        shouldMonitorInternet = true;
    }
    public void OffMonitoringInternet()
    {
        shouldMonitorInternet = false;
    }

    private void MonitorInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if (!internetPanel.activeSelf)
                internetPanel.SetActive(true);
               Toast.Instance.CloseInternetPopup();
               isInternet = false;
        }
        else
        {
            if (internetPanel.activeSelf)
                internetPanel.SetActive(false);
                isInternet = true;
        }
    }

    // ? One-time check (used before login)
    public bool HasInternet()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}
