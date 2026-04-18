using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashSimple : MonoBehaviour
{
    public float normalSpeed = 3f;
    public float dashSpeed = 10f;

    private float currentSpeed;
    private Coroutine dashRoutine;

    void Start()
    {
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        // 按 D 键触发 dash（简单测试）
        if (Keyboard.current != null && Keyboard.current.dKey.wasPressedThisFrame)
        {
            StartDash();
        }

        // 简单移动（一直向右）
        transform.position += Vector3.right * currentSpeed * Time.deltaTime;
    }

    void StartDash()
    {
        if (dashRoutine != null)
        {
            StopCoroutine(dashRoutine);
        }

        dashRoutine = StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        currentSpeed = dashSpeed;

        yield return new WaitForSeconds(1f);

        currentSpeed = normalSpeed;
        dashRoutine = null;
    }
}