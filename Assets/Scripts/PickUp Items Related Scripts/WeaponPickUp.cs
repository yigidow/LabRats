using UnityEngine;

namespace YY_Games_Scripts
{
    public class WeaponPickUp : MonoBehaviour
    {
        public string gunName;
        private bool collected;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !collected)
            {
                PlayerControlller.instance.UnlockGun(gunName);
                collected = true;
                Destroy(gameObject);
                AudioManager.instance.PlaySfx(4);
            }
        }
    }
}