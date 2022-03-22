using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
    // Ãß°¡
    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    private Vector2 m_OriginalPos;

    private Vector2 oldPos;
    private Vector2 newPos;

    public GameObject m_SANS;

    private bool isDragging = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        m_OriginalPos = rectTransform.anchoredPosition;
        oldPos = m_OriginalPos;
        newPos = m_OriginalPos;
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

        // var inputDir = eventData.position - rectTransform.anchoredPosition;

        //var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        newPos = eventData.position;
        if (Vector2.Distance(newPos, m_OriginalPos) < 50f)
        {
            lever.anchoredPosition = eventData.position;
            oldPos = newPos;
        }
        else
        {
            lever.anchoredPosition = oldPos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //var inputDir = eventData.position - rectTransform.anchoredPosition;

        // var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        newPos = eventData.position;
        if (Vector2.Distance(newPos, m_OriginalPos) < 50f)
        {
            lever.anchoredPosition = eventData.position;
            oldPos = newPos;
        }
        else
        {
            lever.anchoredPosition = oldPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        lever.anchoredPosition = m_OriginalPos;
    }
}
