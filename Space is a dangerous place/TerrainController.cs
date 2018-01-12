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
        
        public Texture2D TerrainSkin { get; private set; }
        public Texture2D AmmoDropSkin { get; private set; }
        public Texture2D ScoreDropSkin { get; private set; }
        public Texture2D TerrainBrokenLeftSkin { get; private set; }
        public Texture2D TerrainBrokenRightSkin { get; private set; }

        System.Drawing.Rectangle triggerRectangleLeft;
        System.Drawing.Rectangle triggerRectangleRight;

        Terrain newTerrain;

        bool warningLeft;
        bool warningRight;
        

        public TerrainController(Texture2D terrainskin, Texture2D ammoDropSkin, Texture2D scoreDropSkin, Texture2D terrainBrokenLeftSkin, Texture2D terrainBrokenRightSkin)
        {

            TerrainSkin = terrainskin;
            AmmoDropSkin = ammoDropSkin;
            ScoreDropSkin = scoreDropSkin;
            TerrainBrokenLeftSkin = terrainBrokenLeftSkin;
            TerrainBrokenRightSkin = terrainBrokenRightSkin;
            
            triggerRectangleLeft = new System.Drawing.Rectangle(CommonFunctions.borders.Left, -13, 10, 10);
            triggerRectangleRight = new System.Drawing.Rectangle(CommonFunctions.borders.Right - 10, -13, 10, 10);

            CommonFunctions.currentTerrainController = this;

        }

        public void StartRoutine()
        {

            CommonFunctions.terrainSpawning = true;
            newTerrain = new Terrain(TerrainSkin, new Vector2(CommonFunctions.borders.Left, CommonFunctions.borders.Top - 200 * CommonFunctions.aspectRatioMultiplierY), new Size(Convert.ToInt32(200 * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(200 * CommonFunctions.aspectRatioMultiplierY)), AmmoDropSkin, ScoreDropSkin, TerrainBrokenLeftSkin, TerrainBrokenRightSkin);
            CommonFunctions.ICollidableList.Add(newTerrain);
            newTerrain = new Terrain(TerrainSkin, new Vector2(CommonFunctions.borders.Right - 200 * CommonFunctions.aspectRatioMultiplierX, CommonFunctions.borders.Top - 250 * CommonFunctions.aspectRatioMultiplierY), new Size(Convert.ToInt32(200 * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(250 * CommonFunctions.aspectRatioMultiplierY)), AmmoDropSkin, ScoreDropSkin, TerrainBrokenLeftSkin, TerrainBrokenRightSkin);
            CommonFunctions.ICollidableList.Add(newTerrain);

        }

        public bool CheckUntriggered(System.Drawing.Rectangle triggerRectangle)
        {
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
                XrandomSize = CommonFunctions.generalRandom.Next(Convert.ToInt32(200 * CommonFunctions.aspectRatioMultiplierX),Convert.ToInt32( 385 * CommonFunctions.aspectRatioMultiplierX));
            else
                XrandomSize = CommonFunctions.generalRandom.Next(Convert.ToInt32(150 * CommonFunctions.aspectRatioMultiplierX),Convert.ToInt32( 250 * CommonFunctions.aspectRatioMultiplierX));

            YrandomSize = CommonFunctions.generalRandom.Next(Convert.ToInt32(220 * CommonFunctions.aspectRatioMultiplierY), Convert.ToInt32(300 * CommonFunctions.aspectRatioMultiplierY));

            return new Size(XrandomSize, YrandomSize);

        }

        public void CreateTerrain(bool left)
        {

            Size terrainSize;
            Vector2 terrainPosition;

            List<Terrain> temporaryTerrainList = new List<Terrain>();
            foreach (Terrain terrain in CommonFunctions.ICollidableList.OfType<Terrain>().ToList())
                temporaryTerrainList.Add(terrain);

            if (left == true)
            {
                terrainSize = EstablishTerrainSize(warningLeft);

                ICollidable lastTerrain = temporaryTerrainList[temporaryTerrainList.Count - 1];
                if (lastTerrain.PositionForRectangle.X != CommonFunctions.borders.Left) //überprüfen, ob lezter listeneintrag ein linkes Terrain war, sonst wird der vorletzte Listeneintrag genommen
                    lastTerrain = temporaryTerrainList[temporaryTerrainList.Count - 2];
                if (lastTerrain.PositionForRectangle.X != CommonFunctions.borders.Left)
                    lastTerrain = temporaryTerrainList[temporaryTerrainList.Count - 3];

                if (terrainSize.Width > lastTerrain.ObjectSize.Width - 20 * CommonFunctions.aspectRatioMultiplierX && terrainSize.Width < lastTerrain.ObjectSize.Width + 20 * CommonFunctions.aspectRatioMultiplierX)
                    terrainSize = new Size(lastTerrain.ObjectSize.Width, terrainSize.Height);

                if (terrainSize.Width > 250 * CommonFunctions.aspectRatioMultiplierX)
                    warningLeft = true;
                else
                    warningLeft = false;

                terrainPosition = new Vector2(CommonFunctions.borders.Left, lastTerrain.PositionForRectangle.Y - terrainSize.Height);
            }

            else
            {
                terrainSize = EstablishTerrainSize(warningRight);

                ICollidable lastTerrain = temporaryTerrainList[temporaryTerrainList.Count - 1];
                if (lastTerrain.PositionForRectangle.X == CommonFunctions.borders.Left) //überprüfen, ob lezter listeneintrag ein rechtes Terrain war, sonst wird der vorletzte Listeneintrag genommen
                    lastTerrain = temporaryTerrainList[temporaryTerrainList.Count - 2];
                if (lastTerrain.PositionForRectangle.X == CommonFunctions.borders.Left)
                    lastTerrain = temporaryTerrainList[temporaryTerrainList.Count - 3];

                if (terrainSize.Width > lastTerrain.ObjectSize.Width - 20 * CommonFunctions.aspectRatioMultiplierX && terrainSize.Width < lastTerrain.ObjectSize.Width + 20 * CommonFunctions.aspectRatioMultiplierX)
                    terrainSize = new Size(lastTerrain.ObjectSize.Width, terrainSize.Height);

                if (terrainSize.Width > 250f * CommonFunctions.aspectRatioMultiplierX)
                    warningRight = true;
                else
                    warningRight = false;

                terrainPosition = new Vector2(CommonFunctions.borders.Right - terrainSize.Width, lastTerrain.PositionForRectangle.Y - terrainSize.Height);
            }

            newTerrain = new Terrain(TerrainSkin, terrainPosition, terrainSize, AmmoDropSkin, ScoreDropSkin, TerrainBrokenLeftSkin, TerrainBrokenRightSkin);
            CommonFunctions.ICollidableList.Add(newTerrain);

        }

        public void Update()
        {
            
            if (CommonFunctions.terrainSpawning)
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
