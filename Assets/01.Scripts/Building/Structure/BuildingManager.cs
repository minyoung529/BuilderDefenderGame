using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;

    private Camera mainCam;

    public static BuildingManager Instance { get; private set; }

    public event EventHandler<OnActiveBuildingTypeEventArgs> OnActiveBuildingTypeChange;

    public class OnActiveBuildingTypeEventArgs : EventArgs
    {
        public BuildingTypeSO buildingType;
    }

    [SerializeField]
    private Building hqBuilding;

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    private void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType)
            {
                if (CanSpawnBuilding(activeBuildingType, UtilClass.GetMouseWorldPosition(), out string error))
                {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionCostArray))
                    {
                        InstantiateHarvester(activeBuildingType.prefab.gameObject);
                        ResourceManager.Instance.SpendResources(activeBuildingType.constructionCostArray);
                        //TooltipUI.Instance.Hide();
                    }
                    else
                    {
                        TooltipUI.Instance.Show("�ڿ� ����: " + activeBuildingType.GetConstructionCostString(), true);
                    }
                }
                else
                {
                    TooltipUI.Instance.Show(error, true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 enemySpawnPos = UtilClass.GetMouseWorldPosition() + UtilClass.GetRandomDir() * 6f;
            Enemy.Create(enemySpawnPos);
        }
    }

    private GameObject InstantiateHarvester(GameObject prefab)
    {
        return Instantiate(prefab, UtilClass.GetMouseWorldPosition(), Quaternion.identity);
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChange?.Invoke(this, new OnActiveBuildingTypeEventArgs { buildingType = activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
    {
        BoxCollider2D boxCollider = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] cols = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider.offset, boxCollider.size, 0f);
        if (cols.Length != 0)
        {
            errorMessage = "�ǹ��� ���� �� ���� ��ġ�Դϴ�.";
            return false;
        }

        cols = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);

        foreach (Collider2D col in cols)
        {
            BuildingTypeHolder holder = col.GetComponent<BuildingTypeHolder>();

            if (holder && holder.buildingType == buildingType)
            {
                errorMessage = "���� ������ �ǹ��� �ֺ��� �ֽ��ϴ�.";
                return false;
            }
        }

        float maxConstructionRadius = 25f;
        cols = Physics2D.OverlapCircleAll(position, maxConstructionRadius);

        foreach (Collider2D col in cols)
        {
            BuildingTypeHolder holder = col.GetComponent<BuildingTypeHolder>();

            if (holder)
            {
                errorMessage = "";
                return true;
            }
        }

        errorMessage = "�ֺ��� �ǹ��� �־�� �մϴ�.";
        return false;
    }

    public Building GetHQBuilding()
    {
        return hqBuilding;
    }
}
