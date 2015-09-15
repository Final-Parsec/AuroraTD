using System.Collections;
using System.Collections.Generic;

public enum Attribute
{
	Start=-1,
	Range,
	RateOfFire,
	AoeDamage,
	AoeRange,
	Damage,
	DamageOverTime,
	Slow,
	SlowDuration,
	MindControlDuration,
	End
}


public class Stat {
	public Attribute AttribId{get;set;}
	public float Value{get;set;}
	
	public Stat (Attribute attribId, float value)
	{
		this.AttribId = attribId;
		this.Value = value;
	}
}

public class StatInfo {

	public static Dictionary<Attribute, StatInfo> statInfo = new Dictionary<Attribute, StatInfo>()
	{
		{Attribute.Range, new StatInfo("Range", "Rng")},
		{Attribute.RateOfFire, new StatInfo("Rate of Fire", "Rof")},
		{Attribute.AoeDamage, new StatInfo("AOE Damage", "Aod")},
		{Attribute.AoeRange, new StatInfo("AOE Range", "Aor")},
		{Attribute.Damage, new StatInfo("Damage", "Dmg")},
		{Attribute.DamageOverTime, new StatInfo("Damage Over Time", "Dot")},
		{Attribute.Slow, new StatInfo("Slow", "Slw")},
		{Attribute.SlowDuration, new StatInfo("Slow Duration", "Sld")},
		{Attribute.MindControlDuration, new StatInfo("Mind Control Duration", "Mnd")}
	};

	public string Name{get;set;}
	public string Acronym{get;set;}
	
	public StatInfo (string name, string acronym)
	{
		this.Name = name;
		this.Acronym = acronym;
	}
}

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

	public string GetPrettyStats()
	{
		string str = "";

		foreach(Stat stat in stats)
		{
			str += StatInfo.statInfo[stat.AttribId].Acronym + (stat.Value > 0?" +"+stat.Value:" "+stat.Value) + "\t";
		}

		return str;
	}
}
