using System;
using System.Diagnostics;

using Microsoft.Maui.Graphics;

namespace Adventure_in_Maui_Graphics;
public class TestPattern {

  #region Properties
  public M_? M { get; set; }
  public float canvasWidth { get; set; } = 100;
  public float canvasHeight { get; set; } = 100;

  private ICanvas? myCanvas;
  public ICanvas? canvas {
    get {
      return myCanvas;
    }
    set {
      myCanvas = value;
    }
  }
  public float DotRadius { get; set; } = 2.0f;

  private float lineThickness = 2.0f;
  public float LineThickness {
    get {
      return lineThickness;
    }
    set {
      lineThickness = value;
      Debug.WriteLine($"LineThickness is now {LineThickness}");
    }
  }
  public float FontSize { get; set; } = 24;
  public int Offset { get; set; } = 150;
  public float tickLength { get; set; } = 30f;
  #endregion

  public void Draw() {
    try {
      Debug.Assert(M != null);
      var (lx, ly, rx, uy) = M.GetAxis();
      var midX = (lx + rx) / 2.0f;
      var midY = (ly + uy) / 2.0f;
      if (M == null) {
        System.Windows.Forms.MessageBox.Show("Hmmm... canvas has not been definded.");
        Debug.WriteLine("M is null in Draw.");
        return;
      }

      M.EraseChart();

      M.DrawLine(lx, midY, rx, midY, Colors.Black);
      M.DrawLine(lx, ly, lx, uy, Colors.Red);
      M.DrawLine(lx, uy, rx, uy, Colors.Blue);
      M.DrawLine(rx, uy, rx, ly, Colors.Green);
      M.DrawLine(rx, ly, lx, ly, Colors.DarkTurquoise);

      M.DrawLine(lx, midY, lx - tickLength, midY, Colors.Red);
      M.DrawText(lx - tickLength, midY, "1.2", Colors.Red, Horizontal.Left, Vertical.Middle);
      M.DrawLine(midX, uy, midX, uy + tickLength, Colors.Blue);
      M.DrawText(midX, uy + tickLength, "1.23", Colors.Blue, Horizontal.Center, Vertical.Above);
      M.DrawLine(rx, midY, rx + tickLength, midY, Colors.Green);
      M.DrawText(rx + tickLength, midY, "1.234", Colors.Green, Horizontal.Right, Vertical.Middle);
      M.DrawLine(midX, ly, midX, ly - tickLength, Colors.DarkTurquoise);
      M.DrawText(midX, ly - tickLength, "1.2345", Colors.DarkTurquoise, Horizontal.Center, Vertical.Below);
    } catch (Exception ex) {
      Debug.WriteLine(ex.ToString());
    }
  }
}

