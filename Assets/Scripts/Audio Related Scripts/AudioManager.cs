using UnityEngine;

namespace YY_Games_Scripts
{
    public class AudioManager : MonoBehaviour
    {
        #region Variables and References
        //Singleton
        public static AudioManager instance;

        [Header("Musics")]
        public AudioSource backgroundMusic, victoryMusic;

        [Header("Sound Effects")]
        public AudioSource[] sfx;
        #endregion
        #region Functions to play musics and sfxs
        //Function to play background music
        public void PlayBgm()
        {
            backgroundMusic.Play();
        }
        //Function to stop background music
        public void StopBgm()
        {
            backgroundMusic.Stop();
        }
        //Function to play level victory music
        public void PlayLevelVictory()
        {
            StopBgm();
            victoryMusic.Play();
        }
        //Function to play Sfxs music
        public void PlaySfx(int sfxNo)
        {
            sfx[sfxNo].Stop();
            sfx[sfxNo].Play();
        }
        //Function to stop spesific Sfx 
        public void StopSfx(int sfxNo)
        {
            sfx[sfxNo].Stop();
        }
        //Function to stop all Sfxs 
        public void StopAllSfx()
        {
            foreach (AudioSource allSfx in sfx)
            {
                allSfx.Stop();
            }
        }

        #endregion
        #region Unity Functions
        private void Awake()
        {
            instance = this;
        }
        #endregion
    }
}

