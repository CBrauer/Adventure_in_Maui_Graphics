using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

using MathNet.Numerics;
using Microsoft.Maui.Graphics;

namespace Adventure_in_Maui_Graphics;
internal class VelocityChart {

  #region Fields
  private Point lowerLeft;
  private static int offset = 80;
  #endregion

  #region Properties
  public M_? M { get; set; }
  public float canvasWidth { get; set; } = 100;
  public float canvasHeight { get; set; } = 100;
  public float lx { get; set; }
  public float rx { get; set; }
  public float uy { get; set; }
  public float ly { get; set; }
  public int npoints { get; set; }
  public double[] x { get; set; } = new double[0];
  public double[] y { get; set; } = new double[0];
  public double x_min { get; set; }
  public double x_max { get; set; }
  public double y_min { get; set; }
  public double y_max { get; set; }
  public double x_range { get; set; }
  public double y_range { get; set; }
  #endregion

  #region InitChart
  public void InitChart(double[] x_, double[] y_) {
    Debug.Assert(M != null);
    M.Offset = offset = 80;
    (lx, ly, rx, uy) = M.GetAxis();
    canvasWidth = M.canvasWidth;
    canvasHeight = M.canvasHeight;
    M.EraseChart();
    M.LineThickness = 1.0f;
    M.FontSize = 20f;
    lowerLeft = new Point(offset, offset);
    x = x_;
    y = y_;
    if (x == null || y == null) {
      Debug.WriteLine("VelocityChart: x or y is null");
      return;
    }
    x_min = x[0];
    x_max = x.Max();
    y_min = Math.Round(Math.Floor(100.0 * y.Min()) / 100.0, 2);
    y_max = Math.Round(Math.Ceiling(100.0 * y.Max()) / 100.0, 2);
    x_range = Convert.ToSingle(canvasWidth - offset - lowerLeft.X);
    y_range = Convert.ToSingle(canvasHeight - offset - lowerLeft.Y);
    npoints = y.Length;
  }
  #endregion

  #region DrawTitle
  public void DrawTitle(string title1, string title2) {
    Debug.Assert(M != null);
    var lineThicknessSave = M.LineThickness;
    M.LineThickness = 2 * lineThicknessSave;
    var x = lx;
    var y = uy + 50;
    M.DrawLine(x, y, x + 20, y, Colors.Green);
    M.DrawText(x, y, title1, Colors.Black, Horizontal.Right, Vertical.Middle);

    y = uy + 20;
    M.DrawLine(x, y, x + 20, y, Colors.Orange);
    M.DrawText(x, y, title2, Colors.Black, Horizontal.Right, Vertical.Middle);
    M.LineThickness = lineThicknessSave;
  }
  #endregion

  #region DrawXAxisTitle
  public void DrawXAxisTitle(string title) {
    Debug.Assert(M != null);
    M.DrawText((lx + rx) / 2.0, ly - 35, title, Colors.Black, Horizontal.Center, Vertical.Below);
  }
  #endregion

  #region DrawYAxisTitle
  public void DrawYAxisTitle(string title) {
    // M.DrawTextVertical(10, (ly + uy) / 2.0, title, Color.Black, Horizontal.Left, Vertical.Middle);
  }
  #endregion

