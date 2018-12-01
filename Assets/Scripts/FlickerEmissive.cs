using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerEmissive : MonoBehaviour
{
    public int materialId;

    private MeshRenderer meshRenderer;
    private Material material;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.materials[materialId];

        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.0f, 0.3f));
            material.EnableKeyword("_EMISSION");
            material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

            yield return new WaitForSeconds(Random.Range(0.0f, 0.3f));
            material.DisableKeyword("_EMISSION");
            material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
        }
    }
}
