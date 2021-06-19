using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerMovementStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float _handleRange = 1;
    [SerializeField] private RectTransform _handle;
    [SerializeField] private RectTransform _background;
    
    private Canvas _canvas;
    private Vector2 _stickPosition;
    private bool _isTouched;

    public event UnityAction<Vector2> StickMovedInDirection;
    public event UnityAction<bool> StickMoved;

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        _isTouched = false;

        Vector2 center = new Vector2(0.5f, 0.5f);
        _background.pivot = center;
        _handle.anchorMin = center;
        _handle.anchorMax = center;
        _handle.pivot = center;

        ResetStickPosition();
    }

    private void OnValidate()
    {
        if (_handleRange < 0)
            _handleRange = Mathf.Abs(_handleRange);
    }

    private void Update()
    {
        if (_isTouched)
            StickMovedInDirection?.Invoke(_stickPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = _background.position;
        Vector2 radius = _background.sizeDelta / 2;

        _stickPosition = (eventData.position - position) / (radius * _canvas.scaleFactor);

        HandleInput(_stickPosition.magnitude, _stickPosition.normalized);
        _handle.anchoredPosition = _stickPosition * radius * _handleRange;
    }

    private void HandleInput(float magnitude, Vector2 normalized)
    {
        if (magnitude > 1)
            _stickPosition = normalized;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _isTouched = true;

        StickMoved?.Invoke(_isTouched);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isTouched = false;
        ResetStickPosition();

        StickMoved?.Invoke(_isTouched);
    }

    private void ResetStickPosition()
    {
        _stickPosition = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
    }
}
