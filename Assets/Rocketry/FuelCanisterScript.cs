using UnityEngine;

public class FuelCanisterScript : MonoBehaviour
{
    [SerializeField] FlatRocketMovement player;
    private void OnTriggerEnter(Collider other) // Don't know if it is better to have the collision logic on the player but whatever.
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.AddFuel();
            Destroy(gameObject);
        }
    }

}
