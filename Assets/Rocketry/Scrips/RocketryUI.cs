using UnityEngine;
using UnityEngine.UIElements;

public class RocketryUI : MonoBehaviour
{
    [SerializeField] private FlatRocketMovement player;
    UIDocument uIDocument;

    Label scoreCounter;
    ProgressBar fuelBar;

    private void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
        var root = uIDocument.rootVisualElement;

        scoreCounter = root.Q<Label>("ScoreDisplay");
        fuelBar = root.Q<ProgressBar>("FuelDisplay");

        //scoreUpdate();
    }

    public void scoreUpdate(int score)
    {
        scoreCounter.text = "Score: " + score;
    }

    public void fuelUpdate(float currentFuel)
    {
        fuelBar.value = currentFuel;
        fuelBar.title = "Fuel: " + currentFuel.ToString("F0");
    }
}
