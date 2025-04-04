using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;  // Assign ShapeShifter object here
    public Vector3 offset = new Vector3(0, 2, -6); // Camera stays behind and above
    public float smoothSpeed = 5f;

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
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Make the camera look at the player
            transform.LookAt(target);
        }
    }
}
