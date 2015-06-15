using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace gbd.Dominion.Tools
{
  public static class ProcessExtensions
  {
    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern IntPtr SetFocus(HandleRef hWnd);


    public static void SetFocus(this Process p)
    {
      IntPtr hWnd = p.Handle;
      SetFocus(new HandleRef(null, hWnd));

    }
  }
}
