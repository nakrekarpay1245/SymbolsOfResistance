using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int EnemyCount = 3;
    public float SpawnInterval = 1;
}

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Configuration")]
    [Tooltip("List of enemy prefabs to spawn.")]
    [SerializeField] private List<Enemy> _enemyPrefabList;

    [Header("Spawn Points")]
    [Tooltip("List of points where enemies can spawn.")]
    [SerializeField] private List<Transform> _generatePointList;

    [Header("Wave Configuration")]
    [Tooltip("List of waves that define the enemy spawn behavior.")]
    [SerializeField] private List<Wave> _waves;

    [Tooltip("Time interval between each wave of enemies.")]
    [SerializeField] private float _waveInterval = 5f;

    [Header("UI Elements")]
    [Tooltip("Text displaying the current wave number.")]
    [SerializeField] private TextMeshProUGUI _waveText;

    [HideInInspector] public int TotalEnemiesKilled;

    private int _currentWaveIndex = 0;
    private Vector3 _originalTextPosition;

    private void Start()
    {
        _originalTextPosition = _waveText.rectTransform.position;
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (_currentWaveIndex < _waves.Count)
        {
            DisplayWaveText();

            Wave currentWave = _waves[_currentWaveIndex];
            for (int i = 0; i < currentWave.EnemyCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(currentWave.SpawnInterval);
            }

            yield return new WaitUntil(() => AllEnemiesKilled());

            _currentWaveIndex++;
            if (_currentWaveIndex < _waves.Count)
                yield return new WaitForSeconds(_waveInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (_enemyPrefabList.Count == 0 || _generatePointList.Count == 0)
        {
            Debug.LogWarning("No enemies or spawn points available.");
            return;
        }

        Enemy enemyPrefab = _enemyPrefabList[Random.Range(0, _enemyPrefabList.Count)];
        Transform spawnPoint = _generatePointList[Random.Range(0, _generatePointList.Count)];

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void DisplayWaveText()
    {
        _waveText.text = "Wave " + (_currentWaveIndex + 1);
        _waveText.rectTransform.position = _originalTextPosition;
        _waveText.rectTransform.localScale = Vector3.one;

        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_waveText.rectTransform.DOMove(screenCenter, 1f))
                .Join(_waveText.rectTransform.DOScale(1.5f, 1f))
                .AppendInterval(1f)
                .Append(_waveText.rectTransform.DOMove(_originalTextPosition, 1f))
                .Join(_waveText.rectTransform.DOScale(1f, 1f));
    }

    private bool AllEnemiesKilled()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }

    private void OnDrawGizmos()
    {
        if (_generatePointList == null) return;

        foreach (Transform point in _generatePointList)
        {
            if (point == null) continue;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(point.position, 0.5f); // Draw a circle at the spawn point

            Vector3 leftOffset = new Vector3(-20f, 0f, 0f);
            Vector3 squareCorner1 = point.position + new Vector3(0f, 0.5f, 0f);
            Vector3 squareCorner2 = point.position + new Vector3(0f, -0.5f, 0f);
            Vector3 squareCorner3 = squareCorner2 + leftOffset;
            Vector3 squareCorner4 = squareCorner1 + leftOffset;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(squareCorner1, squareCorner2);
            Gizmos.DrawLine(squareCorner2, squareCorner3);
            Gizmos.DrawLine(squareCorner3, squareCorner4);
            Gizmos.DrawLine(squareCorner4, squareCorner1);
        }
    }
}