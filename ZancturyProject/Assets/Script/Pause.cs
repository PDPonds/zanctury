using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject PauseMenu;
    playerInventory _playerInventory;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "LootMap")
        {
            _playerInventory = GameObject.Find("Player").GetComponent<playerInventory>();
        }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Zanctuary")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();

                }
                else
                {
                    PauseGame();
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "LootMap")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_playerInventory.inventoryUI.activeSelf &&
                    !_playerInventory.RepairGenaratorPanel.activeSelf &&
                    !_playerInventory.RadioRepairPanel.activeSelf &&
                    !_playerInventory.UsePliersPanel.activeSelf &&
                    !_playerInventory.UseKeyCard.activeSelf &&
                    !_playerInventory.interacRadio.activeSelf &&
                    !_playerInventory.questPanel.activeSelf)
                {
                    if (GameIsPaused)
                    {
                        Resume();
                    }
                    else
                    {
                        PauseGame();
                    }
                }
                else if (_playerInventory.inventoryUI.activeSelf ||
                    _playerInventory.RepairGenaratorPanel.activeSelf ||
                    _playerInventory.RadioRepairPanel.activeSelf ||
                    _playerInventory.UsePliersPanel.activeSelf ||
                    _playerInventory.UseKeyCard.activeSelf ||
                    _playerInventory.interacRadio.activeSelf ||
                    _playerInventory.questPanel.activeSelf)
                {
                    _playerInventory.CloseInventoryAndLootBox();
                }
            }

        }
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        GameIsPaused = false;
        if (SceneManager.GetActiveScene().name == "Zanctuary")
        {
            ZanctuaryManager.Instance.Play("Campfire");
        }
        else if (SceneManager.GetActiveScene().name == "LootMap")
        {
            AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audios)
            {
                if (a.loop == false)
                {
                    a.Play();
                }
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        GameIsPaused = true;
        if (SceneManager.GetActiveScene().name == "Zanctuary")
        {
            ZanctuaryManager.Instance.Stop("Campfire");
        }
        else if (SceneManager.GetActiveScene().name == "LootMap")
        {
            AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audios)
            {
                if (a.loop == false)
                {
                    a.Pause();
                }
            }
        }
    }

}
