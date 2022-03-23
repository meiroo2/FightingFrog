using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosRock : MonoBehaviour
{
    public GameObject m_Child;
    public CameraController m_Camera;
    public void doAppear()
    {
        m_Child.SetActive(true);
        m_Camera.GameLv = 2;
    }
}
