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
    public float denseArea = 24.0f;
    public Material material;
    private List<List<Matrix4x4>> HiBatches = new List<List<Matrix4x4>>();
    private List<List<Matrix4x4>> LoBatches = new List<List<Matrix4x4>>();
    public GameObject tree;
    public int treeCount = 150;
    public float treeBuffer = 16.0f;
    public GameObject cloud;
    public int cloudCount = 25;
    public float cloudMinHeight = 40.0f;
    public float cloudMaxHeight = 50.0f;
    public float cloudBuffer = 32.0f;
    public float genArea = 64.0f;
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
                float randX = Random.Range(-denseArea, denseArea);
                float randY = Random.Range(-denseArea, denseArea);
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
                float randX = Random.Range(-genArea, genArea);//, 24.0f)*RandomSign();
                float randY = Random.Range(-genArea, genArea);//, 24.0f)*RandomSign();
                while (randX > -denseArea && randX < denseArea && randY > -denseArea && randY < denseArea)
                {
                    randX = Random.Range(-genArea, genArea);
                    randY = Random.Range(-genArea, genArea);
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


        for (int i = 0; i < treeCount; i++)//tree
        {
            float randX = Random.Range(-genArea, genArea);
            float randY = Random.Range(-genArea, genArea);
            while (randX > -treeBuffer && randX < treeBuffer && randY > -treeBuffer && randY < treeBuffer)
            {
                randX = Random.Range(-genArea, genArea);
                randY = Random.Range(-genArea, genArea);
            }
            Instantiate(tree, new Vector3(randX, terrain.SampleHeight(new Vector3(randX, 0, randY)), randY), Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0)));
        }
        for (int i = 0; i < cloudCount; i++)//cloud
        {
            Instantiate(cloud, new Vector3(Random.Range(-genArea-cloudBuffer, genArea + cloudBuffer), Random.Range(cloudMinHeight, cloudMaxHeight), Random.Range(-genArea - cloudBuffer, genArea + cloudBuffer)), Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0)));
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
