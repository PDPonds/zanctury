using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    CharacterMovement _player;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<CharacterMovement>();
    }


    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(_player.spawnDirectionBulletPoint);
    }
}
