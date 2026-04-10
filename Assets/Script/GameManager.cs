using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int selectedSeed = 1;
    public SeedUI seedUI;

    public Vector2 mouseWorldPosition;

    public Pot[] pots;

    public void OnSeed1(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        selectedSeed = 1;
        seedUI.UpdateSelection(selectedSeed);
    }

    public void OnSeed2(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        selectedSeed = 2;
        seedUI.UpdateSelection(selectedSeed);
    }

    public void OnSeed3(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        selectedSeed = 3;
        seedUI.UpdateSelection(selectedSeed);
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = context.ReadValue<Vector2>();
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
    }

    public void OnWater(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        for (int i = 0; i < pots.Length; i++)
        {
            pots[i].WaterPlant();
        }
    }
}