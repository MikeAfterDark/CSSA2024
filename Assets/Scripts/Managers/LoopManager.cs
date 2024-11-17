using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public static int loopCounter;
    public static LoopManager Instance;
    public GameObject myMobPrefab;
    public GameObject myItemPrefab;
    //Todo: separate spawn manager?
    //array of interactable object
    //array of enemies
    public static float playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 50.0f;
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
        loopCounter = 0;
    }

    private void OnEnable()
    {
        // Subscribe to the GameManager event
        GameManager.OnNewPlayerEventTriggered += HandlePlayerEvent;
        SceneManager.sceneLoaded += OnGameSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the GameManager event
        GameManager.OnNewPlayerEventTriggered -= HandlePlayerEvent;
        SceneManager.sceneLoaded -= OnGameSceneLoaded;
    }

    private void HandlePlayerEvent(GameManager.PlayerEvent newPlayerEvent)
    {
        Debug.Log(newPlayerEvent);

        // Update UI based on new game state
        switch (newPlayerEvent)
        {
            case GameManager.PlayerEvent.Died:
                playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>().playerSpeed.GetPlayerSpeed();
                GameManager.Instance.UpdateGameState(GameManager.GameState.Looping);
                break;
        }
    }

    public void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("On Game Scene Loaded: " + scene.name);
        if (scene.name == "Game")
        {
            loopCounter++;
        }
    }
}
