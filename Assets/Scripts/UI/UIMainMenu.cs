using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score;


    private void Start()
    {
        score.text = "Score: " + PlayerPrefs.GetInt("Highscore", 0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
