using System;
using System.Diagnostics;

using Microsoft.Maui.Graphics;

namespace Adventure_in_Maui_Graphics;
internal class Meter {

  #region Fields
  int labelOffset;
  float rOffset, needleLength;
  float minorTicThickness;
  float minorTicHeight;
  float majorTicThickness;
  float majorTicHeight;
  #endregion

  #region Properties
  public M_ M {  get; set; }
  public float canvasWidth { get; set; } = 100;
  public float canvasHeight { get; set; } = 100;
  public float centerX { get; set; }
  public float centerY { get; set; }
  public float DotRadius { get; set; } = 2.0f;

  private float lineThickness = 2.0f;
  public float LineThickness {
    get {
      return lineThickness;
    }
    set {
      lineThickness = value;
      M.LineThickness = lineThickness;
      Debug.WriteLine($"LineThickness is now {LineThickness}");
    }
  }
  public float Radius { get; set; }

  private float fontSize = 18;
  public float FontSize {
    get => fontSize;
    set {
      fontSize = value;
      M.FontSize = fontSize;
    }
  }
  public int Offset { get; set; } = 150;
  public float tickLength { get; set; } = 30f;
  public float Radius1 { get => Radius; set => Radius = value; }
  #endregion

  #region DrawMeter
  public void DrawMeter(string symbol, double trin, double indexChg) {
    if (M == null) {
      Debug.WriteLine("M is null in DrawMeter.");
      return;
    }
    (canvasWidth, canvasHeight) = M.GetChartHeightWidth();
    centerX = canvasWidth / 2.0f;
    centerY = canvasHeight / 2.0f;
    M.EraseChart();

    // This is used to debug the size and position of the drawing
    // M.DrawBox(1.0, 1.0, canvasWidth - 2.0, canvasHeight - 2.0, Colors.Red);

    Radius = Math.Min(canvasWidth, canvasHeight) / 2.0f - 20.0f;
    majorTicHeight = 20f;
    majorTicThickness = 4f;
    minorTicHeight = 10f;
    minorTicThickness = 2f;
    needleLength = Radius - majorTicHeight - 25f;
    labelOffset = 20;
    rOffset = 4f;
    FontSize = 20;

    M.DrawFilledCircle(centerX, centerY, Radius, Colors.Black);

    M.DrawText(centerX - labelOffset, centerY, "Selling", Colors.White, Horizontal.Left, Vertical.Above);
    M.DrawText(centerX - labelOffset, centerY, "Pressure", Colors.White, Horizontal.Left, Vertical.Below);
    M.DrawText(centerX + labelOffset, centerY, "Buying", Colors.White, Horizontal.Right, Vertical.Above);
    M.DrawText(centerX + labelOffset, centerY, "Pressure", Colors.White, Horizontal.Right, Vertical.Below);

    // DrawBuyArc();
    // DrawSellArc();
    // DrawSellArc2();

    SetNeedle(trin, indexChg);

    DateTime dt = DateTime.Now;
    var hour = dt.Hour + 3;  // New york time
    var minute = dt.Minute;
    var second = dt.Second;
    string text = $"{symbol}: {trin:F2} {hour}:{minute}:{second}";
    var (textWidth, textHeight) = M.GetTextSize(text);
    M.DrawText(centerX, centerY - textHeight, text, Colors.White, Horizontal.Center, Vertical.Below);

    text = $"Index Change: {indexChg}";
    M.DrawText(centerX, centerY - 2.5f*textHeight, text, Colors.White, Horizontal.Center, Vertical.Middle);

    double radian = 180.0f / Math.PI;
    for (int t = 0; t <= 39; t += 2) {
      double angle = 90.0f * (1.0f - t / 10.0f);
      double r = Radius - majorTicHeight - rOffset;
      double a = r * Math.Sin(angle / radian);
      double b = r * Math.Cos(angle / radian);
      double x = centerX + a;
      double y = centerY + b;
      string sTrin;
      if (t >= 0 && t <= 8) {
        sTrin = string.Format("{0:F1}", t / 10.0);
        M.DrawText(x, y, sTrin, Colors.White, Horizontal.Left, Vertical.Middle);
      }
      if (t == 10) {
        sTrin = string.Format("{0:F0}", t / 10.0);
        M.DrawText(x, y + 4, sTrin, Colors.White, Horizontal.Center, Vertical.Below);
      }
      if (t > 10 && t < 30) {
        sTrin = string.Format("{0:F1}", t / 10.0);
        M.DrawText(x, y, sTrin, Colors.White, Horizontal.Right, Vertical.Middle);
      }
      if (t == 30) {
        sTrin = string.Format("{0:F0}", t / 10.0);
        M.DrawText(x, y + 6, sTrin, Colors.White, Horizontal.Center, Vertical.Middle);
      }
      if (t > 30 && t <= 39) {
        sTrin = string.Format("{0:F1}", t / 10.0);
        M.DrawText(x, y, sTrin, Colors.White, Horizontal.Left, Vertical.Middle);
      }
    }

    // Draw major tic marks
    M.LineThickness = majorTicThickness;
    for (int t = 0; t <= 39; t += 2) {
      double angle = 90.0f * (1.0f - t / 10.0f);
      double r = Radius;
      double a = r * Math.Sin(angle / radian);
      double b = r * Math.Cos(angle / radian);
      double x1 = centerX + a;
      double y1 = centerY + b;
      r = Radius - majorTicHeight;
      a = r * Math.Sin(angle / radian);
      b = r * Math.Cos(angle / radian);
      double x2 = centerX + a;
      double y2 = centerY + b;
      M.DrawLine(x1, y1, x2, y2, Colors.White);
    }

    // Draw minor tic marks
    M.LineThickness = minorTicThickness;
    int n = 0;
    for (int k = 0; k <= 38; k++) {
      for (int j = 0; j < 3; j++) {
        double angle = 90.0f * (1.0f - n / 20.0f);
        double r = Radius;
        double a = r * Math.Sin(angle / radian);
        double b = r * Math.Cos(angle / radian);
        double x1 = centerX + a;
        double y1 = centerY + b;
        r = Radius - minorTicHeight;
        a = r * Math.Sin(angle / radian);
        b = r * Math.Cos(angle / radian);
        double x2 = centerX + a;
        double y2 = centerY + b;
        M.DrawLine(x1, y1, x2, y2, Colors.White);
        n++;
      }
    }
  }
  #endregion

  #region SetNeedle
  public void SetNeedle(double trin, double indexChg) {
    try {
      float angle = -90.0f * (float) trin;

      var x1 = canvasWidth / 2f;
      var y1 = canvasHeight / 2f;
      var x2 = centerX + needleLength;
      var y2 = centerY;
      if (M == null) {
        Debug.WriteLine("M is null in SetNeedle.");
        return;
      }
      M.DrawNeedle(centerX,
                   centerY,
                   angle, 
                   x1, 
                   y1,
                   x2, 
                   y2,
                   lineThickness:8, 
                   Colors.White);

    } catch (Exception ex) {
      Debug.WriteLine("MeterFaces.txt", "SetNeedle", ex.Message);
    }
  }
  #endregion

  #region GetPoint
  private Point GetPoint(float originY,
                         float length,
                         double trin) 
  {
    var angle = 90.0 * trin;
    var radian = 180.0 / 3.1415926536;
    var a = length * Math.Cos(angle / radian);
    var b = length * Math.Sin(angle / radian);
    var centerX = (canvasWidth + canvasHeight) / 2;
    var x = centerX + a;
    var y = originY - b;

    var point = new Point(x, y);
    return point;
  }
  #endregion

}
