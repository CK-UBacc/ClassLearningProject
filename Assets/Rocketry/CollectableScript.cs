using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    [SerializeField] RocketryGameLogic gameLogic = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collectable has been collected by \"" + other.gameObject.name + "\"");
            gameLogic.score += 1;
            gameLogic.logScore();
            Destroy(gameObject);
        }
    }
}
