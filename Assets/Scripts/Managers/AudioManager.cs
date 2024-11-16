using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private Dictionary<string, AudioClip> audioClipCache = new Dictionary<string, AudioClip>();
    private Dictionary<string, GameObject> loopingAudioObjects = new Dictionary<string, GameObject>();
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
        GameManager.OnNewPlayerEventTriggered += HandleNewPlayerEvent;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
        GameManager.OnNewPlayerEventTriggered -= HandleNewPlayerEvent;
    }

    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Playing:
                PlayAudio("DipteraSonata", true); 
                break;
            case GameManager.GameState.Paused:
            case GameManager.GameState.GameOver:
                StopLoopingAudio("DipteraSonata");
                break;
        }
    }
    private void HandleNewPlayerEvent(GameManager.PlayerEvent newState)
    {
        switch (newState)
        {
            case GameManager.PlayerEvent.Jump:
                PlayAudio("jump");
                break;
        }
    }

    public void PlayAudio(string clipName, bool loop = false)
    {
        AudioClip clip = GetAudioClip(clipName);
        if (clip == null)
        {
            Debug.LogError($"Audio clip '{clipName}' not found in Assets/Resources.");
            return;
        }

        if (loop)
        {
            StopLoopingAudio(clipName);
        }

        GameObject audioObject = SpawnAudioObject(clipName, clip, loop);
        if (loop)
        {
            loopingAudioObjects[clipName] = audioObject;
        }
    }

    public void StopLoopingAudio(string clipName)
    {
        if (loopingAudioObjects.TryGetValue(clipName, out GameObject audioObject))
        {
            Destroy(audioObject);
            loopingAudioObjects.Remove(clipName);
        }
    }

    // Get audio clip (loads and caches if not already loaded)
    private AudioClip GetAudioClip(string clipName)
    {
        if (!audioClipCache.ContainsKey(clipName))
        {
            AudioClip clip = Resources.Load<AudioClip>($"Audio/Music/{clipName}") 
                 ?? Resources.Load<AudioClip>($"Audio/SFX/{clipName}");

            if (clip != null)
            {
                audioClipCache[clipName] = clip;
            }
        }

        audioClipCache.TryGetValue(clipName, out AudioClip cachedClip);
        return cachedClip;
    }

    // Utility function to create and play an audio object
    private GameObject SpawnAudioObject(string name, AudioClip clip, bool loop)
    {
        GameObject audioObject = new GameObject(name);
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.playOnAwake = false;

        audioSource.Play();

        if (!loop)
        {
            Destroy(audioObject, clip.length);
        }

        return audioObject;
    }
}