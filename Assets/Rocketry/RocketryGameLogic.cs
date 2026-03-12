using TMPro;
using UnityEngine;

public class RocketryGameLogic : MonoBehaviour
{
    public int score = 0;

    [SerializeField] TMP_Text scoreCounter;
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
        scoreCounter.text = score.ToString();
    }
}
