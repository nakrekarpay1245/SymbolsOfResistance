using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinManager : MonoBehaviour
{
    [Header("Coin Manager PArams")]
    [SerializeField]
    private Coin _coinPrefab;

    [Header("Coin Manager PArams")]
    [SerializeField]
    private float _animationDuration = 0.5f;

    [Header("Coin Manager PArams")]
    [SerializeField]
    private Transform _collectPoint;

    private ObjectPool<Coin> _coinPool;

    private void Awake()
    {
        _coinPool = new ObjectPool<Coin>(() => InstantiateCoin(), 10);
    }

    private Coin InstantiateCoin()
    {
        Coin coinInstance = Instantiate(_coinPrefab, transform);
        coinInstance.gameObject.SetActive(false);
        return coinInstance;
    }

    public void GenerateCoinAtPoint(Vector3 point)
    {
        Coin coin = _coinPool.GetObjectFromPool();

        Vector3 coinPosition = point - Vector3.up;

        coin.transform.position = point;

        coin.transform.localScale = Vector3.zero;

        coin.gameObject.SetActive(true);

        // Pop-up scale effect
        coin.transform.DOScale(Vector3.one * 1.25f, 0.15f).OnComplete(() =>
        {
            coin.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        });

        Vector3 randomOffset = new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.1f, 0.25f), 0);

        // Pop-up position effect with random offset
        coin.transform.DOMove(point + new Vector3(0, 1, 0), 0.15f).OnComplete(() =>
        {
            coin.transform.DOMove(coinPosition + randomOffset, 0.5f).SetEase(Ease.OutBounce);
        });
    }

    public void HideCoin(Coin coin)
    {
        coin.transform.DOMove(_collectPoint.position, _animationDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            _coinPool.ReturnObjectToPool(coin);
        });
    }
}