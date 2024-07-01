using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [Header("Unit Manager")]
    [SerializeField]
    private List<Unit> _unitList;

    public Unit GetUnit(int index)
    {
        Unit currentUnit = _unitList[index];
        int unitPrice = currentUnit.Price;
        if (GlobalBinder.singleton.EconomyManager.IsMoneyEnoughForPurchase(unitPrice))
        {
            return currentUnit;
        }
        Debug.LogWarning("No Money for: " + currentUnit.name + " !!!");
        return null;
    }

    public Unit GenerateUnit(Unit unit)
    {
        Unit generatedUnit = Instantiate(unit, Vector3.zero, Quaternion.identity);
        return generatedUnit;
    }
}