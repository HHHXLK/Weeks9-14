using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    private int index = 0;

    public void ChangeSprite(InputAction.CallbackContext context)
    {
        // Only for press
        if (!context.performed) return;

        index++;
        if (index >= sprites.Length)
        {
            index = 0;
        }

        spriteRenderer.sprite = sprites[index];
    }
}
