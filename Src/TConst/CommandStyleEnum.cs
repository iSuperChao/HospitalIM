using System;
using System.Collections.Generic;
using System.Text;

namespace TConst
{
    public enum CommandStyleEnum
    {
        cNone = 0,
        cList = 1,//��ʾ�ļ��б�
        cListReturn = 2,//�ļ��б���
        cGetFileLength = 3,//��ʾ�ļ�����
        cGetFileLengthReturn = 4,//�����ļ�����
        cGetFileLengthReturnNone = 5,//�����ļ�����ʧ��
        cGetFile = 6,//�����ļ�
        cGetFileReturn = 7,//�����ļ�����
        cGetFileReturnNone = 8,//��ʾ�ļ�����ʧ��
    }
}
