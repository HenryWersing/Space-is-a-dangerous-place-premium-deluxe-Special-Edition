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
    class ScoreDrop : Drop
    {
        
        public ScoreDrop(Vector2 position, Size size, Texture2D skin, Terrain parentTerrain) : base(position, size, skin, parentTerrain)
        {
            
        }

        public override void Destroy(ICollidable collidingObject)
        {

            if (collidingObject is Spaceship)
                CommonFunctions.currentSpaceship.score += 1 * CommonFunctions.currentSpaceship.scoreMultiplier;

            if (collidingObject is Bullet)
                if (CommonFunctions.currentSpaceship is Titan)
                    CommonFunctions.currentSpaceship.score += 1 * CommonFunctions.currentSpaceship.scoreMultiplier;
                else
                    CommonFunctions.currentSpaceship.score += 2 * CommonFunctions.currentSpaceship.scoreMultiplier;

            base.Destroy(collidingObject);

        }

    }
}
