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
    class UfoAmmoDrop : UfoDrop
    {

        public UfoAmmoDrop(Vector2 position, Size size, Texture2D skin, Terrain parentTerrain) : base(position, size, skin, parentTerrain)
        {

        }

        public override void Destroy(ICollidable collidingObject)
        {

            if (collidingObject is Spaceship)
                CommonFunctions.currentSpaceship.ammunition += 3 * CommonFunctions.currentSpaceship.ammunitionMultiplier;

            if (collidingObject is Bullet)
            {
                CommonFunctions.currentSpaceship.ammunition += 2 * CommonFunctions.currentSpaceship.ammunitionMultiplier;
                CommonFunctions.currentSpaceship.ScreenWipe();
            }

            base.Destroy(collidingObject);

        }
        
    }
}
