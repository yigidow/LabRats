using UnityEngine;

namespace YY_Games_Scripts
{
    public class BulletController : MonoBehaviour
    {
        #region Variables and References
        [Header("Variables for Different Bullet Types")]
        public float bulletSpeed;
        public float lifeTime;
        public GameObject impactEffect;
        public Rigidbody myRigidbody;
        public int damage = 2;

        [Header("Variables for Different Arrows")]
        public bool isArrow;
        public bool isLose;

        [Header("Variables for Damage Types")]
        public bool damageEnemy;
        public bool damagePlayer;
        #endregion
        #region Unity Functions
        void Update()
        {
            if (!isArrow) 
            {
                MoveBullet();
            }
            else
            {
                if (isLose)
                {
                    MoveBullet();
                }
            }

            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy" && damageEnemy)
            {
                other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(damage);
            }

            if (other.gameObject.tag == "EnemyHead" && damageEnemy)
            {
                other.transform.parent.GetComponent<EnemyHealth>().DamageEnemy(damage * 2);
            }
            if (other.gameObject.tag == "Player" && damagePlayer)
            {
                PlayerHealth.instance.DamagePayer(damage);
            }
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position + transform.forward * -bulletSpeed * Time.deltaTime, transform.rotation);
        }
        #endregion
        #region Function to move the bullet
        public void MoveBullet()
        {
            myRigidbody.velocity = transform.forward * bulletSpeed;
        }
        #endregion
    }
}