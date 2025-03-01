using System.Drawing;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  partial class MineControl
  {
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        grayBrush.Dispose();
        darkGrayBrush.Dispose();
        darkGrayPen.Dispose();
        lightPen.Dispose();
        font.Dispose();
        brush1.Dispose();
        brush2.Dispose();
        brush3.Dispose();
        brush4.Dispose();
        brush5.Dispose();
        brush6.Dispose();
        brush7.Dispose();
        brush8.Dispose();
        redBrush.Dispose();
        doubtBrush.Dispose();
        imgMarked.Dispose();
        imgNotDiscovery.Dispose();
        imgMarkedWrong.Dispose();
      }
      base.Dispose( disposing );
    }
    /************************************************/
    private void InitializeComponent()
    {
      this.Name = "mineControl";
      grayBrush = new SolidBrush(gray);
      darkGrayBrush = new SolidBrush(darkGray);
      redBrush = new SolidBrush(Color.Red);
      darkGrayPen = new Pen(darkGray, 1);
      lightPen = new Pen(Color.White, 1);
      font = new Font("Arial Black", 9, FontStyle.Regular);
      brush1 = new SolidBrush(Color.Blue);
      brush2 = new SolidBrush(Color.Green);
      brush3 = new SolidBrush(Color.Red);
      brush4 = new SolidBrush(Color.Navy);
      brush5 = new SolidBrush(Color.Maroon);
      brush6 = new SolidBrush(Color.Teal);
      brush7 = new SolidBrush(Color.Black);
      brush8 = new SolidBrush(Color.Gray);
      doubtBrush = new SolidBrush(Color.Black);
      imgMarked =  getBitmap("Marked.png");
      imgNotDiscovery = getBitmap("NotDiscovery.png");
      imgMarkedWrong = getBitmap("MarkedWrong.png");
    }
  }
}