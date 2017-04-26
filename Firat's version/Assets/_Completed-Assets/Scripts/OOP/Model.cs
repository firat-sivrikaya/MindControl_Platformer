using System.Collections.Generic;


namespace Model
{
    class Model
    {
        public List<IHazard> hazards;

        public bool gameOver;
        public int score;

        public Player player;
        public Boundary boundary;

        public List<Platform> platform;

        public Model()
        {
            player = new Player();
            boundary = new Boundary();
            platform = new List<Platform>();
            ResetHazards();
        }

        public void ResetHazards()
        {
            hazards = new List<IHazard>();
        }

        public void RemoveEnemy(int i)
        {
            hazards.RemoveAt(i);
        }
            
        public void AddEnemySpaceship()
        {
            hazards.Add(new EnemySpaceship());
            Set(hazards.Count - 1);  
        }

        public void AddAsteroid(int kind)
        {
            hazards.Add(new Asteroid(kind));
            Set(hazards.Count - 1);
        }

        public void AddPlatform(int x, int z)
        {
            platform.Add( new Platform(4, 4, x, z) );
            SetPlatform(platform.Count -1);
        }

        private void Set(int i)
        {
            hazards[i].SetPosition();
            hazards[i].Instantiate();
            hazards[i].Init();
        }

        private void SetPlatform(int i)
        {
            platform[i].Instantiate();
        }

        public void Update()
        {

        }
    }
}
