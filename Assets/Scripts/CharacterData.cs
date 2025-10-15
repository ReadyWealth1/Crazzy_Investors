using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Game/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string characterName; // Identifier for the character
    public GameObject menuCharacter; // Character model in the start menu
    public GameObject gameCharacter; // Character model in the game
    public GameObject tutorialCharacter; // Character model in the tutorial
    public GameObject positionTarget; // Position target in the start menu
}
