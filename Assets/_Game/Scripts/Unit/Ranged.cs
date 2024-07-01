using UnityEngine;

public class Ranged : Unit
{
    [Header("Ranged Params")]
    [Header("Detection Params")]
    [SerializeField]
    private float distance = 10f;
    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private float _attackTime = 2f;
    private float _lastAttackTime;

    [Header("Bullet Params")]
    [SerializeField]
    private Rock _rockPrefab;
    [SerializeField]
    private float _rockForce = 5f;
    [Header("Transform Params")]
    [SerializeField]
    private Transform _muzzleTransform;

    [Header("Effects")]
    [Header("Throw")]
    [SerializeField]
    private string _throwParticleKey = "RockThrowParticle";
    [SerializeField]
    private string _throwClipKey = "RockThrowClip";

    public bool _enemyDetected;

    private void Update()
    {
        if (_enemyDetected)
        {
            if (_lastAttackTime <= Time.time)
            {
                _lastAttackTime = Time.time + _attackTime;
                ThrowRock();
            }
        }
    }


    void FixedUpdate()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, enemyLayer);

        if (hit.collider != null)
        {
            _enemyDetected = true;
            Debug.Log("Enemy detected: " + hit.collider.name);
        }
        else
        {
            _enemyDetected = false;
        }
    }

    private void ThrowRock()
    {
        Vector3 muzzlePosition = _muzzleTransform.position;
        Rock generatedRock = Instantiate(_rockPrefab, muzzlePosition, Quaternion.identity);
        generatedRock.GetComponent<Rigidbody2D>().AddForce(Vector3.right * _rockForce);
        GlobalBinder.singleton.ParticleManager.PlayParticleAtPoint(_throwParticleKey, muzzlePosition);
        GlobalBinder.singleton.AudioManager.PlaySound(_throwClipKey);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = _enemyDetected ? Color.red : Color.green;
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.right;
        Gizmos.DrawLine(origin, origin + direction * distance);
    }
}