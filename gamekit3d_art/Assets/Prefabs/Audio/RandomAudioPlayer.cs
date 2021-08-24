using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public Material[] materials;
    public AudioClip[] clips;
}

[RequireComponent(typeof(AudioSource))]
public class RandomAudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] defaultClips;
    public Sound[] sounds;

    private void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();
        if(!audioSource)
        {
            throw new System.Exception("未检测到AudioSource");
        }
    }

    public void PlayRandomAudio()
    {
        PlayRandomAudio(defaultClips);
    }

    public void PlayRandomAudio(AudioClip[] clips)
    {
        int indexer = Random.Range(0, clips.Length);
        audioSource.clip = clips[indexer];
        audioSource.Play();
    }

    public void PlayRandomAudio(Material material)
    {
        if(material == null)
        {
            return;
        }

        for(int i = 0; i < sounds.Length; i++)
        {
            for(int j = 0; j < sounds[i].materials.Length; j++)
            {
                if(material == sounds[i].materials[j])
                {
                    //播放音频
                    PlayRandomAudio(sounds[i].clips);
                    return;
                }
            }
        }
    }
}
