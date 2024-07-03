using DG.Tweening;
using UnityEngine;

public class Selector : MonoBehaviour, ICollector /*,ISelector*/
{
    //[Header("Selector Params")]
    //[SerializeField]
    //private string _placeParticleName = "PlaceParticle";

    [Header("Selector Params")]
    [SerializeField]
    private Transform _unitDisplayer;
    [SerializeField]
    private SpriteRenderer _unitDisplayerSprite;

    private Unit _selectedUnit;

    private void Update()
    {
        HandleMouseInput();

        if (_selectedUnit != null)
        {
            ShowUnitDisplayer();

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _unitDisplayer.transform.position = mousePosition + (Vector3.forward * 10);
        }
    }

    /// <summary>
    /// Handles the mouse input for tile interaction.
    /// </summary>
    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseButtonDown();
        }
        else if (Input.GetMouseButton(0))
        {
            HandleMouseButton();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HandleMouseButtonUp();
        }
    }

    /// <summary>
    /// Handles the mouse button down input for tile selection.
    /// </summary>
    private void HandleMouseButtonDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Tile clickedTile = GlobalBinder.singleton.TileManager.GetNearestTile(mousePosition);
        //if (clickedTile != null)
        //{
        //    Debug.Log("CT: " + clickedTile.name);
        //}
        if (clickedTile != null && _selectedUnit)
        {
            if (clickedTile.TileState != TileState.Full)
            {
                SelectTile(clickedTile, _selectedUnit);
                _selectedUnit = null;
                HideUnitDisplayer();
            }
            else
            {
                _selectedUnit = null;
                Debug.LogWarning(clickedTile.name + " is FULL!");
            }
        }
        //else if (clickedTile == null && _selectedUnit)
        //{
        //    //Debug.Log("Unit Selected But Not Placed");
        //}

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<ICollectable>(out ICollectable collectable))
            {
                Collect(collectable);
            }
        }
    }

    /// <summary>
    /// Handles the mouse button input during tile selection.
    /// </summary>
    private void HandleMouseButton()
    {
        //Debug.Log("Unit Selected But Not Placed");

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (_selectedUnit != null)
        {
            ShowUnitDisplayer();
            _unitDisplayer.transform.position = mousePosition + (Vector3.forward * 10);
            _selectedUnit.transform.position = mousePosition + (Vector3.forward * 10);
        }

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<ICollectable>(out ICollectable collectable))
            {
                Collect(collectable);
            }
        }
    }

    /// <summary>
    /// Handles the mouse button up event. If multiple tiles are selected, it removes the first tile from the line between tiles, updates its state and visual representation, removes it from the selected tiles list, and initiates the smooth movement of the player character to the selected tiles.
    /// </summary>
    private void HandleMouseButtonUp()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Tile currentTile = GlobalBinder.singleton.TileManager.GetNearestTile(mousePosition);

        if (currentTile != null && _selectedUnit)
        {
            if (currentTile.TileState != TileState.Full)
            {
                SelectTile(currentTile, _selectedUnit);
                HideUnitDisplayer();
                _selectedUnit = null;
            }
            else
            {
                _selectedUnit = null;
                HideUnitDisplayer();
                Debug.LogWarning(currentTile.name + " is FULL!");
            }
        }
    }

    /// <summary>
    /// Selects a tile and performs the necessary actions, such as adding it to the line between tiles, 
    /// adding it to the selected tiles list, and updating its visual state.
    /// </summary>
    /// <param name="tile">The tile to select.</param>
    private void SelectTile(Tile tile, Unit unit)
    {
        //Debug.Log(tile.name + " selected!");
        Unit generatedUnit = GlobalBinder.singleton.UnitManager.GenerateUnit(unit);
        tile.Unit = generatedUnit;
        generatedUnit.Tile = tile;
        tile.TileState = TileState.Full;
        tile.Select();
        GlobalBinder.singleton.EconomyManager.DecreaseCoin(generatedUnit.Price);
        //GlobalBinder.singleton.ParticleManager.PlayParticleAtPoint(_placeParticleKey, tile.transform.position);
        //GlobalBinder.singleton.AudioManager.PlaySound(_placeSoundKey);
    }

    public void SelectUnit(int index)
    {
        _selectedUnit = GlobalBinder.singleton.UnitManager.GetUnit(index);
    }

    //public void Select(ISelectable selectable)
    //{
    //    throw new System.NotImplementedException();
    //}

    public void Collect(ICollectable collectable)
    {
        collectable.Collect();
    }

    private void ShowUnitDisplayer()
    {
        _unitDisplayerSprite.sprite = _selectedUnit.UnitIcon;
        _unitDisplayer.DOScale(1f, 0.5f);
    }
    private void HideUnitDisplayer()
    {
        _unitDisplayer.DOScale(0f, 0.5f);
    }
}