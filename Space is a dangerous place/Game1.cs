﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Space_is_a_dangerous_place
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        System.Drawing.Rectangle borders;

        SpaceshipController ShipController;
        TerrainController TerraContr;
        UfoController UfoContr;
        UIController UIContr;
        BackgroundController BgContr;
        GameStartController GSContr;
        MenuController MeContr;
        TextInputController teInContr;
        MySQLController mySQLContr;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            setResolution(650, 650);
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load textures
            Texture2D StandartSpaceshipSkin = Content.Load<Texture2D>("raumschiffblid_t");
            Texture2D StandartTitanSkin = Content.Load<Texture2D>("TitanImage_t");
            Texture2D StandartBulletSkin = Content.Load<Texture2D>("BulletBild");
            Texture2D StandartTerrainSkin = Content.Load<Texture2D>("geländebild3");
            Texture2D StandartTerrainBrokenLeftSkin = Content.Load<Texture2D>("geländzerstörtbildlinks_t");
            Texture2D StandartTerrainBrokenRightSkin = Content.Load<Texture2D>("geländezerstörtbildrechts_t");
            Texture2D StandartUfoSkin = Content.Load<Texture2D>("alienbild_t");
            Texture2D StandartUfoLootAmmoSkin = Content.Load<Texture2D>("alienlootmunition_t");
            Texture2D StandartUfoLootScoreSkin = Content.Load<Texture2D>("alienlootscore_t");
            Texture2D StandartUfoLootBombSkin = Content.Load<Texture2D>("alienlootbombe_t");
            Texture2D StandartScoreDropSkin = Content.Load<Texture2D>("scorebild_t");
            Texture2D StandartAmmoDropSkin = Content.Load<Texture2D>("munitionsbild_t");
            Texture2D StandartUISkin = Content.Load<Texture2D>("UIBild_t");
            Texture2D Background1 = Content.Load<Texture2D>("Hintergrund1");
            Texture2D PointerTexture = Content.Load<Texture2D>("siadpPointerTexture_t");
            
            SpriteFont font = Content.Load<SpriteFont>("UIFont");

            #region buttonskins
            Texture2D ActiveButtonContinue = Content.Load<Texture2D>("ActiveButtonContinue");
            Texture2D ActiveButtonBackToMenu = Content.Load<Texture2D>("ActiveButtonBackToMenu");
            Texture2D ActiveButtonTitan = Content.Load<Texture2D>("ActiveButtonTitan");
            Texture2D ActiveButtonNormal = Content.Load<Texture2D>("ActiveButtonNormal");
            Texture2D ActiveButtonRisky = Content.Load<Texture2D>("ActiveButtonRisky");
            Texture2D ActiveButtonStart = Content.Load<Texture2D>("ActiveButtonStart");
            Texture2D ActiveButtonBack = Content.Load<Texture2D>("ActiveButtonBack");
            Texture2D ActiveButtonQuitGame = Content.Load<Texture2D>("ActiveButtonQuitGame");
            Texture2D ActiveButtonResetScore = Content.Load<Texture2D>("ActiveButtonResetScore");
            Texture2D ActiveButtonTutorial = Content.Load<Texture2D>("ActiveButtonTutorial");
            Texture2D ActiveButtonChangeName = Content.Load<Texture2D>("ActiveButtonChangeName");
            Texture2D ActiveButtonOptions = Content.Load<Texture2D>("ActiveButtonOptions");
            Texture2D ActiveButtonResolution = Content.Load<Texture2D>("ActiveButtonResolution");
            Texture2D ActiveButton500x500 = Content.Load<Texture2D>("ActiveButton500x500");
            Texture2D ActiveButton650x650 = Content.Load<Texture2D>("ActiveButton650x650");
            Texture2D ActiveButton800x800 = Content.Load<Texture2D>("ActiveButton800x800");
            Texture2D ActiveButton950x950 = Content.Load<Texture2D>("ActiveButton950x950");
            Texture2D ActiveButtonSubmitScore = Content.Load<Texture2D>("ActiveButtonSubmitScore");
            Texture2D ActiveButtonHighscores = Content.Load<Texture2D>("ActiveButtonHighscores");
            Texture2D PassiveButtonContinue = Content.Load<Texture2D>("PassiveButtonContinue");
            Texture2D PassiveButtonBackToMenu = Content.Load<Texture2D>("PassiveButtonBackToMenu");
            Texture2D PassiveButtonTitan = Content.Load<Texture2D>("PassiveButtonTitan");
            Texture2D PassiveButtonNormal = Content.Load<Texture2D>("PassiveButtonNormal");
            Texture2D PassiveButtonRisky = Content.Load<Texture2D>("PassiveButtonRisky");
            Texture2D PassiveButtonStart = Content.Load<Texture2D>("PassiveButtonStart");
            Texture2D PassiveButtonBack = Content.Load<Texture2D>("PassiveButtonBack");
            Texture2D PassiveButtonQuitGame = Content.Load<Texture2D>("PassiveButtonQuitGame");
            Texture2D PassiveButtonResetScore = Content.Load<Texture2D>("PassiveButtonResetScore");
            Texture2D PassiveButtonTutorial = Content.Load<Texture2D>("PassiveButtonTutorial");
            Texture2D PassiveButtonChangeName = Content.Load<Texture2D>("PassiveButtonChangeName");
            Texture2D PassiveButtonOptions = Content.Load<Texture2D>("PassiveButtonOptions");
            Texture2D PassiveButtonResolution = Content.Load<Texture2D>("PassiveButtonResolution");
            Texture2D PassiveButton500x500 = Content.Load<Texture2D>("PassiveButton500x500");
            Texture2D PassiveButton650x650 = Content.Load<Texture2D>("PassiveButton650x650");
            Texture2D PassiveButton800x800 = Content.Load<Texture2D>("PassiveButton800x800");
            Texture2D PassiveButton950x950 = Content.Load<Texture2D>("PassiveButton950x950");
            Texture2D PassiveButtonSubmitScore = Content.Load<Texture2D>("PassiveButtonSubmitScore");
            Texture2D PassiveButtonHighscores = Content.Load<Texture2D>("PassiveButtonHighscores");

            CommonFunctions.ActiveButtonContinue = ActiveButtonContinue;
            CommonFunctions.ActiveButonBackToMenu = ActiveButtonBackToMenu;
            CommonFunctions.ActiveButtonStart = ActiveButtonStart;
            CommonFunctions.ActiveButtonBack = ActiveButtonBack;
            CommonFunctions.ActiveButtonQuitGame = ActiveButtonQuitGame;
            CommonFunctions.ActiveButtonTitan = ActiveButtonTitan;
            CommonFunctions.ActiveButtonNormal = ActiveButtonNormal;
            CommonFunctions.ActiveButtonRisky = ActiveButtonRisky;
            CommonFunctions.ActiveButtonResetScore = ActiveButtonResetScore;
            CommonFunctions.ActiveButtonTutorial = ActiveButtonTutorial;
            CommonFunctions.ActiveButtonChangeName = ActiveButtonChangeName;
            CommonFunctions.ActiveButtonOptions = ActiveButtonOptions;
            CommonFunctions.ActiveButtonResolution = ActiveButtonResolution;
            CommonFunctions.ActiveButton500x500 = ActiveButton500x500;
            CommonFunctions.ActiveButton650x650 = ActiveButton650x650;
            CommonFunctions.ActiveButton800x800 = ActiveButton800x800;
            CommonFunctions.ActiveButton950x950 = ActiveButton950x950;
            CommonFunctions.ActiveButtonSubmitScore = ActiveButtonSubmitScore;
            CommonFunctions.ActiveButtonHighscores = ActiveButtonHighscores;
            CommonFunctions.PassiveButtonContinue = PassiveButtonContinue;
            CommonFunctions.PassiveButtonBackToMenu = PassiveButtonBackToMenu;
            CommonFunctions.PassiveButtonStart = PassiveButtonStart;
            CommonFunctions.PassiveButtonBack = PassiveButtonBack;
            CommonFunctions.PassiveButtonQuitGame = PassiveButtonQuitGame;
            CommonFunctions.PassiveButtonTitan = PassiveButtonTitan;
            CommonFunctions.PassiveButtonNormal = PassiveButtonNormal;
            CommonFunctions.PassiveButtonRisky = PassiveButtonRisky;
            CommonFunctions.PassiveButtonResetScore = PassiveButtonResetScore;
            CommonFunctions.PassiveButtonTutorial = PassiveButtonTutorial;
            CommonFunctions.PassiveButtonChangeName = PassiveButtonChangeName;
            CommonFunctions.PassiveButtonOptions = PassiveButtonOptions;
            CommonFunctions.PassiveButtonResolution = PassiveButtonResolution;
            CommonFunctions.PassiveButton500x500 = PassiveButton500x500;
            CommonFunctions.PassiveButton650x650 = PassiveButton650x650;
            CommonFunctions.PassiveButton800x800 = PassiveButton800x800;
            CommonFunctions.PassiveButton950x950 = PassiveButton950x950;
            CommonFunctions.PassiveButtonSubmitScore = PassiveButtonSubmitScore;
            CommonFunctions.PassiveButtonHighscores = PassiveButtonHighscores;
            #endregion

            CommonFunctions.font = font;


            //load objects
            MeContr = new MenuController(PointerTexture);
            ShipController = new SpaceshipController(StandartSpaceshipSkin,StandartTitanSkin, StandartBulletSkin, MeContr);
            TerraContr = new TerrainController(StandartTerrainSkin, StandartAmmoDropSkin, StandartScoreDropSkin, StandartTerrainBrokenLeftSkin, StandartTerrainBrokenRightSkin);
            UfoContr = new UfoController(StandartUfoSkin, StandartUfoLootAmmoSkin, StandartUfoLootScoreSkin, StandartUfoLootBombSkin);
            UIContr = new UIController(StandartUISkin, new Size(Convert.ToInt32(85f * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(68f * CommonFunctions.aspectRatioMultiplierY)));
            mySQLContr = new MySQLController();
            BgContr = new BackgroundController(Background1, Background1);
            GSContr = new GameStartController(ShipController, MeContr, mySQLContr, Background1, this);
            teInContr = new TextInputController(MeContr);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            CommonFunctions.Input = Keyboard.GetState();
            CommonFunctions.mState = Mouse.GetState();

            if (!GSContr.gameStarted)
                GSContr.Update();

            if (GSContr.gameStarted)
            {
                BgContr.Update();
                ShipController.Update();
                TerraContr.Update();
                UfoContr.Update();
                UIContr.Update();
            }
            
            base.Update(gameTime);
            
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);
            

            spriteBatch.Begin();

            if(!GSContr.gameStarted)
                GSContr.Draw(spriteBatch);

            else if (GSContr.gameStarted)
            {
                BgContr.Draw(spriteBatch);
                TerraContr.Draw(spriteBatch);
                UfoContr.Draw(spriteBatch);
                ShipController.Draw(spriteBatch);
                UIContr.Draw(spriteBatch);
            }

            spriteBatch.End();


            base.Draw(gameTime);

        }

        public void Quit()
        {
            Exit();
        }

        public void setResolution(int xValue, int yValue)
        {

            this.graphics.PreferredBackBufferHeight = xValue;
            this.graphics.PreferredBackBufferWidth = yValue;
            borders = new System.Drawing.Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            CommonFunctions.borders = borders;
            CommonFunctions.aspectRatioMultiplierX = borders.Right / 650f;
            CommonFunctions.aspectRatioMultiplierY = borders.Bottom / 650f;
            graphics.ApplyChanges();

        }

    }
}
