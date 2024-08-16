using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestRepair : MonoBehaviour, Iinteractable
{
    public enum QuestState { RepairGenerator, DestroyStorngChain, RepairRadioTower, InteracRadio, OpenDoorWithKey }
    public QuestState currentQuestState;

    public bool isSearch;
    bool startRepair;
    [SerializeField] LayerMask playerMask;
    [SerializeField] float areaInterac;
    [Header("========= Repair Duration ==========")]
    public float currentDuration;
    [SerializeField] float repairDuration;

    CharacterMovement player;
    QuestManager questManager;

    [Header("========= Setup Canva Obj ==========")]
    public GameObject RepairMenu;

    [Header("========= Button ==========")]
    public Button RepairButton;

    [Header("========= Setup Display ==========")]
    [SerializeField] Image item1Image;
    [SerializeField] int item1Amout;
    [SerializeField] TextMeshProUGUI amout1;
    [SerializeField] TextMeshProUGUI toUse1;

    [SerializeField] Image item2Image;
    [SerializeField] int item2Amout;
    [SerializeField] TextMeshProUGUI amout2;
    [SerializeField] TextMeshProUGUI toUse2;

    InventoryObj playerInventory;

    [Header("========= Objtive Result ==========")]
    [SerializeField] GameObject door;
    [SerializeField] GameObject interacRadio;
    [SerializeField] TextMeshProUGUI contactBunker;
    [SerializeField] string bunkerResult;
    private void Awake()
    {
        isSearch = false;
        startRepair = false;
        questManager = GameObject.Find("GameManager").GetComponent<QuestManager>();
        playerInventory = GameObject.Find("Player").GetComponent<playerInventory>().inventory;
        player = GameObject.Find("Player").GetComponent<CharacterMovement>();
    }
    public void Interact()
    {
        if (currentQuestState == QuestState.InteracRadio)
        {
            isSearch = !isSearch;
            if (isSearch)
            {
                interacRadio.SetActive(true);
            }
            else
            {
                interacRadio.SetActive(false);
            }
        }
        else
        {
            isSearch = !isSearch;
            if (isSearch)
            {
                RepairMenu.SetActive(true);
            }
            else
            {
                RepairMenu.SetActive(false);
            }
        }
    }

    private void Update()
    {

        if (questManager.currentQuest.Count > 0)
        {
            switch (currentQuestState)
            {
                case QuestState.RepairGenerator:

                    #region SetUp Desplay UI
                    if (item1Image != null && amout1 != null && toUse1 != null)
                    {
                        var thisItem = questManager.quest[1].item1;
                        item1Image.sprite = thisItem.icon;
                        toUse1.text = questManager.quest[1].item1Amout.ToString();
                    }
                    #endregion

                    #region Setup Amout
                    if (playerInventory.Container.Count > 0)
                    {
                        for (int i = 0; i < playerInventory.Container.Count; i++)
                        {
                            var thisItem = questManager.quest[1].item1;
                            if (playerInventory.Container[i].item == thisItem)
                            {
                                item1Amout = playerInventory.Container[i].amout;
                                break;
                            }
                        }
                        amout1.text = item1Amout.ToString();
                    }
                    #endregion

                    #region Disable Button if Quest to active
                    if (questManager.currentQuest.Count > 0)
                    {
                        if (questManager.currentQuest[0].questID == 2)
                        {
                            if (item1Image != null)
                            {
                                item1Image.color = Color.white;
                            }

                            if (item1Amout >= questManager.currentQuest[0].item1Amout)
                            {
                                RepairButton.interactable = true;
                            }
                            else
                            {
                                RepairButton.interactable = false;
                            }
                        }
                        else
                        {
                            if (item1Image != null)
                            {
                                item1Image.color = Color.gray;
                            }
                            RepairButton.interactable = false;
                        }
                    }
                    else
                    {
                        if (item1Image != null)
                        {
                            item1Image.color = Color.gray;
                        }
                        RepairButton.interactable = false;
                    }
                    #endregion

                    #region currentQuest > Quest
                    if (StateVariableController.currentQuestState > 2)
                    {
                        var DoorGenerator = door.GetComponent<Animator>();
                        DoorGenerator.SetBool("OpenDoor", true);
                    }
                    #endregion

                    break;
                case QuestState.DestroyStorngChain:

                    #region SetUp Desplay UI
                    if (item1Image != null)
                    {
                        item1Image.sprite = questManager.quest[3].item1.icon;
                    }
                    #endregion

                    #region Setup Amout
                    if (playerInventory.Container.Count > 0)
                    {
                        for (int i = 0; i < playerInventory.Container.Count; i++)
                        {
                            var thisItem = questManager.quest[3].item1;
                            if (playerInventory.Container[i].item == thisItem)
                            {
                                item1Amout = playerInventory.Container[i].amout;
                                break;
                            }
                        }
                    }
                    #endregion

                    #region Disable Button if Quest to active
                    if (questManager.currentQuest.Count > 0)
                    {
                        if (questManager.currentQuest[0].questID == 4)
                        {
                            if (item1Image != null)
                            {
                                item1Image.color = Color.white;
                            }

                            if (item1Amout >= questManager.currentQuest[0].item1Amout)
                            {
                                RepairButton.interactable = true;
                            }
                            else
                            {
                                RepairButton.interactable = false;
                            }
                        }
                        else
                        {
                            if (item1Image != null)
                            {
                                item1Image.color = Color.gray;
                            }
                            RepairButton.interactable = false;
                        }
                    }
                    else
                    {
                        if (item1Image != null)
                        {
                            item1Image.color = Color.gray;
                        }
                        RepairButton.interactable = false;
                    }
                    #endregion

                    #region currentQuest > Quest
                    if (StateVariableController.currentQuestState > 4)
                    {
                        door.SetActive(false);
                    }
                    #endregion

                    break;
                case QuestState.RepairRadioTower:

                    #region SetUp Desplay UI
                    if (item1Image != null && amout1 != null && toUse1 != null)
                    {
                        var thisItem = questManager.quest[4].item1;
                        item1Image.sprite = thisItem.icon;
                        toUse1.text = questManager.quest[4].item1Amout.ToString();
                    }
                    if (item2Image != null && amout2 != null && toUse2 != null)
                    {
                        var thisItem2 = questManager.quest[4].item2;
                        item2Image.sprite = thisItem2.icon;
                        toUse2.text = questManager.quest[4].item2Amout.ToString();
                    }
                    #endregion

                    #region Setup Amout
                    if (playerInventory.Container.Count > 0)
                    {
                        for (int i = 0; i < playerInventory.Container.Count; i++)
                        {
                            var thisItem1 = questManager.quest[4].item1;

                            if (playerInventory.Container[i].item == thisItem1)
                            {
                                item1Amout = playerInventory.Container[i].amout;
                                break;
                            }
                        }
                        for (int i = 0; i < playerInventory.Container.Count; i++)
                        {
                            var thisItem2 = questManager.quest[4].item2;
                            if (playerInventory.Container[i].item == thisItem2)
                            {
                                item2Amout = playerInventory.Container[i].amout;
                                break;
                            }
                        }
                        amout1.text = item1Amout.ToString();
                        amout2.text = item2Amout.ToString();
                    }
                    #endregion

                    #region Disable Button if Quest to active
                    if (questManager.currentQuest.Count > 0)
                    {
                        if (questManager.currentQuest[0].questID == 5)
                        {
                            if (item1Image != null)
                            {
                                item1Image.color = Color.white;
                            }
                            if (item2Image != null)
                            {
                                item2Image.color = Color.white;
                            }

                            if (item1Amout >= questManager.currentQuest[0].item1Amout && item2Amout >= questManager.currentQuest[0].item2Amout)
                            {
                                RepairButton.interactable = true;
                            }
                            else
                            {
                                RepairButton.interactable = false;
                            }
                        }
                        else
                        {
                            if (item1Image != null)
                            {
                                item1Image.color = Color.gray;
                            }
                            if (item2Image != null)
                            {
                                item2Image.color = Color.gray;
                            }
                            RepairButton.interactable = false;
                        }
                    }
                    else
                    {
                        if (item1Image != null)
                        {
                            item1Image.color = Color.gray;
                        }
                        if (item2Image != null)
                        {
                            item2Image.color = Color.gray;
                        }
                        RepairButton.interactable = false;
                    }
                    #endregion

                    #region currentQuest > Quest
                    if (StateVariableController.currentQuestState > 5)
                    {
                        currentQuestState = QuestState.InteracRadio;
                    }
                    #endregion


                    break;
                case QuestState.OpenDoorWithKey:

                    #region SetUp Desplay UI
                    if (item1Image != null)
                    {
                        item1Image.sprite = questManager.quest[7].item1.icon;
                    }
                    #endregion

                    #region Setup Amout
                    if (playerInventory.Container.Count > 0)
                    {
                        for (int i = 0; i < playerInventory.Container.Count; i++)
                        {
                            var thisItem = questManager.quest[7].item1;
                            if (playerInventory.Container[i].item == thisItem)
                            {
                                item1Amout = playerInventory.Container[i].amout;
                                break;
                            }
                        }
                    }
                    #endregion

                    #region Disable Button if Quest to active
                    if (questManager.currentQuest.Count > 0)
                    {
                        if (questManager.currentQuest[0].questID == 8)
                        {
                            if (item1Image != null)
                            {
                                item1Image.color = Color.white;
                            }

                            if (item1Amout >= questManager.currentQuest[0].item1Amout)
                            {
                                RepairButton.interactable = true;
                            }
                            else
                            {
                                RepairButton.interactable = false;
                            }
                        }
                        else
                        {
                            if (item1Image != null)
                            {
                                item1Image.color = Color.gray;
                            }
                            RepairButton.interactable = false;
                        }
                    }
                    else
                    {
                        if (item1Image != null)
                        {
                            item1Image.color = Color.gray;
                        }
                        RepairButton.interactable = false;
                    }
                    #endregion

                    #region currentQuest > Quest
                    var DoorGenerator2 = door.GetComponent<Animator>();
                    DoorGenerator2.SetBool("OpenDoor", true);
                    #endregion

                    break;
            }
        }
        if (startRepair)
        {
            if (questManager.currentQuest.Count > 0)
            {
                RepairMenu.SetActive(false);
                player.interacUI.SetActive(false);
                player.coutDownUI.SetActive(true);
                var thisCoutDown = player.coutDownUI.transform.GetComponentInChildren<TextMeshProUGUI>();
                if (thisCoutDown != null)
                {
                    thisCoutDown.text = currentDuration.ToString("0.0");
                }
                currentDuration -= Time.deltaTime;
                if (currentDuration <= 0)
                {
                    player.coutDownUI.SetActive(false);
                    #region Setup Remove Item in Inventory
                    var thisItem = questManager.currentQuest[0].item1;
                    var thisAmout = questManager.currentQuest[0].item1Amout;
                    var thisItem2 = questManager.currentQuest[0].item2;
                    var thisAmout2 = questManager.currentQuest[0].item2Amout;
                    #endregion

                    currentDuration = 0;
                    switch (currentQuestState)
                    {
                        case QuestState.RepairGenerator:
                            if (questManager.currentQuest[0].questID == 2)
                            {
                                #region Remove Item Inventory
                                if (playerInventory.Container.Count > 0)
                                {
                                    for (int i = 0; i < playerInventory.Container.Count; i++)
                                    {
                                        if (playerInventory.Container[i].item == thisItem)
                                        {
                                            playerInventory.RemoveItem(thisItem, thisAmout);
                                            break;
                                        }
                                        else
                                        {
                                            startRepair = false;
                                        }
                                    }
                                }
                                #endregion

                                questManager.currentQuest[0].procress = Quest.QuestProcress.COMPLETE;

                                #region open door
                                var DoorGenerator = door.GetComponent<Animator>();
                                DoorGenerator.SetBool("OpenDoor", true);
                                #endregion
                            }

                            break;
                        case QuestState.DestroyStorngChain:
                            if (questManager.currentQuest[0].questID == 4)
                            {
                                #region Remove Item Inventory
                                if (playerInventory.Container.Count > 0)
                                {
                                    for (int i = 0; i < playerInventory.Container.Count; i++)
                                    {
                                        if (playerInventory.Container[i].item == thisItem)
                                        {
                                            playerInventory.Container.Remove(playerInventory.Container[i]);
                                            break;
                                        }
                                        else
                                        {
                                            startRepair = false;
                                        }
                                    }
                                }
                                #endregion

                                questManager.currentQuest[0].procress = Quest.QuestProcress.COMPLETE;

                                #region open door
                                door.SetActive(false);
                                #endregion
                            }

                            break;
                        case QuestState.RepairRadioTower:
                            if (questManager.currentQuest[0].questID == 5)
                            {
                                #region Remove Item Inventory
                                if (playerInventory.Container.Count > 0)
                                {
                                    for (int i = 0; i < playerInventory.Container.Count; i++)
                                    {
                                        if (playerInventory.Container[i].item == thisItem)
                                        {
                                            playerInventory.RemoveItem(thisItem, thisAmout);
                                            break;
                                        }
                                        else
                                        {
                                            startRepair = false;
                                        }
                                    }
                                    for (int i = 0; i < playerInventory.Container.Count; i++)
                                    {
                                        if (playerInventory.Container[i].item == thisItem2)
                                        {
                                            playerInventory.RemoveItem(thisItem2, thisAmout2);
                                            break;
                                        }
                                        else
                                        {
                                            startRepair = false;
                                        }
                                    }
                                }

                                #endregion

                                questManager.currentQuest[0].procress = Quest.QuestProcress.COMPLETE;

                                currentQuestState = QuestState.InteracRadio;
                            }

                            break;
                        case QuestState.OpenDoorWithKey:

                            if (questManager.currentQuest[0].questID == 8)
                            {
                                #region Remove Item Inventory
                                if (playerInventory.Container.Count > 0)
                                {
                                    for (int i = 0; i < playerInventory.Container.Count; i++)
                                    {
                                        if (playerInventory.Container[i].item == thisItem)
                                        {
                                            playerInventory.Container.Remove(playerInventory.Container[i]);
                                            break;
                                        }
                                        else
                                        {
                                            startRepair = false;
                                        }
                                    }
                                }
                                #endregion

                                questManager.currentQuest[0].procress = Quest.QuestProcress.COMPLETE;

                                #region open door
                                var DoorGenerator2 = door.GetComponent<Animator>();
                                DoorGenerator2.SetBool("OpenDoor", true);
                                #endregion
                            }

                            break;
                    }
                }
            }
        }

    }

    #region DrawGizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaInterac);
    }

    #endregion

    public void Repair()
    {
        startRepair = true;
    }

    public void ContactBucker()
    {
        contactBunker.text = bunkerResult.ToString();
        if (questManager.currentQuest.Count > 0)
        {
            if (questManager.currentQuest[0].questID == 6)
            {
                questManager.currentQuest[0].procress = Quest.QuestProcress.COMPLETE;
            }
        }
    }

}
