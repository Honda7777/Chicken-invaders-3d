using UnityEngine;

public class Chicken : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float swayAmount = 2f;
    public float swaySpeed = 2f;
    
    [Header("Attack Settings")]
    public GameObject eggPrefab;
    public float attackCooldown = 2f;
    private float nextAttackTime;

    private Vector3 startPosition;
    private float timePassed;

    private void Start()
    {
        startPosition = transform.position;
        Debug.Log($"Chicken spawned at {transform.position}");
        
        // Verify components
        if (GetComponent<Collider>() == null)
        {
            Debug.LogError("Chicken is missing a Collider component!");
        }
        else if (!GetComponent<Collider>().isTrigger)
        {
            Debug.LogError("Chicken's Collider must be set as a trigger!");
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        timePassed += Time.deltaTime;
        
        // Sway left and right
        float xOffset = Mathf.Sin(timePassed * swaySpeed) * swayAmount;
        
        // Move downward slowly
        Vector3 newPosition = startPosition + new Vector3(xOffset, 0, -moveSpeed * Time.deltaTime);
        startPosition = new Vector3(startPosition.x, startPosition.y, newPosition.z);
        
        transform.position = newPosition;

        // Destroy if off screen
        if (transform.position.z < -20f)
        {
            Debug.Log($"Chicken destroyed at {transform.position} - off screen");
            Destroy(gameObject);
        }
    }

    private void HandleAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Random.value < 0.3f) // 30% chance to attack
            {
                DropEgg();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void DropEgg()
    {
        if (eggPrefab != null)
        {
            Instantiate(eggPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Chicken collision detected with: {other.gameObject.name}, tag: {other.tag}, at position: {transform.position}");
        
        if (other.CompareTag("PlayerLaser"))
        {
            Debug.Log("Chicken hit by laser! Destroying both objects.");
            Destroy(other.gameObject);
            Destroy(gameObject);
            GameManager.Instance.AddScore(100);
        }
    }

    // Add this to visualize the collider
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (GetComponent<Collider>() is BoxCollider boxCollider)
        {
            Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }
} 