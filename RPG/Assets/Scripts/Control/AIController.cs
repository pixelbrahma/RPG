using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        private GameObject player;
        private Fighter fighter;
        //private Mover mover;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            //mover = GetComponent<Mover>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            HandleAICombat();
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }

        private void HandleAICombat()
        {
            if (DistanceToPlayer() > chaseDistance || !fighter.CanAttack(player))
            {
                fighter.Cancel();
                return;
            }
            
            fighter.Attack(player);
        }
    }
}
