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

        public Model()
        {
            player = new Player();
            boundary = new Boundary();

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

        private void Set(int i)
        {
            hazards[i].SetPosition();
            hazards[i].Instantiate();
            hazards[i].Init();
        }

        public void Update()
        {

        }
    }
}
