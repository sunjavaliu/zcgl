Imports System.Security.Cryptography

Imports System
Imports System.Net
Imports System.Data
Imports System.Drawing
Imports System.Net.Sockets
Imports System.ComponentModel
Public Class LoginForm1

    ' TODO:  插入代码，以使用提供的用户名和密码执行自定义的身份验证
    ' (请参见 http://go.microsoft.com/fwlink/?LinkId=35339)。 
    ' 随后自定义主体可附加到当前线程的主体，如下所示: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' 其中 CustomPrincipal 是用于执行身份验证的 IPrincipal 实现。
    ' 随后，My.User 将返回 CustomPrincipal 对象中封装的标识信息
    ' 如用户名、显示名等

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim sql, username, password As String
        Dim updateCount As Integer


        'Dim HostName As String
        Dim HostIP As String   '定义主机IP地址集
        HostIP = ""
        Dim addrlist() As IPAddress = Dns.GetHostEntry(Dns.GetHostName).AddressList
        For j = 0 To addrlist.Length - 1
            HostIP &= addrlist(j).ToString & "-"
        Next


        username = UsernameTextBox.Text
        LoginUser = username
        password = PasswordTextBox.Text
        sql = "select * from user where username='" + username + "' and password='" + GetMd5HashStr(password) + "'"

        Dim querysql As LiuDataAdapter = New LiuDataAdapter(sql, CONN_STR)
        updateCount = querysql.GetSelectCount(sql)
        If updateCount > 0 Then
            'MsgBox("【" + xm + "】下有" + CStr(updateCount) + "台设备,不能修改姓名信息！", MsgBoxStyle.OkOnly + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, "提示")
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
            sql = "insert into log(user,status,ip) values('" + LoginUser + "','成功','" + HostIP + "')"
        Else
            MsgBox("登录失败！")
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Dispose()
            sql = "insert into log(user,status,ip) values('" + LoginUser + "','失败','" + HostIP + "')"
        End If
        Dim sd As LiuDataAdapter = New LiuDataAdapter
        sd.ExecuteNonQuery(sql)

    End Sub

 

    Private Sub LoginForm1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = My.Application.Info.Title + "--->登陆"
    End Sub
End Class
