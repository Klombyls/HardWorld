using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteSaveMenuScript : MonoBehaviour
{
    public void OpenDeleteSaveMenu()
    {
        SceneManager.LoadScene("DeleteSaveMenu");
    }
}
