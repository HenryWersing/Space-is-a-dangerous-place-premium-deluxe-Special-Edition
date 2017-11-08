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
    class Spaceship : ICollidable //erinnerung: dieses spaceship ist die spitze der ship erbstrucktur, also wenn andere arten der schiffe, erben diese von hier
    {
        
        private KeyboardState Input;

        private Vector2 position;
        public Vector2 PositionForRectangle { get; set; }
        public Size ObjectSize { get; set; }

        public float Speed { get; private set; }
        private Vector2 direction;

        public Texture2D Skin { get; private set; }
        public Texture2D BulletSkin { get; private set; }

        public float startingAmmunition = 3;
        public float startingScore = 0;

        public float ammunition;
        public float score;

        private Size standartBulletSize = new Size(3, 3); //todo: auf relative größe

        public List<Bullet> BulletList = new List<Bullet>();
        private Bullet newBullet;

        private float attackSpeed;
        private DateTime nextAttackTime;

        public float ammunitionMultiplier;
        public float scoreMultiplier;
        public float speedMultiplier;
        public float attackSpeedMultiplier;


        private System.Drawing.Rectangle borders;
        private Microsoft.Xna.Framework.Rectangle destinationRectangle;

        private List<Spaceship> spaceshipList;


        public Spaceship(Texture2D skin, Size size, Vector2 position, Texture2D bulletSkin, List<Spaceship> spaceshipList, float ammMu, float scoMu, float speMu, float AtsMu)
        {

            ammunitionMultiplier = ammMu;
            scoreMultiplier = scoMu;
            speedMultiplier = speMu;
            attackSpeedMultiplier = AtsMu;

            borders = CommonFunctions.borders;

            Skin = skin;
            ObjectSize = size;
            this.position = position;
            BulletSkin = bulletSkin;

            PositionForRectangle = position;

            Speed = borders.Right / 130 * speedMultiplier; //bei 650 breite: 5
            attackSpeed = 500 * 1 / attackSpeedMultiplier; //ms

            ammunition = startingAmmunition;
            score = startingScore;


            this.spaceshipList = spaceshipList;

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

        private void MoveLeftSlow()
        {

            if (position.X > borders.Left + Speed)
                direction.X -= 0.4f;

        }

        private void MoveRightSlow()
        {

            if (position.X < borders.Right - Speed - ObjectSize.Width)
                direction.X += 0.4f;

        }

        public void Shoot()
        {

            if (DateTime.Now > nextAttackTime && ammunition > 0)
            {
                ammunition--;

                nextAttackTime = DateTime.Now.AddMilliseconds(attackSpeed);
                
                Vector2 bulletPosition = new Vector2(position.X + ObjectSize.Width / 2 - standartBulletSize.Width / 2, position.Y - standartBulletSize.Height - 1);

                newBullet = new Bullet(BulletSkin, bulletPosition, standartBulletSize, 0, this);
                BulletList.Add(newBullet);
            }

        }

        public void ShootTripleShot(AmmoDrop initialDrop)
        {

            float yValue = initialDrop.position.Y + initialDrop.ObjectSize.Height / 2 - standartBulletSize.Height / 2;

            Vector2 leftBulletPosition = new Vector2(initialDrop.position.X, yValue);
            Vector2 midleBulletPosition = new Vector2(initialDrop.position.X + initialDrop.ObjectSize.Width / 2 - standartBulletSize.Width / 2, yValue);
            Vector2 rightBulletPosition = new Vector2(initialDrop.position.X + initialDrop.ObjectSize.Width - standartBulletSize.Width, yValue);

            newBullet = new Bullet(BulletSkin, leftBulletPosition, standartBulletSize, 1, this);
            BulletList.Add(newBullet);
            newBullet = new Bullet(BulletSkin, midleBulletPosition, standartBulletSize, 0, this);
            BulletList.Add(newBullet);
            newBullet = new Bullet(BulletSkin, rightBulletPosition, standartBulletSize, 2, this);
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

            //spaceshipList.Remove(this);
            CommonFunctions.gameRunning = false;
            CommonFunctions.ICollidableList.RemoveRange(0, CommonFunctions.ICollidableList.Count);
            BulletList.RemoveRange(0, BulletList.Count);
            ammunition = startingAmmunition;
            score = startingScore;

        }

        public void Update()
        {

            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);

            direction = Vector2.Zero;

            Input = Keyboard.GetState();
            
            if ((Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left)) && Input.IsKeyDown(Keys.LeftShift))
                MoveLeft();

            else if (Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left))
                MoveLeftSlow();

            if ((Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right)) && Input.IsKeyDown(Keys.LeftShift))
                MoveRight();

            else if (Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right))
                MoveRightSlow();
            
            if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
                Shoot();

            if (Input.IsKeyDown(Keys.Escape))
            {
                Destroy(this);
                spaceshipList.Remove(this);
                CommonFunctions.currentGameStartController.gameStarted = false;
            }

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
