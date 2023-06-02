using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YY_Games_Scripts
{
    public class UIController : MonoBehaviour
    {
        #region Variables and References
        //Singleton
        public static UIController instance;

        [Header("Player Health UI")]
        public Slider healthBar;
        public TextMeshProUGUI health;

        [Header("Player Stamina UI")]
        public Slider staminaBar;
        public TextMeshProUGUI stamina;

        [Header("Player Ammo UI")]
        public TextMeshProUGUI ammo;

        [Header("Player Damage Effects")]
        public Image damageEffect;
        public float damageAlpha = 0.25f, damageFadeSpeed = 2f;

        [Header("Pause Screen")]
        public GameObject pauseScreen;

        [Header("Fade Components after loading the level")]
        public Image fadeImg;
        public float fadeSpeed = 1f;
        #endregion
        #region Damage Effect Functions
        public void showDamage()
        {
            damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, damageAlpha);
        }
        #endregion
        #region Unity Functions
        private void Awake()
        {
            instance = this;
        }
        void Update()
        {
            // Returns the damage Effect color to 0 slowly
            if (damageEffect.color.a != 0)
            {
                damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, Mathf.MoveTowards(damageEffect.color.a, 0f, damageFadeSpeed * Time.deltaTime));
            }

            // Fade out and fade in on level changes
            if (!GameManager.instance.levelEnding)
            {
                fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, Mathf.MoveTowards(fadeImg.color.a, 0f, fadeSpeed * Time.deltaTime));
            }
            else
            {
                fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, Mathf.MoveTowards(fadeImg.color.a, 1f, fadeSpeed * Time.deltaTime));
            }
        }
        #endregion
    }
}