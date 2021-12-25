using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditVolumeScript : MonoBehaviour
{
    public Slider slider;

    public void Volume()
    {
        PlayerPrefs.SetFloat("Volume", slider.value);
        Debug.Log(slider.value);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
            slider.value = PlayerPrefs.GetFloat("Volume");
    }
}
