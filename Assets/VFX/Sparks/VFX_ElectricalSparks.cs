using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_ElectricalSparks : MonoBehaviour
{
    public float minWaitTime = 2.0f;
    public float maxWaitTime = 5.0f;

    private ParticleSystem particles;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        StartCoroutine(RandomSparks());
    }

    private IEnumerator RandomSparks()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

            if (particles != null)
            {
                particles.Play();
            }
        }
    }
}
