using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float horizontalBoundary = 20f;
    public float verticalBoundary = 10f;

    [Header("Shooting Settings")]
    public GameObject laserPrefab;
    public Transform shootPoint;
    public float shootCooldown = 0.2f;
    private float nextShootTime;

    private void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        // Clamp position within boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalBoundary, horizontalBoundary);
        newPosition.z = Mathf.Clamp(newPosition.z, -verticalBoundary, verticalBoundary);

        transform.position = newPosition;
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    private void Shoot()
    {
        if (laserPrefab != null && shootPoint != null)
        {
            Instantiate(laserPrefab, shootPoint.position, shootPoint.rotation);
        }
    }
} 