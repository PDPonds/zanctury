using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���ҧ item �ͧ���� type ���кؤ����� type ������� Variable ���ú�ҧ ������͡ type �繢ͧ����ͧ
[CreateAssetMenu(fileName = "New Ammo Obj", menuName = "Inventory System/Items/Ammo")]
public class AmmoObj : ItemObj
{
    private void Awake()
    {
        type = ItemType.Ammo;
    }
}
