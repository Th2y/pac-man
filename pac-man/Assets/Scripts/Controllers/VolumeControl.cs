using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    float volumeEffects, volumeMusics;
    public Slider sliderEffects, sliderMusics;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Effects"))
            sliderEffects.value = 1;
        else
            sliderEffects.value = PlayerPrefs.GetFloat("Effects");

        if (!PlayerPrefs.HasKey("Musicas"))
            sliderMusics.value = 1;
        else
            sliderMusics.value = PlayerPrefs.GetFloat("Musics");
    }

    public void VolumeEffects(float volume)
    {
        volumeEffects = volume;
        GameObject[] effect = GameObject.FindGameObjectsWithTag("Effects");
        if (effect.Length > 0)
        {
            for (int i = 0; i < effect.Length; i++)
            {
                effect[i].GetComponent<AudioSource>().volume = volumeEffects;
            }
        }

        PlayerPrefs.SetFloat("Effects", volumeEffects);
    }

    public void VolumeMusicas(float volume)
    {
        volumeMusics = volume;
        GameObject[] music = GameObject.FindGameObjectsWithTag("Musics");
        if (music.Length > 0)
        {
            for (int i = 0; i < music.Length; i++)
            {
                music[i].GetComponent<AudioSource>().volume = volumeMusics;
            }
        }

        PlayerPrefs.SetFloat("Musics", volumeMusics);
    }
}
