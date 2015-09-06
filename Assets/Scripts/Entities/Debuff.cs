using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using System.Threading;

public class Debuff
{

	EnemyBase owner;
	float magnitude;
	public EnemyState enemyState;
	bool appliedEffect = false;
	float originalSpeed;

	// Time stuff
	public float duration;
	float interval;
	float startTime;
	public float nextEvent = 0;

	public Debuff (EnemyBase owner, int magnitude, float duration, float interval, float startTime, EnemyState enemyState)
	{
		this.owner = owner;
		this.magnitude = magnitude;
		this.enemyState = enemyState;
		this.duration = duration;
		this.interval = interval;
		this.startTime = startTime; 
	}

	public bool Apply (float curTime)
	{
		if (curTime > duration + startTime) {
			EndEffect ();
			return true;
		}

		switch (enemyState) {
		case EnemyState.Burn:
			if (nextEvent <= curTime) {
				owner.DirectDamage ((int)magnitude, 1);
				nextEvent = curTime + interval;
			}
			break;

		case EnemyState.Poison:
			if (nextEvent <= curTime) {
				owner.DirectDamage ((int)magnitude, 3);
				nextEvent = curTime + interval;
			}
			break;

		case EnemyState.MindControl:
			if (!appliedEffect) {
                owner.StopMindControlling = false;
				owner.mindControlled++;
				if(owner.mindControlled == 1)
					MoveOwner(owner._ObjectManager.Map.enemySpawnNode);
				appliedEffect = true;
			}
			break;

		case EnemyState.ReducedArmor:
			if (!appliedEffect) {
				if (owner.armor < magnitude)
					magnitude = owner.armor;
				owner.armor -= (int)magnitude;
				appliedEffect = true;
			}
			break;

		case EnemyState.Slow:
			if (!appliedEffect) {
				if (owner.speed < magnitude)
					magnitude = owner.speed; // This makes reverting the speed to normal easier.
				owner.speed -= magnitude;
				appliedEffect = true;
			}
			break;

		}
	
		return false;
	}

	public void EndEffect ()
	{
		switch (enemyState) {
		case EnemyState.Burn:
		case EnemyState.Poison:
			break;
			
		case EnemyState.MindControl:
			owner.mindControlled--;
			if(owner.mindControlled <= 0)
            {
                MoveOwner(owner._ObjectManager.Map.destinationNode);
                owner.StopMindControlling = true;
            }
				
			break;
			
		case EnemyState.ReducedArmor:
			owner.armor += (int)magnitude;
			break;
			
		case EnemyState.Slow:
			owner.speed += magnitude;
			break;
			
		}

	}

	private void MoveOwner(Node end) {

		List<Node> path = owner._ObjectManager.Pathfinding.Astar (owner.onNode, end);
		
		if (path != null)
			owner.SetPath (path);
		else
			Debug.Log("No path availible");

	}
}

