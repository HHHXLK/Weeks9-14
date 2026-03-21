using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color[] colors;

    private int index = 0;

    void Start()
    {
        if (colors.Length > 0)
        {
            spriteRenderer.color = colors[0];
        }
    }

    public void ChangeColor(InputAction.CallbackContext context)
    {
        Debug.Log("ChangeColor called");

        if (!context.started) return;

        index++;

        if (index >= colors.Length)
        {
            index = 0;
        }

        spriteRenderer.color = colors[index];
    }
}