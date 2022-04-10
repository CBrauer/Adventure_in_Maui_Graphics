
using System;
using System.Windows.Forms;

namespace Adventure_in_Maui_Graphics;
static class Program {
  [STAThread]
  static void Main() {
    try {
      Application.SetHighDpiMode(HighDpiMode.SystemAware);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new myForm());
    } catch (Exception ex) {
      MessageBox.Show(ex.Message);
    }
  }
}
