using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
 /*   private void OnEnable()
    {
        JioWrapperJS.Instance.GratifyRewards.AddListener(GratifyRewards);
    }
    private void OnDisable()
    {
        JioWrapperJS.Instance.GratifyRewards.RemoveListener(GratifyRewards);
    }
*/

    /// <summary>
    /// Gratify your rewards here.
    /// </summary>
    void GratifyRewards()
    {
        Debug.Log("GratifyRewards");
    }

}
