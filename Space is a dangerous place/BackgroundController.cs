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
    class BackgroundController
    {

        private Texture2D background1;
        private Texture2D background2;

        private Vector2 direction;
        private Vector2 position;
        private Vector2 position2;
        private Size size;
        private Size size2;
        private Microsoft.Xna.Framework.Rectangle destinationRectangle;
        private Microsoft.Xna.Framework.Rectangle destinationRectangle2;


        public BackgroundController(Texture2D background1, Texture2D background2)
        {

            this.background1 = background1;
            this.background2 = background2;

            position = new Vector2(0, 0);
            position2 = new Vector2(0, -1600);
            size = new Size(700, 1600);
            size2 = new Size(700, 1600);

        }

        private void MoveBackground()
        {

            if (CommonFunctions.gameRunning)
                direction.Y += 1;

        }

        public void Update()
        {
            
            direction = Vector2.Zero;

            MoveBackground();

            position += direction;
            
            position2 += direction;

            if (position.Y > CommonFunctions.borders.Bottom)
                position.Y -= size.Height * 2;

            if (position2.Y > CommonFunctions.borders.Bottom)
                position2.Y -= size2.Height * 2;

            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), size.Width, size.Height);
            destinationRectangle2 = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position2.X), Convert.ToInt32(position2.Y), size2.Width, size2.Height);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background1, destinationRectangle, Microsoft.Xna.Framework.Color.White);
            spriteBatch.Draw(background2, destinationRectangle2, Microsoft.Xna.Framework.Color.White);

        }

    }
}
