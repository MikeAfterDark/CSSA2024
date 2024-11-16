using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class SpeedBonus : MonoBehaviour
{

    public float boostAmount = 2f; 
    public float boostDuration = 5f;

    private void CollisonDetect(Collider other)
    {
        if (other.CompareTag("Player")) // make sure it is player
        {
            CharacterControl playerMovement = other.GetComponent<CharacterControl>();
            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
            }
            Destroy(gameObject); // delete the object after picking up
        }
    }

    public IEnumerator ApplySpeedBoost(CharacterControl playerMovement)
    {
        float originalSpeed = playerMovement.playerSpeed.getPlayerSpeed(); // Save origin speed
        playerMovement.playerSpeed.Boost(boostAmount); // increase speed

        yield return new WaitForSeconds(boostDuration); // wait some time

        playerMovement.playerSpeed.setPlayerSpeed (originalSpeed); // recover to origin speed
    }

  
}

