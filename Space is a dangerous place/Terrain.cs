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

        public float Speed { get; private set; } = 2;
        private Vector2 direction;

        public System.Drawing.Rectangle borders;

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

            borders = CommonFunctions.borders;

            Skin = skin;
            SkinBrokenLeft = brokenLeftSkin;
            SkinBrokenRight = brokenRightSkin;
            this.position = position;
            ObjectSize = size;
            AmmoDropSkin = ammoDropSkin;
            ScoreDropSkin = scoreDropSkin;

            PositionForRectangle = position;

            Random rdm = new Random();
            SpawnDrops(rdm.Next(1, 29));

        }

        private void MoveDownward()
        {

            direction.Y = +1;

        }

        public void SpawnDrops(int randomNumber)
        {

            Size dropSize = new Size(borders.Right * 05 / 130, borders.Bottom * 05 / 130); //  0.5 / 13
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
                int brokenWidth = rdm.Next(borders.Right * 24 / 130, borders.Right * 34 / 130); //  2.4 / 13, 3.4 / 13
                ObjectSize = new Size(brokenWidth, ObjectSize.Height);

                if (position.X != borders.Left)
                    position.X = borders.Right - ObjectSize.Width;

                if (position.X == borders.Left)
                    brokenLeft = true;

                else
                    brokenLeft = false;
            }

        }
        
        public void Update()
        {

            if (position.Y - 50 > borders.Bottom)
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
