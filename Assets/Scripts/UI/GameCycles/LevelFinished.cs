using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinished : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Image _gameFinishedPanel;
    [SerializeField] private Player _player;
    [SerializeField] private Image _playerDiedPanel;
    [SerializeField] private float _animationTime;

    private float _alphaValue = 0.5f;

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
        _spawner.GameFinished += OnGameFinished;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
        _spawner.GameFinished -= OnGameFinished;
    }

    private void OnPlayerDied()
    {
        ShowPanel(_playerDiedPanel);
    }

    private void OnGameFinished()
    {
        ShowPanel(_gameFinishedPanel);
    }

    private void ShowPanel(Image panel)
    {
        panel.gameObject.SetActive(true);
        panel.DOFade(0f, 0f);
        panel.DOFade(_alphaValue, _animationTime);
    }
}
