using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Map : MonoBehaviour
{
	public TextMesh enemyDeathInt;

	// Prefabs, GameObjects, and Textures
	public List<GameObject> enemyPrefabs;
	public List<GameObject> bossPrefabs;
	public List<GameObject> obstaclePrefabs;
	public Transform left;
	public Transform right;
	public Transform enemySpawnTransform;
	public Transform destinationTransform;
	public Texture healthTexture;
	public Node destinationNode;
	public Node enemySpawnNode;

	// Grid/Node
	public int size_x;
	public int size_z;
	public Vector2 nodeSize;
	public Node[,] nodes;
	public List<Texture> gridTextures;
	private Texture2D gridTexture;
	private ObjectManager _ObjectManager;

	// Wave control attributes
	public List<Wave> upcomingWaves = new List<Wave> ();
	public List<Wave> currentWaves = new List<Wave> ();
	public float waveSpawnDelay = 25;
	private float nextWaveSpawnEvent;
	public bool playerTriggeredWave;


	void Awake()
	{
		_ObjectManager = ObjectManager.GetInstance ();
		nodes = new Node[size_x, size_z];

		SetPositions ();
		BuildNodes ();
		ConnectNodes ();
		MakeWaves ();
		MakeObstacles ();

		destinationTransform.position = nodes[size_x-2, 0].unityPosition;
		destinationTransform.position = new Vector3(destinationTransform.position.x, -.9f, destinationTransform.position.z);
	}

	// Use this for initialization
	void Start ()
	{
		destinationNode = nodes[size_x - 1, 0];
		destinationNode.isBuildable = false;
		enemySpawnNode = nodes[0, size_z - 1];
		enemySpawnNode.isBuildable = false;

		MakeGrid ();
	}

	/// <summary>
	/// Displays the grid.
	/// </summary>
	void OnGUI ()
	{
		foreach (EnemyBase Gob in _ObjectManager.ThingsWithHealthBars()) {
			//Health Bar
			float healthRatio = (((float)Gob.Health) / ((float)Gob.maxHealth));
			if (healthRatio != 1) {
				Vector2 objSize = Gob.GetPixelSize ();
				float width = Gob.healthBarSize.x;
				float height = Gob.healthBarSize.y;
	
				width = width * healthRatio;
				Vector3 wantedPos = Camera.main.WorldToScreenPoint (Gob.transform.position);
	
				GUI.color = new Color (2 * (1 - healthRatio), 2 * healthRatio, 0);
				GUI.DrawTexture (new Rect (wantedPos.x - width / 2, Screen.height - wantedPos.y - objSize.y / 2, width, height), healthTexture);
			}
		}
	}

	/// <summary>
	/// Initializes the waves and choses random enemies for each wave.
	/// The first wave always consists of basic enemies.
	/// </summary>
	private void MakeWaves ()
	{
		int random;
		Wave wave;

        for (int x = 1; x < _ObjectManager.gameState.numberOfWaves; x++)
        {
			if (x % 10 == 0) {
				random = UnityEngine.Random.Range ((int)BossType.Start+1, (int)BossType.Max);
				wave = new Wave ((BossType)random, 1f, 1);
			} else {
				random = UnityEngine.Random.Range ((int)EnemyType.Start+1, (int)EnemyType.Max);
				wave = new Wave ((EnemyType)random, 1f, 10);
			}
			
			upcomingWaves.Add (wave);
		}
	}

	private void MakeObstacles()
	{
		System.Random random = new System.Random();  // Needs to be instantiated outside the loop. http://stackoverflow.com/questions/5398336/random-number-generator-always-picks-the-same-value-when-run-inside-a-loop
	
		if(_ObjectManager.gameState.MapType == MapType.Obstacles){
			for(int x=0; x<size_x; x++){
				for(int z=0; z<size_z; z++){
					if((x%5==3 || x%5==4) && (z%5==3 || z%5==4)){
						nodes[x,z].isBuildable = false;
						nodes[x,z].isWalkable = false;

						if(x%5==4 && 
						   z%5==4 && 
						   !(Camera.main.WorldToScreenPoint(nodes[x,z].unityPosition).y <= 10 ||
	                         Camera.main.WorldToScreenPoint(nodes[x,z].unityPosition).y >= Screen.height - Screen.height * .20)) {
							Vector3 spawnPosition = new Vector3(nodes[x,z].unityPosition.x - nodeSize.x/2, -3, nodes[x,z].unityPosition.z - nodeSize.y/2);
							Array values = Enum.GetValues(typeof(ObstacleType));
							ObstacleType randomObstacle = (ObstacleType)values.GetValue(random.Next(values.Length));
							GameObject obstaclePrefab = obstaclePrefabs[(int)randomObstacle];
							GameObject obstacle = Instantiate (obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation) as GameObject;							
						}
						else if(x%5==4 && z%5==4) {

							if(!(Camera.main.WorldToScreenPoint (nodes[x-1,z-1].unityPosition).y <= 10 ||
							      Camera.main.WorldToScreenPoint (nodes[x-1,z-1].unityPosition).y >= Screen.height - Screen.height * .21)){
								nodes[x,z].isBuildable = true;
								nodes[x,z].isWalkable = true;

								nodes[x-1,z].isBuildable = true;
								nodes[x-1,z].isWalkable = true;

								nodes[x,z-1].isBuildable = true;
								nodes[x,z-1].isWalkable = true;

								nodes[x-1,z-1].isBuildable = true;
								nodes[x-1,z-1].isWalkable = true;
							}
						}
					}
				}
			}
		}



	}

	/// <summary>
	/// Starts a wave from the upcomming Waves list.
	/// Ends the game when all the waves have been ran, all the enemies are dead, and the player didn't lose.
	/// </summary>
	public void SpawnWave ()
	{
		if (upcomingWaves.Count > 0) {
			Wave wave = upcomingWaves [0];
			upcomingWaves.RemoveAt (0);
			currentWaves.Add (wave);
			_ObjectManager.gameState.waveCount++;
		}
	}
	
	/// <summary>
	/// Creates enemies.
	/// </summary>
	public void CreateEnemies ()
	{
		if (Time.time >= nextWaveSpawnEvent || playerTriggeredWave) {
			_ObjectManager.WaveWheel.UpdateSpriteImages = true;

			playerTriggeredWave = false;
			nextWaveSpawnEvent = Time.time + waveSpawnDelay;
			SpawnWave ();
		}

		foreach (Wave wave in currentWaves) {

			if(_ObjectManager.gameState.gameSpeed == GameSpeed.Paused)
			{
				wave.nextEnemySpawnEvent += Time.deltaTime;
			}
			else 
			{
				wave.nextEnemySpawnEvent -= (Time.deltaTime * (float)_ObjectManager.gameState.gameSpeed) - Time.deltaTime;
			}

			if (Time.time >= wave.nextEnemySpawnEvent && wave.numberOfEnemies > 0) {
				GameObject enemy;
				if(wave.enemyType == EnemyType.Max){
					enemy = bossPrefabs [(int)wave.bossType];
				}else{
					enemy = enemyPrefabs [(int)wave.enemyType];
				}

				Vector3 moveSpawnOffScreen = enemySpawnTransform.position;
				moveSpawnOffScreen.x-=10;
				moveSpawnOffScreen.y-=10;

				Instantiate (enemy, moveSpawnOffScreen, Quaternion.Euler (new Vector3 (90, 45, 0)));
				wave.nextEnemySpawnEvent = Time.time + wave.spawnDelay;
				wave.numberOfEnemies--;
			}
		}

		currentWaves.RemoveAll (a => a.numberOfEnemies <= 0);
	}

	/// <summary>
	/// Gets the tile from location.
	/// </summary>
	public Node GetNodeFromLocation (Vector3 location)
	{
		
		int xIndex = (int)Mathf.Floor ((location.x - left.position.x) / nodeSize.x);
		int zIndex = size_z + ((int)Mathf.Floor ((location.z - left.position.z) / nodeSize.y));

		// out of bounds check
		if (zIndex >= size_z || zIndex < 0 || xIndex >= size_x || xIndex < 0)
			return null;

		
		return nodes [xIndex, zIndex];
	}

	/// <summary>
	/// Gets the tile from location.
	/// </summary>
	public Node GetClosestNode (Vector3 location)
	{
		
		int xIndex = (int)Mathf.Floor ((location.x - left.position.x) / nodeSize.x);
		int zIndex = size_z + ((int)Mathf.Floor ((location.z - left.position.z) / nodeSize.y));
		
		if (xIndex >= size_x)
			xIndex = size_x - 1;
		else if (xIndex < 0)
			xIndex = 0;
		
		if (zIndex >= size_z)
			zIndex = size_z - 1;
		else if (zIndex < 0)
			zIndex = 0;
		
		
		return nodes [xIndex, zIndex];
	}

	/// <summary>
	/// Blocks the node at the given position.
	/// returns true if the object can be built 
	/// else false	
	/// </summary>
	public bool BlockNode (Vector3 position)
	{
		Node node = GetNodeFromLocation (position);

		if (!node.isBuildable)
			return false;

		node.isWalkable = false;
		node.isBuildable = false;

		UpdateGridNode(node);
		return true;
	}

	public void UnBlockNode (Vector3 position)
	{
		Node node = GetNodeFromLocation (position);

		node.isWalkable = true;
		node.isBuildable = true;

		UpdateGridNode(node);
	}
	
	public void Update ()
	{
		if(_ObjectManager.gameState.gameStarted && !_ObjectManager.gameState.gameOver){

			if(_ObjectManager.gameState.gameSpeed == GameSpeed.Paused)
			{
				nextWaveSpawnEvent += Time.deltaTime;
			}
			else 
			{
				nextWaveSpawnEvent -= (Time.deltaTime * (float)_ObjectManager.gameState.gameSpeed) - Time.deltaTime;
			}


			_ObjectManager.gameState.nextWaveCountDown = (int)(nextWaveSpawnEvent - Time.time);
			CreateEnemies ();
		}
	}

	private void SetPositions ()
	{
		Vector3 center = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width/2f, Screen.height/2f, 0));
		center.y = transform.position.y;
		transform.position = center;

		Vector3 midLeft = new Vector3 (0, Screen.height / 2);
		Vector3 midRight = new Vector3 (Screen.width, Screen.height / 2);
		
		midLeft = Camera.main.ScreenToWorldPoint (midLeft);
		midRight = Camera.main.ScreenToWorldPoint (midRight);
		
		midLeft.y = 0;
		midRight.y = 0;
		
		left.transform.position = midLeft;
		right.transform.position = midRight;
		
		enemySpawnTransform.transform.position = midLeft;
		destinationTransform.transform.position = midRight;
		
	}
	
	/// <summary>
	/// Initializes nodes that make up the map.
	/// </summary>
	private void BuildNodes ()
	{
		
		float mapSizeX = (right.position.x - left.position.x);
		float mapSizwZ = (left.position.z - right.position.z);

		transform.localScale = new Vector3 (mapSizeX, mapSizwZ, 1);
		
		nodeSize = new Vector2 (mapSizeX / size_x, mapSizwZ / size_z);
		float xPos;
		float zPos;
		for (int x=0; x<size_x; x++) {
			for (int z=0; z<size_z; z++) {
				xPos = left.position.x + (x * nodeSize.x);
				zPos = right.position.z + ((z + 1) * nodeSize.y);
				Vector3 position = new Vector3 (xPos + nodeSize.x / 2, 0, zPos - nodeSize.y / 2);
				Vector3 listIndex = new Vector3 (x, 0, z);
				nodes [x, z] = new Node (true, true, position, listIndex);
				
				if (Camera.main.WorldToScreenPoint (nodes [x, z].unityPosition).y <= 10 ||
				    Camera.main.WorldToScreenPoint (nodes [x, z].unityPosition).y >= Screen.height - Screen.height * .21) {
					nodes [x, z].isWalkable = false;
					nodes [x, z].isBuildable = false;
				}
			}
		}
	}
	
	/// <summary>
	/// Connects the nodes.
	/// </summary>
	private void ConnectNodes ()
	{
		for (int z=0; z<size_z; z++) {
			for (int x=0; x<size_x; x++) {
				//Debug.Log(x+", "+ y);
				
				if (x - 1 >= 0) {
					nodes [x, z].borderTiles [(int)Border.downRight] = nodes [x - 1, z];
					if (z - 1 >= 0)
						nodes [x, z].borderTiles [(int)Border.Down] = nodes [x - 1, z - 1];
					
					if (z + 1 < nodes.GetLength (1))
						nodes [x, z].borderTiles [(int)Border.Right] = nodes [x - 1, z + 1];
				}
				
				if (x + 1 < nodes.GetLength (0)) {
					nodes [x, z].borderTiles [(int)Border.upLeft] = nodes [x + 1, z];
					if (z - 1 >= 0)
						nodes [x, z].borderTiles [(int)Border.Left] = nodes [x + 1, z - 1];
					
					if (z + 1 < nodes.GetLength (1))
						nodes [x, z].borderTiles [(int)Border.Up] = nodes [x + 1, z + 1];
				}
				
				if (z - 1 >= 0)
					nodes [x, z].borderTiles [(int)Border.downLeft] = nodes [x, z - 1];
				
				if (z + 1 < nodes.GetLength (1))
					nodes [x, z].borderTiles [(int)Border.upRight] = nodes [x, z + 1];
			}
		}
	}

	public void UpdateGridNode(Node node)
	{
		float c = node.listIndex.x + node.listIndex.z;
		if (node.isBuildable) 
			gridTexture.SetPixel((int)node.listIndex.x, (int)node.listIndex.z, new Color(c%2, c%2, c%2, .3f));
		else
			gridTexture.SetPixel((int)node.listIndex.x, (int)node.listIndex.z, Color.clear);

		gridTexture.Apply ();
	}

	private void MakeGrid()
	{
		gridTexture = new Texture2D (size_x, size_z);
		gridTexture.wrapMode = TextureWrapMode.Clamp;
		gridTexture.filterMode = FilterMode.Point;
		gridTexture.alphaIsTransparency = true;
		GetComponent<Renderer>().material.mainTexture = gridTexture;

		foreach (Node node in nodes) {
			float c = node.listIndex.x + node.listIndex.z;
			if (node.isBuildable) 
				gridTexture.SetPixel((int)node.listIndex.x, (int)node.listIndex.z, new Color(c%2, c%2, c%2, .3f));
			else
				gridTexture.SetPixel((int)node.listIndex.x, (int)node.listIndex.z, Color.clear);

		}
		gridTexture.Apply ();

	}

	public void SetGrid(bool flag)
	{
		float alpha = GetComponent<Renderer> ().material.color.a;
		GetComponent<Renderer> ().material.color = new Color (1f, 1f, 1f, 1f * (flag?1f:0f));
	}
}
