using UnityEngine;

public class RocketryGameLogic : MonoBehaviour
{
    public int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void logScore()
    {
        Debug.Log("Current score: " + score);
    }
}
