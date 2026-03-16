//using TMPro;
using UnityEngine;

public class RocketryGameLogic : MonoBehaviour
{
    public int score = 0;

    [SerializeField] RocketryUI ui;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ui.scoreUpdate(score);
    }

    public void logScore()
    {
        Debug.Log("Current score: " + score);
        ui.scoreUpdate(score);
    }
}
