Imports System.Data.SqlServerCe
Imports SalesInvoice.globalVars
Public Class AddItemWindow
    Dim i As Integer = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If ITEMNAME.Text IsNot Nothing And ITEMCATEGORY.SelectedItem IsNot Nothing And UNITCOMBO.SelectedItem IsNot Nothing Then
            If ITEMCATEGORY.SelectedItem = rm.GetString("lbEditCategories") Or ITEMCATEGORY.SelectedItem = "- - - - -" Then
                ToolTip1.Show(rm.GetString("lbIncorrectCategory"), ITEMCATEGORY, 3000)
            Else
                If UNITCOMBO.SelectedItem = rm.GetString("lbEditUnits") Or UNITCOMBO.SelectedItem = "- - - - -" Then
                    ToolTip1.Show(rm.GetString("lbIncorrectCategory"), UNITCOMBO, 3000)
                Else
                    Dim cat_id = 0
                    Dim unit_id = 0
                    cmd = New SqlCeCommand("Select top 1 id from Categories where name = '" & ITEMCATEGORY.SelectedItem & "'", con)
                    If con.State = ConnectionState.Closed Then con.Open()
                    cmd.ExecuteNonQuery()

                    Using ird As SqlCeDataReader = cmd.ExecuteReader
                        While ird.Read()
                            cat_id = ird.GetValue(0)
                        End While
                    End Using
                    cmd = New SqlCeCommand("select id from Units where name = '" & UNITCOMBO.SelectedItem & "'", con)
                    If con.State = ConnectionState.Closed Then con.Open()
                    cmd.ExecuteNonQuery()

                    Using ird As SqlCeDataReader = cmd.ExecuteReader
                        While ird.Read()
                            unit_id = ird.GetValue(0)
                        End While
                    End Using



                    ' cmd = New SqlCeCommand("INSERT items(name,code,category,amount,price,service,unit) values('" & ITEMNAME.Text & "','" & ITEMEANCODE.Text & "'," & _
                    ' cat_id & "," & ITEMQUANTITY.Value & ",'" & ITEMPRICE.Text & "'," & If(CHKASSERVICE.Checked, 1, 0) & "," & unit_id & ")", con)
                    ITEMPRICE.Text = ITEMPRICE.Text.Replace(",", ".")
                    cmd = New SqlCeCommand("INSERT Items(name,price,category,unit) values('" & _
                                           ITEMNAME.Text & "'," & ITEMPRICE.Text & "," & cat_id & "," & unit_id & ")", con)
                    If con.State = ConnectionState.Closed Then con.Open()
                    cmd.ExecuteNonQuery()
                    cmd = New SqlCeCommand("select count(*) as c from Items", con)
                    Using rd As SqlCeDataReader = cmd.ExecuteReader
                        rd.Read()
                        If rd.GetValue(0).ToString IsNot "0" Then
                            ToolStripStatusLabel1.Text = rm.GetString("lbItemsInDb") & rd.GetValue(0)
                        Else
                            ToolStripStatusLabel1.Text = rm.GetString("lbItemsInDb") & "0"
                        End If
                    End Using
                    ITEMNAME.Text = Nothing
                    ITEMPRICE.Text = Nothing
                    ITEMCATEGORY.SelectedItem = "- - - - -"
                    con.Close()
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub AddItemWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ITEMNAME.Text = Nothing
    End Sub

    Private Sub AddItemWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setLanguage()

        ITEMNAME.Focus()
        ITEMCATEGORY.Items.Clear()
        UNITCOMBO.Items.Clear()
        cmd = New SqlCeCommand("select name as c from Categories", con)
        If con.State = ConnectionState.Closed Then con.Open()
        cmd.ExecuteNonQuery()
        Using rd As SqlCeDataReader = cmd.ExecuteReader
            While rd.Read()
                    ITEMCATEGORY.Items.Add(rd.GetValue(0))
            End While
        End Using
        ITEMCATEGORY.Items.Add("- - - - -")
        ITEMCATEGORY.SelectedItem = "- - - - -"
        ITEMCATEGORY.Items.Add(rm.GetString("lbEditCategories"))

        cmd = New SqlCeCommand("select name from Units", con)
        If con.State = ConnectionState.Closed Then con.Open()
        cmd.ExecuteNonQuery()
        Using rd As SqlCeDataReader = cmd.ExecuteReader
            While rd.Read()
                UNITCOMBO.Items.Add(rd.GetValue(0))
            End While
        End Using
        UNITCOMBO.Items.Add("- - - - -")
        UNITCOMBO.SelectedItem = "- - - - -"
        UNITCOMBO.Items.Add(rm.GetString("lbEditUnits"))

        cmd = New SqlCeCommand("select count(*) as c from Items", con)
        Using rd As SqlCeDataReader = cmd.ExecuteReader
            rd.Read()
            If rd.GetValue(0).ToString IsNot "0" Then
                ToolStripStatusLabel1.Text = rm.GetString("lbItemsInDb") & rd.GetValue(0)
            Else
                ToolStripStatusLabel1.Text = rm.GetString("lbItemsInDb") & "0"
            End If
        End Using

    End Sub
    Sub setLanguage()
        lbCategories.Text = rm.GetString("lbCategory")
        lbName.Text = rm.GetString("lbItemName")
        lbPrice.Text = rm.GetString("lbPrice")
        Label1.Text = rm.GetString("lbUnit")

        btnAdd.Text = rm.GetString("lbAdd")
        btnClose.Text = rm.GetString("lbClose")

        Me.Text = rm.GetString("lbAddItem")

        Me.Refresh()

    End Sub

    Private Sub AddItemWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode.ToString.ToLower = "insert" Or e.KeyCode.ToString.ToLower = "return" Then
            btnAdd.PerformClick()
        End If
    End Sub
    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ITEMCATEGORY.SelectedValueChanged
        If ITEMCATEGORY.SelectedItem = rm.GetString("lbEditCategories") Then
            If AddCategoriesWindow.ShowDialog() Then
                ITEMCATEGORY.Items.Clear()

                cmd = New SqlCeCommand("select name as c from Categories", con)
                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
                Using rd As SqlCeDataReader = cmd.ExecuteReader
                    While rd.Read()
                        ITEMCATEGORY.Items.Add(rd.GetValue(0))
                    End While
                End Using
                ITEMCATEGORY.Items.Add("- - - - -")
                ITEMCATEGORY.Items.Add(rm.GetString("lbEditCategories"))
                'ComboBox1.SelectedIndex = ComboBox1.Items.Count / ComboBox1.Items.Count
            End If
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ITEMPRICE.KeyPress
        Dim DecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or
                         Asc(e.KeyChar) = 8 Or
                         (e.KeyChar = DecimalSeparator And sender.Text.IndexOf(DecimalSeparator) = -1))
    End Sub

    Private Sub UNITCOMBO_SelectedValueChanged(sender As Object, e As EventArgs) Handles UNITCOMBO.SelectedValueChanged
        If UNITCOMBO.SelectedItem = rm.GetString("lbEditUnits") Then
            If AddUnitsWindow.ShowDialog() Then
                UNITCOMBO.Items.Clear()

                cmd = New SqlCeCommand("select name as c from Units", con)
                If con.State = ConnectionState.Closed Then con.Open()
                cmd.ExecuteNonQuery()
                Using rd As SqlCeDataReader = cmd.ExecuteReader
                    While rd.Read()
                        UNITCOMBO.Items.Add(rd.GetValue(0))
                    End While
                End Using
                UNITCOMBO.Items.Add("- - - - -")
                UNITCOMBO.Items.Add(rm.GetString("lbEditUnits"))
                'ComboBox1.SelectedIndex = ComboBox1.Items.Count / ComboBox1.Items.Count
            End If
        End If
    End Sub
End Class