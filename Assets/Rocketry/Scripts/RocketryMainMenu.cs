using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class RocketryMainMenu : MonoBehaviour
{
    UIDocument uIDocument;

    Button 
        start, 
        options, 
        quit;

    private void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
        var root = uIDocument.rootVisualElement;

        start = root.Q<Button>("Start");
        options = root.Q<Button>("Options");
        quit = root.Q<Button>("Quit");

        start.RegisterCallback<ClickEvent>(StartButtonClicked);
        options.RegisterCallback<ClickEvent>(OptionsButtonClicked);
        quit.RegisterCallback<ClickEvent>(QuitButtonClicked);
    }

    private void StartButtonClicked(ClickEvent evt)
    {
        Debug.Log("Start Button has been clicked");
        SceneManager.LoadScene("Original");
    }

    private void OptionsButtonClicked(ClickEvent evt)
    {
        Debug.Log("Options button has been clicked");
    }
    private void QuitButtonClicked(ClickEvent evt)
    {
        Debug.Log("Quit Button has been clicked");
    }
}
