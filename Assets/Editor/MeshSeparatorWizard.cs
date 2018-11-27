using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class MeshSeparatorWizard : ScriptableWizard
{
    public GameObject parentObject;
    public bool hideOriginalObjects = true;
    public bool destroyOriginalObjects;
    public Material[] materials;

    [MenuItem("Tools/Mesh Separator Wizard")]
    private static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<MeshSeparatorWizard>("Mesh Separator Wizard", "Separate");
    }

    private void OnWizardCreate()
    {
        //Object[] objectsInScene = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Where(obj => HasMeshRenderer(obj)).ToArray();
        Transform[] children = parentObject.GetComponentsInChildren<Transform>().Where(child => HasMeshRenderer(child)).ToArray();
        GameObject[] outputParentGameObjects = new GameObject[materials.Length];

        for (int i = 0; i < outputParentGameObjects.Length; ++i)
        {
            outputParentGameObjects[i] = new GameObject(materials[i].name);
        }

        for (int i = children.Length - 1; i >= 0; --i)
        {
            GameObject gameObject = children[i].gameObject;
            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

            int materialAssigned = GetMaterial(meshRenderer);
            if (materialAssigned != -1)
            {
                GameObject childGameObject = Instantiate(gameObject);

                childGameObject.transform.position = gameObject.transform.position;
                childGameObject.transform.rotation = gameObject.transform.rotation;
                childGameObject.transform.localScale = gameObject.transform.localScale;

                childGameObject.transform.SetParent(outputParentGameObjects[materialAssigned].transform);

                if (hideOriginalObjects)
                {
                    gameObject.SetActive(false);
                }

                if (destroyOriginalObjects)
                {
                    DestroyImmediate(children[i].gameObject);
                }
            }
        }
    }

    private bool HasMeshRenderer(Transform transform)
    {
        GameObject gameObject = transform.gameObject;
        if (gameObject != null && gameObject.GetComponent<MeshRenderer>() != null)
        {
            return true;
        }

        return false;
    }

    private int GetMaterial(MeshRenderer meshRenderer)
    {
        if (meshRenderer != null && meshRenderer.sharedMaterials.Length == 1)
        {
            for (int i = 0; i < materials.Length; ++i)
            {
                if (materials[i] == meshRenderer.sharedMaterials[0])
                {
                    return i;
                }
            }
        }

        return -1;
    }
}
