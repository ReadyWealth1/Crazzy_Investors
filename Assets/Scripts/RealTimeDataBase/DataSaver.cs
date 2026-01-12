using System.Collections;
using UnityEngine;
//using Firebase.Database;
using System;

public class DataSaver : MonoBehaviour
{
    public static DataSaver Instance { get; private set; }

    public UserData UserData
    {
        get => userData;
        set => userData = value;
    }

    public GuestData GuestData => guestData;

    private UserData userData;
    private GuestData guestData;

    public string userId;
  //  private DatabaseReference dbRef;

    public event Action OnFirebaseDataLoaded;

    private void Awake()
    {
   
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //dbRef = FirebaseDatabase.DefaultInstance.RootReference;

          //  if (GoogleController.instance != null)
           //     GoogleController.instance.SubscribeEvent();

            InitGuestOrUser();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitGuestOrUser()
    {
        if (!PlayerPrefs.HasKey("GuestLogin"))
        {
            PlayerPrefs.SetInt("UserGems", 0);
            PlayerPrefs.SetInt("HighScore", 0);

            guestData = new GuestData(0, 0);
            PlayerPrefs.SetInt("GuestLogin", 1);

            Debug.Log("First time guest login initialized.");
        }
        else if (PlayerPrefs.GetInt("GuestLogin") == 1)
        {
            guestData = new GuestData(PlayerPrefs.GetInt("UserGems"), PlayerPrefs.GetInt("HighScore"));
           // Debug.Log("Guest login loaded.");
        }
        else
        {
            Debug.Log("Firebase login detected. Loading data...");
           // LoadDataFn();
        }
    }

    public void SaveDataFn()
    {
        if (userData == null)
        {
            Debug.LogWarning("Cannot save data: userData is null.");
            return;
        }

        string json = JsonUtility.ToJson(userData);
        Debug.Log("Saving user data to Firebase: " + json);
        Debug.Log("UserID: "+userId);
       /* dbRef.Child("users").Child(userId).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Firebase save failed: " + task.Exception);
            }
            else
            {
                Debug.Log("Firebase save successful.");
            }
        });*/
    }

    public void LoadDataFn()
    {
      //  StartCoroutine(LoadDataEnum());
    }

   /* private IEnumerator LoadDataEnum()
    {*/
        //var serverData = dbRef.Child("users").Child(userId).GetValueAsync();
       /* yield return new WaitUntil(() => serverData.IsCompleted);

        if (serverData.Exception != null)
        {
            Debug.LogError("Firebase load error: " + serverData.Exception);
            yield break;
        }

       // DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (!string.IsNullOrEmpty(jsonData))
        {
            Debug.Log("Firebase data loaded: " + jsonData);
          //  string extracted = ExtractInnerJson(jsonData);
            Debug.Log(jsonData);  
            userData = JsonUtility.FromJson<UserData>(jsonData);
            Debug.Log(userData.userName + ", " + userData.diamonds + ", " + userData.highScore);

            if (!IsValidUserData(userData))
            {

                Debug.LogWarning("Invalid user data detected. Creating new one.");
                CreateNewUserData();
            }
        }
        else
        {
            Debug.LogWarning("No Firebase data found. Creating new user.");
            CreateNewUserData();
        }

        OnFirebaseDataLoaded?.Invoke();*/
   // }
    public static string ExtractInnerJson(string json)
    {
        int firstBrace = json.IndexOf('{');
        int colonIndex = json.IndexOf(':');
        int lastBrace = json.LastIndexOf('}');

        if (firstBrace == -1 || colonIndex == -1 || lastBrace == -1) return json;

        // Start from the character after the colon (to skip ":")
        string innerJson = json.Substring(colonIndex + 1, lastBrace - colonIndex - 1).Trim();

        return innerJson;
    }
    private void CreateNewUserData()
    {
        string name = PlayerPrefs.GetString("GoogleUserName", "Player");
        userData = new UserData(name, 0, 0); // You can customize default values
        SaveDataFn();
    }  

    private bool IsValidUserData(UserData data)
    {
        return data != null && data.diamonds >= 0 && data.highScore >= 0 && !string.IsNullOrEmpty(data.userName);
    }
}
