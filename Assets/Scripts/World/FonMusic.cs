using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonMusic : MonoBehaviour
{
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public float audio1Length;
    public float audio2Length;
    public float audio3Length;
    int currentmusic = 1;
    bool play = true;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            audio1.volume = PlayerPrefs.GetFloat("Volume");
            audio2.volume = PlayerPrefs.GetFloat("Volume");
            audio3.volume = PlayerPrefs.GetFloat("Volume");
        }
    }

    private void Update()
    {
        if (play)
        {
            play = false;
            StartCoroutine(PlayingMusic());
        }
    }
    IEnumerator PlayingMusic()
    {
        if (currentmusic == 1)
        {
            audio1.Play();
            yield return new WaitForSeconds(audio1Length);
            play = true;
            currentmusic = 2;
        }
        else if (currentmusic == 2)
        {
            audio2.Play();
            yield return new WaitForSeconds(audio2Length);
            play = true;
            currentmusic = 3;
        }
        else
        {
            audio3.Play();
            yield return new WaitForSeconds(audio3Length);
            play = true;
            currentmusic = 1;
        }
    }
}
