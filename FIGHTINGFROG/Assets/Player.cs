using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody m_Rigid;
    public Stick m_Stick;
    public bool m_isMoving = false;
    public bool m_isDash = false;

    public bool m_isGotCoin = false;
    public GameObject[] m_Coins;

    public int m_HowManyCoins = 0;

    public GameObject JumpBtn;
    public GameObject DashBtn;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isGotCoin)
        {
            foreach(GameObject element in m_Coins)
            {
                element.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject element in m_Coins)
            {
                element.SetActive(false);
            }
        }

        if (transform.position.y > 52f)
        {
            m_Rigid.useGravity = true;
            JumpBtn.SetActive(true);
            DashBtn.SetActive(false);
        }
        else if(transform.position.y < 52f)
        {
            m_Rigid.useGravity = false;
            JumpBtn.SetActive(false);
            DashBtn.SetActive(true);
        }
    }

    public void Dash()
    {
        m_isDash = true;
        m_Rigid.AddForce(m_Stick.m_DirectVec * 4f, ForceMode.Impulse);
        Invoke(nameof(EndDash), 1f);
    }
    public void Jump()
    {
        m_Rigid.AddForce(Vector3.up * 20f, ForceMode.Impulse);
    }
    public void changeState(bool _isMoving)
    {
        m_isMoving = _isMoving;
        if (m_isMoving)
            m_Rigid.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destroy") && m_isDash)
        {
            //Destroy(collision.gameObject);
        }
        m_Rigid.velocity = Vector3.zero;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destroy") && m_isDash)
        {
            //Debug.Log("Sans");
            //Destroy(collision.gameObject);
        }
    }
    private void EndDash()
    {
        m_isDash = false;
        m_Rigid.velocity = Vector3.zero;
    }
}
