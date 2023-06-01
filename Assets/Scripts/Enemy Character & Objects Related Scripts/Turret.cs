using UnityEngine;

namespace YY_Games_Scripts
{
    public class Turret : MonoBehaviour
    {
        #region Variables and References
        [Header("Turrent firearms variables")]
        public GameObject bullet;
        public float range, timeBetweenShots, rotateSpeed = 50f;
        private float timer;

        public Transform turretGun, firePoint;
        #endregion

        #region Unity Functions
        void Start()
        {
            timer = timeBetweenShots;
        }
        void Update()
        {
            if (!GameManager.instance.levelEnding)
            {
                if (Vector3.Distance(transform.position, PlayerControlller.instance.transform.position) < range)
                {
                    turretGun.LookAt(PlayerControlller.instance.transform.position + new Vector3(0f, 1.2f, 0f));

                    timer -= Time.deltaTime;

                    if (timer <= 0)
                    {
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        timer = timeBetweenShots;
                    }
                }
                else
                {
                    timer = timeBetweenShots;

                    turretGun.rotation = Quaternion.Lerp(turretGun.rotation, Quaternion.Euler(0f, turretGun.rotation.eulerAngles.y + 10, 0f), rotateSpeed * Time.deltaTime);
                }
            }
        }
        #endregion
    }
}