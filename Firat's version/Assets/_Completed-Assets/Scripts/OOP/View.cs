﻿using UnityEngine;
using System.Collections.Generic;
using System;

namespace View
{
    class PlayerInput : EventArgs
    {
        public Vector3 position { get; set; }
        public Vector3 velocity { get; set; }

        public Vector3 force { get; set; }
    }

    class View
    {
       // public event EventHandler<EventArgs> OnShoot;
        public event EventHandler<EventArgs> OnRestart;
        public event EventHandler<PlayerInput> OnMove;
		public event EventHandler<PlayerInput> OnJump;
        public event EventHandler<EventArgs> OnThink;

        public PlayerCharacter playerCharacter;
        public BlinkMonster blinkMonster;
      //  public List<object> hazards;
		public List<TeleportPlatform> teleportPlatforms;
		public List<MovingPlatform> movingPlatforms;
		public List<MagicPlatform> magicPlatforms;
        public List<Platform> platforms;
        public List<Pike> pikes;
        PlayerInput playerInput = new PlayerInput();
        public bool jumping;
        public bool jumpTriggered;
        public bool grounded;
        public bool groundedOnMagic;

        public View()
        {
            grounded = false;
            groundedOnMagic = false;
            platforms = new List<Platform>();
            pikes = new List<Pike>();
			teleportPlatforms = new List<TeleportPlatform> ();
			movingPlatforms = new List<MovingPlatform> ();
			magicPlatforms = new List<MagicPlatform> ();
            jumping = false;
        }


        public void Update()
        {
            float SpeedHorizontal = 0.70f;

            if (OnRestart != null && Input.GetKeyDown(KeyCode.R)) OnRestart(this, EventArgs.Empty);

            if (OnThink != null && Input.GetKeyDown(KeyCode.T)) OnThink(this, EventArgs.Empty);

            if (OnJump != null && Input.GetKeyDown(KeyCode.Space) )
            {
                jumping = true;
                //jumpTriggered = false;
                Vector3 newVelocity = new Vector3(Input.GetAxis("Horizontal") * SpeedHorizontal, 0.0f, 500.0f);
                if (playerInput.velocity != newVelocity)
                {
                    playerInput.velocity = newVelocity;
                    OnJump(this, playerInput);
                }
            }
            if (OnMove != null)
            {
                Vector3 newVelocity;

                newVelocity = new Vector3(Input.GetAxis("Horizontal") * SpeedHorizontal, 0.0f);
                if (playerInput.velocity != newVelocity)
                {
                    playerInput.velocity = newVelocity;
                    OnMove(this, playerInput);
                }

                /*
                float Vert = -1.0f;
                Vector3 newPosition = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
                Vector3 newVelocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
               
                if (playerInput.velocity != newVelocity)
                {
                    playerInput.position = newPosition;
                    playerInput.velocity = newVelocity;
                    OnMove(this, playerInput);
                }*/
            }


        }

        public void AddPlayerCharacter(Transform modelTransform)
        {
            playerCharacter = new PlayerCharacter(modelTransform);
        }

        public void AddBlinkMonster(Transform modelTransform)
        {
            blinkMonster = new BlinkMonster(modelTransform);
        }

		public void AddTeleportPlatform(Transform modelTransform)
		{
			teleportPlatforms.Add (new TeleportPlatform (modelTransform));
		}

		public void AddMovingPlatform(Transform modelTransform)
		{
			movingPlatforms.Add (new MovingPlatform (modelTransform));
		}

		public void AddMagicPlatform(Transform modelTransform)
		{
			magicPlatforms.Add (new MagicPlatform (modelTransform));
		}
			
        public void AddPlatform(Transform modelTransform)
        {
            platforms.Add( new Platform(modelTransform));
        }

        public void AddPike(Transform modelTransform)
        {
            pikes.Add(new Pike(modelTransform));
        }
    }
}
