﻿using UnityEngine.AI;
using UnityEngine;


public class Mover : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MoveToCursor(); 
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            GetComponent<NavMeshAgent>().destination = hit.point;
    }
}
