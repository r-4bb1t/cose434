using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glowByBpm : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public int bpm;

    void Start()
    {
      animator.speed = bpm / 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
