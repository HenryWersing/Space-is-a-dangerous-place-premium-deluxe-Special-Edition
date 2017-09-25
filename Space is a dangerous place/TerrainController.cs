using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Xna.Framework.Input;

namespace Space_is_a_dangerous_place
{
    class TerrainController
    {

        private KeyboardState Input;

        public Texture2D TerrainSkin { get; private set; }
        public Texture2D AmmoDropSkin { get; private set; }
        public Texture2D ScoreDropSkin { get; private set; }
        public Texture2D TerrainBrokenLeftSkin { get; private set; }
        public Texture2D TerrainBrokenRightSkin { get; private set; }

        System.Drawing.Rectangle borders;
        System.Drawing.Rectangle triggerRectangleLeft;
        System.Drawing.Rectangle triggerRectangleRight;

        Terrain newTerrain;

        bool started;
        bool warningLeft;
        bool warningRight;

        Random rdm;


        public TerrainController(Texture2D terrainskin, Texture2D ammoDropSkin, Texture2D scoreDropSkin, Texture2D terrainBrokenLeftSkin, Texture2D terrainBrokenRightSkin)
        {

            borders = CommonFunctions.borders;

            TerrainSkin = terrainskin;
            AmmoDropSkin = ammoDropSkin;
            ScoreDropSkin = scoreDropSkin;
            TerrainBrokenLeftSkin = terrainBrokenLeftSkin;
            TerrainBrokenRightSkin = terrainBrokenRightSkin;

            rdm = new Random();

            triggerRectangleLeft = new System.Drawing.Rectangle(borders.Left, -13, 10, 10);
            triggerRectangleRight = new System.Drawing.Rectangle(borders.Right - 10, -13, 10, 10);

        }

        public void StartRoutine()
        {

            newTerrain = new Terrain(TerrainSkin, new Vector2(borders.Left, borders.Top - borders.Bottom * 4 / 13), new Size(borders.Right * 4 / 13, borders.Bottom * 4 / 13), AmmoDropSkin, ScoreDropSkin, TerrainBrokenLeftSkin, TerrainBrokenRightSkin);
            CommonFunctions.ICollidableList.Add(newTerrain);
            newTerrain = new Terrain(TerrainSkin, new Vector2(borders.Right - borders.Right * 4 / 13, borders.Top - borders.Bottom * 5 / 13), new Size(borders.Right * 4 / 13, borders.Bottom * 5 / 13), AmmoDropSkin, ScoreDropSkin, TerrainBrokenLeftSkin, TerrainBrokenRightSkin);
            CommonFunctions.ICollidableList.Add(newTerrain);

        }

        public bool CheckUntriggered(System.Drawing.Rectangle triggerRectangle)
        {
            //todo: bug fixen, wo auf einer seite kein terrain mehr kommt
            //vieleicht wird der gefixt, wenn bullets zerstört werden
            bool result = false;
            int counter = 0;

            foreach (Terrain terrain in CommonFunctions.ICollidableList.OfType<Terrain>().ToList())
                if (triggerRectangle.IntersectsWith(CommonFunctions.SizeToRectangle(terrain.ObjectSize, terrain.PositionForRectangle)))
                    counter++;

            if (counter == 0)
                result = true;

            return result;

        }

        public Size EstablishTerrainSize(bool warning)
        {

            int XrandomSize;
            int YrandomSize;

            if (!warning)
                XrandomSize = rdm.Next(borders.Right * 4 / 13, borders.Right * 77 / 130); //  7.7 / 13
            else
                XrandomSize = rdm.Next(borders.Right * 3 / 13, borders.Right * 5 / 13);
            
            YrandomSize = rdm.Next(borders.Right * 44 / 130, borders.Right * 6 / 13); //  4.4 / 13

            return new Size(XrandomSize, YrandomSize);

        }

