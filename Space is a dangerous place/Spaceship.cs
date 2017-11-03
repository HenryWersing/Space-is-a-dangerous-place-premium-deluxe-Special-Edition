using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Space_is_a_dangerous_place
{
    class Spaceship : ICollidable
    {

        //todo: spaceship controller, später spieleinstieg controller
        private KeyboardState Input;

        private Vector2 position;
        public Vector2 PositionForRectangle { get; set; }
        public Size ObjectSize { get; set; }

        public int Speed { get; private set; }
        private Vector2 direction;

        public Texture2D Skin { get; private set; }
        public Texture2D BulletSkin { get; private set; }

        public int ammunition = 3;
        public int score = 0;

        private Size standartBulletSize = new Size(3, 3); //todo: auf relative größe

        public List<Bullet> BulletList = new List<Bullet>();
        private Bullet newBullet;

        private float attackSpeed = 500; //ms
        private DateTime nextAttackTime;

        private System.Drawing.Rectangle borders;
        private Microsoft.Xna.Framework.Rectangle destinationRectangle;


        public Spaceship(Texture2D skin, Size size, Texture2D bulletSkin)
        {
         
            borders = CommonFunctions.borders;
            
            Skin = skin;
            ObjectSize = size;
            position.X = borders.Right / 2 - ObjectSize.Width / 2;
            position.Y = borders.Bottom - ObjectSize.Height;
            BulletSkin = bulletSkin;

            PositionForRectangle = position;

            Speed = borders.Right / 130; //bei 650 breite: 5


            CommonFunctions.currentSpaceship = this;

        }

        private void MoveLeft()
        {

            if (position.X > borders.Left + Speed)
                direction.X -= 1;

        }

        private void MoveRight()
        {

            if (position.X < borders.Right - Speed - ObjectSize.Width)
                direction.X += 1;

        }

        public void Shoot()
        {

            if (DateTime.Now > nextAttackTime && ammunition > 0)
            {
                ammunition--;

                nextAttackTime = DateTime.Now.AddMilliseconds(attackSpeed);
                
                Vector2 bulletPosition = new Vector2(position.X + ObjectSize.Width / 2 - standartBulletSize.Width / 2, position.Y - standartBulletSize.Height - 1);

                newBullet = new Bullet(BulletSkin, bulletPosition, standartBulletSize, 0);
                BulletList.Add(newBullet);
            }

        }

        public void ShootTripleShot(AmmoDrop initialDrop)
        {

            float yValue = initialDrop.position.Y + initialDrop.ObjectSize.Height / 2 - standartBulletSize.Height / 2;

            Vector2 leftBulletPosition = new Vector2(initialDrop.position.X, yValue);
            Vector2 midleBulletPosition = new Vector2(initialDrop.position.X + initialDrop.ObjectSize.Width / 2 - standartBulletSize.Width / 2, yValue);
            Vector2 rightBulletPosition = new Vector2(initialDrop.position.X + initialDrop.ObjectSize.Width - standartBulletSize.Width, yValue);

            newBullet = new Bullet(BulletSkin, leftBulletPosition, standartBulletSize, 1);
            BulletList.Add(newBullet);
            newBullet = new Bullet(BulletSkin, midleBulletPosition, standartBulletSize, 0);
            BulletList.Add(newBullet);
            newBullet = new Bullet(BulletSkin, rightBulletPosition, standartBulletSize, 2);
            BulletList.Add(newBullet);

        }

        public void ScreenWipe()
        {
            
            foreach(ICollidable collidable in CommonFunctions.ICollidableList)
            {
                if (collidable is Terrain)
                    collidable.Destroy(collidable);
            }

        }

        public void CollisionsNConsequences()
        {

            ICollidable collision = CommonFunctions.CheckCollision(this, CommonFunctions.ICollidableList);

            if (collision is Terrain || collision is Ufo || collision is UfoBombDrop)
                Destroy(collision);

            else if (collision is AmmoDrop || collision is ScoreDrop || collision is UfoAmmoDrop || collision is UfoScoreDrop)
                collision.Destroy(this);

        }

        public void Destroy(ICollidable collidingObject)
        {

            //todo: spaceship destroy einbauen

        }

        public void Update()
        {

            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);

            direction = Vector2.Zero;

            Input = Keyboard.GetState();

            //todo: langsames bewegen
            if (Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left))
                MoveLeft();

            else if (Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right))
                MoveRight();

            else if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
                Shoot();

            direction *= Speed;
            position += direction;
            PositionForRectangle = position;
            
            CollisionsNConsequences();
            
            for (int i = 0; i < BulletList.Count; i++)
            {
                BulletList[i].Update();
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Skin, destinationRectangle, Microsoft.Xna.Framework.Color.White);

            foreach (Bullet bullet in BulletList)
                bullet.Draw(spriteBatch);
            
        }

    }
}
