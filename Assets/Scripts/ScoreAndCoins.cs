using TMPro;
using UnityEngine;

public class ScoreAndCoins : MonoBehaviour
{
    [Header("Score Settings")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    [Header("Coin Settings")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI bestCoinText;

    [HideInInspector] public int score;
    [HideInInspector] public int bestScore;

    [HideInInspector] public int coin;
    [HideInInspector] public int bestCoin;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "" + bestScore;
        scoreText.text = "Score: 0";

        bestCoin = PlayerPrefs.GetInt("BestCoin", 0);
        bestCoinText.text = "" + bestCoin;
        coinText.text = "0";

        if(SettingsMenu.isGameActive == true)
        {
            score = 0;
            scoreText.text = "0";
        }
    }

    private void Update()
    {
        if (SettingsMenu.isGameActive == true && SettingsMenu.isPauseMenu == false)
        {
            score++;
            scoreText.text = "" + score;

            coin = SettingsCoin.coin;
            coinText.text = "" + coin;
        }

        if(PlayerController.fallTrue == true)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SettingsMenu.isGameActive = false;

        //Score Settings
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            bestScoreText.text = "0" + bestScore;
        }

        //Coin Settings
        if (coin > bestCoin)
        {
            bestCoin = coin;
            PlayerPrefs.SetInt("BestCoin", bestCoin);
            bestCoinText.text = "0" + bestCoin;
        }
    }
}
