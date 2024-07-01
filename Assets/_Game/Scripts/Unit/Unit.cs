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
}