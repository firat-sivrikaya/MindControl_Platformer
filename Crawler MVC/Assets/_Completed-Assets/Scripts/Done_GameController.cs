using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*public class Done_GameController : MonoBehaviour
{
    Controller controller;

    System.Func<bool, bool> NOT_OPERATOR = (a) => !a;
    System.Func<bool, bool> NO_OPERATOR = (a) => a;

	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

    //private IHazard[] hazards;

	private bool gameOver;
	private int score;

    Player player;
    Boundary boundary;

    void Start ()
	{
        controller = new Controller(new Model.Model(), new View.view());

        player = new Player();
        boundary = new Boundary();

        hazards = new IHazard[hazardCount];

        gameOver = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore();
		StartCoroutine(SpawnWaves());
    }
	
	void Update ()
	{
        controller.Update();

        if (gameOver)
        {
            restartText.text = "Press 'R' for Restart";

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
        }
    }

    private void FixedUpdate()
    {
        controller.FixedUpdate();

        for (int i = 0; i < hazardCount; i++)
        {
            try
            {
                if (hazards[i].GetCollider() != null)
                {
                    hazards[i].Updates();

                    hazards[i].CollisionDetection(boundary.collider, NOT_OPERATOR);

                    if (player.spaceship.CollisionDetection(hazards[i].GetCollider(), NO_OPERATOR))
                        GameOver();

                    for (int j = 0; j < player.spaceship.cannon.bolts.Count; j++)
                    {
                        player.spaceship.cannon.bolts[j].CollisionDetection(hazards[i].GetCollider(), NO_OPERATOR);

                        if (hazards[i].CollisionDetection(player.spaceship.cannon.bolts[j].GetCollider(), NO_OPERATOR))
                        { 
                            AddScore(hazards[i].GetPoints());
                            player.spaceship.cannon.DestroyBolt(j);
                        }
                    }

                    if (hazards[i].GetType() == typeof(EnemySpaceship))
                        for (int j = 0; j < hazards[i].GetCannon().bolts.Count; j++)
                        {
                            if (player.spaceship.CollisionDetection(hazards[i].GetCannon().bolts[j].collider, NO_OPERATOR))
                                GameOver();
                        }
                }
            }
            catch (System.NullReferenceException) { }
        }

        try
        {
            for (int j = 0; j < player.spaceship.cannon.bolts.Count; j++)
            {
                if (player.spaceship.cannon.bolts[j].CollisionDetection(boundary.collider, NOT_OPERATOR))
                    player.spaceship.cannon.DestroyBolt(j);
            }

            if (!gameOver)
            {
                player.spaceship.Updates();
            }
        }
        catch (System.NullReferenceException) { }
    }

    IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);

		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
                int random = Random.Range(0, 4);

                if (random == 3)
                    hazards[i] = new EnemySpaceship();
                else
                    hazards[i] = new Asteroid(random);

                hazards[i].SetPosition();
                hazards[i].Instantiate();
                hazards[i].Init();
                
                yield return new WaitForSeconds (spawnWait);
			}

			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				break;
			}
		}
	}

    public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}
	
	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver()
	{
        gameOverText.text = "Game Over!";
		gameOver = true;
	}
}*/