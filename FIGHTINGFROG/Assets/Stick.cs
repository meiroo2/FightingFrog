using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
    // �߰�
    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    private Vector2 m_OriginalPos;

    public GameObject m_SANS;

    private bool isDragging = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        m_OriginalPos = rectTransform.anchoredPosition;
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            m_SANS.transform.Translate(-(m_OriginalPos - lever.anchoredPosition) / 1000f);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        //�߰�
        var clampedDir = inputDir.magnitude < leverRange ?
            inputDir : inputDir.normalized * leverRange;

        //lever.anchoredPosition = inputDir;

        lever.anchoredPosition = eventData.position;
        //lever.anchoredPosition = clampedDir;    // ����
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        // �߰�
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        lever.anchoredPosition = eventData.position;
        //lever.anchoredPosition = inputDir;
        //lever.anchoredPosition = clampedDir;    // ����
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        lever.anchoredPosition = m_OriginalPos;
    }
}
