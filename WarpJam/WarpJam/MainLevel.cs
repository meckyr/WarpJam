using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WarpJam.Tools;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace WarpJam
{
    class MainLevel : GameScene 
    {
        private GameSprite bg;
        private Texture2D visualizer;
        private GameAnimatedSprite hero;
        private SpriteFont text;

        // atribut visualizer
        private Color color = Color.White;
        private VisualizationData visdat = new VisualizationData();
        private int barWidth = 800/256;
        private int barHeight = 5;
        private int amount;

        public MainLevel()
            : base("MainLevel")
        {
            bg = new GameSprite("level\\background");
            AddSceneObject(bg);

            hero = new GameAnimatedSprite("menu\\hero", 8, 80, new Point(60,52));
            hero.Translate(400, 50);
            hero.PlayAnimation(true);
            AddSceneObject(hero);

            MediaPlayer.IsVisualizationEnabled = true;
        }

        private int GetAverage(Point Between, VisualizationData _visData)
        {

            int average = 0;
            for (int i = Between.X; i < Between.Y; i++)
            {
                average += Convert.ToInt32(_visData.Frequencies[i]);
            }
            int diff = Between.Y - Between.X + 1;
            return average / diff;
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);

            text = contentmanager.Load<SpriteFont>("font\\font");
            visualizer = new Texture2D(SceneManager.RenderContext.GraphicsDevice, 1, 1);
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            MediaPlayer.GetVisualizationData(visdat);
            amount = GetAverage(new Point(0, 256), visdat);

            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);

            rendercontext.SpriteBatch.DrawString(text, "Ngetes deh...", new Vector2(470, 85), Color.White);
            
            //behaviour visualizer
            if (MediaPlayer.State == MediaState.Playing)
            {
                for (int i = 0; i < 256; i++)
                {
                    color = Color.FromNonPremultiplied(Convert.ToInt32(visdat.Frequencies[amount] * 255), Convert.ToInt32(visdat.Frequencies[amount] * 255), Convert.ToInt32(visdat.Frequencies[amount] * 255), 255);
                    for (int j = 0; j < Convert.ToInt32(i * visdat.Samples[i]); j++)
                    {
                        if (Convert.ToInt32(i * visdat.Samples[i]) < 0)
                        {
                            rendercontext.SpriteBatch.Draw(visualizer, new Rectangle(i * barWidth, (480 / 2) + Convert.ToInt32(i * visdat.Samples[i]) - 150 - j * barHeight, barWidth, barHeight), Color.FromNonPremultiplied(255, 0, i, 200));
                            rendercontext.SpriteBatch.Draw(visualizer, new Rectangle(i * barWidth, (480 / 2) - Convert.ToInt32(i * visdat.Samples[i]) + 150 + j * barHeight, barWidth, barHeight), Color.FromNonPremultiplied(255, 0, i, 200));
                        }
                        else
                        {
                            rendercontext.SpriteBatch.Draw(visualizer, new Rectangle(i * barWidth, (480 / 2) - 150 - j * barHeight, barWidth, barHeight), Color.FromNonPremultiplied(255, 0, i, 200));
                            rendercontext.SpriteBatch.Draw(visualizer, new Rectangle(i * barWidth, (480 / 2) + 150 + j * barHeight, barWidth, barHeight), Color.FromNonPremultiplied(255, 0, i, 200));
                        }
                    }
                }
            }
        }

        public override void ResetScene()
        {
        }
    }
}
