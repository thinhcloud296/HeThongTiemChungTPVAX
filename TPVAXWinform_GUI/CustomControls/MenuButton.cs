using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TPVAXWinform.CustomControls
{
    public class MenuButton : Button
    {
        private Color _backgroundColor = Color.FromArgb(41, 128, 185);
        private Color _hoverBackgroundColor = Color.FromArgb(52, 152, 219);
        private Color _pressedBackgroundColor = Color.FromArgb(31, 97, 141);
 private Color _textColor = Color.White;
        private Color _iconColor = Color.White;
        private int _borderRadius = 8;
        private int _borderSize = 0;
  private Color _borderColor = Color.PaleVioletRed;

        private bool isHovering = false;
        private bool isPressed = false;

        // Icon
        private string _iconText = "";
        private Font _iconFont = new Font("Segoe MDL2 Assets", 16F);

public MenuButton()
        {
            FlatStyle = FlatStyle.Flat;
     FlatAppearance.BorderSize = 0;
     Size = new Size(150, 50);
       BackColor = _backgroundColor;
            ForeColor = _textColor;
Cursor = Cursors.Hand;
   Font = new Font("Segoe UI", 10F, FontStyle.Bold);
       TextAlign = ContentAlignment.MiddleLeft;
       Padding = new Padding(50, 0, 10, 0);

            // Thi?t l?p ?? control t? v?
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

  // Properties
        public Color BackgroundColor
        {
      get => _backgroundColor;
      set
       {
     _backgroundColor = value;
                Invalidate();
            }
        }

        public Color HoverBackgroundColor
        {
            get => _hoverBackgroundColor;
       set
      {
      _hoverBackgroundColor = value;
                Invalidate();
            }
        }

        public Color PressedBackgroundColor
        {
   get => _pressedBackgroundColor;
            set
            {
       _pressedBackgroundColor = value;
        Invalidate();
            }
        }

        public Color TextColor
        {
            get => _textColor;
   set
            {
        _textColor = value;
                Invalidate();
            }
     }

        public Color IconColor
    {
            get => _iconColor;
         set
            {
_iconColor = value;
         Invalidate();
 }
        }

        public int BorderRadius
        {
        get => _borderRadius;
            set
            {
        _borderRadius = value;
  Invalidate();
         }
        }

        public int BorderSize
        {
            get => _borderSize;
set
     {
 _borderSize = value;
    Invalidate();
   }
    }

        public Color BorderColor
        {
    get => _borderColor;
    set
            {
     _borderColor = value;
  Invalidate();
  }
        }

        public string IconText
        {
            get => _iconText;
            set
        {
     _iconText = value;
          Invalidate();
        }
        }

      public Font IconFont
        {
      get => _iconFont;
         set
        {
   _iconFont = value;
      Invalidate();
            }
        }

        // Methods
        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
   GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

     path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
     path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
        path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
       path.CloseFigure();
  return path;
     }

        protected override void OnPaint(PaintEventArgs e)
        {
   e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

   Rectangle rectSurface = ClientRectangle;
  Rectangle rectBorder = Rectangle.Inflate(rectSurface, -_borderSize, -_borderSize);
            int smoothSize = 2;

            if (_borderSize > 0)
 smoothSize = _borderSize;

            if (_borderRadius > 2) // Rounded button
            {
using (GraphicsPath pathSurface = GetFigurePath(rectSurface, _borderRadius))
       using (GraphicsPath pathBorder = GetFigurePath(rectBorder, _borderRadius - _borderSize))
    using (Pen penSurface = new Pen(Parent.BackColor, smoothSize))
          using (Pen penBorder = new Pen(_borderColor, _borderSize))
                {
  e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
         // Button surface
          Region = new Region(pathSurface);
           // Draw surface border for HD result
    e.Graphics.DrawPath(penSurface, pathSurface);

            // Button background color
        Color bgColor = _backgroundColor;
    if (isPressed)
             bgColor = _pressedBackgroundColor;
              else if (isHovering)
   bgColor = _hoverBackgroundColor;

                    using (SolidBrush brush = new SolidBrush(bgColor))
      {
       e.Graphics.FillPath(brush, pathBorder);
       }

     // Button border         
   if (_borderSize >= 1)
  e.Graphics.DrawPath(penBorder, pathBorder);
      }
      }
            else // Normal button
            {
        Region = new Region(rectSurface);

 Color bgColor = _backgroundColor;
    if (isPressed)
       bgColor = _pressedBackgroundColor;
       else if (isHovering)
     bgColor = _hoverBackgroundColor;

   using (SolidBrush brush = new SolidBrush(bgColor))
     {
        e.Graphics.FillRectangle(brush, rectBorder);
 }

       if (_borderSize >= 1)
      {
       using (Pen penBorder = new Pen(_borderColor, _borderSize))
       {
          penBorder.Alignment = PenAlignment.Inset;
        e.Graphics.DrawRectangle(penBorder, 0, 0, Width - 1, Height - 1);
             }
                }
    }

    // Draw icon
       if (!string.IsNullOrEmpty(_iconText))
      {
          using (SolidBrush brush = new SolidBrush(_iconColor))
       {
    SizeF iconSize = e.Graphics.MeasureString(_iconText, _iconFont);
            e.Graphics.DrawString(_iconText, _iconFont, brush, 
         new PointF(15, (Height - iconSize.Height) / 2));
     }
  }

   // Draw text
  using (SolidBrush brush = new SolidBrush(_textColor))
            {
      StringFormat stringFormat = new StringFormat
  {
         Alignment = StringAlignment.Near,
          LineAlignment = StringAlignment.Center
    };
        
           Rectangle textRect = new Rectangle(50, 0, Width - 60, Height);
      e.Graphics.DrawString(Text, Font, brush, textRect, stringFormat);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
     {
        base.OnMouseEnter(e);
 isHovering = true;
      Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
 {
          base.OnMouseLeave(e);
    isHovering = false;
            Invalidate();
        }

  protected override void OnMouseDown(MouseEventArgs e)
   {
 base.OnMouseDown(e);
     isPressed = true;
    Invalidate();
        }

      protected override void OnMouseUp(MouseEventArgs e)
    {
  base.OnMouseUp(e);
   isPressed = false;
       Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
   Parent.BackColorChanged += Container_BackColorChanged;
        }

    private void Container_BackColorChanged(object sender, EventArgs e)
     {
         Invalidate();
        }
    }
}
