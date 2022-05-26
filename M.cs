using System;
using System.Diagnostics;

using Microsoft.Maui.Graphics;

namespace Adventure_in_Maui_Graphics;

#region Positioning
public enum Vertical {
  Above,
  Middle,
  Below
}
public enum Horizontal {
  Left,
  Center,
  Right
}
#endregion
public class M_ {

  #region Properties
  public ICanvas? canvas { get; set; }
  public float canvasWidth { get; set; } = 100;
  public float canvasHeight { get; set; } = 100;
  public float DotRadius { get; set; } = 2.0f;
  public float LineThickness { set; get; }
  public float FontSize { get; set; } = 24;
  public int Offset { get; set; } = 130;
  public float tickLength { get; set; } = 20f;
  #endregion

  #region EraseChart
  public void EraseChart() {
    if (canvas == null) {
      System.Windows.Forms.MessageBox.Show("Hmmm... canvas has not been definded.");
      System.Windows.Forms.Application.Exit();
      return;
    }
    canvas.FillColor = Colors.White;
    canvas.FillRectangle(0, 0, canvasWidth, canvasHeight);
    // Used to debug the drawing.
    // DrawBox(1.0, 1.0, canvasWidth - 2.0, canvasHeight - 2.0, Colors.Red);
  }
  #endregion

  #region DrawLine
  public void DrawLine(double x1, double y1, double x2, double y2, Color color) {
    try {
      // Cartesian coordinates (origin is in the lower left of first quadrant)
      var X1 = (float) x1;
      var Y1 = (float) (canvasHeight - y1);
      var X2 = (float) x2;
      var Y2 = (float) (canvasHeight - y2);
      Debug.Assert(canvas != null);
      canvas.StrokeColor = color;
      canvas.StrokeSize = LineThickness;
      canvas.DrawLine(X1, Y1, X2, Y2);
    } catch (Exception ex) {
      Debug.WriteLine("DrawLine error: ", ex);
    }
  }
  #endregion

  #region Rectangle
  public void DrawRectangle(float x, float y, float width, float height, Color color) {
    // Cartesian coordinates (origin is in the lower left of first quadrant)
    var X = (float) x;
    var Y = (float) (canvasHeight - y);
    var Width = (float) canvasWidth;
    var Height = (float) (canvasHeight - y);
    Debug.Assert(canvas != null);
    canvas.StrokeColor = color;
    canvas.StrokeSize = LineThickness;
    canvas.DrawRectangle(X, Y, Width, Height);
  }
  #endregion

  #region DrawFilledRectangle
  public void DrawFilledRectangle(double x, double y, double width, double height, Color color) {
    // Cartesian coordinates (origin is in the lower left of first quadrant)
    var X = (float) x;
    var Y = (float) (canvasHeight - y - height);
    //var X = (float) x;
    //var Y = (float) y;
    var Width = (float) width;
    var Height = (float) height;
    Debug.Assert(canvas != null);
    canvas.FillColor = color;
    canvas.FillRectangle(X, Y, Width, Height);
  }
  #endregion

  #region DrawBox
  public void DrawBox(double lx, double ly, double rx, double uy, Color borderColor) {
    DrawLine(lx, ly, lx, uy, borderColor);
    DrawLine(lx, uy, rx, uy, borderColor);
    DrawLine(rx, uy, rx, ly, borderColor);
    DrawLine(rx, ly, lx, ly, borderColor);
  }
  #endregion

  #region DrawCircle
  public void DrawCircle(double x, double y, double circleRadius, Color color) {
    var radius = (float) circleRadius;
    var X = (float) x;
    var Y = canvasHeight - (float) y;
    Debug.Assert(canvas != null);
    canvas.StrokeColor = color;
    canvas.DrawCircle(X, Y, radius);
  }
  #endregion

  #region DrawFilledCircle
  public void DrawFilledCircle(double x,
                               double y,
                               double circleRadius,
                               Color color) 
  {
    var radius = (float) circleRadius;
    var X = (float) x;
    var Y = canvasHeight - (float) y;
    Debug.Assert(canvas != null, "canvas is null in DrawFilledCircle");
    canvas.FillColor = color;
    canvas.FillCircle(X, Y, radius);
  }
  #endregion

  #region DrawArc
  public void DrawArc(double x, 
                      double y,
                      double width,
                      double height,
                      double startAngle,
                      double endAngle,
                      bool clockwise,
                      bool closed,
                      Color color) 
  {
    Debug.Assert(canvas != null, "canvas is null in DrawArc");
    var X = (float) x;
    var Y = canvasHeight - (float) y;  // Convert to Cartesian coordinates
    canvas.StrokeColor = color;
    canvas.StrokeSize = 10;
    canvas.DrawArc(X,
                   Y,
                   (float) width,
                   (float) height,
                   (float) startAngle,
                   (float) endAngle,
                   clockwise,
                   closed);
  }
  #endregion

  #region DrawDot
  public void DrawDot(double x_,
                      double y_, 
                      double circleRadius_,
                      Color color) 
  {
    var x = (float) x_;
    var y = (float) (canvasHeight - y_);
    var radius = (float) circleRadius_;
    Debug.Assert(canvas != null);
    canvas.FillColor = color;
    canvas.StrokeSize = 2;
    canvas.DrawArc(x, y, radius, radius, 0, 360, true, true);
  }
  #endregion

