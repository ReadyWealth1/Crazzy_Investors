using System.Collections;
using System.Collections.Generic;
//using Firebase.Database;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public int diamonds;
    public int highScore;
    public string userName;

    public UserData(string _userName, int _diamonds, int _highScore)
    {
        userName = _userName;
        diamonds = _diamonds;
        highScore = _highScore;
    }
}

[System.Serializable]
public class GuestData
{
    public int diamonds;
    public int highScore;

    public GuestData(int _diamonds, int _highScore)
    {
        diamonds = _diamonds;
        highScore = _highScore;
    }
}
