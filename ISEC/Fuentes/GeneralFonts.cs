using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ISEC.ITextSHARP
{
    public class GeneralFonts
    {
        public static Font fontTitleBold()
        {
            return new Font(FontFactory.GetFont("Arial Narrow", BaseFont.IDENTITY_H, false, 12, Font.BOLD));
        }
        public static Font fontTitleNormal()
        {
            var f = new Font(FontFactory.GetFont("Arial Narrow", BaseFont.IDENTITY_H, false, 12, Font.NORMAL));
            f.SetColor(255,255,255);
            return f;
        }
        public static Font fontTitleBold10()
        {
            return new Font(FontFactory.GetFont("Arial Narrow", BaseFont.IDENTITY_H, false, 10, Font.BOLD));
        }
    }
}