  #region GetTextSize
  public (float, float) GetTextSize(string text) {
    Font font = new();
    float fontSize = FontSize;
    Debug.Assert(canvas != null);
    SizeF stringSize = canvas.GetStringSize(text, font, fontSize);
    var textWidth = stringSize.Width * 1.1f;
    var textHeight = stringSize.Height;
    return (textWidth, textHeight);
  }
  #endregion

  #region DrawText
  public void DrawText(double x, double y,
                       string text,
                       Color color,
                       Horizontal horizontal,
                       Vertical vertical) 
  {
    try {
      var X = (float) x;
      var Y = (float) (canvasHeight - y); // Cartesian coordinate system
      Debug.Assert(canvas != null);
      canvas.FontColor = color;
      canvas.FontSize = FontSize;
      var xText = X;
      var yText = Y;

      Font font = new();
      float fontSize = FontSize;
      SizeF stringSize = canvas.GetStringSize(text, font, fontSize);
      var textWidth = stringSize.Width * 1.1f;
      var textHeight = stringSize.Height;
      Rect stringRect;
      PointF stringLocation;

      var h = HorizontalAlignment.Center;
      switch (horizontal) {
        case Horizontal.Center:
          xText = X - textWidth / 2;
          break;
        case Horizontal.Right:
          xText = X + 5.0f; // ToDo: this offset should not be necessary
          h = HorizontalAlignment.Right;
          break;
        case Horizontal.Left:
          xText = X - textWidth - 5.0f;
          h = HorizontalAlignment.Right;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(horizontal), horizontal, null);
      }
      var v = VerticalAlignment.Center;
      switch (vertical) {
        case Vertical.Above:
          yText = Y - textHeight - 3.0f;  // ToDo: this offset should not be necessary
          v = VerticalAlignment.Top;
          break;
        case Vertical.Middle:
          yText = Y - textHeight / 2.0f;
          v = VerticalAlignment.Center;
          break;
        case Vertical.Below:
          yText = Y + textHeight/2.0f - 10.0f;
          v = VerticalAlignment.Bottom;
          break;
      }

      stringLocation = new(xText, yText);
      stringRect = new(stringLocation, stringSize);
      stringRect.X = xText;
      stringRect.Y = yText;
      stringRect.Width = textWidth;

      // Text string bounding box used only for debugging
      //LineThicknesSave = LineThickness;
      //M.LineThickness = 1.0f;
      //canvas.StrokeSize = 1.0f;
      //canvas.StrokeColor = color;
      //canvas.DrawRectangle(stringRect);
      //M.LineThickness = LineThicknesSave;

      Debug.Assert(canvas != null);
      canvas.DrawString(value: text,
                        x: xText,
                        y: yText,
                        width: textWidth,
                        height: textHeight,
                        horizontalAlignment: h,
                        verticalAlignment: v,
                        textFlow: TextFlow.OverflowBounds,
                        lineSpacingAdjustment: 0);
    } catch (Exception ex) {
      Debug.WriteLine("DrawText", ex);
    }
  }
  #endregion

  #region DrawNeedle
  public void DrawNeedle(float x,
                         float y,               // rotate about this center.
                         float angle,           // angle (in degrees)
                         float x1,              // start of line segment
                         float y1,
                         float x2,              // end of line segment
                         float y2,              
                         float lineThickness,   // thickness of the line
                         Color color)           // color of the line
  {
    LineThickness = lineThickness;
    var X = x;
    var Y = canvasHeight - y;
    var X1 = x1;
    var Y1 = canvasHeight - y1;
    var X2 = x2;
    var Y2 = canvasHeight - y2;
    if (canvas == null) {
      System.Windows.Forms.MessageBox.Show("Hmmm... canvas has not been definded.");
      return;
    }
    canvas.Rotate(angle, X, Y);
    DrawLine(X1, Y1, X2, Y2, color);
    canvas.Rotate(-angle, X, Y);
  }
  #endregion

  #region GetColorFromRGB
  public Color GetColorFromRGB(byte r,
                               byte g,
                               byte b)
  {
    var color = new Color(r, g, b);
    return color;
  }
  #endregion

  #region Offsets
  public (float, float) GetChartHeightWidth() {
    canvasWidth = canvasWidth;
    canvasHeight = canvasHeight;
    if (canvasWidth <= 0 || canvasHeight <= 0) {
      System.Windows.Forms.MessageBox.Show("Hmmm... canvas has not been definded.");
      System.Windows.Forms.Application.Exit();
    }
    return (canvasWidth, canvasHeight);
  }
  public (float, float, float, float) GetAxis() {
    var lx = Offset;
    var ly = Offset;
    if (canvasWidth <= 0 || canvasHeight <= 0) {
      (canvasWidth, canvasHeight) = GetChartHeightWidth();
      if (canvasWidth <= 0 || canvasHeight <= 0) {
        System.Windows.Forms.MessageBox.Show("Hmmm... canvas has not been definded.");
        System.Windows.Forms.Application.Exit();
      }
    }
    var rx = canvasWidth - Offset;
    var uy = canvasHeight - Offset;
    return (lx, ly, rx, uy);
  } 
  #endregion

}
