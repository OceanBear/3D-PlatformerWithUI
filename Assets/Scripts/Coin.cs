using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // Rotation speed in degrees per second
    // Update is called once per frame
    void Update()
    {
        // Rotate the coin continuously around its Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    // When the player enters the coin trigger, increase score and destroy the coin

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Increase the score by 1
            GameManager.Instance.IncreaseScore();
            // Destroy this coin object to simulate collection
            Destroy(gameObject);
        }
    }
}
