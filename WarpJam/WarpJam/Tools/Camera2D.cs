using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WarpJam.Tools
{
    /// <summary>
    /// Cara pakainya:
    /// di konstruktor game: camera2D.Focus = objek yang mau diikuti oleh kamera;
    /// Components.Add(camera2D);
    /// 
    /// di draw():
    /// spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera2D.Transform);
    /// </summary>
    public class Camera2D : GameComponent, ICamera2D
    {
        private Vector2 _position;
        protected float _viewportHeight;
        protected float _viewportWidth;

        public bool IsIgnoreY { get; set; }

        public Camera2D(Game game)
            : base(game)
        { }

        #region Properties

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public Vector2 ScreenCenter { get; protected set; }
        public Matrix Transform { get; set; }
        public IFocusable Focus { get { return focus; } set { focus = value; } }
        private IFocusable focus;
        public float MoveSpeed { get; set; }

        #endregion

        /// <summary>
        /// Called when the GameComponent needs to be initialized. 
        /// </summary>
        public override void Initialize()
        {
            _viewportWidth = Game.GraphicsDevice.Viewport.Width;
            _viewportHeight = Game.GraphicsDevice.Viewport.Height;
            _position = new Vector2();
            ScreenCenter = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            Scale = 1;
            MoveSpeed = 4f;
            IsIgnoreY = false;

            base.Initialize();
        }

        public void SetScreenCenter(float x, float y)
        {
            ScreenCenter = new Vector2(_viewportWidth / x, _viewportHeight / y);
        }

        public void ResetScreenCenter() 
        {
            ScreenCenter = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
        }

        public override void Update(GameTime gameTime)
        {
            if (Focus != null)
            {
                // Create the Transform used by any
                // spritebatch process
                Transform = Matrix.Identity *
                            Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                            Matrix.CreateRotationZ(Rotation) *
                            Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                            Matrix.CreateScale(new Vector3(Scale, Scale, Scale));

                Origin = ScreenCenter / Scale;

                // Move the Camera to the position that it needs to go
                var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

                _position.X += (Focus.Position.X - Position.X) * MoveSpeed * delta;

                if (!IsIgnoreY)
                    _position.Y += (Focus.Position.Y - Position.Y) * MoveSpeed * delta;

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// Determines whether the target is in view given the specified position.
        /// This can be used to increase performance by not drawing objects
        /// directly in the viewport
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="texture">The texture.</param>
        /// <returns>
        ///     <c>true</c> if [is in view] [the specified position]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInView(Vector2 position, Texture2D texture)
        {
            // If the object is not within the horizontal bounds of the screen

            if ((position.X + texture.Width) < (Position.X - Origin.X) || (position.X) > (Position.X + Origin.X))
                return false;

            // If the object is not within the vertical bounds of the screen
            if ((position.Y + texture.Height) < (Position.Y - Origin.Y) || (position.Y) > (Position.Y + Origin.Y))
                return false;

            // In View
            return true;
        }
    }
}
