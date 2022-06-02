using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameoverPanel;
    public static bool isGameStarted;
    public GameObject StartingText;
    public static int numberOfCoins;
    public Text coinsText;
    

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameoverPanel.SetActive(true);
        }
        
         coinsText.text = "COINS:" + numberOfCoins; 
         if (numberOfCoins > PlayerPrefs.GetInt("highscore"))
         {
             PlayerPrefs.SetInt("highscore",numberOfCoins);
         }
         

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(StartingText);
        }
    }
}
