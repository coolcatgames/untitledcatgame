using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public static event Action OnCollected; 
    public static int total;

    void Awake() => total++;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}
