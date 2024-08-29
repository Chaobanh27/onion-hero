using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject endScreen;
    private float time;
    private int scoreValue;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= 1)
        {
            time += Time.deltaTime;
            //lấy phần dư sau khi chia cho 60, từ đó lấy số giây hiện tại
            int seconds = ((int)time % 60);
            //chia / 60 lấy phần nguyên khi chia cho 60, từ đó lấy số phút hiện tại
            int minutes = ((int)time / 60);
            clockText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void AddScore()
    {
        scoreValue++;
        scoreText.text = scoreValue.ToString("#,#");
    }

    public void UpdateAmmoInfo(int currentBullets, int maxBullets)
    {
        ammoText.text = currentBullets + "/" + maxBullets;
    }

    public void OpenEndScreen()
    {
        Time.timeScale = 0;

        endScreen.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
