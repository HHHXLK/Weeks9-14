using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // stores which seed is currently selected
    public int selectedSeed = 1;

    // reference to seed UI script
    public SeedUI seedUI;

    // stores mouse position in world space
    public Vector2 mouseWorldPosition;

    // references to all pots in the scene
    public Pot[] pots;

    // reference to shovel UI feedback
    public ShovelUI shovelUI;

    // called when player presses key 1
    public void OnSeed1(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        selectedSeed = 1;
        seedUI.UpdateSelection(selectedSeed);
    }

    // called when player presses key 2
    public void OnSeed2(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        selectedSeed = 2;
        seedUI.UpdateSelection(selectedSeed);
    }

    // called when player presses key 3
    public void OnSeed3(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        selectedSeed = 3;
        seedUI.UpdateSelection(selectedSeed);
    }

    // updates mouse position from screen space to world space
    public void OnPoint(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = context.ReadValue<Vector2>();
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
    }

    // called when player clicks to plant a seed
    public void OnPlantClick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        for (int i = 0; i < pots.Length; i++)
        {
            pots[i].TryPlant();
        }
    }

    // called when player presses Q to water all planted pots
    public void OnWater(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        for (int i = 0; i < pots.Length; i++)
        {
            pots[i].WaterPlant();
        }
    }

    // called when player presses R to reset everything
    public void OnReset(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        for (int i = 0; i < pots.Length; i++)
        {
            pots[i].ResetPot();
        }

        // play shovel UI feedback if reference exists
        if (shovelUI != null)
        {
            shovelUI.PlayResetFeedback();
        }
    }
}