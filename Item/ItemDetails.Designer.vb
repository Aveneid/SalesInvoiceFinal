<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemDetails
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemDetails))
        Me.lbPrice = New System.Windows.Forms.Label()
        Me.itemPrice = New System.Windows.Forms.TextBox()
        Me.itemUnit = New System.Windows.Forms.ComboBox()
        Me.lbUnit = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.itemCategory = New System.Windows.Forms.ComboBox()
        Me.lbCategory = New System.Windows.Forms.Label()
        Me.itemCode = New System.Windows.Forms.TextBox()
        Me.lbCode = New System.Windows.Forms.Label()
        Me.itemName = New System.Windows.Forms.TextBox()
        Me.lbName = New System.Windows.Forms.Label()
        Me.lbCategoryEdit = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'lbPrice
        '
        resources.ApplyResources(Me.lbPrice, "lbPrice")
        Me.lbPrice.Name = "lbPrice"
        '
        'itemPrice
        '
        resources.ApplyResources(Me.itemPrice, "itemPrice")
        Me.itemPrice.Name = "itemPrice"
        '
        'itemUnit
        '
        Me.itemUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.itemUnit.FormattingEnabled = True
        resources.ApplyResources(Me.itemUnit, "itemUnit")
        Me.itemUnit.Name = "itemUnit"
        '
        'lbUnit
        '
        resources.ApplyResources(Me.lbUnit, "lbUnit")
        Me.lbUnit.Name = "lbUnit"
        '
        'btnClose
        '
        resources.ApplyResources(Me.btnClose, "btnClose")
        Me.btnClose.Name = "btnClose"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'itemCategory
        '
        Me.itemCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.itemCategory.FormattingEnabled = True
        resources.ApplyResources(Me.itemCategory, "itemCategory")
        Me.itemCategory.Name = "itemCategory"
        '
        'lbCategory
        '
        resources.ApplyResources(Me.lbCategory, "lbCategory")
        Me.lbCategory.Name = "lbCategory"
        '
        'itemCode
        '
        resources.ApplyResources(Me.itemCode, "itemCode")
        Me.itemCode.Name = "itemCode"
        Me.itemCode.ReadOnly = True
        '
        'lbCode
        '
        resources.ApplyResources(Me.lbCode, "lbCode")
        Me.lbCode.Name = "lbCode"
        '
        'itemName
        '
        resources.ApplyResources(Me.itemName, "itemName")
        Me.itemName.Name = "itemName"
        '
        'lbName
        '
        resources.ApplyResources(Me.lbName, "lbName")
        Me.lbName.Name = "lbName"
        '
        'lbCategoryEdit
        '
        resources.ApplyResources(Me.lbCategoryEdit, "lbCategoryEdit")
        Me.lbCategoryEdit.Name = "lbCategoryEdit"
        Me.lbCategoryEdit.TabStop = True
        '
        'ItemDetails
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lbCategoryEdit)
        Me.Controls.Add(Me.lbPrice)
        Me.Controls.Add(Me.itemPrice)
        Me.Controls.Add(Me.itemUnit)
        Me.Controls.Add(Me.lbUnit)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.itemCategory)
        Me.Controls.Add(Me.lbCategory)
        Me.Controls.Add(Me.itemCode)
        Me.Controls.Add(Me.lbCode)
        Me.Controls.Add(Me.itemName)
        Me.Controls.Add(Me.lbName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ItemDetails"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbPrice As System.Windows.Forms.Label
    Friend WithEvents itemPrice As System.Windows.Forms.TextBox
    Friend WithEvents itemUnit As System.Windows.Forms.ComboBox
    Friend WithEvents lbUnit As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents itemCategory As System.Windows.Forms.ComboBox
    Friend WithEvents lbCategory As System.Windows.Forms.Label
    Friend WithEvents itemCode As System.Windows.Forms.TextBox
    Friend WithEvents lbCode As System.Windows.Forms.Label
    Friend WithEvents itemName As System.Windows.Forms.TextBox
    Friend WithEvents lbName As System.Windows.Forms.Label
    Friend WithEvents lbCategoryEdit As LinkLabel
End Class
