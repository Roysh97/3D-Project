using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : Army
{
    protected Camera myCam;
    public NavMeshAgent agent;
    public LayerMask ground;
    public LayerMask redTeam;
    public LayerMask blueTeam;

    protected Vector3 distToHitPoint;
    protected Vector3 distToFire;

    public GameObject triggerMovement;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                agent.SetDestination(hit.point);
                distToHitPoint = hit.point;
            }
        }

    }


}
