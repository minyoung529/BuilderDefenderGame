using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameObject;
    [SerializeField] private ResourceNearbyOverlay resourceNearbyOverlay;

    private void Awake()
    {
        spriteGameObject = transform.Find("Visual").gameObject;
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChange += OnActiveBuildingTypeChange;
        Hide();
    }

    void Update()
    {
        transform.position = UtilClass.GetMouseWorldPosition();
    }

    private void OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeEventArgs e)
    {
        if (e.buildingType)
        {
            Show(e.buildingType.sprite);
            resourceNearbyOverlay?.Show(e.buildingType.resourceGeneratorData);
        }
        else
        {
            Hide();
            resourceNearbyOverlay?.Hide();
        }
    }

    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }

    private void Show(Sprite sprite)
    {
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        spriteGameObject.SetActive(true);
    }
}
