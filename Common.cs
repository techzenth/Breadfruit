using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kanb
{
    public static class Common
    {
        /// <summary>
        ///  Add all the common code functions necessary for the processes
        /// </summary>
        public static void testFunc()
        {
            System.Windows.Forms.MessageBox.Show("Test");
        }
       public static bool listboxIsValid(object sender)
        {
            ComboBox cbo = sender as ComboBox;
            bool valid = cbo.TabIndex == 0 || cbo.SelectedIndex > -1;

            return valid;
        }
       public static bool textboxIsValid(object sender)
        {
            TextBox txt = sender as TextBox;
            bool valid = txt.TabIndex == 0 || txt.Text.Length > 0;

            return valid;
        }
       public static Bitmap Grayscale(Bitmap Bmp)
       {
           int rgb;
           Color c;

           for (int y = 0; y < Bmp.Height; y++)
           {
               for (int x = 0; x < Bmp.Width; x++)
               {
                   c = Bmp.GetPixel(x, y);
                   rgb = (int)((c.R + c.G + c.B) / 3);
                   Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
               }
           }
           return Bmp;
       }

       public static string hashPassword(string password)
       {
           UnicodeEncoding uEncode = new UnicodeEncoding();
           byte[] bytPassword = uEncode.GetBytes(password);
           SHA1Managed sha = new SHA1Managed();
           byte[] hash = sha.ComputeHash(bytPassword);
           return Convert.ToBase64String(hash);
       }
    }
}
