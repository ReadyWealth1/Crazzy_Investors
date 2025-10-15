using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float move = 10f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * move * Time.deltaTime, Space.World);

    }
}
