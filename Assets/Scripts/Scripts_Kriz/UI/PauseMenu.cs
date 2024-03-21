using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool menuActivated;
    public GameObject Inventory, Hud;

    public static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    public PlayerMovement PlayerMove;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && menuActivated)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenuUI.SetActive(false);
            menuActivated = false;
            Inventory.SetActive(true);
            Hud.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && !menuActivated)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            pauseMenuUI.SetActive(true);
            menuActivated = true;
            Hud.SetActive(false);
            Inventory.SetActive(false);
        }
    }

    public void Home()
    {
        SceneManager.LoadScene("MASTER_Start");
    }
}