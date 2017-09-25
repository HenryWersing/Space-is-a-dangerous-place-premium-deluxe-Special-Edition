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

        private int Speed { get; set; } = 3;
        private Vector2 direction;

        public Texture2D Skin { get; private set; }

        private Microsoft.Xna.Framework.Rectangle destinationRectangle;


        public Bullet(Texture2D skin, Vector2 position, Size size)
        {

            Skin = skin;
            this.position = position;
            ObjectSize = size;

            PositionForRectangle = position;

        }

        public void MoveForward()
        {
            direction.Y -= 1;
        }

        public void CollisionsNConsequences()
        {

            ICollidable collision = CommonFunctions.CheckCollision(this, CommonFunctions.ICollidableList);

            if (collision is Terrain)
            {
                collision.Destroy(this);
                Destroy(collision);
            }

            else if (collision is AmmoDrop)
                collision.Destroy(this);

            else if (collision is ScoreDrop)
                collision.Destroy(this);

            else if (collision is Ufo)
            {
                collision.Destroy(this);
                Destroy(collision);
            }

        }

        public void Destroy(ICollidable collidingObject)
        {
            //todo: destroy
            position = new Vector2(-1000, 1000);

        }

        public void Update()
        {

            if (position.Y < -200)
                Destroy(this);

            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);

            direction = Vector2.Zero;

            MoveForward();

            direction *= Speed;
            position += direction;
            PositionForRectangle = position;

            CollisionsNConsequences();

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Skin, destinationRectangle, Microsoft.Xna.Framework.Color.White);

        }

    }
}
