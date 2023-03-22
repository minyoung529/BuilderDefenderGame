using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionCostArray;

    public string GetConstructionCostString()
    {
        string str = "";

        foreach(ResourceAmount res in constructionCostArray)
        {
            str += "<color=#" + res.resourceType.colorHex + ">" + res.resourceType.nameShort + res.amount + "</color> ";
        }

        return str;
    }
}
