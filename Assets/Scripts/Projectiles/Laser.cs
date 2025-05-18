using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 20f;
    public float maxDistance = 30f;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        Debug.Log($"Laser spawned at {transform.position} with tag: {gameObject.tag}");
        
        // Ensure the laser has the correct tag
        if (!gameObject.CompareTag("PlayerLaser"))
        {
            Debug.LogError("Laser prefab is missing 'PlayerLaser' tag!");
        }
    }

    private void Update()
    {
        // Move forward in world space
        transform.position += Vector3.forward * speed * Time.deltaTime;

        // Destroy if too far from start position
        if (Vector3.Distance(transform.position, startPosition) > maxDistance)
        {
            Debug.Log($"Laser destroyed at {transform.position} due to max distance");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Laser hit: {other.gameObject.name}, tag: {other.tag}, at position: {transform.position}");
    }
} 