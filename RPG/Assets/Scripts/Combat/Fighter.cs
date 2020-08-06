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
        private Animator animator;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
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

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) return false;
            return (combatTarget.GetComponent<Health>() != null && !combatTarget.GetComponent<Health>().isDead);
        }

        public void Attack(CombatTarget combatTarget)
        {
            // Triggers Hit() Event
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }
        
        public void Cancel()
        {
            ManageAnimatorTriggerCalls("Attack", "CancelAttack");
            target = null;
        }

        // Animation Event
        public void Hit()
        {
            if (!target || !target.GetComponent<Health>()) return;
            target.GetComponent<Health>().TakeDamage(weaponDamage);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target);

            if (!target.GetComponent<Health>().isDead)
                if (timeSinceLastAttack > timeBetweenAttacks)
                {
                    ManageAnimatorTriggerCalls("CancelAttack", "Attack");
                    timeSinceLastAttack = 0;
                }
        }

        private void ManageAnimatorTriggerCalls(string triggerToReset, string triggerToSet)
        {
            animator.ResetTrigger(triggerToReset);
            animator.SetTrigger(triggerToSet);
        }

        private float GetTargetDistance()
        {
            return Mathf.Abs(Vector3.Distance(transform.position, target.transform.position));
        }
    }
}