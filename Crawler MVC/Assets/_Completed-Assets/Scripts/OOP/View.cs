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

        public PlayerSpaceship playerSpaceship;
        public List<object> hazards;

        PlayerInput playerInput = new PlayerInput();

        public ViewText gameOverText;
        public ViewText scoreText;
        public ViewText restartText;

        public View()
        {
            hazards = new List<object>();
            gameOverText = new ViewText("Game Over Text");
            scoreText = new ViewText("Score Text");
            restartText = new ViewText("Restart Text");
        }

        public void Update()
        {
            if (OnShoot != null && Input.GetButton("Fire1")) OnShoot(this, EventArgs.Empty);

            if (OnRestart != null && Input.GetKeyDown(KeyCode.R)) OnRestart(this, EventArgs.Empty);

            if (OnMove != null)
            {
				float Vert = -1.0f;
				if (Input.GetKey (KeyCode.Space))
					Vert = 3.0f;
				//print(Input.GetAxis("Vertical"));
                //Vector3 newPosition = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
				Vector3 newPosition = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Vert);
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
