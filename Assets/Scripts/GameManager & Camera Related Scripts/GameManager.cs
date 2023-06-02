using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YY_Games_Scripts
{
    public class GameManager : MonoBehaviour
    {
        #region Variables and References
        //Singleton
        public static GameManager instance;

        [Header("Variables for Player Death")]
        public float waitAfterDied = 2f;
        public bool levelEnding;
        #endregion
        #region Function that called when player dies
        public void PlayerDied()
        {
            StartCoroutine(PlayerDeathCorutine());
        }

        public IEnumerator PlayerDeathCorutine()
        {
            yield return new WaitForSeconds(waitAfterDied);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion
        #region Pause & Unpause functions
        public void PauseUnPause()
        {
            if (UIController.instance.pauseScreen.activeInHierarchy)
            {
                UIController.instance.pauseScreen.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;

                PlayerControlller.instance.footStepFast.Play();
                PlayerControlller.instance.footStepSlow.Play();
                AudioManager.instance.PlayBgm();
            }
            else
            {
                UIController.instance.pauseScreen.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;

                AudioManager.instance.StopBgm();
                PlayerControlller.instance.footStepFast.Stop();
                PlayerControlller.instance.footStepSlow.Stop();
            }
        }
        #endregion
        #region Unity Functions
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            AudioManager.instance.StopAllSfx();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseUnPause();
            }
        }
        #endregion
    }
}