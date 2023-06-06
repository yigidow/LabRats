using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YY_Games_Scripts
{
    public class ClassPickManager : MonoBehaviour
    {
        #region Variables and References
        //Singleton
        public static ClassPickManager instance;

        [Header("Class variables")]
        public List<GameObject> allClasses = new List<GameObject>();
        public string pickedClass;

        #endregion

        #region Functions for pickin a Class
        public void SetPickedClass(string className)
        {
            //Get the name of chosen class
            pickedClass = className;
            
            //Destroy the classPick Objects
            foreach (GameObject playerClass in allClasses)
            {
                Destroy(playerClass);
            }

            //Set up class values

            //GunMan
            if(pickedClass == "GunMan")
            {
                SetForGunMan();
            }

            //BowMan
            if (pickedClass == "BowMan")
            {
                SetForBowMan();
            }

            //SwordMan
            if (pickedClass == "SwordMan")
            {
                SetForSwordMan();
            }

        }

        private void SetForGunMan()
        {
            //Setting Class
            PlayerControlller.instance.isGunMan = true;
            //Ammo
            UIController.instance.ammoPanel.SetActive(true);
            //Health
            PlayerHealth.instance.maxHealth = 100;
            PlayerHealth.instance.SetHealthValues();

            //Stamina
            PlayerStamina.instance.maxStamina = 10;
            PlayerStamina.instance.SetStaminaValues();

            //BulletTime
            PlayerBulletTime.instance.maxBulletTime = 0;

            //Movement Speed & Run Speed
            PlayerControlller.instance.moveSpeed = 8f;
            PlayerControlller.instance.runSpeed = 13f;

            //Jump Power
            PlayerControlller.instance.jumpPow = 8;
            PlayerControlller.instance.ableToDoubleJump = false;

            //Equip the weapon
            PlayerControlller.instance.SetClassWeapon();
        }
        private void SetForBowMan()
        {
            //Setting Class
            PlayerControlller.instance.isBowMan = true;
            //Health
            PlayerHealth.instance.maxHealth = 150;
            PlayerHealth.instance.SetHealthValues();

            //Stamina
            PlayerStamina.instance.maxStamina = 10;
            PlayerStamina.instance.SetStaminaValues();

            //BulletTime
            PlayerBulletTime.instance.maxBulletTime = 10;
            PlayerBulletTime.instance.SetBulletTimeValues();

            //Movement Speed & Run Speed
            PlayerControlller.instance.moveSpeed = 10f;
            PlayerControlller.instance.runSpeed = 15f;

            //Jump Power
            PlayerControlller.instance.jumpPow = 10;
            PlayerControlller.instance.ableToDoubleJump = false;

            //Equip the weapon
            PlayerControlller.instance.SetClassWeapon();

        }
        private void SetForSwordMan()
        {
            //Setting Class
            PlayerControlller.instance.isSwordMan = true;
            //Health
            PlayerHealth.instance.maxHealth = 200;
            PlayerHealth.instance.SetHealthValues();

            //Stamina
            PlayerStamina.instance.maxStamina = 15;
            PlayerStamina.instance.SetStaminaValues();

            //BulletTime
            PlayerBulletTime.instance.maxBulletTime = 15;
            PlayerBulletTime.instance.SetBulletTimeValues();

            //Movement Speed & Run Speed
            PlayerControlller.instance.moveSpeed = 12f;
            PlayerControlller.instance.runSpeed = 18f;

            //Jump Power
            PlayerControlller.instance.jumpPow = 12;
            PlayerControlller.instance.ableToDoubleJump = true;

            //Equip the weapon
            PlayerControlller.instance.SetClassWeapon();
        }
        #endregion

        #region Unity Functions
        private void Awake()
        {
            instance = this;
        }
        #endregion
    }
}