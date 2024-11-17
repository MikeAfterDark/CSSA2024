using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;               // Movement speed of the enemy
    public Transform player;

    public float interval = 1f;
    private float currentTime;              // Reference to the player object
    public float chaseRange = 10f;         // Range within which the enemy will chase the player

    private Vector3 initialPosition;
    private Vector3 targetPosition;        // Target position for the enemy to move towards

    private void Start()
    {
        currentTime = 3; 
        initialPosition = transform.position;
    }
    void Update()
    {
        // Check distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            currentTime -= Time.deltaTime;
            // If within chase range, move the enemy towards the player
            ChasePlayer();
        }

        if (currentTime <= 0)
        {
            transform.position = initialPosition;
        }
    }

    // Move the enemy towards the player
    void ChasePlayer()
    {
        // Move the enemy towards the player position using Transform
        Vector3 direction = (player.position - transform.position).normalized; // Direction vector to player
        transform.position += direction * speed * Time.deltaTime; // Move towards player
    }
}
