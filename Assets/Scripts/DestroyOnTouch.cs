using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Si el Player choca contra el cubo
        {
            Destroy(collision.gameObject); // Destruye el Player
        }
    }
}
