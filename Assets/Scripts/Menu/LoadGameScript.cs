using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScript : MonoBehaviour
{
    public void LoadGame()
    {
        string path = Path.Combine(Application.dataPath, "Save.json");
        if (File.Exists(path))
            SceneManager.LoadScene("World");
    }
}
