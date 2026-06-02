using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    Vector3 offset;

    void Start()
    {
        offset = transform.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ResetCamera()
    {
        target = null;
    }
}