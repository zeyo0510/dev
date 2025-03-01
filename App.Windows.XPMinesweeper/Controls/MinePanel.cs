using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  public class MinePanel : Panel
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public MinePanel()
    {
      InitializeComponent();

      SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer |
        ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
      SetStyle(ControlStyles.Selectable, false);
      BackColor = Color.Silver;
      Width = 320;
      Height = 240;
      ChangeFace(1);
      ArrangeChildChildren();
    }

    /// <summary> 
    /// 燴垀衄淏婓妏蚚腔訧埭﹝
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        darkGrayPen.Dispose();
        lightPen.Dispose();
        rbReset.Dispose();
        pnlLeft.Dispose();
        pnlRight.Dispose();
        ilLED.Dispose();
        tmrCount.Dispose();
      }
      base.Dispose( disposing );
    }

    private Color darkGray = Color.Gray;

    private Pen lightPen,  darkGrayPen;

    private ResetButton rbReset;
    private LEDPanel pnlLeft, pnlRight;
    private ImageList ilLED;
    private Timer tmrCount;

    #region 郪璃扢數汜傖腔測鎢
    /// <summary>
    /// 扢數盓厥垀剒腔源楊 - 祥猁妏蚚測鎢晤憮党蜊
    /// 森源楊腔囀﹝
    /// </summary>
    private void InitializeComponent()
    {
      Name = "minePanel";
      darkGrayPen = new Pen(darkGray, 1);
      lightPen = new Pen(Color.White, 1);
      rbReset = new ResetButton();
      rbReset.Name = "rbReset";
      rbReset.Text = "";
      rbReset.Parent = this;
      rbReset.Click += new EventHandler(OnReset);

      ilLED = new ImageList();
      ilLED.ImageSize = new Size(13, 23);
      string FileName = "0123456789-";
      for (int i = 0; i < FileName.Length; i++)
      {
        ilLED.Images.Add(getBitmap(FileName[i] + ".png", false));
      }

      pnlLeft = new LEDPanel();
      pnlLeft.Parent = this;
      pnlLeft.Name = "pnlLeft";
      pnlLeft.LEDImages = ilLED;
      pnlRight = new LEDPanel();
      pnlRight.Parent = this;
      pnlRight.Name = "pnlRight";
      pnlRight.LEDImages = ilLED;

      tmrCount = new Timer();
      tmrCount.Interval = 1000;
      tmrCount.Tick += new EventHandler(tmrCount_Tick);
      tmrCount.Stop();
    }
    #endregion

    private void tmrCount_Tick(object sender, EventArgs e)
    {
      pnlRight.Number = pnlRight.Number + 1;
    }

    private Bitmap getBitmap(string fileName, bool transparent)
    {
      Image img =  Image.FromStream(GetResource(fileName));
      Bitmap bmp = new Bitmap(img);
      img.Dispose();
      img = null;
      if (transparent)
        bmp.MakeTransparent(bmp.GetPixel(1, 1));
      return bmp;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Rectangle rect = ClientRectangle;
      Graphics g = e.Graphics;

      // Top Border
      g.DrawLine(lightPen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
      g.DrawLine(lightPen, rect.Left, rect.Top + 1, rect.Right - 1, rect.Top + 1);
      g.DrawLine(lightPen, rect.Left, rect.Top + 2, rect.Right - 1, rect.Top + 2);

      //Left Border
      g.DrawLine(lightPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
      g.DrawLine(lightPen, rect.Left + 1, rect.Top, rect.Left + 1, rect.Bottom - 1);
      g.DrawLine(lightPen, rect.Left + 2, rect.Top, rect.Left + 2, rect.Bottom - 1);

      drawFrame(g, new Rectangle(rect.Left + 9, rect.Top + 9, rect.Width - 9 - 6, 36), false);

      drawFrame(g, new Rectangle(rect.Left + 9, rect.Top + 9 + 36 + 6, rect.Width - 9 - 6, rect.Height - 9 - 36 - 6 - 6), true);
    }

    /// <summary>
    /// 跦擂MineControl腔湮苤殿隙Window腔湮苤
    /// </summary>
    public Size GetWindowClientSize(Size mineControlSize)
    {
      return new Size(mineControlSize.Width + 9 + 3 + 3 + 6, mineControlSize.Height + 9 + 2 + 36 + 2 + 6 + 3 + 4);
    }

    /// <summary>
    /// 鳳MineControl腔弇离
    /// </summary>
    /// <returns></returns>
    public Point MineControlLocation
    {
      get
      {
        return new Point(ClientRectangle.Left + 9 + 3, ClientRectangle.Top + 9 + 36 + 6 + 3);
      }
    }

    private void drawFrame(Graphics g, Rectangle rect, bool frameWidthIsThree)
    {
      #region Top Border
      g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Right - 1, rect.Top);
      g.DrawLine(darkGrayPen, rect.Left, rect.Top + 1, rect.Right - 2, rect.Top + 1);
      if (frameWidthIsThree)
        g.DrawLine(darkGrayPen, rect.Left, rect.Top + 2, rect.Right - 3, rect.Top + 2);
      #endregion

      #region Bottom Border
      g.DrawLine(lightPen, rect.Left + 1, rect.Bottom, rect.Right, rect.Bottom);
      g.DrawLine(lightPen, rect.Left + 2, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
      if (frameWidthIsThree)
        g.DrawLine(lightPen, rect.Left + 3, rect.Bottom - 2, rect.Right, rect.Bottom - 2);
      #endregion

      #region Left Border
      g.DrawLine(darkGrayPen, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
      g.DrawLine(darkGrayPen, rect.Left + 1, rect.Top, rect.Left + 1, rect.Bottom - 2);
      if (frameWidthIsThree)
        g.DrawLine(darkGrayPen, rect.Left + 2, rect.Top, rect.Left + 2, rect.Bottom - 3);
      #endregion

      #region Right Border
      g.DrawLine(lightPen, rect.Right, rect.Top + 1, rect.Right, rect.Bottom);
      g.DrawLine(lightPen, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom);
      if (frameWidthIsThree)
        g.DrawLine(lightPen, rect.Right - 2, rect.Top + 3, rect.Right - 2, rect.Bottom);
      #endregion
    }

    public override string Text
    {
      get
      {
        return "";
      }
      set
      {
      }
    }

    /// <summary>
    /// 植訧埭DLL笢腕剒猁腔訧埭
    /// </summary>
    public Stream GetResource(string fileName)
    {
      if (fileName == null || fileName.Length == 0)
        return null;

      Stream stream = null;
      Type resourceType = this.GetType();
      string resourceName = "App.Windows.XPMinesweeper.Resources." + fileName.Replace("\\", ".");
      System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(resourceType);
      if (assembly == null)
        throw new MineException("拸楊蚾婥訧埭恅璃: " + resourceType.Namespace + ".dll");
      stream = System.Reflection.Assembly.GetAssembly(resourceType).GetManifestResourceStream(resourceName);
      if (stream == null)
        throw new MineException("拸楊腕訧埭: " + fileName);
      return stream;
    }

    internal void ChangeFace(int faceID)
    {
      if (rbReset.Image != null)
      {
        rbReset.Image.Dispose();
        rbReset.Image = null;
      }
      rbReset.Image = getBitmap("Face" + faceID.ToString() + ".png", true);
    }

    public void ArrangeChildChildren()
    {
      rbReset.Left = 3 + (ClientSize.Width - 3 - rbReset.Width) / 2;
      rbReset.Top = 3 + (6 + 6 + 36 - rbReset.Height) / 2;

      pnlLeft.Left = 3 + 6 + 7;
      pnlLeft.Top = rbReset.Top;
      pnlRight.Left = ClientSize.Width - 6 - 7 - pnlRight.Width;
      pnlRight.Top = rbReset.Top;
    }

    public event EventHandler Reset;

    private void OnReset(object sender, EventArgs e)
    {
      if (Reset != null)
        Reset(this, e);
    }

    public int RemainMineCount
    {
      get
      {
        return pnlLeft.Number;
      }
      set
      {
        pnlLeft.Number = value;
      }
    }

    public int CountSecond
    {
      get
      {
        return pnlRight.Number;
      }
      set
      {
        pnlRight.Number = value;
      }
    }

    public void StartTimer()
    {
      tmrCount.Start();
    }

    public void StopTimer()
    {
      tmrCount.Stop();
    }
  }
}
