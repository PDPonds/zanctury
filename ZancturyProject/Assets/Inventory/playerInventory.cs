using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerInventory : MonoBehaviour
{
    #region Variable
    public InventoryObj lootBox;
    public InventoryObj inventory;
    public InventoryObj equipment;
    public InventoryObj melee;

    //กำหนด inventory
    public GameObject inventoryUI;
    public GameObject lootUI;
    public GameObject equipmentHotbar;
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI slash;
    public TextMeshProUGUI ammoAmount;
    public GameObject equipmentInInventory;

    CombatSystem _comboScript;
    CharacterMovement _playerMovement;

    public GameObject RepairGenaratorPanel;
    public GameObject UsePliersPanel;
    public GameObject interacRadio;
    public GameObject RadioRepairPanel;
    public GameObject UseKeyCard;
    public GameObject questPanel;

    #endregion

    private void Start()
    {
        #region GetComponent
        _comboScript = this.GetComponent<CombatSystem>();
        _playerMovement = this.GetComponent<CharacterMovement>();

        #endregion
    }
    private void Update()
    {
        if (Pause.GameIsPaused == false)
        {
            #region useInventory
            if (Input.GetKeyDown(KeyCode.Tab) && !inventoryUI.activeSelf)
            {
                GameManager.Instance.OpenUISound();
                inventoryUI.SetActive(true);
                equipmentHotbar.SetActive(false);
                equipmentInInventory.SetActive(true);
                currentAmmo.gameObject.SetActive(false);
                slash.gameObject.SetActive(false);
                ammoAmount.gameObject.SetActive(false);
            }
            #endregion

            if(Input.GetKeyDown(KeyCode.M) && !questPanel.activeSelf)
            {
                GameManager.Instance.OpenUISound();
                questPanel.SetActive(true);
            }
            else if(Input.GetKeyDown(KeyCode.M) && questPanel.activeSelf)
            {
                GameManager.Instance.CloseUISound();
                questPanel.SetActive(false);
            }

            #region Can't use Atk & AimMode
            if (!inventoryUI.activeSelf && !lootUI.activeSelf && !questPanel.activeSelf)
            {
            }
            else
            {
                _playerMovement.isAimMode = false;
                _comboScript.canAtk = false;
            }
            #endregion

            #region Switch What u use gun
            if (equipment.Container.Count == 1)
            {
                var equipmentInHand = equipment.Container[0].item.itemName;
                switch (equipmentInHand)
                {
                    case "Glock":
                        _playerMovement.gunGlock = true;
                        _playerMovement.gunM16 = false;
                        _playerMovement.gunShootGun = false;
                        break;
                    case "Rifle":
                        _playerMovement.gunGlock = false;
                        _playerMovement.gunM16 = true;
                        _playerMovement.gunShootGun = false;
                        break;
                    case "Shotgun":
                        _playerMovement.gunGlock = false;
                        _playerMovement.gunM16 = false;
                        _playerMovement.gunShootGun = true;
                        break;
                }
            }
            else if (equipment.Container.Count == 0)
            {
                _playerMovement.gunGlock = false;
                _playerMovement.gunM16 = false;
                _playerMovement.gunShootGun = false;
            }
            #endregion

            #region Switch What u use gun
            if (melee.Container.Count == 1)
            {
                var meleeInHand = melee.Container[0].item.itemName;
                switch (meleeInHand)
                {
                    case "Knife":
                        _playerMovement.isknife = true;
                        _playerMovement.isIronBar = false;
                        _playerMovement.isBaseballBat = false;
                        break;
                    case "BaseBallBat":
                        _playerMovement.isknife = false;
                        _playerMovement.isIronBar = false;
                        _playerMovement.isBaseballBat = true;
                        break;
                    case "IronBar":
                        _playerMovement.isknife = false;
                        _playerMovement.isIronBar = true;
                        _playerMovement.isBaseballBat = false;
                        break;
                }
            }
            else if (melee.Container.Count == 0)
            {
                _playerMovement.isknife = false;
                _playerMovement.isIronBar = false;
                _playerMovement.isBaseballBat = false;
            }
            #endregion

        }

    }

    public void CloseInventoryAndLootBox()
    {
        GameManager.Instance.CloseUISound();
        lootBox = null;
        lootUI.SetActive(false);
        equipmentHotbar.SetActive(true);
        currentAmmo.gameObject.SetActive(true);
        slash.gameObject.SetActive(true);
        ammoAmount.gameObject.SetActive(true);
        inventoryUI.SetActive(false);
        if(RepairGenaratorPanel.activeSelf)
        {
            RepairGenaratorPanel.SetActive(false);
        }
        if (RadioRepairPanel.activeSelf)
        {
            RadioRepairPanel.SetActive(false);
        }
        if (UsePliersPanel.activeSelf)
        {
            UsePliersPanel.SetActive(false);
        }
        if(questPanel.activeSelf)
        {
            questPanel.SetActive(false);
        }
        if(UseKeyCard.activeSelf)
        {
            UseKeyCard.SetActive(false);
        }
        if(interacRadio.activeSelf)
        {
            interacRadio.SetActive(false);
        }
    }

}

