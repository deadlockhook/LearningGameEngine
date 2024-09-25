using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class LevelManager : MonoBehaviour
{
    [Header("Script References")]
    public GameStateManager _gameStateManager;
    public CameraManager _cameraManager;
    public GameManager _gameManager;
    public PlayerManager _playerManager;
    public UIManager _uIManager;

    
    public int nextScene;

    private AsyncOperation loadingOperation;
    private IGameState stateToChangeAfterSceneLoad;

    public void Awake()
    {
        // Check for missing script references
        if (_gameStateManager == null) { Debug.LogError("LevelManager is not assigned to LevelManager in the inspector!"); }
        if (_cameraManager == null) { Debug.LogError("CameraManager is not assigned to LevelManager in the inspector!"); }
        if (_gameManager == null) { Debug.LogError("GameManager is not assigned to LevelManager in the inspector!"); }
        if (_playerManager == null) { Debug.LogError("PlayerManager is not assigned to LevelManager in the inspector!"); }
        if (_uIManager == null) { Debug.LogError("UIManager is not assigned to LevelManager in the inspector!"); }
    }

    void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void LoadNextlevel()
    {
        nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        LoadScene(nextScene);
        _gameStateManager.SwitchToState(_gameStateManager.gameState_GamePlay);       
    }

    public void LoadMainMenuScene()
    {
        LoadScene(0);
        _gameStateManager.SwitchToState(_gameStateManager.gameState_GameInit);
    }

    public void LoadSceneAsyncEx(string sceneName)
    {
        _gameStateManager.SwitchToState(_gameStateManager.gameState_Loading);

        switch (sceneName)
        {
            case "MainMenu":
                stateToChangeAfterSceneLoad = _gameStateManager.gameState_MainMenu;
                break;

            case "TestLevel":
                stateToChangeAfterSceneLoad = _gameStateManager.gameState_GamePlay;
                break;

            default:
                sceneName = "MainMenu";
                stateToChangeAfterSceneLoad = _gameStateManager.gameState_MainMenu;
                break;
        }

        StartSceneLoad(sceneName);
    }

    public bool IsLevelLoadFinished()
    {
        return loadingOperation.isDone;
    }

    public float GetLoadingProgress()
    {
        return loadingOperation.progress;
    }

    private void SceneLoadFinishCallback(AsyncOperation operation)
    {
        _gameStateManager.SwitchToState(stateToChangeAfterSceneLoad);
        loadingOperation.completed -= SceneLoadFinishCallback;
    }

    private void StartSceneLoad(string sceneName)
    {
        loadingOperation = SceneManager.LoadSceneAsync(name);
        loadingOperation.completed += SceneLoadFinishCallback;
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
        _gameStateManager.SwitchToState(_gameStateManager.gameState_GamePlay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}



