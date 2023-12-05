using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementFollow : MonoBehaviour
{
    public Transform Target;
    public float UpdateSpeed = 0.1f;

    private NavMeshAgent Agent;

    private void Awake(){
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start(){
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget(){
        WaitForSeconds Wait = new WaitForSeconds(UpdateSpeed);
        while (enabled) {
            Agent.SetDestination((Target.transform.position) * -1);

            yield return Wait;
        }
    }
}
