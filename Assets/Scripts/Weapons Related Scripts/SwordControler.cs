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

            HitEnemy();
   
        }

        private void HitEnemy()
        {
            if (isSwinging)
            {
                RaycastHit hit;

                if (Physics.SphereCast(firePoint.position, 1f, firePoint.forward, out hit, 0.5f))
                {
                    if (hit.transform.tag == "Enemy" && damageEnemy)
                    {
                        hit.transform.gameObject.GetComponent<EnemyHealth>().DamageEnemy(damage);
                        Debug.Log("hit");
                    }
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

        //private void OnDrawGizmosSelected()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawSphere(firePoint.position, 2f);
        //    Gizmos.DrawWireSphere(firePoint.position, 2f);
        //}
    }
}