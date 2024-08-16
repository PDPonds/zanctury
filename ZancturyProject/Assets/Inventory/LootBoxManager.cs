using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LootBoxManager : MonoBehaviour
{
    //public Button takeAllButton;
    InventoryObj lootBox;
    InventoryObj inventory;

    playerInventory _playerInventory;

    public List<Transform> slotPosInven = new List<Transform>();
    public List<Transform> slotPosLootBox = new List<Transform>();

    private void Start()
    {
        _playerInventory = GameObject.Find("Player").GetComponent<playerInventory>();
    }
    private void Update()
    {
        lootBox = _playerInventory.lootBox;
        inventory = _playerInventory.inventory;
    }

}
