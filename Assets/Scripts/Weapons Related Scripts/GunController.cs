using UnityEngine;

namespace YY_Games_Scripts
{
    public class GunController : MonoBehaviour
    {
        #region Variables and References
        [Header("Variables for Different Gun Types")]
        public GameObject bullet;

        public Transform firePoint;
        public bool canAutoFire;
        public float fireRate;
        public int ammoCount, ammoPickAmount;
        public float zoomAmount;
        public string gunName;

        public Animation gunAnimation;

        [HideInInspector] public double fireCounter;
        #endregion
        #region Functions to add more ammo to gun
        public void GetAmmo()
        {
            ammoCount += ammoPickAmount;
            UIController.instance.ammo.text = "AMMO:" + ammoCount;
        }
        #endregion
        #region Unity Functions
        void Start()
        {
            UIController.instance.ammo.text = "AMMO:" + ammoCount;
        }

        void Update()
        {
            if (fireCounter > 0)
            {
                fireCounter -= Time.deltaTime;
            }
        }
        #endregion
    }
}