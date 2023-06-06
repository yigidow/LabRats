using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YY_Games_Scripts
{
    public class SwordControler : MonoBehaviour
    {
        #region Variables and References
        [Header("Variables for Sword")]
        public Transform firePoint;
        public float attackSpeed;
        public SphereCollider swordHitBox;
        public int damage = 2;
        public bool isSwinging = false;
        [HideInInspector] public double attackSpeedCounter;

        [Header("Variables for Damage Types")]
        public bool damageEnemy;
        public bool damagePlayer;

        [Header("Variables for Damage Types")]

        public string weaponName;
        #endregion

        #region Unity Functions
        void Update()
        {
            if (attackSpeedCounter > 0)
            {
                attackSpeedCounter -= Time.deltaTime;
            }
            if (isSwinging)
            {
                StartCoroutine(StopSwing());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isSwinging)
            {
                if (other.gameObject.tag == "Enemy" && damageEnemy)
                {
                    other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(damage);
                    
                    Debug.Log("Hit");
                }

                if (other.gameObject.tag == "Player" && damagePlayer)
                {
                    PlayerHealth.instance.DamagePayer(damage);
                }
            }
        }
        #endregion

        #region Functions To Control Sword
        public IEnumerator StopSwing()
        {
            yield return new WaitForSeconds(0.5f);
            isSwinging = false;
        }
        #endregion
    }
}