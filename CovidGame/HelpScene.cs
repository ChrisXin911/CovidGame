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
    public class HelpScene : GameScene
    {
        //private SpriteBatch spriteBatch;
        //private Game1 g;
        private Texture2D tex;

        public HelpScene(Game game) : base(game)
        {
            //this.g = (Game1)game;
            //this.spriteBatch = g._spriteBatch;
            this.tex = g.Content.Load<Texture2D>("images/HelpScene");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero,Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    
    }
}
