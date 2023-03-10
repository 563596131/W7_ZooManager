using System;

namespace ZooManager
{
    /* Feature a //HL
     * Feature d //HL
     * A new subclass of class Bird 
     * Has all the properties of the "grandfather" class Animal
     */
    public class Raptor : Bird, Predator
    {
        public Raptor(string name)
        {
            emoji = "🦅";
            species = "raptor";
            this.name = name;
            reactionTime = 1; // reaction time 1 (fast)
        }
        
        public void AutoHunt()
        {
            DoubleHunt("cat", "mouse"); // will hunt both cat and mouse
        }
        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a raptor. Humming.");
            AutoHunt();
        }
    }
}

