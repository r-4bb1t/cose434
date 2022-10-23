using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Note
{
    public int noteType;
}

[System.Serializable]
public class Beat
{
    public Note[] notes;
}

[System.Serializable]
public class Bar
{
    public Beat[] beats;
}

[System.Serializable]
public class Chart
{
    public string title;
    public int level;
    public string composer;
    public string cover;
    public int bpm;
    public int barNumber;
    public Bar[] bars;
}

[RequireComponent(typeof(AudioSource))]
public class makeNote : MonoBehaviour
{
    public GameObject defaultNote;
    public GameObject longNote;
    public GameObject startNote;
    public GameObject endNote;
    public GameObject UI;
    NoteParent script;
    Chart data;
    int n;
    float deltaTime = 0.0f;
    private AudioSource audioSource;

    void MakeNote()
    {
        for (int i = 0; i < 8; i++)
        {
            Note note = data.bars[0].beats[n].notes[i];
            if (note.noteType == 1)
            {
                GameObject dn = Instantiate(defaultNote);
                dn.transform.SetParent(UI.transform);
                dn.transform.localPosition = Vector3.zero;

                dn.GetComponent<NoteParent>().dir = i * 45;
                Destroy(dn, 2f);
            }
            if (note.noteType == 2)
            {
                GameObject sn = Instantiate(startNote);
                sn.transform.SetParent(UI.transform);
                sn.transform.localPosition = Vector3.zero;

                sn.GetComponent<NoteParent>().dir = i * 45;
                Destroy(sn, 2f);
            }
            if (note.noteType == 3)
            {
                GameObject ln = Instantiate(longNote);
                ln.transform.SetParent(UI.transform);
                ln.transform.localPosition = Vector3.zero;

                ln.GetComponent<NoteParent>().dir = i * 45;
                Destroy(ln, 2f);
            }
            if (note.noteType == 4)
            {
                GameObject en = Instantiate(endNote);
                en.transform.SetParent(UI.transform);
                en.transform.localPosition = Vector3.zero;

                en.GetComponent<NoteParent>().dir = i * 45;
                Destroy(en, 2f);
            }
        }
        n++;
    }

    void SetUI()
    {
        UI.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = data.title;
        UI.transform.Find("Composer").GetComponent<TextMeshProUGUI>().text = data.composer;
        UI.transform.Find("Level").GetComponent<TextMeshProUGUI>().text = "LV " + data.level;
    }

    void setBgm()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        AudioClip audioAsset = (AudioClip)Resources.Load("Audio/emocloche");
        audioSource.clip = (AudioClip)audioAsset;
        audioSource.Play();
    }

    private void Awake()
    {
        Application.targetFrameRate = 40;
    }

    void Start()
    {
        var loadedJson = Resources.Load("JSON/emocloche") as TextAsset;
        data = JsonUtility.FromJson<Chart>(loadedJson.ToString());
        SetUI();
        Invoke("setBgm", 5f);
        InvokeRepeating("MakeNote", 5f - 0.5f, 60f / (float)data.bpm);

    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        UI.transform.Find("FPS").GetComponent<TextMeshProUGUI>().text = "FPS " + (1.0f / deltaTime).ToString();
    }
}
