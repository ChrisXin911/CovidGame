using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CovidGame
{
    public class ScoreScene : GameScene
    {
        //private SpriteBatch spriteBatch;
        //private Game1 g;
        private string title;       
        private SpriteFont scoreFont;

        public ScoreScene(Game game) : base(game)
        {
          
            title = "TOP 5 HIGH SCORE ^_^";
            scoreFont = game.Content.Load<SpriteFont>("fonts/highlightFont");
}
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //add title
            Vector2 titlePosition = new Vector2(Shared.Stage.X / 2 - scoreFont.MeasureString(title).X / 2, 50);
            spriteBatch.DrawString(scoreFont,title,titlePosition,Color.White);

            //add top 5 high score
            int Top5 = Math.Min(5, Shared.HighScores.Count);

            List<NameScore> ScoreList = new List<NameScore>(Shared.HighScores);
            ScoreList.Sort((s1, s2) => s2.Score.CompareTo(s1.Score));
            

            for (int i = 0; i < Top5; i++)
            {
                string scorelist = $"{i+1}: {ScoreList[i].Name} <=====> {ScoreList[i].Score}";
                Vector2 scorePosition = titlePosition + new Vector2(60, 70+50*i);
                spriteBatch.DrawString(scoreFont, scorelist, scorePosition, Color.Red);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

    
    }
}
