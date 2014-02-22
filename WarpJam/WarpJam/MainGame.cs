using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Phone.Shell;
using WarpJam.Tools;
using System.Windows;
using FarseerPhysics;

namespace WarpJam
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);

            // Set full screen
            graphics.IsFullScreen = true;
            // Set orientation & size
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
            graphics.PreferMultiSampling = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;

            // Set farseer ratio (1 m = ?? px)
            ConvertUnits.SetDisplayUnitToSimUnitRatio(100f);

            CameraManager.prepareManager(new Camera2D(this));
            Components.Add(CameraManager.getInstance().camera);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            // Disable autolocking
            PhoneApplicationService phoneAppService = PhoneApplicationService.Current;
            phoneAppService.UserIdleDetectionMode = IdleDetectionMode.Disabled;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SceneManager.RenderContext.GraphicsDevice = graphics.GraphicsDevice;
            SceneManager.RenderContext.LineBatch = new LineBatch(graphics.GraphicsDevice);

            // Add semua scene yang ada
            SceneManager.AddGameScene(new MainMenu());
            SceneManager.AddGameScene(new MainLevel());

            // Set Scene pertama
            SceneManager.SetActiveScene("MainMenu");
            SceneManager.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            SceneManager.RenderContext.SpriteBatch = spriteBatch;
            SceneManager.LoadContent(Content);
            Extensions.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (SceneManager.ActiveScene.SceneName == "MainMenu")
                {
                    if (SceneManager.ActiveScene.BackPressed())
                    {
                        MessageBoxResult Choice;
                        Choice = MessageBox.Show("Are you sure?", "Exit Game", MessageBoxButton.OKCancel);

                        if (Choice == MessageBoxResult.OK)
                            this.Exit();
                    }
                }
                else if (SceneManager.ActiveScene.SceneName == "MainLevel")
                {
                    SceneManager.push.Play();
                    SceneManager.SetActiveScene("MainMenu");
                    SceneManager.ActiveScene.ResetScene();
                    SceneManager.PlaySong(1);
                }
            }

            // TODO: Add your update logic here
            SceneManager.Update(gameTime, Content);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            SceneManager.Draw();
            base.Draw(gameTime);
        }
    }
}
