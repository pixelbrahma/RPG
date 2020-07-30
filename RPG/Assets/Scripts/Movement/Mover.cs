using UnityEngine.AI;
using UnityEngine;

using RPG.Combat;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private Fighter fighter;

        public void StartMovementAction(Vector3 destination)
        {
            fighter.CancelAttackTarget();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = destination;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
    }
}