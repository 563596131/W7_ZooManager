using System;
namespace ZooManager
{
    public class Entity // highest level class
    {
        public Point location;
        public string name;
        public string emoji;
        public string species;
        public int reactionTime = 5; // default reaction time for animals (1 - 10)
        public int turn = 0; // a new trait tracking the turns for each Animal on the board 
        public bool Moved = false; // Define the move bool value in the activate function 
        public void ReportLocation()
        {
            Console.WriteLine($"I am at {location.x},{location.y}");
        }
        virtual public void Activate()
        {
            Console.WriteLine($"Animal {name} at {location.x},{location.y} activated");
        }
    }
}