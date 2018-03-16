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
    class Bullet : ICollidable
    {

        public Vector2 position;
        public Vector2 PositionForRectangle { get; set; }
        public Size ObjectSize { get; set; }

        private Spaceship parentSpaceship;

        private float Speed { get; set; } = 3;
        private Vector2 direction;

        private int generalDirection; //0 geradeaus, 1 links. 2 rechts

        public Texture2D Skin { get; private set; }

        private Microsoft.Xna.Framework.Rectangle destinationRectangle;


        public Bullet(Texture2D skin, Vector2 position, Size size, int generalDirection, Spaceship parentSpaceship)
        {

            Skin = skin;
            this.position = position;
            ObjectSize = size;
            this.generalDirection = generalDirection;
            this.parentSpaceship = parentSpaceship;

            PositionForRectangle = position;

        }

        public void MoveForward()
        {
            direction.Y -= 1f * CommonFunctions.aspectRatioMultiplierY;
        }

        public void MoveLeft()
        {
            direction.Y -= 0.7f * CommonFunctions.aspectRatioMultiplierY;
            direction.X -= 0.5f * CommonFunctions.aspectRatioMultiplierX;
        }

        public void MoveRight()
        {
            direction.Y -= 0.7f * CommonFunctions.aspectRatioMultiplierY;
            direction.X += 0.5f * CommonFunctions.aspectRatioMultiplierX;
        }

        public void CollisionsNConsequences()
        {

            ICollidable collision = CommonFunctions.CheckCollision(this, CommonFunctions.ICollidableList);

            if (collision is Terrain || collision is Ufo || collision is AmmoDrop || collision is UfoAmmoDrop || collision is UfoBombDrop)
            {
                collision.Destroy(this);
                Destroy(collision);
            }

            else if (collision is ScoreDrop || collision is UfoScoreDrop)
                collision.Destroy(this);

        }

        public void Destroy(ICollidable collidingObject)
        {

            parentSpaceship.BulletList.Remove(this);

        }

        public void Update()
        {

            if (position.Y < -200 * CommonFunctions.aspectRatioMultiplierY)
                Destroy(this);

            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);

            direction = Vector2.Zero;

            if (generalDirection == 0)
                MoveForward();
            if (generalDirection == 1)
                MoveLeft();
            if (generalDirection == 2)
                MoveRight();

            direction *= Speed* CommonFunctions.generalGameSpeed;
            position += direction;
            PositionForRectangle = position;

            CollisionsNConsequences();

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Skin, destinationRectangle, CommonFunctions.generalColour);

        }

    }
}
