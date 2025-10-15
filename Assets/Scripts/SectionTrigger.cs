using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SectionTrigger : MonoBehaviour
{
    public GameObject roadSection;
    private int z = 242;
    private float y = 1.444f;
    private float x = -0.6f; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(roadSection, new Vector3(x, y, z), Quaternion.identity);
            z += 98;
        }
    }
}
