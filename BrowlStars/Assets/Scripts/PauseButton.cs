using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public bool pauseGame;
    public GameObject pauseGameMenu;

    private void Start()
    {
        pauseGameMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame)
            {
                Resum();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resum()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseGame = false;

    }
    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0;
        pauseGame = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
