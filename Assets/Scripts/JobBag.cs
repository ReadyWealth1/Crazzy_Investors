using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobBag : MonoBehaviour
{
    [SerializeField] private GameObject jobHead;
    [SerializeField] private GameObject child2;
    [SerializeField] private GameObject child1;

    public void Start()
    {
        UpdateJob();
    }
    void Update()
    {
        UpdateJob();
    }
    public void UpdateJob()
    {
        if (PlayerPrefs.GetString("SelectedCharacter") == "Witch")
        {

            Debug.Log("cube call++++++++++");

            child1.SetActive(true);
            child2.SetActive(false);
        }
        else
        {

            child2.SetActive(true);
            child1.SetActive(false);
            Debug.Log("Cylinder++++++++++++");

        }
    }
}
