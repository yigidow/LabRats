using UnityEngine;
using UnityEngine.SceneManagement;

namespace YY_Games_Scripts
{
    public class CheckPointController : MonoBehaviour
    {
        public string checkpointName;
        void Start()
        {
            if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp"))
            {
                if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp") == checkpointName)
                {
                    PlayerControlller.instance.transform.position = transform.position;
                    Debug.Log("player start" + checkpointName);
                }
            }
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", "");
            }

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", checkpointName);

                AudioManager.instance.PlaySfx(1);
            }
        }
    }
}