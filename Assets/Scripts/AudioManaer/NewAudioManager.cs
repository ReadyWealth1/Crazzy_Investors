using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewAudioManager : MonoBehaviour
{
    private static NewAudioManager instance;

    [SerializeField] private AudioSource sfx_AudioSource;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip spinPopupSound;
    [SerializeField] private AudioClip badLuckPopupSound;
    [SerializeField] private AudioClip buttonClickSound;

    public static NewAudioManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
      //  DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CheckSetting();

        // Attach event listener to detect all UI clicks
        EventSystem.current.gameObject.AddComponent<GlobalClickListener>();

    
            CheckSetting();

            // Find all buttons in the scene and add a listener dynamically
            Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button btn in allButtons)
        {
            btn.onClick.AddListener(() => PlayButtonClickSound());
        }
        

    }

    public void CheckSetting()
    {
        sfx_AudioSource.mute = (PlayerPrefs.GetInt("Sound", 1) == 1) ? false : true;
    }

    public void PlayPopupSound()
    {
        PlaySound(spinPopupSound);
    }

    public void PlayButtonClickSound()
    {
        PlaySound(buttonClickSound);
    }
    public void BadLuckPopupSound()
    {
        PlaySound(badLuckPopupSound);
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfx_AudioSource.PlayOneShot(clip);
        }
    }
}

// 🔹 Separate Component to Listen for Global UI Button Clicks
public class GlobalClickListener : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerPress != null)
        {
            Button button = eventData.pointerPress.GetComponent<Button>();
            if (button != null)
            {
                NewAudioManager.Instance()?.PlayButtonClickSound();
            }
        }
    }
}
