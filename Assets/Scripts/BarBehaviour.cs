using UnityEngine;

public class BarBehaviour : MonoBehaviour
{
    const float SPEED = 6f;
    Vector3 desiredScale;

    void Start()
    {
        desiredScale = transform.localScale;
    }


    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, Time.deltaTime * SPEED);
    }
    public void SetScale(float y)
    {
        desiredScale.y = y;
    }
    public void Reset()
    {
        desiredScale.y = 0;
        transform.localScale = desiredScale;
    }
}
