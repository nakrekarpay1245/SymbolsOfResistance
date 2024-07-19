using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BoycottListGenerator creates prefabs based on the given boycott list and populates them with relevant information.
/// </summary>
public class BoycottListGenerator : MonoBehaviour
{
    /// <summary>
    /// List of boycott information.
    /// </summary>
    [Header("Boycott List")]
    [Tooltip("List of boycott information")]
    [SerializeField]
    private List<Boycott> boycottList;

    /// <summary>
    /// Prefab to be instantiated for each boycott item.
    /// </summary>
    [Header("Prefab Settings")]
    [Tooltip("Prefab to be instantiated for each boycott item")]
    [SerializeField]
    private GameObject boycottPrefab;

    /// <summary>
    /// Parent object where the boycott prefabs will be instantiated.
    /// </summary>
    [Tooltip("Parent object where the boycott prefabs will be instantiated")]
    [SerializeField]
    private Transform parentTransform;

    /// <summary>
    /// Creates boycott prefabs and populates them with boycott information.
    /// </summary>
    private void Start()
    {
        GenerateBoycottList();
    }

    /// <summary>
    /// Generates the boycott list and populates the prefabs with information.
    /// </summary>
    private void GenerateBoycottList()
    {
        // Clear existing children
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }

        // Iterate through the boycott list and create prefabs
        foreach (Boycott boycott in boycottList)
        {
            GameObject newBoycottItem = Instantiate(boycottPrefab, parentTransform);

            // Find the Icon and Text components
            Image icon = newBoycottItem.transform.Find("BoycottProductIcon").GetComponent<Image>();
            TextMeshProUGUI brandName = newBoycottItem.transform.Find("BoycottProductNameText").GetComponent<TextMeshProUGUI>();

            // Populate the components with boycott information
            if (icon != null)
            {
                icon.sprite = boycott.BoycotIcon;
            }

            if (brandName != null)
            {
                brandName.text = boycott.BoycotBrandName;
            }
        }
    }
}