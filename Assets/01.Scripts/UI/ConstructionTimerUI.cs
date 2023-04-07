using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField]
    private BuildingConstruction buildingConstruction;

    [SerializeField]
    private Image constructionProgressImage;

    void Update()
    {
        constructionProgressImage.fillAmount = buildingConstruction.GetConstructionTimeNormalized();  
    }
}
