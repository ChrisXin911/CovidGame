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
    public class StartScene : GameScene
    {
        //game name
        private Texture2D gameName;
        //menu
        public MenuComponent Menu { get; set; }
        private SpriteBatch spriteBatch;
        private Game1 g;
        string[] menus = {"Start Game",
             "Help",
             "High Score",
             "About",
             "Quit"};
        public StartScene(Game game) : base(game)
        {
            
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            //load font .......from game
            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont highlightFont = g.Content.Load<SpriteFont>("fonts/highlightFont");
            Menu = new MenuComponent(game, spriteBatch, regularFont, highlightFont, menus);
            this.Components.Add(Menu);
            //add gameName
            gameName = g.Content.Load<Texture2D>("images/DefeatCovid19");

        }

        public override void Draw(GameTime gameTime)
        {
            //add gamename
            spriteBatch.Begin();

            spriteBatch.Draw(gameName, Vector2.Zero, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
