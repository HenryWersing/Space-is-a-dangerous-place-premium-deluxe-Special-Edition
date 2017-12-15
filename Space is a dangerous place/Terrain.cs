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
    class Terrain : ICollidable
    {

        public Vector2 position;
        public Vector2 PositionForRectangle { get; set; }
        public Size ObjectSize { get; set; }

        public float Speed { get; private set; }
        private Vector2 direction;

        public Texture2D Skin { get; private set; }
        public Texture2D SkinBrokenLeft { get; private set; }
        public Texture2D SkinBrokenRight { get; private set; }
        public Texture2D AmmoDropSkin { get; private set; }
        public Texture2D ScoreDropSkin { get; private set; }

        private AmmoDrop newAmmoDrop;
        private ScoreDrop newScoreDrop;

        private Microsoft.Xna.Framework.Rectangle destinationRectangle;

        public bool broken = false;
        private bool brokenLeft;


        public Terrain(Texture2D skin, Vector2 position, Size size, Texture2D ammoDropSkin, Texture2D scoreDropSkin, Texture2D brokenLeftSkin, Texture2D brokenRightSkin)
        {

            Skin = skin;
            SkinBrokenLeft = brokenLeftSkin;
            SkinBrokenRight = brokenRightSkin;
            this.position = position;
            ObjectSize = size;
            AmmoDropSkin = ammoDropSkin;
            ScoreDropSkin = scoreDropSkin;

            PositionForRectangle = position;

            Speed = CommonFunctions.normalDownwardSpeed;

            Random rdm = new Random();
            SpawnDrops(rdm.Next(1, 29));

        }

        private void MoveDownward()
        {

            direction.Y += 1 * CommonFunctions.aspectRatioMultiplierY;

        }

        public void SpawnDrops(int randomNumber)
        {

            Size dropSize = new Size(Convert.ToInt32(25 * CommonFunctions.aspectRatioMultiplierX),Convert.ToInt32( 25 * CommonFunctions.aspectRatioMultiplierY));
            Vector2 dropPosition = CommonFunctions.DetermineDropPosition(this, dropSize);

            if (randomNumber == 1 || randomNumber == 2 || randomNumber == 3)
            {
                newAmmoDrop = new AmmoDrop(dropPosition, dropSize, AmmoDropSkin, this);
                CommonFunctions.ICollidableList.Add(newAmmoDrop);
            }

            if (randomNumber == 4 || randomNumber == 5 || randomNumber == 6 || randomNumber == 7 || randomNumber == 8)
            {
                newScoreDrop = new ScoreDrop(dropPosition, dropSize, ScoreDropSkin, this);
                CommonFunctions.ICollidableList.Add(newScoreDrop);
            }

        }

        public void Destroy(ICollidable collidingObject)
        {

            if (!broken)
            {
                broken = true;

                Random rdm = new Random();
                int brokenWidth = rdm.Next(Convert.ToInt32(120 * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(170 * CommonFunctions.aspectRatioMultiplierX));
                ObjectSize = new Size(brokenWidth, ObjectSize.Height);

                if (position.X != CommonFunctions.borders.Left)
                    position.X = 650 * CommonFunctions.aspectRatioMultiplierX - ObjectSize.Width;

                if (position.X == CommonFunctions.borders.Left)
                    brokenLeft = true;

                else
                    brokenLeft = false;
            }

        }

        public void Update()
        {

            if (position.Y - 50 > CommonFunctions.borders.Bottom)
                CommonFunctions.ICollidableList.Remove(this);

            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);

            direction = Vector2.Zero;

            MoveDownward();

            direction *= Speed;
            position += direction;
            PositionForRectangle = position;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (broken == false)
                spriteBatch.Draw(Skin, destinationRectangle, Microsoft.Xna.Framework.Color.White);

            if (broken == true && brokenLeft == true)
                spriteBatch.Draw(SkinBrokenLeft, destinationRectangle, Microsoft.Xna.Framework.Color.White);

            if (broken == true && brokenLeft == false)
                spriteBatch.Draw(SkinBrokenRight, destinationRectangle, Microsoft.Xna.Framework.Color.White);

        }

    }
}
