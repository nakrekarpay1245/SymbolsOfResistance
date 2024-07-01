using TMPro;
using UnityEngine;
using DG.Tweening;

public class EconomyManager : MonoBehaviour
{
    [Header("Economy Manager Params")]
    [SerializeField]
    private int _coinCount;
    public int CoinCount { get => _coinCount; private set => _coinCount = value; }

    [Space]

    [SerializeField]
    private TextMeshProUGUI _coinCountText;

    private void Awake()
    {
        UpdateCoinDisplay();
    }

    public bool IsMoneyEnoughForPurchase(int price)
    {
        bool isMoneyEnoughForPurchase = _coinCount >= price;
        return isMoneyEnoughForPurchase;
    }

    public void IncreaseCoin(int amount = 1)
    {
        _coinCount += amount;
        UpdateCoinDisplay();
    }

    public void DecreaseCoin(int amount = 1)
    {
        _coinCount -= amount;
        UpdateCoinDisplay();
    }

    private void UpdateCoinDisplay()
    {
        int currentCoinCount = int.Parse(_coinCountText.text);

        DOTween.To(() => currentCoinCount, x => currentCoinCount = x, _coinCount, 1f)
            .OnUpdate(() =>
            {
                _coinCountText.text = currentCoinCount.ToString();
            });
    }
}