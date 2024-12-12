using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomCubesGenerator : MonoBehaviour
{
    List<Vector3> positions = new List<Vector3>();
    public float delay = 3.0f;
    public int objectCount = 10;
    int objectCounter = 0;
    public GameObject block;

    public Material[] materials;

    void Start()
    {
        Bounds platformBounds = GetComponent<Renderer>().bounds;

        List<float> pozycje_x = new List<float>(Enumerable.Range(0, objectCount)
            .Select(_ => UnityEngine.Random.Range(platformBounds.min.x, platformBounds.max.x)));
        List<float> pozycje_z = new List<float>(Enumerable.Range(0, objectCount)
            .Select(_ => UnityEngine.Random.Range(platformBounds.min.z, platformBounds.max.z)));

        for (int i = 0; i < objectCount; i++)
        {
            this.positions.Add(new Vector3(pozycje_x[i], platformBounds.max.y + 1, pozycje_z[i]));
        }

        foreach (Vector3 elem in positions)
        {
            Debug.Log(elem);
        }

        StartCoroutine(GenerujObiekt());
    }

    IEnumerator GenerujObiekt()
    {
        foreach (Vector3 pos in positions)
        {
            GameObject newBlock = Instantiate(this.block, pos, Quaternion.identity);

            if (materials.Length > 0)
            {
                Renderer renderer = newBlock.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = materials[UnityEngine.Random.Range(0, materials.Length)];
                }
            }

            yield return new WaitForSeconds(this.delay);
        }

        StopCoroutine(GenerujObiekt());
    }
}
