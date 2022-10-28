using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    Renderer m_Renderer;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision hand)
    {
        if (hand.collider.gameObject.CompareTag("Hand"))
            m_Renderer.enabled = true;
    }

    /*     private void OnCollisionStay(Collision hand)
        {
            if (!hand.collider.gameObject.CompareTag("Hand")) return;

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
            }

            if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
            { 
            }
        } */

    private void OnCollisionExit(Collision hand)
    {
        if (hand.collider.gameObject.CompareTag("Hand"))
            m_Renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
