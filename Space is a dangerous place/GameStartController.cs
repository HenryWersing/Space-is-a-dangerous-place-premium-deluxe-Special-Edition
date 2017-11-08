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

        private KeyboardState Input;
        private SpaceshipController SpaceShipController;

        public bool gameStarted = false;


        public GameStartController(SpaceshipController spShipContr, SpriteFont font)
        {

            this.font = font;

            SpaceShipController = spShipContr;
            CommonFunctions.currentGameStartController = this;

        }

        public void Update()
        {

            Input = Keyboard.GetState();

            if (!gameStarted)
            {
                //spawnMode: 0=normal, 1=risky
                if (Input.IsKeyDown(Keys.N))
                {
                    SpaceShipController.SpawnShip(0);
                    gameStarted = true;
                }

                if (Input.IsKeyDown(Keys.R))
                {
                    SpaceShipController.SpawnShip(1); 
                    gameStarted = true;
                }

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            //todo: schaltflächen
            spriteBatch.DrawString(font, "[N]ormal mode: everything is normal\n[R]isky mode: score *2, speed *0.6, attackspeed *0.4", new Vector2(3, 3), Color.White, 0, new Vector2(0, 0), 0.7f, 0, 0);

        }

    }
}
