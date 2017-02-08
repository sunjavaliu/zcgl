<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form7
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TableField = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContentField = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(18, 18)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(844, 432)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ListBox1)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.TextBox1)
        Me.TabPage1.Controls.Add(Me.Button2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 28)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage1.Size = New System.Drawing.Size(836, 400)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "第一步：选择要导入的文件"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 18
        Me.ListBox1.Location = New System.Drawing.Point(27, 123)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(652, 184)
        Me.ListBox1.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(690, 314)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(112, 34)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "下一步"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(251, 18)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "请选择要导入的Excel/CSV文件"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(27, 64)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(652, 28)
        Me.TextBox1.TabIndex = 3
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(690, 62)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(112, 34)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "选择文件"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Button4)
        Me.TabPage2.Controls.Add(Me.ListBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 28)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage2.Size = New System.Drawing.Size(836, 400)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "第二步：选择对应表"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(668, 302)
        Me.Button4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(112, 34)
        Me.Button4.TabIndex = 1
        Me.Button4.Text = "下一步"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 18
        Me.ListBox2.Items.AddRange(New Object() {"部门", "资产", "类别", "通用（计量单位、职务等）"})
        Me.ListBox2.Location = New System.Drawing.Point(9, 9)
        Me.ListBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(284, 274)
        Me.ListBox2.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Button6)
        Me.TabPage3.Controls.Add(Me.DataGridView1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 28)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage3.Size = New System.Drawing.Size(836, 400)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "第三步：对应字段"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(658, 351)
        Me.Button6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(112, 34)
        Me.Button6.TabIndex = 1
        Me.Button6.Text = "下一步 "
        Me.Button6.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TableField, Me.ContentField})
        Me.DataGridView1.Location = New System.Drawing.Point(10, 12)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(807, 330)
        Me.DataGridView1.TabIndex = 0
        '
        'TableField
        '
        Me.TableField.HeaderText = "表字段"
        Me.TableField.Name = "TableField"
        '
        'ContentField
        '
        Me.ContentField.HeaderText = "内容字段"
        Me.ContentField.Name = "ContentField"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Button8)
        Me.TabPage4.Controls.Add(Me.Button7)
        Me.TabPage4.Controls.Add(Me.DataGridView2)
        Me.TabPage4.Location = New System.Drawing.Point(4, 28)
        Me.TabPage4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage4.Size = New System.Drawing.Size(836, 400)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "第四步：导入数据"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(694, 350)
        Me.Button8.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(112, 33)
        Me.Button8.TabIndex = 2
        Me.Button8.Text = "导入"
        Me.Button8.UseVisualStyleBackColor = True
        Me.Button8.Visible = False
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(573, 348)
        Me.Button7.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(112, 34)
        Me.Button7.TabIndex = 1
        Me.Button7.Text = "浏览"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(9, 9)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowTemplate.Height = 23
        Me.DataGridView2.Size = New System.Drawing.Size(814, 321)
        Me.DataGridView2.TabIndex = 0
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(508, 480)
        Me.Button3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(112, 34)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "返回"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(312, 480)
        Me.Button5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(112, 34)
        Me.Button5.TabIndex = 3
        Me.Button5.Text = "Button5"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Form7
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1006, 610)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TabControl1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "Form7"
        Me.Text = "Form7"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TableField As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContentField As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
End Class
