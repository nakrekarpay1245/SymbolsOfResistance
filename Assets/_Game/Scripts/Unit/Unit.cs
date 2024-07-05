using UnityEngine;

public abstract class Unit : AbstractDamagable
{
    [Header("Unit Params")]
    [SerializeField]
    private int _price = 50;
    public int Price
    {
        get => _price;
        private set => _price = value;
    }

    [SerializeField]
    private Sprite _unitIcon;
    public Sprite UnitIcon
    {
        get => _unitIcon;
        private set => _unitIcon = value;
    }

    private Tile _tile;

    public Tile Tile
    {
        get => _tile;
        set
        {
            _tile = value;
            transform.position = _tile.transform.position;
        }
    }

    [Header("Animator Params")]
    [SerializeField]
    protected Animator _animator;
    public int _isHurtHash;

    public override void Awake()
    {
        base.Awake();
        _animator = GetComponentInChildren<Animator>();
        _isHurtHash = Animator.StringToHash("isHurt");
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        _animator.SetTrigger(_isHurtHash);
    }

    public override void Die()
    {
        base.Die();
        Tile.TileState = TileState.Empty;
        Tile.Unit = null;
    }
}