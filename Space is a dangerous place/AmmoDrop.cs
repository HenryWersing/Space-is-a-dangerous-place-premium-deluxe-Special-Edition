using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_is_a_dangerous_place
{
    class AmmoDrop : Drop
    {
        
        public AmmoDrop(Vector2 position, Size size, Texture2D skin, Terrain parentTerrain) : base(position, size, skin, parentTerrain)
        {

            rdm = new Random();

        }

        public override void Destroy(ICollidable collidingObject)
        {

            if (collidingObject is Spaceship)
            {
                rdmInt = rdm.Next(0, 2);
                if (rdmInt == 0)
                    CommonFunctions.currentSpaceship.ammunition += 1 * CommonFunctions.currentSpaceship.ammunitionMultiplier;
                if (rdmInt == 1)
                    CommonFunctions.currentSpaceship.ammunition += 2 * CommonFunctions.currentSpaceship.ammunitionMultiplier;
            }
            if (collidingObject is Bullet)
            {
                CommonFunctions.currentSpaceship.ammunition += 1 * CommonFunctions.currentSpaceship.ammunitionMultiplier;
                CommonFunctions.currentSpaceship.ShootTripleShot(this);
            }

            base.Destroy(collidingObject);

        }

    }
}
