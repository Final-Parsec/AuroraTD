using UnityEngine;
using System;
using System.Collections.Generic;

public class Node
{
	public Turret turret = null;

	public bool isWalkable;
	public bool isBuildable;

	// Use border enum to access tiles.
	public Node[] borderTiles = new Node[8];
	public Vector3 unityPosition;
	public Vector3 listIndex;

	// A* variables
	public int gScore = int.MaxValue; // undefined 
	public int fScore = int.MaxValue; // undefined 
	public bool isInOpenSet = false;
	public bool isInClosedSet = false;
	public Node parent = null;
	
	public Node ()
	{
	}

	public Node (bool isWalkable, bool isBuildable, Vector3 unityPosition, Vector3 listIndex)
	{
		this.isWalkable = isWalkable;
		this.isBuildable = isBuildable;
		this.unityPosition = unityPosition;
		this.listIndex = listIndex;
	}
	
	public Node clone ()
	{
		return new Node (isWalkable, isBuildable, unityPosition, listIndex);
	}

	public void setUnityPosition (Vector3 unityPosition)
	{
		this.unityPosition = unityPosition;
	}

	public void MakeWalkable ()
	{
		isWalkable = true;
	}

	public void MakeBuildable ()
	{
		isBuildable = true;
	}
	
	public Node[] getDiagnalNeighbors ()
	{
		return new Node[4] {borderTiles [(int)Border.downLeft],
								borderTiles [(int)Border.downRight],
								borderTiles [(int)Border.upLeft],
								borderTiles [(int)Border.upRight]};
	}

	public List<Node> getDiagnalWalkableNeighbors ()
	{
		List<Node> returnList = new List<Node>();
		
		if(borderTiles [(int)Border.Left]!=null && borderTiles [(int)Border.Left].isWalkable){
			if(borderTiles [(int)Border.Up]!=null &&borderTiles [(int)Border.Up].isWalkable)
				returnList.Add(borderTiles [(int)Border.upLeft]);
			if(borderTiles [(int)Border.Down]!=null &&borderTiles [(int)Border.Down].isWalkable)
				returnList.Add(borderTiles [(int)Border.downLeft]);
		}

		if(borderTiles [(int)Border.Right]!=null &&borderTiles [(int)Border.Right].isWalkable){
			if(borderTiles [(int)Border.Up]!=null &&borderTiles [(int)Border.Up].isWalkable)
				returnList.Add(borderTiles [(int)Border.upRight]);
			if(borderTiles [(int)Border.Down]!=null &&borderTiles [(int)Border.Down].isWalkable)
				returnList.Add(borderTiles [(int)Border.downRight]);
		}
		
		return returnList;
	}

	public Node[] getCloseNeighbors ()
	{
		return new Node[4] {borderTiles [(int)Border.Left],
								borderTiles [(int)Border.Right],
								borderTiles [(int)Border.Down],
								borderTiles [(int)Border.Up]};
	}

	public int GetDirection(Node node)
	{
        if (node == this)
        {
            return (int)Border.Right;
        }

		for(int index = 0; index < borderTiles.Length; index++){
			if(node == borderTiles[index])
				return index;
		}
		return (int)Border.Center;
		
	}
}

