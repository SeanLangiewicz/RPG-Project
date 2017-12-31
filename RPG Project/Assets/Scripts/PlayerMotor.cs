using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{

    Transform target;

    NavMeshAgent agent;
	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
	}

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
            
     }

    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }
    
    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.raidus * .8f;
        agent.updateRotation = false;
        target = newTarget.interactTransform;
    }

    public void StopFollowTarget()
    {
        agent.stoppingDistance = 0;
        agent.updateRotation = true;
        target = null;
    }
}
