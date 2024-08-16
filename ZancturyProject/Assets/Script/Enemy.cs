using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : MonoBehaviour
{
    #region Valiable
    public enum State { Idle, Search, Entry, }
    [Header("================ State ================")]
    public State currentState = State.Idle;
    [Header("================ Damage & life ================")]
    ZombieData enemy;
    Animator animator;
    public bool isDead;
    [Header("================ UpdateState ================")]
    [SerializeField] NavMeshAgent navMeshAgent;
    GameObject player;
    public bool sawPlayer;
    [Header("================ OutofRange ================")]
    [SerializeField] float _currentOutOfSightDuration;

    [Header("================ AtkPlayer ================")]
    [SerializeField] int countAtk;
    [SerializeField] GameObject atkPoint;
    [SerializeField] GameObject jumpAtkPoint;
    public bool atkPlayer;
    public float enemyDamage;
    public int firshSaw;
    [SerializeField] GameObject jumpAtkTarget;
    [Header("================ Set SkinZombie ================")]
    [SerializeField] private List<GameObject> zombieMesh = new List<GameObject>();
    [SerializeField] private List<Avatar> zombieAvatar = new List<Avatar>();
    public bool isMutant;

    [SerializeField] GameObject lootPrefab;
    [SerializeField] int dropRate;
    [SerializeField] int dropAlready;

    [Header("=============== ParticleSystem ===========")]
    public ParticleSystem blood;
    public GameObject groundCrack;
    [Header("========== Audio ============")]
    public AudioSource audioSource;
    int takeDamageInt;
    [SerializeField] List<AudioClip> TakeDamageSound = new List<AudioClip>();

    int zombieHurtInt;
    [SerializeField] List<AudioClip> HurtSound = new List<AudioClip>();
    [SerializeField] AudioClip mutantHurt;

    [SerializeField] AudioClip roar;

    public AudioClip mutantDeadSound;
    public int deadInt;
    public List<AudioClip> zombieDead = new List<AudioClip>();

    int zomAtkInt;
    [SerializeField] List<AudioClip> zomAtkSound;

    int mutantAtkInt;
    [SerializeField] List<AudioClip> mutantAtkSound;

    int zombieIdleInt;
    [SerializeField] List<AudioClip> zombieIdleSound = new List<AudioClip>();
    float IdleDuration = 5f;
    float currentIdleTime = 5f;

    int zombieWalkInt;
    float walkDuration = 0.3f;
    float currentWalkTime = 0.3f;
    [SerializeField] List<AudioClip> zombieWalkSound = new List<AudioClip>();

    #endregion

    #region FieldOfView

    public float radiues;
    [Range(0,360)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obsticleMask;

    #endregion

    private void Start()
    {
        #region GetComponent
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        animator = gameObject.GetComponent<Animator>();
        isDead = false;
        dropRate = Random.Range(0, 10);
        if (isMutant == false)
        {
            IdleDuration = Random.Range(5, 8);
            currentIdleTime = IdleDuration;
        }
        #endregion

        #region SetupZombie
        enemy = new ZombieData();
        enemy.ZombieDatas();
        #endregion

        #region Setup Mutant or Standard Zombie
        if (isMutant == false)
        {
            var whatMeshZombie = Random.Range(0, zombieMesh.Count);
            for (int i = 0; i < zombieMesh.Count; i++)
            {
                zombieMesh[i].SetActive(false);
                if (zombieMesh[i] == zombieMesh[whatMeshZombie])
                {
                    zombieMesh[i].SetActive(true);
                    animator.avatar = zombieAvatar[i];
                }
            }
            enemyDamage = enemy.enemyDamage;
        }
        else if (isMutant == true)
        {
            enemy.enemyLife = 100f;
            enemyDamage = 20f;
            enemy.atkRange = 3f;
            enemy.sawRange = 20f;
            enemy.alertArea = 20f;
        }
        #endregion

        #region isMutant
        if (!isMutant)
            Debug.Log("enemy Damage : " + enemy.enemyDamage + " enemey Life" + enemy.enemyLife);
        else
            Debug.Log("enemy Damage : " + enemyDamage + " enemey Life" + enemy.enemyLife);
        #endregion

        #region FieldOfView
        StartCoroutine(FOVRoutine());
        #endregion

    }
    private void Update()
    {
        #region IsDead
        if (!isDead)
        {
            UpdateState();
        }

        if (enemy.enemyLife <= 0)
        {
            Die();
            navMeshAgent.SetDestination(gameObject.transform.position);
        }
        #endregion

        #region atkPoint
        if (!atkPoint.activeSelf && atkPoint != null)
        {
            var hitCount = atkPoint.GetComponent<AtkPoint>();
            hitCount.hit = 0;
        }

        if (isMutant)
        {
            if (!jumpAtkPoint.activeSelf && jumpAtkPoint != null)
            {
                var hitCount = jumpAtkPoint.GetComponent<AtkPoint>();
                hitCount.hit = 0;
            }
        }
        #endregion


    }

    #region FieldOfView

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FIeldOfViewCheck();
        }
    }

    private void FIeldOfViewCheck()
    {
        radiues = enemy.sawRange;

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radiues, targetMask);

        if (rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 direction = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direction) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, direction, distanceToTarget, obsticleMask))
                {
                    if(sawPlayer == false)
                    {
                        _currentOutOfSightDuration = 0;
                        sawPlayer = true;
                        EnterState(State.Entry);
                    }
                }
                else
                {
                    _currentOutOfSightDuration += Time.deltaTime;
                    if (_currentOutOfSightDuration > enemy.OutOfSightDuration)
                    {
                        sawPlayer = false;
                        //navMeshAgent.SetDestination(gameObject.transform.position);
                        EnterState(State.Search);
                    }
                }

            }
            else
            {
                _currentOutOfSightDuration += Time.deltaTime;
                if (_currentOutOfSightDuration > enemy.OutOfSightDuration)
                {
                    sawPlayer = false;
                    //navMeshAgent.SetDestination(gameObject.transform.position);
                    EnterState(State.Search);
                }
            }

        }

    }

    #endregion

    #region EnterState
    public void EnterState(State stateToChage)
    {
        ExitState();
        currentState = stateToChage;
        switch (currentState)
        {
            case State.Idle:

                break;
            case State.Search:

                break;
            case State.Entry:
                animator.SetBool("Roaring", true);
                break;
        }
    }
    #endregion

    #region UpdateState
    void UpdateState()
    {
        Vector3 eyePosition = transform.position + new Vector3(0, 1.25f, 0);
        Vector3 direction = (player.transform.position + new Vector3(0, 1.25f, 0)) - eyePosition;
        RaycastHit hit;
        Debug.DrawRay(eyePosition, direction.normalized * enemy.sawRange, Color.red);
        Debug.DrawRay(eyePosition, direction.normalized * enemy.atkRange, Color.green);

        #region switchState
        switch (currentState)
        {
            case State.Idle:
                EnterState(State.Search);
                break;
            case State.Search:
                navMeshAgent.SetDestination(gameObject.transform.position);
                animator.SetBool("Roaring", false);
                if (isMutant == false)
                {
                    currentIdleTime -= Time.deltaTime;
                    if (currentIdleTime <= 0)
                    {
                        RandomSound(zombieIdleInt, zombieIdleSound);
                        currentIdleTime = IdleDuration;
                    }
                }
                break;
            case State.Entry:
                Alert();
                if (Physics.Raycast(eyePosition, direction, out hit, enemy.sawRange))
                {
                    if (hit.collider.tag == "Player")
                    {
                        _currentOutOfSightDuration = 0;
                        navMeshAgent.SetDestination(gameObject.transform.position);
                        var lookPos = player.transform.position - transform.position;
                        lookPos.y = 0;
                        var rotation = Quaternion.LookRotation(lookPos);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f * Time.deltaTime);

                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Roaring")
                            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .6f)
                        {
                            animator.SetBool("Roaring", false);
                            navMeshAgent.SetDestination(player.transform.position);
                        }
                        
                        sawPlayer = true;
                    }
                    else
                    {
                        _currentOutOfSightDuration += Time.deltaTime;
                        if (_currentOutOfSightDuration > enemy.OutOfSightDuration)
                        {
                            sawPlayer = false;
                            navMeshAgent.SetDestination(gameObject.transform.position);
                            EnterState(State.Search);
                        }
                    }
                }

                if (sawPlayer)
                {
                    if (isMutant == false)
                    {
                        IdleDuration = 5f;
                        currentIdleTime -= Time.deltaTime;
                        if (currentIdleTime <= 0)
                        {
                            RandomSound(zombieIdleInt, zombieIdleSound);
                            currentIdleTime = IdleDuration;
                        }
                    }

                    if (Physics.Raycast(eyePosition, direction, out RaycastHit hitinfo, enemy.atkRange))
                    {
                        if (hitinfo.collider.tag == "Player")
                        {
                            atkPlayer = true;
                        }
                    }

                    if (atkPlayer)
                    {
                        animator.SetBool("walkAble", false);
                        animator.SetBool("atkPlayer", true);
                        if (isMutant)
                        {
                            if (countAtk == 0 || countAtk % 4 == 0)
                            {
                                animator.SetBool("jumpAtk", true);
                                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Roaring"))
                                {
                                    navMeshAgent.SetDestination(jumpAtkTarget.transform.position);
                                }
                                else
                                {
                                    navMeshAgent.SetDestination(gameObject.transform.position);
                                }
                            }
                            else
                            {
                                navMeshAgent.SetDestination(gameObject.transform.position);
                                animator.SetBool("jumpAtk", false);
                            }
                        }
                        else
                        {
                            currentIdleTime = IdleDuration;
                            navMeshAgent.SetDestination(gameObject.transform.position);
                        }

                    }
                    else if (!atkPlayer && animator.GetBool("Roaring") == false)
                    {
                        animator.SetBool("walkAble", true);
                        if (isMutant == false)
                        {
                            currentWalkTime -= Time.deltaTime;
                            if (currentWalkTime <= 0)
                            {
                                RandomSound(zombieWalkInt, zombieWalkSound);
                                currentWalkTime = walkDuration;
                            }
                        }
                        navMeshAgent.SetDestination(player.transform.position);
                        animator.SetBool("atkPlayer", false);
                    }
                }
                else
                {
                    _currentOutOfSightDuration = 0;
                    animator.SetBool("atkPlayer", false);
                    animator.SetBool("walkAble", false);
                    if (isMutant) animator.SetBool("Roaring", false);
                    EnterState(State.Search);
                }
                break;

        }
        #endregion

    }
    #endregion

    #region ExitState
    void ExitState()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Search:
                break;
            case State.Entry:
                break;
        }
    }
    #endregion

    #region AlertFuction
    void Alert()
    {
        Collider[] inSideCollider = Physics.OverlapSphere(transform.position, enemy.alertArea);
        foreach (Collider c in inSideCollider)
        {
            if (c.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                var anyEnemy = c.GetComponent<Enemy>();
                if (anyEnemy && anyEnemy.sawPlayer == false)
                {
                    anyEnemy.EnterState(State.Entry);
                }
            }
        }
    }
    #endregion

    #region DrawGizmosZombie
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemy.alertArea);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemy.sawRange);
    }
    #endregion

    #region ZombieTakeDamage
    public void TakeDamage(float amount)
    {
        if (!isMutant)
        {
            animator.SetBool("TakeDamage", true);
            animator.SetBool("Roaring", false);
            animator.SetBool("walkAble", false);
            animator.SetBool("atkPlayer", false);
        }
        if (currentState == State.Search || currentState == State.Idle)
        {
            amount *= 2f;
            if (StateVariableController.player3Select)
            {
                var hannahStealth = amount * StateVariableController.hannahStealthDamage;
                amount *= 2;
                amount += hannahStealth;
            }
            if (!sawPlayer)
            {
                EnterState(State.Entry);
            }
            navMeshAgent.SetDestination(player.transform.position);
        }
        RandomSound(takeDamageInt, TakeDamageSound);

        if (isMutant)
        {
            audioSource.PlayOneShot(mutantHurt);
        }
        else
        {
            RandomSound(zombieHurtInt, HurtSound);
        }

        blood.Play();
        enemy.enemyLife -= amount;
        if (!sawPlayer)
            EnterState(State.Entry);
    }
    #endregion

    #region Die
    public void Die()
    {
        isDead = true;
        if (dropRate < 5)
        {
            dropAlready += 1;
            if (dropAlready == 1)
            {
                GameObject obj = Instantiate(lootPrefab, transform.position, Quaternion.identity);
                var objLoot = obj.GetComponent<LootBox>();
                objLoot.currentCategury = LootBox.LootCategory.normalDropBox;
            }
            else if (dropAlready > 2)
            {
                dropAlready = 2;
            }
        }
    }
    #endregion

    #region AtkAnim
    public void startAtkAnim(string hit)
    {
        if (hit == "jump")
        {
            Instantiate(groundCrack.gameObject, jumpAtkTarget.transform.position, Quaternion.identity);
            jumpAtkPoint.SetActive(true);
            navMeshAgent.SetDestination(gameObject.transform.position);
        }
        else if (hit == "hit")
        {
            atkPoint.SetActive(true);
            if (isMutant)
            {
                RandomSound(mutantAtkInt, mutantAtkSound);
            }
            else
            {
                RandomSound(zomAtkInt, zomAtkSound);
            }
        }
        else if (hit == "TakeDamage")
        {
            navMeshAgent.SetDestination(gameObject.transform.position);
            atkPlayer = false;
        }
    }
    #endregion

    #region endAnim
    public void endAtkAnim(string hit)
    {
        if (hit == "jump")
        {
            animator.SetBool("jumpAtk", false);
            jumpAtkPoint.SetActive(false);
            countAtk++;
            atkPlayer = false;
        }
        else if (hit == "hit")
        {
            atkPoint.SetActive(false);
            countAtk++;
            atkPlayer = false;
        }
        else if (hit == "TakeDamage")
        {
            animator.SetBool("TakeDamage", false);
            animator.SetBool("atkPlayer", false);
            animator.SetBool("Roaring", false);
        }
    }
    #endregion

    #region PlaySound
    public void PlayRoarSound()
    {
        audioSource.PlayOneShot(roar);
    }

    public void RandomSound(int value, List<AudioClip> Sound)
    {
        value = Random.Range(0, Sound.Count);
        audioSource.PlayOneShot(Sound[value]);
    }
    #endregion
}
