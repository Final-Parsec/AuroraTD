using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyBase : MonoBehaviour
{
	// Status colors
	private static Color normal = new Color (1,1,1,1);
	private static Color burn = new Color (1,0,0,1); // red
	private static Color poison = new Color (.5f,0,.5f,1); // purple
	private static float statusFlashDuration = .4f;

	private float lastStatusFlashTime;
	public int damageValue;
	public int moneyValue;
	public Vector2 healthBarSize;
	public ObjectManager _ObjectManager;
	public SpriteRenderer spriteRenderer;
	protected Animator animator;
	protected int health;
	public int maxHealth = 100;
	public int armor = 2;
	public int elementType; // 0-3; 0:earth, 1:fire, 2:storm, 3:voodoo

	// Variables used for pathing
	public Node onNode;
	protected float minWaypointDisplacement;
	protected int currentWayPoint = 0;
	protected List<Node> path = null;
	public float speed = 10;
	public int mindControlled = 0; // 0 is walking forward, >0 are stacked mindControle commands.

    public bool StopMindControlling { get; set; }

	// Debuffs
	protected List<Debuff> debuffs = new List<Debuff> ();

	public int Health {
		get {
			return health;
		}
		private set {
			if (health == 0) {
				// We're already dead. Don't do anything.
				// There appeared to some race condition where an enemy would destory itself multiple times.
				return;
			}
		
			if (value < 1) {
				health = 0;
				DestroyThisEntity ();
			} else if (value > maxHealth) {
				health = maxHealth;
			} else {
				health = value;
			}
		}
	}

	// Runs when entity is Instantiated
	void Awake ()
	{
		_ObjectManager = ObjectManager.GetInstance ();
		_ObjectManager.AddEntity (this);
		onNode = _ObjectManager.Map.GetClosestNode (transform.position);
		InitAttributes();
	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		CorrectPosition();
        Move ();
		ApplyDebuffs ();
	}

    private void CorrectPosition()
    {
        // perfect for non mind control
		float correctedY = -((onNode.listIndex.z / _ObjectManager.Map.size_z) + (onNode.listIndex.x / _ObjectManager.Map.size_x));
        transform.position = new Vector3(transform.position.x, correctedY, transform.position.z);
        
    }

	protected void InitAttributes(){
		minWaypointDisplacement = _ObjectManager.Map.nodeSize.x / 10;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator>();
		SetPath (_ObjectManager.Pathfinding.Astar (onNode, _ObjectManager.Map.destinationNode));
		animator.speed = speed;

//		Vector3 correctedPosition = transform.position;
//		correctedPosition.y = ((onNode.listIndex.z) / _ObjectManager._Map.size_z)+1;
//		transform.position = correctedPosition;

		maxHealth += (int)(maxHealth * ((float)_ObjectManager.gameState.dificultyFactor * (float)(_ObjectManager.gameState.waveCount)));
		moneyValue += (int)(moneyValue * _ObjectManager.gameState.enemyValueFactor * (_ObjectManager.gameState.waveCount));
		health = maxHealth;
		speed = speed + Random.Range(-1f,1f);
	}

	/// <summary>
	/// Sets the path.
	/// </summary>
	/// <param name="path">Path.</param>
	public void SetPath (List<Node> path)
	{
		if (path == null || path.Count == 0)
			return;
		
		this.path = path;
		currentWayPoint = path.Count - 1;
		animator.SetInteger("walking", onNode.GetDirection(path[currentWayPoint]));
		// Start walk animation.
		
	}

	// called in update
	// move the unit closer to the next tile in it's path.
	public void Move ()
	{
		if(path == null)
			return;

		SetState((int)State.Walking);

		// don't move in the Y direction.
		Vector3 moveVector = new Vector3 (transform.position.x - path [currentWayPoint].unityPosition.x,
		                                 0,
		                                 transform.position.z - path [currentWayPoint].unityPosition.z).normalized;
		
		// update the position
		transform.position = new Vector3 (transform.position.x - moveVector.x * (speed * (int)_ObjectManager.gameState.gameSpeed) * Time.deltaTime,
		                                 transform.position.y,
		                                 transform.position.z - moveVector.z * (speed * (int)_ObjectManager.gameState.gameSpeed) * Time.deltaTime);
		
		// unit has reached the waypoint
		Vector3 position = transform.position;
		position.y = path [currentWayPoint].unityPosition.y;
		//if (_ObjectManager._Map.GetNodeFromLocation(transform.position) == path[currentWayPoint]) {
		if(Vector3.Distance(position, path [currentWayPoint].unityPosition) <= minWaypointDisplacement){

			onNode = path [currentWayPoint];
			currentWayPoint--;

            if (StopMindControlling)
            {
                debuffs = debuffs.Where(d => d.enemyState != EnemyState.MindControl).ToList();
                mindControlled = 0;
                SetPath(_ObjectManager.Pathfinding.Astar(onNode, _ObjectManager.Map.destinationNode));
                StopMindControlling = false;
                return;
            }

			// unit has reached the destination
			if (currentWayPoint < 0) {
				if(mindControlled > 0){
                    debuffs = debuffs.Where(d => d.enemyState != EnemyState.MindControl).ToList();
                    mindControlled = 0;
                    SetPath(_ObjectManager.Pathfinding.Astar(onNode, _ObjectManager.Map.destinationNode));
					return;
				}
				DestroyThisEntity ();
				return;
			}
			animator.SetInteger("walking", onNode.GetDirection(path[currentWayPoint]));
		}
	}

	/// <summary>
	/// Applies the debuffs to the enemy
	/// </summary>
	public void ApplyDebuffs ()
	{
		List<Debuff> removeList = new List<Debuff> ();
		foreach (Debuff debuff in debuffs)
			if (debuff.Apply (Time.time))
				removeList.Add (debuff);

		foreach (Debuff debuff in removeList) {
			debuffs.Remove (debuff);
			spriteRenderer.color = normal;
		}

		FlashDebuffColor();
	}

	public void FlashDebuffColor ()
	{

		foreach(Debuff debuff in debuffs){

			if(debuff.enemyState == EnemyState.Burn || debuff.enemyState == EnemyState.Poison){
				if(lastStatusFlashTime < Time.time){
					lastStatusFlashTime = Time.time + statusFlashDuration;

					if(spriteRenderer.color == normal){
						switch (debuff.enemyState){
						case EnemyState.Burn:
							spriteRenderer.color = burn;
							break;
						case EnemyState.Poison:
							spriteRenderer.color = poison;
							break;
						}
					}else{
						spriteRenderer.color = normal;
					}
				}

				return;
			}
		}

		if (spriteRenderer.color != normal)
			spriteRenderer.color = normal;


	}

	/// <summary>
	/// Dosn't take armor into acount.
	/// </summary>
	public void DirectDamage (int damage, int towerElement)
	{
		if(towerElement == elementType)
			damage = (int) (damage * .8f); // reduces damage by 20%

		if (damage == 0)
			damage = 1;

		Health -= damage;
	}

	/// <summary>
	/// Reduces the damage the enemy takes by it's armor.
	/// </summary>
	public void Damage (int damage, int towerElement)
	{
		if(towerElement == elementType)
			damage = (int) (damage * .8f);; // reduces damage by 20%

		if (damage == 0)
			damage = 1;

        Health -= damage;
        
        // In case we want armor to be used again.
        //int armorOffsetDamage = damage - armor;
        //if (armorOffsetDamage > 0) {
        //    Health -= armorOffsetDamage;
        //}
	}

	/// <summary>
	/// Damages the enemy over time. Takes into acount the enemy's armor.
	/// </summary>
	public void DamageOverTime (int damage, float duration, float interval, EnemyState enemyState, int towerElement)
	{
		if(towerElement == elementType)
			return;

        int dot = damage;
        if (dot > 0)
        {
            Debuff debuff = new Debuff(this, dot, duration, interval, Time.time, enemyState);
            debuffs.Add(debuff);
        }

        // In case we want armor to be used again.
        //int armorOffsetDamage = damage - armor;

        //if (armorOffsetDamage > 0) {
        //    armorOffsetDamage = armorOffsetDamage / (int)(duration / interval);
        //    Debuff debuff = new Debuff (this, armorOffsetDamage, duration, interval, Time.time, enemyState);
        //    debuffs.Add (debuff);
        //}
	}

	/// <summary>
	/// Reduces the armor.
	/// </summary>
	public void ReduceArmor (int amount, float duration, int towerElement)
	{
		if(towerElement == elementType)
			return;

		Debuff debuff = new Debuff (this, amount, duration, 0, Time.time, EnemyState.ReducedArmor);
		debuffs.Add (debuff);
	}

	/// <summary>
	/// Makes the enemy move backwards along it's path.
	/// </summary>
	public void MindControl (float duration, int towerElement)
	{
		if(towerElement == elementType)
			return;

        if (duration <= 0)
            return;
        
		Debuff debuff = new Debuff (this, 0, duration, 0, Time.time, EnemyState.MindControl);
		debuffs.Add (debuff);
	}

	//Note: percentage is 0-1 not 0-100
	public void Slow (float percentage, float duration, int towerElement)
	{
		if(towerElement == elementType)
			return;

		int amountToSlow = (int)(speed * percentage);
		Debuff debuff = new Debuff (this, amountToSlow, duration, 0, Time.time, EnemyState.Slow);
		debuffs.Add (debuff);
	}

	public virtual Vector2 GetPixelSize ()
	{
		Vector3 start = Camera.main.WorldToScreenPoint (new Vector3 (spriteRenderer.bounds.min.x, spriteRenderer.bounds.min.y, spriteRenderer.bounds.min.z));
		Vector3 end = Camera.main.WorldToScreenPoint (new Vector3 (spriteRenderer.bounds.max.x, spriteRenderer.bounds.max.y, spriteRenderer.bounds.max.z));
		
		int widthX = (int)(end.z - start.z);
		int widthY = (int)(end.y - start.y);
		
		return new Vector2 (widthX, widthY);
	}

	public virtual void DestroyThisEntity ()
	{
		TextMesh deathInt = Instantiate(_ObjectManager.Map.enemyDeathInt, new Vector3 (transform.position.x, 40, transform.position.z), Quaternion.Euler(new Vector3(90, 45, 0))) as TextMesh;
		if(onNode == _ObjectManager.Map.destinationNode){
			_ObjectManager.gameState.PlayerHealth -= damageValue;
			deathInt.text = "-"+damageValue;
		}else if (!_ObjectManager.gameState.gameOver){
			_ObjectManager.gameState.playerMoney += moneyValue;
			_ObjectManager.gameState.score += moneyValue;
			deathInt.text = "+"+moneyValue;
		}

		_ObjectManager.DeReference (this);
		Destroy (gameObject);
	}

	public void SetState(int stateId){
		if(animator.GetInteger("currentState") != stateId){
			animator.SetTrigger("resetState");
			animator.SetInteger("currentState",stateId);
		}
	}
	
}
