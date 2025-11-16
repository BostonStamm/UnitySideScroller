using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) // Calls this function when the player collides with a 2D trigger object.
    {
        if (other.gameObject.CompareTag("Coin")) // If the object is a coin, destroy it.
        {
            Destroy(other.gameObject);
        }
    }
}
