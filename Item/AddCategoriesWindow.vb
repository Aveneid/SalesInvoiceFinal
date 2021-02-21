Imports SalesInvoice.globalVars
Imports System.Data.SqlServerCe
Public Class AddCategoriesWindow
    Public Sub refreshListBox()
        ListBox1.Items.Clear()
        cmd = New SqlCeCommand("select name as c from Categories", con)
        If con.State = ConnectionState.Closed Then con.Open()
        cmd.ExecuteNonQuery()
        Using rd As SqlCeDataReader = cmd.ExecuteReader
            While rd.Read()
                ListBox1.Items.Add(rd.GetValue(0))
            End While
        End Using
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If TextBox1.Text IsNot Nothing Then
            cmd = New SqlCeCommand("SELECT name FROM  Categories WHERE name = '" & TextBox1.Text & "';", con)
            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()
            Dim d As SqlCeDataReader = cmd.ExecuteReader
            If Not d.Read() Then
                cmd = New SqlCeCommand("INSERT Categories(name) values('" & TextBox1.Text & "');", con)
                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
                refreshListBox()
                TextBox1.Text = ""
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        refreshListBox()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If ListBox1.SelectedItem IsNot Nothing Then
            'MsgBox(ListBox1.SelectedItem & vbNewLine & ListBox1.SelectedValue)
            cmd = New SqlCeCommand("SELECT * Categories where name = '" & ListBox1.SelectedItem.ToString & "'", con)
            If con.State = ConnectionState.Closed Then con.Open()
            Dim d As SqlCeDataReader = cmd.ExecuteReader
            If Not d.Read() Then
                cmd = New SqlCeCommand("DELETE FROM Categories WHERE name = '" & ListBox1.SelectedItem.ToString & "'", con)
                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
            Else
                MsgBox(rm.GetString("msgCategoryDelError"))

            End If
            refreshListBox()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode.ToString.ToLower = "return" Then
            btnAdd.PerformClick()
        End If
    End Sub

    Private Sub AddCategoriesWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refreshListBox()
        setLanguage()
        TextBox1.Focus()
    End Sub

    Private Sub AddCategoriesWindow_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
        Me.ResetText()

    End Sub
    Sub setLanguage()
        btnAdd.Text = rm.GetString("lbAdd")
        btnDelete.Text = rm.GetString("lbDelete")
        btnOk.Text = rm.GetString("lbOk")
        lbCategories.Text = rm.GetString("lbCategories")
        Me.Text = rm.GetString("lbAddCategory")

    End Sub
End Class