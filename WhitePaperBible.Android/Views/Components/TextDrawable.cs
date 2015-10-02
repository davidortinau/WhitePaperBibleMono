﻿using System;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace Rendr.Droid.Components
{
    /// <summary>
    /// Shows text as a drawable.
    /// </summary>
    /// Based on http://stackoverflow.com/questions/3972445/how-to-put-text-in-a-drawable
    public class TextDrawable: Drawable {

        private readonly string _text;
        private readonly Paint _paint;
        private static Typeface _iconFont;

        public TextDrawable(string text, Android.Content.Context ctx) {

            _text = text;


            if (_iconFont == null)
                _iconFont = Typeface.CreateFromAsset(ctx.Assets, "fontawesome-webfont.ttf");

            _paint = new Paint {Color = (Color.White), TextSize = 58f, AntiAlias = true};
            _paint.SetTypeface(_iconFont);
            //            _paint.SetShadowLayer(6f, 0, 0, Color.Black);
            _paint.SetStyle(Paint.Style.Fill);
            _paint.TextAlign = Paint.Align.Center;
        }


        public override void Draw(Canvas canvas) {
            canvas.DrawText(_text, 0, 25, _paint);
        }


        public override void SetAlpha(int alpha) {
            _paint.Alpha = alpha;
        }


        public override void SetColorFilter(ColorFilter cf)
        {
            _paint.SetColorFilter(cf);
        }

        public override int Opacity
        {
            get { return -3; /*translucent*/ }
        }


    }
}

