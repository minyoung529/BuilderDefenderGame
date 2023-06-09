using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    public static GameAssets Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.Load<GameAssets>("GameAsset");
            return instance;

        }
    }

    public ArrowProjectile arrowProjectile;
    public BuildingConstruction buildingConstruction;
    public BuildingTypeListSO buildingTypeList;
    public Transform pfBuildingDestroyedParticles;
    public Transform pfBuildingPlacedParticles;
    public Transform pfEnemyDieParticles;
    public ResourceTypeListSO resourceTypeList;
    public Enemy enemy;
    public SkillListSO skillList;

    [Header("Skill")]
    public Transform explosionSkill;
    public IceSkillObject iceSkill;
    public LineSkillObject lineSkill;
    public PianoSkillObject pianoSkill;

    private void Awake()
    {
        instance = this;
    }
}
