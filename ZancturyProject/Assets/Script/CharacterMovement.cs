using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CharacterMovement : MonoBehaviour
{
    #region Create Variable
    //MaxHP
    public float maxLife;
    public float playerLife;

    public playerInventory _playerInventory;

    public string currentHungryState1;
    public string currentHPState1;

    public string currentHungryState2;
    public string currentHPState2;

    public string currentHungryState3;
    public string currentHPState3;

    public SliderBar hpBar;

    [SerializeField] private Avatar carterAvatar;
    [SerializeField] private Avatar lukeAvatar;
    [SerializeField] private Avatar hannahAvatar;

    [SerializeField] public GameObject carterObj;
    [SerializeField] public GameObject lukeObj;
    [SerializeField] public GameObject hannahObj;

    [Header("================ Move & rotation ================")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    public Camera cam;
    CameraFollow camfollow;
    public bool canMove;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isWalk;
    [SerializeField] private bool isRun;

    private Vector2 move, mouseLook;
    private Vector3 rotationTarget;

    public float turnSmoothTime;
    float turnSmoothVelocity;

    CombatSystem _combo;
    [SerializeField] private float anim_ForwardAmout;
    [SerializeField] private float anim_TurnAmout;

    [Header("================ RageCombat ================")]
    [SerializeField] bool isShoot;
    public bool isAimMode;
    [SerializeField] private bool isAim;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Transform bulletSpawnPoint;

    public Vector3 hitDirection;
    public Vector3 hitPosition;

    [Header("================ Cround ================")]
    CapsuleCollider capcollider;
    Rigidbody rb;
    [SerializeField] private bool isCrouch = false;
    [SerializeField] private float crouchSpeed;

    [Header("================ Interaction ================")]
    [SerializeField] private float interacRage = 10f;
    public LayerMask interactLayer;
    public bool isChecked;
    public GameObject interacUI;
    public GameObject coutDownUI;

    [Header("================ Weapon ================")]
    public GameObject glock;
    public GameObject m16;
    public GameObject shootgun;
    public GameObject knife;
    public GameObject ironBar;
    public GameObject baseballBat;

    [Header("================ SoundInterac ================")]
    [SerializeField] private float baseNoisLeght;
    [SerializeField] LayerMask enemyLayerMask;

    [Header(" ================ Gun ================ ")]
    public bool gunGlock;
    public bool gunM16;
    public bool gunShootGun;

    public int ammoCount;
    public int currentRifleAmmoInStake;
    public int currentGlockAmmoInStake;
    public int currentShotgunAmmoInStake;


    [SerializeField] private int stackAmmo;
    [SerializeField] private float reloadDelay;
    [SerializeField] private float currentDeley;
    [SerializeField] private bool isReload;

    [SerializeField] private float shotgunReloadDelay;
    [SerializeField] private float currentShotgunDelay;

    [SerializeField] private float fireRate;
    [SerializeField] private float currentFireRateTime;

    [SerializeField] float _yOffset;
    public Vector3 spawnDirectionBulletPoint;
    [SerializeField] GameObject crosshair;

    [Header(" ================ Melee ================ ")]
    public bool isknife;
    public bool isBaseballBat;
    public bool isIronBar;

    [Header("============ Stamina =============")]
    public SliderBar staminaBar;
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float maxStaminaThisRound;
    public float currentStamina;
    [SerializeField] private float staminaDepletion;
    [SerializeField] private float staminaRechargeRate;
    [SerializeField] private float staminaRechargeDelay;
    [SerializeField] private float _currentStaminaDelayCounter;

    [Header("============ Dead =============")]
    public GameObject DiePanel;
    public bool isDead;

    [Header("============ Aduio =============")]
    float chageFootStepDuration;
    float currentChageFootStepTime;

    public AudioSource audioSource;
    public List<AudioClip> footStep_List = new List<AudioClip>();
    int randomInt;

    [SerializeField] AudioClip M16Sound;
    [SerializeField] AudioClip GlockSound;
    [SerializeField] AudioClip ShotgunSound;

    int hitInt;
    [SerializeField] List<AudioClip> playerHit = new List<AudioClip>();

    [SerializeField] AudioClip RifleReloadSound;
    [SerializeField] AudioClip GlockReloadSound;
    [SerializeField] AudioClip ShotgunReloadSound;
    [Header("============= Particle =============")]
    [SerializeField] ParticleSystem blood;
    //[SerializeField] ParticleSystem ripple;
    [SerializeField] GameObject bulletCap;
    [SerializeField] GameObject shotgunCap;
    [SerializeField] GameObject glockCap;

    Transform muzzlePistolTranform;
    [SerializeField] GameObject muzzlePistol;

    Transform muzzleRifleTranform;
    [SerializeField] GameObject muzzleRifle;

    Transform muzzleShotgunTranform;
    [SerializeField] GameObject muzzleShotgun;
    #endregion
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }
    private void Awake()
    {
        #region Setup and GetComponent
        animator = GetComponent<Animator>();
        isAim = false;
        isAimMode = false;
        capcollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        _playerInventory = GetComponent<playerInventory>();
        _combo = GetComponent<CombatSystem>();
        camfollow = GameObject.Find("CameraController").GetComponent<CameraFollow>();
        isDead = false;
        muzzleRifleTranform = GameObject.Find("RifleMuzzleSpawnPoint").transform;
        muzzlePistolTranform = GameObject.Find("GlockMuzzleSpawnPoint").transform;
        muzzleShotgunTranform = GameObject.Find("ShotgunMuzzleSpawnPoint").transform;
        #endregion

        #region SelectCharacter
        if (SceneManager.GetActiveScene().name == "LootMap")
        {
            if (StateVariableController.player1Select == true)
            {
                //HP Setup ==================================================
                playerLife = StateVariableController.player1CurrentLife;
                hpBar.SetMaxValue(StateVariableController.carterMaxLife, playerLife);
                maxLife = StateVariableController.carterMaxLife;
                animator.avatar = carterAvatar;
                carterObj.SetActive(true);
                lukeObj.SetActive(false);
                hannahObj.SetActive(false);
                //Speed Setup ==================================================
                var carterWalkSpeed = walkSpeed * StateVariableController.carterMultipleWalkSpeed;
                var carterRunSpeed = runSpeed * StateVariableController.carterMultipleWalkSpeed;
                walkSpeed = walkSpeed - carterWalkSpeed;
                runSpeed = runSpeed - carterRunSpeed;
                //Reload Setup ==================================================
                reloadDelay = 2;
                currentDeley = 2;
                //Stamina Setup ==================================================
                maxStaminaThisRound = StateVariableController.player1MaxStamina;
                staminaBar.SetMaxValue(maxStamina, maxStaminaThisRound);
            }
            else if (StateVariableController.player2Select == true)
            {
                //HP Setup ==================================================
                playerLife = StateVariableController.player2CurrentLife;
                hpBar.SetMaxValue(maxLife, playerLife);
                animator.avatar = lukeAvatar;
                carterObj.SetActive(false);
                lukeObj.SetActive(true);
                hannahObj.SetActive(false);

                //Reload Setup ==================================================
                reloadDelay = StateVariableController.lukeReloadDelay;
                currentDeley = StateVariableController.lukeReloadDelay;
                //Stamina Setup ==================================================
                maxStaminaThisRound = StateVariableController.player2MaxStamina;
                maxStamina = StateVariableController.lukeMaxStamina;
                staminaBar.SetMaxValue(maxStamina, maxStaminaThisRound);
            }
            else if (StateVariableController.player3Select == true)
            {
                //HP Setup ==================================================
                playerLife = StateVariableController.player3CurrentLife;
                hpBar.SetMaxValue(StateVariableController.hannahMaxLife, playerLife);
                maxLife = StateVariableController.hannahMaxLife;
                animator.avatar = hannahAvatar;
                carterObj.SetActive(false);
                lukeObj.SetActive(false);
                hannahObj.SetActive(true);

                //Reload Setup ==================================================
                reloadDelay = 2;
                currentDeley = 2;
                //Stamina Setup ==================================================
                maxStaminaThisRound = StateVariableController.player3MaxStamina;
                staminaBar.SetMaxValue(maxStamina, maxStaminaThisRound);
            }

            glock = GameObject.Find("Glock").gameObject;
            m16 = GameObject.Find("m16").gameObject;
            shootgun = GameObject.Find("Pump Shotgun").gameObject;
            knife = GameObject.Find("Knife").gameObject;
            baseballBat = GameObject.Find("Baseball Bat").gameObject;
            ironBar = GameObject.Find("IronBar").gameObject;

            glock.SetActive(false);
            m16.SetActive(false);
            shootgun.SetActive(false);

            knife.SetActive(false);
            baseballBat.SetActive(false);
            ironBar.SetActive(false);
        }

        #endregion
    }

    [Obsolete]
    private void Update()
    {

        #region player1 Hungry And HP Update State

        //Player1
        #region HungryState
        if (StateVariableController.player1HungryScal > 8)
        {
            currentHungryState1 = "Be Full";
            StateVariableController.current1Hungry = currentHungryState1;
        }
        else if (StateVariableController.player1HungryScal > 6 && StateVariableController.player1HungryScal < 9)
        {
            currentHungryState1 = "Stomach growling";
            StateVariableController.current1Hungry = currentHungryState1;
        }
        else if (StateVariableController.player1HungryScal > 4 && StateVariableController.player1HungryScal < 7)
        {
            currentHungryState1 = "Hungry";
            StateVariableController.current1Hungry = currentHungryState1;
        }
        else if (StateVariableController.player1HungryScal > 2 && StateVariableController.player1HungryScal < 5)
        {
            currentHungryState1 = "Very Hungry";
            StateVariableController.current1Hungry = currentHungryState1;
        }
        else if (StateVariableController.player1HungryScal < 3 && StateVariableController.player1HungryScal > 0)
        {
            currentHungryState1 = "Starving";
            StateVariableController.current1Hungry = currentHungryState1;
        }
        else if (StateVariableController.player1HungryScal <= 0)
        {
            Debug.Log("2 turn Die");
            StateVariableController.current1Hungry = "Two ture Die";
        }
        #endregion
        #region HPState
        if (StateVariableController.player1CurrentLife > 79f)
        {
            currentHPState1 = "Healthy";
            StateVariableController.current1Injury = currentHPState1;
        }
        else if (StateVariableController.player1CurrentLife > 49f && StateVariableController.player1CurrentLife < 80f)
        {
            currentHPState1 = "Wouded";
            StateVariableController.current1Injury = currentHPState1;
        }
        else if (StateVariableController.player1CurrentLife > 20f && StateVariableController.player1CurrentLife < 50f)
        {
            currentHPState1 = "Heavy Wounded";
            StateVariableController.current1Injury = currentHPState1;
        }
        else if (StateVariableController.player1CurrentLife < 20f && StateVariableController.player1CurrentLife > 0f)
        {
            currentHPState1 = "Fatal Wounded";
            StateVariableController.current1Injury = currentHPState1;
        }
        else if (StateVariableController.player1CurrentLife <= 0f)
        {
            Debug.Log("Die");
            StateVariableController.current1Injury = "Die";
        }
        #endregion

        #endregion

        #region player2 Hungry And HP Update State

        //Player2
        #region HungryState
        if (StateVariableController.player2HungryScal > 8)
        {
            currentHungryState2 = "Be Full";
            StateVariableController.current2Hungry = currentHungryState2;
        }
        else if (StateVariableController.player2HungryScal > 6 && StateVariableController.player2HungryScal < 9)
        {
            currentHungryState2 = "Stomach growling";
            StateVariableController.current2Hungry = currentHungryState2;
        }
        else if (StateVariableController.player2HungryScal > 4 && StateVariableController.player2HungryScal < 7)
        {
            currentHungryState2 = "Hungry";
            StateVariableController.current2Hungry = currentHungryState2;
        }
        else if (StateVariableController.player2HungryScal > 2 && StateVariableController.player2HungryScal < 5)
        {
            currentHungryState2 = "Very Hungry";
            StateVariableController.current2Hungry = currentHungryState2;
        }
        else if (StateVariableController.player2HungryScal < 3 && StateVariableController.player2HungryScal > 0)
        {
            currentHungryState2 = "Starving";
            StateVariableController.current2Hungry = currentHungryState2;
        }
        else if (StateVariableController.player2HungryScal <= 0)
        {
            Debug.Log("2 turn Die");
            StateVariableController.current2Hungry = "Two ture Die";
        }
        #endregion
        #region HPState
        if (StateVariableController.player2CurrentLife > 79f)
        {
            currentHPState2 = "Healthy";
            StateVariableController.current2Injury = currentHPState2;
        }
        else if (StateVariableController.player2CurrentLife > 49f && StateVariableController.player2CurrentLife < 80f)
        {
            currentHPState2 = "Wouded";
            StateVariableController.current2Injury = currentHPState2;
        }
        else if (StateVariableController.player2CurrentLife > 20f && StateVariableController.player2CurrentLife < 50f)
        {
            currentHPState2 = "Heavy Wounded";
            StateVariableController.current2Injury = currentHPState2;
        }
        else if (StateVariableController.player2CurrentLife < 20f && StateVariableController.player2CurrentLife > 0f)
        {
            currentHPState2 = "Fatal Wounded";
            StateVariableController.current2Injury = currentHPState2;
        }
        else if (StateVariableController.player2CurrentLife <= 0f)
        {
            Debug.Log("Die");
            StateVariableController.current2Injury = "Die";
        }
        #endregion

        #endregion

        #region player3 Hungry And HP Update State

        //Player3
        #region HungryState
        if (StateVariableController.player3HungryScal > 8)
        {
            currentHungryState3 = "Be Full";
            StateVariableController.current3Hungry = currentHungryState3;
        }
        else if (StateVariableController.player3HungryScal > 6 && StateVariableController.player3HungryScal < 9)
        {
            currentHungryState3 = "Stomach growling";
            StateVariableController.current3Hungry = currentHungryState3;
        }
        else if (StateVariableController.player3HungryScal > 4 && StateVariableController.player3HungryScal < 7)
        {
            currentHungryState3 = "Hungry";
            StateVariableController.current3Hungry = currentHungryState3;
        }
        else if (StateVariableController.player3HungryScal > 2 && StateVariableController.player3HungryScal < 5)
        {
            currentHungryState3 = "Very Hungry";
            StateVariableController.current3Hungry = currentHungryState3;
        }
        else if (StateVariableController.player3HungryScal < 3 && StateVariableController.player3HungryScal > 0)
        {
            currentHungryState3 = "Starving";
            StateVariableController.current3Hungry = currentHungryState3;
        }
        else if (StateVariableController.player3HungryScal <= 0)
        {
            Debug.Log("2 turn Die");
            StateVariableController.current3Hungry = "Two ture Die";
        }
        #endregion
        #region HPState
        if (StateVariableController.player3CurrentLife > 79f)
        {
            currentHPState3 = "Healthy";
            StateVariableController.current3Injury = currentHPState3;
        }
        else if (StateVariableController.player3CurrentLife > 49f && StateVariableController.player3CurrentLife < 80f)
        {
            currentHPState3 = "Wouded";
            StateVariableController.current3Injury = currentHPState3;
        }
        else if (StateVariableController.player3CurrentLife > 20f && StateVariableController.player3CurrentLife < 50f)
        {
            currentHPState3 = "Heavy Wounded";
            StateVariableController.current3Injury = currentHPState3;
        }
        else if (StateVariableController.player3CurrentLife < 20f && StateVariableController.player3CurrentLife > 0f)
        {
            currentHPState3 = "Fatal Wounded";
            StateVariableController.current3Injury = currentHPState3;
        }
        else if (StateVariableController.player3CurrentLife <= 0f)
        {
            Debug.Log("Die");
            StateVariableController.current3Injury = "Die";
        }
        #endregion

        #endregion

        if (SceneManager.GetActiveScene().name == "LootMap")
        {
            if (_playerInventory.equipment.Container.Count <= 0)
            {
                isAimMode = false;
            }

            if (canMove)
            {
                #region ShootingModeInLootMap
                if (isAimMode)
                {
                    Ray raycast = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(raycast, out RaycastHit hitTarget))
                    {
                        Vector3 hypotenuseLine = (raycast.origin - hitTarget.point).normalized;

                        float angle = Vector3.Angle(hypotenuseLine, new Vector3(hypotenuseLine.x, 0, hypotenuseLine.z));
                        //float opposite = bulletSpawnPoint.position.y - hitTarget.point.y;
                        float opposite = transform.position.y - hitTarget.point.y;

                        float hypotenuseLength = opposite / Mathf.Sin(angle * Mathf.Deg2Rad);

                        hitPosition = hitTarget.point + (hypotenuseLine * hypotenuseLength);

                        spawnDirectionBulletPoint = new Vector3(hitPosition.x, _yOffset, hitPosition.z);

                        hitDirection = spawnDirectionBulletPoint - bulletSpawnPoint.position;

                        Debug.DrawRay(transform.position, hitDirection.normalized * 10f, Color.black);
                    }

                    animator.SetLayerWeight(1, 1);
                    moveSpeed = walkSpeed;
                    isRun = false;
                    knife.SetActive(false);
                    ironBar.SetActive(false);
                    baseballBat.SetActive(false);
                    _combo.canAtk = false;
                    RaycastHit hitInfo;
                    Ray ray = Camera.main.ScreenPointToRay(mouseLook);

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        rotationTarget = hitInfo.point;
                    }

                    movePlayerWithAim();

                    if (gunGlock && !gunM16 && !gunShootGun)
                    {
                        stackAmmo = 12;
                        glock.SetActive(true);
                        m16.SetActive(false);
                        shootgun.SetActive(false);
                        animator.SetBool("AimRifle", false);
                        animator.SetBool("AimPistol", true);
                        for (int i = 0; i < _playerInventory.inventory.Container.Count; i++)
                        {
                            if (_playerInventory.inventory.Container[i].item.itemID == 2)
                            {
                                ammoCount = _playerInventory.inventory.Container[i].amout;
                                break;
                            }
                            else ammoCount = 0;
                        }
                        if (_playerInventory.inventory.Container.Count <= 0)
                        {
                            ammoCount = 0;
                        }
                    }
                    else if (!gunGlock && gunM16 && !gunShootGun)
                    {
                        stackAmmo = 15;
                        glock.SetActive(false);
                        m16.SetActive(true);
                        shootgun.SetActive(false);
                        animator.SetBool("AimRifle", true);
                        animator.SetBool("AimPistol", false);
                        for (int i = 0; i < _playerInventory.inventory.Container.Count; i++)
                        {
                            if (_playerInventory.inventory.Container[i].item.itemID == 1)
                            {
                                ammoCount = _playerInventory.inventory.Container[i].amout;
                                break;
                            }
                            else ammoCount = 0;
                        }
                        if (_playerInventory.inventory.Container.Count <= 0)
                        {
                            ammoCount = 0;
                        }

                    }
                    else if (!gunGlock && !gunM16 && gunShootGun)
                    {
                        stackAmmo = 8;
                        glock.SetActive(false);
                        m16.SetActive(false);
                        shootgun.SetActive(true);
                        animator.SetBool("AimRifle", true);
                        animator.SetBool("AimPistol", false);
                        for (int i = 0; i < _playerInventory.inventory.Container.Count; i++)
                        {
                            if (_playerInventory.inventory.Container[i].item.itemID == 9)
                            {
                                ammoCount = _playerInventory.inventory.Container[i].amout;
                                break;
                            }
                            else ammoCount = 0;
                        }
                        if (_playerInventory.inventory.Container.Count <= 0)
                        {
                            ammoCount = 0;
                        }

                    }
                    else if (!gunGlock && !gunM16 && !gunShootGun)
                    {
                        isAimMode = !isAimMode;
                        ammoCount = 0;
                        if (_playerInventory.inventory.Container.Count <= 0)
                        {
                            ammoCount = 0;
                        }
                        return;
                    }

                    Shoot();
                    crosshair.SetActive(true);
                    if (gunGlock)
                    {
                        if (currentGlockAmmoInStake <= 0 && ammoCount > 0)
                        {
                            isReload = true;
                        }
                        if (Input.GetKeyDown(KeyCode.R) && currentGlockAmmoInStake < ammoCount && ammoCount > 0)
                        {
                            isReload = true;
                        }
                    }

                    if (gunM16)
                    {
                        if (currentRifleAmmoInStake <= 0 && ammoCount > 0)
                        {
                            isReload = true;
                        }
                        if (Input.GetKeyDown(KeyCode.R) && currentRifleAmmoInStake < ammoCount && ammoCount > 0)
                        {
                            isReload = true;
                        }
                    }

                    if (gunShootGun)
                    {
                        if (currentShotgunAmmoInStake <= 0 && ammoCount > 0)
                        {
                            isReload = true;
                        }
                        if (Input.GetKeyDown(KeyCode.R) && currentShotgunAmmoInStake < ammoCount && ammoCount > 0)
                        {
                            isReload = true;
                        }
                    }

                    if (isReload)
                    {
                        if (gunGlock || gunM16)
                        {
                            if (gunGlock)
                            {
                                animator.SetBool("glockReload", true);
                            }
                            else if (gunM16)
                            {
                                animator.SetBool("rifleReload", true);
                            }
                            currentDeley -= 1 * Time.deltaTime;
                            if (currentDeley <= 0)
                            {
                                if (gunGlock)
                                {
                                    if (stackAmmo <= ammoCount)
                                    {
                                        currentGlockAmmoInStake = stackAmmo;
                                        animator.SetBool("rifleReload", false);
                                        animator.SetBool("glockReload", false);
                                        isReload = false;
                                    }
                                    else if (stackAmmo > ammoCount)
                                    {
                                        currentGlockAmmoInStake = ammoCount;
                                        animator.SetBool("rifleReload", false);
                                        animator.SetBool("glockReload", false);
                                        isReload = false;
                                    }
                                    currentDeley = reloadDelay;
                                }
                                else if (gunM16)
                                {
                                    if (stackAmmo <= ammoCount)
                                    {
                                        currentRifleAmmoInStake = stackAmmo;
                                        animator.SetBool("rifleReload", false);
                                        animator.SetBool("glockReload", false);
                                        isReload = false;
                                    }
                                    else if (stackAmmo > ammoCount)
                                    {
                                        currentRifleAmmoInStake = ammoCount;
                                        animator.SetBool("rifleReload", false);
                                        animator.SetBool("glockReload", false);
                                        isReload = false;
                                    }
                                    currentDeley = reloadDelay;
                                }
                            }
                        }
                        else if (gunShootGun)
                        {
                            animator.SetBool("shotgunReload", false);
                            currentShotgunDelay -= 1 * Time.deltaTime;
                            if (currentShotgunDelay <= 0)
                            {
                                animator.SetBool("shotgunReload", true);
                                currentShotgunAmmoInStake++;
                                currentShotgunDelay = shotgunReloadDelay;
                                if (currentShotgunAmmoInStake == stackAmmo)
                                {
                                    animator.SetBool("shotgunReload", false);
                                    isReload = false;
                                }
                            }
                        }
                    }
                }
                else if (!isAim && !isAimMode)
                {
                    glock.SetActive(false);
                    m16.SetActive(false);
                    shootgun.SetActive(false);
                    crosshair.SetActive(false);
                    movePlayer();
                    animator.SetLayerWeight(1, 0);
                    animator.SetBool("AimRifle", false);
                    animator.SetBool("AimPistol", false);
                    animator.SetBool("rifleReload", false);
                    animator.SetBool("glockReload", false);
                    animator.SetBool("shotgunReload", false);
                }
                #endregion
            }
            if (Input.GetMouseButtonDown(1) && !isAim && _playerInventory.equipment.Container.Count > 0)
            {
                isAim = true;
                isAimMode = true;
            }
            else if (Input.GetMouseButtonDown(1) && isAim)
            {
                isAim = false;
                isAimMode = false;
            }
            hpBar.SetValue(playerLife);
            staminaBar.SetValue(currentStamina);

            if (currentStamina < 10)
            {
                moveSpeed = walkSpeed;
                _combo.canAtk = false;
            }

            if (isknife && !isAimMode && !_playerInventory.inventoryUI.activeSelf)
            {
                knife.SetActive(true);
                ironBar.SetActive(false);
                baseballBat.SetActive(false);
                _combo.canAtk = true;
            }
            else if (isBaseballBat && !isAimMode && !_playerInventory.inventoryUI.activeSelf)
            {
                knife.SetActive(false);
                ironBar.SetActive(false);
                baseballBat.SetActive(true);
                _combo.canAtk = true;
            }
            else if (isIronBar && !isAimMode && !_playerInventory.inventoryUI.activeSelf)
            {
                knife.SetActive(false);
                ironBar.SetActive(true);
                baseballBat.SetActive(false);
                _combo.canAtk = true;
            }
            else if (!isknife && !isIronBar && !isBaseballBat)
            {
                knife.SetActive(false);
                ironBar.SetActive(false);
                baseballBat.SetActive(false);
                _combo.canAtk = false;
            }
        }

        if (SceneManager.GetActiveScene().name == "Zanctuary")
        {
            #region ShootingModeInZanctuary
            glock.SetActive(false);
            m16.SetActive(false);
            shootgun.SetActive(false);
            knife.SetActive(false);
            animator.SetBool("Aim", false);
            //crosshair.SetActive(false);
            #endregion
        }


        #region Crouch

        if (Input.GetKeyDown(KeyCode.C) && !isCrouch && !isRun)
        {
            animator.SetBool("Crouch", true);
            animator.SetBool("Run", false);
            isCrouch = true;
            capcollider.height /= 2f;
            capcollider.center /= 2f;
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            moveSpeed = crouchSpeed;
            //crosshair.transform.position -= new Vector3(0f, 0.5f, 0f);
            bulletSpawnPoint.transform.position -= new Vector3(0f, 0.5f, 0f);

        }

        if (Input.GetKeyUp(KeyCode.C) && isCrouch)
        {
            animator.SetBool("Crouch", false);
            animator.SetBool("Run", false);
            isCrouch = false;
            capcollider.height *= 2f;
            capcollider.center *= 2f;
            moveSpeed = walkSpeed;
            //crosshair.transform.position += new Vector3(0f, 0.5f, 0f);
            bulletSpawnPoint.transform.position += new Vector3(0f, 0.5f, 0f);

        }

        #endregion

        #region Run
        if (currentStamina >= 5)
        {
            if (!isCrouch && Input.GetKey(KeyCode.LeftShift) && isAimMode == false)
            {
                isRun = true;
                isWalk = false;
                moveSpeed = runSpeed;
                animator.SetBool("Run", true);

            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRun = false;
                moveSpeed = walkSpeed;
                animator.SetBool("Run", false);
            }
        }
        else
        {
            isRun = false;
            animator.SetBool("Run", false);
            moveSpeed = walkSpeed;
        }
        #endregion

        #region Interacable
        var thisInteracPoint = transform.position + new Vector3(0, 0.1f, 0);
        Debug.DrawRay(thisInteracPoint, gameObject.transform.TransformDirection(Vector3.forward).normalized * interacRage, Color.blue);
        Ray rayCast = new Ray(thisInteracPoint, gameObject.transform.TransformDirection(Vector3.forward).normalized);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(rayCast, out RaycastHit hit, interacRage))
            {
                if (hit.collider.gameObject.TryGetComponent(out Iinteractable iInteractableObject))
                {
                    iInteractableObject.Interact();
                }
                isChecked = false;
            }
            else
            {
                if (!isChecked)
                {
                    isChecked = true;
                }
            }
        }
        if (Physics.Raycast(rayCast, out RaycastHit hitInfoForInterface, interacRage))
        {
            if (hitInfoForInterface.collider.gameObject.TryGetComponent(out Iinteractable iInteractableObject))
            {
                interacUI.SetActive(true);
            }
            
        }
        else
        {
            interacUI.SetActive(false);
        }

        #endregion

        #region Nois

        if (isIdle)
        {
            baseNoisLeght = 1f;
            //ripple.startSize = 3f;
        }

        if (isCrouch && isIdle)
        {
            baseNoisLeght = .5f;
            //ripple.startSize = 3f;
        }

        if (isRun && !isCrouch)
        {
            baseNoisLeght = 5f;
            //ripple.startSize = 14f;
        }

        if (isWalk && !isCrouch)
        {
            baseNoisLeght = 3f;
            //ripple.startSize = 8f;
        }
        else if (isWalk && isCrouch)
        {
            baseNoisLeght = 2f;
            //ripple.startSize = 5f;
        }

        if (isShoot) baseNoisLeght = 7f;

        if (StateVariableController.player3Select == true)
        {
            var hannahNois = baseNoisLeght * StateVariableController.hannahMultipleNoiseArea;
            baseNoisLeght -= hannahNois;
        }
        #endregion

        #region enemy entry check

        Collider[] collider = Physics.OverlapSphere(transform.position, baseNoisLeght, enemyLayerMask);
        foreach (Collider e in collider)
        {
            if (e.GetComponent<Enemy>())
            {
                var enemy = e.GetComponent<Enemy>();
                if (enemy.sawPlayer == false)
                {
                    enemy.sawPlayer = true;
                    enemy.EnterState(Enemy.State.Entry);
                }
            }
        }

        #endregion

        #region die
        if (playerLife <= 0)
        {
            isDead = true;
            Die();
        }
        #endregion

    }
    
    #region MovePlayer
    public void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        Vector3 moveDirec = movement.normalized;

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 300 * Time.deltaTime);

            //var rotatedVector = transform.rotation * Vector3.forward;
            //float targetAgle = Mathf.Atan2(moveDirec.x, moveDirec.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAgle, ref turnSmoothVelocity, turnSmoothTime);
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Vector3 moveDir = Quaternion.Euler(0f, targetAgle, 0f) * Vector3.forward;

            //transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        }
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        if (movement.magnitude > .1f && !isRun)
        {
            move.Normalize();
            isWalk = true;
            isIdle = false;
            staminaRechargeRate = 1;
            ReStamina();

            chageFootStepDuration = 0.6f;
            audioSource.volume = 0.1f;
            currentChageFootStepTime -= Time.deltaTime;
            if (currentChageFootStepTime <= 0)
            {
                randomInt = Random.Range(0, footStep_List.Count);
                audioSource.PlayOneShot(footStep_List[randomInt]);
                currentChageFootStepTime = chageFootStepDuration;
            }
        }
        else if (movement.magnitude == 0)
        {
            isIdle = true;
            isWalk = false;
            isRun = false;
            staminaRechargeRate = 2;
            ReStamina();
        }
        else if (movement.magnitude > .1f && isRun)
        {
            move.Normalize();
            isWalk = false;
            isIdle = false;
            UseStaminaToRun();

            chageFootStepDuration = 0.3f;
            audioSource.volume = 0.2f;
            currentChageFootStepTime -= Time.deltaTime;
            if (currentChageFootStepTime <= 0)
            {
                randomInt = Random.Range(0, footStep_List.Count);
                audioSource.PlayOneShot(footStep_List[randomInt]);
                currentChageFootStepTime = chageFootStepDuration;
            }
        }

        anim_ForwardAmout = movement.normalized.z;
        anim_TurnAmout = Mathf.Atan2(movement.normalized.x, movement.normalized.z);

        animator.SetFloat("Forward", anim_ForwardAmout, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", anim_TurnAmout, 0.1f, Time.deltaTime);
    }
    #endregion

    #region movePlayerWithAim
    public void movePlayerWithAim()
    {
        var lookPos = rotationTarget - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);

        Vector3 aimDiraction = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

        if (aimDiraction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.5f);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 300 * Time.deltaTime);
            
        }
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if (movement.magnitude > .1f && !isRun)
        {
            move.Normalize();
            isWalk = true;
            isIdle = false;

            chageFootStepDuration = 0.6f;
            audioSource.volume = 0.1f;
            currentChageFootStepTime -= Time.deltaTime;
            if (currentChageFootStepTime <= 0)
            {
                RandomSound(randomInt, footStep_List);
                currentChageFootStepTime = chageFootStepDuration;
            }
        }
        else if (movement.magnitude == 0)
        {
            isIdle = true;
            isWalk = false;
            isRun = false;
            staminaRechargeRate = 2;
            ReStamina();
        }

        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        animator.SetFloat("Forward", movement.normalized.z, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", movement.normalized.x, 0.1f, Time.deltaTime);
    }
    #endregion

    #region ShootFunction
    private void Shoot()
    {
        if (gunM16)
        {
            if (Input.GetMouseButton(0) && currentRifleAmmoInStake > 0)
            {
                currentFireRateTime -= 1f * Time.deltaTime;
                if (currentFireRateTime <= 0)
                {
                    isReload = false;
                    currentRifleAmmoInStake--;
                    for (int i = 0; i < _playerInventory.inventory.Container.Count; i++)
                    {
                        bool CanM16Shoot = false;
                        var AmmoM16 = _playerInventory.inventory.Container[i].item.itemID == 1;

                        if (AmmoM16 && currentRifleAmmoInStake >= 0)
                        {
                            CanM16Shoot = true;
                        }

                        if (CanM16Shoot)
                        {
                            isShoot = true;
                            animator.SetBool("fireRifle", true);
                            audioSource.PlayOneShot(M16Sound);
                            _playerInventory.equipment.Container[0].durability -= 0.2f;

                            Instantiate(muzzleRifle, muzzleRifleTranform.position, Quaternion.identity);
                            Instantiate(bulletCap, m16.transform.position, Quaternion.identity);

                            GameObject spawnedProjectile = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                            spawnedProjectile.transform.forward = hitDirection.normalized;
                            spawnedProjectile.GetComponent<Rigidbody>().AddForce(hitDirection.normalized * bulletSpeed, ForceMode.Impulse);

                            _playerInventory.inventory.RemoveItem(_playerInventory.inventory.Container[i].item, 1);
                            currentFireRateTime = fireRate;
                        }
                    }
                }
            }
            else
            {
                animator.SetBool("fireRifle", false);
            }
        }

        if (gunGlock)
        {
            if (Input.GetMouseButtonDown(0) && currentGlockAmmoInStake > 0)
            {
                isReload = false;
                currentGlockAmmoInStake--;
                for (int i = 0; i < _playerInventory.inventory.Container.Count; i++)
                {
                    bool CanGlockShoot = false;

                    var AmmoGlock = _playerInventory.inventory.Container[i].item.itemID == 2;

                    if (AmmoGlock && currentGlockAmmoInStake >= 0)
                    {
                        CanGlockShoot = true;
                    }

                    if (CanGlockShoot && animator.GetBool("fireGlock") == false)
                    {
                        audioSource.PlayOneShot(GlockSound);
                        isShoot = true;
                        animator.SetBool("fireGlock", true);
                        _playerInventory.equipment.Container[0].durability -= 0.2f;
                        Ray raycast = cam.ScreenPointToRay(Input.mousePosition);

                        Instantiate(muzzlePistol, muzzlePistolTranform.position, Quaternion.identity);
                        Instantiate(glockCap, glock.transform.position, Quaternion.identity);

                        GameObject spawnedProjectile = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                        
                        spawnedProjectile.transform.forward = hitDirection.normalized;
                        spawnedProjectile.GetComponent<Rigidbody>().AddForce(hitDirection.normalized * bulletSpeed, ForceMode.Impulse);

                        _playerInventory.inventory.RemoveItem(_playerInventory.inventory.Container[i].item, 1);
                    }

                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isShoot = false;
                animator.SetBool("fireGlock", false);
            }
        }
        if (gunShootGun)
        {
            if (Input.GetMouseButtonDown(0) && currentShotgunAmmoInStake > 0)
            {
                isReload = false;
                currentShotgunAmmoInStake--;
                for (int i = 0; i < _playerInventory.inventory.Container.Count; i++)
                {
                    bool CanShotGunShoot = false;

                    var AmmoShotGun = _playerInventory.inventory.Container[i].item.itemID == 9;

                    if (AmmoShotGun && currentShotgunAmmoInStake >= 0)
                    {
                        CanShotGunShoot = true;
                    }

                    if (CanShotGunShoot)
                    {
                        isShoot = true;
                        animator.SetBool("fireShotgun", true);
                        audioSource.PlayOneShot(ShotgunSound);
                        _playerInventory.equipment.Container[0].durability -= 0.2f;
                        Ray raycast = cam.ScreenPointToRay(Input.mousePosition);

                        Instantiate(muzzleShotgun, muzzleShotgunTranform.position, Quaternion.identity);
                        Instantiate(shotgunCap, shootgun.transform.position, Quaternion.identity);

                        GameObject spawnedProjectile = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                        spawnedProjectile.transform.forward = hitDirection.normalized;
                        spawnedProjectile.GetComponent<Rigidbody>().AddForce(hitDirection.normalized * bulletSpeed, ForceMode.Impulse);

                        _playerInventory.inventory.RemoveItem(_playerInventory.inventory.Container[i].item, 1);

                    }

                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                isShoot = false;
                animator.SetBool("fireShotgun", false);
            }
        }
    }
    #endregion

    #region DrawGizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, baseNoisLeght);
    }

    #endregion

    #region TakeDamage
    public void TakeDamage(float amount)
    {
        RandomSound(hitInt, playerHit);
        playerLife -= amount;
        animator.SetTrigger("TakeDamage");
        blood.Play();
    }
    #endregion

    public void UseStamina(float _value)
    {
        currentStamina -= _value;
        _currentStaminaDelayCounter = 0;
        if (currentStamina <= 0)
        {
            currentStamina = 0;
        }
    }

    public void UseStaminaToRun()
    {
        currentStamina -= staminaDepletion * Time.deltaTime;
        _currentStaminaDelayCounter = 0;
        if (currentStamina <= 0)
        {
            currentStamina = 0;
        }
    }

    public void ReStamina()
    {
        _currentStaminaDelayCounter += Time.deltaTime;
        if (_currentStaminaDelayCounter >= staminaRechargeDelay)
        {
            currentStamina += staminaRechargeRate * Time.deltaTime;
            _currentStaminaDelayCounter = staminaRechargeDelay;
        }
        if (currentStamina >= maxStaminaThisRound)
        {
            currentStamina = maxStaminaThisRound;
        }

    }
    public void Die()
    {
        if (StateVariableController.player1Select)
        {
            StateVariableController.carterIsDead = true;
        }
        else if (StateVariableController.player2Select)
        {
            StateVariableController.lukeIsDead = true;
        }
        else if (StateVariableController.player3Select)
        {
            StateVariableController.hannahIsDead = true;
        }
        DiePanel.SetActive(true);
        _playerInventory.inventory.Container.Clear();
        _playerInventory.melee.Container.Clear();
        _playerInventory.equipment.Container.Clear();
        //ZInventory Add Key item;

    }

    public void startAnim()
    {
        canMove = false;
    }
    public void endAnim()
    {
        canMove = true;
    }

    public void startReload()
    {
        if (gunM16)
            audioSource.PlayOneShot(RifleReloadSound);
        if (gunGlock)
            audioSource.PlayOneShot(GlockReloadSound);
        if (gunShootGun)
            audioSource.PlayOneShot(ShotgunReloadSound);
    }

    public void RandomSound(int value, List<AudioClip> Sound)
    {
        value = Random.Range(0, Sound.Count);
        audioSource.PlayOneShot(Sound[value]);
    }

}
