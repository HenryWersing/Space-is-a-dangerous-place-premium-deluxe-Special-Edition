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
    class UfoDrop:Drop
    {
        
        public float speed;


        public UfoDrop(Vector2 position, Size size, Texture2D skin, Terrain parentTerrain) : base(position, size, skin, parentTerrain)
        {
            speed = CommonFunctions.normalDownwardSpeed;
        }

        public void FreeMovement()
        {

            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);

            direction = Vector2.Zero;

            direction.Y += 1f * CommonFunctions.aspectRatioMultiplierY;

            direction *= speed * CommonFunctions.generalGameSpeed;
            position += direction;
            PositionForRectangle = position;

        }


        public override void Update()
        {

            FreeMovement();

        }

    }
}
