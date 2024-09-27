using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class GameState_Loading : IGameState
{
    public void EnterState(GameStateManager gameStateManager)
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
      //  gameStateManager._uIManager.LoadingScreenUI();
        gameStateManager._cameraManager.isCameraMoveEnabled = false;
    }

    public void FixedUpdateState(GameStateManager gameStateManager)
    {  
    

    }

    public void UpdateState(GameStateManager gameStateManager)
    {


    }

    public void LateUpdateState(GameStateManager gameStateManager) 
    { 

    
    }

    public void ExitState(GameStateManager gameStateManager)
    {
        Time.timeScale = 1f;
        //gameStateManager._cameraManager.EnableCameraRotation();
    }
}
