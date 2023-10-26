using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        if(transform.position.x > 50)
        {
            transform.position = new Vector3(Random.Range(-55.0f, -45.0f), Random.Range(20.0f, 30.0f), Random.Range(-50.0f, 50.0f));
            transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(-360f, 360f), 0));
        }
        transform.position = transform.position + (direction * (speed * Time.deltaTime));
    }
}
