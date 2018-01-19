using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_is_a_dangerous_place
{
    class GameStartController
    {
        private SpriteFont font;
        
        private SpaceshipController SpaceshipController;
        private MenuController meContr;
        private Game1 game;

        public bool gameStarted = false;

        public int menuPage = 0; //0->Startseite, 1->Schiff, 2->Difficulty
        private int shipChoiceSaver; //0->Spaceship, 1->Titan


        public GameStartController(SpaceshipController spShipContr, SpriteFont font, MenuController meContr, Game1 game)
        {

            this.font = font;

            SpaceshipController = spShipContr;
            CommonFunctions.currentGameStartController = this;

            this.meContr = meContr;
            this.game = game;
        }

        public void Update()
        {
            
            List<Texture2D> textureListActive = new List<Texture2D>();
            List<Texture2D> textureListPassive = new List<Texture2D>();
            textureListActive.Clear();
            textureListPassive.Clear();

            switch (menuPage)
            {
                case 0:
                    textureListActive.Add(CommonFunctions.ActiveButtonStart);
                    textureListActive.Add(CommonFunctions.ActiveButtonResetScore);
                    textureListActive.Add(CommonFunctions.ActiveButtonQuitGame);
                    textureListPassive.Add(CommonFunctions.PassiveButtonStart);
                    textureListPassive.Add(CommonFunctions.PassiveButtonResetScore);
                    textureListPassive.Add(CommonFunctions.PassiveButtonQuitGame); ;

                    switch (meContr.MenuControll(3, textureListActive, textureListPassive))
                    {
                        case 0:
                            menuPage = 1;
                            break;
                        case 1:
                            Properties.Settings.Default.Highscore = 0;
                            Properties.Settings.Default.Save();
                            break;
                        case 2:
                            game.Quit();
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    textureListActive.Add(CommonFunctions.ActiveButtonNormal);
                    textureListActive.Add(CommonFunctions.ActiveButtonTitan);
                    textureListActive.Add(CommonFunctions.ActiveButtonBack);
                    textureListPassive.Add(CommonFunctions.PassiveButtonNormal);
                    textureListPassive.Add(CommonFunctions.PassiveButtonTitan);
                    textureListPassive.Add(CommonFunctions.PassiveButtonBack);

                    switch (meContr.MenuControll(3, textureListActive, textureListPassive))
                    {
                        case 0:
                            shipChoiceSaver = 0;
                            menuPage = 2;
                            break;
                        case 1:
                            shipChoiceSaver = 1;
                            menuPage = 2;
                            break;
                        case 2:
                            menuPage = 0;
                            break;
                        default:
                            break;
                    }

                    break;
                case 2:
                    textureListActive.Add(CommonFunctions.ActiveButtonNormal);
                    textureListActive.Add(CommonFunctions.ActiveButtonRisky);
                    textureListActive.Add(CommonFunctions.ActiveButtonBack);
                    textureListPassive.Add(CommonFunctions.PassiveButtonNormal);
                    textureListPassive.Add(CommonFunctions.PassiveButtonRisky);
                    textureListPassive.Add(CommonFunctions.PassiveButtonBack);

                    switch (meContr.MenuControll(3, textureListActive, textureListPassive))
                    {
                        case 0:
                            if (shipChoiceSaver == 0)
                                SpaceshipController.SpawnShip(0);
                            else if (shipChoiceSaver == 1)
                                SpaceshipController.SpawnShip(2);

                            gameStarted = true;

                            break;
                        case 1:
                            if (shipChoiceSaver == 0)
                                SpaceshipController.SpawnShip(1);
                            else if (shipChoiceSaver == 1)
                                SpaceshipController.SpawnShip(3);

                            gameStarted = true;

                            break;
                        case 2:
                            menuPage = 1;
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }
            
            if (!gameStarted)
            {
                //spawnMode: 0=normal, 1=risky, 2=Titan, 3=risky Titan
                //diese shortcuts bleiben
                if (CommonFunctions.Input.IsKeyDown(Keys.N))
                {
                    SpaceshipController.SpawnShip(0);
                    gameStarted = true;
                }
                if (CommonFunctions.Input.IsKeyDown(Keys.R))
                {
                    SpaceshipController.SpawnShip(1); 
                    gameStarted = true;
                }
                if (CommonFunctions.Input.IsKeyDown(Keys.T))
                {
                    SpaceshipController.SpawnShip(2);
                    gameStarted = true;
                }
                if (CommonFunctions.Input.IsKeyDown(Keys.I))
                {
                    SpaceshipController.SpawnShip(3);
                    gameStarted = true;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            //spriteBatch.DrawString(font, "[N]ormal mode: everything is normal\n[R]isky mode: score *2, general speed *1.5\n[T]itan Saceship\nT[i]tan - risky modeHighscore: " + Properties.Settings.Default.Highscore, new Vector2(3, 3), Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);
            spriteBatch.DrawString(font, "Highscore: " + Properties.Settings.Default.Highscore, new Vector2(3, 3), Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);

            meContr.DrawMenu(spriteBatch);

        }

    }
}
