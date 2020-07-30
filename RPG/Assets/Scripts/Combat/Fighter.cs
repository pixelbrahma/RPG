﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;

        private Transform target;

        private Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
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
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }
        
        public void Cancel()
        {
            target = null;
        }

        // Animation Event
        public void Hit()
        {

        }
        
        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }

        private float GetTargetDistance()
        {
            return Mathf.Abs(Vector3.Distance(transform.position, target.transform.position));
        }
    }
}