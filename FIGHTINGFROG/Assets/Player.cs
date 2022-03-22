using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody m_Rigid;
    public Stick m_Stick;
    public bool m_isMoving = false;
    public bool m_isDash = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dash()
    {
        m_isDash = true;
        m_Rigid.AddForce(m_Stick.m_DirectVec * 4f, ForceMode.Impulse);
        Invoke(nameof(EndDash), 1f);
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
