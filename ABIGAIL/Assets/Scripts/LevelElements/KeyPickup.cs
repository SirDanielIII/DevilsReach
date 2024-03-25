using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Abigail"))
        {
            GameManager.instance.CollectKey();
            Destroy(gameObject);
        }
    }
}
