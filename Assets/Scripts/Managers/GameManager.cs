using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnGameStateChanged;
    public static event Action<PlayerEvent> OnNewPlayerEventTriggered;

    public enum GameState
    {
        MainMenu,
        Playing,
        Options,
        Paused,
        GameOver,
    }

    public enum PlayerEvent
    {
        Jump,
        Sing,
        Impact,
    }

    private GameState currentGameState;

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
        
        UpdateGameState(GameState.MainMenu);
    }

    /// <summary>
    /// Updates the game state only if the new state is different from the current one.
    /// This method triggers state change events only the first time a unique state is set, 
    /// ensuring that duplicate updates are ignored. 
    /// To re-trigger the same state, it must be changed to a different state first.
    /// </summary>
    /// <param name="newGameState">The desired game state to update to.</param>
    public void UpdateGameState(GameState newGameState)
    {
        if (currentGameState == newGameState) return;
        
        currentGameState = newGameState;

        // Trigger the event and notify listeners
        OnGameStateChanged?.Invoke(currentGameState);
    }
    public void NewPlayerEvent(PlayerEvent newPlayerEvent)
    {
        //Handle some player event before others react to it

        // Trigger the event and notify listeners
        OnNewPlayerEventTriggered?.Invoke(newPlayerEvent);
    }
}
