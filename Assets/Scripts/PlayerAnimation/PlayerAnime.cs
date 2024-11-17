using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {   
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && Input.GetKeyDown(KeyCode.W)){
            anim.Play("RUN00_F", -1, 0f);
        }
        else if(Input.GetKeyDown(KeyCode.W)){
            anim.Play("RUN00_F", -1, 0f);
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            anim.Play("RUN00_L", -1, 0f);
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            anim.Play("WALK00_B", -1, 0f);
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            anim.Play("RUN00_R", -1, 0f);
        }
        else if(Input.GetKeyDown(KeyCode.Space)){
            anim.Play("JUMP00B", -1, 0f);
        }
    }
}
