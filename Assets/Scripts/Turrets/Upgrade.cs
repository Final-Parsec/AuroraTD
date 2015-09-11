using System.Collections;
using System.Collections.Generic;

public class Upgrade {

	public Stat[] stats;
	public float Cost{get;set;}
	public string Name{get;set;}
	public string Description{get;set;}

	public Upgrade(string name, string description, float cost, Stat[] stats)
	{
		this.stats = stats;
		this.Cost = cost;
		this.Description = description;
		this.Name = name;
	}
}
