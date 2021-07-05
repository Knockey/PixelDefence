using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class WaveNumberView : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _animationTime;
    [SerializeField] private float _displayTime;

    private TMP_Text _text;
    private int _waveNumber;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _spawner.WaveStarted += OnWaveStarted;
    }

    private void OnDisable()
    {
        _spawner.WaveStarted += OnWaveStarted;        
    }

    private void Start()
    {
        _waveNumber = 1;

        StartCoroutine(ShowWaveNumber());
    }
    
    private void OnWaveStarted()
    {
        _waveNumber++;

        StartCoroutine(ShowWaveNumber());
    }

    private IEnumerator ShowWaveNumber()
    {
        _text.text = $"WAVE {_waveNumber}";
        _text.DOFade(1f, _animationTime);

        yield return new WaitForSeconds(_displayTime);

        _text.DOFade(0f, _animationTime);
    }
}
