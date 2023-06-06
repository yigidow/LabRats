using UnityEngine;

namespace YY_Games_Scripts
{
    public class BowController : MonoBehaviour
    {
        #region Variables and References
        [Header("Variables for Bow and arrows")]
        public GameObject arrow;
        public GameObject nockedArrow;
        public bool isArrowNocked;

        public Transform nockPoint;
        public Transform losePoint;

        public Vector3 originalNockPos;

        private float nockSpeed = 1f;
        private float t = 0f;

        public float fireRate;
        public float zoomAmount;

        public string weaponName;

        [HideInInspector] public double fireCounter;
        #endregion
        #region Unity Functions
        private void Awake()
        {
            originalNockPos = nockPoint.position;
        }
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
            if (isArrowNocked)
            {
                t += Time.deltaTime * nockSpeed;
                t = Mathf.Clamp01(t);
                //nockPoint.position = Vector3.Lerp(nockPoint.position,losePoint.position,t);
            }
            else
            {
                //nockPoint.position = originalNockPos;
            }
        }
        #endregion
    }
}