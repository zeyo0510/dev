using System;
/************************************************/
namespace App.Windows.XPMinesweeper.Controls
{
  [Serializable]
  public class MineException : Exception
  {
    public MineException() : base()
    {
      // do nothing...
    }
    /************************************************/
    public MineException(string message) : base(message)
    {
      // do nothing...
    }
    /************************************************/
    public MineException(string message, Exception innerException) : base(message, innerException)
    {
      // do nothing...
    }
  }
}