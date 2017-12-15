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
    class UIController
    {

        private Texture2D skin;

        private Spaceship spaceship;

        private Microsoft.Xna.Framework.Rectangle destinationRectangle;

        private SpriteFont font;


        public UIController(Texture2D skin, SpriteFont font, Size size)
        {

            this.skin = skin;
            this.font = font;
            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(CommonFunctions.borders.Left, CommonFunctions.borders.Bottom - size.Height, size.Width, size.Height);

        }

        public void Update()
        {

            spaceship = CommonFunctions.currentSpaceship;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(skin, destinationRectangle, Microsoft.Xna.Framework.Color.White);

            spriteBatch.DrawString(font, Convert.ToString(spaceship.ammunition), new Vector2(5 * CommonFunctions.aspectRatioMultiplierX, 612.5f * CommonFunctions.aspectRatioMultiplierY), Microsoft.Xna.Framework.Color.Black);
            spriteBatch.DrawString(font, Convert.ToString(spaceship.score), new Vector2(45 * CommonFunctions.aspectRatioMultiplierX, 612.5f * CommonFunctions.aspectRatioMultiplierY), Microsoft.Xna.Framework.Color.Black);

        }

    }
}
