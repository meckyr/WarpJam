using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Devices;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Windows;
using Microsoft.Xna.Framework.Graphics;
using ProjectMercury.Renderers;

namespace WarpJam.Tools
{
    class SceneManager
    {
        public static List<GameScene> GameScenes { get; private set; }
        public static GameScene ActiveScene { get; private set; }
        public static RenderContext RenderContext { get; private set; }
        public static GameTime gameTime;

        // kumpulan soundeffect
        public static SoundEffect push { get; private set; }
        
        // kumpulan music
        public static Song mainmusic;
        public static Song levelmusic;

        public static bool IsMenuInitialized = false;
        public static bool IsMusicPlaying = false;

        public static VibrateController Vibrator = VibrateController.Default;

        static SceneManager()
        {
            GameScenes = new List<GameScene>();
            RenderContext = new RenderContext();
        }

        public static void AddGameScene(GameScene gamescene)
        {
            if (!GameScenes.Contains(gamescene))
                GameScenes.Add(gamescene);
        }

        public static void RemoveGameScene(GameScene gamescene)
        {
            GameScenes.Remove(gamescene);

            if (ActiveScene == gamescene)
                ActiveScene = null;
        }

        public static bool SetActiveScene(string name)
        {
            var chosenscene = GameScenes.FirstOrDefault(scene =>
                scene.SceneName.Equals(name));

            if (chosenscene != null)
                ActiveScene = chosenscene;

            return chosenscene != null;
        }

        public static void Initialize()
        {
            GameScenes.ForEach(scene => scene.Initialize());
        }

        public static void PlaySong(int songnumber)
        {
            if (IsMusicPlaying)
            {
                if (songnumber == 1)
                    MediaPlayer.Play(mainmusic);
                else if (songnumber == 2)
                    MediaPlayer.Play(levelmusic);
            }
            else
            {
                if (MediaPlayer.State == MediaState.Playing)
                {
                    MessageBoxResult Choice;
                    Choice = MessageBox.Show("Media is currently playing music, do you want to stop it?", "Background Music", MessageBoxButton.OKCancel);

                    if (Choice == MessageBoxResult.OK)
                    {
                        if (songnumber == 1)
                            MediaPlayer.Play(mainmusic);
                        else
                            MediaPlayer.Play(levelmusic);

                        MediaPlayer.IsRepeating = true;
                        IsMusicPlaying = true;
                    }

                }
                else
                {
                    if (songnumber == 1)
                        MediaPlayer.Play(mainmusic);
                    else
                        MediaPlayer.Play(levelmusic);

                    MediaPlayer.IsRepeating = true;
                    IsMusicPlaying = true;
                }
            }
        }

        public static void LoadContent(ContentManager contentmanager)
        {
            push = contentmanager.Load<SoundEffect>("sfx\\button");
            mainmusic = contentmanager.Load<Song>("song\\mainmusic");
            levelmusic = contentmanager.Load<Song>("song\\levelmusic");

            // mainkan musik
            PlaySong(1);

            GameScenes.ForEach(scene => scene.LoadContent(contentmanager));
            GameScenes.ForEach(scene => scene.LoadParticle(contentmanager, RenderContext.particleRenderer));
        }

        public static void Update(GameTime gametime, ContentManager contentmanager)
        {
            if (ActiveScene != null)
            {
                RenderContext.GameTime = gametime;
                ActiveScene.Update(RenderContext, contentmanager);

                // initialisasi scene menu
                if (ActiveScene.SceneName == "MainMenu" && !IsMenuInitialized)
                {
                    var chosenscene = GameScenes.FirstOrDefault(scene =>
                        scene.SceneName.Equals("MainMenu"));
                    chosenscene.Update(RenderContext, contentmanager);

                    chosenscene = GameScenes.FirstOrDefault(scene =>
                        scene.SceneName.Equals("MainLevel"));
                    chosenscene.Update(RenderContext, contentmanager);

                    IsMenuInitialized = true;
                }
            }
            RenderContext.TouchPanelState = TouchPanel.GetState();
        }

        public static void Draw()
        {
            if (ActiveScene != null)
            {
                //draw HUD
                RenderContext.SpriteBatch.Begin();
                ActiveScene.DrawHUD(RenderContext);
                RenderContext.SpriteBatch.End();

                if (CameraManager.getInstance().camera.Focus == null)
                {
                    RenderContext.SpriteBatch.Begin();
                    ActiveScene.Draw(RenderContext);
                    RenderContext.SpriteBatch.End();
                }
                else
                {
                    RenderContext.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, CameraManager.getInstance().camera.Transform);
                    ActiveScene.Draw(RenderContext);
                    RenderContext.SpriteBatch.End();
                }

                ActiveScene.DrawParticle(RenderContext);
            }
        }

    }
}
