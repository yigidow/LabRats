using UnityEngine;

namespace YY_Games_Scripts
{
    public class PlayerHealth : MonoBehaviour
    {
        #region Variables and References
        //Singleton
        public static PlayerHealth instance;

        [Header("Player Health variables")]
        public int maxHealth;
        public int currentHealth;

        [Header("Variables to have invisible frames")]
        public float invisTime;
        private float invisCount;

        #endregion
        #region Functions to Damage and Increase the health of player
        public void DamagePayer(int damage)
        {
            if (invisCount <= 0 && !GameManager.instance.levelEnding)
            {
                AudioManager.instance.PlaySfx(7);

                currentHealth -= damage;

                UIController.instance.showDamage();

                if (currentHealth <= 0)
                {
                    gameObject.SetActive(false);

                    currentHealth = 0;

                    GameManager.instance.PlayerDied();
                    AudioManager.instance.StopBgm();
                    AudioManager.instance.StopSfx(7);
                    AudioManager.instance.PlaySfx(6);
                }

                invisCount = invisTime;

                UIController.instance.healthBar.value = currentHealth;
                UIController.instance.health.text = "HEALTH:" + currentHealth + "/" + maxHealth;
            }
        }

        public void HealPLayer(int healAmount)
        {
            currentHealth += healAmount;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            UIController.instance.healthBar.value = currentHealth;
            UIController.instance.health.text = "HEALTH:" + currentHealth + "/" + maxHealth;
        }

        public void SetHealthValues()
        {
            currentHealth = maxHealth;
            UIController.instance.healthBar.value = currentHealth;
            UIController.instance.healthBar.maxValue = maxHealth;
            UIController.instance.health.text = "HEALTH:" + currentHealth + "/" + maxHealth;
            UIController.instance.healthPanel.SetActive(true);
        }
        #endregion
        #region Unity Functions
        private void Awake()
        {
            instance = this;
        }
        void Update()
        {
            if (invisCount > 0)
            {
                invisCount -= Time.deltaTime;
            }
        }
        #endregion
    }
}