using System;
using UnityEngine;

public class Factory : Unit
{
    [Header("Factory Params")]
    [SerializeField]
    private float _produceTime = 5;
    private float _lastProduceTime;

    [Header("Produce Effects")]
    [SerializeField]
    private string _produceClipKey = "ProduceClip";

    public void OnEnable()
    {
        _lastProduceTime = Time.time + _produceTime;
    }

    private void Update()
    {
        if (_lastProduceTime <= Time.time)
        {
            _lastProduceTime = Time.time + _produceTime;
            Produce();
        }
    }

    private void Produce()
    {
        //Debug.Log("Coin Produced!");
        GlobalBinder.singleton.CoinManager.GenerateCoinAtPoint(transform.position);
        GlobalBinder.singleton.AudioManager.PlaySound(_produceClipKey);
    }
}
