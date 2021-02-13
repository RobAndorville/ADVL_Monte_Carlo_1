<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMatrix
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.pbIcon1 = New System.Windows.Forms.PictureBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtItemStatus = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtItemDescription = New System.Windows.Forms.TextBox()
        Me.txtItemType = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.pbIcon2 = New System.Windows.Forms.PictureBox()
        Me.dgvMatrixItem = New System.Windows.Forms.DataGridView()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.txtMatrixItemName = New System.Windows.Forms.TextBox()
        Me.txtMatrixItemNCols = New System.Windows.Forms.TextBox()
        Me.txtMatrixItemDescr = New System.Windows.Forms.TextBox()
        Me.txtMatrixItemNRows = New System.Windows.Forms.TextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.pbIcon1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.pbIcon2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvMatrixItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(295, 311)
        Me.TabControl1.TabIndex = 19
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.pbIcon1)
        Me.TabPage1.Controls.Add(Me.txtItemName)
        Me.TabPage1.Controls.Add(Me.Label66)
        Me.TabPage1.Controls.Add(Me.Label56)
        Me.TabPage1.Controls.Add(Me.txtItemStatus)
        Me.TabPage1.Controls.Add(Me.Label57)
        Me.TabPage1.Controls.Add(Me.txtItemDescription)
        Me.TabPage1.Controls.Add(Me.txtItemType)
        Me.TabPage1.Controls.Add(Me.Label58)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(287, 285)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Node Info"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'pbIcon1
        '
        Me.pbIcon1.Location = New System.Drawing.Point(6, 205)
        Me.pbIcon1.Name = "pbIcon1"
        Me.pbIcon1.Size = New System.Drawing.Size(40, 40)
        Me.pbIcon1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIcon1.TabIndex = 87
        Me.pbIcon1.TabStop = False
        '
        'txtItemName
        '
        Me.txtItemName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemName.Location = New System.Drawing.Point(6, 19)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(275, 20)
        Me.txtItemName.TabIndex = 11
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(6, 81)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(40, 13)
        Me.Label66.TabIndex = 16
        Me.Label66.Text = "Status:"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(6, 3)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(59, 13)
        Me.Label56.TabIndex = 9
        Me.Label56.Text = "Item name:"
        '
        'txtItemStatus
        '
        Me.txtItemStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemStatus.Location = New System.Drawing.Point(6, 97)
        Me.txtItemStatus.Name = "txtItemStatus"
        Me.txtItemStatus.Size = New System.Drawing.Size(275, 20)
        Me.txtItemStatus.TabIndex = 15
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(6, 42)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(34, 13)
        Me.Label57.TabIndex = 10
        Me.Label57.Text = "Type:"
        '
        'txtItemDescription
        '
        Me.txtItemDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemDescription.Location = New System.Drawing.Point(6, 136)
        Me.txtItemDescription.Multiline = True
        Me.txtItemDescription.Name = "txtItemDescription"
        Me.txtItemDescription.Size = New System.Drawing.Size(275, 63)
        Me.txtItemDescription.TabIndex = 14
        '
        'txtItemType
        '
        Me.txtItemType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemType.Location = New System.Drawing.Point(6, 58)
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.ReadOnly = True
        Me.txtItemType.Size = New System.Drawing.Size(275, 20)
        Me.txtItemType.TabIndex = 12
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(6, 120)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(63, 13)
        Me.Label58.TabIndex = 13
        Me.Label58.Text = "Description:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.pbIcon2)
        Me.TabPage2.Controls.Add(Me.dgvMatrixItem)
        Me.TabPage2.Controls.Add(Me.Label72)
        Me.TabPage2.Controls.Add(Me.txtMatrixItemName)
        Me.TabPage2.Controls.Add(Me.txtMatrixItemNCols)
        Me.TabPage2.Controls.Add(Me.txtMatrixItemDescr)
        Me.TabPage2.Controls.Add(Me.txtMatrixItemNRows)
        Me.TabPage2.Controls.Add(Me.Label69)
        Me.TabPage2.Controls.Add(Me.Label71)
        Me.TabPage2.Controls.Add(Me.Label70)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(287, 285)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Matrix Info"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'pbIcon2
        '
        Me.pbIcon2.Location = New System.Drawing.Point(6, 58)
        Me.pbIcon2.Name = "pbIcon2"
        Me.pbIcon2.Size = New System.Drawing.Size(40, 40)
        Me.pbIcon2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIcon2.TabIndex = 86
        Me.pbIcon2.TabStop = False
        '
        'dgvMatrixItem
        '
        Me.dgvMatrixItem.AllowUserToAddRows = False
        Me.dgvMatrixItem.AllowUserToDeleteRows = False
        Me.dgvMatrixItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMatrixItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMatrixItem.Location = New System.Drawing.Point(6, 113)
        Me.dgvMatrixItem.Name = "dgvMatrixItem"
        Me.dgvMatrixItem.Size = New System.Drawing.Size(275, 166)
        Me.dgvMatrixItem.TabIndex = 48
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(3, 9)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(38, 13)
        Me.Label72.TabIndex = 55
        Me.Label72.Text = "Name:"
        '
        'txtMatrixItemName
        '
        Me.txtMatrixItemName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMatrixItemName.Location = New System.Drawing.Point(47, 6)
        Me.txtMatrixItemName.Name = "txtMatrixItemName"
        Me.txtMatrixItemName.ReadOnly = True
        Me.txtMatrixItemName.Size = New System.Drawing.Size(234, 20)
        Me.txtMatrixItemName.TabIndex = 56
        '
        'txtMatrixItemNCols
        '
        Me.txtMatrixItemNCols.Location = New System.Drawing.Point(128, 32)
        Me.txtMatrixItemNCols.Name = "txtMatrixItemNCols"
        Me.txtMatrixItemNCols.Size = New System.Drawing.Size(36, 20)
        Me.txtMatrixItemNCols.TabIndex = 54
        '
        'txtMatrixItemDescr
        '
        Me.txtMatrixItemDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMatrixItemDescr.Location = New System.Drawing.Point(52, 58)
        Me.txtMatrixItemDescr.Multiline = True
        Me.txtMatrixItemDescr.Name = "txtMatrixItemDescr"
        Me.txtMatrixItemDescr.Size = New System.Drawing.Size(229, 49)
        Me.txtMatrixItemDescr.TabIndex = 50
        '
        'txtMatrixItemNRows
        '
        Me.txtMatrixItemNRows.Location = New System.Drawing.Point(50, 32)
        Me.txtMatrixItemNRows.Name = "txtMatrixItemNRows"
        Me.txtMatrixItemNRows.Size = New System.Drawing.Size(36, 20)
        Me.txtMatrixItemNRows.TabIndex = 53
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(6, 42)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(38, 13)
        Me.Label69.TabIndex = 49
        Me.Label69.Text = "Descr:"
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(92, 35)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(30, 13)
        Me.Label71.TabIndex = 52
        Me.Label71.Text = "Cols:"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(6, 29)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(37, 13)
        Me.Label70.TabIndex = 51
        Me.Label70.Text = "Rows:"
        '
        'frmMatrix
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(319, 335)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmMatrix"
        Me.Text = "Matrix"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.pbIcon1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.pbIcon2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvMatrixItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents pbIcon1 As PictureBox
    Friend WithEvents txtItemName As TextBox
    Friend WithEvents Label66 As Label
    Friend WithEvents Label56 As Label
    Friend WithEvents txtItemStatus As TextBox
    Friend WithEvents Label57 As Label
    Friend WithEvents txtItemDescription As TextBox
    Friend WithEvents txtItemType As TextBox
    Friend WithEvents Label58 As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents pbIcon2 As PictureBox
    Friend WithEvents dgvMatrixItem As DataGridView
    Friend WithEvents Label72 As Label
    Friend WithEvents txtMatrixItemName As TextBox
    Friend WithEvents txtMatrixItemNCols As TextBox
    Friend WithEvents txtMatrixItemDescr As TextBox
    Friend WithEvents txtMatrixItemNRows As TextBox
    Friend WithEvents Label69 As Label
    Friend WithEvents Label71 As Label
    Friend WithEvents Label70 As Label
End Class
