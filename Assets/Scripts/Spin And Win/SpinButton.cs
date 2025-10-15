using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinButton : MonoBehaviour
{
    [SerializeField] private Button spinBtn;
    [SerializeField] private GameObject spinPanel;

    public void Start()
    {
        spinBtn.onClick.AddListener(OpenSpin);
    }

   public void OpenSpin()
    {
        spinPanel.SetActive(true);
    }
    
}
