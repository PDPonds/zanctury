using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    void Update()
    {
        Destroy(this.gameObject, 3f);
        if(this.CompareTag("ParticleMuzzle"))
        {
            Destroy(this.gameObject, 0.3f);
        }
    }
}
