using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows;
using System.Data;

namespace QQControls
{
    public class Monitor : System.MarshalByRefObject
    {
        #region 常量
        private const uint MOUSEEVENTF_MOVE       = 0x0001; //系统消息：鼠标移动
		private const uint MOUSEEVENTF_LEFTDOWN   = 0x0002; //系统消息：左键按下
		private const uint MOUSEEVENTF_LEFTUP     = 0x0004; //系统消息：左键放开
		private const uint MOUSEEVENTF_RIGHTDOWN  = 0x0008; //系统消息：右键按下
		private const uint MOUSEEVENTF_RIGHTUP    = 0x0010; //系统消息：右键放开
		private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020; //系统消息：中间健按下
		private const uint MOUSEEVENTF_MIDDLEUP   = 0x0040; //系统消息：中间健放开
		private const uint MOUSEEVENTF_WHEEL      = 0x0800; //系统消息：滚动滚轮
		private const uint MOUSEEVENTF_ABSOLUTE   = 0x8000; //指定鼠标坐标系统中的一个绝对位置
		private const uint KEYEVENTF_EXTENDEDKEY  = 0x0001; //一个扩展键
		private const uint KEYEVENTF_KEYUP        = 0x0002; //模拟松开一个键
		private const uint INPUT_MOUSE			  = 0;      //模拟鼠标事件
		private const uint INPUT_KEYBOARD		  = 1;      //模拟键盘事件
		private static byte[] PreviousBitmapBytes = null;
        private Button bt = new Button();
        #endregion

        #region 构造函数
        public Monitor() 
		{

        }
        #endregion

        #region Win32API方法包装
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt
        (
            IntPtr hdcDest, //指向目标设备环境的句柄
            int nXDest, //指定目标矩形区域克上角的X轴逻辑坐标
            int nYDest, //指定目标矩形区域左上角的Y轴逻辑坐标
            int nWidth, //指定源和目标矩形区域的逻辑宽度
            int nHeight, //指定源和目标矩形区域的逻辑高度
            IntPtr hdcSrc, //指向源设备环境句柄
            int nXSrc, //指定源矩形区域左上角的X轴逻辑坐标
            int nYSrc, //指定源矩形区域左上角的Y轴逻辑坐标
            System.Int32 dwRop //指定光栅操作代码。这些代码将定义源矩形区域的颜色数据，如何与目标矩形区域的颜色数据组合以完成最后的颜色
        );

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        private static extern uint SendInput
        (
            uint nInputs,
            ref INPUT input,
            int cbSize
        );

        [DllImport("user32.dll")]
        private static extern void SetCursorPos(int x, int y);
        #endregion

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll ")]
        private static extern IntPtr CreateDC(
        string lpszDriver,   //   驱动名称 
        string lpszDevice,   //   设备名称 
        string lpszOutput,   //   无用，可以设定位 "NULL " 
        IntPtr lpInitData   //   任意的打印机数据 
        ); 

        #region Win32结构包装
        struct MOUSE_INPUT
        {
            public uint dx;
            public uint dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public uint dwExtraInfo;
        }

        struct KEYBD_INPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public uint dwExtraInfo;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct INPUT
        {
            [FieldOffset(0)]
            public uint type;

            [FieldOffset(4)]
            public MOUSE_INPUT mi;

            [FieldOffset(4)]
            public KEYBD_INPUT ki;
        }


        #endregion

        #region 获得屏幕的大小
        public Size GetDesktopBitmapSize()
		{
            //Rectangle rect = System.Windows.Forms.SystemInformation.VirtualScreen;
            //return new Size(rect.Width, rect.Height);
            return new Size(GetSystemMetrics(0), GetSystemMetrics(1));
        }
        #endregion

