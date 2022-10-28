using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;

public class BoxScript : MonoBehaviour
{
    Hand hand;
    Renderer m_Renderer;

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<Hand>();
        m_Renderer = GetComponent<Renderer>();
    }

    private void OnHandHoverBegin(Hand hand)
    {
        m_Renderer.enabled = true;
    }

    private void OnHandHoverEnd(Hand hand)
    {
        m_Renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
