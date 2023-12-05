using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Instancer : MonoBehaviour
{
    //public int seed = 123456789;
    public Mesh grass;
    public Mesh grass2;
    public int instance = 10;
    public Material material;
    private List<List<Matrix4x4>> HiBatches = new List<List<Matrix4x4>>();
    private List<List<Matrix4x4>> LoBatches = new List<List<Matrix4x4>>();
    public GameObject tree;
    public GameObject cloud;
    public Terrain terrain;

    //CPU instancing is bad, avoid for objects where lighting and collisions aren't necessary (grass, background objects, etc.), this is just a temporary example

    // Start is called before the first frame update
    void Start()
    {
        //Random.seed = seed;

        int matCount = 0;
        //main area
        for (int i = 0; i< instance; i++)
        {
            if(matCount < 1000 && HiBatches.Count != 0)
            {
                float randX = Random.Range(-24.0f, 24.0f);
                float randY = Random.Range(-24.0f, 24.0f);
                HiBatches[HiBatches.Count - 1].Add(Matrix4x4.TRS(new Vector3(randX, terrain.SampleHeight(new Vector3(randX, 0, randY))+0.3f, randY), Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0)), new Vector3(0.2f, 0.2f, 0.2f)));
                matCount+=1;
            }
            else
            {
                HiBatches.Add(new List<Matrix4x4>());
                matCount = 0;
            }
        }
        //background area
        matCount = 0;
        for (int i = 0; i < instance; i++)
        {
            if (matCount < 1000 && LoBatches.Count != 0)
            {
                float randX = Random.Range(-64.0f, 64.0f);//, 24.0f)*RandomSign();
                float randY = Random.Range(-64.0f, 64.0f);//, 24.0f)*RandomSign();
                while (randX > -24 && randX < 24 && randY > -24 && randY < 24)
                {
                    randX = Random.Range(-64.0f, 64.0f);
                    randY = Random.Range(-64.0f, 64.0f);
                }
                LoBatches[LoBatches.Count - 1].Add(Matrix4x4.TRS(new Vector3(randX, terrain.SampleHeight(new Vector3(randX, 0, randY)) + 0.3f, randY), Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0)), new Vector3(0.2f, 0.2f, 0.2f)));
                matCount += 1;
            }
            else
            {
                LoBatches.Add(new List<Matrix4x4>());
                matCount = 0;
            }
        }


        for (int i = 0; i < 150; i++)//tree
        {
            float randX = Random.Range(-64.0f, 64.0f);
            float randY = Random.Range(-64.0f, 64.0f);
            while (randX > -16 && randX < 16 && randY > -16 && randY < 16)
            {
                randX = Random.Range(-64.0f, 64.0f);
                randY = Random.Range(-64.0f, 64.0f);
            }
            Instantiate(tree, new Vector3(randX, terrain.SampleHeight(new Vector3(randX, 0, randY)), randY), Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0)));
        }
        for (int i = 0; i < 25; i++)//cloud
        {
            Instantiate(cloud, new Vector3(Random.Range(-96.0f, 96.0f), Random.Range(40.0f, 50.0f), Random.Range(-96.0f, 96.0f)), Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        RenderBatches();
    }

    void RenderBatches()
    {
        foreach(var Batch in HiBatches)
        {
            Graphics.DrawMeshInstanced(grass, 0, material, Batch);
        }
        foreach (var Batch in LoBatches)
        {
            Graphics.DrawMeshInstanced(grass2, 0, material, Batch);
        }
    }

    int RandomSign()
    {
        return (Random.Range(0,2) *2)-1;
    }
}
