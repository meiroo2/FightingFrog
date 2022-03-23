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

    public float StickLimit = 150f;

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
            m_SANS.transform.Translate(-(m_OriginalPos - lever.anchoredPosition) / 900f);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_Player.changeState(true);
        isDragging = true;

        // var inputDir = eventData.position - rectTransform.anchoredPosition;

        //var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        if (Vector2.Distance(eventData.position, m_OriginalPos) < StickLimit)
        {
            lever.anchoredPosition = eventData.position;
        }
        else
        {
            Vector2 temp = (m_OriginalPos - eventData.position).normalized * (StickLimit - 1f);
            lever.anchoredPosition = temp;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        //var inputDir = eventData.position - rectTransform.anchoredPosition;

        // var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        if (Vector2.Distance(eventData.position, m_OriginalPos) < StickLimit)
        {
            lever.anchoredPosition = eventData.position;
            oldPos = lever.anchoredPosition;
        }
        else
        {
            Vector2 temp = (eventData.position - m_OriginalPos).normalized * (StickLimit - 1f);
            temp.x += m_OriginalPos.x;
            temp.y += m_OriginalPos.y;

            /*
            if(oldPos.x < 50f)
            {
                oldPos.x = -oldPos.x;
            }
            if(oldPos.y < 50f)
            {
                oldPos.y = -oldPos.y;
            }
            */
            lever.anchoredPosition = temp;
        }

        if (lever.anchoredPosition.x > m_OriginalPos.x)
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
