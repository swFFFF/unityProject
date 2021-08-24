using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionView : ViewBase
{
    public Slider music;
    public Slider sound;

    private void Awake()
    {
        music.value = PlayerPrefs.GetFloat(AudioManager.key_music, 0.5f);
        sound.value = PlayerPrefs.GetFloat(AudioManager.key_sound, 0.5f);
    }

    public void OnSliderMusicChange(float f)
    {
        PlayerPrefs.SetFloat(AudioManager.key_music, f);
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.UpdateVolume();
        }
    }

    public void OnSliderSoundChange(float f)
    {
        PlayerPrefs.SetFloat(AudioManager.key_sound, f);
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.UpdateVolume();
        }
    }
}
