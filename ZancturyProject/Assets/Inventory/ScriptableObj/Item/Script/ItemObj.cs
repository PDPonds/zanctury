using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���ҧ Item function �˭������ҷء� item ����դ�����ú�ҧ�� type ���� description ͸Ժ�� item
public enum ItemType
{ 
    Food,
    Equipment,
    Material,
    Ammo,
    KeyItem
}

//�� ScriptableObje ������� item �����ѹ��Ҷ֧�����
public abstract class ItemObj : ScriptableObject
{
    public int itemID;
    //public GameObject prefab;
    public string itemName;
    public Sprite icon;

    public ItemType type;

    public float weight;
    public bool stackAble;

    public bool isGun;
}
