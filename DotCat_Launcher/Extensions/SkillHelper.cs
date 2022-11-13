using DotCat_Launcher.Common.Modules;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DotCat_Launcher.Extensions
{
    public class MCSkillHelper
    {
        private static Bitmap LoadImage(string path)
        {
            //load img into byte
            if (!File.Exists(path))
                throw new Exception("There is no file!");
            FileStream fs = File.OpenRead(path);
            int fileLength = (int)fs.Length;
            byte[] _image = new byte[fileLength];
            fs.Read(_image, 0, fileLength);
            System.Drawing.Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            Bitmap image = new Bitmap(result);
            return image;
        }
        /// <summary>
        /// get Bitmap[] from path
        /// </summary>
        /// <param name="path">Image Path</param>
        /// <returns></returns>
        public static Bitmap[] GetImageCute(string path)
        {
            Bitmap image = LoadImage(path);
            int rowNum = 16;
            int colNum = 16;
            int bitmapNum = rowNum * colNum;
            Bitmap[] bitmaps = new Bitmap[bitmapNum];
            int width = 64;
            int height = 64;
            int perWidth = width / colNum;
            int perHeight = height / rowNum;
            int srcImageX = 0;
            int srcImageY = 0;
            for (int rowIndex = 0; rowIndex < rowNum; rowIndex++)
            {
                for (int colIndex = 0; colIndex < colNum; colIndex++)
                {

                }
            }
            return bitmaps;
        }
    }
}
