using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public GameObject damageTextPrefab, Grid_Obj;
    public string textToDisplay;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (damageTextPrefab == null)
            {
                Debug.LogError("damageTextPrefab is not assigned!");
                return;
            }

            if (Grid_Obj == null)
            {
                Debug.LogError("Grid_Obj is not assigned!");
                return;
            }

            if (string.IsNullOrEmpty(textToDisplay))
            {
                Debug.LogError("textToDisplay is not set!");
                return;
            }

            GameObject DamageText = Instantiate(damageTextPrefab, Grid_Obj.transform);
            var textComponent = DamageText.transform.GetChild(0).GetComponent<TextMeshPro>();
            if (textComponent != null)
            {
                textComponent.SetText(textToDisplay);
            }
            else
            {
                Debug.LogError("TextMeshPro component not found on the child of the instantiated prefab!");
            }
        }
    }
}
