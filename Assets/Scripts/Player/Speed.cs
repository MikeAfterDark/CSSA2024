using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Speed : MonoBehaviour
{
    [SerializeField]
    private float normalMoveSpeed = 2.0f;
    private float moveSpeed = 2.0f;

    private void Start()
    {
        moveSpeed = LoopManager.playerSpeed;
    }

    public float GetPlayerSpeed(){
        return moveSpeed;
    }

    public void SetPlayerSpeed(float speed){
        this.moveSpeed = speed;
    }

    public void Boost(float speed){
        this.moveSpeed += speed;
    }
}