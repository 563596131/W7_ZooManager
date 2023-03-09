using System;
namespace ZooManager
{
    public class Alien:Entity,Predator
    {
        public int huntReactionDistance = 1;

        public Alien(string name)
        {
            emoji = "👽";
            species = "E.T.";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = 0; // most fast reaction Time
            
        }
        static private int Seek(int x, int y, Direction d, string target, int distance)
        {
            for (int i = 0; i < distance; i++)
            {
                switch (d)
                {
                    case Direction.up:
                        y--;
                        break;
                    case Direction.down:
                        y++;
                        break;
                    case Direction.left:
                        x--;
                        break;
                    case Direction.right:
                        x++;
                        break;
                }
                if (y < 0 || x < 0 || y > Game.numCellsY - 1 || x > Game.numCellsX - 1) return 0;
                if (Game.animalZones[y][x].occupant != null)
                {
                    if (Game.animalZones[y][x].occupant.species == target)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }
        private void Attack(Alien attacker, Direction d)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            int x = attacker.location.x;
            int y = attacker.location.y;

            switch (d)
            {
                case Direction.up:
                    Game.animalZones[y - 1][x].occupant = attacker;
                    turn++; //HL
                    break;
                case Direction.down:
                    Game.animalZones[y + 1][x].occupant = attacker;
                    turn++; //HL
                    break;
                case Direction.left:
                    Game.animalZones[y][x - 1].occupant = attacker;
                    turn++; //HL
                    break;
                case Direction.right:
                    Game.animalZones[y][x + 1].occupant = attacker;
                    turn++; //HL
                    break;
            }
            Game.animalZones[y][x].occupant = null;
        }
        
        public void Hunt(string specie)
        {
            if (Seek(location.x, location.y, Direction.up, specie, huntReactionDistance) != 0)
            {
                Attack(this, Direction.up);
            }
            else if (Seek(location.x, location.y, Direction.down, specie, huntReactionDistance) != 0)
            {
                Attack(this, Direction.down);
            }
            else if (Seek(location.x, location.y, Direction.left, specie, huntReactionDistance) != 0)
            {
                Attack(this, Direction.left);
            }
            else if (Seek(location.x, location.y, Direction.right, specie, huntReactionDistance) != 0)
            {
                Attack(this, Direction.right);
            }
        }

        /* hunt anyone
         */
        public void AutoHunt()
        {
            Hunt("cat");
            Hunt("chick");
            Hunt("mouse");
            Hunt("raptor");
        }

        public override void Activate()
        {
            Console.WriteLine("Alien is comming!");
            AutoHunt();
        }
    }
}