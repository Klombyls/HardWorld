using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonScripts : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("New game");
    }

    public void LoadGame()
    {
        Debug.Log("Load game");
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}