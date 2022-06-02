using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text HSText;
    private void Start()
    {
        HSText.text = "HIGHSCORE:" + PlayerPrefs.GetInt("highscore");
    }
    public void PlayGame()
    {
       SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
