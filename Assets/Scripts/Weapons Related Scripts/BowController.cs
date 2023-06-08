using UnityEngine;

namespace YY_Games_Scripts
{
    public class BowController : MonoBehaviour
    {
        #region Variables and References
        [Header("Variables for Bow and arrows")]
        public GameObject arrow;
        public GameObject nockedArrow;
        public Transform nockPoint;
        public float fireRate;
        public float zoomAmount;
        public string weaponName;

        public Animation bowAnimation;

        [HideInInspector] public double fireCounter;
        #endregion

        #region Unity Functions
        void Update()
        {
            if (fireCounter > 0)
            {
                fireCounter -= Time.deltaTime;
            }
            if(nockedArrow != null)
            {
                nockedArrow.transform.position = nockPoint.transform.position;
                nockedArrow.transform.rotation = nockPoint.transform.rotation;
            }

        }
        #endregion
    }
}