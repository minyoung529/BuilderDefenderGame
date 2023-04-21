using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    [SerializeField]
    private SkillPanel skillPanelPrefab;

    private List<SkillPanel> skillUIs = new List<SkillPanel>();

    void Start()
    {
        foreach (SkillBase skillBase in SkillManager.Instance.Skills)
        {
            SkillPanel skillUI = Instantiate(skillPanelPrefab, skillPanelPrefab.transform.parent);
            skillUI.Initialize(skillBase);

            skillUIs.Add(skillUI);
        }

        skillPanelPrefab.gameObject.SetActive(false);
    }
}
