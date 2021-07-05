using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevelMenu : MonoBehaviour
{
    [SerializeField] private FadePanel _fadePanel;
    [SerializeField] private List<Button> _buttons = new List<Button>();
    [SerializeField] private List<string> _scenes = new List<string>();
    
    private void OnEnable()
    {
        foreach (var button in _buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
        {
            button.onClick.RemoveListener(() => OnButtonClick(button));
        }
    }

    private void Start()
    {
        StartCoroutine(ShowMenu());
    }

    private void OnButtonClick(Button button)
    {
        int indexOfButton = _buttons.IndexOf(button);
        
        StartCoroutine(LoadScene(_scenes[indexOfButton]));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        foreach (var button in _buttons)
        {
            button.interactable = false;
        }

        _fadePanel.BecomeFullyFaded();

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator ShowMenu()
    {
        _fadePanel.BecomeFullyVisible();

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        foreach (var button in _buttons)
        {
            button.interactable = true;
        }
    }
}
