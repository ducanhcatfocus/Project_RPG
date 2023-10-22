using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIIngame : MonoBehaviour
{

    static UIIngame instance;

    public static UIIngame Instance => instance;

    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider waveTimeSlider;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject waveNoti;
    [SerializeField] private GameObject screenFadeOut;
    [SerializeField] private float dashCooldown;



    [SerializeField] TextMeshProUGUI waveTextNoti;
    [SerializeField] TextMeshProUGUI killScoreText;
    [SerializeField] TextMeshProUGUI killHighScoreText;



    [SerializeField] private Image dashImage;
    [SerializeField] private Image attackImage;
    [SerializeField] private Image flaskImage;






    private void Awake()
    {
        instance = this;
    }



    void Start()
    {
        if (playerStats != null)
        {
            playerStats.onHPChanged += UpdateHealthUI;
        }

        dashCooldown = SkillManager.Instance.dash.cooldown;

    }

    private void Update()
    {


        CheckCD(dashImage, dashCooldown, KeyCode.K);
        CheckCD(attackImage, 0.2f, KeyCode.J);
        CheckCD(flaskImage, 10, KeyCode.Alpha1);


    }

    private void CheckCD(Image image, float timeCd, KeyCode key)
    {
        if (Input.GetKeyUp(key))
        {
            SetCoolDownOff(image);
        }
        CheckCoolDownOf(image, timeCd);
    }


    private void UpdateHealthUI()
    {
        hpSlider.maxValue = playerStats.GetMaxHealth();
        hpSlider.value = playerStats.curentHp;

    }
    public void UpdateWaveTimeSlider(float totalTime, float currentTime)
    {
        waveTimeSlider.maxValue = totalTime;
        waveTimeSlider.value = currentTime;
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = "Wave " + wave.ToString();
    }

    public void DisplayWaveNoti()
    {
        waveTextNoti.text = waveText.text + " is coming!";
        waveNoti.SetActive(true);
    }

    public void HideWaveNoti()
    {
        waveNoti.GetComponent<Animator>().SetBool("PopDown", true);

        Invoke("ResetWaveNoti", 2);
    }

    void ResetWaveNoti()
    {
        waveNoti.GetComponent<Animator>().SetBool("PopDown", false);

        waveNoti.SetActive(false);
    }

    public void DisplayDeathScreen(int score, bool isBestScore)
    {
        if (isBestScore)
            killHighScoreText.text = "New Record " + score.ToString();
        else
            killHighScoreText.text = "Kills: " + score.ToString();

        screenFadeOut.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene(0);
    }


    private void SetCoolDownOff(Image image)
    {
        if(image.fillAmount <= 0)
        {
            image.fillAmount = 1;
        }
    }

    private void CheckCoolDownOf(Image image, float cd)
    {
        if(image.fillAmount > 0)
        {
            image.fillAmount -= 1 / cd * Time.deltaTime;
        }
    }

    public void UpdateKillScore(int score)
    {
        killScoreText.text = "Kills: " + score.ToString();
    }
}
