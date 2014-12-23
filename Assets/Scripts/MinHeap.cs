using System.Collections.Generic;
using System;

public class MinHeap
{
	//The heap is treated like it is 1 based not 0
	public List<Node> heap;
	
	public MinHeap (Node root)
	{
		heap = new List<Node> ();
		heap.Add (null); // dummy to make heap 1 based
		heap.Add (root);
		root.isInOpenSet = true;
	}
	
	/// <summary>
	/// Gets the root.
	/// </summary>
	/// <returns>The root.</returns>
	public Node GetRoot ()
	{
		Node root = heap [1];
		root.isInOpenSet = false;
		root.isInClosedSet = true;
		BubbleDown ();
		return root;
	}
	
	/// <summary>
	/// Removes root and bubbles down.
	/// </summary>
	/// <param name="list">List.</param>
	private void BubbleDown ()
	{
		//replace root with right most leaf.
		heap [1] = null;
		heap [1] = heap [heap.Count - 1];
		heap.RemoveAt (heap.Count - 1);
		MinHeapify (1);
		
	}
	
	/// <summary>
	/// Adds an element to the heap and bubbles up.
	/// </summary>
	/// <param name="element">element to add</param>
	public void BubbleUp (Node element)
	{
		element.isInOpenSet = true;
		element.isInClosedSet = false;
		heap.Add (element); 
		
		int child = heap.Count - 1;
		int parent = child / 2;
		while (heap[parent] != null && heap[child].fScore < heap[parent].fScore) {
			Swap (parent, child);
			if (parent == 1)
				return;
			child = parent;
			parent = child / 2;
		}
	}
	
	/// <summary>
	/// Mins the heapify.
	/// </summary>
	/// <param name="index">Index.</param>
	private void MinHeapify (int index)
	{
		int left = 2 * index;
		int right = 2 * index + 1;
		int smallest = index;
		
		if (left < heap.Count && heap [left].fScore < heap [smallest].fScore)
			smallest = left;
		if (right < heap.Count && heap [right].fScore < heap [smallest].fScore)
			smallest = right;
		if (smallest != index) {
			Swap (index, smallest);
			MinHeapify (smallest);
		}
	}
	
	/// <summary>
	/// Swap the specified list items, a and b.
	/// </summary>
	/// <param name="a">The alpha component.</param>
	/// <param name="b">The beta component.</param>
	private void Swap (int a, int b)
	{
		Node temp = heap [a];
		heap [a] = heap [b];
		heap [b] = temp;
	}
}

