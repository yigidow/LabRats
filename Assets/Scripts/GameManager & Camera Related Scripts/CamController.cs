using UnityEngine;

namespace YY_Games_Scripts
{
    public class CamController : MonoBehaviour
    {
        #region Variables and References
        public static CamController instance;

        public Transform target;

        private float startFOV, targetFOV;
        public float zoomSpeed = 1f;
        public Camera myCam;
        #endregion
        #region Camera zoom in and zoom out functions
        public void ZoomIn(float newZoom)
        {
            targetFOV = newZoom;
        }
        public void ZoomOut()
        {
            targetFOV = startFOV;
        }
        #endregion
        #region Unity Functions
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            startFOV = myCam.fieldOfView;
            targetFOV = startFOV;
        }

        void LateUpdate()
        {
            transform.position = target.position;
            transform.rotation = target.rotation;

            myCam.fieldOfView = Mathf.Lerp(myCam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
        }
        #endregion
    }
}