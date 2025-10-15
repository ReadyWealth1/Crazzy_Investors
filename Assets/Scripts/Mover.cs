using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public float movespeed = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xvalue = Input.GetAxis("Horizontal") * Time.deltaTime * movespeed;
        float zvalue = Input.GetAxis("Vertical") * Time.deltaTime * movespeed;
        transform.Translate(xvalue,0,zvalue);
        
    }
}
