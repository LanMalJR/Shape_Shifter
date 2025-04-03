using UnityEngine;

public class ShapeShifter : MonoBehaviour
{
    public GameObject sphereShape;
    public GameObject cubeShape;
    private Rigidbody rb;
    private bool isSphere = true;
    public float moveSpeed = 20f;
    public float gravity = 9.8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Ensure only the sphere is visible initially
        SetShape(true);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.linearVelocity = new Vector3(moveSpeed, rb.linearVelocity.y, 0);
        rb.MovePosition(transform.position + new Vector3(moveSpeed * (Time.deltaTime * 2f), 0, 0));

        // Check for spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSphere = !isSphere;
            SetShape(isSphere);
        }
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
