using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public float rotationSpeed = 0;
    public float scaleSpeed = 0; 
    public float scaleAmount = 0; 

    private Vector3 originalScale;

  
    void Start()
    {
        originalScale = transform.localScale;
    }

   
    void Update()
    {
       
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f); 

       
        float scaleChange = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount; 
        transform.localScale = originalScale + new Vector3(scaleChange, scaleChange, scaleChange);
    }
}
