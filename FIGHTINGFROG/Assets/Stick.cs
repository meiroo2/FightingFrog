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
    public Vector2 m_DirectVec;

    private Player m_Player;

    private bool isDragging = false;

    public GameObject m_RightView;
    public GameObject m_LeftView;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        m_OriginalPos = rectTransform.anchoredPosition;
        oldPos = m_OriginalPos;
        newPos = m_OriginalPos;

        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            m_DirectVec = -(m_OriginalPos - lever.anchoredPosition).normalized;
            m_SANS.transform.Translate(-(m_OriginalPos - lever.anchoredPosition) / 700f);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_Player.changeState(true);
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

        if(lever.anchoredPosition.x > 50)
        {
            m_RightView.SetActive(true);
            m_LeftView.SetActive(false);
        }
        else
        {
            m_RightView.SetActive(false);
            m_LeftView.SetActive(true);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_Player.changeState(false);
        isDragging = false;
        lever.anchoredPosition = m_OriginalPos;
    }
}
