using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Header("Tile Manager Parameters")]
    [Header("Grid Parameters")]
    [Header("Tile Prefab")]
    [SerializeField]
    private Tile _tilePrefab;

    [Header("Grid Scale")]
    [Tooltip("Grid horizontal scale")]
    [SerializeField]
    private int _gridWidth;
    [Tooltip("Grid vertical scale")]
    [SerializeField]
    private int _gridHeight;

    [Header("Tile Spacing")]
    [SerializeField]
    private Vector2 _tileSpacing;

    private Tile[,] _tileGrid;
    public List<Tile> _activeTileList;

    [Header("Tile Parent")]
    [SerializeField]
    private Transform _tileParent;

    private void Awake()
    {
        _tileGrid = new Tile[_gridWidth, _gridHeight];
        _activeTileList = new List<Tile>();
        GenerateTiles();
    }

    /// <summary>
    /// Generates the tiles of the game board based on the grid width and height.
    /// </summary>
    private void GenerateTiles()
    {
        int index = 0;

        // Calculate total grid size including spacing
        float totalWidth = (_gridWidth - 1) * (1 + _tileSpacing.x);
        float totalHeight = (_gridHeight - 1) * (1 + _tileSpacing.y);

        // Calculate the offset to center the grid
        Vector2 gridOffset = new Vector2(totalWidth / 2f, totalHeight / 2f) * -1f;

        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                // Apply spacing to the tile position
                Vector2 tilePosition = new Vector2(x * (1 + _tileSpacing.x), y * (1 + _tileSpacing.y)) + gridOffset;

                // Adjust position to be relative to the TileParent
                Vector3 worldPosition = _tileParent.TransformPoint(tilePosition);

                Tile generatedTile = Instantiate(_tilePrefab, worldPosition, Quaternion.identity, _tileParent);

                Vector2Int tileGridPosition = new Vector2Int(x, y);

                generatedTile.TileGridPosition = tileGridPosition;

                generatedTile.name = "Tile(" + x + "," + y + ") " + index;
                index++;

                _tileGrid[x, y] = generatedTile;
                _activeTileList.Add(generatedTile);

                generatedTile.TileState = TileState.Empty;
            }
        }
    }

    /// <summary>
    /// Retrieves the nearest tile to the specified position based on minimum selection distance.
    /// </summary>
    /// <param name="position">The position to compare with.</param>
    /// <returns>The nearest tile to the position within the minimum selection distance, or null 
    /// if no tile is found.</returns>    
    public Tile GetNearestTile(Vector3 position)
    {
        Tile nearestTile = _activeTileList
                .OrderBy(tile => Vector3.Distance(tile.transform.position, position))
                    .FirstOrDefault(tile => Vector2.Distance(tile.transform.position, position) <
                       GlobalBinder.singleton.GameSettings.MINIMUM_SELECTION_DISTANCE);

        return nearestTile;
    }
}