using UnityEngine;
using DG.Tweening;

public class StartEase : MonoBehaviour
{
    public enum EaseTarget
    {
        Position,
        Rotation,
        Scale
    }
    
    [SerializeField] private EaseTarget target = EaseTarget.Position;
    [SerializeField] private Vector3 targetValue = new Vector3(100, 0, 0);
    [SerializeField] private float duration = 1f;
    [SerializeField] private Ease easeType = Ease.OutBack;
    [SerializeField] private bool loop = false;
    [SerializeField] private LoopType loopType = LoopType.Yoyo;

    private void Start()
    {
        ApplyEase();
    }

    public void ApplyEase()
    {
        switch (target)
        {
            case EaseTarget.Position:
                AnimatePosition();
                break;
            case EaseTarget.Rotation:
                AnimateRotation();
                break;
            case EaseTarget.Scale:
                AnimateScale();
                break;
        }
    }

    private void AnimatePosition()
    {
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos + targetValue;

        if (loop)
            transform.DOLocalMove(endPos, duration).SetEase(easeType).SetLoops(-1, loopType);
        else
            transform.DOLocalMove(endPos, duration).SetEase(easeType);
    }

    private void AnimateRotation()
    {
        Vector3 startRot = transform.localEulerAngles;
        Vector3 endRot = startRot + targetValue;

        if (loop)
            transform.DOLocalRotate(endRot, duration).SetEase(easeType).SetLoops(-1, loopType);
        else
            transform.DOLocalRotate(endRot, duration).SetEase(easeType);
    }

    private void AnimateScale()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale + targetValue;

        if (loop)
            transform.DOScale(endScale, duration).SetEase(easeType).SetLoops(-1, loopType);
        else
            transform.DOScale(endScale, duration).SetEase(easeType);
    }
}
