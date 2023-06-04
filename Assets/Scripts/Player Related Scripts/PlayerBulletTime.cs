
using UnityEngine;

namespace YY_Games_Scripts
{
    public class PlayerBulletTime : MonoBehaviour
    {
        #region Variables and References
        //Singleton
        public static PlayerBulletTime instance;

        [Header("Player BulletTime Variables")]
        public int maxBulletTime;
        public float currentBulletTime;

        [Header("Player BulletTime Control Variables")]
        public float bulletTimeDeccreaseRate = 2;
        public float bulletTimeIncreaseRate = 4;
        public float bulletTimeCooldown = 2f;
        #endregion

        #region Functions to Control Player BulletTime
        public void DecreaseBulletTime()
        {
            currentBulletTime -= Time.deltaTime * bulletTimeDeccreaseRate;
            if (currentBulletTime <= 0)
            {
                currentBulletTime = 0;
            }
            UIController.instance.bulletTimeBar.value = currentBulletTime;
            bulletTimeCooldown = 3f;
        }

        public void IncreaseBulletTime()
        {
            if (bulletTimeCooldown <= 0)
            {
                currentBulletTime += Time.deltaTime * bulletTimeIncreaseRate;
                if (currentBulletTime >= maxBulletTime)
                {
                    currentBulletTime = maxBulletTime;
                }
                UIController.instance.bulletTimeBar.value = currentBulletTime;
            }
        }

        public void SetBulletTimeValues()
        {
            currentBulletTime = maxBulletTime;
            if(maxBulletTime > 0)
            {
                UIController.instance.bulletTimeBar.value = currentBulletTime;
                UIController.instance.bulletTimeBar.maxValue = maxBulletTime;
                UIController.instance.bulletTinePanel.SetActive(true);
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
            currentBulletTime = maxBulletTime;
            UIController.instance.bulletTimeBar.maxValue = maxBulletTime;
        }
        private void Update()
        {
            bulletTimeCooldown -= Time.deltaTime;
            if (bulletTimeCooldown <= 0)
            {
                bulletTimeCooldown = 0;
            }
        }

        #endregion
    }
}