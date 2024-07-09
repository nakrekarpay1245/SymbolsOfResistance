using Leaf._helpers;
using UnityEngine;

public class GlobalBinder : MonoSingleton<GlobalBinder>
{
    [Header("Audio")]
    public AudioManager AudioManager;

    [Header("Coin")]
    public CoinManager CoinManager;

    [Header("Economy")]
    public EconomyManager EconomyManager;

    [Header("Enemy")]
    public EnemyManager EnemyManager;

    [Header("Economy")]
    public GameSettings GameSettings;

    [Header("Level")]
    public LevelManager LevelManager;

    [Header("Particle")]
    public ParticleManager ParticleManager;

    [Header("PopUp")]
    public PopUpTextManager PopUpTextManager;

    [Header("Tile")]
    public TileManager TileManager;

    [Header("Time")]
    public TimeManager TimeManager;

    [Header("Unit")]
    public UnitManager UnitManager;
}