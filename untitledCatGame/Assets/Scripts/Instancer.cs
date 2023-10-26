using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instancer : MonoBehaviour
{
    //public int seed = 123456789;
    public GameObject grass;
    public GameObject tree;
    public GameObject cloud;

    //CPU instancing is bad, avoid for objects where lighting and collisions aren't necessary (grass, background objects, etc.), this is just a temporary example

    // Start is called before the first frame update
    void Start()
    {
        //Random.seed = seed;
        for (int i = 0; i < 50; i++)//tree
        {
            Instantiate(grass, new Vector3(Random.Range(-20.0f, 20.0f),0.0f, Random.Range(-20.0f, 20.0f)), Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0)));
        }
        for (int i = 0; i < 25; i++)//grass
        {
            float x = Random.Range(-20.0f, 20.0f);
            float y = Random.Range(-20.0f, 20.0f);
            while (x>-10 && x<10 && y>-10 && y<10)
            {
                x = Random.Range(-20.0f, 20.0f);
                y = Random.Range(-20.0f, 20.0f);
            }
            Instantiate(tree, new Vector3(x, 0.0f, y), Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0)));
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
