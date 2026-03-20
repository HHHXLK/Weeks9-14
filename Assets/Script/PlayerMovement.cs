using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveInput;

    private Vector2 lookInput;

    // Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.action.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.action.ReadValue<Vector2>();
    }

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0);
        transform.position += move * speed * Time.deltaTime;

        if (lookInput != Vector2.zero)
        {
            transform.right = new Vector3(lookInput.x, lookInput.y, 0);
        }
    }
}