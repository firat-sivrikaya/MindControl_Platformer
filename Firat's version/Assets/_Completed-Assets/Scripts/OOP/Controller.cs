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

       // model.player.spaceship.gameObject.transform.position = new Vector3(2, 2, 2);
        //model.player.spaceship.rigidbody.transform.position = new Vector3(2, 2, 2);
        view.AddPlayerSpaceship(model.player.spaceship.gameObject.transform);
        print(model.player.spaceship.gameObject.transform);
        BuildLevel ();
        BuildLevel2();

        //view.AddPlatform(model.platform.gameObject.transform);   
        removeHazards = new List<int>();
        //view.ShootEvent += ShootEvent;
        view.OnMove += MoveEvent;
        view.OnJump += JumpEvent;
        view.OnThink += ThinkEvent;
        model.gameOver = false;
        view.restartText.gUIText.text = "";
        view.gameOverText.gUIText.text = "";
        model.score = 0;
        UpdateScore();
        //StartCoroutine(SpawnWaves());
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
        //checker les plates formes mouvantes
        for (int i = 0; i < model.movingPlatform.Count; i++)
        {
            if (model.movingPlatform[i]._direction == 0)
            {

                if (model.movingPlatform[i].right == true && model.movingPlatform[i].rigidbody.position.x <= model.movingPlatform[i].origin_x + model.movingPlatform[i]._amplitude)
                {
                   // print("moving right");
                    model.movingPlatform[i].rigidbody.transform.Translate(new Vector3(0.01f, 0.0f, 0.0f));
                    //  model.movingPlatform[i].position += new Vector3(0.1f, 0, 0);
                }
                //if platform goes to the right and is at the good place or too far, invert direction
                else if (model.movingPlatform[i].right == true && model.movingPlatform[i].rigidbody.position.x >= model.movingPlatform[i].origin_x + model.movingPlatform[i]._amplitude)
                {
                  //  print("stoping!!!");
                    model.movingPlatform[i].right = false;
                }
                else if (model.movingPlatform[i].right == false && model.movingPlatform[i].rigidbody.position.x >= model.movingPlatform[i].origin_x)
                {
                  //  print("moving left");
                    model.movingPlatform[i].rigidbody.transform.Translate(new Vector3(-0.01f, 0.0f, 0.0f));
                }
                else if (model.movingPlatform[i].rigidbody.position.x <= model.movingPlatform[i].origin_x)
                {
                    model.movingPlatform[i].right = true;
                }
            }
            else
            {
                if (model.movingPlatform[i].right == true && model.movingPlatform[i].rigidbody.position.z <= model.movingPlatform[i].origin_z + model.movingPlatform[i]._amplitude)
                {
                  //  print("moving right");
                    model.movingPlatform[i].rigidbody.transform.Translate(new Vector3(0.00f, 0.0f, 0.01f));
                }
                //if platform goes to the right and is at the good place or too far, invert direction
                else if (model.movingPlatform[i].right == true && model.movingPlatform[i].rigidbody.position.z >= model.movingPlatform[i].origin_z + model.movingPlatform[i]._amplitude)
                {
                   // print("stoping!!!");
                    model.movingPlatform[i].right = false;
                }
                else if (model.movingPlatform[i].right == false && model.movingPlatform[i].rigidbody.position.z >= model.movingPlatform[i].origin_z)
                {
                   // print("moving left");
                    model.movingPlatform[i].rigidbody.transform.Translate(new Vector3(0.00f, 0.0f, -0.01f));
                }
                else if (model.movingPlatform[i].rigidbody.position.z <= model.movingPlatform[i].origin_z)
                {
                    model.movingPlatform[i].right = true;
                }
            }
        }

        for (int i = 0; i < model.teleportPlatform.Count; i++)
        {
           // print("x axis : " + (model.teleportPlatform[i].rigidbody.position.x - model.player.spaceship.rigidbody.position.x));
           // print("z axis :  " + (model.teleportPlatform[i].rigidbody.position.z - model.player.spaceship.rigidbody.position.z));
            //check if collision or if "in contact" so If less than 1 between
            //check if difference between the two obj.position.x between 0.5 and -0.5
            float sizex = 1;
            float sizez = 1.4f;
            if (model.teleportPlatform[i].rigidbody.position.z - model.player.spaceship.rigidbody.position.z <= sizez &&
                model.teleportPlatform[i].rigidbody.position.z - model.player.spaceship.rigidbody.position.z >= -sizez && 
                model.teleportPlatform[i].rigidbody.position.x - model.player.spaceship.rigidbody.position.x <= sizex &&
                model.teleportPlatform[i].rigidbody.position.x - model.player.spaceship.rigidbody.position.x >= -sizex)
            {
                print("Beam me up Scotty");
                //how to put the cam in the center of the new level?
                model.player.spaceship.rigidbody.position = new Vector3(model.teleportPlatform[i].destination_x, 0, model.teleportPlatform[i].destination_z);
                Camera.main.transform.position = new Vector3(model.teleportPlatform[i].destination_x, 20, model.teleportPlatform[i].destination_z);
              //  GetComponent<Camera>().transform.position = new Vector3(model.teleportPlatform[i].destination_x, 0, model.teleportPlatform[i].destination_z);
            } 
        }

        //Player/platform control
        //Destroy bolts that exits the boundary
        for (int j = 0; j < model.player.spaceship.cannon.bolts.Count; j++)
        {
            if (model.player.spaceship.cannon.bolts[j].CollisionDetection(model.boundary.collider, NOT_OPERATOR))
                model.player.spaceship.cannon.DestroyBolt(j);
        }

        //Check if player is grounded on a platform
        for (int i = 0; i < model.platform.Count; i++)
        {
            if (model.player.spaceship.CollisionDetection(model.platform[i].collider, NO_OPERATOR))
            {
                view.grounded = true;
            }
        }
        //Check if player is grounded on a moving platform
        for (int i = 0; i < model.movingPlatform.Count; i++)
        {
            if (model.player.spaceship.CollisionDetection(model.movingPlatform[i].collider, NO_OPERATOR))
            {
                view.grounded = true;
                model.player.spaceship.rigidbody.transform.parent = model.movingPlatform[i].rigidbody.transform;
            }
            if (model.player.spaceship.CollisionDetection(model.movingPlatform[i].collider, NOT_OPERATOR))
            {
                model.player.spaceship.rigidbody.transform.parent = null;
            }
        }

        //Check if the player is grounded on a magic platform
        for (int i = 0 ; i < model.magicPlatform.Count; i++ )
        {
            if( model.player.spaceship.CollisionDetection(model.magicPlatform[i].collider, NO_OPERATOR))
            {
                view.grounded = true;
                model.player.spaceship.rigidbody.transform.parent = model.magicPlatform[i].rigidbody.transform;
            }
            if( model.player.spaceship.CollisionDetection(model.magicPlatform[i].collider, NOT_OPERATOR))
            {
                model.player.spaceship.rigidbody.transform.parent = null;
            }            
        }

        //Check if player is grounded on a teleport platform
        for (int i = 0; i < model.magicPlatform.Count; i++)
        {
            if (model.player.spaceship.CollisionDetection(model.magicPlatform[i].collider, NO_OPERATOR))
            {
                view.grounded = true;
            }
        }

        /*
        // Check if the player has grounded on the bottom platform
        if(model.player.spaceship.CollisionDetection(model.platform[0].collider, NO_OPERATOR))
        {
            view.grounded = true;
        }
        if(model.player.spaceship.CollisionDetection(model.platform[0].GetCollider(), NOT_OPERATOR))
        {
            view.grounded = false;
        }
        */

        //Pike control
        //End the game if the player touches a pike
        for (int i = 0; i < model.pikes.Count; i++)
        {
            if (model.player.spaceship.CollisionDetection(model.pikes[i].GetCollider(), NO_OPERATOR))
                GameOver();
        }


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
                    if (Time.time > enemySpaceship.nextMove.x)
                    {
                        enemySpaceship.nextMove.x = Time.time + enemySpaceship.moveRate.x;
                        enemySpaceship.targetManeuver = (enemySpaceship.kind == 0 ? UnityEngine.Random.Range(7, 10) : UnityEngine.Random.Range(4, 6)) * -Mathf.Sign(gameObject.transform.position.x);
                        enemySpaceship.moveRate.x = UnityEngine.Random.Range(enemySpaceship.moveRateMin, enemySpaceship.moveRateMax);
                    }

                    if (Time.time > enemySpaceship.nextMove.y)
                    {
                        enemySpaceship.nextMove.y = Time.time + enemySpaceship.moveRate.y;
                        enemySpaceship.targetManeuver = (enemySpaceship.kind == 0 ? UnityEngine.Random.Range(5, 8) : UnityEngine.Random.Range(0, 3)) * -Mathf.Sign(gameObject.transform.position.x);
                        enemySpaceship.moveRate.y = UnityEngine.Random.Range(enemySpaceship.moveRateMin, enemySpaceship.moveRateMax);
                    }

                    float newManeuver = Mathf.MoveTowards(enemySpaceship.rigidbody.velocity.x, enemySpaceship.targetManeuver, 7.5f * Time.deltaTime);
                    enemySpaceship.rigidbody.velocity = new Vector3(newManeuver, 0.0f, enemySpaceship.rigidbody.velocity.z);


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

       /* 
        if(model.player.spaceship.CollisionDetection(model.platform[0].collider, NO_OPERATOR))
            print("YEY!");*/

        /*
        if (!model.gameOver)
        {
            //Clamp player within the screen
            model.player.spaceship.rigidbody.position = new Vector3(
                Mathf.Clamp(model.player.spaceship.rigidbody.position.x, model.player.spaceship.boundary.x, model.player.spaceship.boundary.y),
                0.0f,
                Mathf.Clamp(model.player.spaceship.rigidbody.position.z, model.player.spaceship.boundary.z, model.player.spaceship.boundary.w)
            );

            //Roll player spaceship by sideways velocity
            model.player.spaceship.rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, model.player.spaceship.rigidbody.velocity.x * -7f);
        }
        */
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

    /* 
    //Hazard spawn proces
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
        view.OnJump -= JumpEvent;
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
        //float moveSpeed = 1.0f;
        //Update player position by input
        //print("Player moved");
        //model.player.spaceship.rigidbody.velocity = e.position * 10;
        //model.player.spaceship.position.y = 0;
        model.player.spaceship.rigidbody.velocity = new Vector3(e.velocity.x * 10, 0, e.velocity.z);
        //model.player.spaceship.rigidbody.transform.Translate( new Vector3(e.velocity.x / 4, 0, e.velocity.z));
        //model.player.spaceship.rigidbody.AddForce(e.force);
        //model.player.spaceship.rigidbody.velocity = e.force;
        //model.player.spaceship.rigidbody.transform.Translate(Vector3.right * e.position.x / 10 );
        //model.player.spaceship.rigidbody.useGravity = true;
    }

    private void ThinkEvent(object sender, EventArgs e)
    {
        print("JEDI POWWWAWAAAAAAAAAAA");
        //check if magic platform in the hood
        //when one is, movie it accordingly to its direction
        for (int i = 0; i < model.magicPlatform.Count; i++)
        {
            float sizex = 5;
            float sizez = 5;
            if (model.magicPlatform[i].rigidbody.position.z - model.player.spaceship.rigidbody.position.z >= -sizez &&
             model.magicPlatform[i].rigidbody.position.z - model.player.spaceship.rigidbody.position.z <= sizez &&
             model.magicPlatform[i].rigidbody.position.x - model.player.spaceship.rigidbody.position.x <= sizex &&
             model.magicPlatform[i].rigidbody.position.x - model.player.spaceship.rigidbody.position.x >= -sizex)
            {
                //0 == Horizontal from left to right
                //1 == Horizontal from right to left.
                //2 == vertical UP so platform will move from up to bottom
                //3 == vertical DOWN
                if (model.magicPlatform[i]._direction == 0)
                { 
                    if (model.magicPlatform[i].right == true && model.magicPlatform[i].rigidbody.position.x <= model.magicPlatform[i].origin_x + model.magicPlatform[i]._amplitude)
                    {
                        model.magicPlatform[i].rigidbody.transform.Translate(new Vector3(0.1f, 0.0f, 0.0f));
                    }
                    //if platform goes to the right and is at the good place or too far, invert direction
                    else if (model.magicPlatform[i].right == true && model.magicPlatform[i].rigidbody.position.x >= model.magicPlatform[i].origin_x + model.magicPlatform[i]._amplitude)
                    {
                        model.magicPlatform[i].right = false;
                    }
                    else if (model.magicPlatform[i].right == false && model.magicPlatform[i].rigidbody.position.x >= model.magicPlatform[i].origin_x)
                    {
                        model.magicPlatform[i].rigidbody.transform.Translate(new Vector3(-0.1f, 0.0f, 0.0f));
                    }
                    else if (model.magicPlatform[i].rigidbody.position.x <= model.magicPlatform[i].origin_x)
                    {
                        model.magicPlatform[i].right = true;
                    }
                 }
                else if (model.magicPlatform[i]._direction == 1)//start right and go left
                {
                    if (model.magicPlatform[i].right == true && model.magicPlatform[i].rigidbody.position.x >= model.magicPlatform[i].origin_x - model.magicPlatform[i]._amplitude)
                    {
                        model.magicPlatform[i].rigidbody.transform.Translate(new Vector3(-0.1f, 0.0f, 0.0f));
                    }
                    //if platform goes to the left and is at the good place or too far, invert direction
                    else if (model.magicPlatform[i].right == true && model.magicPlatform[i].rigidbody.position.x <= model.magicPlatform[i].origin_x - model.magicPlatform[i]._amplitude)
                    {
                        //  print("stoping!!!");
                        model.magicPlatform[i].right = false;
                    }
                    else if (model.magicPlatform[i].right == false && model.magicPlatform[i].rigidbody.position.x <= model.magicPlatform[i].origin_x)
                    {
                        //  print("moving left");
                        // model.magicPlatform[i].rigidbody.MovePosition(new Vector3(-0.1f, 0.0f, 0.0f));
                        model.magicPlatform[i].rigidbody.transform.Translate(new Vector3(0.1f, 0.0f, 0.0f));
                    }
                    else if (model.magicPlatform[i].rigidbody.position.x >= model.magicPlatform[i].origin_x)
                    {
                        model.magicPlatform[i].right = true;
                    }
                }
                else if (model.magicPlatform[i]._direction == 2)//start up and go down
                {
                    if (model.magicPlatform[i].right == true && model.magicPlatform[i].rigidbody.position.z >= model.magicPlatform[i].origin_z - model.magicPlatform[i]._amplitude)
                    {
                        model.magicPlatform[i].rigidbody.transform.Translate(new Vector3(0.0f, 0.0f, -0.1f));
                    }
                    //if platform goes to the left and is at the good place or too far, invert direction
                    else if (model.magicPlatform[i].right == true && model.magicPlatform[i].rigidbody.position.z <= model.magicPlatform[i].origin_z - model.magicPlatform[i]._amplitude)
                    {
                        //  print("stoping!!!");
                        model.magicPlatform[i].right = false;
                    }
                    else if (model.magicPlatform[i].right == false && model.magicPlatform[i].rigidbody.position.z <= model.magicPlatform[i].origin_z)
                    {
                        //  print("moving left");
                        // model.magicPlatform[i].rigidbody.MovePosition(new Vector3(-0.1f, 0.0f, 0.0f));
                        model.magicPlatform[i].rigidbody.transform.Translate(new Vector3(0.0f, 0.0f, 0.1f));
                    }
                    else if (model.magicPlatform[i].rigidbody.position.z >= model.magicPlatform[i].origin_z)
                    {
                        model.magicPlatform[i].right = true;
                    }
                }
                else if (model.magicPlatform[i]._direction == 3)//start down and goes up
                {
                    if (model.magicPlatform[i].right == true && model.magicPlatform[i].rigidbody.position.z <= model.magicPlatform[i].origin_z + model.magicPlatform[i]._amplitude)
                    {
                          print("moving up");
                        model.magicPlatform[i].rigidbody.transform.Translate(new Vector3(0.00f, 0.0f, 0.1f));
                    }
                    //if platform goes to the right and is at the good place or too far, invert direction
                    else if (model.magicPlatform[i].right == true && model.magicPlatform[i].rigidbody.position.z >= model.magicPlatform[i].origin_z + model.magicPlatform[i]._amplitude)
                    {
                         print("stoping!!!");
                        model.magicPlatform[i].right = false;
                    }
                    else if (model.magicPlatform[i].right == false && model.magicPlatform[i].rigidbody.position.z >= model.magicPlatform[i].origin_z)
                    {
                         print("moving down");
                        model.magicPlatform[i].rigidbody.transform.Translate(new Vector3(0.00f, 0.0f, -0.1f));
                    }
                    else if (model.magicPlatform[i].rigidbody.position.z <= model.magicPlatform[i].origin_z)
                    {
                        model.magicPlatform[i].right = true;
                    }
                }
                    //model.player.spaceship.rigidbody.position = new Vector3(model.teleportPlatform[i].destination_x, 0, model.teleportPlatform[i].destination_z);
            }
        }
    }
    private void RestartEvent(object sender, EventArgs e)
    { 
        //Reload game scene
        view.OnRestart -= RestartEvent;
        view.OnShoot += ShootEvent;
        view.OnMove += MoveEvent;
        view.OnJump += JumpEvent;
        view.OnThink += ThinkEvent;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	private void BuildLevel ()
	{
		BuildLevel1 ();
	}

	private void BuildLevel1()
	{
        //level frame
        Camera.main.transform.position = new Vector3(10, 15, 10);
        Camera.main.orthographicSize = 20;

        //min x= -2 max x =20
        //min z = -2 max z = 16
        //framework for level
        model.AddPlatform (21, 1, 9, -2);//floor
        model.AddPlatform(1, 19, -2, 7);//left wall
        model.AddPlatform(1, 19, 20, 7);//right wall
        model.AddPlatform(21, 1, 9, 16);//ceilling

        //other platform
        model.AddPlatform(3, 1, 0, 6);//start platform
        model.AddPlatform(10, 1, 12, 7);//middle one
        //model.AddPlatform(5, 1,18, 7);
       // model.AddPlatform(10, 1, 0, 0);
        /*model.AddPlatform(10, 1, 0, 0);
        model.AddPlatform(10, 1, 0, 0);
        model.AddPlatform(10, 1, 0, 0);*/

        for ( int i = 0 ; i < model.platform.Count ; i++ )
		{
			view.AddPlatform(model.platform[i].gameObject.transform);
		}

		model.AddTeleportPlatform (1, 1, 5, 2, 50, 60);

		for ( int i = 0 ; i < model.teleportPlatform.Count ; i++ )
		{
			view.AddTeleportPlatform(model.teleportPlatform[i].gameObject.transform);
		}

	//	model.AddMovingPlatform (1, 1, 0, 7, 1, 2);
    /*
		for ( int i = 0 ; i < model.movingPlatform.Count ; i++ )
		{
			view.AddMovingPlatform(model.movingPlatform[i].gameObject.transform);
		}

		model.AddMagicPlatform (3, 1, 14, 3, 3, 2);

		for ( int i = 0 ; i < model.magicPlatform.Count ; i++ )
		{
			view.AddMagicPlatform(model.magicPlatform[i].gameObject.transform);
		}

        model.AddPike(1, 1, 6, 3);
        for (int i = 0; i < model.magicPlatform.Count; i++)
        {
            view.AddMagicPlatform(model.magicPlatform[i].gameObject.transform);
        }*/

    }

    private void BuildLevel2()
    {
        //Camera.main.transform.position = new Vector3(10, 15, 10);
        //Camera.main.orthographicSize = 13;

        //min x = 48 max x = 70
        //min z = 48 max z = 66

        //framework for level
        model.AddPlatform(21, 1, 59, 48);//ground
        model.AddPlatform(1, 19, 48, 57);//left wall
        model.AddPlatform(1, 19, 70, 57);//right wall
        model.AddPlatform(21, 1, 59, 66);//ceiling

        //Other platforms
        model.AddPlatform(3, 1, 51, 50);// start platform
        model.AddPlatform(3, 1, 66, 51);//down right platform
        model.AddPlatform(3, 1, 66, 54);//just above previous
        model.AddPlatform(3, 1, 60, 62);//to jump on the last one
        model.AddPlatform(2, 1, 55, 60);//below the teleporter

        //Moving platform
        model.AddMovingPlatform(3, 1, 58, 50, 0, 3);//post pike platform
        model.AddMovingPlatform(3, 1, 64, 56, 1, 3);//elevator platform

        //Magic Platform
        model.AddMagicPlatform(1, 1, 55, 62, 0, 2);//blocking teleporter

        //Teleporter
        model.AddTeleportPlatform(1, 1, 55, 63, 50, 60);

        for (int i = 0; i < model.platform.Count; i++)
        {
            view.AddPlatform(model.platform[i].gameObject.transform);
        }

        for (int i = 0; i < model.movingPlatform.Count; i++)
        {
            view.AddMovingPlatform(model.movingPlatform[i].gameObject.transform);
        }
        // model.AddPike(1, 1, 62, 62);

        for (int i = 0; i < model.pikes.Count; i++)
        {
            view.AddPike(model.pikes[i].gameObject.transform);
        }

        for (int i = 0; i < model.magicPlatform.Count; i++)
        {
            view.AddMagicPlatform(model.magicPlatform[i].gameObject.transform);
        }

        for (int i = 0; i < model.teleportPlatform.Count; i++)
        {
            view.AddTeleportPlatform(model.teleportPlatform[i].gameObject.transform);
        }
    }

    private void JumpEvent(object sender, View.PlayerInput e)
    {
        float JumpSpeed = 2.0f;
        /*
        if(model.player.spaceship.grounded )
        {
            model.player.spaceship.rigidbody.AddForce(Vector3.forward * JumpSpeed);
            model.player.spaceship.grounded = false;
        }*/


        if (!view.jumpTriggered)
        {
            //model.player.spaceship.rigidbody.AddRelativeForce(0, 0, -10, ForceMode.Acceleration);
            view.jumpTriggered = true;
        }

        print("Player jumped");
        if (view.grounded)
        {
            view.grounded = false;
            //view.jumping = false;
            //model.player.spaceship.rigidbody.AddForce(0, 0, 5, ForceMode.Impulse);
            //model.player.spaceship.rigidbody.transform.Translate(Vector3.forward * JumpSpeed);
            model.player.spaceship.rigidbody.AddForce(e.velocity);
            model.player.spaceship.rigidbody.transform.parent = null;
        }

        //playerSpaceship.gameObject.GetComponent<Rigidbody>().AddForce(0, 50, 0, ForceMode.Impulse);
    }
    /*private void UpdateView(GameObject model, GameObject view)
    {
        view.transform.position = model.transform.position;
        view.transform.rotation = model.transform.rotation;
    }*/
}

