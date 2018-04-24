using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour {

    public GameObject hud, pause;
    public TextMeshProUGUI tmp;

    bool dead = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (hud.activeSelf) {
                PauseGame();
            }
            else if (pause.activeSelf && !dead) {
                UnPauseGame();
            }
        }
    }

    public void MainMenu() {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void PauseGame() {
        Time.timeScale = 0;
        hud.SetActive(false);
        pause.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnPauseGame() {
        Time.timeScale = 1;
        hud.SetActive(true);
        pause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseDead() {
        tmp.text = "You're dead!";
        Time.timeScale = 0;
        hud.SetActive(false);
        pause.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        dead = true;
    }
}