  #region DrawGrid
  public void DrawGrid() {
    if (x == null || y == null)
      return;

    var nVerticalLines = 10;
    var nHorizontalLines = 10;
    var deltaX = (rx - lx) / nVerticalLines;
    var deltaY = (uy - ly) / nHorizontalLines;
    Debug.Assert(M != null);
    M.LineThickness = 1.0f;

    // Draw the vertical grid lines
    for (var k = 0; k <= nVerticalLines; k++) {
      double X = lx + k * deltaX;
      // Debug.WriteLine("vertical grid, k: {0}, X: {1} deltaX {2} incrementX {3}", k, X, deltaX, incrementX);
      var value = x_min + k * deltaX;
      M.DrawLine(X, ly, X, uy, value == 0 ? Colors.Black : Colors.LightGray);
      if (k < nVerticalLines) {
        var index = k * npoints / nVerticalLines;
        value = Math.Round(x[index], 0);
      } else {
        value = x.Last() + (x[1] - x[0]);
      }
      M.DrawLine(X, ly, X, ly - 10, Colors.Black);
      M.DrawText(X, ly - 12, value.ToString(), Colors.Black, Horizontal.Center, Vertical.Below);
    }
    // Debug.WriteLine("grid 1, xmin: {0}, xmax: {1} incrementX {2} nVerticleLines: {3}",
    //                 xmin, xmax, incrementX, nVerticalLines);

    // Draw the horizontal grid lines
    for (var k = 0; k <= nHorizontalLines; k++) {
      double Y = ly + k * deltaY;
      // Debug.WriteLine("horizontal grid, k: {0}, Y: {1}", k, Y);
      var value = y_min + k * deltaY;
      M.DrawLine(lx, Y, rx, Y, value == 0 ? Colors.Black : Colors.LightGray);
      if (k < nHorizontalLines) {
        var index = k * npoints / nHorizontalLines;
        value = Math.Round(y[index], 3);
      } else {
        value = y.Last() + (y[1] - y[0]);
        value = Math.Round(value, 3);
      }
      M.DrawLine(lx, Y, lx - 10, Y, Colors.Black);
      M.DrawText(lx - 12, Y, value.ToString(), Colors.Black, Horizontal.Left, Vertical.Middle);
    }
    // Debug.WriteLine("grid 2, ymin: {0}, ymax: {1} incrementY {2} nHorizontalLines: {3}",
    //                 ymin, ymax, incrementY, nHorizontalLines);
  }
  #endregion

  #region DrawScatterPlot
  public void DrawScatterPlot(Color color) {
    if (x == null || y == null) {
      Debug.WriteLine("x or y is null");
      return;
    }
    if ((y_max - y_min) == 0) {
      return;
    }

    var X = new float[npoints];
    var Y = new float[npoints];
    const float circleRadius = 3.0f;
    for (var k = 0; k < npoints; k++) {
      X[k] = (float) (lowerLeft.X + x_range * (x[k] - x_min) / (x_max - x_min));
      Y[k] = (float) (lowerLeft.Y + y_range * (y[k] - y_min) / (y_max - y_min));
      // Debug.WriteLine("X: {0}, Y: {1}", X, Y);
      Debug.Assert(M != null);
      M.DrawFilledCircle(X[k], Y[k], circleRadius, color);
    }
  }
  #endregion

  #region DrawLinearRegressionLine
  public void DrawLinearRegressionLine(Color color) {
    if (x == null || y == null) {
      Debug.WriteLine("x or y is null");
      return;
    }

    var X = new double[npoints];
    var Y = new double[npoints];
    for (var k = 0; k < npoints; k++) {
      X[k] = lowerLeft.X + x_range * (x[k] - x_min) / (x_max - x_min);
      Y[k] = lowerLeft.Y + y_range * (y[k] - y_min) / (y_max - y_min);
    }

    var tuple = Fit.Line(X, Y);
    var intercept = tuple.Item1;
    var slope = tuple.Item2;
    var y1 = slope * X[0] + intercept;
    var y2 = slope * X.Last() + intercept;

    Debug.Assert(M != null);
    M.LineThickness = 2.0f;
    M.DrawLine(X[0], y1, X.Last(), y2, color);
    M.LineThickness = 1.0f;
  }
  #endregion

  #region DrawRegressionCurve
  public void DrawRegressionCurve(double[] fit_curve,Color color) {
    if (x == null || y == null)
      return;

    var X = new double[npoints];
    var Y = new double[npoints];
    for (var k = 0; k < npoints; k++) { 
      X[k] = lowerLeft.X + x_range * (x[k] - x_min) / (x_max - x_min);
      Y[k] = lowerLeft.Y + y_range * (y[k] - y_min) / (y_max - y_min);
    }

    for (var k = 0; k < npoints; k++) {
      var y_curve = fit_curve[k];
      X[k] = lowerLeft.X + x_range * (x[k] - x_min) / (x_max - x_min);
      Y[k] = lowerLeft.Y + y_range * (y_curve - y_min) / (y_max - y_min);
    }

    Debug.Assert(M != null);
    M.LineThickness = 2.0f;
    for (var k = 1; k < npoints; k++) {
      M.DrawLine(X[k - 1], Y[k - 1], X[k], Y[k], color);
    }
    M.LineThickness = 1.0f;
  }
  #endregion

