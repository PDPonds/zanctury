using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public struct GunData
{
    public string setName;
    public float damageGun;

    public GunData(string name,float amouth)
    {
        setName = name;
        damageGun = amouth;
    }
}
