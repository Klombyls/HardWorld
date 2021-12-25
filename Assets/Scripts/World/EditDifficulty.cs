using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditDifficulty : MonoBehaviour
{
    LoadWorld myWorld;
    public GameObject difficultCounter;

    private void Start()
    {
        myWorld = GameObject.Find("World").GetComponent<LoadWorld>();
    }
    public void Edit()
    {
        if (myWorld.difficult < 10)
            myWorld.difficult++;
        difficultCounter.GetComponent<Text>().text = myWorld.difficult.ToString();
    }
}
