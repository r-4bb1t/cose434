using System.Collections;
using UnityEngine;

public class NoteParent : MonoBehaviour
{
    public Rigidbody note;
    public int dir;

    void Start()
    {
        note = GetComponent<Rigidbody>();
        float x = Mathf.Sin(dir * Mathf.Deg2Rad) * 0.05f;
        float y = Mathf.Cos(dir * Mathf.Deg2Rad) * 0.05f;
        //note.velocity = new Vector3(x, y, 0);

        transform.localScale = new Vector3(0, 0, 0);
        transform.Rotate(0f, 0f, dir, Space.Self);
    }

    void Update() {
        transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.z + 0.1f);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
