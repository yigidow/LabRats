using UnityEngine;
using UnityEngine.SceneManagement;

namespace YY_Games_Scripts
{
    public class MainMenu : MonoBehaviour
    {
        public string firstLevel;
        public void PlayGame()
        {
            SceneManager.LoadScene(firstLevel);
        }

        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("Quit");
        }


    }
}