using UnityEngine;

public class SeedUI : MonoBehaviour
{
    // references to the three seed UI icons
    public RectTransform seed1;
    public RectTransform seed2;
    public RectTransform seed3;

    // normal size of icons
    public Vector3 normalScale = Vector3.one;

    // slightly bigger scale when selected
    public Vector3 selectedScale = new Vector3(1.15f, 1.15f, 1f);

    // called when player presses 1 / 2 / 3
    public void UpdateSelection(int selectedSeed)
    {
        // reset all seeds to normal size
        seed1.localScale = normalScale;
        seed2.localScale = normalScale;
        seed3.localScale = normalScale;

        // scale up the selected seed
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
        // default selection is seed 1 at start
        UpdateSelection(1);
    }
}