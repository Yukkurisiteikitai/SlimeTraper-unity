using UnityEngine;
using DG.Tweening;

public class Corner : MonoBehaviour
{
    [SerializeField] private Vector2 facingDirection = new Vector2(1, 1);
    [SerializeField] private float moveDistance = 25f;
    [SerializeField] private float duration = 2f; 

    private RectTransform rectTransform;
    private Vector2 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        
        Vector2 targetPos = startPos + facingDirection.normalized * moveDistance;
        
        rectTransform.DOAnchorPos(targetPos, duration)
            .SetEase(Ease.InOutCubic)
            .SetLoops(-1, LoopType.Yoyo);
    }
}