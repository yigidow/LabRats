using UnityEngine;

namespace YY_Games_Scripts
{
    public class EnemyHealth : MonoBehaviour
    {
        #region Variables and References
        public int currentHealth = 3;
        public EnemyController ec;
        #endregion

        #region Function to damage the Enemy
        public void DamageEnemy(int damageAmount)
        {
            currentHealth -= damageAmount;

            if (ec != null)
            {
                ec.GetShot();
            }

            if (currentHealth <= 0)
            {
                Destroy(gameObject);

                AudioManager.instance.PlaySfx(2);
            }
        }
        #endregion
    }
}