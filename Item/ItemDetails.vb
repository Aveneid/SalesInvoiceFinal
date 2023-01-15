Imports SalesInvoice.Utils
Imports System.Data.SqlServerCe
Public Class ItemDetails
    Dim AitemCode As String
    Dim AitemPrice As Double
    Dim AitemName As String = ""
    Dim categoryID As Integer = 0
    Sub setlanguage()
        lbCategory.Text = Globals.resManager.GetString("lbCategory")
        lbCode.Text = Globals.resManager.GetString("lbCode")
        lbName.Text = Globals.resManager.GetString("lbItemName")
        lbPrice.Text = Globals.resManager.GetString("lbPrice")
        lbUnit.Text = Globals.resManager.GetString("lbUnit")
        btnClose.Text = Globals.resManager.GetString("lbClose")
        lbCategoryEdit.Text = Globals.resManager.GetString("lbEdit")
        Me.Text = Globals.resManager.GetString("lbItemDetails")
    End Sub
    Private Sub ItemDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setlanguage()

        itemUnit.Items.Clear()
        itemCategory.Items.Clear()
        itemUnit.Enabled = False
        itemCategory.Enabled = False
        If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
        DatabaseHelper.cmd = New SqlCeCommand("select name from Units", DatabaseHelper.con)
        DatabaseHelper.cmd.ExecuteNonQuery()
        Using rd As SqlCeDataReader = DatabaseHelper.cmd.ExecuteReader
            While rd.Read()
                itemUnit.Items.Add(rd.GetValue(0))
            End While
        End Using

        DatabaseHelper.cmd = New SqlCeCommand("select name from Categories", DatabaseHelper.con)
        DatabaseHelper.cmd.ExecuteNonQuery()
        Using rd As SqlCeDataReader = DatabaseHelper.cmd.ExecuteReader
            While rd.Read()
                itemCategory.Items.Add(rd.GetValue(0))
            End While
        End Using

        DatabaseHelper.cmd = New SqlCeCommand("select Items.name,Items.id,Items.price,Categories.name,Units.name " &
                               "from items inner join Categories on category = Categories.id " &
                               "inner join units on unit = Units.id where Items.id = '" & AitemCode & "'", DatabaseHelper.con)

        If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
        DatabaseHelper.cmd.ExecuteNonQuery()
        Using rd As SqlCeDataReader = DatabaseHelper.cmd.ExecuteReader()
            While rd.Read()
                itemName.Text = rd.GetValue(0)
                AitemName = rd.GetValue(0)
                itemCode.Text = rd.GetValue(1)

                itemPrice.Text = rd.GetValue(2)
                AitemPrice = rd.GetValue(2)

                itemCategory.SelectedItem = rd.GetValue(3)
                itemUnit.SelectedItem = rd.GetValue(4)


            End While
        End Using

        itemPrice.Select()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If Not AitemPrice = itemPrice.Text Then
            If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
            DatabaseHelper.cmd = New SqlCeCommand("update items set price = " & itemPrice.Text &
                              " WHERE id = '" & itemCode.Text & "'", DatabaseHelper.con)
            DatabaseHelper.cmd.ExecuteNonQuery()
        End If
        If categoryID > 0 Then
            If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
            DatabaseHelper.cmd = New SqlCeCommand("update items set category = " & categoryID &
                              " WHERE id = '" & itemCode.Text & "'", DatabaseHelper.con)
            DatabaseHelper.cmd.ExecuteNonQuery()
        End If
        If AitemName <> itemName.Text Then
            If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
            DatabaseHelper.cmd = New SqlCeCommand("update items set name = " & itemName.Text &
                              " WHERE id = '" & itemCode.Text & "'", DatabaseHelper.con)
            DatabaseHelper.cmd.ExecuteNonQuery()

        End If
        MainWindow.updateDataView()
        Me.Close()
    End Sub

    Private Sub itemPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles itemPrice.KeyPress
        Dim DecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or
                         Asc(e.KeyChar) = 8 Or
                         (e.KeyChar = DecimalSeparator And sender.Text.IndexOf(DecimalSeparator) = -1))
    End Sub

    Public Sub New(ByVal iCode As String)
        ' This call is required by the designer.
        InitializeComponent()
        AitemCode = iCode
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ItemDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode.ToString.ToLower = "insert" Or e.KeyCode.ToString.ToLower = "return" Then
            btnClose.PerformClick()
        End If
    End Sub

    Private Sub categoryEdit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbCategoryEdit.LinkClicked
        Dim editCat = New EditCategory()
        If DialogResult.OK = editCat.ShowDialog() Then
            If DatabaseHelper.con.State = ConnectionState.Closed Then DatabaseHelper.con.Open()
            Dim cmd = New SqlCeCommand("SELECT id FROM categories WHERE name ='" & editCat.cbCategories.SelectedItem & "'", DatabaseHelper.con)
            DatabaseHelper.cmd.ExecuteNonQuery()
            Dim rd As SqlCeDataReader = DatabaseHelper.cmd.ExecuteReader
            rd.Read()
            categoryID = rd.GetValue(0)
            itemCategory.SelectedItem = editCat.cbCategories.SelectedItem
            DatabaseHelper.con.Close()
        End If
    End Sub
End Class