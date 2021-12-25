using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    string path;
    public bool pauseActive;
    void Start()
    {
        pause.SetActive(false);
        pauseActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseActive)
            {
                pause.SetActive(true);
                pauseActive = true;
                Time.timeScale = 0;
            }
            else
            {
                pause.SetActive(false);
                pauseActive = false;
                Time.timeScale = 1;
            }
        }
    }

    public void PauseOff()
    {
        pause.SetActive(false);
        pauseActive = false;
        Time.timeScale = 1;
    }

    public void SaveAndExit()
    {
        Time.timeScale = 1;
        SaveGame sg = new SaveGame();
        sg.Savegame();
        SceneManager.LoadScene("Menu");
    }
}
