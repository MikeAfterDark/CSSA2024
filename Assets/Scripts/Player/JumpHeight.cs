using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpHeight : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame update
    private float defaultJumpHeight = 5f;
    private float height;
    private int boostCount;

    void Start()
    {
        height = defaultJumpHeight;
        boostCount = 0;
    }

    public float getJumpHeight(){
        return this.height;
    }
    
    public void ResetJumpHeight(){
        this.height = defaultJumpHeight;
    }

    public void BoostJumpHeight(float jumpHeight, int addCount){
        //add jump boost in height
        this.height += jumpHeight;
        boostCount += addCount;
    }
                
    public int UseBoost(){
        //Detect if there is boost left.
        if(boostCount > 0){
            //If there are, -1
            boostCount --;
        }
        //reset the jump height if run out of count
        if(boostCount <= 0){
            ResetJumpHeight();
        }
        return boostCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
