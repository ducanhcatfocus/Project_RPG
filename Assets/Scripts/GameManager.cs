using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public static GameManager Instance => instance;


    private bool isGameOver;

    private int highScore;
    bool isBestScore;


    [SerializeField] int kills;


    private void Awake()
    {
        if (instance != null) Debug.LogError("Only 1 instance of GameManager allow");
        instance = this;

    }
    private void Start()
    {
        isGameOver = false;
        isBestScore = false;
        highScore = PlayerPrefs.GetInt("Highscore", 0);
    }

    public int GetKill() => kills;

    public void IncreateKill() 
    { 
        kills++; 
        UIIngame.Instance.UpdateKillScore(GetKill());
        if (kills > highScore)
        {
            isBestScore=true;
            PlayerPrefs.SetInt("Highscore", kills); 
            PlayerPrefs.Save(); 
        }
    }

    public bool IsGameOver() => isGameOver;

    public void GameOver()
    {
        isGameOver = true;
        Invoke("InvokeDisplayDeathScreen", 3);
    }

    void InvokeDisplayDeathScreen()
    {
        UIIngame.Instance.DisplayDeathScreen(kills,isBestScore);

    }


}
