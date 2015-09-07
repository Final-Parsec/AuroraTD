using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Turret : MonoBehaviour
{
	// Configurable
	public float accuracyError = 2.0f;
    public int aoeDamage = 0;
    public int aoeRange = 0;
	public int damage = 10;
    public int damageOverTime = 0;
	public float focusMenuBlue = 0;
	public float focusMenuGreen = 175;
	public float focusMenuRed = 0;
	public GameObject projectileType;
	public int range = 5;
	public int rateOfFire = 5;
	public Sprite selectedSprite;
    public Sprite levelOneSprite;
    public Sprite levelTwoSprite;
    public Sprite levelThreeSprite;
    public Sprite levelOneSpriteSelected;
    public Sprite levelTwoSpriteSelected;
    public Sprite levelThreeSpriteSelected;
	
	// Constants
	private const float MinAttackDelay = 0.1f;
	private const float MaxAttackDelay = 2f;
	
	// Internal
	private Sprite defaultSprite;
	private List<EnemyBase> myTargets;
	private float nextDamageEvent;
	private ObjectManager objectManager;	
	private static readonly object syncRoot = new object ();
	
	// Properties
	private float AttackDelay
	{
		get 
		{
			int inverted = rateOfFire;
			if (rateOfFire == 0) 
            { 
				return float.MaxValue;
			}
			else if (rateOfFire < 5)
            {
				inverted = rateOfFire + 2 * (5 - rateOfFire);
			}
			else if (rateOfFire > 5) 
            {
				inverted = rateOfFire - 2 * (rateOfFire - 5);
			}
			
			return (((float)inverted - 1f) / (10f - 1f)) * (MaxAttackDelay - MinAttackDelay) + .1f;
		}
	}

    public float AoeRange
    {
        get
        {
            float minRange = Mathf.Min(objectManager.Map.nodeSize.x, objectManager.Map.nodeSize.y) * 1.5f;
            float maxRange = minRange * 4f;

            float computedRange = (((float)aoeRange - 1f) / (10f - 1f)) * (maxRange - minRange) + minRange;
            computedRange = computedRange / transform.localScale.x;

            return computedRange;
        }
    }
	
	public float DetectionRadius
	{ 
		get 
        {	
			float minRange = Mathf.Min(objectManager.Map.nodeSize.x, objectManager.Map.nodeSize.y) * 1.5f;
			float maxRange = minRange * 4f;
			
			float detectionRadius = (((float)range - 1f) / (10f - 1f)) * (maxRange - minRange) + minRange;
			detectionRadius = detectionRadius / transform.localScale.x;
			
			return detectionRadius;
		}
		set 
        {
			float minRange = Mathf.Min(objectManager.Map.nodeSize.x, objectManager.Map.nodeSize.y) * 1.5f;
			float maxRange = minRange * 4f;
			
			float detectionRadius = (((float)value - 1f) / (10f - 1f)) * (maxRange - minRange) + minRange;
			detectionRadius = detectionRadius / transform.localScale.x;
			
			SphereCollider collider = transform.GetComponent<SphereCollider> ();
			collider.radius = detectionRadius;
		}
	}

    private int level = 0;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            switch (value)
            {
                case 1:
                    defaultSprite = levelOneSprite;
                    selectedSprite = levelOneSpriteSelected;
                    break;
                case 2:
                    defaultSprite = levelTwoSprite;
                    selectedSprite = levelTwoSpriteSelected;
                    break;
                case 3:
                    defaultSprite = levelThreeSprite;
                    selectedSprite = levelThreeSpriteSelected;
                    break;
            }

            if (this == objectManager.TurretFocusMenu.SelectedTurret)
            {
                this.GetComponent<SpriteRenderer>().sprite = selectedSprite;    
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
            
            level = value;
        }
    }

    public int MindControlDuration { get; set; }
    
    // How much the turret can be sold for. 
    // Based on upgrades, bling, and current market conditions.
    public int Msrp { get; set; }

    public float Slow { get; set; }
    public float SlowDuration { get; set; }

    public TurretType TurretType { get; set; }

    public int UpgradeOneLevel { get; set; }
    public int UpgradeTwoLevel { get; set; }
    public int UpgradeThreeLevel { get; set; }


	// Runs when entity is Instantiated
	void Awake()
	{
		objectManager = ObjectManager.GetInstance ();
		objectManager.AddEntity (this);
	}
	
	public void Deselect()
	{
		this.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        Level = level;
	}
	
	void Fire (EnemyBase myTarget)
	{
        var targetPosition = myTarget.transform.position;
		var aimError = Random.Range (-accuracyError, accuracyError);
		var aimPoint = new Vector3 (targetPosition.x + aimError, targetPosition.y + aimError, targetPosition.z + aimError);
		nextDamageEvent = Time.time + AttackDelay;
		GameObject projectileObject = Instantiate (projectileType, transform.position, Quaternion.LookRotation (targetPosition)) as GameObject;
		Projectile projectile = projectileObject.GetComponent<Projectile> ();
		projectile.Damage = damage;
        projectile.DamageOverTime = damageOverTime;
        projectile.Slow = Slow;
        projectile.SlowDuration = SlowDuration;
		projectile.target = myTarget;
		projectile.targetPosition = aimPoint;
        projectile.Owner = this;

        // Turret is AoE enabled. Do stuff.
        if (aoeDamage > 0 && aoeRange > 0)
        {
            projectile.AoeDamage = aoeDamage;
            projectile.AoeRange = AoeRange;
        }
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "enemy") {	
			myTargets.Add (other.GetComponent<EnemyBase>());
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		lock (syncRoot) {
			if (other != null &&
				myTargets.Select (t => t!= null && t.gameObject).Contains(other.gameObject)) {
				myTargets.Remove (other.GetComponent<EnemyBase>());
			}
		}
		
	}
	
	public void Select()
	{
        this.GetComponent<SpriteRenderer>().sprite = selectedSprite;
	}

	// Use this for initialization
	void Start ()
	{
		defaultSprite = GetComponent<SpriteRenderer>().sprite;
		DetectionRadius = range;
		myTargets = new List<EnemyBase>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(objectManager.gameState.GameSpeed == GameSpeed.Paused)
		{
			nextDamageEvent += Time.deltaTime;
			return;
		}

        if (myTargets.Any())
        {
            EnemyBase myTarget = myTargets.ElementAt(Random.Range(0, myTargets.Count));
            
            
            if (myTarget != null) {
                if (Time.time >= nextDamageEvent)
                {
                    Fire(myTarget);
                }
            }
            else
            {
				nextDamageEvent = Time.time + (AttackDelay * 1f/(float)objectManager.gameState.GameSpeed);
                myTargets.Remove(myTarget);
            }              
        }
			
	}
}