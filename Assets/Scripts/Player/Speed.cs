using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Speed : MonoBehaviour
{
    [SerializeField]
    private float normalMoveSpeed = 2.0f;
    private float moveSpeed = 0;

    private void Start()
    {
        moveSpeed = normalMoveSpeed;
    }

    public float getPlayerSpeed(){
        return moveSpeed;
    }

    public void setPlayerSpeed(float speed){
        this.moveSpeed = speed;
    }

    public void Boost(float speed){
        this.moveSpeed += speed;
    }
}