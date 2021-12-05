using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditVolumeScript : MonoBehaviour
{
    public Slider slider;

    public void Volume()
    {
        AudioListener.volume = slider.value;
    }

    private void Start()
    {
        slider.value = AudioListener.volume;
    }
}
