using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [Header("========== QuestState ==========")]
    public int currentQuestState;
    [Header("========== QuestList ==========")]
    public List<Quest> quest = new List<Quest>();
    public List<Quest> currentQuest = new List<Quest>();
    [Header("========== QuestAssetZanctuary ==========")]
    [SerializeField] TextMeshProUGUI currentQuestTextZanctuary;
    [SerializeField] TextMeshProUGUI titleQuestTextZanctuary;
    [SerializeField] TextMeshProUGUI discriptionQuestTextZanctuary;
    [SerializeField] Image questImage;
    [SerializeField] GameObject EndGamePanel;
    [Header("========== QuestAssetLootmap ==========")]
    [SerializeField] Image starImage;
    [SerializeField] TextMeshProUGUI titleQuestTextLootMap;
    [SerializeField] TextMeshProUGUI discriptionQuestTextLootMapFont;

    [SerializeField] string fixRadioTitle;
    [SerializeField] string fixRadioDiscription;
    [SerializeField] TextMeshProUGUI currentQuestTextLootmap;
    [SerializeField] TextMeshProUGUI titleQuestTextLootmap;
    [SerializeField] TextMeshProUGUI discriptionQuestTextLootmap;
    [SerializeField] Image questImageLootmap;

    private void Update()
    {
        #region SetUp Panel
        if (SceneManager.GetActiveScene().name == "Zanctuary")
        {
            if (StateVariableController.radioFixAlready == true && currentQuest.Count > 0)
            {
                currentQuestTextZanctuary.text = currentQuestState.ToString();
                titleQuestTextZanctuary.text = currentQuest[0].title.ToString();
                discriptionQuestTextZanctuary.text = currentQuest[0].discription.ToString();
                questImage.sprite = currentQuest[0].questIcon;
            }

        }
        if (SceneManager.GetActiveScene().name == "LootMap")
        {
            if (StateVariableController.radioFixAlready == true && currentQuest.Count > 0)
            {
                starImage.gameObject.SetActive(true);
                titleQuestTextLootMap.text = currentQuest[0].title.ToString();
                discriptionQuestTextLootMapFont.text = currentQuest[0].discription.ToString();

                questImageLootmap.gameObject.SetActive(true);
                currentQuestTextLootmap.text = currentQuestState.ToString();
                titleQuestTextLootmap.text = currentQuest[0].title.ToString();
                discriptionQuestTextLootmap.text = currentQuest[0].discription.ToString();
                questImageLootmap.sprite = currentQuest[0].questIcon;
            }
            else if (StateVariableController.radioFixAlready == false)
            {
                starImage.gameObject.SetActive(false);
                titleQuestTextLootMap.text = "";
                discriptionQuestTextLootMapFont.text = "";

                currentQuestTextLootmap.text = "Fix Radio Station";
                titleQuestTextLootmap.text = fixRadioTitle.ToString();
                discriptionQuestTextLootmap.text = fixRadioDiscription.ToString();
                questImageLootmap.gameObject.SetActive(false);
            }
        }
        #endregion

        #region Add Quest with currentQuestState
        if (StateVariableController.radioFixAlready == false)
        {
            currentQuestState = 0;
            for (int i = 0; i < quest.Count; i++)
            {
                quest[i].procress = Quest.QuestProcress.NOT_AVAILABLE;
            }
        }
        else
        {
            currentQuestState = StateVariableController.currentQuestState;
            if (currentQuestState > 0 && currentQuest.Count == 0)
            {
                AcceptQuest(currentQuestState);
            }
        }
        #endregion

        #region Complete Quest remove currentQuest and Add CurrentQuestState++
        for (int i = 0; i < currentQuest.Count; i++)
        {
            if (currentQuest[i].procress == Quest.QuestProcress.COMPLETE)
            {
                StateVariableController.currentQuestState++;
                currentQuest.Remove(currentQuest[i]);
                break;
            }
        }
        #endregion

        if (currentQuestState > 9)
        {
            if(SceneManager.GetActiveScene().name == "Zanctuary")
            {
                EndGamePanel.SetActive(true);
            }
        }

    }

    #region Accept Quest
    public void AcceptQuest(int questID)
    {
        for (int i = 0; i < quest.Count; i++)
        {
            if (quest[i].questID == questID && quest != null)
            {
                currentQuest.Add(quest[i]);
                quest[i].procress = Quest.QuestProcress.ACCEPTED;
                break;
            }
        }
    }
    #endregion


}
