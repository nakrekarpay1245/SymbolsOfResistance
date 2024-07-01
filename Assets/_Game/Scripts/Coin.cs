using DG.Tweening;
using Leaf._helpers;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    [Header("Coin Params")]
    [Header("Value")]
    [SerializeField]
    private int _value = 50;

    [Space]

    //[Header("Effects & Animations")]
    //[Header("Effects")]
    //[Header("Particles")]
    //[SerializeField]
    //private string _collectParticleKey = "CoinCollectParticle";
    //[Header("Audios")]
    //[SerializeField]
    //private string _collectClipKey = "CoinCollectClip";
    [Space]

    [Header("Components & References")]
    [Header("Components")]
    [SerializeField]
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        GameStateManager.Singleton.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Singleton.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnEnable()
    {
        if (_collider != null)
        {
            _collider.enabled = true;
        }
        else
        {
            _collider = GetComponent<Collider2D>();
        }
    }

    public void Collect()
    {
        _collider.enabled = false;

        //GlobalBinder.singleton.ParticleManager.PlayParticleAtPoint(_collectParticleKey, transform.position);

        //GlobalBinder.singleton.AudioManager.PlaySound(_collectClipKey);

        GlobalBinder.singleton.CoinManager.HideCoin(this);

        GlobalBinder.singleton.EconomyManager.IncreaseCoin(_value);
    }

    public void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}