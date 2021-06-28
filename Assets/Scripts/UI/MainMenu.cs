using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private FadePanel _fadePanel;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);        
    }

    private void Start()
    {
        StartCoroutine(ShowMenu());
    }

    private void OnStartButtonClick()
    {
        StartCoroutine(LoadNewScene());
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private IEnumerator ShowMenu()
    {
        _fadePanel.BecomeFullyVisible();

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        _startButton.interactable = true;
        _exitButton.interactable = true;
    }

    private IEnumerator LoadNewScene()
    {
        _startButton.interactable = false;
        _exitButton.interactable = false;

        _fadePanel.BecomeFullyFaded();

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        ChooseLevel.Load();
    }
}
