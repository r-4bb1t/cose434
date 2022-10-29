using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowByBpm : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public int bpm;

    void Start()
    {
        animator.speed = bpm / 120;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
