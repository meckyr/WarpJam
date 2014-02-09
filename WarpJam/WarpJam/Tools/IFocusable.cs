using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WarpJam.Tools
{
    public interface IFocusable
    {
        Vector2 Position { get; set; }
    }
}
