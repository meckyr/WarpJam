using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace WarpJam.Tools
{
    class GameScene
    {
        public string SceneName { get; private set; }
        public List<GameObject2D> SceneObjects2D { get; private set; }

        public GameScene(string scenename)
        {
            SceneName = scenename;
            SceneObjects2D = new List<GameObject2D>();
        }

        public void AddSceneObject(GameObject2D sceneobject)
        {
            if (!SceneObjects2D.Contains(sceneobject))
            {
                sceneobject.Scene = this;
                SceneObjects2D.Add(sceneobject);
            }
        }

        public void RemoveSceneObject(GameObject2D sceneobject)
        {
            if (SceneObjects2D.Remove(sceneobject))
            {
                sceneobject.Scene = null;
            }
        }

        public virtual void Initialize()
        {
            SceneObjects2D.ForEach(sceneobject => sceneobject.Initialize());
        }

        public virtual void Draw(RenderContext rendercontext)
        {
            SceneObjects2D.ForEach(sceneobject => sceneobject.Draw(rendercontext));
        }

        public virtual void LoadContent(ContentManager contentmanager)
        {
            SceneObjects2D.ForEach(sceneobject => sceneobject.LoadContent(contentmanager));
        }

        public virtual void Update(RenderContext rendercontext, ContentManager contentmanager)
        {
            SceneObjects2D.ForEach(sceneobject => sceneobject.Update(rendercontext));
        }

        public virtual void ResetScene() { }

        public virtual void ChangeCalibrationValue() { }

        public virtual void BackPressed() { }
    }
}
