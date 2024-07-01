using UnityEngine;

public class Tile : MonoBehaviour, ISelectable
{
    private TileState _tileState;
    public TileState TileState
    {
        get { return _tileState; }
        set { _tileState = value; }
    }

    private Vector2Int _tileGridPosition;
    public Vector2Int TileGridPosition
    {
        get { return _tileGridPosition; }
        set { _tileGridPosition = value; }
    }

    private Unit _unit;
    public Unit Unit
    {
        get { return _unit; }
        set { _unit = value; }
    }

    //[Header("Tile visualization reference")]
    //[Tooltip("Image that will appear when tile is not selected")]
    //[SerializeField]
    //private Sprite _deSelectedTile;
    //[Tooltip("Image that will appear when tile is selected")]
    //[SerializeField]
    //private Sprite _selectedTile;

    //private SpriteRenderer _spriteRendererComponent;

    //private void Awake()
    //{
    //    _spriteRendererComponent = GetComponent<SpriteRenderer>();
    //}

    /// <summary>
    /// Deselects the tile by changing its sprite to the deselected tile sprite and hides the 
    /// GoToDisplayer object.
    /// </summary>
    public void DeSelect()
    {
        //_spriteRendererComponent.sprite = _deSelectedTile;
        //Debug.Log(name + "DESelected!");
    }

    /// <summary>
    /// Selects the tile by changing its sprite to the selected tile sprite and hides the 
    /// GoToDisplayer object.
    /// </summary>
    public void Select()
    {
        //_spriteRendererComponent.sprite = _selectedTile;
        //Debug.Log(name + " Selected!");
    }
}

public enum TileState
{
    Full,
    Empty
}