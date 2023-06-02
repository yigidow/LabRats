using UnityEngine;

namespace YY_Games_Scripts
{
    public class PlayerStamina : MonoBehaviour
    {
        #region Variables and References
        //Singleton
        public static PlayerStamina instance;

        [Header("Player Stamina Variables")]
        public int maxStamina;
        public float currentStamina;

        [Header("Player Stamina Control Variables")]
        public float staminaDecreaseRate = 2f;
        public float staminaIncreaseRate = 4f;
        public float staminaCooldown = 2f;
        #endregion

        #region Functions to Control Player Stamina

        public void DecreaseStaminaForDash()
        {
            currentStamina -= staminaDecreaseRate;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
            }
            UIController.instance.staminaBar.value = currentStamina;
            staminaCooldown = 2f;
        }
        public void DecreaseStaminaForRun()
        {
            currentStamina -= Time.deltaTime * staminaIncreaseRate;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
            }
            UIController.instance.staminaBar.value = currentStamina;
            staminaCooldown = 2f;
        }

        public void IncreaseStamina()
        {
            if(staminaCooldown <= 0)
            {
                currentStamina += Time.deltaTime * staminaIncreaseRate;
                if (currentStamina >= maxStamina)
                {
                    currentStamina = maxStamina;
                }
                UIController.instance.staminaBar.value = currentStamina;
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
            currentStamina = maxStamina;
            UIController.instance.staminaBar.maxValue = maxStamina;
        }
        private void Update()
        {
            staminaCooldown -= Time.deltaTime;
            if(staminaCooldown <= 0)
            {
                staminaCooldown = 0;
            }
        }

        #endregion
    }
}