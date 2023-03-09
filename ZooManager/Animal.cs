using System;
namespace ZooManager
{
    public class Animal:Entity
    {
        public int fleeReactionDistance = 1; // Defines the reaction distance for fleeing //HL
        public int huntReactionDistance = 1; // Defines the reaction distance for hunting //HL
        // Feature m //HL

        /* Feature f //HL
         * Modified function Seek with new parameter distance to seek zone one by one 
         * and finally return a int
         */
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

        /* This method currently assumes that the attacker has determined there is prey
         * in the target direction. In addition to bug-proofing our program, can you think
         * of creative ways that NOT just assuming the attack is on the correct target (or
         * successful for that matter) could be used?
         */

        private void Attack(Animal attacker, Direction d)
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

        /* We can't make the same assumptions with this method that we do with Attack, since
         * the animal here runs AWAY from where they spotted their target (using the Seek method
         * to find a predator in this case). So, we need to figure out if the direction that the
         * retreating animal wants to move is valid. Is movement in that direction still on the board?
         * Is it just going to send them into another animal? With our cat & mouse setup, one is the
         * predator and the other is prey, but what happens when we have an animal who is both? The animal
         * would want to run away from their predators but towards their prey, right? Perhaps we can generalize
         * this code (and the Attack and Seek code) to help our animals strategize more...
         */

        private bool Retreat(Animal runner, Direction d)
        {
            Console.WriteLine($"{runner.name} is retreating {d.ToString()}");
            int x = runner.location.x;
            int y = runner.location.y;

            switch (d)
            {
                case Direction.up:
                    /* The logic below uses the "short circuit" property of Boolean &&.
                     * If we were to check our list using an out-of-range index, we would
                     * get an error, but since we first check if the direction that we're modifying is
                     * within the ranges of our lists, if that check is false, then the second half of
                     * the && is not evaluated, thus saving us from any exceptions being thrown.
                     */
                    if (y > 0 && Game.animalZones[y - 1][x].occupant == null)
                    {
                        Game.animalZones[y - 1][x].occupant = runner;
                        Game.animalZones[y][x].occupant = null;
                        turn++; //HL
                        return true; // retreat was successful
                    }
                    return false; // retreat was not successful
                /* Note that in these four cases, in our conditional logic we check
                 * for the animal having one square between itself and the edge that it is
                 * trying to run to. For example,in the above case, we check that y is greater
                 * than 0, even though 0 is a valid spot on the list. This is because when moving
                 * up, the animal would need to go from row 1 to row 0. Attempting to go from row 0
                 * to row -1 would cause a runtime error. This is a slightly different way of testing
                 * if 
                 */
                case Direction.down:
                    if (y < Game.numCellsY - 1 && Game.animalZones[y + 1][x].occupant == null)
                    {
                        Game.animalZones[y + 1][x].occupant = runner;
                        Game.animalZones[y][x].occupant = null;
                        turn++; //HL
                        return true;
                    }
                    return false;
                case Direction.left:
                    if (x > 0 && Game.animalZones[y][x - 1].occupant == null)
                    {
                        Game.animalZones[y][x - 1].occupant = runner;
                        Game.animalZones[y][x].occupant = null;
                        turn++; //HL
                        return true;
                    }
                    return false;
                case Direction.right:
                    if (x < Game.numCellsX - 1 && Game.animalZones[y][x + 1].occupant == null)
                    {
                        Game.animalZones[y][x + 1].occupant = runner;
                        Game.animalZones[y][x].occupant = null;
                        turn++; //HL
                        return true;
                    }
                    return false;
            }
            return false; // fallback
        }

        public void Flee(string specie)
        {
            if (Seek(location.x, location.y, Direction.up, specie, fleeReactionDistance) != 0)
            {
                if (Retreat(this, Direction.down)) return;
            }
            if (Seek(location.x, location.y, Direction.down, specie, fleeReactionDistance) != 0)
            {
                if (Retreat(this, Direction.up)) return;
            }
            if (Seek(location.x, location.y, Direction.left, specie, fleeReactionDistance) != 0)
            {
                if (Retreat(this, Direction.right)) return;
            }
            if (Seek(location.x, location.y, Direction.right, specie, fleeReactionDistance) != 0)
            {
                if (Retreat(this, Direction.left)) return;
            }
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

        public void DoubleHunt(string specie1, string specie2)
        {
            if (Seek(location.x, location.y, Direction.up, specie1, huntReactionDistance) != 0 || Seek(location.x, location.y, Direction.up, specie2, huntReactionDistance) !=0)
            {
                Attack(this, Direction.up);
            }
            else if (Seek(location.x, location.y, Direction.down, specie1, huntReactionDistance) != 0 || Seek(location.x, location.y, Direction.down, specie2, huntReactionDistance) != 0)
            {
                Attack(this, Direction.down);
            }
            else if (Seek(location.x, location.y, Direction.left, specie1, huntReactionDistance) != 0 || Seek(location.x, location.y, Direction.left, specie2, huntReactionDistance) != 0)
            {
                Attack(this, Direction.left);
            }
            else if (Seek(location.x, location.y, Direction.right, specie1, huntReactionDistance) != 0 || Seek(location.x, location.y, Direction.right, specie2, huntReactionDistance) != 0)
            {
                Attack(this, Direction.right);
            }
        }
        
    }
}
