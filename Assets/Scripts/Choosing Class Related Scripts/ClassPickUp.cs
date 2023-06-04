using UnityEngine;

namespace YY_Games_Scripts
{
    public class ClassPickUp : MonoBehaviour
    {
        public string className;
        private bool isTriggered;
        private bool picked;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !picked)
            {
                isTriggered = true;           
            }
        }
        private void OnTriggerExit(Collider other)
        {
            isTriggered = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && isTriggered)
            {
                PickClass();
            }
        }

        private void PickClass()
        {
            picked = true;
            ClassPickManager.instance.SetPickedClass(className);
            AudioManager.instance.PlaySfx(4);
        }
    }
}