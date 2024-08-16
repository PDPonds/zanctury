using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    #region oldCombo
    [Header("Combo State")]
    public int currentComboState;
    public GameObject atkPoint;

    [Header("Next Combo Delay")]
    public float countdownTime;
    public float currentCountTime;
    public bool countDownAlready;

    [Header("Next Step")]
    public float countdownDelay;
    public float currentCountDelay;
    public bool delayAlready;

    [Header("Bool")]
    public bool canAtk;
    public bool isAtk;
    public bool canClick;
    public bool atkEnemy;

    Vector3 rotationTarget;

    CharacterMovement _player;
    Animator anim;
    [Header("========== Audio ==========")]
    public AudioSource audioSource;
    [SerializeField] AudioClip atkSound;

    void Start()
    {
        anim = GetComponent<Animator>();
        _player = GetComponent<CharacterMovement>();
        countDownAlready = true;
        canClick = true;
    }

    private void Update()
    {
        if (canAtk)
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit))
            {
                rotationTarget = hit.point;
                if (countDownAlready)
                {
                    if (canClick)
                    {
                        currentCountTime = countdownTime;
                        if (_player.currentStamina > 5f)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                currentCountDelay = countdownDelay;
                                currentComboState++;
                                delayAlready = true;
                                anim.SetFloat("comboCout", currentComboState);
                                _player.UseStamina(5);

                                var lookPos = rotationTarget - transform.position;
                                lookPos.y = 0;
                                var rotation = Quaternion.LookRotation(lookPos);

                                Vector3 aimDiraction = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

                                if (aimDiraction != Vector3.zero)
                                {
                                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 360);
                                }
                            }
                        }
                    }

                    if (currentComboState == 3)
                    {
                        countDownAlready = false;
                        delayAlready = false;
                        
                    }
                }
                else if (!countDownAlready)
                {
                    currentCountTime -= 1 * Time.deltaTime;
                    if (currentCountTime <= 0)
                    {
                        currentComboState = 0;
                        anim.SetFloat("comboCout", 0);
                        countDownAlready = true;
                        isAtk = false;
                        delayAlready = false;
                    }
                }

                if (delayAlready)
                {
                    currentCountDelay -= 1 * Time.deltaTime;
                    if (currentCountDelay <= .7f)
                    {
                        canClick = true;
                    }
                    else if (currentCountDelay > .7f)
                    {
                        canClick = false;
                    }
                    if (currentCountDelay <= 0)
                    {
                        anim.SetFloat("comboCout", 0);
                        countDownAlready = false;
                        delayAlready = false;
                        canClick = true;
                    }
                }
                else if (!delayAlready)
                {
                    currentCountDelay = countdownDelay;
                }

                if (anim.GetFloat("comboCout") == 0)
                {
                    isAtk = false;
                }
                else
                {
                    isAtk = true;
                }

                if (isAtk)
                {
                    _player.canMove = false;
                }
                else
                {
                    _player.canMove = true;
                }

            }

            if (!atkPoint.activeSelf && atkPoint != null)
            {
                var hitCount = atkPoint.GetComponent<AtkPoint>();
                hitCount.hit = 0;
            }
        }
        else
        {
            currentCountTime = 0;
            countDownAlready = true;
            canClick = true;
            currentCountDelay = 0;
            currentCountTime = 0;
            isAtk = false;
            anim.SetFloat("comboCout", 0);
            atkPoint.SetActive(false);
            _player.canMove = true;
        }
    }
    #endregion

    public void StartAtkEnemy()
    {
        audioSource.PlayOneShot(atkSound);
        atkPoint.SetActive(true);
    }
    public void EndAtkEnemy()
    {
        atkPoint.SetActive(false);
    }

    public void endAnim(float hit)
    {
        if (hit == 1 && anim.GetFloat("comboCout") <= 1)
            currentCountDelay = 0;
        if (hit == 2 && anim.GetFloat("comboCout") <= 2)
            currentCountDelay = 0;
        if (hit == 3 && anim.GetFloat("comboCout") == 3)
        {
            currentCountTime = 0;
            countDownAlready = false;
            delayAlready = true;
        }

    }


}