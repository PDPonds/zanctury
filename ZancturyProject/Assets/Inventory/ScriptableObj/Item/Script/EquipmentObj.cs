using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���ҧ item �ͧ���� type ���кؤ����� type ������� Variable ���ú�ҧ ������͡ type �繢ͧ����ͧ
[CreateAssetMenu(fileName = "New Equipment Obj", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObj : ItemObj
{
    private void Awake()
    {
        type = ItemType.Equipment;
    }
}
