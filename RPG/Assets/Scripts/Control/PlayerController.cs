using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
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
                GetComponent<Mover>().StartMovementAction(hit.point);
            return true;
        }

        private bool HandleCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                if (!hit.transform.GetComponent<CombatTarget>()) continue;
                if (Input.GetMouseButtonDown(0))
                    GetComponent<Fighter>().Attack(hit.transform.GetComponent<CombatTarget>());
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