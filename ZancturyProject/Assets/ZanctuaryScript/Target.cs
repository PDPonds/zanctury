using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject myUI;
    public GameObject questUI;

    Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
    }
    private void OnMouseEnter()
    {
        if (Pause.GameIsPaused == false)
        {
            outline.OutlineWidth = 2f;
        }
    }

    private void OnMouseExit()
    {
        outline.OutlineWidth = 0f;
    }
}
