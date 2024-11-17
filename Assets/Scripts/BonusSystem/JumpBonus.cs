using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBonus : MonoBehaviour
{
    public float targetHeight = 5f; // Height added
    public int boostJumps = 5;      // Count limit for jump

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterControl playerMovement = other.GetComponent<CharacterControl>();
            if (playerMovement != null)
            {
                playerMovement.jumpHeight.BoostJumpHeight(targetHeight, boostJumps);
            }
            Destroy(gameObject); // delete the object after picking up
        }
    }
}
