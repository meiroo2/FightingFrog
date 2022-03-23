using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobster : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Player temp = collision.gameObject.GetComponent<Player>();

            if (temp.m_isGotCoin)
            {
                temp.m_HowManyCoins += 1;
                temp.m_isGotCoin = false;
                if(temp.m_HowManyCoins == 4)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().GameLv = 4;
                }

            }
        }
    }
}
