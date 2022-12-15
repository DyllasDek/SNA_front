using System;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;


public class PlayerData
{
    public string CurrentName="";
    public int CurrentScore;
    public int CurrentMaxScore;

    public PlayerData(string danila, int i)
    {
        CurrentMaxScore = i;
        CurrentName = danila;
    }

    public PlayerData()
    {
    }

    public void UpdateMaxScore()
    {
        if (CurrentMaxScore < CurrentScore)
        {
            CurrentMaxScore = CurrentScore;
        }
    }


    public string Stringify() 
    {
        return JsonUtility.ToJson(this);
    }

    public static PlayerData Parse(string json)
    {
        return JsonUtility.FromJson<PlayerData>(json);
    }
}