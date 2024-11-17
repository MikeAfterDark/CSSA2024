using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private CharacterController controller;
    private Health health;
    private Vector3 playerVelocity;
    private float verticalVelocity;
    private float groundedTimer;  
    //I am adding a Speed class for player to better control the speed bonus
    //private float playerSpeed = 2.0f;
    public Speed playerSpeed;

    //private float jumpHeight = 10f;
    public jumpHeight jumpHeight;
    private float gravityValue = 9.81f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        health = GetComponent<Health>();
        playerSpeed = GetComponent<Speed>();
        jumpHeight = GetComponent<jumpHeight>();
    }

    // Update is called once per frame
    void Update()
    {
        bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            // cooldown interval to allow reliable jumping even whem coming down ramps
            groundedTimer = 0.2f;
        }
        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }
        // slam into the ground
        if (groundedPlayer && verticalVelocity < 0)
        {
            // hit ground
            verticalVelocity = 0f;
        }
        // apply gravity always, to let us track down ramps properly
        verticalVelocity -= gravityValue * Time.deltaTime;

        // gather lateral input control
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        // scale by speed
        move *= playerSpeed.getPlayerSpeed();
        // only align to motion if we are providing enough input
        if (move.magnitude > 0.05f)
        {
            gameObject.transform.forward = move;
        }

        // allow jump as long as the p  layer is on the ground
        if (Input.GetButtonDown("Jump"))
        {
            // must have been grounded recently to allow jump
            if (groundedTimer > 0)
            {
                // no more until we recontact ground
                groundedTimer = 0;
                //Detect if there is boost left.
                //If there are, -1 count, number bonus is already excuted when eating buffs
                //else, reset the jump height
                jumpHeight.useBoost();
                // Physics dynamics formula for calculating jump up velocity based on height and gravity
                verticalVelocity += Mathf.Sqrt(jumpHeight.getJumpHeight() * 2 * gravityValue);
                
                GameManager.Instance.NewPlayerEvent(GameManager.PlayerEvent.Jump); //trigger jump event
            }
        }
        // inject Y velocity before we use it
        move.y = verticalVelocity;

        // call .Move() once only
        controller.Move(move * Time.deltaTime);

        if (Input.GetKeyDown("k"))
        {
            health.Kill();
        }
    }
}
