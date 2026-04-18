using UnityEngine;

public class PulseMover : MonoBehaviour
{
    public float speed = 5f;
    public float screenWidth = 10f;

    public AnimationCurve yCurve;

    private float xPos = 0f;
    private TrailRenderer trail;

    void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        xPos += speed * Time.deltaTime;

        if (xPos > screenWidth)
        {

            trail.enabled = false;

            xPos = 0f;


            trail.enabled = true;
        }

        float t = xPos / screenWidth;
        float y = yCurve.Evaluate(t) * 3f;

        transform.position = new Vector3(xPos, y, 0f);
    }
}