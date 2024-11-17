using UnityEngine;

public class MouseCameraControl : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform player; 
    
    public Vector2 turn;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen.
    }

    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        turn.y += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0f); // Rotate camera up and down.
        player.rotation = Quaternion.LookRotation(transform.forward, transform.up); // Rotate player body left and right.
    }
}
