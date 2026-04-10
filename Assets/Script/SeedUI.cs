using UnityEngine;

public class SeedUI : MonoBehaviour
{
    public RectTransform seed1;
    public RectTransform seed2;
    public RectTransform seed3;

    public Vector3 normalScale = Vector3.one;
    public Vector3 selectedScale = new Vector3(1.15f, 1.15f, 1f);

    public void UpdateSelection(int selectedSeed)
    {
        seed1.localScale = normalScale;
        seed2.localScale = normalScale;
        seed3.localScale = normalScale;

        if (selectedSeed == 1)
        {
            seed1.localScale = selectedScale;
        }
        else if (selectedSeed == 2)
        {
            seed2.localScale = selectedScale;
        }
        else if (selectedSeed == 3)
        {
            seed3.localScale = selectedScale;
        }
    }

    private void Start()
    {
        UpdateSelection(1);
    }
}