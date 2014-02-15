using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarpJam.Tools;
using Microsoft.Xna.Framework;

namespace WarpJam
{

    class Pesawat : GameAnimatedSprite, IFocusable
    {
        #region Properties
        public Microsoft.Xna.Framework.Vector2 Position
        {
            get
            {
                return WorldPosition;
            }
            set
            {
                WorldPosition = value;
            }
        }

        #endregion

        #region Constructors
        public Pesawat(string assetfile, int numframes,
            int frameinterval, Point framesize, int framesperrow)
            : base(assetfile, numframes,
             frameinterval, framesize, framesperrow)
        {
        }

        public Pesawat(string assetfile, int numframes,
            int frameinterval, Point framesize)
            : base(assetfile, numframes,
                frameinterval, framesize)
        {
        }
        #endregion

        #region Methods

        #endregion
    }
}
