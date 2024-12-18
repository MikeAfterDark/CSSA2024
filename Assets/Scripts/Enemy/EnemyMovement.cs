using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;               // Movement speed of the enemy
    private Transform player;

    public float interval = 1f;
    public float chasingTime = 3f;              // Time enemy chasing the player then coming back
    private float chasingTimeReset;
    public float chaseRange = 10f;         // Range within which the enemy will chase the player

    private Vector3 targetPosition;        // Target position for the enemy to move towards

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        chasingTimeReset = chasingTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set the color of the gizmo
        Gizmos.DrawWireSphere(transform.position, chaseRange); // Draw a wire sphere around the object
    }
    void Update()
    {
        // Check distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            // If within chase range, move the enemy towards the player
            ChasePlayer();
        }
        else
        {
            chasingTimeReset -= Time.deltaTime;
        }

        if (chasingTimeReset <= 0)
        {
            chasingTimeReset = chasingTime;
        }
        
        //kill player
        if(distanceToPlayer <= 8){
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.Damage(20);
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