using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class SpeedBonus : MonoBehaviour
{

    public float boostAmount = 10f; 
    public float boostDuration = 5f;

    private void OnTriggerEnter(Collider other)
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
        float originalSpeed = playerMovement.playerSpeed.GetPlayerSpeed(); // Save origin speed
        playerMovement.playerSpeed.Boost(boostAmount); // increase speed

        yield return new WaitForSeconds(boostDuration); // wait some time

        playerMovement.playerSpeed.SetPlayerSpeed (originalSpeed); // recover to origin speed
    }

    public void Start(){

    }

    public void Update(){

    }

  
}

