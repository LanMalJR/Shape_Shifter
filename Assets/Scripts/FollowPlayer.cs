using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;  // Assign your ShapeShifter object here
    public Vector3 offset = new Vector3(0, 5, -10); // Adjust as needed
    public float smoothSpeed = 0.130f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
