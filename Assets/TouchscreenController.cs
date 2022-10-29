using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchscreenController : MonoBehaviour
{
    public GameObject fingertip;
    public GameObject game;
    public GameObject ui;
    float touchDistance;
    Vector3 FingertipForward;

    int t = 0;
    void Start()
    {
        FingertipForward = fingertip.transform.TransformDirection(Vector3.forward);
        touchDistance = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (t < 100)
        {
            t++;
        }
        if (Physics.Raycast(fingertip.transform.position, FingertipForward, out RaycastHit ray, touchDistance) && t >= 100)
        {
            Collider rayCollider = ray.collider;
            if (rayCollider.gameObject.name.Equals("Emocloche"))
            {
                rayCollider.gameObject.transform.Find("Light").GetComponent<Light>().enabled = true;
                game.SetActive(true);
                ui.SetActive(false);
            }
        }
    }
}
