using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace YY_Games_Scripts
{
    public class VictoryScreen : MonoBehaviour
    {
        #region Variables and References
        [Header("Main Menu Name")]
        public string mainMenu;

        [Header("Victory Screen Components")]
        public GameObject textBox;
        public GameObject returnButton;
        public Image blackScreen;
        #endregion
        #region Victory Screen Functions
        public void MainMenu()
        {
            SceneManager.LoadScene(mainMenu);
        }
        public IEnumerator ShowTexts()
        {
            yield return new WaitForSeconds(1f);

            textBox.SetActive(true);

            yield return new WaitForSeconds(1f);

            returnButton.SetActive(true);
        }
        #endregion
        #region Unity Functions
        void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            StartCoroutine(ShowTexts());
        }

        void Update()
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, 3f * Time.deltaTime));
        }
        #endregion
    }
}