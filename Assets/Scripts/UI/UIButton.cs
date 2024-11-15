using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public void MenuPlayButton() 
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Playing);
    }
    public void MenuOptionsButton() 
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Options);
    }
    public void UndefinedButton()
    {
        Debug.Log("Button not implemented yet");
    }
}
