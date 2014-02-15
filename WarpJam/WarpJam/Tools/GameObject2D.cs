using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace WarpJam.Tools
{
    class GameObject2D : IFocusable
    {
        private GameScene scene;

        public GameScene Scene
        {
            get
            {
                if (scene != null) return scene;
                if (Parent != null) return Parent.Scene;
                return null;
            }

            set { scene = value; }
        }

        private Rectangle? relativeBoundingRect;
        public Rectangle? BoundingRect { get; protected set; }

        protected Matrix WorldMatrix;

        public Vector2 PivotPoint { get; set; }

        public Vector2 LocalPosition { get; set; }
        public Vector2 WorldPosition { get; protected set; }

        public Vector2 LocalScale { get; set; }
        public Vector2 WorldScale { get; private set; }

        public float LocalRotation { get; set; }
        public float WorldRotation { get; private set; }

        public GameObject2D Parent { get; set; }
        public List<GameObject2D> Children { get; private set; }

        public bool CanDraw { get; set; }

        public GameObject2D()
        {
            LocalScale = WorldScale = Vector2.One;
            Children = new List<GameObject2D>();
            CanDraw = true;
        }

        public void AddChild(GameObject2D child)
        {
            if (!Children.Contains(child))
            {
                child.Scene = Scene;
                child.Parent = this;
                Children.Add(child);
            }
        }

        public void RemoveChild(GameObject2D child)
        {
            if (Children.Remove(child))
            {
                child.Scene = null;
                child.Parent = null;
            }
        }

        public void Rotate(float rotation)
        {
            LocalRotation = rotation;
        }

        public void Translate(float posX, float posY)
        {
            Translate(new Vector2(posX, posY));
        }

        public void Translate(Vector2 position)
        {
            LocalPosition = position;
        }

        public void Scale(float scaleX, float scaleY)
        {
            Scale(new Vector2(scaleX, scaleY));
        }

        public void Scale(Vector2 scale)
        {
            LocalScale = scale;
        }

        public void RemoveBoundingRect()
        {
            relativeBoundingRect = null;
            BoundingRect = null;
        }

        public void CreateBoundingRect(int width, int height, Vector2 offset, bool isorigincenter)
        {
            if (isorigincenter)
                relativeBoundingRect = new Rectangle((-width / 2) + (int)offset.X, (-height / 2) + (int)offset.Y, width, height);
            else
                relativeBoundingRect = new Rectangle(0 + (int)offset.X, 0 + (int)offset.Y, width, height);

            BoundingRect = relativeBoundingRect;
        }

        public void CreateBoundingRect(int width, int height, bool isorigincenter)
        {
            CreateBoundingRect(width, height, Vector2.Zero, isorigincenter);
        }

        public bool HitTest(GameObject2D gameObj)
        {
            if (!gameObj.BoundingRect.HasValue) return false;
            if (BoundingRect.HasValue && BoundingRect.Value.Intersects(
                gameObj.BoundingRect.Value)) return true;

            return Children.FirstOrDefault(child => child.HitTest(gameObj)) != null;
        }

        public bool HitTest(Vector2 position, bool includechildren)
        {
            if (BoundingRect.HasValue && BoundingRect.Value.Contains(
                (int)position.X, (int)position.Y)) return true;

            if (includechildren)
                return Children.FirstOrDefault(child => child.HitTest(position, includechildren))
                    != null;

            return false;
        }

        public bool HitTest(Vector2 position)
        {
            return HitTest(position, true);
        }

        public virtual void Initialize()
        {
            Children.ForEach(child => child.Initialize());
        }

        public virtual void LoadContent(ContentManager contentManager)
        {
            Children.ForEach(child => child.LoadContent(contentManager));
        }

        public virtual void Update(RenderContext renderContext)
        {
            WorldMatrix = Matrix.CreateTranslation(new Vector3(-PivotPoint, 0))
                * Matrix.CreateScale(new Vector3(LocalScale, 1))
                * Matrix.CreateRotationZ(MathHelper.ToRadians(LocalRotation))
                * Matrix.CreateTranslation(new Vector3(LocalPosition, 0));

            if (Parent != null)
            {
                WorldMatrix = Matrix.Multiply(WorldMatrix, Matrix.CreateTranslation(new Vector3(Parent.PivotPoint, 0)));
                WorldMatrix = Matrix.Multiply(WorldMatrix, Parent.WorldMatrix);
            }

            Vector3 pos, scale;
            Quaternion rot;
            if (!WorldMatrix.Decompose(out scale, out rot, out pos))
                Debug.WriteLine("Object2D Decompose World Matrix FAILED!");

            var direction = Vector2.Transform(Vector2.UnitX, rot);
            WorldRotation = (float)Math.Atan2(direction.Y, direction.X);
            WorldRotation = float.IsNaN(WorldRotation) ? 0 : MathHelper.ToDegrees(WorldRotation);

            WorldPosition = new Vector2(pos.X, pos.Y);
            WorldScale = new Vector2(scale.X, scale.Y);

            Children.ForEach(child => child.Update(renderContext));

            if (relativeBoundingRect.HasValue)
                BoundingRect = relativeBoundingRect.Value.Update(WorldMatrix);
        }

        public virtual void Draw(RenderContext renderContext)
        {
            if (CanDraw)
                Children.ForEach(child => { if (child.CanDraw) child.Draw(renderContext); });

            // Draw BoundingRect (optional)
            //if (CanDraw && BoundingRect.HasValue)
            //{
            //    BoundingRect.Value.Draw(renderContext, Color.Red);
            //}
        }

        public Vector2 Position
        {
            get
            {
                return LocalPosition;
            }
            set
            {
                LocalPosition = value;
            }
        }
    }
}
