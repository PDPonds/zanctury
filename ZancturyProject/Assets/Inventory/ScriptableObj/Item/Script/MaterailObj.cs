using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���ҧ item �ͧ���� type ���кؤ����� type ������� Variable ���ú�ҧ ������͡ type �繢ͧ����ͧ
[CreateAssetMenu(fileName = "New Materail Obj", menuName = "Inventory System/Items/Materail")]
public class MaterailObj : ItemObj
{
    private void Awake()
    {
        type = ItemType.Material;
    }
}
