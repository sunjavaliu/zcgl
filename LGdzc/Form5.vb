Public Class Form5

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
 
        Dim password As String
        Dim oldpassword As String
        Dim newpassword As String
        Dim newpassword2 As String
        Dim sql As String

        oldpassword = TextBox1.Text
        newpassword = TextBox2.Text
        newpassword2 = TextBox3.Text

        If newpassword = newpassword2 Then
            Dim sd As LiuDataAdapter = New LiuDataAdapter
            sql = "update user set password='" + GetMd5HashStr(newpassword) + "' where username='" + LoginUser + "' and password= '" + GetMd5HashStr(oldpassword) + "'"
            If sd.ExecuteNonQuery(sql) > 0 Then
                MsgBox("密码修改成功！", MsgBoxStyle.OkOnly, "提示")
            End If
        Else
            MsgBox("新设置的密码不一致！", MsgBoxStyle.Exclamation, "警告")
        End If

    End Sub
End Class