  #region SmoothingSpline
  public static class SmoothingSpline {
    public static (bool, string) Fit(double[] x,
                                     double[] y,
                                     double rho,
                                     int n,
                                     out double[] smoothY,
                                     out double[] firstDeriv,
                                     out double[] secondDeriv) {

      // alglib.spline1dfitreport rep
      // Spline fitting report:
      //   RMSError RMS error
      //   AvgError average error
      //   AvgRelError average relative error(for non - zero Y[I])
      //   MaxError maximum error

      try {
        alglib.spline1dfitpenalized(x,
                                    y,
                                    n,
                                    rho,
                                    out int info,
                                    out alglib.spline1dinterpolant s,
                                    out _);   // alglib.spline1dfitreport rep


        smoothY = new double[n];
        firstDeriv = new double[n];
        secondDeriv = new double[n];
        for (var k = 0; k < n; k++) {
          double v, d1, d2;
          alglib.spline1ddiff(s, x[k], out v, out d1, out d2);
          smoothY[k] = v;
          firstDeriv[k] = d1;
          secondDeriv[k] = d2;
        }
        if (info == 1) {
          return (true, "ok");
        } else {
          var msg = "Dammit";
          return (false, msg);
        }
      } catch (Exception ex) {
        Debug.WriteLine("SmoothingSpline error: " + ex.Message);
        smoothY = new double[0];
        firstDeriv = new double[0];
        secondDeriv = new double[0];
        return (false, ex.Message);
      }
    }
  }
  #endregion

  #region DrawChart
  public void DrawChart() {
    var filePath = @"../../../../Data/velocity_acceleration.csv";
    if (!File.Exists(filePath)) {
      Debug.WriteLine("Cannot find velocity_acceleration.csv.");
      return;
    }
    var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
    var sw = new StreamReader(fs);

    var header1 = sw.ReadLine();
    var header2 = sw.ReadLine();
    Debug.Assert(header2 != null);
    var fields = header2.Split(',');
    var str = fields[0].Split(' ')[1].Trim();
    var velocity = Convert.ToDouble(str);
    str = fields[1].Split(' ')[2].TrimStart().TrimEnd();
    var acceleration = Convert.ToDouble(str);

    var header3 = sw.ReadLine();
    //"           dateTime,          pc     velocity acceleration";
    //"-------------------, -----------, -----------, -----------";

    var npoints = 60;
    var x = new double[npoints];
    var y = new double[npoints];
    var smoothY = new double[npoints];
    var firstDeriv = new double[npoints];
    var secondDeriv = new double[npoints];
    var velocityCurve = new double[npoints];
    var accelerationCurve = new double[npoints];

    for (var k = 0; k < npoints; k++) {
      var line = sw.ReadLine();
      Debug.Assert(line != null);
      fields = line.Split(',');
      var date_time = fields[0];
      var pc = Convert.ToDouble(fields[1].Trim());
      var vc = Convert.ToDouble(fields[2].Trim());
      var ac = Convert.ToDouble(fields[3].Trim());
      x[k] = k;
      y[k] = pc;
      velocityCurve[k] = vc;
      accelerationCurve[k] = ac;
    }
    sw.Close();

    InitChart(x, y);
    DrawGrid();
    DrawScatterPlot(Colors.Black);

    try {
      DrawRegressionCurve(velocityCurve, Colors.ForestGreen);

      var rho = 3.0;
      var n = y.Length;
      SmoothingSpline.Fit(x,
                          y,
                          rho,
                          n,
                          out smoothY,
                          out firstDeriv,
                          out secondDeriv);

      DrawRegressionCurve(smoothY, Colors.Orange);

      // Green line
      var title1 = string.Format($"velocity: {velocity:f6}, acceleration: {acceleration:f6}");
      // Orange Line
      var velocity2 = firstDeriv.Last();
      var acceleration2 = secondDeriv.Last();
      var title2 = string.Format($"velocity: {velocity2:f6}, acceleration: {acceleration2:f6}");
      DrawTitle(title1, title2);
    } catch (Exception) {
      DrawTitle("", "Oops, regression line is all zeros!");
    }
    DrawXAxisTitle("Time");
    DrawYAxisTitle("Velocity");
  }
  #endregion
}
