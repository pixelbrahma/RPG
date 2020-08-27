using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Fighter fighter;
        private Mover mover;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (HandleCombat()) return;
            if (HandleMovement()) return;
        }

        private bool HandleMovement()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
                return false;
            if (Input.GetMouseButton(0))
                mover.StartMovementAction(hit.point);
            return true;
        }

        private bool HandleCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!fighter.CanAttack(target.gameObject)) continue;
                if (Input.GetMouseButtonDown(0))
                    fighter.Attack(target.gameObject);
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}