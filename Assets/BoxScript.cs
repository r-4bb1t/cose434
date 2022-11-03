using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public float perfect = 0.2f;
    public float good = 0.3f;
    public float miss = 0.4f;
    Queue<GameObject> notes = null;
    Queue<int> noteTypes = null;
    public GameObject ring;

    // Start is called before the first frame update
    void Start()
    {
        notes = new Queue<GameObject>();
        noteTypes = new Queue<int>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == gameObject.name)
                {
                    Trigger();
                }
            }
        }

        while (notes.Count > 0)
        {
            GameObject note = notes.Peek();
            float distance = Mathf.Abs(FindDistance(note));
            if (IsPassed(note) && distance > miss)
            {
                /* MISS */
                //Debug.Log("MISS OUT");
                notes.Dequeue();
                noteTypes.Dequeue();
                Destroy(note);
            }
            else
            {
                break;
            }
        }
    }

    /*GameObject FindClosestNote()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Note");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }*/

    float FindDistance(GameObject note)
    {
        Vector3 scale = note.transform.localScale;
        return (1.0f - scale.x);
    }

    bool IsPassed(GameObject note)
    {
        return note.transform.localScale.x > 1.0f;
    }

    public void Trigger()
    {
        /*GameObject nearest = FindClosestNote();
        if (nearest == null) return;
        Debug.Log(nearest);
        Destroy(nearest.gameObject);*/

        if (notes.Count < 1) return;
        GameObject note = notes.Peek();
        float distance = Mathf.Abs(FindDistance(note));
        if (distance > miss) return;
        if (distance > good)
        {
            /* MISS */
            Debug.Log("MISS");
        }
        else if (distance > perfect)
        {
            /* GOOD */
            Debug.Log("GOOD");
        }
        else
        {
            /* PERFECT */
            Debug.Log("PERFECT");
        }
        notes.Dequeue();
        noteTypes.Dequeue();
        Destroy(note);
    }

    public void LongTrigger()
    {
        if (notes.Count < 1) return;
        GameObject note = notes.Peek();
        int noteType = noteTypes.Peek();
        if (noteType < 3) return;
        float distance = Mathf.Abs(FindDistance(note));
        if (distance > miss) return;
        else
        {
            /* PERFECT */
            Debug.Log("PERFECT LONG");
        }
        notes.Dequeue();
        noteTypes.Dequeue();
        Destroy(note);
    }

    public void AddNewNote(GameObject note, int noteType)
    {
        notes.Enqueue(note);
        noteTypes.Enqueue(noteType);
    }
}
