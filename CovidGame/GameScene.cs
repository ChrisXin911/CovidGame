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
    public abstract class GameScene : DrawableGameComponent
    {
        public SpriteBatch spriteBatch;
        public Game1 g;
        public List<GameComponent> Components { get; set; }
        protected GameScene(Game game) : base(game)
        {

            this.g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            Components = new List<GameComponent>();
            hide();
        }
        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }
        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }
        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }


            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }

            }
            base.Update(gameTime);
        }
    }
}
