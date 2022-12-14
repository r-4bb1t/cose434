using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteParent : MonoBehaviour
{
    public int dir;

    void Awake(){
        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    void Start()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        transform.Rotate(0f, 0f, dir, Space.Self);
    }

    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + 0.03f, transform.localScale.y + 0.03f, transform.localScale.z + 0.03f);
    }
}
