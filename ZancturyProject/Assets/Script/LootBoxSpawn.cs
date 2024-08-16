using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxSpawn : MonoBehaviour
{
    [SerializeField] GameObject lootboxObj;
    [SerializeField] GameObject gunboxObj;
    [SerializeField] GameObject weaponboxObj;

    [SerializeField] GameObject Day1WeaponSpawn;


    [SerializeField] List<Transform> allSpawnLootBoxPoints = new List<Transform>();
    [SerializeField] List<Transform> allSpawnGunBoxPoints = new List<Transform>();
    [SerializeField] List<Transform> allSpanwWeaponBOxPoints = new List<Transform>();

    [SerializeField] int spawnLootBoxPercentage;
    [SerializeField] int spawnGunBoxPercentage;
    [SerializeField] int spawnWeaponBoxPercentage;

    [SerializeField] int spawnInt;
    private void Awake()
    {
        if(StateVariableController.currentDay <= 2)
        {
            Day1WeaponSpawn.SetActive(true);
        }
        else
        {
            Day1WeaponSpawn.SetActive(false);
        }

        if(allSpawnLootBoxPoints.Count > 0)
        {
            for (int i = 0; i < allSpawnLootBoxPoints.Count; i++)
            {
                spawnInt = Random.Range(1, 100);
                if (spawnInt <= spawnLootBoxPercentage)
                {
                    Instantiate(lootboxObj, allSpawnLootBoxPoints[i].position, lootboxObj.transform.rotation);
                }
            }
        }
        if (allSpawnGunBoxPoints.Count > 0)
        {
            for (int i = 0; i < allSpawnGunBoxPoints.Count; i++)
            {
                spawnInt = Random.Range(1, 100);
                if (spawnInt <= spawnGunBoxPercentage)
                {
                    Instantiate(gunboxObj, allSpawnGunBoxPoints[i].position, Quaternion.identity);
                }
            }
        }
        if (allSpanwWeaponBOxPoints.Count > 0)
        {
            for (int i = 0; i < allSpanwWeaponBOxPoints.Count; i++)
            {
                spawnInt = Random.Range(1, 100);
                if (spawnInt <= spawnWeaponBoxPercentage)
                {
                    Instantiate(weaponboxObj, allSpanwWeaponBOxPoints[i].position, weaponboxObj.transform.rotation);
                }
            }
        }
    }

}
