using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
  [SerializeField] GameObject GameOver;
  
   public void ReplayGame()
   {
     SceneManager.LoadScene("Level");
   }

   public void QuitGame()
   {
       Application.Quit();
   }

   public void Home()
   {
     SceneManager.LoadScene("Menu");
   }
}
