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
    class Ufo : ICollidable
    {

        private Vector2 position;
        public Vector2 PositionForRectangle { get; set; }
        public Size ObjectSize { get; set; }

        public float Speed { get; set; } = 3;
        private Vector2 direction;

        private bool movingRight;
        private int randomXValue;

        private Microsoft.Xna.Framework.Rectangle destinationRectangle;

        private Texture2D skin;
        private Texture2D ufoAmmoDropSkin;
        private Texture2D ufoScoreDropSkin;
        private Texture2D ufoBombDropSkin;

        private UfoDrop newUfoDrop;

        private Random rdm;
        

        public Ufo(Vector2 position,Size size, Texture2D skin, Texture2D ufoAmmoDropSkin, Texture2D ufoScoreDropSkin, Texture2D ufoBombDropSkin)
        {
            
            this.position = position;
            ObjectSize = size;
            this.skin = skin;
            this.ufoAmmoDropSkin = ufoAmmoDropSkin;
            this.ufoScoreDropSkin = ufoScoreDropSkin;
            this.ufoBombDropSkin = ufoBombDropSkin;

            rdm = new Random();

            movingRight = true;
            randomXValue = rdm.Next(300, 500);

            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);

        }

        public void Moving()
        {

            direction.Y += 0.2f * CommonFunctions.aspectRatioMultiplierY;

            if (movingRight)
                direction.X += 0.6f * CommonFunctions.aspectRatioMultiplierX;

            if (!movingRight)
                direction.X -= 0.6f * CommonFunctions.aspectRatioMultiplierX;

            if (movingRight && position.X > randomXValue)
            {
                randomXValue = rdm.Next(Convert.ToInt32(180 * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(300 * CommonFunctions.aspectRatioMultiplierX));
                movingRight = false;
            }

            if (!movingRight && position.X < randomXValue)
            {
                randomXValue = rdm.Next(Convert.ToInt32(350 * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(470 * CommonFunctions.aspectRatioMultiplierX));
                movingRight = true;
            }

        }

        public void Destroy(ICollidable collidingObject)
        {

            int randomNumber = rdm.Next(1, 10);

            switch (randomNumber)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    newUfoDrop = new UfoAmmoDrop(position, ObjectSize, ufoAmmoDropSkin, null);
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    newUfoDrop = new UfoScoreDrop(position, ObjectSize, ufoScoreDropSkin, null);
                    break;
                case 9:
                    newUfoDrop = new UfoBombDrop(position, ObjectSize, ufoBombDropSkin, null);
                    break;
                default:
                    break;
            }
            
            CommonFunctions.ICollidableList.Add(newUfoDrop);

            CommonFunctions.ICollidableList.Remove(this);

        }

        public void Update()
        {

            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);

            direction = Vector2.Zero;

            Moving();

            direction *= Speed;
            position += direction;
            PositionForRectangle = position;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(skin, destinationRectangle, Microsoft.Xna.Framework.Color.White);

        }
        
    }
}
