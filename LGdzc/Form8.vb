Public Class Form8


    Dim dtTable As New DataTable("Employee")    ' CREATE A DATATABLE.

    Private Sub Form8_Load(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles Me.Load
        AddRows()                ' ADD FEW BLANK ROWS TO START WITH.
    End Sub

    Private Sub AddRows()
        Dim row As DataRow
        row = dtTable.NewRow

        For iCntCol = 1 To 10
            row = dtTable.NewRow        ' ADD BLANK ROWS TO THE DATATABLE.
            dtTable.Rows.Add(row)
        Next

        With DataGridView1
            .DataSource = dtTable       ' ADD DATATABLE TO GRID. (WITH THE BLANK ROWS)

            ' JUST FOR THE LOOKS.
            .GridColor = Color.FromArgb(211, 222, 229)
            .BackgroundColor = Color.Wheat

            .RowsDefaultCellStyle.BackColor = Color.AliceBlue
            .RowsDefaultCellStyle.SelectionBackColor = Color.CornflowerBlue
            .RowsDefaultCellStyle.SelectionForeColor = Color.White
        End With
    End Sub

    ' CONTROL THE KEY STROKES.
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, _
            ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If keyData = Keys.Enter Then

            ' ON ENTER KEY, GO TO THE NEXT CELL. 
            ' WHEN THE CURSOR REACHES THE LAST COLUMN, CARRY IT ON TO THE NEXT ROW.

            If ActiveControl.Name = "DataGridView1" Then
                With DataGridView1
                    If .CurrentCell.ColumnIndex = .ColumnCount - 1 Then             ' CHECK IF ITS THE LAST COLUMN
                        .CurrentCell = .Rows(.CurrentCell.RowIndex + 1).Cells(0)    ' GO TO THE FIRST COLUMN, NEXT ROW.
                    Else
                        .CurrentCell = .Rows(.CurrentRow.Index).Cells(.CurrentCell.ColumnIndex + 1)     ' NEXT COLUMN.
                    End If
                End With

            ElseIf TypeOf ActiveControl Is DataGridViewTextBoxEditingControl Then

                ' SHOW THE COMBOBOX WHEN FOCUS IS ON A CELL CORRESPONDING TO THE "QUALIFICATION" COLUMN.
                With DataGridView1
                    If .Columns(.CurrentCell.ColumnIndex).Name = "PresentAddress" Then
                        .CurrentCell = .Rows(.CurrentRow.Index).Cells(.CurrentCell.ColumnIndex + 1)

                        ' SHOW COMBOBOX.
                        Show_Combobox(.CurrentRow.Index, .CurrentCell.ColumnIndex)
                        SendKeys.Send("{F4}")               ' DROP DOWN THE LIST.
                    Else
                        If .CurrentCell.ColumnIndex = .ColumnCount - 1 Then             ' CHECK IF ITS THE LAST COLUMN
                            .CurrentCell = .Rows(.CurrentCell.RowIndex + 1).Cells(0)    ' GO TO THE FIRST COLUMN, NEXT ROW.
                        Else
                            .CurrentCell = .Rows(.CurrentRow.Index).Cells(.CurrentCell.ColumnIndex + 1)     ' NEXT COLUMN.
                        End If
                    End If
                End With

            ElseIf ActiveControl.Name = "ComboBox1" Then
                ' HIDE THE COMBOBOX AND ASSIGN COMBO'S VALUE TO THE CELL.
                ComboBox1.Visible = False

                With DataGridView1
                    .Focus()            ' ONCE THE COMBO IS SET AS INVISIBLE, SET FOCUS BACK TO THE GRID. (IMPORTANT)
                    .Item(.CurrentCell.ColumnIndex, .CurrentRow.Index).Value = Trim(ComboBox1.Text)
                    .CurrentCell = .Rows(.CurrentRow.Index).Cells(.CurrentCell.ColumnIndex + 1)
                End With
            Else
                SendKeys.Send("{TAB}")
            End If
            Return True

        ElseIf keyData = Keys.Escape Then       ' PRESS ESCAPE TO HIDE THE COMBOBOX.
            If ActiveControl.Name = "ComboBox1" Then
                ComboBox1.Text = "" : ComboBox1.Visible = False

                With DataGridView1
                    .CurrentCell = .Rows(.CurrentCell.RowIndex).Cells(.CurrentCell.ColumnIndex)
                    .Focus()
                    Return True
                End With
            End If
        Else
            Return MyBase.ProcessCmdKey(msg, keyData)
        End If
        Return True
    End Function

    Private Sub Show_Combobox(ByVal iRowIndex As Integer, ByVal iColumnIndex As Integer)
        ' DESCRIPTION: SHOW THE COMBO BOX IN THE SELECTED CELL OF THE GRID.
        ' PARAMETERS: iRowIndex - THE ROW ID OF THE GRID.
        '             iColumnIndex - THE COLUMN ID OF THE GRID.

        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim Width As Integer = 0
        Dim height As Integer = 0

        ' GET THE ACTIVE CELL'S DIMENTIONS TO BIND THE COMBOBOX WITH IT.
        Dim rect As Rectangle
        rect = DataGridView1.GetCellDisplayRectangle(iColumnIndex, iRowIndex, False)
        x = rect.X + DataGridView1.Left
        y = rect.Y + DataGridView1.Top

        Width = rect.Width
        height = rect.Height

        With ComboBox1
            .SetBounds(x, y, Width, height)
            .Visible = True
            .Focus()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub
End Class