using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpHeight : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame update
    private float DefaultJumpHeight = 5f;
    private float JumpHeight;
    private int boostCount;

    void Start()
    {
        JumpHeight = DefaultJumpHeight;
        boostCount = 0;
    }

    public float getJumpHeight(){
        return this.JumpHeight;
    }
    
    public void resetJumpHeight(){
        this.JumpHeight = DefaultJumpHeight;
    }

    public void boostJumpHeight(float jumpHeight, int addCount){
        //add jump boost in height
        this.JumpHeight += jumpHeight;
        boostCount += addCount;
    }
                
    public int useBoost(){
        //Detect if there is boost left.
        if(boostCount > 0){
            //If there are, -1
            boostCount --;
        }
        //reset the jump height if run out of count
        if(boostCount <= 0){
            resetJumpHeight();
        }
        return boostCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
