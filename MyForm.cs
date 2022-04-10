
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Timers;

using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

namespace Adventure_in_Maui_Graphics;
public partial class myForm:Form {
  private readonly M_ M = new();
  public myForm() {
    InitializeComponent();
  }
  private void SetTimer() {
    var myTimer = new System.Timers.Timer(1000);
    myTimer.Elapsed += OnTimedEvent;
    myTimer.AutoReset = true;
    myTimer.Enabled = true;
  }
  private void OnTimedEvent(Object? source, ElapsedEventArgs e) {
    Debug.Assert(e != null);
    skglControl1.Invalidate();
    skglControl2.Invalidate();
    skglControl3.Invalidate();
  }
  private void myForm_Load(object sender, EventArgs e) {
    SetTimer();
  }
  private void skglControl1_PaintSurface(object sender, 
                                           SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e) {
    try {
      ICanvas canvas = new SkiaCanvas() {
        Canvas = e.Surface.Canvas
      };

      M.canvas = canvas;
      M.canvasWidth = skglControl1.Width;
      M.canvasHeight = skglControl1.Height;
      M.LineThickness = 4.0f;
      M.FontSize = 24;
      M.EraseChart();

      var testPattern = new TestPattern();
      testPattern.M = M;
      testPattern.Draw();

    } catch (Exception ex) {
      _ = MessageBox.Show(ex.Message);
    }
  }

  private void skglControl2_PaintSurface(object sender,
                                         SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e) {
    try {
      ICanvas canvas = new SkiaCanvas() {
        Canvas = e.Surface.Canvas
      };

      M.canvas = canvas;
      M.canvasWidth = skglControl2.Width;
      M.canvasHeight = skglControl2.Height;
      M.LineThickness = 4.0f;
      M.FontSize = 24;
      M.EraseChart();

      var meter = new Meter();
      meter.M = M;
      var rnd = new Random();
      var trin = rnd.NextDouble() * 4.0;
      meter.DrawMeter("NYSE TRIN", trin, 123.45);

    } catch (Exception ex) {
      _ = MessageBox.Show(ex.Message);
    }
  }

  private void skglControl3_PaintSurface(object sender,
                                         SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e) {
    try {
      ICanvas canvas = new SkiaCanvas() {
        Canvas = e.Surface.Canvas
      };

      M.canvas = canvas;
      M.canvasWidth = skglControl3.Width;
      M.canvasHeight = skglControl3.Height;
      M.LineThickness = 4.0f;
      M.FontSize = 24;
      M.EraseChart();

      var velocityChart = new VelocityChart();
      velocityChart.M = M;
      velocityChart.DrawChart();

    } catch (Exception ex) {
      _ = MessageBox.Show(ex.Message);
    }
  }

  private void skglControl1_SizeChanged(object sender, EventArgs e) => skglControl1.Invalidate();
  private void skglControl2_SizeChanged(object sender, EventArgs e) => skglControl2.Invalidate();
  private void skglControl3_SizeChanged(object sender, EventArgs e) => skglControl3.Invalidate();

}

