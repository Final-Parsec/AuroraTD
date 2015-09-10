using System.Collections;
using System.Collections.Generic;

public class Upgrade {

	public List<Stat> stats = new List<Stat>();
	public float Cost{get;set;}
	public string Name{get;set;}
	public string Description{get;set;}

	public Upgrade(string name, string description, float cost, List<Stat> stats)
	{
		this.stats = stats;
		this.Cost = cost;
		this.Description = description;
		this.Name = name;
	}
}
