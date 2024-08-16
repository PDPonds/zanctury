using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEnterSceneEnable : MonoBehaviour
{
    float currentDuration = 2f;
    void Update()
    {
        currentDuration -= Time.deltaTime;
        if(currentDuration <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
