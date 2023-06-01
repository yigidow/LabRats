using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YY_Games_Scripts
{
    public class PlayerControlller : MonoBehaviour
    {
        #region Variables and References
        //Singleton
        public static PlayerControlller instance;

        [Header("Player movement variables")]
        public float moveSpeed;
        public float runSpeed;
        public float gravityMod;
        public float jumpPow;
        public CharacterController charCon;

        [Header("Player movement input variables")]
        public float mouseSens;
        private bool invertX;
        private bool invertY;
        private Vector3 moveInput;

        [Header("Player camera variables")]
        public Transform camTrans;
        public float maxViewAng = 60f;

        [Header("Variables to jump and doublejump")]
        public Transform groundCheck;
        public LayerMask whatIsGround;
        private bool canJump, canDoubleJump;

        [Header("Player firearms variables")]
        public Transform firePoint;
        public GunController myGun;
        public List<GunController> myGuns = new List<GunController>();
        public int currentGun;
        public List<GunController> unlockGuns = new List<GunController>();
        public Transform zoomPoint, gunHolder;
        private Vector3 gunStartPos;
        public float zoomSpeed = 2f;

        [Header("Variables to make player bounce at pad")]
        private float bounceAmount;
        private bool bounce;

        [Header("Variables to make player dash")]
        public float dashSpeed = 10f;
        public float dashForce = 5f;
        public bool canDash = true;
        private Vector3 dashDirection = Vector3.zero;

        [Header("Player animation variables")]
        public Animator animate;

        [Header("Player audio variables")]
        public AudioSource footStepFast;
        public AudioSource footStepSlow;

        #endregion
        #region Unity Functions
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            currentGun--;
            SwitchGunNext();
            gunStartPos = gunHolder.localPosition;
        }

        void Update()
        {
            if (!UIController.instance.pauseScreen.activeInHierarchy && !GameManager.instance.levelEnding)
            {
                float yStore = moveInput.y;

                Vector3 vertMove = transform.forward * Input.GetAxisRaw("Vertical");
                Vector3 horzMove = transform.right * Input.GetAxisRaw("Horizontal");

                moveInput = horzMove + vertMove;
                moveInput.Normalize();

                //To Run

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveInput *= runSpeed;
                }
                else
                {
                    moveInput *= moveSpeed;
                }

                //Gravity
                moveInput.y = yStore;
                moveInput.y += Physics.gravity.y * gravityMod * Time.deltaTime;

                if (charCon.isGrounded)
                {
                    moveInput.y = Physics.gravity.y * gravityMod * Time.deltaTime;
                }

                //Jump
                canJump = Physics.OverlapSphere(groundCheck.position, 0.25f, whatIsGround).Length > 0;

                if (Input.GetKeyDown(KeyCode.Space) && canJump)
                {
                    moveInput.y = jumpPow;
                    canDoubleJump = true;
                    AudioManager.instance.StopSfx(8);
                }
                else if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
                {
                    moveInput.y = jumpPow;
                    canDoubleJump = false;
                    AudioManager.instance.StopSfx(8);
                }

                if (bounce)
                {
                    bounce = false;
                    moveInput.y = bounceAmount;

                    canDoubleJump = true;
                }

                charCon.Move(moveInput * Time.deltaTime);

                //Dash Movement

                if (Input.GetKeyDown(KeyCode.LeftControl) && canDash)
                {
                    Dash();
                    canDash = false;
                    StartCoroutine(DashDelay());
                }
                //camera movement

                Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSens;

                if (invertX)
                {
                    mouseInput.x = -mouseInput.x;
                }
                if (invertY)
                {
                    mouseInput.y = -mouseInput.y;
                }
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
                camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

                if (camTrans.rotation.eulerAngles.x > maxViewAng && camTrans.rotation.eulerAngles.x < 180)
                {
                    camTrans.rotation = Quaternion.Euler(maxViewAng, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);

                }
                else if (camTrans.rotation.eulerAngles.x > 180 && camTrans.rotation.eulerAngles.x < 360f - maxViewAng)
                {
                    camTrans.rotation = Quaternion.Euler(-maxViewAng, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);
                }

                //For animation

                animate.SetFloat("MovSpeed", moveInput.magnitude);
                animate.SetBool("onGround", canJump);


                //For Shooting

                //Single Shots
                if (Input.GetMouseButtonDown(0) && myGun.fireCounter <= 0)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50))
                    {
                        if (Vector3.Distance(camTrans.position, hit.point) > 2)
                        {
                            firePoint.LookAt(hit.point);
                        }
                    }
                    else
                    {
                        firePoint.LookAt(camTrans.position + camTrans.forward * 50);
                    }
                    FireShot();

                }

                //swtich gun
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    SwitchGunNext();
                    CamController.instance.ZoomOut();
                }
                if (Input.mouseScrollDelta.y > 0)
                {
                    SwitchGunNext();
                    CamController.instance.ZoomOut();
                }

                if (Input.mouseScrollDelta.y < 0)
                {
                    SwitchGunPrev();
                    CamController.instance.ZoomOut();
                }

                //Zoom
                if (Input.GetMouseButtonDown(1))
                {
                    CamController.instance.ZoomIn(myGun.zoomAmount);
                    gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, gunStartPos, zoomSpeed * Time.deltaTime);
                }

                if (Input.GetMouseButton(1))
                {
                    gunHolder.position = Vector3.MoveTowards(gunHolder.position, zoomPoint.position, zoomSpeed * Time.deltaTime);
                }
                else
                {
                    gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, gunStartPos, zoomSpeed * Time.deltaTime);
                }
                if (Input.GetMouseButtonUp(1))
                {
                    CamController.instance.ZoomOut();
                }

                //Repeating Shots
                if (Input.GetMouseButton(0) && myGun.canAutoFire)
                {
                    if (myGun.fireCounter <= 0)
                    {
                        FireShot();
                    }
                }
            }
        }
        #endregion
        #region Functions to Get and Shoot firearms
        public void FireShot()
        {
            if (myGun.ammoCount > 0)
            {
                myGun.ammoCount--;
                Instantiate(myGun.bullet, firePoint.position, firePoint.rotation);
                myGun.fireCounter = myGun.fireRate;
                UIController.instance.ammo.text = "AMMO:" + myGun.ammoCount;
            }
        }

        public void SwitchGunNext()
        {
            myGun.gameObject.SetActive(false);
            currentGun++;

            if (currentGun >= myGuns.Count)
            {
                currentGun = 0;
            }
            myGun = myGuns[currentGun];
            myGun.gameObject.SetActive(true);

            UIController.instance.ammo.text = "AMMO:" + myGun.ammoCount;

            firePoint.position = myGun.firePoint.position;
        }
        public void SwitchGunPrev()
        {
            myGun.gameObject.SetActive(false);
            currentGun--;

            if (currentGun < 0)
            {
                currentGun = myGuns.Count - 1;
            }
            myGun = myGuns[currentGun];
            myGun.gameObject.SetActive(true);

            UIController.instance.ammo.text = "AMMO:" + myGun.ammoCount;

            firePoint.position = myGun.firePoint.position;
        }

        public void UnlockGun(string gunName)
        {
            bool gunUnlocked = false;

            if (unlockGuns.Count > 0)
            {
                for (int i = 0; i < unlockGuns.Count; i++)
                {
                    if (unlockGuns[i].gunName == gunName)
                    {
                        gunUnlocked = true;

                        myGuns.Add(unlockGuns[i]);

                        unlockGuns.RemoveAt(i);

                        i = unlockGuns.Count;
                    }
                }
            }
            else
            {
                Debug.Log("nope");
            }

            if (gunUnlocked == true)
            {
                currentGun = myGuns.Count - 2;
                SwitchGunNext();
            }
        }
        #endregion
        #region Function to additional movement
        public void Bounce(float bounceForce)
        {
            bounceAmount = bounceForce;
            bounce = true;
        }
        public void Dash()
        {
            dashDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
                dashDirection += transform.forward;
            if (Input.GetKey(KeyCode.S))
                dashDirection -= transform.forward;
            if (Input.GetKey(KeyCode.A))
                dashDirection -= transform.right;
            if (Input.GetKey(KeyCode.D))
                dashDirection += transform.right;

            dashDirection.Normalize();

            StartCoroutine(DashMovement());
        }

        private IEnumerator DashMovement()
        {
            float elapsedTime = 0f;
            Vector3 dashVector = dashDirection * dashSpeed * dashForce;

            while (elapsedTime < 0.3f)
            {
                charCon.Move(dashVector * Time.deltaTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        private IEnumerator DashDelay()
        {
            yield return new WaitForSeconds(1.5f);
            canDash = true;
        }
        #endregion
    }
    }