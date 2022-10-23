using System.Collections;
using UnityEngine;

public class NoteParent : MonoBehaviour
{
    public int dir;

    void Start()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        transform.Rotate(0f, 0f, dir, Space.Self);
    }

    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + 0.05f, transform.localScale.y + 0.05f, transform.localScale.z + 0.05f);
    }
}
