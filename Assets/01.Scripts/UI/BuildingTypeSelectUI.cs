using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private List<BuildingTypeSO> ignoreBuildingList;
    private Transform arrowButton;

    private BuildingTypeListSO buildingTypeList;

    private Dictionary<BuildingTypeSO, Transform> resourceTransformDict;

    private void Awake()
    {
        buildingTypeList = GameAssets.Instance.buildingTypeList;
        resourceTransformDict = new Dictionary<BuildingTypeSO, Transform>();

        RectTransform resourceTemplate = transform.Find("ButtonTemplate").GetComponent<RectTransform>();
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;

        arrowButton = Instantiate(resourceTemplate, transform);
        arrowButton.gameObject.SetActive(true);
        arrowButton.Find("Image").GetComponent<Image>().sprite = sprite;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = resourceTemplate.anchoredPosition + new Vector2(120f * index, 0);
        arrowButton.GetComponent<Button>().onClick.AddListener(() => BuildingManager.Instance.SetActiveBuildingType(null));
        arrowButton.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        MouseEnterExitEvents mouseEnterExitEvents = arrowButton.GetComponent<MouseEnterExitEvents>();
        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Show("Arrow Button");
        };
        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Hide();
        };

        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            if (ignoreBuildingList.Contains(buildingType))
            {
                continue;
            }

            Transform resourceTrnasform = Instantiate(resourceTemplate, transform);
            resourceTrnasform.gameObject.SetActive(true);

            float offsetAmount = 120f;
            resourceTrnasform.GetComponent<RectTransform>().anchoredPosition = resourceTemplate.anchoredPosition + new Vector2(offsetAmount * index, 0);

            resourceTrnasform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            Button button = resourceTrnasform.GetComponent<Button>();
            button.onClick.AddListener(() => BuildingManager.Instance.SetActiveBuildingType(buildingType));

            MouseEnterExitEvents events = button.GetComponent<MouseEnterExitEvents>();
            events.OnMouseEnter += (object sender, EventArgs args) =>
            {
                TooltipUI.Instance.Show(buildingType.nameString + '\n' + buildingType.GetConstructionCostString());
            };

            events.OnMouseExit += (object sender, EventArgs args) =>
            {
                TooltipUI.Instance.Hide();
            };

            resourceTransformDict[buildingType] = resourceTrnasform;

            index++;
        }
    }

    private void Start()
    {
        UpdatActiveBuildingType();
        BuildingManager.Instance.OnActiveBuildingTypeChange += OnBuildingTypeChanged;
    }

    private void OnBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeEventArgs e)
    {
        UpdatActiveBuildingType();
    }

    private void UpdatActiveBuildingType()
    {
        arrowButton.Find("Selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            if (ignoreBuildingList.Contains(buildingType)) continue;

            Transform button = resourceTransformDict[buildingType];

            button.Find("Selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if(activeBuildingType)
        {
            Transform active = resourceTransformDict[activeBuildingType];
            active.Find("Selected").gameObject.SetActive(true);
        }
        else
        {
            arrowButton.Find("Selected").gameObject.SetActive(true);
        }
    }
}
