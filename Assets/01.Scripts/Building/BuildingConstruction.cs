using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    private Material constructionMaterial;
    private float constructionTimer;
    private float constructionTimerMax;

    private BuildingTypeSO buildingType;
    private BoxCollider2D boxCollider;

    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        constructionMaterial = spriteRenderer.material;
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();

        Instantiate(GameAssets.Instance.pfBuildingPlacedParticles, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        constructionTimer -= Time.deltaTime;

        if (constructionTimer <= 0f)
        {
            Debug.Log("Ding!");
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            Instantiate(GameAssets.Instance.pfBuildingPlacedParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        constructionMaterial.SetFloat("_Progress", GetConstructionTimeNormalized());
    }

    public void SetBuildingType(BuildingTypeSO buildingType)
    {
        this.buildingType = buildingType;

        constructionTimerMax = buildingType.constructionTimerMax;
        constructionTimer = buildingType.constructionTimerMax;

        boxCollider.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;

        spriteRenderer.sprite = buildingType.sprite;
        buildingTypeHolder.buildingType = buildingType;
    }

    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        SoundManager.Instance.PlaySound(Sound.BuildingPlaced);

        BuildingConstruction temp = Instantiate(GameAssets.Instance.buildingConstruction, position, Quaternion.identity);
        temp.SetBuildingType(buildingType);

        return temp;
    }

    public float GetConstructionTimeNormalized()
    {
        return 1f - constructionTimer / constructionTimerMax;
    }
}