        #region 模拟鼠标、键盘操作
        public void PressOrReleaseMouseButton(bool Press, bool Left, int X, int Y)
		{
			INPUT input = new INPUT();

			input.type           = INPUT_MOUSE;
			input.mi.dx          = (uint) X;
			input.mi.dy          = (uint) Y;
			input.mi.mouseData   = 0;
			input.mi.dwFlags     = 0;
			input.mi.time        = 0;
			input.mi.dwExtraInfo = 0;

			if (Left)
			{
				input.mi.dwFlags = Press ? MOUSEEVENTF_LEFTDOWN : MOUSEEVENTF_LEFTUP;
			}
			else
			{
				input.mi.dwFlags = Press ? MOUSEEVENTF_RIGHTDOWN : MOUSEEVENTF_RIGHTUP;
			}

			SendInput(1, ref input, Marshal.SizeOf(input));
		}

        public void MoveMouse(int x, int y)
        {
            SetCursorPos(x, y);
        }

		public void SendKeystroke(byte VirtualKeyCode, byte ScanCode, bool KeyDown, bool ExtendedKey)//发送键盘事件
		{
			INPUT input = new INPUT();

			input.type           = INPUT_KEYBOARD;
			input.ki.wVk         = VirtualKeyCode;
			input.ki.wScan       = ScanCode;
			input.ki.dwExtraInfo = 0;
			input.ki.time        = 0;

			if (!KeyDown)
			{
				input.ki.dwFlags |= KEYEVENTF_KEYUP;
			}

			if (ExtendedKey)
			{
				input.ki.dwFlags |= KEYEVENTF_EXTENDEDKEY;
			}

			SendInput(1, ref input, Marshal.SizeOf(input));
        }
        #endregion

        #region 获得屏幕截图
        private Bitmap GetDesktopBitmap()
		{
            Size DesktopBitmapSize = GetDesktopBitmapSize();// GetDesktopBitmapSize();
            Graphics Graphic = Graphics.FromHwnd(GetDesktopWindow());//从窗口的指定句柄创建新的 Graphics 对象
            
            Bitmap MemImage = new Bitmap(DesktopBitmapSize.Width, DesktopBitmapSize.Height, Graphic);//生成图像
            Graphics MemGraphic = Graphics.FromImage(MemImage);//从指定的 Image 对象创建新 Graphics 对象
            IntPtr dc1 = Graphic.GetHdc();//获取与此 Graphics 对象关联的设备上下文的句柄
            IntPtr dc2 = MemGraphic.GetHdc();
            BitBlt(dc2, 0, 0, DesktopBitmapSize.Width, DesktopBitmapSize.Height, dc1, 0, 0, 0xCC0020);
            Graphic.ReleaseHdc(dc1);//释放通过以前对此 Graphics 对象的 GetHdc 方法的调用获得的设备上下文句柄
            MemGraphic.ReleaseHdc(dc2);
            Graphic.Dispose();
            MemGraphic.Dispose();
            return MemImage;
        }
        #endregion

        #region 判断两个图的二进制数组是否相同
        private static bool BitmapsAreEqual(ref byte[] a, ref byte[] b)
		{
			bool Result = (a != null && b != null && a.Length == b.Length);

			if (Result)
			{
				for (int i = 0; Result && i < a.Length; i++)
				{
					if (a[i] != b[i])
					{
						Result = false;
					}
				}
			}
			return Result;
        }
        #endregion

        #region 获得图像二进制的数组
        public byte[] GetDesktopBitmapBytes()
		{
			Bitmap CurrentBitmap = GetDesktopBitmap();
			MemoryStream MS = new MemoryStream();
			CurrentBitmap.Save(MS, ImageFormat.Jpeg);//将图片写入流
			CurrentBitmap.Dispose();
			MS.Seek(0, SeekOrigin.Begin);
			byte[] CurrentBitmapBytes = new byte[MS.Length];
			int NumBytesToRead = (int) MS.Length;
			int NumBytesRead = 0;

			while (NumBytesToRead > 0) 
			{
				int n = MS.Read(CurrentBitmapBytes, NumBytesRead, NumBytesToRead);
				if (n == 0)
				{
					break;
				}
				NumBytesRead   += n;
				NumBytesToRead -= n;
			}
			MS.Close();

			byte[] Result = new byte[0];

			if (!BitmapsAreEqual(ref CurrentBitmapBytes, ref PreviousBitmapBytes))
			{
				Result = CurrentBitmapBytes;
				PreviousBitmapBytes = CurrentBitmapBytes;
			}
			return Result;
        }
        #endregion
    }
}
