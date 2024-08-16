using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    Camera cam;
    [SerializeField] GameObject UI1;
    [SerializeField] GameObject UI2;
    [SerializeField] GameObject UI3;
    [SerializeField] GameObject UI4;
    [SerializeField] GameObject UI5;
    [SerializeField] GameObject UI6;
    [SerializeField] GameObject UI7;
    [SerializeField] GameObject UI8;
    void Start()
    {
        cam = gameObject.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (Pause.GameIsPaused == false)
        {
            if (!UI1.activeSelf && !UI2.activeSelf && !UI3.activeSelf &&
            !UI4.activeSelf && !UI5.activeSelf && !UI6.activeSelf &&
            !UI7.activeSelf && !UI8.activeSelf)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject.GetComponent<Target>() != null)
                        {
                            var targetUI = hit.collider.gameObject.GetComponent<Target>();
                            
                            if(hit.collider.CompareTag("Radio"))
                            {
                                if(StateVariableController.radioFixAlready == false)
                                {
                                    showUI(targetUI.myUI);
                                }
                                else
                                {
                                    showUI(targetUI.questUI);
                                }
                            }
                            else
                            {
                                showUI(targetUI.myUI);
                            }

                            if (hit.collider.CompareTag("Carter"))
                            {
                                var CharacterUI = targetUI.myUI.GetComponent<UIZanctuaryNoSlot>();
                                if (CharacterUI != null)
                                {
                                    CharacterUI.currentPlayerSelect = 1;
                                }
                            }
                            if (hit.collider.CompareTag("Luke"))
                            {
                                var CharacterUI = targetUI.myUI.GetComponent<UIZanctuaryNoSlot>();
                                if (CharacterUI != null)
                                {
                                    CharacterUI.currentPlayerSelect = 2;
                                }
                            }
                            if (hit.collider.CompareTag("Hannah"))
                            {
                                var CharacterUI = targetUI.myUI.GetComponent<UIZanctuaryNoSlot>();
                                if (CharacterUI != null)
                                {
                                    CharacterUI.currentPlayerSelect = 3;
                                }
                            }

                        }
                    }
                }
            }
        }
    }
    public void showUI(GameObject _UI)
    {
        ZanctuaryManager.Instance.OpenUISound();
        _UI.SetActive(true);
    }
}
