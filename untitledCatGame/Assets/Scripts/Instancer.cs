using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instancer : MonoBehaviour
{
    //public int seed = 123456789;
    public GameObject grass;
    public GameObject tree;
    public GameObject cloud;
    public Terrain terrain;

    //CPU instancing is bad, avoid for objects where lighting and collisions aren't necessary (grass, background objects, etc.), this is just a temporary example

    // Start is called before the first frame update
    void Start()
    {
        //Random.seed = seed;
        for (int i = 0; i < 50; i++)//tree
        {
            float randX = Random.Range(-20.0f, 20.0f);
            float randY = Random.Range(-20.0f, 20.0f);
            Instantiate(grass, new Vector3(randX , terrain.SampleHeight(new Vector3(randX, 0, randY)), randY), Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0)));
        }
        for (int i = 0; i < 25; i++)//grass
        {
            float randX = Random.Range(-20.0f, 20.0f);
            float randY = Random.Range(-20.0f, 20.0f);
            while (randX > -10 && randX < 10 && randY > -10 && randY < 10)
            {
                randX = Random.Range(-20.0f, 20.0f);
                randY = Random.Range(-20.0f, 20.0f);
            }
            Instantiate(tree, new Vector3(randX, terrain.SampleHeight(new Vector3(randX, 0, randY)), randY), Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0)));
        }
        for (int i = 0; i < 15; i++)//cloud
        {
            Instantiate(cloud, new Vector3(Random.Range(-50.0f, 50.0f), Random.Range(20.0f, 30.0f), Random.Range(-50.0f, 50.0f)), Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
