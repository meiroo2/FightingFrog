using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMan : MonoBehaviour
{
    private int count = 2;

    private bool canTouch = true;
    private float Timer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canTouch)
        {
            Timer -= Time.deltaTime;
            if(Timer <= 0f)
            {
                Timer = 1f;
                canTouch = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && count > 0 && canTouch)
        {
            canTouch = false;
            count -= 1;
            if(count == 1)
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 20f, ForceMode.Impulse);
            }
            else if(count == 0)
            {
                GetComponent<BoxCollider>().isTrigger = true;
                GetComponent<Rigidbody>().AddForce(Vector3.right * 20f, ForceMode.Impulse);
            }
        }
    }
}
