using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleAction : MonoBehaviour
{
    public float duration = 0.5f;
    public AnimationCurve curve;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PlayAction();
        }
    }

    public void PlayAction()
    {
        StartCoroutine(DoAction());
    }

    IEnumerator DoAction()
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float p = t / duration;

            float scale = curve.Evaluate(p);

            transform.localScale = Vector3.one * (1 + scale * 2f);

            yield return null;
        }

        transform.localScale = Vector3.one;
    }
}