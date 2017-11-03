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

        Spaceship Spaceship1;

        TerrainController TerraContr;
        UfoController UfoContr;
        UIController UIContr;

        SpriteFont font;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferHeight = 650;
            this.graphics.PreferredBackBufferWidth = 650;
            borders = new System.Drawing.Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            CommonFunctions.borders = borders;

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
            Texture2D StandartTerrainBrokenLeftSkin = Content.Load<Texture2D>("geländezerstörtbildlinks");
            Texture2D StandartTerrainBrokenRightSkin = Content.Load<Texture2D>("geländezerstörtbildrechts");
            Texture2D StandartUfoSkin = Content.Load<Texture2D>("alienbild_t");
            Texture2D StandartUfoLootAmmoSkin = Content.Load<Texture2D>("alienlootmunition");
            Texture2D StandartUfoLootScoreSkin = Content.Load<Texture2D>("alienlootscore");
            Texture2D StandartUfoLootBombSkin = Content.Load<Texture2D>("alienlootbombe");
            Texture2D StandartScoreDropSkin = Content.Load<Texture2D>("scorebild");
            Texture2D StandartAmmoDropSkin = Content.Load<Texture2D>("munitionsbild");
            Texture2D StandartEndSreen = Content.Load<Texture2D>("endebild");
            Texture2D StandartUISkin = Content.Load<Texture2D>("UIBild_t");

            font = Content.Load<SpriteFont>("UIFont");
            

            //load objects
            Spaceship1 = new Spaceship(StandartSpaceshipSkin, new System.Drawing.Size(borders.Right * 07 / 130, borders.Bottom * 14 / 130), StandartBulletSkin); //  0.7 / 13 ,  1.4 / 13
            TerraContr = new TerrainController(StandartTerrainSkin, StandartAmmoDropSkin, StandartScoreDropSkin, StandartTerrainBrokenLeftSkin, StandartTerrainBrokenRightSkin);
            UfoContr = new UfoController(StandartUfoSkin, StandartUfoLootAmmoSkin, StandartUfoLootScoreSkin, StandartUfoLootBombSkin);
            UIContr = new UIController(StandartUISkin, font, new Size(borders.Right * 17 / 130, borders.Bottom * 136 / 1300), Spaceship1); // 85% der Ursprungsgröße
            
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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //todo: ? alles zu drawen und zu updaten, wegen codemetrix
            Spaceship1.Update();
            TerraContr.Update();
            UfoContr.Update();
            UIContr.Update();

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);
            

            spriteBatch.Begin();

            Spaceship1.Draw(spriteBatch);
            TerraContr.Draw(spriteBatch);
            UfoContr.Draw(spriteBatch);
            UIContr.Draw(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);

        }

    }
}
