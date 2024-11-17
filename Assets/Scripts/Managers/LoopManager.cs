using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    private static int loopCounter;
    public static LoopManager Instance;
    public GameObject myMobPrefab;
    public GameObject myItemPrefab;
    //Todo: separate spawn manager?
    //array of interactable object
    //array of enemies

    // Start is called before the first frame update
    void Start()
    {
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
            //spawn here
            SpawnDistractionMob(scene);
            SpawnPatrolGhost(scene);
            SpawnItems(scene);
        }
    }

    //These mobs are in a room to distract player
    private void SpawnDistractionMob(Scene scene)
    {
        GameObject gameObject = Instantiate(myMobPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(0f, 10.0f)), Quaternion.identity);
        SceneManager.MoveGameObjectToScene(gameObject, scene);
    }

    private void SpawnPatrolGhost(Scene scene)
    {

    }

    private void SpawnItems(Scene scene)
    {
        GameObject gameObject = Instantiate(myItemPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(0f, 10.0f)), Quaternion.identity);
        SceneManager.MoveGameObjectToScene(gameObject, scene);
    }
    //Spawn 
}
