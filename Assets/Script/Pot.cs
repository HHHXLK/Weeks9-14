using UnityEngine;
using UnityEngine.InputSystem;

public class Pot : MonoBehaviour
{
    public SpriteRenderer potRenderer;
    public GameManager gameManager;

    public GameObject emptyPot;
    public GameObject filledPot;

    public GameObject molePlant;
    public GameObject cactusPlant;
    public GameObject carrotPlant;

    Vector3 normalScale;
    Vector3 hoverScale;

    bool isHovered = false;
    bool isEmpty = true;

    void Start()
    {
        normalScale = transform.localScale;
        hoverScale = normalScale * 1.1f;
    }

    void Update()
    {
        CheckHover();
        CheckClick();
    }

    void CheckHover()
    {
        if (!isEmpty)
        {
            transform.localScale = normalScale;
            isHovered = false;
            return;
        }

        Vector2 mousePos = gameManager.mouseWorldPosition;

        if (potRenderer.bounds.Contains(mousePos))
        {
            if (!isHovered)
            {
                isHovered = true;
                transform.localScale = hoverScale;
            }
        }
        else
        {
            if (isHovered)
            {
                isHovered = false;
                transform.localScale = normalScale;
            }
        }
    }

    void CheckClick()
    {
        if (!isEmpty) return;

        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector2 mousePos = gameManager.mouseWorldPosition;

        if (potRenderer.bounds.Contains(mousePos))
        {
            PlantSelectedSeed();
        }
    }

    void PlantSelectedSeed()
    {
        isEmpty = false;

        emptyPot.SetActive(false);
        filledPot.SetActive(true);

        molePlant.SetActive(false);
        cactusPlant.SetActive(false);
        carrotPlant.SetActive(false);

        if (gameManager.selectedSeed == 1)
        {
            cactusPlant.SetActive(true);
        }
        else if (gameManager.selectedSeed == 2)
        {
            carrotPlant.SetActive(true);
        }
        else if (gameManager.selectedSeed == 3)
        {
            molePlant.SetActive(true);
        }

        transform.localScale = normalScale;
    }
}