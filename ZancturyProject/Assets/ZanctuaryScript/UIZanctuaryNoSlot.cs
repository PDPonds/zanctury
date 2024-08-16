using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIZanctuaryNoSlot : MonoBehaviour
{
    public InventoryObj ZInventory;
    [Header("================ CookingStove =================")]
    [SerializeField] private TextMeshProUGUI restorHungryText;
    [SerializeField] private TextMeshProUGUI FoodAmout;
    [SerializeField] private Button useFoodButton;
    [Header("================ Meds Station =================")]
    [SerializeField] private TextMeshProUGUI restorHPText;
    [SerializeField] private TextMeshProUGUI firstAidAmout;
    [SerializeField] private Button usefirstAidButton;
    [Header("================ State Player =================")]
    public int currentPlayerSelect;

    [SerializeField] private TextMeshProUGUI hP;
    [SerializeField] private TextMeshProUGUI maxHp;
    [SerializeField] private TextMeshProUGUI injury;
    [SerializeField] private TextMeshProUGUI hungryScale;

    [SerializeField] private TextMeshProUGUI characterDiscription;
    [SerializeField] private string carterDiscription;
    [SerializeField] private string lukeDiscription;
    [SerializeField] private string hannahDiscription;

    [SerializeField] GameObject carterName;
    [SerializeField] GameObject lukeName;
    [SerializeField] GameObject hannahName;

    [SerializeField] private Button carterSelect;
    [SerializeField] private Button lukeSelect;
    [SerializeField] private Button hannahSelect;

    [SerializeField] RawImage CharacterImage;
    [SerializeField] RenderTexture carterImage;
    [SerializeField] RenderTexture hannahImage;
    [SerializeField] RenderTexture lukeImage;

    [SerializeField] Image currentBg;
    [SerializeField] Sprite carterBg;
    [SerializeField] Sprite lukeBg;
    [SerializeField] Sprite hannahBg;

    [SerializeField] Image carterSlashStateUI;
    [SerializeField] Image lukeSlashStateUI;
    [SerializeField] Image hannahSlashStateUI;

    [Header("================ SelectPlayer =================")]
    [SerializeField] TextMeshProUGUI currentInjuryCarter;
    [SerializeField] TextMeshProUGUI currentInjuryLuke;
    [SerializeField] TextMeshProUGUI currentInjuryHannah;
    [SerializeField] TextMeshProUGUI currentHungryCarter;
    [SerializeField] TextMeshProUGUI currentHungryLuke;
    [SerializeField] TextMeshProUGUI currentHungryHannah;

    [SerializeField] Image carterSlashSelect;
    [SerializeField] Image lukeSlashSelect;
    [SerializeField] Image hannahSlashSelect;

    private void Start()
    {
        if (gameObject.name == "CookingStoveUI")
        {
            useFoodButton = GameObject.Find("UseFoodItem").GetComponent<Button>();
        }
        else if (gameObject.name == "Meds StationUI")
        {
            usefirstAidButton = GameObject.Find("UseFirstAidItem").GetComponent<Button>();
        }
        else if (gameObject.name == "CharacterStateUI")
        {
            carterSelect = GameObject.Find("carterButton").GetComponent<Button>();
            lukeSelect = GameObject.Find("lukeButton").GetComponent<Button>();
            hannahSelect = GameObject.Find("hannahButton").GetComponent<Button>();
            CharacterImage = GameObject.Find("CharacterImage").GetComponent<RawImage>();
        }

    }
    void Update()
    {
        #region Cooking
        if (gameObject.name == "CookingStoveUI")
        {
            restorHungryText.text = StateVariableController.restorHungry.ToString();
            for (int i = 0; i < ZInventory.Container.Count; i++)
            {
                if (ZInventory.Container[i].item.itemID == 4)
                {
                    FoodAmout.text = ZInventory.Container[i].amout.ToString();
                    useFoodButton.interactable = true;
                    break;
                }
                else
                {
                    FoodAmout.text = "0";
                    if (StateVariableController.player1HungryScal >= 10 && StateVariableController.player2HungryScal >= 10 && StateVariableController.player3HungryScal >= 10)
                    {
                        useFoodButton.interactable = false;
                    }
                }
                
            }
            if (ZInventory.Container.Count <= 0)
            {
                FoodAmout.text = "0";
            }
        }
        #endregion

        #region Meds
        else if (gameObject.name == "Meds StationUI")
        {
            restorHPText.text = StateVariableController.restorHP.ToString();
            for (int i = 0; i < ZInventory.Container.Count; i++)
            {
                if (ZInventory.Container[i].item.itemID == 3)
                {
                    firstAidAmout.text = ZInventory.Container[i].amout.ToString();
                    usefirstAidButton.interactable = true;
                    break;
                }
                else
                {
                    firstAidAmout.text = "0";
                    if (StateVariableController.player1CurrentLife >= 100 && StateVariableController.player2CurrentLife >= 100 && StateVariableController.player3CurrentLife >= 90)
                    {
                        usefirstAidButton.interactable = false;
                    }
                }
            }
            if (ZInventory.Container.Count <= 0)
            {
                firstAidAmout.text = "0";
            }
        }
        #endregion

        #region CharacterStateUI
        else if (gameObject.name == "CharacterStateUI")
        {
            if(StateVariableController.carterIsDead == false)
            {
                carterSlashStateUI.gameObject.SetActive(false);
            }else carterSlashStateUI.gameObject.SetActive(true);

            if (StateVariableController.lukeIsDead == false)
            {
                lukeSlashStateUI.gameObject.SetActive(false);
            }
            else lukeSlashStateUI.gameObject.SetActive(true);

            if (StateVariableController.hannahIsDead == false)
            {
                hannahSlashStateUI.gameObject.SetActive(false);
            }
            else hannahSlashStateUI.gameObject.SetActive(true);

            if (currentPlayerSelect == 0)
            {
                if (StateVariableController.carterIsDead == false) currentPlayerSelect = 1;
                else
                {
                    if (StateVariableController.lukeIsDead == false) currentPlayerSelect = 2;
                    else currentPlayerSelect = 3;
                }
            }
            else if (currentPlayerSelect == 1)
            {

                carterName.SetActive(true);
                lukeName.SetActive(false);
                hannahName.SetActive(false);
                injury.text = StateVariableController.current1Injury.ToString();
                hungryScale.text = StateVariableController.current1Hungry.ToString();
                CharacterImage.texture = carterImage;
                currentBg.sprite = carterBg;
                characterDiscription.text = carterDiscription;

            }
            else if (currentPlayerSelect == 2)
            {

                carterName.SetActive(false);
                lukeName.SetActive(true);
                hannahName.SetActive(false);
                injury.text = StateVariableController.current2Injury.ToString();
                hungryScale.text = StateVariableController.current2Hungry.ToString();
                CharacterImage.texture = lukeImage;
                currentBg.sprite = lukeBg;
                characterDiscription.text = lukeDiscription;

            }
            else if (currentPlayerSelect == 3)
            {

                carterName.SetActive(false);
                lukeName.SetActive(false);
                hannahName.SetActive(true);
                injury.text = StateVariableController.current3Injury.ToString();
                hungryScale.text = StateVariableController.current3Hungry.ToString();
                CharacterImage.texture = hannahImage;
                currentBg.sprite = hannahBg;
                characterDiscription.text = hannahDiscription;

            }
        }
        #endregion

        #region SelectPlayer
        else if (gameObject.name == "SelectPlayer")
        {
            if (StateVariableController.carterIsDead == false)
            {
                carterSlashSelect.gameObject.SetActive(false);
            }
            else carterSlashSelect.gameObject.SetActive(true);

            if (StateVariableController.lukeIsDead == false)
            {
                lukeSlashSelect.gameObject.SetActive(false);
            }
            else lukeSlashSelect.gameObject.SetActive(true);

            if (StateVariableController.hannahIsDead == false)
            {
                hannahSlashSelect.gameObject.SetActive(false);
            }
            else hannahSlashSelect.gameObject.SetActive(true);

            currentInjuryCarter.text = StateVariableController.current1Injury.ToString();
            currentHungryCarter.text = StateVariableController.current1Hungry.ToString();

            currentInjuryLuke.text = StateVariableController.current2Injury.ToString();
            currentHungryLuke.text = StateVariableController.current2Hungry.ToString();

            currentInjuryHannah.text = StateVariableController.current3Injury.ToString();
            currentHungryHannah.text = StateVariableController.current3Hungry.ToString();
        }
        #endregion
    }

    public void ChangeCharacterState(int playerID)
    {
        currentPlayerSelect = playerID;
    }
    

}
