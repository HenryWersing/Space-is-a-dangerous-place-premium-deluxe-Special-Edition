﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_is_a_dangerous_place
{
    class UfoScoreDrop : UfoDrop
    {

        public UfoScoreDrop(Vector2 position, Size size, Texture2D skin, Terrain parentTerrain) : base(position, size, skin, parentTerrain)
        {

        }

        public override void Destroy(ICollidable collidingObject)
        {

            if (collidingObject is Spaceship)
                CommonFunctions.currentSpaceship.score += 2 * CommonFunctions.currentSpaceship.scoreMultiplier;

            if (collidingObject is Bullet)
                CommonFunctions.currentSpaceship.score += 4 * CommonFunctions.currentSpaceship.scoreMultiplier;

            base.Destroy(collidingObject);

        }
        
    }
}
