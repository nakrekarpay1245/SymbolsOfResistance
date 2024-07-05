using System.Collections.Generic;
using UnityEngine;

public class Melee : Unit
{
    [Header("Melee Attack Params")]
    [Header("Weapon Params")]
    [SerializeField]
    private List<Weapon> _weaponList;
    [SerializeField]
    private float _attackTime = 5f;
    private float _lastAttackTime;
    [Space]
    [SerializeField]
    private float _damage = 1f;
    [Header("Detection Params")]
    [SerializeField]
    private float _distance = 1f;
    [SerializeField]
    private LayerMask _enemyLayer;

    [Header("Particles")]
    [SerializeField]
    private string _attackParticleKey = "AttackParticle";
    [SerializeField]
    private string _attackClipKey = "AttackClip";

    public int _isAttackHash;

    //Private
    private bool _enemyDetected;
    public override void Awake()
    {
        base.Awake();
        _isAttackHash = Animator.StringToHash("isAttack");

        _weaponList = new List<Weapon>();

        // Get all Weapon components in children and add them to the list
        Weapon[] weapons = GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            _weaponList.Add(weapon);
        }

        foreach (Weapon weapon in weapons)
        {
            weapon.DamageAmount = _damage;
        }
    }

    private void OnEnable()
    {
        _lastAttackTime = Time.time + _attackTime;
    }

    private void Update()
    {
        if (_enemyDetected)
        {
            if (_lastAttackTime <= Time.time)
            {
                Debug.Log(name + " Attacked!");
                _lastAttackTime = Time.time + _attackTime;
                Attack();
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (Weapon weapon in _weaponList)
        {
            Vector2 origin = weapon.transform.position;
            Vector2 direction = weapon.transform.right; // Assuming weapons are facing right

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, _distance, _enemyLayer);

            if (hit.collider != null)
            {
                _enemyDetected = true;
                //Debug.Log(weapon.name + " Detect Enemy:" + hit.collider.name);
                return;
            }
            _enemyDetected = false;
            //Debug.Log("Enemy not detected!");
        }
    }

    private void Attack()
    {
        GlobalBinder.singleton.ParticleManager.PlayParticleAtPoint(_attackParticleKey, transform.position);
        GlobalBinder.singleton.AudioManager.PlaySound(_attackClipKey);

        _animator.SetTrigger(_isAttackHash);

        foreach (Weapon weapon in _weaponList)
        {
            weapon.Attack();
        }
    }

    private void OnDrawGizmos()
    {
        if (_weaponList == null) return;

        Gizmos.color = _enemyDetected ? Color.red : Color.green;

        foreach (Weapon weapon in _weaponList)
        {
            if (weapon != null)
            {
                Vector2 origin = weapon.transform.position;
                Vector2 direction = weapon.transform.right; // Assuming weapons are facing right

                Gizmos.DrawRay(origin, direction * _distance);
            }
        }
    }
}
