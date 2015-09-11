using System.Collections;

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
	public string Name{get;set;}
	public float Value{get;set;}
	public string Acronym{get;set;}

	public Stat (Attribute attribId, string name, float value, string acronym)
	{
		this.AttribId = attribId;
		this.Name = name;
		this.Value = value;
		this.Acronym = acronym;
	}
}
