using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_is_a_dangerous_place
{
    public interface ICollidable
    {

        Size ObjectSize { get; set; }
        Vector2 PositionForRectangle { get; set; }

        void Destroy(ICollidable collidingObject);

    }
}
