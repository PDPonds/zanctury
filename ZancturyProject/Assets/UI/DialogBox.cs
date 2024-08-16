using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    public Transform box;
    public CanvasGroup bg;
    public Transform text;

    private void OnEnable()
    {
        bg.alpha = 0;
        bg.LeanAlpha(1, 0.2f);

        box.localPosition = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 0.5f).setEaseInOutExpo().delay = 0.1f;
        text.localPosition = new Vector2(0, -Screen.height);
        text.LeanMoveLocalY(60, 0.5f).setEaseInOutExpo().delay = 0.1f;
    }

    public void CloseBox()
    {
        bg.LeanAlpha(0, 5f);
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseOutExpo();
        text.LeanMoveLocalY(-Screen.height, 0.5f).setEaseOutExpo();
    }
}
