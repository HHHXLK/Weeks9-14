using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Pot : MonoBehaviour
{
    public SpriteRenderer potRenderer;
    public GameManager gameManager;

    public GameObject emptyPot;
    public GameObject filledPot;

    public GameObject molePlant;
    public GameObject cactusPlant;
    public GameObject carrotPlant;

    public GameObject cactusMoustache;
    public GameObject cactusHat;

    public GameObject carrotWindow;
    public GameObject carrotRabbit;

    public GameObject moleClover;

    Vector3 normalScale;
    Vector3 hoverScale;

    bool isHovered = false;
    bool isEmpty = true;

    int plantedSeed = 0;

    Coroutine growRoutine;

    // cactus growth settings (可以在 inspector 调)
    public float cactusGrowSpeed = 0.1f;
    public float cactusGrowMultiplier = 2f;
    bool hasGrown = false;

    public float carrotGrowSpeed = 0.1f;
    public float carrotGrowMultiplier = 3.5f;

    public float molePopSpeed = 0.5f;
    public float molePopHeight = 2f;


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
        plantedSeed = gameManager.selectedSeed;

        emptyPot.SetActive(false);
        filledPot.SetActive(true);

        molePlant.SetActive(false);
        cactusPlant.SetActive(false);
        carrotPlant.SetActive(false);

        cactusMoustache.SetActive(false);
        cactusHat.SetActive(false);

        carrotWindow.SetActive(false);
        carrotRabbit.SetActive(false);

        moleClover.SetActive(false);

        // 1 cactus
        if (plantedSeed == 1)
        {
            cactusPlant.SetActive(true);
        }
        // 2 carrot
        else if (plantedSeed == 2)
        {
            carrotPlant.SetActive(true);
        }
        // 3 mole
        else if (plantedSeed == 3)
        {
            molePlant.SetActive(true);
        }

        transform.localScale = normalScale;
    }

    public void WaterPlant()
    {
        if (isEmpty) return;

        // cactus coroutine growth
        if (plantedSeed == 1)
        {
            if (growRoutine == null && !hasGrown)
            {
                hasGrown = true;
                growRoutine = StartCoroutine(GrowCactus());
            }
        }

        // carrot 临时测试
        if (plantedSeed == 2)
        {
            if (growRoutine == null && !hasGrown)
            {
                hasGrown = true;
                growRoutine = StartCoroutine(GrowCarrot());
            }
        }

        // mole 临时测试
        if (plantedSeed == 3)
        {
            if (growRoutine == null && !hasGrown)
            {
                hasGrown = true;
                growRoutine = StartCoroutine(PopMole());
            }
        }
    }

    IEnumerator GrowCactus()
    {
        Vector3 startScale = cactusPlant.transform.localScale;
        Vector3 targetScale = startScale * cactusGrowMultiplier;

        float t = 0f;
        bool moustacheShown = false;

        while (t < 1f)
        {
            t += cactusGrowSpeed * Time.deltaTime;

            cactusPlant.transform.localScale =
                Vector3.Lerp(startScale, targetScale, t);

            if (t >= 0.5f && !moustacheShown)
            {
                cactusMoustache.SetActive(true);
                moustacheShown = true;
            }

            yield return null;
        }

        cactusPlant.transform.localScale = targetScale;
        cactusHat.SetActive(true);

        growRoutine = null;
    }

    IEnumerator GrowCarrot()
    {
        Vector3 startScale = carrotPlant.transform.localScale;
        Vector3 targetScale = startScale * carrotGrowMultiplier;

        float t = 0f;
        bool windowShown = false;

        while (t < 1f)
        {
            t += carrotGrowSpeed * Time.deltaTime;

            carrotPlant.transform.localScale =
                Vector3.Lerp(startScale, targetScale, t);

            if (t >= 0.5f && !windowShown)
            {
                carrotWindow.SetActive(true);
                windowShown = true;
            }

            yield return null;
        }

        carrotPlant.transform.localScale = targetScale;
        carrotRabbit.SetActive(true);

        growRoutine = null;
    }

    IEnumerator PopMole()
    {
        Vector3 startPos = molePlant.transform.localPosition;
        Vector3 targetPos = startPos + new Vector3(0f, molePopHeight, 0f);

        float t = 0f;

        while (t < 1f)
        {
            t += molePopSpeed * Time.deltaTime;

            molePlant.transform.localPosition =
                Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        molePlant.transform.localPosition = targetPos;
        moleClover.SetActive(true);

        growRoutine = null;
    }
}