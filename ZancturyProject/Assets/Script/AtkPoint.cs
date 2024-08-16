using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkPoint : MonoBehaviour
{
    CharacterMovement player;
    Enemy enemy;
    MeleeSystem melee;
    public float hit;
    playerInventory playerInven;
    InventoryObj meleeInven;
    public float currentDamage;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        player = GameObject.Find("Player").GetComponent<CharacterMovement>();
        playerInven = GameObject.Find("Player").GetComponent<playerInventory>();
        melee = GameObject.Find("Player").GetComponentInChildren<MeleeSystem>();

        if (this.transform.parent.name == "enemy")
        {
            enemy = this.transform.GetComponentInParent<Enemy>();
        }
        if (this.transform.parent.name == "Player")
        {
            meleeInven = playerInven.melee;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            
            hit += 1;
            if (hit == 1)
            {
                player.TakeDamage(enemy.enemyDamage);
                this.gameObject.SetActive(false);
            }
            
        }

        if (other.CompareTag("enemy") && this.CompareTag("Player"))
        {
            
            hit += 1;
            if (hit == 1)
            {
                if (StateVariableController.player1Select == true)
                {
                    var carterDamage = melee.damage * StateVariableController.carterMultipleMeleeDamage;
                    currentDamage = melee.damage += carterDamage;
                }
                currentDamage = melee.damage;

                var enemy = other.GetComponent<Enemy>();
                enemy.TakeDamage(currentDamage);

                if (meleeInven.Container[0].item.itemID == 12)
                {
                    meleeInven.Container[0].durability -= 2f;
                    if (meleeInven.Container[0].durability <= 0)
                    {
                        meleeInven.Container.Clear();
                    }
                }
                else if (meleeInven.Container[0].item.itemID == 13)
                {
                    meleeInven.Container[0].durability -= 1f;
                    if (meleeInven.Container[0].durability <= 0)
                    {
                        meleeInven.Container.Clear();
                    }
                }
                else if (meleeInven.Container[0].item.itemID == 14)
                {
                    meleeInven.Container[0].durability -= 3f;
                    if (meleeInven.Container[0].durability <= 0)
                    {
                        meleeInven.Container.Clear();
                    }
                }
                this.gameObject.SetActive(false);
            }
        }
    }

}
