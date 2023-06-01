using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YY_Games_Scripts
{
    public class LevelExit : MonoBehaviour
    {
        public string nextLevel;
        public float waitTime;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GameManager.instance.levelEnding = true;

                StartCoroutine(EndLevel());

                AudioManager.instance.StopAllSfx();
                AudioManager.instance.PlayLevelVictory();
            }
        }
        private IEnumerator EndLevel()
        {
            PlayerPrefs.SetString(nextLevel + "_cp", "");

            yield return new WaitForSeconds(waitTime);

            SceneManager.LoadScene(nextLevel);
        }
    }
}