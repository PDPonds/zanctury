using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI day;
    [SerializeField] GameObject correctRepairRadioStation;
    [SerializeField] TextMeshProUGUI questComplete;

    [SerializeField] GameObject carterSlash;
    [SerializeField] GameObject lukeSlash;
    [SerializeField] GameObject hannahSlash;

    private void Update()
    {
        day.text = StateVariableController.currentDay.ToString();

        if(StateVariableController.radioFixAlready == true)
        {
            correctRepairRadioStation.SetActive(true);
            int currentQuestComplete = StateVariableController.currentQuestState - 1;
            if(currentQuestComplete < 0)
            {
                currentQuestComplete = 0;
            }
            questComplete.text = currentQuestComplete.ToString();
        }
        else
        {
            correctRepairRadioStation.SetActive(false);
            questComplete.text = "0";
        }

        if(StateVariableController.carterIsDead == true) carterSlash.SetActive(true);
        else carterSlash.SetActive(false);

        if (StateVariableController.lukeIsDead == true) lukeSlash.SetActive(true);
        else lukeSlash.SetActive(false);

        if (StateVariableController.hannahIsDead == true) hannahSlash.SetActive(true);
        else hannahSlash.SetActive(false);

    }
}
