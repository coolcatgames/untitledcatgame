using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public LayerMask HidableLayers;
    public EnemyLineOfSightChecker LineOfSightChecker;
    public NavMeshAgent Agent;
    [Range(-1, 1)]
    [Tooltip("The lower the number, the better the hiding spot")]
    public float HideSensitivity = 0;

    private Coroutine MovementCoroutine;
    private Collider[] Colliders = new Collider[10]; // Larger collider arrays are less performant but give more options for hiding

    private void Awake(){
        Agent = GetComponent<NavMeshAgent>();

        LineOfSightChecker.OnGainSight += HandleGainSight;
        LineOfSightChecker.OnLoseSight += HandleLoseSight;
    }

    private void HandleGainSight(Transform Target){
        if (MovementCoroutine != null){
            StopCoroutine(MovementCoroutine);
        }

        MovementCoroutine = StartCoroutine(Hide(Target));
    }

    private void HandleLoseSight(Transform Target){
        if (MovementCoroutine != null){
            StopCoroutine(MovementCoroutine);
        }
        // Whenever the agent loses sight
        //      Will stop running hide coroutine and stand still
        //          - Might want to add more interesting behavior in future
    }

    private IEnumerator Hide(Transform Target){
        while(true){
            // \/ All the geometry we might want to hide behind
            int hits = Physics.OverlapSphereNonAlloc(Agent.transform.position, LineOfSightChecker.Collider.radius, Colliders, HidableLayers);

            for(int i=0;i < hits; i++){
                if (NavMesh.SamplePosition(Colliders[i].transform.position, out NavMeshHit hit, 2f, Agent.areaMask)){
                    if (!NavMesh.FindClosestEdge(hit.position, out hit, Agent.areaMask)){
                        Debug.LogError($"Unable to find an edge close to {hit.position}");   
                    }
                    
                    if (Vector3.Dot(hit.normal, (Target.position - hit.position).normalized) < HideSensitivity){
                        Agent.SetDestination(hit.position);
                        break;
                    }
                    else {
                        // Side we chose was facing the player, we will try again on the other side of the object
                        // 2 here assumes the object is 2 units wide or narrower. We might need to change it for our game.
                        if (NavMesh.SamplePosition(Colliders[i].transform.position - (Target.position - hit.position).normalized * 1, out NavMeshHit hit2, 2f, Agent.areaMask)){
                            if (!NavMesh.FindClosestEdge(hit2.position, out hit2, Agent.areaMask)){
                                Debug.LogError($"Unable to find an edge close to {hit2.position} (second attempt)");   
                            }
                    
                            if (Vector3.Dot(hit2.normal, (Target.position - hit2.position).normalized) < HideSensitivity){
                                Agent.SetDestination(hit2.position);
                                break;
                            }
                        }
                    }
                }
                else {
                    Debug.LogError($"Unable to find a NavMesh near the object {Colliders[i].name} at {Colliders[i].transform.position}");
                }
            }
            yield return null; 
        }
    }

}
