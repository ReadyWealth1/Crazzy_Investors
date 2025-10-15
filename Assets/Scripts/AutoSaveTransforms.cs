using UnityEngine;

public class AutoSaveTransforms : MonoBehaviour
{
    [Header("Assign the tutorial images here (or leave empty to auto-detect children)")]
    public Transform[] tutorialImages;

    [HideInInspector] public Vector3[] savedPositions;
    [HideInInspector] public Quaternion[] savedRotations;
    [HideInInspector] public Vector3[] savedScales;

    public void SaveTransforms()
    {
        if (tutorialImages == null || tutorialImages.Length == 0)
            tutorialImages = GetComponentsInChildren<Transform>(true);

        savedPositions = new Vector3[tutorialImages.Length];
        savedRotations = new Quaternion[tutorialImages.Length];
        savedScales = new Vector3[tutorialImages.Length];

        for (int i = 0; i < tutorialImages.Length; i++)
        {
            savedPositions[i] = tutorialImages[i].position;
            savedRotations[i] = tutorialImages[i].rotation;
            savedScales[i] = tutorialImages[i].localScale;
        }
    }

    public void ApplyTransforms()
    {
        if (savedPositions == null || tutorialImages == null) return;

        for (int i = 0; i < tutorialImages.Length; i++)
        {
            tutorialImages[i].position = savedPositions[i];
            tutorialImages[i].rotation = savedRotations[i];
            tutorialImages[i].localScale = savedScales[i];
        }
    }
}
