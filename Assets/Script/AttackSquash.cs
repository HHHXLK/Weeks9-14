using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSquash : MonoBehaviour
{
    public float duration = 0.5f;

    private Coroutine currentRoutine;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Space pressed");
            OnAttack();
        }
    }

    public void OnAttack()
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(Squash());
    }

    IEnumerator Squash()
    {
        float t = 0f;

        Vector3 normal = Vector3.one;
        Vector3 squashed = new Vector3(2f, 0.3f, 1f);

        while (t < duration)
        {
            t += Time.deltaTime;
            float p = t / duration;

            transform.localScale = Vector3.Lerp(normal, squashed, p);

            yield return null;
        }

        transform.localScale = normal;
        currentRoutine = null;
    }
}