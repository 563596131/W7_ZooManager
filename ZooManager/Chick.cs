using System;
namespace ZooManager
{
    /* Feature c //HL
     * A new subclass of class Bird 
     * Has all the properties of the "grandfather" class Animal
     */
    public class Chick : Bird
    {
        public Chick(string name)
        {
            emoji = "🐥";
            species = "chick";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(6, 11); // reaction time of 6 (medium) to 10 (slow)
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a chick. Tweet.");
            Flee("cat"); // will flee from cat
        }
    }
}

