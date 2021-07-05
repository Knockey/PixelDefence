using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private FadePanel _fadePanel;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButtonCliked);
        _exitButton.onClick.AddListener(OnExitButtonCliked);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButtonCliked);
        _exitButton.onClick.RemoveListener(OnExitButtonCliked);
    }

    private void OnResumeButtonCliked()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void OnExitButtonCliked()
    {
        Time.timeScale = 1;
        _fadePanel.gameObject.SetActive(true);
        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        _resumeButton.interactable = false;
        _exitButton.interactable = false;

        _fadePanel.BecomeFullyFaded();

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        MainMenuScene.Load();
    }
}
