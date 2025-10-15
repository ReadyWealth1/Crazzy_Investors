using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLVibration : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void TriggerVibration(int duration);

    public static void VibrateDevice(int duration)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        TriggerVibration(duration);
#else
        Debug.Log("Vibration only supported in WebGL builds on supported devices.");
#endif
    }
}
 