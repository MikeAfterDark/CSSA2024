using UnityEngine;

public class MouseCameraControl : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform player; 
    
    public Vector2 turn;

     Vector3 playerLook;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen.
    }

    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        turn.y += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        turn.y = Mathf.Clamp(turn.y, -90f, 0f);

        playerLook = new Vector3(-turn.y, 0f, turn.x);

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0f); // Rotate camera up and down.
        player.rotation = Quaternion.Euler(0f, turn.x, 0f);
        //player.rotation = Quaternion.LookRotation(playerLook,transform.up); // Rotate player body left and right.4
        // player.Rotate(Vector3.up * turn.x);
    }
}