        public void CreateTerrain(bool left)
        {

            Size terrainSize;
            Vector2 terrainPosition;

            List<Terrain> temporaryTerrainList = new List<Terrain>();
            foreach (Terrain terrain in CommonFunctions.ICollidableList.OfType<Terrain>().ToList())
                temporaryTerrainList.Add(terrain);
            //todo: fix bug, wenn der letzte und vorletzte eintrag nicht ein rechtes terrain ist,
            //kommen keine rechten terrains mehr. könnte mit desrtoyen der bullets behoben werden
            if (left == true)
            {
                terrainSize = EstablishTerrainSize(warningLeft);
                
                ICollidable lastTerrain = temporaryTerrainList[temporaryTerrainList.Count - 1];
                if (lastTerrain.PositionForRectangle.X != borders.Left) //überprüfen, ob lezter listeneintrag ein linkes Terrain war, sonst wird der vorletzte Listeneintrag genommen
                    lastTerrain = temporaryTerrainList[temporaryTerrainList.Count - 2];

                if (terrainSize.Width > lastTerrain.ObjectSize.Width - borders.Right * 04 / 130 && terrainSize.Width < lastTerrain.ObjectSize.Width + borders.Right * 04 / 130) //  0.4 / 13
                    terrainSize = new Size(lastTerrain.ObjectSize.Width, terrainSize.Height);

                if (terrainSize.Width > borders.Right * 5 / 13)
                    warningLeft = true;
                else
                    warningLeft = false;

                terrainPosition = new Vector2(borders.Left, lastTerrain.PositionForRectangle.Y - terrainSize.Height);
            }

            else
            {
                terrainSize = EstablishTerrainSize(warningRight);

                ICollidable lastTerrain = temporaryTerrainList[temporaryTerrainList.Count - 1];
                if (lastTerrain.PositionForRectangle.X == borders.Left) //überprüfen, ob lezter listeneintrag ein rechtes Terrain war, sonst wird der vorletzte Listeneintrag genommen
                    lastTerrain = temporaryTerrainList[temporaryTerrainList.Count - 2];

                if (terrainSize.Width > lastTerrain.ObjectSize.Width - borders.Right * 04 / 130 && terrainSize.Width < lastTerrain.ObjectSize.Width + borders.Right * 04 / 130) //  0.4 / 13
                    terrainSize = new Size(lastTerrain.ObjectSize.Width, terrainSize.Height);

                if (terrainSize.Width > borders.Right * 5 / 13)
                    warningRight = true;
                else
                    warningRight = false;

                terrainPosition = new Vector2(borders.Right - terrainSize.Width, lastTerrain.PositionForRectangle.Y - terrainSize.Height);
            }

            newTerrain = new Terrain(TerrainSkin, terrainPosition, terrainSize, AmmoDropSkin, ScoreDropSkin, TerrainBrokenLeftSkin, TerrainBrokenRightSkin);
            CommonFunctions.ICollidableList.Add(newTerrain);

        }

        public void Update()
        {

            Input = Keyboard.GetState();

            if (!started && Input.IsKeyDown(Keys.Enter))
            {
                started = true;
                StartRoutine();
            }

            if (started)
            {
                if (CheckUntriggered(triggerRectangleLeft))
                    CreateTerrain(true);

                if (CheckUntriggered(triggerRectangleRight))
                    CreateTerrain(false);
            }
            
            foreach (Terrain terrain in CommonFunctions.ICollidableList.OfType<Terrain>().ToList())
                terrain.Update();
            
            foreach (AmmoDrop ammoDrop in CommonFunctions.ICollidableList.OfType<AmmoDrop>().ToList())
                ammoDrop.Update();

            foreach (ScoreDrop scoreDrop in CommonFunctions.ICollidableList.OfType<ScoreDrop>().ToList())
                scoreDrop.Update();
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Terrain terrain in CommonFunctions.ICollidableList.OfType<Terrain>().ToList())
                terrain.Draw(spriteBatch);
            
            foreach (AmmoDrop ammoDrop in CommonFunctions.ICollidableList.OfType<AmmoDrop>().ToList())
                ammoDrop.Draw(spriteBatch);

            foreach (ScoreDrop scoreDrop in CommonFunctions.ICollidableList.OfType<ScoreDrop>().ToList())
                scoreDrop.Draw(spriteBatch);
            
        }

    }
}
