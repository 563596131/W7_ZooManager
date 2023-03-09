using System;
namespace ZooManager
{
    public class Zone
    {
        private Entity _occupant = null;
        public Entity occupant
        {
            get { return _occupant; }
            set {
                _occupant = value;
                if (_occupant != null) {
                    _occupant.location = location;
                }
            }
        }

        public Point location;

        public string emoji
        {
            get
            {
                if (occupant == null) return "";
                return occupant.emoji;
            }
        }

        public string rtLabel
        {
            get
            {
                if (occupant == null) return "";
                return occupant.reactionTime.ToString();
            }
        }

        /* Feature r //HL
         * the turns label on the right-top corner of emoji
         */
        public string ltLabel
        {
            get
            {
                if (occupant == null) return "";
                return occupant.turn.ToString();
            }
        }

        public Zone(int x, int y, Entity entity)
        {
            location.x = x;
            location.y = y;

            occupant = entity;
        }
    }
}
