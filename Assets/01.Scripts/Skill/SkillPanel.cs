using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    private SkillBase skillBase;

    [SerializeField]
    private Image coolTimerFill;

    [SerializeField]
    private Image iconImage;

    private readonly Color INACTIVE_COLOR = new Color(0f, 0f, 0f, 0.76f);

    public void Initialize(SkillBase skillBase)
    {
        this.skillBase = skillBase;
        iconImage.sprite = skillBase.SkillSO.icon;
        coolTimerFill.color = Color.clear;
    }

    private void Update()
    {
        // 스킬 중이거나 사용할 수 없다면 비활성화
        if (skillBase.IsSkilling || !skillBase.CanSkill)
        {
            Color color = INACTIVE_COLOR;
            coolTimerFill.color = color;

            if (!skillBase.IsSkilling)
            {
                coolTimerFill.fillAmount = 1f - skillBase.NormalizedValue();
            }
        }
        else
        {
            coolTimerFill.color = Color.clear;
        }
    }
}
