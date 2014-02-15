using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarpJam.Tools
{
    class CameraManager
    {
        private static CameraManager INSTANCE = new CameraManager();

        public Camera2D camera;

        public static void prepareManager(Camera2D camera)
        {
            INSTANCE.camera = camera;
        }

        public static CameraManager getInstance()
        {
            return INSTANCE;
        }
    }
}
