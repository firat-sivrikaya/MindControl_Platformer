using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    Model.Model model;
    View.View view;

    Func<bool, bool> NOT_OPERATOR = (a) => !a;
    Func<bool, bool> NO_OPERATOR = (a) => a;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public List<int> removeHazards;

    //View.PlayerSpaceship playerSpaceship;

    void Start()
    {
        model = new Model.Model();
        view = new View.View();

        view.AddPlayerSpaceship(model.player.spaceship.gameObject.transform);
		model.AddEnemySpaceship();
		//int j = model.hazards.Count - 1;
		view.AddEnemySpaceship(model.hazards[0].GetKind(), model.hazards[0].GetGameObject().transform);
        removeHazards = new List<int>();
        //view.ShootEvent += ShootEvent;
        view.OnMove += MoveEvent;

        model.gameOver = false;
        view.restartText.gUIText.text = "";
        view.gameOverText.gUIText.text = "";
        model.score = 0;
        UpdateScore();
       // StartCoroutine(SpawnWaves());
    }

    void FixedUpdate()
    {
        view.Update();
    }

    void Update()
    {
        if (model.gameOver)
        {
            view.restartText.gUIText.text = "Press 'R' for Restart";
        }

        //Hazard control
        for (int i = 0; i < model.hazards.Count; i++)
        {
            if (model.hazards[i].GetCollider() != null)
            {
                if (model.hazards[i].GetType() == typeof(Model.EnemySpaceship))
                {
                    Model.EnemySpaceship enemySpaceship = (Model.EnemySpaceship)model.hazards[i];

                    //Player/Enemy Spaceship bolts collision
                    for (int j = 0; j < enemySpaceship.GetCannon().bolts.Count; j++)
                    {
                        if (model.player.spaceship.CollisionDetection(enemySpaceship.GetCannon().bolts[j].collider, NO_OPERATOR))
                        {
                            Transform modelTransform = model.player.spaceship.gameObject.transform;
                            view.playerSpaceship.explosion.Explode(modelTransform.position, modelTransform.rotation);
                            GameOver();
                            break;
                        }

                        //Enemy Spaceship bolts/Boundary collision
                        if (enemySpaceship.GetCannon().bolts[j].CollisionDetection(model.boundary.collider, NOT_OPERATOR))
                            enemySpaceship.GetCannon().DestroyBolt(j);
                    }

                    //Enemy spaceship control
                  /*  if (Time.time > enemySpaceship.nextMove.x)
                    {
                        enemySpaceship.nextMove.x = Time.time + enemySpaceship.moveRate.x;
                        enemySpaceship.targetManeuver = (enemySpaceship.kind == 0 ? UnityEngine.Random.Range(7, 10) : UnityEngine.Random.Range(4, 6)) * -Mathf.Sign(gameObject.transform.position.x);
                        enemySpaceship.moveRate.x = UnityEngine.Random.Range(enemySpaceship.moveRateMin, enemySpaceship.moveRateMax);
                    }
					*//*
                    if (Time.time > enemySpaceship.nextMove.y)
                    {
                        enemySpaceship.nextMove.y = Time.time + enemySpaceship.moveRate.y;
                        enemySpaceship.targetManeuver = (enemySpaceship.kind == 0 ? UnityEngine.Random.Range(5, 8) : UnityEngine.Random.Range(0, 3)) * -Mathf.Sign(gameObject.transform.position.x);
                        enemySpaceship.moveRate.y = UnityEngine.Random.Range(enemySpaceship.moveRateMin, enemySpaceship.moveRateMax);
                    }*/
					/*
                    float newManeuver = Mathf.MoveTowards(enemySpaceship.rigidbody.velocity.x, enemySpaceship.targetManeuver, 7.5f * Time.deltaTime);
                    enemySpaceship.rigidbody.velocity = new Vector3(newManeuver, 0.0f, enemySpaceship.rigidbody.velocity.z);
					*/

                    if (enemySpaceship.rigidbody != null)
                        enemySpaceship.rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, enemySpaceship.rigidbody.velocity.x * -7f);

                    if (Time.time > enemySpaceship.cannon.nextFire)
                    {
                        enemySpaceship.cannon.nextFire = Time.time + enemySpaceship.cannon.fireRate;
                        enemySpaceship.cannon.Fire();
                        View.EnemySpaceship enemySpaceshipView = (View.EnemySpaceship)view.hazards[i];
                        enemySpaceshipView.NewBolt(enemySpaceship.cannon.bolts[enemySpaceship.cannon.bolts.Count - 1].gameObject.transform);
                    }
                }
                else
                {
                    //Asteroid control
                    Model.Asteroid asteroid = (Model.Asteroid)model.hazards[i];

                    asteroid.eulerAngles.x += Time.deltaTime * asteroid.rotationSpeed.x;
                    asteroid.eulerAngles.y += Time.deltaTime * asteroid.rotationSpeed.y;
                    asteroid.eulerAngles.z += Time.deltaTime * asteroid.rotationSpeed.z;

                    if (asteroid.gameObject != null)
                        asteroid.gameObject.transform.eulerAngles = asteroid.eulerAngles;
                }

                //Flag hazard for deletion by exit of boundary
           
				if (model.hazards[i].CollisionDetection(model.boundary.collider, NOT_OPERATOR))
                {
                    removeHazards.Add(i);
                }

                //Player/Hazard collision
                if (model.player.spaceship.CollisionDetection(model.hazards[i].GetCollider(), NO_OPERATOR))
                {
                    Transform modelTransform = model.player.spaceship.gameObject.transform;
                    view.playerSpaceship.explosion.Explode(modelTransform.position, modelTransform.rotation);

                    modelTransform = model.hazards[i].GetGameObject().transform;
                    if (view.hazards[i].GetType() == typeof(View.EnemySpaceship))
                    {
                        View.EnemySpaceship enemySpaceship = (View.EnemySpaceship)view.hazards[i];
                        enemySpaceship.explosion.Explode(modelTransform.position, modelTransform.rotation);
                    }
                    else if (view.hazards[i].GetType() == typeof(View.Asteroid))
                    {
                        View.Asteroid asteroid = (View.Asteroid)view.hazards[i];
                        asteroid.explosion.Explode(modelTransform.position, modelTransform.rotation);
                    }

                    model.hazards[i].Destroy();
                    removeHazards.Add(i);

                    GameOver();
                    break;
                }

                //Hazards/Player bolts collision 
                for (int j = 0; j < model.player.spaceship.cannon.bolts.Count; j++)
                {
                    model.player.spaceship.cannon.bolts[j].CollisionDetection(model.hazards[i].GetCollider(), NO_OPERATOR);

                    if (model.hazards[i].CollisionDetection(model.player.spaceship.cannon.bolts[j].GetCollider(), NO_OPERATOR))
                    {
                        Transform modelTransform = model.hazards[i].GetGameObject().transform;
                        if (view.hazards[i].GetType() == typeof(View.EnemySpaceship))
                        {
                            View.EnemySpaceship enemySpaceship = (View.EnemySpaceship)view.hazards[i];
                            enemySpaceship.explosion.Explode(modelTransform.position, modelTransform.rotation);
                        }
                        else if (view.hazards[i].GetType() == typeof(View.Asteroid))
                        {
                            View.Asteroid asteroid = (View.Asteroid)view.hazards[i];
                            asteroid.explosion.Explode(modelTransform.position, modelTransform.rotation);
                        }

                        AddScore(model.hazards[i].GetPoints());
                        model.player.spaceship.cannon.DestroyBolt(j);
                    }
                }
            }
        }

        //Player control
        //Destroy bolts that exits the boundary
        for (int j = 0; j < model.player.spaceship.cannon.bolts.Count; j++)
        {
            if (model.player.spaceship.cannon.bolts[j].CollisionDetection(model.boundary.collider, NOT_OPERATOR))
                model.player.spaceship.cannon.DestroyBolt(j);
        }

        if (!model.gameOver)
        {
            //Clamp player within the screen
            model.player.spaceship.rigidbody.position = new Vector3(
                Mathf.Clamp(model.player.spaceship.rigidbody.position.x, model.player.spaceship.boundary.x, model.player.spaceship.boundary.y),
                0.0f,
                Mathf.Clamp(model.player.spaceship.rigidbody.position.z, model.player.spaceship.boundary.z, model.player.spaceship.boundary.w)
            );

            //Roll player spaceship by sideways velocity
          //  model.player.spaceship.rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, model.player.spaceship.rigidbody.velocity.x * -7f);
        }

        //Subscribe the ShootEvent ones when possible to fire
        if (!view.ShootEventSubscribed() && Time.time > model.player.spaceship.cannon.nextFire)
        {
            view.OnShoot += ShootEvent;
        }

        //UpdateView(model.player.spaceship.gameObject, view.playerSpaceship.gameObject);

        //Destroy hazards flagged for destroy
        foreach (int i in removeHazards)
        {
            model.hazards[i].Destroy();
        }

        //Reset destroy flag list
        removeHazards = new List<int>();
    }

    //Hazard spawn proces
	/*
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                int random = UnityEngine.Random.Range(0, 4);

                if (random == 3)
                {
                    model.AddEnemySpaceship();
                    int j = model.hazards.Count - 1;
                    view.AddEnemySpaceship(model.hazards[j].GetKind(), model.hazards[j].GetGameObject().transform);
                }
                else
                {
                    model.AddAsteroid(random);
                    int j = model.hazards.Count - 1;
                    view.AddAsteroid(random, model.hazards[j].GetGameObject().transform);
                }

                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (model.gameOver)
            {
                break;
            }
        }
    }
*/

    public void AddScore(int newScoreValue)
    {
        model.score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        view.scoreText.gUIText.text = "Score: " + model.score;
    }

    public void GameOver()
    {
        view.OnRestart += RestartEvent;
        view.OnShoot -= ShootEvent;
        view.OnMove -= MoveEvent;
        view.gameOverText.gUIText.text = "Game Over!";
        model.gameOver = true;
    }

    private void ShootEvent(object sender, EventArgs e)
    {
        model.player.spaceship.cannon.nextFire = Time.time + model.player.spaceship.cannon.fireRate;
        view.OnShoot -= ShootEvent;
        model.player.spaceship.cannon.Fire();
        view.playerSpaceship.NewBolt(model.player.spaceship.cannon.bolts[model.player.spaceship.cannon.bolts.Count - 1].gameObject.transform);
    }

    private void MoveEvent(object sender, View.PlayerInput e)
    {
        //Update player position by input
		print(e);
        model.player.spaceship.rigidbody.velocity = e.position * 10;
    }

    private void RestartEvent(object sender, EventArgs e)
    { 
        //Reload game scene
        view.OnRestart -= RestartEvent;
        view.OnShoot += ShootEvent;
        view.OnMove += MoveEvent;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*private void UpdateView(GameObject model, GameObject view)
    {
        view.transform.position = model.transform.position;
        view.transform.rotation = model.transform.rotation;
    }*/
}

