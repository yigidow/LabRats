using UnityEngine;

namespace YY_Games_Scripts
{
    public class BowController : MonoBehaviour
    {
        #region Variables and References
        [Header("Variables for Bow and arrows")]
        public GameObject arrow;
        public GameObject nockedArrow;

        public Transform firePoint;

        public float fireRate;
        public float zoomAmount;

        public string gunName;

        [HideInInspector] public double fireCounter;
        #endregion
        #region Unity Functions
        void Start()
        {

        }

        void Update()
        {
            if (fireCounter > 0)
            {
                fireCounter -= Time.deltaTime;
            }
            if(nockedArrow != null)
            {
                nockedArrow.transform.position = firePoint.transform.position;
                nockedArrow.transform.rotation = firePoint.transform.rotation;
            }
        }
        #endregion
    }
}