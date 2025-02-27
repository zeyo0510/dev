﻿using System;
using System.Collections.Generic;
/************************************************/
namespace App.Windows.DotnetFrameworkViewer.Core
{
  partial class DotnetFramework
  {
    private static List<DotnetFramework> items = null;
    /************************************************/
    public static DotnetFramework[] Items
    {
      get
      {
        if (DotnetFramework.items == null)
        {
          DotnetFramework.items = new List<DotnetFramework>();
          /************************************************/
          DotnetFramework.items.Add(DotnetFramework.V20      );
          DotnetFramework.items.Add(DotnetFramework.V30      );
          DotnetFramework.items.Add(DotnetFramework.V35      );
          DotnetFramework.items.Add(DotnetFramework.V40Client);
          DotnetFramework.items.Add(DotnetFramework.V40Full  );
          DotnetFramework.items.Add(DotnetFramework.V4xClient);
          DotnetFramework.items.Add(DotnetFramework.V4xFull  );
        }
        /************************************************/
        return DotnetFramework.items.ToArray();
      }
    }
  }
}