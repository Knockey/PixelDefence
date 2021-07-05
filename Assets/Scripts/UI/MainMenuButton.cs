using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private FadePanel _fadePanel;
    [SerializeField] private List<Button> _levelButtons = new List<Button>(); 

    private Button _exitButton;

    private void Awake()
    {
        _exitButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnButtonCliked);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnButtonCliked);        
    }

    private void OnButtonCliked()
    {
        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        foreach (var button in _levelButtons)
        {
            button.interactable = false;
        }

        _exitButton.interactable = false;

        _fadePanel.BecomeFullyFaded();

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        MainMenuScene.Load();
    }
}
