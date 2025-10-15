using UnityEngine;
using UnityEngine.UI;

public class SpeedTester : MonoBehaviour
{
    public Slider speedSlider;
    public Character character;
    public float maxTestSpeed = 15f;

  /*  void Update()
    {
        if (character != null && speedSlider != null)
        {
            character.FwdSpeed = speedSlider.value * maxTestSpeed;
        }
    }*/
}
