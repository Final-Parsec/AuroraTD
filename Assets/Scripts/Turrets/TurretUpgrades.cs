
namespace Assets.Scripts.Turrets
{

	using UnityEngine;
	using System.Collections.Generic;

	public static class TurretUpgrades
	{
		public static Dictionary<string, Upgrade[]> upgrades = new Dictionary<string, Upgrade[]>();

		static public int infernoCost = 40;
		static public int armageddonCost = 20;
		static public int burnCost = 30;

		static public int chainLightningCost = 20;
		static public int frostCost = 30;
		static public int lightningStrikeCost = 30;

		static public int poisonCost = 30;
		static public int mindControlCost = 50;
		static public int hexCost = 20;

		static public float costScaling = .5f; // costs of upgrades increase by 50% with each upgrade

		public static void MakeUpgrades()
		{
            upgrades = new Dictionary<string, Upgrade[]>();

			//Quake
			Stat[] stats = {
				new Stat(Attribute.Range,-1f),
				new Stat(Attribute.RateOfFire,1f),
				new Stat(Attribute.Slow,.1f),
				new Stat(Attribute.SlowDuration,1f)
			};

			Upgrade[] tempUpgrades = new Upgrade[] {
				new Upgrade ("Quake", "A tremendous earth quake that slows enemies!", 30, stats),
				new Upgrade ("Quake", "A tremendous earth quake that slows enemies!", 45, stats),
				new Upgrade ("Quake", "A tremendous earth quake that slows enemies!", 60, stats)
			};

			upgrades.Add("Quake", tempUpgrades);


			//Meteor Shower
			tempUpgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.Range,-1f),
				new Stat(Attribute.RateOfFire,-1f),
				new Stat(Attribute.AoeDamage,1f)
			};

			tempUpgrades[0] = new Upgrade ("Meteor Shower", "Meteors that cause splash damage to near by enemies!", 20, stats);

			stats = new Stat[] {
				new Stat (Attribute.AoeDamage, 1f)
			};
			tempUpgrades[1] = new Upgrade ("Meteor Shower", "Meteors that cause splash damage to near by enemies!", 30, stats);

			stats = new Stat[] {
				new Stat(Attribute.Range, -1f),
				new Stat(Attribute.AoeDamage, 2f),
				new Stat(Attribute.AoeRange,1f)
			};
			tempUpgrades[2] = new Upgrade ("Meteor Shower", "Meteors that cause splash damage to near by enemies!", 40, stats);

			upgrades.Add("Meteor Shower", tempUpgrades);


