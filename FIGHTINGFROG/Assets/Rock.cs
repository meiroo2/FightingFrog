using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    int isPushedOnce = 0;
    Rigidbody m_rigid;

    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
        if(isPushedOnce == 1)
        {
            m_rigid.AddForce(Vector3.up * 0.002f, ForceMode.Impulse);
            Invoke(nameof(PushEnd), 0.5f);
        }
        else if (isPushedOnce == 2)
        {
            m_rigid.velocity = Vector3.zero;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isPushedOnce == 0 && collision.gameObject.GetComponent<Player>())
        {
            if (collision.gameObject.GetComponent<Player>().m_isDash)
            {
                m_rigid.mass = 0.001f;
                GetComponent<BoxCollider>().isTrigger = true;
                isPushedOnce = 1;
            }
        }
        else if (collision.gameObject.CompareTag("PosRock"))
        {
            collision.gameObject.GetComponent<PosRock>().doAppear();
            Destroy(this.gameObject);
        }
    }
    void PushEnd()
    {
        GetComponent<BoxCollider>().isTrigger = false;
        isPushedOnce = 2;
        m_rigid.velocity = Vector3.zero;
    }
}
