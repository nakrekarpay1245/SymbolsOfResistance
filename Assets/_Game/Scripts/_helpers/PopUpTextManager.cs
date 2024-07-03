using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PopUpTextManager : MonoBehaviour
{
    [Header("Pop Up Text Manager")]
    [SerializeField]
    private PopUpText _popUpTextPrefab;
    [SerializeField]
    private int _poolSize = 10;

    [SerializeField]
    private float _positionRandomizer = 0.1f;

    [SerializeField]
    private float _verticalPositionOverride = 0.25f;

    [SerializeField]
    private Vector3 _popUpTextScale;

    private ObjectPool<PopUpText> _popUpTextPool;

    private void Awake()
    {
        _popUpTextPool = new ObjectPool<PopUpText>(() => InstantiatePopUpText(), _poolSize);
    }

    private PopUpText InstantiatePopUpText()
    {
        PopUpText popUpTextInstance = Instantiate(_popUpTextPrefab, transform);
        popUpTextInstance.gameObject.SetActive(false);
        return popUpTextInstance;
    }

    public void ShowPopUpText(Vector3 position, string text, /*Quaternion rotation,*/  float duration = 0.25f)
    {
        PopUpText popUpText = _popUpTextPool.GetObjectFromPool();

        popUpText.transform.position = position +
            Vector3.right * Random.Range(-_positionRandomizer, _positionRandomizer) +
                Vector3.up * Random.Range(-_positionRandomizer, _positionRandomizer);
        //popUpText.transform.rotation = rotation;
        popUpText.SetText(text);

        popUpText.transform.localScale = Vector3.zero;

        popUpText.gameObject.SetActive(true);

        Vector3 startPosition = popUpText.transform.position;
        Vector3 endPosition = startPosition + Vector3.up * _verticalPositionOverride;

        Sequence popUpSequence = DOTween.Sequence();

        popUpSequence.Append(popUpText.transform.DOScale(_popUpTextScale, GlobalBinder.singleton.TimeManager.PopUpTextAnimationDuration).SetEase(Ease.OutBack))
                     .Join(popUpText.transform.DOMove(endPosition, GlobalBinder.singleton.TimeManager.PopUpTextAnimationDuration).SetEase(Ease.OutQuad))
                     .AppendInterval(GlobalBinder.singleton.TimeManager.PopUpTextAnimationDelay)
                     .Append(popUpText.transform.DOScale(0, GlobalBinder.singleton.TimeManager.PopUpTextAnimationDuration).SetEase(Ease.InQuad))
                     .Join(popUpText.transform.DOMove(endPosition + Vector3.up * _verticalPositionOverride, GlobalBinder.singleton.TimeManager.PopUpTextAnimationDuration).SetEase(Ease.InQuad))
                     .OnComplete(() => StartCoroutine(HidePopUpText(popUpText, duration)));

        popUpSequence.Play();
    }

    private IEnumerator HidePopUpText(PopUpText popUpText, float duration)
    {
        yield return new WaitForSeconds(duration);

        _popUpTextPool.ReturnObjectToPool(popUpText);
    }
}