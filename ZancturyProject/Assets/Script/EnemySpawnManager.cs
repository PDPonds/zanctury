using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject zombie;

    [SerializeField] int enemySpawnCount;
    [Header("===== tranform =====")]
    public float _xMin;
    public float _xMax;
    public float _zMin;
    public float _zMax;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemySpawnCount > 0)
            {
                for (int i = 0; i < enemySpawnCount; i++)
                {
                    float xPos = Random.Range(_xMin, _xMax);
                    float zPos = Random.Range(_zMin, _zMax);
                    GameObject enemy = Instantiate(zombie, new Vector3(xPos, 1f, zPos), zombie.transform.rotation);
                    Destroy(gameObject);
                }
            }

        }
    }
}
