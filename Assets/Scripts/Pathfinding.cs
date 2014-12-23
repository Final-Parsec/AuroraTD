using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
	private ObjectManager _ObjectManager;
	public List<Node> pathToDestination = null;

	public List<Node> Astar (Node start, Node goal)
	{
		if (start == null || goal == null)
			return null;

        if (start == goal)
        {
            return new List<Node>()
            {
                start
            };
        }
		
		// initialize pathfinding variables
		foreach (Node node in _ObjectManager.Map.nodes) {
			node.gScore = int.MaxValue;
			node.fScore = int.MaxValue;
			node.parent = null;
			node.isInOpenSet = false;
			node.isInClosedSet = false;
		}
		
		MinHeap openSet = new MinHeap (start);
		
		start.gScore = 0;
		start.fScore = start.gScore + Heuristic_cost_estimate (start, goal);
		
		while (openSet.heap.Count > 1) {
			// get closest node
			Node current = openSet.GetRoot ();
			
			// if its the goal, return
			if (current == goal)
				return Reconstruct_path (start, goal);
			
			// look at the neighbors of the node
			foreach (Node neighbor in current.getDiagnalNeighbors()) {
				// ignore the ones that are unwalkable or are in the closed set
				if (neighbor != null && neighbor.isWalkable && !neighbor.isInClosedSet) {
					
					// if the new gscore is lower replace it
					int tentativeGscore = current.gScore + 14 + UnityEngine.Random.Range (0, 8);
					if (!neighbor.isInOpenSet || tentativeGscore < neighbor.gScore) {
						
						neighbor.parent = current;
						neighbor.gScore = tentativeGscore;
						neighbor.fScore = neighbor.gScore + Heuristic_cost_estimate (neighbor, goal);
					}
					
					// if neighbor's not in the open set add it
					if (!neighbor.isInOpenSet) {
						openSet.BubbleUp (neighbor);
					}
				}
			}
			
			// look at the neighbors of the node
			foreach (Node neighbor in current.getCloseNeighbors()) {
				// ignore the ones that are unwalkable or are in the closed set
				if (neighbor != null && neighbor.isWalkable && !neighbor.isInClosedSet) {
					
					// if the new gscore is lower replace it
					int tentativeGscore = current.gScore + 10 + UnityEngine.Random.Range (0, 8);
					if (!neighbor.isInOpenSet || tentativeGscore < neighbor.gScore) {
						
						neighbor.parent = current;
						neighbor.gScore = tentativeGscore;
						neighbor.fScore = neighbor.gScore + Heuristic_cost_estimate (neighbor, goal);
					}
					
					// if neighbor's not in the open set add it
					if (!neighbor.isInOpenSet) {
						openSet.BubbleUp (neighbor);
					}
				}
			}
			
		}
		// Fail
		return null;
	}
	
	public int Heuristic_cost_estimate (Node start, Node goal)
	{
		// manhattan heuristic
		int xComponent = (int)Mathf.Abs (start.unityPosition.x - goal.unityPosition.x);
		int zComponent = (int)Mathf.Abs (start.unityPosition.z - goal.unityPosition.z);
		
		int hyp = (int)Math.Sqrt (Math.Pow (xComponent, 2) + Math.Pow (zComponent, 2));
		
		return hyp;
	}

	/// <summary>
	/// Checks Checks that there is a path and updates it's path and the Enemies on the field.
	/// </summary>
	/// <returns><c>true</c>, if there is a valid <c>false</c> otherwise.</returns>
	public bool CheckAndUpdatePaths ()
	{
		Node destination = _ObjectManager.Map.destinationNode;
		Node spawn = _ObjectManager.Map.enemySpawnNode;
		
		pathToDestination = Astar (spawn, destination);
		if (pathToDestination == null)
			return false;
		
		foreach (EnemyBase entity in _ObjectManager.enemies) {
			List<Node> path;

            
			if(entity.mindControlled>0)
            {
                path = Astar(entity.onNode, spawn);
                Debug.Log("Mindcontrolled " + entity.mindControlled);
            }
			else
            {
                path = Astar(entity.onNode, destination);
            }

			if (path == null)
				return false;
			entity.SetPath (path);
		}
		return true;
	}

	/// <summary>
	/// Reconstruct_path the specified start and goal.
	/// </summary>
	/// <param name="start">Start.</param>
	/// <param name="goal">Goal.</param>
	private List<Node> Reconstruct_path (Node start, Node goal)
	{
		List<Node> path = new List<Node> ();
		path.Add (goal);
		
		Node itr = goal;
		while (itr.parent != start) {
			path.Add (itr.parent);
			itr = itr.parent;
		}
		return path;
	}

	// Use this for initialization
	void Start ()
	{
		_ObjectManager = ObjectManager.GetInstance ();
		pathToDestination = Astar (_ObjectManager.Map.enemySpawnNode,
		                          _ObjectManager.Map.destinationNode);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}