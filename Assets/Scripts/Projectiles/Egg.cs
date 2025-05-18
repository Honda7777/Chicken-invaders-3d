using UnityEngine;

public class Egg : MonoBehaviour
{
    public float speed = 8f;
    public float rotationSpeed = 180f;

    private void Update()
    {
        // Move downward
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        
        // Rotate for visual effect
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);

        // Destroy if off screen
        if (transform.position.z < -20f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
} 