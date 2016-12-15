using System;
using System.Collections.Generic;
using System.Text;

namespace TConst
{
    public enum CommandStyleEnum
    {
        cNone = 0,
        cList = 1,//请示文件列表
        cListReturn = 2,//文件列表返回
        cGetFileLength = 3,//请示文件长度
        cGetFileLengthReturn = 4,//返回文件长度
        cGetFileLengthReturnNone = 5,//返回文件长度失败
        cGetFile = 6,//请求文件
        cGetFileReturn = 7,//请求文件返回
        cGetFileReturnNone = 8,//请示文件返回失败
    }
}
