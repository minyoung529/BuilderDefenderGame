using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSkillObject : MonoBehaviour
{
    private readonly float DURATION = 2f;
    private readonly int LINE_COUNT = 4;

    [SerializeField]
    private GameObject linePrefab;

    private Line[] lines;

    public Action OnEnd { get; set; }

    private void Awake()
    {
        lines = new Line[LINE_COUNT];
        OnEnd += DestroyData;

        transform.SetParent(Camera.main.transform);
        transform.localPosition = Vector3.zero;
    }

    private void Start()
    {
        float startY = 15f;
        float interval = (startY * 2f) / LINE_COUNT;

        for (int i = 0; i < LINE_COUNT; i++)
        {
            Vector3 localPos = Vector3.zero;
            localPos.y = startY - interval * i;
            localPos.z = 10f;

            Vector3 scale = linePrefab.transform.localScale;
            scale.y -= 0.85f * i;

            Transform line = Instantiate(linePrefab, transform).transform;
            line.localPosition = localPos;
            line.localScale = scale;

            lines[i] = line.GetComponent<Line>();
        }

        SkillManager.Instance.StartCoroutine(PlayLine());
    }

    private IEnumerator PlayLine()
    {
        for (int i = 0; i < LINE_COUNT; i++)
        {
            lines[i].Attack();
            yield return new WaitForSeconds(DURATION / LINE_COUNT);
        }

        for (int i = 0; i < LINE_COUNT; i++)
        {
            lines[i].Attack();
            yield return new WaitForSeconds(0.35f / LINE_COUNT);
        }

        yield return new WaitForSeconds(1f);
        OnEnd.Invoke();
    }

    private void DestroyData()
    {
        for (int i = 0; i < LINE_COUNT; i++)
        {
            lines[i].transform.DOKill();
            Destroy(lines[i].gameObject);
            lines[i] = null;
        }
    }
}
