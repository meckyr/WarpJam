using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WarpJam.Tools
{
    class GameAnimatedSprite : GameSprite
    {
        private readonly int rowCount;
        private readonly int columnCount;

        private int totalFrameTime;
        private Rectangle frameRect;

        public int NumFrames { get; private set; }
        public Point FrameSize { get; private set; }
        public bool IsPlaying { get; private set; }
        public bool IsPaused { get; private set; }

        public int CurrentFrame { get; set; }
        public int FrameInterval { get; set; }
        public bool IsLooping { get; set; }

        public GameAnimatedSprite(string assetfile, int numframes,
            int frameinterval, Point framesize, int framesperrow)
            : base(assetfile)
        {
            NumFrames = numframes;
            FrameInterval = frameinterval;
            FrameSize = framesize;

            frameRect = new Rectangle(0, 0, framesize.X, framesize.Y);
            rowCount = 1;
            columnCount = numframes;

            if (framesperrow < numframes)
            {
                rowCount = numframes / framesperrow;
                columnCount = framesperrow;
            }

            DrawRect = frameRect;
        }

        public GameAnimatedSprite(string assetfile, int numframes,
            int frameinterval, Point framesize)
            : this(assetfile, numframes, frameinterval, framesize, numframes)
        {
        }

        public void PlayAnimation()
        {
            PlayAnimation(false);
        }

        public void PlayAnimation(bool loop)
        {
            if (IsPaused)
            {
                IsPaused = false;
                return;
            }

            IsPlaying = true;
            IsLooping = loop;
        }

        public void StopAnimation()
        {
            IsPlaying = false;
            CurrentFrame = 0;
            totalFrameTime = 0;
        }

        public void PauseAnimation()
        {
            IsPaused = true;
        }

        public override void Update(RenderContext renderContext)
        {
            if (IsPlaying && !IsPaused)
            {
                totalFrameTime += renderContext.GameTime.ElapsedGameTime.Milliseconds;

                if (totalFrameTime >= FrameInterval)
                {
                    totalFrameTime = 0;

                    if (rowCount > 1)
                    {
                        frameRect.Location = new Point(FrameSize.X
                            * (CurrentFrame % columnCount), FrameSize.Y
                            * (int)Math.Floor(CurrentFrame / columnCount)
                            );
                    }
                    else
                    {
                        frameRect.Location = new Point(FrameSize.X
                            * CurrentFrame, 0);
                    }

                    DrawRect = frameRect;
                    ++CurrentFrame;

                    if (CurrentFrame >= NumFrames)
                    {
                        CurrentFrame = 0;
                        IsPlaying = IsLooping;
                    }
                }
            }

            base.Update(renderContext);
        }
    }
}