			//Root Binding
			tempUpgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.RateOfFire,-1f),
				new Stat(Attribute.Slow,.8f),
				new Stat(Attribute.SlowDuration,1f)
			};
			
			tempUpgrades[0] = new Upgrade ("Root Binding", "Entangle enemies in roots to severly slow them down!", 30, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.RateOfFire,-1f),
				new Stat(Attribute.Slow,.1f),
				new Stat(Attribute.SlowDuration,1f)
			};
			tempUpgrades[1] = new Upgrade ("Root Binding", "Entangle enemies in roots to severly slow them down!", 45, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.Slow,.1f),
				new Stat(Attribute.SlowDuration,1f)
			};
			tempUpgrades[2] = new Upgrade ("Root Binding", "Entangle enemies in roots to severly slow them down!", 60, stats);
			
			upgrades.Add("Root Binding", tempUpgrades);


			//Inferno
			tempUpgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.RateOfFire,1f),
				new Stat(Attribute.Damage,-1f)
			};
			
			tempUpgrades[0] = new Upgrade ("Inferno", "Devistate enemies with rapidly fired fireballs!", 40, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.RateOfFire,1f)
			};
			tempUpgrades[1] = new Upgrade ("Inferno", "Devistate enemies with rapidly fired fireballs!", 60, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.RateOfFire,1f)
			};
			tempUpgrades[2] = new Upgrade ("Inferno", "Devistate enemies with rapidly fired fireballs!", 80, stats);
			
			upgrades.Add("Inferno", tempUpgrades);


			//Armageddon
			tempUpgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.AoeDamage, 1f),
				new Stat(Attribute.AoeRange,1f),
				new Stat(Attribute.RateOfFire,-2f)
			};
			
			tempUpgrades[0] = new Upgrade ("Armageddon", "Deal damage to multiple enemies!", 40, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.AoeDamage, 2f),
				new Stat(Attribute.AoeRange,1f),
				new Stat(Attribute.RateOfFire,-1f)
			};
			tempUpgrades[1] = new Upgrade ("Armageddon", "Deal damage to multiple enemies!", 60, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.AoeDamage, 2f),
				new Stat(Attribute.AoeRange,2f)
			};
			tempUpgrades[2] = new Upgrade ("Armageddon", "Deal damage to multiple enemies!", 80, stats);
			
			upgrades.Add("Armageddon", tempUpgrades);


			//Burn
			tempUpgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.DamageOverTime, 1f),
				new Stat(Attribute.RateOfFire,-1f)
			};
			
			tempUpgrades[0] = new Upgrade ("Burn", "Burn an enemy for damage over time!", 20, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.DamageOverTime, 1f)
			};
			tempUpgrades[1] = new Upgrade ("Burn", "Burn an enemy for damage over time!", 30, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.DamageOverTime, 2f)
			};
			tempUpgrades[2] = new Upgrade ("Burn", "Burn an enemy for damage over time!", 40, stats);
			
			upgrades.Add("Burn", tempUpgrades);


			//Chain Lightning
			tempUpgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.AoeDamage, 1f),
				new Stat(Attribute.AoeRange,1f),
				new Stat(Attribute.RateOfFire,-1f)
			};
			
			tempUpgrades[0] = new Upgrade ("Chain Lightning", "Damage multiple enemies with a web of lightning!", 20, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.AoeDamage,2f),
				new Stat(Attribute.AoeRange,1f)
			};
			tempUpgrades[1] = new Upgrade ("Chain Lightning", "Damage multiple enemies with a web of lightning!", 30, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.AoeDamage,2f),
				new Stat(Attribute.AoeRange,1f)
			};
			tempUpgrades[2] = new Upgrade ("Chain Lightning", "Damage multiple enemies with a web of lightning!", 40, stats);
			
			upgrades.Add("Chain Lightning", tempUpgrades);


			//Frost
			tempUpgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.Slow, .1f),
				new Stat(Attribute.SlowDuration,1f),
				new Stat(Attribute.Range,-3f)
			};
			
			tempUpgrades[0] = new Upgrade ("Frost", "Freeze your enemies to slow them down!", 20, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.Slow, .1f),
				new Stat(Attribute.SlowDuration,2f)
			};
			tempUpgrades[1] = new Upgrade ("Frost", "Freeze your enemies to slow them down!", 40, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.Slow, .8f),
				new Stat(Attribute.SlowDuration,1f)
			};
			tempUpgrades[2] = new Upgrade ("Frost", "Freeze your enemies to slow them down!", 50, stats);
			
			upgrades.Add("Frost", tempUpgrades);


			//Lightning Strike
			tempUpgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.Damage, 1f),
				new Stat(Attribute.RateOfFire,-1f)
			};
			
			tempUpgrades[0] = new Upgrade ("Lightning Strike", "Strike distant enemies with a bold of lightning!", 30, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.Damage, 1f),
				new Stat(Attribute.Range, 2f)
			};
			tempUpgrades[1] = new Upgrade ("Lightning Strike", "Strike distant enemies with a bold of lightning!", 50, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.Damage, 3f),
				new Stat(Attribute.Range, 1f)
			};
			tempUpgrades[2] = new Upgrade ("Lightning Strike", "Strike distant enemies with a bold of lightning!", 65, stats);
			
			upgrades.Add("Lightning Strike", tempUpgrades);


			//Poison
			tempUpgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.DamageOverTime, 1f),
				new Stat(Attribute.Range,-1f)
			};
			
			tempUpgrades[0] = new Upgrade ("Poison", "Slowly poison enemies!", 20, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.Damage, 2f),
				new Stat(Attribute.DamageOverTime, 1f)
			};
			tempUpgrades[1] = new Upgrade ("Poison", "Slowly poison enemies!", 40, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.DamageOverTime, 3f),
			};
			tempUpgrades[2] = new Upgrade ("Poison", "Slowly poison enemies!", 50, stats);
			
			upgrades.Add("Poison", tempUpgrades);

			//Mind Control
			tempUpgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.Damage, -1f),
				new Stat(Attribute.MindControlDuration,1f)
			};
			
			tempUpgrades[0] = new Upgrade ("Mind Control", "Send enemies back to their spawning ground!", 40, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.MindControlDuration,2f),
				new Stat(Attribute.RateOfFire, -1f)
			};
			tempUpgrades[1] = new Upgrade ("Mind Control", "Send enemies back to their spawning ground!", 70, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.MindControlDuration,3f)
			};
			tempUpgrades[2] = new Upgrade ("Mind Control", "Send enemies back to their spawning ground!", 100, stats);
			
			upgrades.Add("Mind Control", tempUpgrades);


			//Hex
			tempUpgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.Damage, 1f),
				new Stat(Attribute.Range,1f)
			};
			
			tempUpgrades[0] = new Upgrade ("Hex", "Damage enemies with a powerful hex!", 20, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.Damage,1f),
				new Stat(Attribute.Range, 1f)
			};
			tempUpgrades[1] = new Upgrade ("Hex", "Damage enemies with a powerful hex!", 30, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.Damage,2f),
				new Stat(Attribute.Range,2f)
			};
			tempUpgrades[2] = new Upgrade ("Hex", "Damage enemies with a powerful hex!", 40, stats);
			
			upgrades.Add("Hex", tempUpgrades);

		}

	    #region Earth Type Upgrades

	    // Quake (Earth Turrets)
	    //   Level 1: -1 Range, +1 Slow, +1 RoF, +1 Slow Duration
	    //   Level 2: -1 Range, +1 Slow, +1 RoF, +1 Slow Duration
	    //   Level 3: -1 Range, +1 Slow, +1 RoF, +1 Slow Duration
	    public static void QuakeUpgrade(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.EarthTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Quake Upgrade Level " + turret.UpgradeOneLevel);

			turret.UpgradeTurret(upgrades["Quake"][turret.UpgradeOneLevel], 1);
		}
		
		// Meteor Shower
	    //   Level 1: +1 AoE Range, -1 Range, -1 RoF 
	    //   Level 2: +1 AoE Damage
	    //   Level 3: +1 AoE Range, -1 Range
	    public static void MeteorShower(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.EarthTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Meteor Shower Upgrade Level " + turret.UpgradeTwoLevel);

			turret.UpgradeTurret(upgrades["Meteor Shower"][turret.UpgradeTwoLevel], 2);
	    }

	    // Root Binding
	    //   Level 1: -1 RoF, +8 Slow, +1 Slow Duration
	    //   Level 2: -1 RoF, +1 Slow, +1 Slow Duration
	    //   Level 3: +1 Slow, +1 Slow Duration
	    public static void RootBindingUpgrade(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.EarthTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Root Binding Upgrade Level " + turret.UpgradeThreeLevel);

			turret.UpgradeTurret(upgrades["Root Binding"][turret.UpgradeThreeLevel], 3);
		}

	    #endregion

	    #region Fire Type Upgrades

	    // Inferno
	    //   Level 1: +1 RoF, -1 Damage
	    //   Level 2: +1 RoF
	    //   Level 3: +1 RoF
	    public static void InfernoUpgrade(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.FireTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Inferno Upgrade Level " + turret.UpgradeOneLevel);

			turret.UpgradeTurret(upgrades["Inferno"][turret.UpgradeOneLevel], 1);
	    }

	    // Armageddon
	    //   Level 1: +1 AoE, +1 AoE Range, -1 RoF
	    //   Level 2: +1 AoE, +1 AoE Range, -1 RoF
	    //   Level 3: +1 AoE, +1 AoE Range, -1 RoF
	    public static void ArmageddonUpgrade(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.FireTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Armageddon Upgrade Level " + turret.UpgradeTwoLevel);

			turret.UpgradeTurret(upgrades["Armageddon"][turret.UpgradeTwoLevel], 2);
	    }

	    // Burn
	    //   Level 1: + 1 DoT, -1 RoF
	    //   Level 2: + 1 DoT
	    //   Level 3: + 1 DoT
	    public static void BurnUpgrade(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.FireTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Burn Upgrade Level " + turret.UpgradeThreeLevel);
			
			turret.UpgradeTurret(upgrades["Burn"][turret.UpgradeThreeLevel], 3);
	    }

	    #endregion

	    #region Storm Type Upgrades

	    // Chain Lightning
	    //   Level 1: +1 AoE, +1 AoE Range, -1 RoF
	    //   Level 2: +1 AoE,  +1 AoE Range, -1 RoF
	    //   Level 3: +1 AoE
	    public static void ChainLightningUpgrade(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.StormTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Chain Lightning Upgrade Level " + turret.UpgradeOneLevel);

			turret.UpgradeTurret(upgrades["Chain Lightning"][turret.UpgradeOneLevel], 1);
	    }

	    // Frost
	    //   Level 1: +1 Slow, +1 Slow Duration, -1 Range
	    //   Level 2: +1 Slow, -1 Range
	    //   Level 3: +8 Slow, -1 Range
	    public static void FrostUpgrade(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.StormTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Frost Upgrade Level " + turret.UpgradeTwoLevel);

			turret.UpgradeTurret(upgrades["Frost"][turret.UpgradeTwoLevel], 2);

	    }

	    // Lightning Strike
	    //   Level 1: +1 Damage, -1 RoF
	    //   Level 2: +1 Damage, +1 Range
	    //   Level 3: +1 Damage, +1 Range
	    public static void LightningStrikeUpgrade(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.StormTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Lightning Strike Upgrade Level " + turret.UpgradeThreeLevel);
			
			turret.UpgradeTurret(upgrades["Lightning Strike"][turret.UpgradeThreeLevel], 3);
	    }

	    #endregion

	    #region Voodoo Type Upgrades

	    // Poison
	    //   Level 1: +1 DoT, -1 Range
	    //   Level 2: +1 Damage, +1 DoT
	    //   Level 3: +1 DoT
	    public static void PoisonUpgrade(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.VoodooTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Poison Upgrade Level " + turret.UpgradeOneLevel);

			turret.UpgradeTurret(upgrades["Poison"][turret.UpgradeOneLevel], 1);
	    }

	    // Mind Control
	    //   Level 1: +1 Mind Control, -1 Damage
	    //   Level 2: +2 Mind Control, -1 RoF
	    //   Level 3: +3 Mind Control
	    public static void MindControlUpgrade(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.VoodooTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Mind Control Upgrade Level " + turret.UpgradeTwoLevel);

			turret.UpgradeTurret(upgrades["Mind Control"][turret.UpgradeTwoLevel], 2);
	    }
	    
	    // Hex
	    //   Level 1: +1 Armor Reduce, +1 ARD, -1 Damage
	    //   Level 2: +1 ARD
	    //   Level 3:  +1 RoF
	    public static void HexUpgrade(this Turret turret)
	    {
	        if (turret == null || turret.TurretType != TurretType.VoodooTurret)
	            return;

	        if (MaxLevels(turret))
	            return;

	        Debug.Log("Hex Upgrade Level " + turret.UpgradeThreeLevel);

			turret.UpgradeTurret(upgrades["Hex"][turret.UpgradeThreeLevel], 3);
	    }

	    #endregion

	    private static bool MaxLevels(Turret turretToCheck)
	    {
	        return turretToCheck.UpgradeOneLevel +
	            turretToCheck.UpgradeTwoLevel +
	            turretToCheck.UpgradeThreeLevel >= 3;
	    }

		public static string GetUpgradeCost(int cost, int upgradeLevel)
		{
			if (upgradeLevel == 3)
				return "";

			return "("+ (cost+(int)(cost * upgradeLevel * TurretUpgrades.costScaling))+")";
		}
	}
}