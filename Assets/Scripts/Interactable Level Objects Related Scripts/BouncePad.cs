using UnityEngine;

namespace YY_Games_Scripts
{
    public class BouncePad : MonoBehaviour
    {
        public float bounceForce;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                PlayerControlller.instance.Bounce(bounceForce);
                AudioManager.instance.PlaySfx(0);
            }
        }
    }
}