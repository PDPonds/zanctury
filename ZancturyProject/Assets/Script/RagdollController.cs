using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public GameObject mainObj;
    Animator _animator;
    Collider mainCol;
    Rigidbody mainRb;

    //Collider atkPoint;
    Collider[] ragdollCol;
    Rigidbody[] ragdollRb;

    Enemy stateEnemy;
    CharacterMovement _player;
    int currentDeadTime;
    void Start()
    {
        if(this.CompareTag("enemy"))
        {
            _animator = GetComponent<Animator>();
            mainCol = GetComponent<Collider>();
            mainRb = GetComponent<Rigidbody>();
        }
        if(this.CompareTag("Player"))
        {
            _animator = GameObject.Find("Player").GetComponent<Animator>();
            mainCol = GameObject.Find("Player").GetComponent<Collider>();
            mainRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        }

        ragdollCol = mainObj.GetComponentsInChildren<Collider>();
        ragdollRb = mainObj.GetComponentsInChildren<Rigidbody>();
        stateEnemy = GetComponent<Enemy>();
        _player = GameObject.Find("Player").GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if(this.CompareTag("enemy"))
        {
            if (stateEnemy.isDead == true)
            {
                RagdollOn();
            }
            else if (stateEnemy.isDead == false)
            {
                RagdollOff();
            }
        }
        if (this.CompareTag("Player"))
        {
            if (_player.isDead == false)
            {
                RagdollOff();
            }
            else if (_player.isDead == true)
            {
                RagdollOn();
                _player.enabled = false;
            }
        }
        
    }


    public void RagdollOn()
    {
        currentDeadTime++;
        _animator.enabled = false;
        foreach (Collider col in ragdollCol)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rb in ragdollRb)
        {
            rb.isKinematic = false;
        }
        mainCol.enabled = false;
        mainRb.isKinematic = true;
        if(this.CompareTag("enemy"))
        {
            if (currentDeadTime == 1)
            {
                if (stateEnemy.isMutant)
                {
                    stateEnemy.audioSource.PlayOneShot(stateEnemy.mutantDeadSound);
                }
                else
                {
                    EnemyRandomSound(stateEnemy.deadInt, stateEnemy.zombieDead);
                }
            }
        }
        
        if(currentDeadTime > 1)
        {
            currentDeadTime = 2;
        }
    }

    public void RagdollOff()
    {
        _animator.enabled = true;
        foreach(Collider col in ragdollCol)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rb in ragdollRb)
        {
            rb.isKinematic = true;
        }
        mainCol.enabled = true;
        mainRb.isKinematic = false;
    }

    public void EnemyRandomSound(int value, List<AudioClip> Sound)
    {
        value = Random.Range(0, Sound.Count);
        stateEnemy.audioSource.PlayOneShot(Sound[value]);
    }
    public void PlayerRandomSound(int value, List<AudioClip> Sound)
    {
        value = Random.Range(0, Sound.Count);
        _player.audioSource.PlayOneShot(Sound[value]);
    }
}
