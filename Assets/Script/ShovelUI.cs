using UnityEngine;
using System.Collections;

public class ShovelUI : MonoBehaviour
{
    // normal size of shovel icon
    Vector3 normalScale;

    // slightly bigger size for feedback
    Vector3 feedbackScale;

    // stop it if pressed again
    Coroutine feedbackRoutine;

    void Start()
    {
        // store starting scale
        normalScale = transform.localScale;

        // make a slightly bigger scale for animation
        feedbackScale = normalScale * 1.15f;
    }

    // called when player presses R to reset
    public void PlayResetFeedback()
    {
        // stop previous animation if still running
        if (feedbackRoutine != null)
        {
            StopCoroutine(feedbackRoutine);
        }

        // start scale feedback animation
        feedbackRoutine = StartCoroutine(PlayScaleFeedback());
    }

    // simple coroutine to scale up then back down
    IEnumerator PlayScaleFeedback()
    {
        // scale up
        transform.localScale = feedbackScale;

        // wait a short time
        yield return new WaitForSeconds(0.15f);

        // return to normal size
        transform.localScale = normalScale;

        // clear routine reference
        feedbackRoutine = null;
    }
}
