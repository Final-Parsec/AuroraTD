using System;
public class GameState
{
	public GameSpeed gameSpeed = GameSpeed.X1;
	public int waveCount = 0;
	public int nextWaveCountDown;
    public int numberOfWaves = 10;
	public float dificultyFactor = .20f;
    public string friendlyDifficulty = "DEV";
	public float enemyValueFactor = .20f;
	public bool gameStarted = false;
	public bool gameOver = false;
	public int score = 0;

	// Options
	public bool isMuted = false;
	public bool optionsOn = false;
	private int playerHealth;
    public int PlayerHealth
    {
        get
        {
            return playerHealth;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            playerHealth = value;
        }
    }
	public int playerMoney;
	public MapType MapType = MapType.Obstacles;


	public GameState (int playerHealth, int playerMoney, MapType MapType)
	{
		PlayerHealth = playerHealth;
		this.playerMoney = playerMoney;
		this.MapType = MapType;
	}

	public GameState ()
	{
	}
}

