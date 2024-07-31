using UnityEngine;

public class ObjectAnimator : AbstractDamagable
{
    private Animator _animator;

    private int _isAttackHash;
    private int _isDeadHash;
    private int _isHurtHash;
    private int _isWalkingHash;

    private bool _isWalking;

    public override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();

        _isAttackHash = Animator.StringToHash("isAttack");
        _isDeadHash = Animator.StringToHash("isDead");
        _isHurtHash = Animator.StringToHash("isHurt");
        _isWalkingHash = Animator.StringToHash("isWalking");
    }

    public void SetMoveSpeed(float speed)
    {
        _isWalking = speed <= 0.1f ? false : true;
        _animator.SetBool(_isWalkingHash, _isWalking);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_isAttackHash);
    }

    public void PlayDeadAnimation()
    {
        _animator.SetTrigger(_isDeadHash);
    }

    public void PlayHurtAnimation()
    {
        _animator.SetTrigger(_isHurtHash);
    }
}