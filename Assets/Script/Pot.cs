using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;

public class Pot : MonoBehaviour
{
    // renderer used to detect mouse hover
    public SpriteRenderer potRenderer;
    public GameManager gameManager;

    // empty and filled pot visuals
    public GameObject emptyPot;
    public GameObject filledPot;

    // plant objects
    public GameObject molePlant;
    public GameObject cactusPlant;
    public GameObject carrotPlant;

    // cactus extra visuals
    public GameObject cactusMoustache;
    public GameObject cactusHat;

    // carrot extra visuals
    public GameObject carrotWindow;
    public GameObject carrotRabbit;

    // mole extra visual
    public GameObject moleClover;

    // hover scaling values
    Vector3 normalScale;
    Vector3 hoverScale;

    // pot state flags
    bool isHovered = false;
    bool isEmpty = true;

    // which seed is planted (1 cactus, 2 carrot, 3 mole)
    int plantedSeed = 0;

    // reference to running coroutine
    Coroutine growRoutine;

    // cactus growth settings
    public float cactusGrowSpeed = 0.1f;
    public float cactusGrowMultiplier = 2f;

    // carrot growth settings
    public float carrotGrowSpeed = 0.1f;
    public float carrotGrowMultiplier = 3.5f;

    // mole pop settings
    public float molePopSpeed = 0.5f;
    public float molePopHeight = 2f;

    // prevents growing more than once
    bool hasGrown = false;

    // event triggered when plant finishes growing
    public UnityEvent onPlantFinishedGrowing;

    // store starting transforms so reset returns correctly
    Vector3 cactusStartScale;
    Vector3 carrotStartScale;
    Vector3 moleStartLocalPosition;

    void Start()
    {
        // store pot scale for hover effect
        normalScale = transform.localScale;
        hoverScale = normalScale * 1.1f;

        // store original plant transforms
        cactusStartScale = cactusPlant.transform.localScale;
        carrotStartScale = carrotPlant.transform.localScale;
        moleStartLocalPosition = molePlant.transform.localPosition;
    }

    void Update()
    {
        // check mouse hover
        CheckHover();
    }

    void CheckHover()
    {
        // disable hover if already planted
        if (!isEmpty)
        {
            transform.localScale = normalScale;
            isHovered = false;
            return;
        }

        Vector2 mousePos = gameManager.mouseWorldPosition;

        // mouse inside pot bounds
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

    public void TryPlant()
    {
        // only allow planting if pot is empty
        if (!isEmpty) return;

        Vector2 mousePos = gameManager.mouseWorldPosition;

        // plant if mouse is inside pot bounds
        if (potRenderer.bounds.Contains(mousePos))
        {
            PlantSelectedSeed();
        }
    }

    void PlantSelectedSeed()
    {
        // mark pot as used
        isEmpty = false;
        hasGrown = false;

        // store selected seed
        plantedSeed = gameManager.selectedSeed;

        // switch pot visuals
        emptyPot.SetActive(false);
        filledPot.SetActive(true);

        // hide all plants first
        molePlant.SetActive(false);
        cactusPlant.SetActive(false);
        carrotPlant.SetActive(false);

        // hide all decorations
        cactusMoustache.SetActive(false);
        cactusHat.SetActive(false);

        carrotWindow.SetActive(false);
        carrotRabbit.SetActive(false);

        moleClover.SetActive(false);

        // reset transforms when planting
        cactusPlant.transform.localScale = cactusStartScale;
        carrotPlant.transform.localScale = carrotStartScale;
        molePlant.transform.localPosition = moleStartLocalPosition;

        // activate correct plant
        if (plantedSeed == 1)
        {
            cactusPlant.SetActive(true);
        }
        else if (plantedSeed == 2)
        {
            carrotPlant.SetActive(true);
        }
        else if (plantedSeed == 3)
        {
            molePlant.SetActive(true);
        }

        transform.localScale = normalScale;
    }

    public void WaterPlant()
    {
        // ignore if nothing planted
        if (isEmpty) return;

        // cactus growth
        if (plantedSeed == 1)
        {
            if (growRoutine == null && !hasGrown)
            {
                hasGrown = true;
                growRoutine = StartCoroutine(GrowCactus());
            }
        }

        // carrot growth
        if (plantedSeed == 2)
        {
            if (growRoutine == null && !hasGrown)
            {
                hasGrown = true;
                growRoutine = StartCoroutine(GrowCarrot());
            }
        }

        // mole pop
        if (plantedSeed == 3)
        {
            if (growRoutine == null && !hasGrown)
            {
                hasGrown = true;
                growRoutine = StartCoroutine(PopMole());
            }
        }
    }

    // cactus grows and shows moustache then triggers final event
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

            // show moustache halfway
            if (t >= 0.5f && !moustacheShown)
            {
                cactusMoustache.SetActive(true);
                moustacheShown = true;
            }

            yield return null;
        }

        // finish growth and raise event
        cactusPlant.transform.localScale = targetScale;
        onPlantFinishedGrowing.Invoke();

        growRoutine = null;
    }

    // carrot grows and reveals window then triggers final event
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

            // show window halfway
            if (t >= 0.5f && !windowShown)
            {
                carrotWindow.SetActive(true);
                windowShown = true;
            }

            yield return null;
        }

        // finish growth and raise event
        carrotPlant.transform.localScale = targetScale;
        onPlantFinishedGrowing.Invoke();

        growRoutine = null;
    }

    // mole pops up and triggers final event
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

        // finish pop and raise event
        molePlant.transform.localPosition = targetPos;
        onPlantFinishedGrowing.Invoke();

        growRoutine = null;
    }

    public void ResetPot()
    {
        // stop growth coroutine
        if (growRoutine != null)
        {
            StopCoroutine(growRoutine);
            growRoutine = null;
        }

        // reset state
        isEmpty = true;
        isHovered = false;
        hasGrown = false;
        plantedSeed = 0;

        // reset pot visuals
        transform.localScale = normalScale;
        emptyPot.SetActive(true);
        filledPot.SetActive(false);

        // hide plants
        molePlant.SetActive(false);
        cactusPlant.SetActive(false);
        carrotPlant.SetActive(false);

        // hide decorations
        cactusMoustache.SetActive(false);
        cactusHat.SetActive(false);

        carrotWindow.SetActive(false);
        carrotRabbit.SetActive(false);

        moleClover.SetActive(false);

        // reset transforms
        cactusPlant.transform.localScale = cactusStartScale;
        carrotPlant.transform.localScale = carrotStartScale;
        molePlant.transform.localPosition = moleStartLocalPosition;
    }
}