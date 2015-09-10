using System.Collections;

public class Stat {
	public enum Attribute
	{
		Start=-1,
		Range,
		RateOfFire,
		AoeDamage,
		AoeRange,
		Damage,
		DamageOverTime,
		End
	}

	public string Name{get;set;}
	public float Value{get;set;}
	public string Acronym{get;set;}

	public Stat (string name, float value, string acronym)
	{
		Name = name;
		Value = value;
		Acronym = acronym;
	}
}
