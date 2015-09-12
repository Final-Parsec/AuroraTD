
namespace Assets.Scripts.Turrets
{

	using UnityEngine;
	using System.Collections.Generic;

	public static class TurretUpgrades
	{
		public static Dictionary<string, Upgrade[]> earthUpgrades = new Dictionary<string, Upgrade[]>();

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

			//Quake
			Stat[] stats = {
				new Stat(Attribute.Range, "Range",-1f,"RNG"),
				new Stat(Attribute.RateOfFire,"Rate of Fire",1f,"ROF"),
				new Stat(Attribute.Slow,"Slow",.1f,"SLW"),
				new Stat(Attribute.SlowDuration,"Slow Duration",1f,"SLD")
			};

			Upgrade[] upgrades = new Upgrade[] {
				new Upgrade ("Quake", "A tremendous earth quake that slows enemies!", 30, stats),
				new Upgrade ("Quake", "A tremendous earth quake that slows enemies!", 45, stats),
				new Upgrade ("Quake", "A tremendous earth quake that slows enemies!", 60, stats)
			};

			earthUpgrades.Add("Quake", upgrades);


			//Meteor Shower
			upgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.Range, "Range",-1f,"RNG"),
				new Stat(Attribute.RateOfFire,"Rate of Fire",-1f,"ROF"),
				new Stat(Attribute.AoeDamage,"AOE Damage",1f,"AOD")
			};

			upgrades[0] = new Upgrade ("Meteor Shower", "Meteors that cause splash damage to near by enemies!", 20, stats);

			stats = new Stat[] {
				new Stat (Attribute.AoeDamage, "AOE Damage", 1f, "AOD")
			};
			upgrades[1] = new Upgrade ("Meteor Shower", "Meteors that cause splash damage to near by enemies!", 30, stats);

			stats = new Stat[] {
				new Stat(Attribute.Range, "Range", -1f, "RNG"),
				new Stat(Attribute.AoeDamage,"AOE Damage", 2f,"AOD"),
				new Stat(Attribute.AoeRange,"AOE Radius",1f,"AOE")
			};
			upgrades[2] = new Upgrade ("Meteor Shower", "Meteors that cause splash damage to near by enemies!", 40, stats);

			earthUpgrades.Add("Meteor Shower", upgrades);


			//Root Binding
			upgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.RateOfFire,"Rate of Fire",-1f,"ROF"),
				new Stat(Attribute.Slow,"Slow",.8f,"SLW"),
				new Stat(Attribute.SlowDuration,"Slow Duration",1f,"SLD")
			};
			
			upgrades[0] = new Upgrade ("Root Binding", "Entangle enemies in roots to severly slow them down!", 30, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.RateOfFire,"Rate of Fire",-1f,"ROF"),
				new Stat(Attribute.Slow,"Slow",.1f,"SLW"),
				new Stat(Attribute.SlowDuration,"Slow Duration",1f,"SLD")
			};
			upgrades[1] = new Upgrade ("Root Binding", "Entangle enemies in roots to severly slow them down!", 45, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.Slow,"Slow",.1f,"SLW"),
				new Stat(Attribute.SlowDuration,"Slow Duration",1f,"SLD")
			};
			upgrades[2] = new Upgrade ("Root Binding", "Entangle enemies in roots to severly slow them down!", 60, stats);
			
			earthUpgrades.Add("Root Binding", upgrades);


			//Inferno
			upgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.RateOfFire,"Rate of Fire",1f,"ROF"),
				new Stat(Attribute.Damage,"Damage",-1f,"SLW")
			};
			
			upgrades[0] = new Upgrade ("Inferno", "Devistate enemies with rapidly fired fireballs!", 40, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.RateOfFire,"Rate of Fire",1f,"ROF")
			};
			upgrades[1] = new Upgrade ("Inferno", "Devistate enemies with rapidly fired fireballs!", 60, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.RateOfFire,"Rate of Fire",1f,"ROF")
			};
			upgrades[2] = new Upgrade ("Inferno", "Devistate enemies with rapidly fired fireballs!", 80, stats);
			
			earthUpgrades.Add("Inferno", upgrades);


			//Armageddon
			upgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.AoeDamage,"AOE Damage", 1f,"AOD"),
				new Stat(Attribute.AoeRange,"AOE Radius",1f,"AOE"),
				new Stat(Attribute.RateOfFire,"Rate of Fire",-2f,"ROF")
			};
			
			upgrades[0] = new Upgrade ("Armageddon", "Deal damage to multiple enemies!", 40, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.AoeDamage,"AOE Damage", 2f,"AOD"),
				new Stat(Attribute.AoeRange,"AOE Radius",1f,"AOE"),
				new Stat(Attribute.RateOfFire,"Rate of Fire",-1f,"ROF")
			};
			upgrades[1] = new Upgrade ("Armageddon", "Deal damage to multiple enemies!", 60, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.AoeDamage,"AOE Damage", 2f,"AOD"),
				new Stat(Attribute.AoeRange,"AOE Radius",2f,"AOE")
			};
			upgrades[2] = new Upgrade ("Armageddon", "Deal damage to multiple enemies!", 80, stats);
			
			earthUpgrades.Add("Armageddon", upgrades);


			//Burn
			upgrades = new Upgrade[3];
			stats = new Stat[] {
				new Stat(Attribute.DamageOverTime,"Damage Over Time", 1f,"DOT"),
				new Stat(Attribute.RateOfFire,"Rate of Fire",-1f,"ROF")
			};
			
			upgrades[0] = new Upgrade ("Burn", "Burn an enemy for damage over time!", 20, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.DamageOverTime,"Damage Over Time", 1f,"DOT")
			};
			upgrades[1] = new Upgrade ("Burn", "Burn an enemy for damage over time!", 30, stats);
			
			stats = new Stat[] {
				new Stat(Attribute.DamageOverTime,"Damage Over Time", 2f,"DOT")
			};
			upgrades[2] = new Upgrade ("Burn", "Burn an enemy for damage over time!", 40, stats);
			
			earthUpgrades.Add("Burn", upgrades);


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

			turret.UpgradeTurret(earthUpgrades["Quake"][turret.UpgradeOneLevel], 1);
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

			turret.UpgradeTurret(earthUpgrades["Meteor Shower"][turret.UpgradeTwoLevel], 2);
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

			turret.UpgradeTurret(earthUpgrades["Root Binding"][turret.UpgradeThreeLevel], 3);
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

			turret.UpgradeTurret(earthUpgrades["Inferno"][turret.UpgradeOneLevel], 1);
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

			turret.UpgradeTurret(earthUpgrades["Armageddon"][turret.UpgradeTwoLevel], 2);
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
			
			turret.UpgradeTurret(earthUpgrades["Burn"][turret.UpgradeThreeLevel], 3);
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

			ObjectManager objectManager = ObjectManager.GetInstance();
			int upgradeCost = chainLightningCost;
			upgradeCost += (int)(upgradeCost * turret.UpgradeOneLevel * costScaling);
	        if (upgradeCost > objectManager.gameState.playerMoney)
	            return;
	        objectManager.gameState.playerMoney -= upgradeCost;
	        turret.Msrp += upgradeCost / 2;

	        turret.UpgradeOneLevel++;
	        turret.Level++;

	        Debug.Log("Chain Lightning Upgrade Level " + turret.UpgradeOneLevel);

	        switch (turret.UpgradeOneLevel)
	        {
	            case 1:
	                turret.rateOfFire--;
	                turret.aoeDamage++;
	                turret.aoeRange++;
	                break;
	            case 2:
	                turret.rateOfFire--;
	                turret.DetectionRadius = ++turret.range;
	                turret.aoeDamage++;
	                turret.aoeRange++;
	                break;
	            case 3:
	                turret.DetectionRadius = ++turret.range;
	                turret.aoeDamage++;
	                break;
	        }
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

			ObjectManager objectManager = ObjectManager.GetInstance();
			int upgradeCost = frostCost;
			upgradeCost += (int)(upgradeCost * turret.UpgradeTwoLevel * costScaling);
	        if (upgradeCost > objectManager.gameState.playerMoney)
	            return;
	        objectManager.gameState.playerMoney -= upgradeCost;
	        turret.Msrp += upgradeCost / 2;

	        turret.UpgradeTwoLevel++;
	        turret.Level++;

	        Debug.Log("Frost Upgrade Level " + turret.UpgradeTwoLevel);

	        switch (turret.UpgradeTwoLevel)
	        {
	            case 1:
	                turret.Slow += .1f;
	                turret.SlowDuration++;
	                turret.DetectionRadius = --turret.range;
	                break;
	            case 2:
	                turret.Slow += .1f;
	                turret.DetectionRadius = --turret.range;
	                break;
	            case 3:
	                turret.Slow += .8f;
	                turret.DetectionRadius = --turret.range;
	                break;
	        }
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

			ObjectManager objectManager = ObjectManager.GetInstance();
			int upgradeCost = lightningStrikeCost;
			upgradeCost += (int)(upgradeCost * turret.UpgradeThreeLevel * costScaling);
	        if (upgradeCost > objectManager.gameState.playerMoney)
	            return;
	        objectManager.gameState.playerMoney -= upgradeCost;
	        turret.Msrp += upgradeCost / 2;

	        turret.UpgradeThreeLevel++;
	        turret.Level++;

	        Debug.Log("Lightning Strike Upgrade Level " + turret.UpgradeThreeLevel);

	        switch (turret.UpgradeThreeLevel)
	        {
	            case 1:
	                turret.damage++;
	                turret.rateOfFire--;
	                break;
	            case 2:
	                turret.damage++;
	                turret.DetectionRadius = ++turret.range;
	                break;
	            case 3:
	                turret.damage++;
	                turret.DetectionRadius = ++turret.range;
	                break;
	        }
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

			ObjectManager objectManager = ObjectManager.GetInstance();
			int upgradeCost = poisonCost;
			upgradeCost += (int)(upgradeCost * turret.UpgradeOneLevel * costScaling);
	        if (upgradeCost > objectManager.gameState.playerMoney)
	            return;
	        objectManager.gameState.playerMoney -= upgradeCost;
	        turret.Msrp += upgradeCost / 2;

	        turret.UpgradeOneLevel++;
	        turret.Level++;

	        Debug.Log("Poison Upgrade Level " + turret.UpgradeOneLevel);

	        switch (turret.UpgradeOneLevel)
	        {
	            case 1:
	                turret.DetectionRadius = --turret.range;
	                turret.damageOverTime++;
	                break;
	            case 2:
	                turret.damage++;
	                turret.damageOverTime++;
	                break;
	            case 3:
	                turret.damageOverTime++;
	                break;
	        }
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

			ObjectManager objectManager = ObjectManager.GetInstance();
			int upgradeCost = mindControlCost;
			upgradeCost += (int)(upgradeCost * (turret.UpgradeTwoLevel) * costScaling);
	        if (upgradeCost > objectManager.gameState.playerMoney)
	            return;
	        objectManager.gameState.playerMoney -= upgradeCost;
	        turret.Msrp += upgradeCost / 2;

	        turret.UpgradeTwoLevel++;
	        turret.Level++;

	        Debug.Log("Mind Control Upgrade Level " + turret.UpgradeTwoLevel);

	        switch (turret.UpgradeTwoLevel)
	        {
	            case 1:
	                turret.damage--;
	                turret.MindControlDuration++;
	                break;
	            case 2:
	                turret.rateOfFire--;
	                turret.MindControlDuration++;
	                break;
	            case 3:
	                turret.MindControlDuration++;
	                break;
	        }
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

			ObjectManager objectManager = ObjectManager.GetInstance();
			int upgradeCost = hexCost;
			upgradeCost += (int)(upgradeCost * (turret.UpgradeThreeLevel) * costScaling);
	        if (upgradeCost > objectManager.gameState.playerMoney)
	            return;
	        objectManager.gameState.playerMoney -= upgradeCost;
	        turret.Msrp += upgradeCost / 2;

	        turret.UpgradeThreeLevel++;
	        turret.Level++;

	        Debug.Log("Hex Upgrade Level " + turret.UpgradeThreeLevel);

	        switch (turret.UpgradeThreeLevel)
	        {
	            case 1:
	                turret.damage++;
	                turret.rateOfFire--;
	                break;
	            case 2:
	                turret.damage++;
	                turret.DetectionRadius = ++turret.range;
	                break;
	            case 3:
	                turret.damage++;
	                turret.DetectionRadius = ++turret.range;
	                break;
	        }
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