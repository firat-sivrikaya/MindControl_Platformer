using UnityEngine;
using System.Collections.Generic;
using System;

namespace View
{
    class PlayerInput : EventArgs
    {
        public Vector3 position { get; set; }
    }

    class View
    {
        public event EventHandler<EventArgs> OnShoot;
        public event EventHandler<EventArgs> OnRestart;
        public event EventHandler<PlayerInput> OnMove;
		public event EventHandler<EventArgs> OnJump;

        public PlayerSpaceship playerSpaceship;
        public List<object> hazards;
		public List<TeleportPlatform> teleportPlatforms;
		public List<MovingPlatform> movingPlatforms;
		public List<MagicPlatform> magicPlatforms;
        public List<Platform> platforms;
        PlayerInput playerInput = new PlayerInput();
        public bool jumping;
        public bool jumpTriggered;

        public ViewText gameOverText;
        public ViewText scoreText;
        public ViewText restartText;

        public View()
        {
            hazards = new List<object>();
            platforms = new List<Platform>();
			teleportPlatforms = new List<TeleportPlatform> ();
			movingPlatforms = new List<MovingPlatform> ();
			magicPlatforms = new List<MagicPlatform> ();
            gameOverText = new ViewText("Game Over Text");
            scoreText = new ViewText("Score Text");
            restartText = new ViewText("Restart Text");
        }


        public void Update()
        {
            if (OnShoot != null && Input.GetButton("Fire1")) OnShoot(this, EventArgs.Empty);

            if (OnRestart != null && Input.GetKeyDown(KeyCode.R)) OnRestart(this, EventArgs.Empty);

            if (OnJump != null && Input.GetKeyDown(KeyCode.Space))
            {
                jumping = true;
                jumpTriggered = false;
                Console.WriteLine("Jump triggered");
                OnJump(this, EventArgs.Empty);
            }
            if (OnMove != null)
            {
                float Vert = -1.0f;
                Vector3 newPosition = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

                if (playerInput.position != newPosition)
                {
                    playerInput.position = newPosition;
                    OnMove(this, playerInput);
                }
            }

        }

        public bool ShootEventSubscribed()
        {
            return (OnShoot == null) ? false : true; 
        }

        public void AddPlayerSpaceship(Transform modelTransform)
        {
            playerSpaceship = new PlayerSpaceship(modelTransform);
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

        public void AddEnemySpaceship(int kind, Transform modelTransform)
        {
            hazards.Add(new EnemySpaceship(kind));
            EnemySpaceship enemySpaceship = (EnemySpaceship)hazards[hazards.Count - 1];
            enemySpaceship.Instantiate(modelTransform);
        }

        public void AddAsteroid(int kind, Transform modelTransform)
        {
            hazards.Add(new Asteroid(kind));
            Asteroid asteroid = (Asteroid)hazards[hazards.Count - 1];
            asteroid.Instantiate(modelTransform);
        }
    }
}
