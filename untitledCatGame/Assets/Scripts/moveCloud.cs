using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class moveCloud : MonoBehaviour
{
    public Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 96)
        {
            transform.position = new Vector3(Random.Range(-88.0f, -104.0f), Random.Range(40.0f, 50.0f), Random.Range(-96.0f, 96.0f));
            transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0));
        }
        transform.position = transform.position + (direction * (speed * Time.deltaTime));
    }
}
