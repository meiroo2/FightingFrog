using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WEED : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PushFish"))
        {
            collision.gameObject.GetComponentInChildren<PushableFishAss>().m_HowManyWeed += 1;
            Destroy(gameObject);
        }
    }
}
