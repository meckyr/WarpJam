using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace WarpJam.Tools
{
    class Background : GameObject2D
    {
        

        private const float bg_speed = 5.0f;
        private const float star_speed1 = 2.0f;
        private const float star_speed2 = 1.0f;
        private GameSprite bg1, bg2;
        private GameAnimatedSprite star1, star2, star3, star4, star5;
        private GameAnimatedSprite star6, star7, star8, star9, star10;

        public override void Initialize()
        {
            bg1 = new GameSprite("level\\background1");
            AddChild(bg1);

            bg2 = new GameSprite("level\\background2");
            bg2.Translate(800, 0);
            AddChild(bg2);

            InitiateStar();

            base.Initialize();
        }

        public void InitiateStar()
        {
            star1 = new GameAnimatedSprite("menu\\star", 10, 10, new Point(55, 58));
            star1.Color = Color.Cyan;
            star1.Translate(220, 200);
            star1.PlayAnimation(true);
            AddChild(star1);

            star2 = new GameAnimatedSprite("menu\\star", 10, 105, new Point(55, 58));
            star2.Color = Color.Purple;
            star2.Scale(1.2f, 1.2f);
            star2.Translate(720, 50);
            star2.PlayAnimation(true);
            AddChild(star2);

            star3 = new GameAnimatedSprite("menu\\star", 10, 80, new Point(55, 58));
            star3.Color = Color.Red;
            star3.Translate(10, 340);
            star3.PlayAnimation(true);
            AddChild(star3);

            star4 = new GameAnimatedSprite("menu\\star", 10, 65, new Point(55, 58));
            star4.Color = Color.Yellow;
            star4.Translate(500, 500);
            star4.PlayAnimation(true);
            AddChild(star4);

            star5 = new GameAnimatedSprite("menu\\star", 10, 40, new Point(55, 58));
            star5.Color = Color.Orange;
            star5.Scale(0.5f, 0.5f);
            star5.Translate(420, 200);
            star5.PlayAnimation(true);
            AddChild(star5);

            star6 = new GameAnimatedSprite("menu\\star", 10, 10, new Point(55, 58));
            star6.Color = Color.Chocolate;
            star6.Translate(800, 420);
            star6.PlayAnimation(true);
            AddChild(star6);

            star7 = new GameAnimatedSprite("menu\\star", 10, 105, new Point(55, 58));
            star7.Color = Color.White;
            star7.Scale(1.5f, 1.5f);
            star7.Translate(900, 85);
            star7.PlayAnimation(true);
            AddChild(star7);

            star8 = new GameAnimatedSprite("menu\\star", 10, 80, new Point(55, 58));
            star8.Color = Color.Blue;
            star8.Translate(1200, 300);
            star8.PlayAnimation(true);
            AddChild(star8);

            star9 = new GameAnimatedSprite("menu\\star", 10, 65, new Point(55, 58));
            star9.Color = Color.Gold;
            star9.Translate(1450, 150);
            star9.PlayAnimation(true);
            AddChild(star9);

            star10 = new GameAnimatedSprite("menu\\star", 10, 40, new Point(55, 58));
            star10.Color = Color.Pink;
            star10.Scale(0.5f, 0.5f);
            star10.Translate(1100, 90);
            star10.PlayAnimation(true);
            AddChild(star10);
        }

        public override void Update(RenderContext renderContext)
        {
            // pastiin frame-rate independent
            // background
            var objectSpeed = renderContext.GameSpeed * bg_speed;
            objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            var objectPosX = bg1.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            bg1.Translate(objectPosX, 0);

            objectPosX = bg2.LocalPosition.X - objectSpeed;
            if (objectPosX < -800)
                objectPosX += 1600;

            bg2.Translate(objectPosX, 0);

            // star1
            //objectSpeed = renderContext.GameSpeed * star_speed1;
            //objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            objectPosX = star1.LocalPosition.X - objectSpeed;
            if (objectPosX < -400)
                objectPosX += 1600;

            star1.Translate(objectPosX, star1.LocalPosition.Y);

            objectPosX = star2.LocalPosition.X - objectSpeed;
            if (objectPosX < -400)
                objectPosX += 1600;

            star2.Translate(objectPosX, star2.LocalPosition.Y);

            objectPosX = star3.LocalPosition.X - objectSpeed;
            if (objectPosX < -400)
                objectPosX += 1600;

            star3.Translate(objectPosX, star3.LocalPosition.Y);

            // star2
            //objectSpeed = renderContext.GameSpeed * star_speed2;
            //objectSpeed *= (float)renderContext.GameTime.ElapsedGameTime.TotalSeconds;

            objectPosX = star4.LocalPosition.X - objectSpeed;
            if (objectPosX < -400)
                objectPosX += 1600;

            star4.Translate(objectPosX, star4.LocalPosition.Y);

            objectPosX = star5.LocalPosition.X - objectSpeed;
            if (objectPosX < -400)
                objectPosX += 1600;

            star5.Translate(objectPosX, star5.LocalPosition.Y);

            objectPosX = star6.LocalPosition.X - objectSpeed;
            if (objectPosX < -400)
                objectPosX += 1600;

            star6.Translate(objectPosX, star6.LocalPosition.Y);

            objectPosX = star7.LocalPosition.X - objectSpeed;
            if (objectPosX < -400)
                objectPosX += 1600;

            star7.Translate(objectPosX, star7.LocalPosition.Y);

            objectPosX = star8.LocalPosition.X - objectSpeed;
            if (objectPosX < -400)
                objectPosX += 1600;

            star8.Translate(objectPosX, star8.LocalPosition.Y);

            objectPosX = star9.LocalPosition.X - objectSpeed;
            if (objectPosX < -400)
                objectPosX += 1600;

            star9.Translate(objectPosX, star9.LocalPosition.Y);

            objectPosX = star10.LocalPosition.X - objectSpeed;
            if (objectPosX < -400)
                objectPosX += 1600;

            star10.Translate(objectPosX, star10.LocalPosition.Y);

            base.Update(renderContext);
        }
    }
}
