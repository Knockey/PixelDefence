using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameFinishedMenu : MonoBehaviour
{
    [SerializeField] private Button _chooseLevelButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private FadePanel _fadePanel;

    private void OnEnable()
    {
        _chooseLevelButton.onClick.AddListener(()=> StartCoroutine(LoadChooseLevelScene()));
        _exitButton.onClick.AddListener(() => StartCoroutine(LoadMainMenuScene()));
    }

    private void OnDisable()
    {
        _chooseLevelButton.onClick.RemoveListener(() => StartCoroutine(LoadChooseLevelScene()));
        _exitButton.onClick.RemoveListener(() => StartCoroutine(LoadMainMenuScene()));
    }

    private IEnumerator LoadChooseLevelScene()
    {
        PrepareToLoad();

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        ChooseLevel.Load();
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

        _chooseLevelButton.interactable = false;
        _exitButton.interactable = false;

        _fadePanel.BecomeFullyFaded();
    }
}
