using UnityEngine;

public class ShapeShifter : MonoBehaviour
{
    public GameObject sphereShape;
    public GameObject cubeShape;
    
    private Rigidbody rb;
    
    private bool isSphere = true;
    public float moveSpeed = 20f;
    public float gravity = 9.8f;

    public float[] lanePositions = { -5f, 0f, 5f }; // Left, Middle, Right
    private int currentLane = 1; // Start in the middle
    public float laneChangeSpeed = 10f; // Speed for smooth movement

    private Vector3 targetPosition; // Store the lane transition target

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Ensure only the sphere is visible initially
        SetShape(true);

        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //rb.linearVelocity = new Vector3(moveSpeed, rb.linearVelocity.y, 0);
        Vector3 forwardMovement = new Vector3(moveSpeed * Time.deltaTime, 0, 0);

        // Check for spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSphere = !isSphere;
            SetShape(isSphere);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane < lanePositions.Length - 1)
        {
            currentLane++; // Move left
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane > 0)
        {
            currentLane--; // Move right
        }

        // Smooth movement to the target lane
        targetPosition = new Vector3(transform.position.x, transform.position.y, lanePositions[currentLane]);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * laneChangeSpeed);

        // Maintain forward movement while sliding between lanes
        rb.MovePosition(smoothedPosition + forwardMovement);
    }

    void SetShape(bool sphereActive)
    {
        sphereShape.SetActive(sphereActive);
        cubeShape.SetActive(!sphereActive);

        if (sphereActive)
        {
            rb.constraints = RigidbodyConstraints.None;  // Allow rolling
            rb.angularDamping = 0.05f; // Enable rolling motion
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rb.angularDamping = 2f; // Stop rolling behavior
            rb.angularVelocity = Vector3.zero;
        }
    }
}
