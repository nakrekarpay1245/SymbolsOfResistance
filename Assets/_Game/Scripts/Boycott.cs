using UnityEngine;

/// <summary>
/// Boycott class holds the information about the boycotted products.
/// </summary>
[System.Serializable]
public class Boycott
{
    /// <summary>
    /// The icon of the boycotted product.
    /// </summary>
    [Tooltip("The icon of the boycotted product")]
    public Sprite BoycotIcon;

    /// <summary>
    /// The name of the boycotted brand.
    /// </summary>
    [Tooltip("The name of the boycotted brand")]
    public string BoycotBrandName;
}