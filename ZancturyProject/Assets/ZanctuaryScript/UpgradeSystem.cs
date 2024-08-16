using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    public InventoryObj ZInventory;

    public ItemObj item1;
    public ItemObj item2;
    public ItemObj item3;

    [SerializeField] private TextMeshProUGUI currentLevel;
    [Header("============== Button ==============")]
    [SerializeField] private Button upgradeButton;

    [Header("============== If Use 1 Item To Upgrade ==============")]
    [SerializeField] private Image item1Image;
    [SerializeField] private TextMeshProUGUI item1AmoutText;
    [SerializeField] private int item1Amout;
    [SerializeField] private TextMeshProUGUI upgrade1AmoutText;
    [SerializeField] private int upgrade1Amout;
    [Header("============== If Use 2 Item To Upgrade ==============")]
    [SerializeField] private Image item2Image;
    [SerializeField] private int item2Amout;
    [SerializeField] private TextMeshProUGUI item2AmoutText;
    [SerializeField] private TextMeshProUGUI upgrade2AmoutText;
    [SerializeField] private int upgrade2Amout;

    [Header("============== If Use 3 Item To Upgrade ==============")]
    [SerializeField] private Image item3Image;
    [SerializeField] private int item3Amout;
    [SerializeField] private TextMeshProUGUI item3AmoutText;
    [SerializeField] private TextMeshProUGUI upgrade3AmoutText;
    [SerializeField] private int upgrade3Amout;

    [Header("============= FixRaioUI =================")]
    public GameObject fixRadioUI;
    void Start()
    {
        if (this.name == "UpgradeCookPanel")
        {
            upgradeButton = GameObject.Find("UpgradeButtonCook").GetComponent<Button>();
            //SetUpItem(StateVariableController.cookingStoveLevel, 2);
        }
        else if (this.name == "UpgradeMedPanel")
        {
            upgradeButton = GameObject.Find("UpgradeButtonMed").GetComponent<Button>();
            //SetUpItem(StateVariableController.medStationLevel, 3);
        }
        else if (this.name == "UpgradeSecurityPanel")
        {
            upgradeButton = GameObject.Find("UpgradeButtonSecurity").GetComponent<Button>();
            //SetUpItem(StateVariableController.securityLevel, 2);
        }
        else if (this.name == "FixRadioPanel")
        {
            upgradeButton = GameObject.Find("FixRadio").GetComponentInChildren<Button>();
        }
    }

    void Update()
    {
        if (this.name == "UpgradeCookPanel")
        {
            SetUpItem(StateVariableController.cookingStoveLevel,2);
        }
        else if (this.name == "UpgradeMedPanel")
        {
            SetUpItem(StateVariableController.medStationLevel, 3);
        }
        if(this.name == "UpgradeSecurityPanel")
        {
            SetUpItem(StateVariableController.securityLevel, 2);
        }

        if(this.name == "FixRadioPanel")
        {
            SetUpFixRadio(StateVariableController.radioFixAlready, 3);
        }
    }

    public void SetUpItem(int stationLevel , int UseItemCount)
    {
        if(UseItemCount == 2)
        {
            currentLevel.text = stationLevel.ToString();
            item1Image.sprite = item1.icon;
            item2Image.sprite = item2.icon;
            upgrade1AmoutText.text = upgrade1Amout.ToString();
            upgrade2AmoutText.text = upgrade2Amout.ToString();
            for (int i = 0; i < ZInventory.Container.Count; i++)
            {
                if (ZInventory.Container[i].item.itemID == item1.itemID)
                {
                    item1Amout = ZInventory.Container[i].amout;
                    item1AmoutText.text = ZInventory.Container[i].amout.ToString();
                    break;
                }
                else
                {
                    item1Amout = 0;
                    item1AmoutText.text = "0";
                }
            }

            for (int i = 0; i < ZInventory.Container.Count; i++)
            {

                if (ZInventory.Container[i].item.itemID == item2.itemID)
                {
                    item2Amout = ZInventory.Container[i].amout;
                    item2AmoutText.text = ZInventory.Container[i].amout.ToString();
                    break;
                }
                else
                {
                    item2Amout = 0;
                    item2AmoutText.text = "0";
                }
            }

            if (item1Amout >= upgrade1Amout && item2Amout >= upgrade2Amout && stationLevel < 3)
            {
                upgradeButton.interactable = true;
            }
            else
            {
                upgradeButton.interactable = false;
            }
            if (ZInventory.Container.Count <= 0)
            {
                item1Amout = 0;
                item1AmoutText.text = "0";
                item2Amout = 0;
                item2AmoutText.text = "0";
            }
        }
        else if (UseItemCount == 3)
        {
            currentLevel.text = stationLevel.ToString();
            item1Image.sprite = item1.icon;
            item2Image.sprite = item2.icon;
            item3Image.sprite = item3.icon;
            upgrade1AmoutText.text = upgrade1Amout.ToString();
            upgrade2AmoutText.text = upgrade2Amout.ToString();
            upgrade3AmoutText.text = upgrade3Amout.ToString();
            for (int i = 0; i < ZInventory.Container.Count; i++)
            {
                if (ZInventory.Container[i].item.itemID == item1.itemID)
                {
                    item1Amout = ZInventory.Container[i].amout;
                    item1AmoutText.text = ZInventory.Container[i].amout.ToString();
                    break;
                }
                else
                {
                    item1Amout = 0;
                    item1AmoutText.text = "0";
                }
            }
            for (int i = 0; i < ZInventory.Container.Count; i++)
            {

                if (ZInventory.Container[i].item.itemID == item2.itemID)
                {
                    item2Amout = ZInventory.Container[i].amout;
                    item2AmoutText.text = ZInventory.Container[i].amout.ToString();
                    break;
                }
                else
                {
                    item2Amout = 0;
                    item2AmoutText.text = "0";
                }
            }

            for (int i = 0; i < ZInventory.Container.Count; i++)
            {

                if (ZInventory.Container[i].item.itemID == item3.itemID)
                {
                    item3Amout = ZInventory.Container[i].amout;
                    item3AmoutText.text = ZInventory.Container[i].amout.ToString();
                    break;
                }
                else
                {
                    item3Amout = 0;
                    item3AmoutText.text = "0";
                }
            }

            if (item1Amout >= upgrade1Amout && item2Amout >= upgrade2Amout && item3Amout >= upgrade3Amout && stationLevel < 3)
            {
                upgradeButton.interactable = true;
            }
            else
            {
                upgradeButton.interactable = false;
            }
            if (ZInventory.Container.Count <= 0)
            {
                item1Amout = 0;
                item1AmoutText.text = "0";
                item2Amout = 0;
                item2AmoutText.text = "0";
                item3Amout = 0;
                item3AmoutText.text = "0";
            }
        }

    }


    public void UpgradeButton()
    {
        if (this.name == "UpgradeCookPanel")
        {
            StateVariableController.cookingStoveLevel++;
            ZInventory.RemoveItem(item1, upgrade1Amout);
            ZInventory.RemoveItem(item2, upgrade2Amout);
            upgrade1Amout *= 2;
            upgrade2Amout *= 2;
        }
        else if(this.name == "UpgradeMedPanel")
        {
            StateVariableController.medStationLevel++;
            ZInventory.RemoveItem(item1, upgrade1Amout);
            ZInventory.RemoveItem(item2, upgrade2Amout);
            ZInventory.RemoveItem(item3, upgrade3Amout);
            upgrade1Amout *= 2;
            upgrade2Amout *= 2;
            upgrade3Amout *= 2;
        }
        if (this.name == "UpgradeSecurityPanel")
        {
            StateVariableController.securityLevel++;
            ZInventory.RemoveItem(item1, upgrade1Amout);
            ZInventory.RemoveItem(item2, upgrade2Amout);
            upgrade1Amout *= 2;
            upgrade2Amout *= 2;
        }
        if(this.name == "FixRadioPanel")
        {
            StateVariableController.radioFixAlready = true;
            StateVariableController.currentQuestState++;
            ZInventory.RemoveItem(item1, upgrade1Amout);
            ZInventory.RemoveItem(item2, upgrade2Amout);
            ZInventory.RemoveItem(item3, upgrade3Amout);
            fixRadioUI.SetActive(false);
        }
    }

    public void SetUpFixRadio(bool fixAlready,int UseItemCount)
    {
        if(StateVariableController.radioFixAlready == false)
        {
            if (UseItemCount == 3)
            {
                item1Image.sprite = item1.icon;
                item2Image.sprite = item2.icon;
                item3Image.sprite = item3.icon;
                upgrade1AmoutText.text = upgrade1Amout.ToString();
                upgrade2AmoutText.text = upgrade2Amout.ToString();
                upgrade3AmoutText.text = upgrade3Amout.ToString();
                for (int i = 0; i < ZInventory.Container.Count; i++)
                {
                    if (ZInventory.Container[i].item.itemID == item1.itemID)
                    {
                        item1Amout = ZInventory.Container[i].amout;
                        item1AmoutText.text = ZInventory.Container[i].amout.ToString();
                        break;
                    }
                    else
                    {
                        item1Amout = 0;
                        item1AmoutText.text = "0";
                    }
                }
                for (int i = 0; i < ZInventory.Container.Count; i++)
                {

                    if (ZInventory.Container[i].item.itemID == item2.itemID)
                    {
                        item2Amout = ZInventory.Container[i].amout;
                        item2AmoutText.text = ZInventory.Container[i].amout.ToString();
                        break;
                    }
                    else
                    {
                        item2Amout = 0;
                        item2AmoutText.text = "0";
                    }
                }

                for (int i = 0; i < ZInventory.Container.Count; i++)
                {

                    if (ZInventory.Container[i].item.itemID == item3.itemID)
                    {
                        item3Amout = ZInventory.Container[i].amout;
                        item3AmoutText.text = ZInventory.Container[i].amout.ToString();
                        break;
                    }
                    else
                    {
                        item3Amout = 0;
                        item3AmoutText.text = "0";
                    }
                }

                if (item1Amout >= upgrade1Amout && item2Amout >= upgrade2Amout && item3Amout >= upgrade3Amout)
                {
                    upgradeButton.interactable = true;
                }
                else
                {
                    upgradeButton.interactable = false;
                }
                if (ZInventory.Container.Count <= 0)
                {
                    item1Amout = 0;
                    item1AmoutText.text = "0";
                    item2Amout = 0;
                    item2AmoutText.text = "0";
                    item3Amout = 0;
                    item3AmoutText.text = "0";
                }
            }
        }
        
    }

}
