using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���ҧ item �ͧ���� type ���кؤ����� type ������� Variable ���ú�ҧ ������͡ type �繢ͧ����ͧ
[CreateAssetMenu(fileName = "New Food Obj", menuName ="Inventory System/Items/Food")]
public class FoodObj : ItemObj
{

    private void Awake()
    {
        type = ItemType.Food;
    }
}
