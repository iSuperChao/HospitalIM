using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ClientView
{
    /// <summary>
    /// 上下文
    /// </summary>
    public class Functions
    {

        
        //===============================================获取文件或路径大小=========================
 /// <summary>
 /// 图片黑白化处理
 /// </summary>
 /// <param name="image"></param>
 /// <returns></returns>
        public static Bitmap WhiteAndBlack(System.Drawing.Bitmap image)
        {
            if (image == null)
                return image;
            int Height = image.Height;
            int Width = image.Width;
            Bitmap newBitmap = new Bitmap(Width, Height);
            Bitmap oldBitmap = (Bitmap)image;
            try
            {
                Color pixel;
                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                    {
                        pixel = oldBitmap.GetPixel(x, y);
                        int r, g, b, Result = 0;
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        //实例程序以加权平均值法产生黑白图像、
                        int iType = 2;
                        switch (iType)
                        {
                            case 0://平均值法
                                Result = ((r + g + b) / 3);
                                break;
                            case 1://最大值法
                                Result = r > g ? r : g;
                                Result = Result > b ? Result : b;
                                break;
                            case 2://加权平均值法
                                Result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
                                break;
                        }
                        newBitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                    }            
                return newBitmap;            
            }catch(Exception e){
                return newBitmap;  
            }
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn) 
        {
            MemoryStream ms = new MemoryStream();
            if (imageIn == null)
                return new byte[ms.Length];
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            byte[] BPicture = new byte[ms.Length];
            BPicture = ms.GetBuffer();
            return BPicture;
        }

        public static Image byteArrayToImage(byte[] byteArrayIn) 
        {
            if (byteArrayIn.Length == 0)
                return null;
            System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }

        public static Image MakeTransparentGif(Image bitmap, Color color)
        {
            byte R = color.R;
            byte G = color.G;
            byte B = color.B;
            MemoryStream fin = new MemoryStream();
            bitmap.Save(fin, System.Drawing.Imaging.ImageFormat.Gif);

            MemoryStream fout = new MemoryStream((int)fin.Length);
            int count = 0;
            byte[] buf = new byte[256];
            byte transparentIdx = 0;
            fin.Seek(0, SeekOrigin.Begin);
            //header   
            count = fin.Read(buf, 0, 13);
            if ((buf[0] != 71) || (buf[1] != 73) || (buf[2] != 70)) return null; //GIF   

            fout.Write(buf, 0, 13);

            int i = 0;
            if ((buf[10] & 0x80) > 0)
            {
                i = 1 << ((buf[10] & 7) + 1) == 256 ? 256 : 0;
            }

            for (; i != 0; i--)
            {
                fin.Read(buf, 0, 3);
                if ((buf[0] == R) && (buf[1] == G) && (buf[2] == B))
                {
                    transparentIdx = (byte)(256 - i);
                }
                fout.Write(buf, 0, 3);
            }

            bool gcePresent = false;
            while (true)
            {
                fin.Read(buf, 0, 1);
                fout.Write(buf, 0, 1);
                if (buf[0] != 0x21) break;
                fin.Read(buf, 0, 1);
                fout.Write(buf, 0, 1);
                gcePresent = (buf[0] == 0xf9);
                while (true)
                {
                    fin.Read(buf, 0, 1);
                    fout.Write(buf, 0, 1);
                    if (buf[0] == 0) break;
                    count = buf[0];
                    if (fin.Read(buf, 0, count) != count) return null;
                    if (gcePresent)
                    {
                        if (count == 4)
                        {
                            buf[0] |= 0x01;
                            buf[3] = transparentIdx;
                        }
                    }
                    fout.Write(buf, 0, count);
                }
            }
            while (count > 0)
            {
                count = fin.Read(buf, 0, 1);
                fout.Write(buf, 0, 1);
            }
            fin.Close();
            fout.Flush();

            return new Bitmap(fout);
        }

        //===============================================获取文件或路径大小=========================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static long GetDirectoryLength(string dirPath, ref int filesnumb)
        {
            long len = 0;
            //判断该路径是否存在（是否为文件夹） 
            if (!Directory.Exists(dirPath))
            {
                //查询文件的大小 
                len = FileSize(dirPath);
            }
            else
            {
                //定义一个DirectoryInfo对象 
                DirectoryInfo di = new DirectoryInfo(dirPath);

                //通过GetFiles方法，获取di目录中的所有文件的大小 
                foreach (FileInfo fi in di.GetFiles())
                {
                    len += fi.Length;
                    filesnumb += 1;
                }
                //获取di中所有的文件夹，并存到一个新的对象数组中，以进行递归 
                DirectoryInfo[] dis = di.GetDirectories();
                if (dis.Length > 0)
                {
                    for (int i = 0; i < dis.Length; i++)
                    {
                        len += GetDirectoryLength(dis[i].FullName,ref filesnumb);
                    }
                }
            }
            return len;
        }

        //所给路径中所对应的文件大小 
        public static long FileSize(string filePath)
        {
            //定义一个FileInfo对象，是指与filePath所指向的文件相关联，以获取其大小 
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }



    }
}