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
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;

namespace WarpJam
{
    class MainLevel : GameScene
    {
        private GameSprite bg;
        private Background backdrop;
        private Pesawat pesawat;
        private GameButton fire;
        private SpriteFont text;
        private SoundEffect gunfire;

        public MainLevel()
            : base("MainLevel")
        {
        }

        public override void Initialize()
        {
            // set game speed
            SceneManager.RenderContext.GameSpeed = 100;

            bg = new GameSprite("level\\background");
            bg.Translate(-400, 0);
            AddSceneObject(bg);

            backdrop = new Background();
            AddHUDObject(backdrop);

            fire = new GameButton("level\\invisbutton", false);
            fire.CanDraw = false;
            fire.Translate(400, 0);
            fire.OnClick += () =>
            {
                gunfire.Play();
            };
            AddSceneObject(fire);

            pesawat = new Pesawat();
            AddSceneObject(pesawat);

            base.Initialize();
        }

        public override void LoadContent(ContentManager contentmanager)
        {
            base.LoadContent(contentmanager);

            gunfire = contentmanager.Load<SoundEffect>("sfx\\gunfire");
            text = contentmanager.Load<SpriteFont>("font\\font");
        }

        public override void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            base.Update(rendercontext, contentmanager);
        }

        public override void Draw(RenderContext rendercontext)
        {
            base.Draw(rendercontext);
            rendercontext.SpriteBatch.DrawString(text, "Time: " + MediaPlayer.PlayPosition.TotalSeconds, new Vector2(470, 85), Color.White);
        }

        public override void ResetScene()
        {
            pesawat.Reset();
        }
    }
}
