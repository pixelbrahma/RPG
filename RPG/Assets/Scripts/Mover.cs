using UnityEngine.AI;
using UnityEngine;


public class Mover : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        GetComponent<NavMeshAgent>().destination = target.position;
    }
}
