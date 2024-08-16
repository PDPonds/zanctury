using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IsDeadPanel : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI currentPlayerName;
    [SerializeField] Image currentPlayerImage;
    [SerializeField] Sprite carterImage;
    [SerializeField] Sprite lukeImage;
    [SerializeField] Sprite hannahImage;

    void Update()
    {
        if(gameObject.activeSelf)
        {
            if(StateVariableController.player1Select)
            {
                currentPlayerImage.sprite = carterImage;
                currentPlayerName.text = "Carter";
            }
            else if(StateVariableController.player2Select)
            {
                currentPlayerImage.sprite = lukeImage;
                currentPlayerName.text = "Luke";
            }
            else if(StateVariableController.player3Select)
            {
                currentPlayerImage.sprite = hannahImage;
                currentPlayerName.text = "Hannah";
            }
        }
    }
}
