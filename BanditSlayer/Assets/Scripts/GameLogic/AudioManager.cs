using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    public List<AudioClip> clips;
    public List<string> keys;

    private Dictionary<string, AudioClip> audioDictionary;
    private AudioSource audioSource;
    private AudioSource musicAudioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        musicAudioSource = gameObject.AddComponent<AudioSource>();
        audioDictionary = new Dictionary<string, AudioClip>();

        for (int i = 0; i < Mathf.Min(clips.Count, keys.Count); i++)
        {
            audioDictionary.Add(keys[i], clips[i]);
        }
        
        PlayMusic("Background");
    }

    public void Play(string key)
    {
        if (audioDictionary.ContainsKey(key))
        {
            audioSource.clip = audioDictionary[key];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not found for key: " + key);
        }
    }
    
    public void PlayMusic(string key)
    {
        if (audioDictionary.ContainsKey(key))
        {
            musicAudioSource.clip = audioDictionary[key];
            musicAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not found for key: " + key);
        }
    }
    
    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = Mathf.Clamp01(volume);
    }

    public void SetFXVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }
}
