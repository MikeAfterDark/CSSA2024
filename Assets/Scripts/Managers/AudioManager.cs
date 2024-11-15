using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
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
                PlayBackgroundMusic();
                break;
            case GameManager.GameState.Paused:
            case GameManager.GameState.GameOver:
                StopBackgroundMusic();
                break;
        }
    }
    private void HandleNewPlayerEvent(GameManager.PlayerEvent newState)
    {
        switch (newState)
        {
            case GameManager.PlayerEvent.Jump:
                PlayJumpAudio();
                break;
            case GameManager.PlayerEvent.Sing:
                PlaySingAudio();
                break;
            case GameManager.PlayerEvent.Impact:
                PlayImpactAudio();
                break;
        }
    }

    // Game State
    private void PlayBackgroundMusic() { /* Audio code here */ }
    private void StopBackgroundMusic() { /* Audio code here */ }

    // Player Events
    private void PlayJumpAudio() { /* Audio code here */ }
    private void PlaySingAudio() { /* Audio code here */ }
    private void PlayImpactAudio() { /* Audio code here */ }
}
