using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    OVRHand hand;
    Renderer m_Renderer;

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<OVRHand>();
        m_Renderer = GetComponent<Renderer>();
    }

    private void OnHandHoverBegin(OVRHand hand) {
        m_Renderer.enabled = true;
    }

    private void OnHandHoverEnd(OVRHand hand) {
        m_Renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
