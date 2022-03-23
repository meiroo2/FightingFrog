using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableFishAss : MonoBehaviour
{
    public GameObject m_Parent;
    public Vector3[] m_lv1Pos;
    private int m_curLv = 0;
    private bool m_Lerp = false;
    public GameObject HeadRing;
    public GameObject m_willSpawnRing;

    public int m_HowManyWeed = 0;

    private void Update()
    {
        if (m_Lerp && (m_lv1Pos.Length >= m_curLv))
        {
            m_Parent.transform.position = Vector3.Lerp(m_Parent.transform.position, m_lv1Pos[m_curLv - 1], Time.deltaTime);
            if (Vector3.Distance(m_Parent.transform.position, m_lv1Pos[m_curLv - 1]) < 0.3f)
            {
                m_Lerp = false;
                GetComponent<BoxCollider>().isTrigger = false;
            }
        }

        if(m_HowManyWeed == 3)
        {
            HeadRing.gameObject.SetActive(false);
            m_willSpawnRing.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().GameLv = 3;
            m_HowManyWeed = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Player tempP = collision.gameObject.GetComponent<Player>();
            if (tempP.m_isDash)
            {
                m_curLv += 1;
                m_Lerp = true;
                GetComponent<BoxCollider>().isTrigger = true;
                if (m_curLv == 3)
                    m_Parent.transform.localScale = new Vector3(-m_Parent.transform.localScale.x, m_Parent.transform.localScale.y, m_Parent.transform.localScale.z);
                if(m_curLv == 6)
                    m_Parent.transform.localScale = new Vector3(-m_Parent.transform.localScale.x, m_Parent.transform.localScale.y, m_Parent.transform.localScale.z);
            }
        }
    }
}
