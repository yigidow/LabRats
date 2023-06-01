using UnityEngine;
using UnityEngine.SceneManagement;

namespace YY_Games_Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        public string mainMenu;
        public void Resume()
        {
            GameManager.instance.PauseUnPause();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(mainMenu);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}