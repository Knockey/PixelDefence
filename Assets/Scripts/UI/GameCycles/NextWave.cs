using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Button _nextWaveButton;

    private void OnEnable()
    {
        _spawner.WaveFinished += OnWaveFinished;
    }

    private void OnDisable()
    {    
        _spawner.WaveFinished -= OnWaveFinished;
    }

    private void OnWaveFinished()
    {
        _nextWaveButton.gameObject.SetActive(true);
        _nextWaveButton.onClick.AddListener(OnNextWaveButtonClick);
    }

    private void OnNextWaveButtonClick()
    {
        _spawner.NextWave();
        _nextWaveButton.onClick.RemoveListener(OnNextWaveButtonClick);
        _nextWaveButton.gameObject.SetActive(false);
    }
}
