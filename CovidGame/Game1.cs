using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CovidGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private StartScene startScene;
        private HelpScene helpScene;
        private ActionScene actionScene;
        private AboutScene aboutScene;
        private ScoreScene scoreScene;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        private void hideAllScenes()
        {
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    GameScene gs = (GameScene)item;
                    gs.hide();
                }
            }
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //set the global value
            Shared.Stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //add start startScene
            startScene = new StartScene(this);
            this.Components.Add(startScene);
            startScene.show();
            //other scene
            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);
            //Action scene
            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);
            //about scene
            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);
            //score scene
            scoreScene = new ScoreScene(this);
            this.Components.Add(scoreScene);



            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            int selectIndex = 0;
            //get the keyboard state
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                selectIndex = startScene.Menu.selectIndex;
                if (selectIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    actionScene.show();
                }
                else if (selectIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.show();
                }
                else if (selectIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    scoreScene.show();
                }
                else if (selectIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    aboutScene.show();
                }
                else if (selectIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            if (helpScene.Enabled || actionScene.Enabled || aboutScene.Enabled ||scoreScene.Enabled )
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }

            }
            if (Shared.Status ==1)
            {
                hideAllScenes();
                startScene.show();
                Shared.Status = 0;
            }
           


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}