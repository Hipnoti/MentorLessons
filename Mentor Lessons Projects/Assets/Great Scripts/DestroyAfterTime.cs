using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] float destroyDelayTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyDelayTime);
    }

}
