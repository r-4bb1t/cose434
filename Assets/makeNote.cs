using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeNote : MonoBehaviour
{
    public GameObject noteParent;
    NoteParent script;

    void MakeNote()
    {
        GameObject np = Instantiate(noteParent);
        np.transform.position = new Vector3(-10.85f, 2f, -5.409f);

        np.GetComponent<NoteParent>().dir = Random.Range(0, 8) * 45;
    }
    
    void Start()
    {
        InvokeRepeating("MakeNote", 2f, 2f); // 2초뒤 0.3초주기로 ExampleInvokeUse함수 반복 호출
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
