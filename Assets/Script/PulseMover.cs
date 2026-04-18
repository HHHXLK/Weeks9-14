using UnityEngine;

public class PulseMover : MonoBehaviour
{
    public float speed = 5f;
    public float screenWidth = 10f;

    public AnimationCurve yCurve;

    private float xPos = 0f;

    void Update()
    {
        // X移动
        xPos += speed * Time.deltaTime;

        if (xPos > screenWidth)
        {
            xPos = 0f;
        }

        // 归一化时间（0~1）
        float t = xPos / screenWidth;

        // 用 curve 控制 Y
        float y = yCurve.Evaluate(t) * 3f;

        transform.position = new Vector3(xPos, y, 0f);
    }
}
