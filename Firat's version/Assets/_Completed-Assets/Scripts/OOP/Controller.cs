using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Controller : MonoBehaviour
{
    Model.Model model;
    View.View view;

    Func<bool, bool> NOT_OPERATOR = (a) => !a;
    Func<bool, bool> NO_OPERATOR = (a) => a;

    void Start()
    {
        model = new Model.Model();
        view = new View.View();
        view.AddPlayerCharacter(model.player.character.gameObject.transform);
        print(model.player.character.gameObject.transform);
        BuildLevel();

        view.OnMove += MoveEvent;
        view.OnJump += JumpEvent;
        view.OnThink += ThinkEvent;
        model.gameOver = false;
    }

    void FixedUpdate()
    {
        view.Update();
    }

    void Update()
    {

        //Hazard control
        //Make platform move
        for (int i = 0; i < model.movingPlatform.Count; i++)
        {
            if (model.movingPlatform[i]._direction == 0)
            {

                if (model.movingPlatform[i].right == true && model.movingPlatform[i].rigidbody.position.x <= model.movingPlatform[i].origin_x + model.movingPlatform[i]._amplitude)
                    model.movingPlatform[i].rigidbody.transform.Translate(new Vector3(0.01f, 0.0f, 0.0f));
                else if (model.movingPlatform[i].right == true && model.movingPlatform[i].rigidbody.position.x >= model.movingPlatform[i].origin_x + model.movingPlatform[i]._amplitude)
                    model.movingPlatform[i].right = false;
                else if (model.movingPlatform[i].right == false && model.movingPlatform[i].rigidbody.position.x >= model.movingPlatform[i].origin_x)
                    model.movingPlatform[i].rigidbody.transform.Translate(new Vector3(-0.01f, 0.0f, 0.0f));
                else if (model.movingPlatform[i].rigidbody.position.x <= model.movingPlatform[i].origin_x)
                    model.movingPlatform[i].right = true;
            }
            else
            {
                if (model.movingPlatform[i].right == true && model.movingPlatform[i].rigidbody.position.z <= model.movingPlatform[i].origin_z + model.movingPlatform[i]._amplitude)
                    model.movingPlatform[i].rigidbody.transform.Translate(new Vector3(0.00f, 0.0f, 0.01f));
                else if (model.movingPlatform[i].right == true && model.movingPlatform[i].rigidbody.position.z >= model.movingPlatform[i].origin_z + model.movingPlatform[i]._amplitude)
                    model.movingPlatform[i].right = false;
                else if (model.movingPlatform[i].right == false && model.movingPlatform[i].rigidbody.position.z >= model.movingPlatform[i].origin_z)
                    model.movingPlatform[i].rigidbody.transform.Translate(new Vector3(0.00f, 0.0f, -0.01f));
                else if (model.movingPlatform[i].rigidbody.position.z <= model.movingPlatform[i].origin_z)
                {
                    model.movingPlatform[i].right = true;
                }
            }
        }
        //make the ghost move
        //launch rand. one on 120 chances (+- one move every two seconds)
        if (model.blinkMonster != null && UnityEngine.Random.Range(1, 220) == 42)
        {
            int transx = 0;
            int transz = 0;
            if (model.player.character.rigidbody.position.x > model.blinkMonster.rigidbody.position.x)
                transx = 1;
            else if (model.player.character.rigidbody.position.x < model.blinkMonster.rigidbody.position.x)
                transx = -1;
            if (model.player.character.rigidbody.position.z > model.blinkMonster.rigidbody.position.z)
                transz = 1;
            else if (model.player.character.rigidbody.position.z < model.blinkMonster.rigidbody.position.z)
                transz = -1;
            model.blinkMonster.rigidbody.transform.Translate(transx, 0, transz);
        }

        if (model.blinkMonster != null && model.blinkMonster.rigidbody.position.z - model.player.character.rigidbody.position.z <= 1.4 &&
                model.blinkMonster.rigidbody.position.z - model.player.character.rigidbody.position.z >= -1.4 &&
                model.blinkMonster.rigidbody.position.x - model.player.character.rigidbody.position.x <= 1 &&
                model.blinkMonster.rigidbody.position.x - model.player.character.rigidbody.position.x >= -1)
            GameOver();

            for (int i = 0; i < model.teleportPlatform.Count; i++)
        {
            //check if collision or if "in contact" so If less than 1 between
            //check if difference between the two obj.position.x between 0.5 and -0.5
            float sizex = 1;
            float sizez = 1.4f;
            if (model.teleportPlatform[i].rigidbody.position.z - model.player.character.rigidbody.position.z <= sizez &&
                model.teleportPlatform[i].rigidbody.position.z - model.player.character.rigidbody.position.z >= -sizez && 
                model.teleportPlatform[i].rigidbody.position.x - model.player.character.rigidbody.position.x <= sizex &&
                model.teleportPlatform[i].rigidbody.position.x - model.player.character.rigidbody.position.x >= -sizex)
            {
                // Check if the player has reached the final teleporter
                if (model.teleportPlatform[model.teleportPlatform.Count - 1].rigidbody.position.z - model.player.character.rigidbody.position.z <= sizez &&
                    model.teleportPlatform[model.teleportPlatform.Count - 1].rigidbody.position.z - model.player.character.rigidbody.position.z >= -sizez && 
                    model.teleportPlatform[model.teleportPlatform.Count - 1].rigidbody.position.x - model.player.character.rigidbody.position.x <= sizex &&
                    model.teleportPlatform[model.teleportPlatform.Count - 1].rigidbody.position.x - model.player.character.rigidbody.position.x >= -sizex)
                    {
                        GameOver();
                        Camera.main.transform.position = new Vector3(-40, 20, -200);
                        return;
                    }
                model.player.character.rigidbody.position = new Vector3(model.teleportPlatform[i].destination_x, 0, model.teleportPlatform[i].destination_z);
                Camera.main.transform.position = new Vector3(model.teleportPlatform[i].camera_x, 20, model.teleportPlatform[i].camera_z);

            } 
        }

        //Player/platform control
        //Check if player is grounded on a platform
        for (int i = 0; i < model.platform.Count; i++)
        {
            if (model.player.character.CollisionDetection(model.platform[i].collider, NO_OPERATOR))
            {
                view.grounded = true;
            }
        }
        //Check if player is grounded on a moving platform
        for (int i = 0; i < model.movingPlatform.Count; i++)
        {
            if (model.player.character.CollisionDetection(model.movingPlatform[i].collider, NO_OPERATOR))
            {
                view.grounded = true;
                print("Grounded on moving");
                model.player.character.rigidbody.transform.parent = model.movingPlatform[i].rigidbody.transform;
            }
            else if (model.player.character.CollisionDetection(model.movingPlatform[i].collider, NOT_OPERATOR))
            {
                if (!view.grounded)
                    model.player.character.rigidbody.transform.parent = null;
            }
        }

        //Check if the player is grounded on a magic platform
        for (int i = 0 ; i < model.magicPlatform.Count; i++ )
        {
            if( model.player.character.CollisionDetection(model.magicPlatform[i].collider, NO_OPERATOR))
            {
                view.grounded = true;
                view.groundedOnMagic = true;
                model.player.character.rigidbody.transform.parent = model.magicPlatform[i].rigidbody.transform;
            }
             
            if( model.player.character.CollisionDetection(model.magicPlatform[i].collider, NOT_OPERATOR))
            {
                if (!view.grounded)
                    model.player.character.rigidbody.transform.parent = null;
            }            
        }
        /* 
        // Move the camera to congratz screen if the player achieves to get to last teleport
        if(model.player.character.CollisionDetection(model.teleportPlatform[model.teleportPlatform.Count - 1].collider, NO_OPERATOR))
        {
            GameOver();
            Camera.main.transform.position = new Vector3(-40, 20, -200);
        }*/
        
        // Check if the player has grounded on the bottom platform
        /*if(model.player.character.CollisionDetection(model.platform[0].collider, NO_OPERATOR))
        {
            view.grounded = true;
        }
        if(model.player.character.CollisionDetection(model.platform[0].GetCollider(), NOT_OPERATOR))
        {
            view.grounded = false;
        }*/
        

        //Pike control
        //End the game if the player touches a pike
        for (int i = 0; i < model.pikes.Count; i++)
        {
            if (model.player.character.CollisionDetection(model.pikes[i].GetCollider(), NO_OPERATOR))
                GameOver();
        }


   
        //UpdateView(model.player.character.gameObject, view.playercharacter.gameObject);

   }
    

    public void GameOver()
    {
        view.OnRestart += RestartEvent;
        view.OnMove -= MoveEvent;
        view.OnJump -= JumpEvent;
        Camera.main.transform.position = new Vector3(0, 20, -80);
        model.gameOver = true;
    }


    private void MoveEvent(object sender, View.PlayerInput e)
    {
        //float moveSpeed = 1.0f;
        //Update player position by input
        //print("Player moved");
        //model.player.character.rigidbody.velocity = e.position * 10;
        //model.player.character.position.y = 0;
        model.player.character.rigidbody.velocity = new Vector3(e.velocity.x * 10, 0, e.velocity.z);
        //model.player.character.rigidbody.transform.Translate( new Vector3(e.velocity.x / 4, 0, e.velocity.z));
        //model.player.character.rigidbody.AddForce(e.force);
        //model.player.character.rigidbody.velocity = e.force;
        //model.player.character.rigidbody.transform.Translate(Vector3.right * e.position.x / 10 );
        //model.player.character.rigidbody.useGravity = true;
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
            if (model.magicPlatform[i].rigidbody.position.z - model.player.character.rigidbody.position.z >= -sizez &&
             model.magicPlatform[i].rigidbody.position.z - model.player.character.rigidbody.position.z <= sizez &&
             model.magicPlatform[i].rigidbody.position.x - model.player.character.rigidbody.position.x <= sizex &&
             model.magicPlatform[i].rigidbody.position.x - model.player.character.rigidbody.position.x >= -sizex)
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
                    //model.player.character.rigidbody.position = new Vector3(model.teleportPlatform[i].destination_x, 0, model.teleportPlatform[i].destination_z);
            }
        }
    }
    private void RestartEvent(object sender, EventArgs e)
    { 
        //Reload game scene
        view.OnRestart -= RestartEvent;
        view.OnMove += MoveEvent;
        view.OnJump += JumpEvent;
        view.OnThink += ThinkEvent;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

	private void BuildLevel ()
	{
		BuildLevel1();
        BuildLevel2();
	}

	private void BuildLevel1()
	{
        //level frame
        Camera.main.transform.position = new Vector3(10, 15, 10);
        Camera.main.orthographicSize = 20;
        //Change player's starting position
        model.player.character.rigidbody.position = new Vector3(0, 0, 8);
        //min x= -2 max x =20
        //min z = -2 max z = 16
        //framework for level
        model.AddPike(21, 1, 9, -2);//floor
        model.AddPlatform(1, 19, -2, 7);//left wall
        model.AddPlatform(1, 19, 20, 7);//right wall
        model.AddPlatform(21, 1, 9, 16);//ceilling
        //other platform
        model.AddPlatform(3, 1, 0, 6);//start platform
        //model.AddPlatform(10, 1, 12, 7);//middle one
        model.AddPlatform(3, 1, 5, 10); //second platform next to start
        model.AddPlatform(1, 1, 9, 3); //Small platform on the bottom

        model.AddPlatform(1, 4, 15, 3); //left part of teleport container
        model.AddPlatform(3, 1, 16, 2); //bottom part of teleport container
        model.AddPlatform(1, 4, 18, 3); //right part of teleport container

        //Moving platforms
        model.AddMovingPlatform(3, 1, 12, 5, 1, 4); // Elevator platform near small platform

        // Magic platforms
        model.AddMagicPlatform(4, 4, 16, 7, 3, 3);
        // Teleport platform
        model.AddTeleportPlatform(1, 1, 17, 3, 50, 60, 59, 57);

        // Pikes
        model.AddPike(1, 5, 9, 10); //pike near second platform
    }

    private void BuildLevel2()
    {
        //Camera.main.transform.position = new Vector3(10, 15, 10);
        //Camera.main.orthographicSize = 13;

        //min x = 48 max x = 70
        //min z = 48 max z = 66

        //framework for level
        model.AddPike(21, 1, 59, 48);//ground
        model.AddPlatform(1, 19, 48, 57);//left wall
        model.AddPlatform(1, 19, 70, 57);//right wall
        model.AddPlatform(21, 1, 59, 66);//ceiling

        //Other platforms
        model.AddPlatform(3, 1, 51, 50);// start platform
        model.AddPlatform(3, 1, 66, 51);//down right platform
        model.AddPlatform(3, 1, 66, 54);//just above previous
        model.AddPlatform(3, 1, 60, 62);//to jump on the last one
        model.AddPlatform(2, 1, 55, 60);//below the teleporter


        //Pike platforms
        model.AddPike(1, 4, 54, 50); //down left corner pike
        model.AddPike(1, 5, 69, 51); //down right corner pike
        model.AddPike(1, 2, 56, 65); //ceiling pike
        //Moving platform
        model.AddMovingPlatform(3, 1, 58, 50, 0, 3);//post pike platform
        model.AddMovingPlatform(3, 1, 64, 56, 1, 3);//elevator platform

        //Magic Platform
        model.AddMagicPlatform(2, 1, 55, 64, 0, 3);//blocking teleporter

        //Teleporter
        model.AddTeleportPlatform(1, 1, 55, 65, 50, 60, 50, 60); //fake coordinate for now

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
        // float JumpSpeed = 2.0f;
        /*
        if(model.player.character.grounded )
        {
            model.player.character.rigidbody.AddForce(Vector3.forward * JumpSpeed);
            model.player.character.grounded = false;
        }*/


        if (!view.jumpTriggered)
        {
            //model.player.character.rigidbody.AddRelativeForce(0, 0, -10, ForceMode.Acceleration);
            view.jumpTriggered = true;
        }

        
        if (view.grounded)
        {
            
            view.grounded = false;
            //view.jumping = false;
            //model.player.character.rigidbody.AddForce(0, 0, 5, ForceMode.Impulse);
            //model.player.character.rigidbody.transform.Translate(Vector3.forward * JumpSpeed);
            model.player.character.rigidbody.AddForce(e.velocity);
            print("Player jumped");
            model.player.character.rigidbody.transform.parent = null;
            view.jumping = false;
        }

        //playerCharacter.gameObject.GetComponent<Rigidbody>().AddForce(0, 50, 0, ForceMode.Impulse);
    }
    /*private void UpdateView(GameObject model, GameObject view)
    {
        view.transform.position = model.transform.position;
        view.transform.rotation = model.transform.rotation;
    }*/
}

