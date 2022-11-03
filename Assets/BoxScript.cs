using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxScript : MonoBehaviour
{
    public float perfect = 0.6f;
    public float good = 0.8f;
    public float miss = 0.9f;
    Queue<GameObject> notes = null;
    Queue<int> noteTypes = null;
    public GameObject ring;
    public GameObject UI;
    public GameObject check;

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
            if (IsPassed(note) && distance > 0.5)
            {
                /* MISS */
                //Debug.Log("MISS OUT");
                UI.transform.Find("Checker").GetComponent<TextMeshPro>().text = "MISS";
                UI.transform.Find("Checker").GetComponent<TextMeshPro>().color = Color.red;

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

    void FixedUpdate() {
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
            //Debug.Log("MISS");
            UI.transform.Find("Checker").GetComponent<TextMeshPro>().text = "MISS";
            UI.transform.Find("Checker").GetComponent<TextMeshPro>().color = Color.red;
        }
        else if (distance > perfect)
        {
            /* GOOD */
           // Debug.Log("GOOD");
            UI.transform.Find("Checker").GetComponent<TextMeshPro>().text = "GOOD";
            UI.transform.Find("Checker").GetComponent<TextMeshPro>().color = Color.blue;
            check.GetComponent<Check>().score += 50;
        }
        else
        {
            /* PERFECT */
            //Debug.Log("PERFECT");
            UI.transform.Find("Checker").GetComponent<TextMeshPro>().text = "PERFECT";
            UI.transform.Find("Checker").GetComponent<TextMeshPro>().color = Color.green;
            check.GetComponent<Check>().score += 100;
        }

        notes.Dequeue();
        noteTypes.Dequeue();
        Destroy(note);
    }

    public void LongTrigger()
    {
        while(notes.Count > 0){
            GameObject note = notes.Peek();
            int noteType = noteTypes.Peek();
            if (noteType < 3) return;
            float distance = Mathf.Abs(FindDistance(note));
            if (distance > miss) return;
            else
            {
                /* PERFECT */
                //Debug.Log("PERFECT LONG");
                UI.transform.Find("Checker").GetComponent<TextMeshPro>().text = "PERFECT";
                UI.transform.Find("Checker").GetComponent<TextMeshPro>().color = Color.green;
                check.GetComponent<Check>().score += 100;
            }

            notes.Dequeue();
            noteTypes.Dequeue();
            Destroy(note);
        }
    }

    public void AddNewNote(GameObject note, int noteType)
    {
        notes.Enqueue(note);
        noteTypes.Enqueue(noteType);
    }
}
