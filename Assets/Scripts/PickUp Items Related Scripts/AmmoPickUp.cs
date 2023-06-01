using UnityEngine;

namespace YY_Games_Scripts
{
    public class AmmoPickUp : MonoBehaviour
    {
        private bool collected;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !collected)
            {
                PlayerControlller.instance.myGun.GetAmmo();
                collected = true;
                Destroy(gameObject);

                AudioManager.instance.PlaySfx(3);
            }
        }
    }
}

