using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Image _pausePanelImage;

    private Button _pauseButton;

    private void Awake()
    {
        _pauseButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClick);        
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);                
    }

    private void OnPauseButtonClick()
    {
        Time.timeScale = 0;
        _pausePanelImage.gameObject.SetActive(true);
    }
}
