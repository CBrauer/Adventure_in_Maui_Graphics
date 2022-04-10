namespace Adventure_in_Maui_Graphics {
  partial class myForm {

    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    private void InitializeComponent() {
      this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
      this.skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl();
      this.skglControl3 = new SkiaSharp.Views.Desktop.SKGLControl();
      this.skglControl2 = new SkiaSharp.Views.Desktop.SKGLControl();
      this.tableLayoutPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel
      // 
      this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.tableLayoutPanel.ColumnCount = 3;
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
      this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
      this.tableLayoutPanel.Controls.Add(this.skglControl3, 2, 0);
      this.tableLayoutPanel.Controls.Add(this.skglControl2, 1, 0);
      this.tableLayoutPanel.Controls.Add(this.skglControl1, 0, 0);
      this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      this.tableLayoutPanel.RowCount = 1;
      this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel.Size = new System.Drawing.Size(1799, 704);
      this.tableLayoutPanel.TabIndex = 7;
      // 
      // skglControl1
      // 
      this.skglControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.skglControl1.BackColor = System.Drawing.Color.Black;
      this.skglControl1.Location = new System.Drawing.Point(6, 7);
      this.skglControl1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.skglControl1.Name = "skglControl1";
      this.skglControl1.Size = new System.Drawing.Size(617, 690);
      this.skglControl1.TabIndex = 6;
      this.skglControl1.VSync = true;
      this.skglControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.skglControl1_PaintSurface);
      this.skglControl1.SizeChanged += new System.EventHandler(this.skglControl1_SizeChanged);
      // 
      // skglControl3
      // 
      this.skglControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.skglControl3.BackColor = System.Drawing.Color.Black;
      this.skglControl3.Location = new System.Drawing.Point(1174, 7);
      this.skglControl3.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.skglControl3.Name = "skglControl3";
      this.skglControl3.Size = new System.Drawing.Size(619, 690);
      this.skglControl3.TabIndex = 8;
      this.skglControl3.VSync = true;
      this.skglControl3.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.skglControl3_PaintSurface);
      this.skglControl3.SizeChanged += new System.EventHandler(this.skglControl3_SizeChanged);
      // 
      // skglControl2
      // 
      this.skglControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.skglControl2.BackColor = System.Drawing.Color.Black;
      this.skglControl2.Location = new System.Drawing.Point(635, 7);
      this.skglControl2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.skglControl2.Name = "skglControl2";
      this.skglControl2.Size = new System.Drawing.Size(527, 690);
      this.skglControl2.TabIndex = 7;
      this.skglControl2.VSync = true;
      this.skglControl2.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.skglControl2_PaintSurface);
      this.skglControl2.SizeChanged += new System.EventHandler(this.skglControl2_SizeChanged);
      // 
      // myForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.InactiveCaption;
      this.ClientSize = new System.Drawing.Size(1799, 704);
      this.Controls.Add(this.tableLayoutPanel);
      this.ForeColor = System.Drawing.SystemColors.Window;
      this.Margin = new System.Windows.Forms.Padding(6);
      this.Name = "myForm";
      this.Text = "Maui.Graphics - WinForms";
      this.Load += new System.EventHandler(this.myForm_Load);
      this.tableLayoutPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    private SkiaSharp.Views.Desktop.SKGLControl skglControl1;
    private SkiaSharp.Views.Desktop.SKGLControl skglControl2;
    private SkiaSharp.Views.Desktop.SKGLControl skglControl3;
  }
}