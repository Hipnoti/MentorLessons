using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public Animator gateAnimator;
    public AudioSource gateAudioSource;

    public void OpenGate()
    {
        gateAnimator.enabled = true;
        gateAudioSource.Play();
    }
}
