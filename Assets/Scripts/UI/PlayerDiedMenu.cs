using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDiedMenu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private FadePanel _fadePanel;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(() => StartCoroutine(RestartLevel()));
        _exitButton.onClick.AddListener(() => StartCoroutine(LoadMainMenuScene()));
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(() => StartCoroutine(RestartLevel()));
        _exitButton.onClick.RemoveListener(() => StartCoroutine(LoadMainMenuScene()));
    }

    private IEnumerator RestartLevel()
    {
        PrepareToLoad();

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator LoadMainMenuScene()
    {
        PrepareToLoad();

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        MainMenuScene.Load();
    }

    private void PrepareToLoad()
    {
        _fadePanel.gameObject.SetActive(true);

        _restartButton.interactable = false;
        _exitButton.interactable = false;

        _fadePanel.BecomeFullyFaded();
    }
}
