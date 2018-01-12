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

//todo: shift fähigkeiten? spaceship wird schneller, titan...?
//todo: jedes spaceship hat eine up fähigkeit mit cooldown, z.B. kurz in terrain crashen zu können, oder score gegen munition tauschen?dann aber nur 5 mal verfügbar oder so
//todo: feedback holen, was am terrain verbessern
//todo: animationen?
//todo: in spaceship shift zum beschleunigen umschreiben-> moveleft wird moveleftfast und moveleftslow wird moveleft. shift soll ja die fähigkeit zum beschleunigen sein
namespace Space_is_a_dangerous_place
{
    class Spaceship : ICollidable //erinnerung: dieses spaceship ist die spitze der ship erbstrucktur, also wenn andere arten der schiffe, erben diese von hier
    {

        public KeyboardState Input;

        public Vector2 position;
        public Vector2 PositionForRectangle { get; set; }
        public Size ObjectSize { get; set; }

        public float Speed { get; set; }
        public Vector2 direction;

        public Texture2D Skin { get; private set; }
        public Texture2D BulletSkin { get; private set; }

        public float startingAmmunition = 3;
        public float startingScore = 0;

        public float ammunition;
        public float score;

        public bool paused = false;
        public static int pauseMenuNumberOfOptions = 2;

        public float generalSpeedSaver;

        public Size standartBulletSize;

        public List<Bullet> BulletList = new List<Bullet>();
        public Bullet newBullet;

        public float attackSpeed;
        public DateTime nextAttackTime;

        public float ammunitionMultiplier;
        public float scoreMultiplier;
        public float speedMultiplier;
        public float attackSpeedMultiplier;

        public Microsoft.Xna.Framework.Rectangle destinationRectangle;

        public List<Spaceship> spaceshipList;

        public MenuController meContr;


        public Spaceship(Texture2D skin, Size size, Vector2 position, Texture2D bulletSkin, List<Spaceship> spaceshipList, MenuController meContr, float ammMu, float scoMu, float speMu, float AtsMu)
        {

            this.meContr = meContr;
            ammunitionMultiplier = ammMu;
            scoreMultiplier = scoMu;
            speedMultiplier = speMu;
            attackSpeedMultiplier = AtsMu;

            Skin = skin;
            ObjectSize = size;
            this.position = position;
            BulletSkin = bulletSkin;

            PositionForRectangle = position;

            Speed = 5 * speedMultiplier;
            attackSpeed = 500 * attackSpeedMultiplier; //ms

            ammunition = startingAmmunition;
            score = startingScore;

            standartBulletSize = new Size(Convert.ToInt32(3 * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(3 * CommonFunctions.aspectRatioMultiplierY));


            this.spaceshipList = spaceshipList;

            CommonFunctions.currentSpaceship = this;

        }

        #region Moving
        public void MoveLeft()
        {

            if (position.X > CommonFunctions.borders.Left + Speed)
                direction.X -= 1f * CommonFunctions.aspectRatioMultiplierX;

        }

        public void MoveRight()
        {

            if (position.X < CommonFunctions.borders.Right - Speed - ObjectSize.Width)
                direction.X += 1f * CommonFunctions.aspectRatioMultiplierX;

        }

        public void MoveLeftSlow()
        {

            if (position.X > CommonFunctions.borders.Left + Speed)
                direction.X -= 0.4f * CommonFunctions.aspectRatioMultiplierX;

        }

        public void MoveRightSlow()
        {

            if (position.X < CommonFunctions.borders.Right - Speed - ObjectSize.Width)
                direction.X += 0.4f * CommonFunctions.aspectRatioMultiplierX;

        }
        #endregion

        #region shooting etc
        public virtual void Shoot()
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

            foreach (ICollidable collidable in CommonFunctions.ICollidableList)
            {
                if (collidable is Terrain)
                    collidable.Destroy(collidable);
            }

        }
        #endregion

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
            
            CommonFunctions.terrainSpawning = false;
            CommonFunctions.ICollidableList.RemoveRange(0, CommonFunctions.ICollidableList.Count);
            BulletList.RemoveRange(0, BulletList.Count);
            ammunition = startingAmmunition;
            if (score > Properties.Settings.Default.Highscore)
            {
                Properties.Settings.Default.Highscore = Convert.ToInt32(score);
                Properties.Settings.Default.Save();
            }
            score = startingScore;

        }

        public virtual void InputChecking()
        {

            Input = Keyboard.GetState();

            if (!paused)
            {
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

                if (Input.IsKeyDown(Keys.Enter) && !CommonFunctions.terrainSpawning)
                    CommonFunctions.currentTerrainController.StartRoutine();

                if (Input.IsKeyDown(Keys.Escape))
                {
                    paused = true;
                    meContr.pauseMenuNavigator = 0;
                    generalSpeedSaver = CommonFunctions.generalGameSpeed;
                    CommonFunctions.generalGameSpeed = 0;
                    CommonFunctions.generalColour = Microsoft.Xna.Framework.Color.Gray;
                }
            }

            if (paused)
            {
                menuControlls();
            }

        }

        public void menuControlls()
        {

            #region Texture Lists
            List<Texture2D> activeButtonsList = new List<Texture2D>();
            List<Texture2D> passiveButtonsList = new List<Texture2D>();
            activeButtonsList.Add(CommonFunctions.ActiveButtonContinue);
            activeButtonsList.Add(CommonFunctions.ActiveButonBackToMenu);
            passiveButtonsList.Add(CommonFunctions.PassiveButtonContinue);
            passiveButtonsList.Add(CommonFunctions.PassiveButtonBackToMenu);
            #endregion
            
            switch (meContr.MenuControll(2, activeButtonsList, passiveButtonsList))
            {
                case 0:
                    paused = false;
                    CommonFunctions.generalGameSpeed = generalSpeedSaver;
                    CommonFunctions.generalColour = Microsoft.Xna.Framework.Color.White;
                    break;
                case 1:
                    Destroy(this);
                    spaceshipList.Remove(this);
                    CommonFunctions.currentGameStartController.gameStarted = false;
                    CommonFunctions.currentGameStartController.menuPage = 0;
                    CommonFunctions.generalColour = Microsoft.Xna.Framework.Color.White;
                    break;
                default:
                    break;
            }

        }

        public void Update()
        {
            
            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);
            
            direction = Vector2.Zero;

            InputChecking();

            direction *= Speed* CommonFunctions.generalGameSpeed;
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

            spriteBatch.Draw(Skin, destinationRectangle, CommonFunctions.generalColour);

            foreach (Bullet bullet in BulletList)
                bullet.Draw(spriteBatch);

            if (paused)
            {
                meContr.DrawMenu(spriteBatch);
            }
        }

    }
}
