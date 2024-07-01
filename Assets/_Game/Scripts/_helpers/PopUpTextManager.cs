using DG.Tweening;
using Leaf._helpers;
using System.Collections;
using UnityEngine;

public class PopUpTextManager : MonoBehaviour
{
    [Header("Pop Up Text Manager")]
    [SerializeField]
    private PopUpText _popUpTextPrefab;

    [SerializeField]
    private float _positionRandomizer = 1f;

    private ObjectPool<PopUpText> _popUpTextPool;

    private void Awake()
    {
        _popUpTextPool = new ObjectPool<PopUpText>(() => InstantiatePopUpText(), 10);
    }

    private PopUpText InstantiatePopUpText()
    {
        PopUpText popUpTextInstance = Instantiate(_popUpTextPrefab, transform);
        popUpTextInstance.gameObject.SetActive(false);
        return popUpTextInstance;
    }

    public void ShowPopUpText(Vector3 position, Quaternion rotation, string text, float duration)
    {
        PopUpText popUpText = _popUpTextPool.GetObjectFromPool();

        popUpText.transform.position = position +
            Vector3.right * Random.Range(-_positionRandomizer, _positionRandomizer) +
                Vector3.up * Random.Range(-_positionRandomizer, _positionRandomizer);
        popUpText.transform.rotation = rotation;
        popUpText.SetText(text);

        popUpText.transform.DOScale(1f, GlobalBinder.singleton.TimeManager.PopUpTextAnimationDuration);

        popUpText.transform.DOMoveY(popUpText.transform.position.y + 1f,
            GlobalBinder.singleton.TimeManager.PopUpTextAnimationDuration).OnComplete(() =>
            {
                popUpText.transform.DOScale(0f, GlobalBinder.singleton.TimeManager.PopUpTextAnimationDuration).
                    SetDelay(GlobalBinder.singleton.TimeManager.PopUpTextAnimationDelay);

                //popUpText.transform.DOMoveY(transform.position.y - 1f,
                //    GlobalBinder.singleton.TimeManager.PopUpTextAnimationDuration).
                //        SetDelay(GlobalBinder.singleton.TimeManager.PopUpTextAnimationDelay);

            });

        popUpText.gameObject.SetActive(true);

        StartCoroutine(HidePopUpText(popUpText, duration));
    }

    private IEnumerator HidePopUpText(PopUpText popUpText, float duration)
    {
        yield return new WaitForSeconds(duration);

        _popUpTextPool.ReturnObjectToPool(popUpText);
    }
}