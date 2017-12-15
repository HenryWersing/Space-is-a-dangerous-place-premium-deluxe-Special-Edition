using Microsoft.Xna.Framework;
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

        SpriteFont font;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferHeight = 850;
            this.graphics.PreferredBackBufferWidth = 850;
            borders = new System.Drawing.Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            CommonFunctions.borders = borders;
            CommonFunctions.aspectRatioMultiplierX = borders.Right / 650f;
            CommonFunctions.aspectRatioMultiplierY = borders.Bottom / 650f;

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
            Texture2D StandartEndSreen = Content.Load<Texture2D>("endebild");
            Texture2D StandartUISkin = Content.Load<Texture2D>("UIBild_t");
            Texture2D Background1 = Content.Load<Texture2D>("Hintergrund1");

            font = Content.Load<SpriteFont>("UIFont");
            

            //load objects
            ShipController = new SpaceshipController(StandartSpaceshipSkin, StandartBulletSkin);
            TerraContr = new TerrainController(StandartTerrainSkin, StandartAmmoDropSkin, StandartScoreDropSkin, StandartTerrainBrokenLeftSkin, StandartTerrainBrokenRightSkin);
            UfoContr = new UfoController(StandartUfoSkin, StandartUfoLootAmmoSkin, StandartUfoLootScoreSkin, StandartUfoLootBombSkin);
            UIContr = new UIController(StandartUISkin, font, new Size(borders.Right * 17 / 130, borders.Bottom * 136 / 1300)); //todo: aspectratio           // 85% der Ursprungsgröße 
            BgContr = new BackgroundController(Background1, Background1);
            GSContr = new GameStartController(ShipController, font);

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
            
            //todo: ? alles zu drawen und zu updaten, wegen codemetrix
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

            GSContr.Draw(spriteBatch);
            if (GSContr.gameStarted)
            {
                BgContr.Draw(spriteBatch);
                ShipController.Draw(spriteBatch);
                TerraContr.Draw(spriteBatch);
                UfoContr.Draw(spriteBatch);
                UIContr.Draw(spriteBatch);
            }

            spriteBatch.End();


            base.Draw(gameTime);

        }

    }
}
