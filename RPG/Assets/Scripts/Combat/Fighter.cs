using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 5;

        private Transform target;
        private float timeSinceLastAttack;
        private Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (GetTargetDistance() >= weaponRange)
                mover.MoveTo(target.position);
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            // Triggers Hit() Event
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }
        
        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("CancelAttack");
            target = null;
        }

        // Animation Event
        public void Hit()
        {
            target.GetComponent<Health>().TakeDamage(weaponDamage);
        }

        private void AttackBehaviour()
        {
            if (!target.GetComponent<Health>().isDead)
                if (timeSinceLastAttack > timeBetweenAttacks)
                {
                    GetComponent<Animator>().SetTrigger("Attack");
                    timeSinceLastAttack = 0;
                }
        }

        private float GetTargetDistance()
        {
            return Mathf.Abs(Vector3.Distance(transform.position, target.transform.position));
        }
    }
}