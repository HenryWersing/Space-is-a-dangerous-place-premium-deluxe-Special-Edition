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
        private MySQLController mySQLContr;
        private Texture2D background;
        private Game1 game;

        public bool gameStarted = false;
        //TODO: im main menu highscoreliste
        public int menuPage = 0; //0->Startseite, 1->Schiff, 2->Difficulty, 3->Tutorialscreen, 4->Options, 5->resolution
        private int shipChoiceSaver; //0->Spaceship, 1->Titan

        private string tutorialText = "Controls in Menu:\nW + S / Up + Down to navigate, Enter to select  /  Mouse\n\nControls in Game:\nA + D / Left + Right to move, S / Down to shoot, Escape to open menu\n\nTips:\nYou can either collect or shoot the drops. The green drops give you\nammounition, the purple ones rise your score. Shooting them generally\ngives you more, but while shooting ammo-drops is almost always a good\nidea, you might run out of ammo when you shoot the score-drops as well.\nThe UFOs leave stronger drops when destroyed, but be warned:\nsometimes they leave bombs, which act like terrain.\n\nIn this game you choose between Normal Mode and Riscy Mode. In\nRiscy Mode everything is much faster but you also gain double the score.\n\nWhen using the smaller spaceship you can use shift to accelerate.\n\nThere are shortcuts in the menu: [n]ormal mode with normal spaceship,\n[r]iscy mode with normal spaceship, [t]itan in normal mode and\nt[i]tan in riscy mode.\n\n\nPress Enter to leave.";
        private bool showTutorial = false;
        public bool scoreSubmitted = true;

        private Rectangle borders;
        private Rectangle backgroundRectangle;
        
        
        public GameStartController(SpaceshipController spShipContr, MenuController meContr, MySQLController mySQLContr, Texture2D background, Game1 game)
        {

            font = CommonFunctions.font;
            
            SpaceshipController = spShipContr;
            CommonFunctions.currentGameStartController = this;

            this.meContr = meContr;
            this.mySQLContr = mySQLContr;
            this.background = background;
            this.game = game;

            borders = new Rectangle(CommonFunctions.borders.Left, CommonFunctions.borders.Top, CommonFunctions.borders.Right, CommonFunctions.borders.Bottom);
            backgroundRectangle = new Rectangle(borders.Left, borders.Top, Convert.ToInt32(700 * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(1600 * CommonFunctions.aspectRatioMultiplierY));
            
        }
        
        public void Update()
        {

            if (Properties.Settings.Default.Name=="")
            {
                CommonFunctions.currentTextInputController.Update("Please enter your name:");
            }
            else
            { 

                List<Texture2D> textureListActive = new List<Texture2D>();
                List<Texture2D> textureListPassive = new List<Texture2D>();
                textureListActive.Clear();
                textureListPassive.Clear();

                switch (menuPage)
                {
                    case 0:
                        textureListActive.Add(CommonFunctions.ActiveButtonStart);
                        textureListActive.Add(CommonFunctions.ActiveButtonOptions);
                        textureListActive.Add(CommonFunctions.ActiveButtonTutorial);
                        textureListActive.Add(CommonFunctions.ActiveButtonQuitGame);
                        textureListPassive.Add(CommonFunctions.PassiveButtonStart);
                        textureListPassive.Add(CommonFunctions.PassiveButtonOptions);
                        textureListPassive.Add(CommonFunctions.PassiveButtonTutorial);
                        textureListPassive.Add(CommonFunctions.PassiveButtonQuitGame);

                        switch (meContr.MenuControll(4, textureListActive, textureListPassive))
                        {
                            case 0:
                                menuPage = 1;
                                break;
                            case 1:
                                menuPage = 4;
                                break;
                            case 2:
                                menuPage = 3;
                                break;
                            case 3:
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
                    case 3:
                        showTutorial = true;

                        textureListActive.Add(CommonFunctions.ActiveButtonBack);
                        textureListPassive.Add(CommonFunctions.PassiveButtonBack);

                        if (meContr.MenuControll(1, textureListActive, textureListPassive) == 0)
                        {
                            showTutorial = false;
                            menuPage = 0;
                        }

                        break;
                    case 4:
                        textureListActive.Add(CommonFunctions.ActiveButtonResetScore);
                        textureListActive.Add(CommonFunctions.ActiveButtonSubmitScore);
                        textureListActive.Add(CommonFunctions.ActiveButtonChangeName);
                        //textureListActive.Add(CommonFunctions.ActiveButtonResolution);
                        textureListActive.Add(CommonFunctions.ActiveButtonBack);
                        textureListPassive.Add(CommonFunctions.PassiveButtonResetScore);
                        textureListPassive.Add(CommonFunctions.PassiveButtonSubmitScore);
                        textureListPassive.Add(CommonFunctions.PassiveButtonChangeName);
                        //textureListPassive.Add(CommonFunctions.PassiveButtonResolution);
                        textureListPassive.Add(CommonFunctions.PassiveButtonBack);

                        switch (meContr.MenuControll(4, textureListActive, textureListPassive))
                        {
                            case 0:
                                Properties.Settings.Default.Highscore = 0;
                                Properties.Settings.Default.Save();
                                break;
                            case 1:
                                if (!scoreSubmitted)
                                {
                                    mySQLContr.insertHighscore(Properties.Settings.Default.Name, Properties.Settings.Default.Highscore);
                                    scoreSubmitted = true;
                                }
                                break;
                            case 2:
                                Properties.Settings.Default.Name = "";
                                Properties.Settings.Default.Save();
                                break;
                            case 3:
                                menuPage = 0;
                                break;
                                /*
                            case 2:
                                menuPage = 5;
                                break;
                                */
                            default:
                                break;
                        }

                        break;
                    case 5:
                        
                        textureListActive.Add(CommonFunctions.ActiveButton500x500);
                        textureListActive.Add(CommonFunctions.ActiveButton650x650);
                        textureListActive.Add(CommonFunctions.ActiveButton800x800);
                        textureListActive.Add(CommonFunctions.ActiveButton950x950);
                        textureListActive.Add(CommonFunctions.ActiveButtonBack);
                        textureListPassive.Add(CommonFunctions.PassiveButton500x500);
                        textureListPassive.Add(CommonFunctions.PassiveButton650x650);
                        textureListPassive.Add(CommonFunctions.PassiveButton800x800);
                        textureListPassive.Add(CommonFunctions.PassiveButton950x950);
                        textureListPassive.Add(CommonFunctions.PassiveButtonBack);

                        switch (meContr.MenuControll(5, textureListActive, textureListPassive))
                        {
                            case 0:
                                game.setResolution(500, 500); //TODO: reload window
                                break;                          //dann wieder in optionen einbauen
                            case 1:
                                game.setResolution(650, 650);
                                break;
                            case 2:
                                game.setResolution(800, 800);
                                break;
                            case 3:
                                game.setResolution(950, 950);
                                break;
                            case 4:
                                menuPage = 0;
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

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            if (Properties.Settings.Default.Name == "")
            {
                CommonFunctions.currentTextInputController.Draw(spriteBatch);
            }
            else
            {

                spriteBatch.Draw(background, backgroundRectangle, Color.White);

                spriteBatch.DrawString(font, "Highscore: " + Properties.Settings.Default.Highscore, new Vector2(3 * CommonFunctions.aspectRatioMultiplierX, 3 * CommonFunctions.aspectRatioMultiplierY), Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);

                if(!scoreSubmitted)
                    spriteBatch.DrawString(font, "Highscore submitted! View GLOBAL highscores in the main menu.", new Vector2(3 * CommonFunctions.aspectRatioMultiplierX, 26 * CommonFunctions.aspectRatioMultiplierY), Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);

                meContr.DrawMenu(spriteBatch);

                if (showTutorial)
                {
                    spriteBatch.Draw(background, borders, Color.Black);
                    spriteBatch.DrawString(font, tutorialText, new Vector2(3 * CommonFunctions.aspectRatioMultiplierX, 3 * CommonFunctions.aspectRatioMultiplierY), Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);
                }

            }

        }

    }
}
