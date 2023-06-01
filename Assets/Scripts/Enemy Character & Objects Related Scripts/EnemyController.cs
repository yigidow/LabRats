using UnityEngine;
using UnityEngine.AI;

namespace YY_Games_Scripts
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables and References
        [Header("Variables for chasing Enemies")]
        public bool chasing;
        public float distanceToChase = 10f;
        public float distanceToLose = 15f;
        public float distanceToStop = 2f;
        public float keepChasingTime = 5f;
        private float chaseCounter;
        private Vector3 targetPoint, startPoint;

        [Header("Enemy navAgent")]
        public NavMeshAgent navAgent;

        [Header("Variables for enemy firearms")]
        public GameObject bullet;
        public Transform firePoint;

        public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
        private float fireCount, shotWaitCounter, shootTimeCounter;


        public Animator anim;

        private bool wasShot;
        #endregion
        #region Function to shoot the enemy
        public void GetShot()
        {
            wasShot = true;
            chasing = true;
        }
        #endregion
        #region Unity Functions
        void Start()
        {
            startPoint = transform.position;

            shootTimeCounter = timeToShoot;
            shotWaitCounter = waitBetweenShots;
        }

        void Update()
        {
            targetPoint = PlayerControlller.instance.transform.position;

            if (!chasing && !GameManager.instance.levelEnding)
            {
                if (Vector3.Distance(transform.position, PlayerControlller.instance.transform.position) < distanceToChase)
                {
                    chasing = true;

                    shootTimeCounter = timeToShoot;
                    shotWaitCounter = waitBetweenShots;
                }

                if (chaseCounter > 0)
                {
                    chaseCounter -= Time.deltaTime;

                    if (chaseCounter < 0)
                    {
                        navAgent.destination = startPoint;
                    }
                }

                if (navAgent.remainingDistance < 0.25f)
                {
                    anim.SetBool("isMoving", false);
                }
                else
                {
                    anim.SetBool("isMoving", true);
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
                {
                    navAgent.destination = targetPoint;
                }
                else
                {
                    navAgent.destination = transform.position;
                }

                if (Vector3.Distance(transform.position, PlayerControlller.instance.transform.position) > distanceToLose)
                {
                    if (!wasShot)
                    {
                        chasing = false;

                    }
                }
                else
                {
                    wasShot = false;
                }

                if (shotWaitCounter > 0)
                {
                    shotWaitCounter -= Time.deltaTime;

                    if (shotWaitCounter <= 0)
                    {
                        shootTimeCounter = timeToShoot;
                    }
                    anim.SetBool("isMoving", true);
                }
                else
                {
                    if (PlayerControlller.instance.gameObject.activeInHierarchy)
                    {
                        shootTimeCounter -= Time.deltaTime;

                        if (shootTimeCounter > 0)
                        {
                            fireCount -= Time.deltaTime;

                            if (fireCount <= 0)
                            {
                                fireCount = fireRate;

                                firePoint.LookAt(PlayerControlller.instance.transform.position + new Vector3(0f, 1.5f, 0f));

                                //Check the angle

                                Vector3 targetDirection = PlayerControlller.instance.transform.position - transform.position;
                                float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

                                if (Mathf.Abs(angle) < 30)
                                {
                                    Instantiate(bullet, firePoint.position, firePoint.rotation);

                                    anim.SetTrigger("fireShot");
                                }
                                else
                                {
                                    shotWaitCounter = waitBetweenShots;
                                }
                            }
                            navAgent.destination = transform.position;
                        }
                        else
                        {
                            shotWaitCounter = waitBetweenShots;
                        }
                    }
                    anim.SetBool("isMoving", false);
                }
            }
        }

        #endregion
    }
}