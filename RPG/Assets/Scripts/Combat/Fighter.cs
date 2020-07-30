using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float weaponRange = 2f;

        public  Transform target;

        private Mover mover;

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }

        public void CancelAttackTarget()
        {
            target = null;
        }

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
                mover.Stop();
        }

        private float GetTargetDistance()
        {
            return Mathf.Abs(Vector3.Distance(transform.position, target.transform.position));
        }
    }
}