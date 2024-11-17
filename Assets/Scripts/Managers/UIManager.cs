using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
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
        // Subscribe to the GameManager event
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe from the GameManager event
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameManager.GameState newState)
    {
        Debug.Log(newState);

        // Update UI based on new game state
        switch (newState)
        {
            case GameManager.GameState.MainMenu:
                ShowMainMenu();
                break;
            case GameManager.GameState.Playing:
                ShowGameplay();
                break;
            case GameManager.GameState.Looping:
                GameManager.Instance.UpdateGameState(GameManager.GameState.Playing);
                break;
            case GameManager.GameState.Options:
                ShowOptions();
                break;
            case GameManager.GameState.Paused:
                ShowPauseMenu();
                break;
            case GameManager.GameState.GameOver:
                ShowGameOverScreen();
                break;
        }
    }

    private async void ShowMainMenu() 
    {
        Debug.Log("trying to load main menu");
        
        await LoadSceneAsync("Loading", LoadSceneMode.Additive);
        await UnloadAllScenesExcept("Manager", "Loading");
        await LoadSceneAsync("Menu", LoadSceneMode.Additive);
        await UnloadSceneAsync("Loading");
    }
    private async void ShowGameplay() 
    {
        await LoadSceneAsync("Loading", LoadSceneMode.Additive);
        await UnloadAllScenesExcept("Manager", "Loading");
        await LoadSceneAsync("Game", LoadSceneMode.Additive);
        await UnloadSceneAsync("Loading");
    }
    private async void ShowOptions() 
    {
        await LoadSceneAsync("Loading", LoadSceneMode.Additive);
        await UnloadAllScenesExcept("Manager", "Loading");
        await LoadSceneAsync("Options", LoadSceneMode.Additive);
        await UnloadSceneAsync("Loading");
    }
    private void ShowPauseMenu() { /* UI code here */ }
    private void ShowGameOverScreen() { /* UI code here */ }


    //==================================
    // Helper scene management functions
    // TODO: handle the case of 'infinite loading' where something goes wrong, set timeout ~15s?
    //==================================
    private async Task LoadSceneAsync(string sceneName, LoadSceneMode mode)
    {
        var loadOperation = SceneManager.LoadSceneAsync(sceneName, mode);
        while (!loadOperation.isDone)
        {
            await Task.Yield();
        }
    }

    private async Task UnloadSceneAsync(string sceneName)
    {
        var unloadOperation = SceneManager.UnloadSceneAsync(sceneName);
        while (unloadOperation != null && !unloadOperation.isDone)
        {
            await Task.Yield();
        }
    }

    private async Task UnloadAllScenesExcept(params string[] scenesToKeep)
    {
        List<Task> unloadTasks = new List<Task>();

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (System.Array.IndexOf(scenesToKeep, scene.name) < 0)
            {
                unloadTasks.Add(UnloadSceneAsync(scene.name));
            }
        }

        await Task.WhenAll(unloadTasks);
    }
}


