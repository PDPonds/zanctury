using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float lifeTime = 3f;
    public float damage;

    GunData glock;
    GunData m16;
    GunData shootGun;

    GameObject playerObj;
    CharacterMovement player;
    private void Start()
    {
        glock = new GunData("Glock", 5f);
        m16 = new GunData("m16", 10f);
        shootGun = new GunData("shootgun", 20f);

        playerObj = GameObject.Find("Player").gameObject;
        player = playerObj.GetComponent<CharacterMovement>();
        if (player.gunGlock == true)
        {
            damage = glock.damageGun;
        }
        if (player.gunM16 == true) 
        {
            damage = m16.damageGun;
        }
        if (player.gunShootGun == true)
        {
            damage = shootGun.damageGun;
        }

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>())
        {
            Destroy(gameObject);
            Debug.Log("hit Enemy");

            var enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }

    }
}
