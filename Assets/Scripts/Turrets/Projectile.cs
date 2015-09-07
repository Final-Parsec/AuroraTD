using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	// Configurable
	public float range;
	public float speed;
	public float Speed { get{return speed * (float)objectManager.gameState.GameSpeed;} set{speed = value;} }
	public EnemyBase target;
	public Vector3 targetPosition;

    public int AoeDamage { get; set; }

    public float AoeRange { get; set; }

	public int Damage { get; set; }

    public int DamageOverTime { get; set; }

    public float Slow { get; set; }
    public float SlowDuration { get; set; }

    public Turret Owner { get; set; }
	
	// Internal
	private float distance;
    private ObjectManager objectManager;
	
	// Runs when entity is Instantiated
	void Awake ()
	{
		distance = 0;
        objectManager = ObjectManager.GetInstance();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 moveVector = new Vector3 (transform.position.x - targetPosition.x,
		                                 transform.position.y - targetPosition.y,
		                                 transform.position.z - targetPosition.z).normalized;
		
		// update the position
		transform.position = new Vector3 (transform.position.x - moveVector.x * Speed * Time.deltaTime,
		                                 transform.position.y - moveVector.y * Speed * Time.deltaTime,
		                                 transform.position.z - moveVector.z * Speed * Time.deltaTime);
		                                 
		distance += Time.deltaTime * Speed;
		
		if (distance > range ||
			Vector3.Distance (transform.position, new Vector3 (targetPosition.x, targetPosition.y, targetPosition.z)) < 1) 
        {
			Destroy (gameObject);
			if (target != null) 
            {
                if (Slow > 0)
                {
					target.Slow(Slow, SlowDuration, (int)Owner.TurretType);
                }

				target.Damage (Damage, (int)Owner.TurretType);
				if(DamageOverTime > 0)
				{
	                if (Owner.TurretType == TurretType.EarthTurret)
	                {
	                    
	                }
	                else if (Owner.TurretType == TurretType.FireTurret)
	                {
						target.DamageOverTime(DamageOverTime, 3.0f, .5f, EnemyState.Burn, (int)Owner.TurretType);
	                }
	                else if (Owner.TurretType == TurretType.StormTurret)
	                {

	                }
	                else if (Owner.TurretType == TurretType.VoodooTurret)
	                {
						target.DamageOverTime(DamageOverTime, 3.0f, .5f, EnemyState.Poison, (int)Owner.TurretType);
	                }  
				}

				if(Owner.MindControlDuration > 0)
				{
					target.MindControl(Owner.MindControlDuration, (int)Owner.TurretType);
				}
              
                if (AoeDamage > 0 && AoeRange > 0)
                {
                    foreach (EnemyBase enemy in objectManager.enemies)
                    {
                        Vector3 temp1 = new Vector3(targetPosition.x, enemy.transform.position.y, targetPosition.z);
                        if (Vector3.Distance(temp1, enemy.transform.position) < AoeRange)
                        {
                            var enemyTargetPosition = enemy.transform.position;
                            GameObject projectileObject = Instantiate(Owner.projectileType, targetPosition, Quaternion.LookRotation(enemyTargetPosition)) as GameObject;
                            Projectile projectile = projectileObject.GetComponent<Projectile>();
                            projectile.Damage = AoeDamage;
                            projectile.target = enemy;
                            projectile.targetPosition = enemyTargetPosition;
                            projectile.Owner = this.Owner;
							projectile.DamageOverTime = DamageOverTime;
                        }
                    }
                }
			}
		}
	}
}