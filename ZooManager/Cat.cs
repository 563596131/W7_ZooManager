using System;

namespace ZooManager
{
    public class Cat : Animal, Prey, Predator
    {
        // Feature e //HL
        public Cat(string name)
        {
            emoji = "🐱";
            species = "cat";
            this.name = name;
            reactionTime = new Random().Next(1, 6); // reaction time 1 (fast) to 5 (medium)
        }

        public void AutoFlee()
        {
            Flee("raptor");
        }
        
        public void AutoHunt()
        {
            DoubleHunt("mouse", "chick"); // second will hunt both mouse and chick
        }
        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a cat. Meow.");
            AutoFlee();
            AutoHunt();
        }

        /* Note that our cat is currently not very clever about its hunting.
         * It will always try to attack "up" and will only seek "down" if there
         * is no mouse above it. This does not affect the cat's effectiveness
         * very much, since the overall logic here is "look around for a mouse and
         * attack the first one you see." This logic might be less sound once the
         * cat also has a predator to avoid, since the cat may not want to run in
         * to a square that sets it up to be attacked!
         */
    }
}

