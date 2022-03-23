using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    private CameraController m_Cam;
    // Start is called before the first frame update
    void Start()
    {
        m_Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_Cam.GameLv == 3)
        {
            if (transform.position.y > 0.2f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player temp = other.gameObject.GetComponent<Player>();

            if (!temp.m_isGotCoin)
            {
                temp.m_isGotCoin = true;
                Destroy(gameObject);
            }
        }
    }
}
