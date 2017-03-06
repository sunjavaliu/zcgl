﻿Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Security.Cryptography
Module MD5ENDOD

    ''' <summary>
    ''' MD5(16位加密)
    ''' </summary>
    ''' <param name="ConvertString">需要加密的字符串</param>
    ''' <returns>MD5加密后的字符串</returns>
    Public Function GetMd5Str(ConvertString As String) As String
        Dim md5Pwd As String = String.Empty

        '使用加密服务提供程序
        Dim md5 As New MD5CryptoServiceProvider()

        '将指定的字节子数组的每个元素的数值转换为它的等效十六进制字符串表示形式。
        md5Pwd = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.[Default].GetBytes(ConvertString)), 4, 8)

        md5Pwd = md5Pwd.Replace("-", "")

        Return md5Pwd
    End Function

    ''' <summary>
    ''' MD5(32位加密)
    ''' </summary>
    ''' <param name="str">需要加密的字符串</param>
    ''' <returns>MD5加密后的字符串</returns>
    Public Function GetMd5HashStr(str As String) As String
        Dim pwd As String = String.Empty

        '实例化一个md5对像
        Dim md5__1 As MD5 = MD5.Create()

        ' 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
        Dim s As Byte() = md5__1.ComputeHash(Encoding.UTF8.GetBytes(str))

        ' 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
        For i As Integer = 0 To s.Length - 1
            ' 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
            pwd = pwd & s(i).ToString("X")
        Next

        Return pwd
    End Function
End Module
