<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScalar
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
        Me.pbIcon1 = New System.Windows.Forms.PictureBox()
        Me.txtScalarName = New System.Windows.Forms.TextBox()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.txtScalarItem = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtItemStatus = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtItemDescription = New System.Windows.Forms.TextBox()
        Me.txtItemType = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        CType(Me.pbIcon1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbIcon1
        '
        Me.pbIcon1.Location = New System.Drawing.Point(12, 113)
        Me.pbIcon1.Name = "pbIcon1"
        Me.pbIcon1.Size = New System.Drawing.Size(40, 40)
        Me.pbIcon1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIcon1.TabIndex = 101
        Me.pbIcon1.TabStop = False
        '
        'txtScalarName
        '
        Me.txtScalarName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtScalarName.Location = New System.Drawing.Point(87, 159)
        Me.txtScalarName.Name = "txtScalarName"
        Me.txtScalarName.Size = New System.Drawing.Size(265, 20)
        Me.txtScalarName.TabIndex = 100
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.Location = New System.Drawing.Point(15, 188)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(37, 13)
        Me.Label78.TabIndex = 99
        Me.Label78.Text = "Value:"
        '
        'txtScalarItem
        '
        Me.txtScalarItem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtScalarItem.Location = New System.Drawing.Point(87, 185)
        Me.txtScalarItem.Name = "txtScalarItem"
        Me.txtScalarItem.Size = New System.Drawing.Size(265, 20)
        Me.txtScalarItem.TabIndex = 98
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(12, 162)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(69, 13)
        Me.Label67.TabIndex = 97
        Me.Label67.Text = "Scalar name:"
        '
        'txtItemName
        '
        Me.txtItemName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemName.Location = New System.Drawing.Point(77, 12)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(275, 20)
        Me.txtItemName.TabIndex = 91
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(12, 67)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(40, 13)
        Me.Label66.TabIndex = 96
        Me.Label66.Text = "Status:"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(12, 15)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(59, 13)
        Me.Label56.TabIndex = 89
        Me.Label56.Text = "Item name:"
        '
        'txtItemStatus
        '
        Me.txtItemStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemStatus.Location = New System.Drawing.Point(77, 64)
        Me.txtItemStatus.Name = "txtItemStatus"
        Me.txtItemStatus.Size = New System.Drawing.Size(275, 20)
        Me.txtItemStatus.TabIndex = 95
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(12, 41)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(34, 13)
        Me.Label57.TabIndex = 90
        Me.Label57.Text = "Type:"
        '
        'txtItemDescription
        '
        Me.txtItemDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemDescription.Location = New System.Drawing.Point(77, 90)
        Me.txtItemDescription.Multiline = True
        Me.txtItemDescription.Name = "txtItemDescription"
        Me.txtItemDescription.Size = New System.Drawing.Size(275, 63)
        Me.txtItemDescription.TabIndex = 94
        '
        'txtItemType
        '
        Me.txtItemType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemType.Location = New System.Drawing.Point(77, 38)
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.ReadOnly = True
        Me.txtItemType.Size = New System.Drawing.Size(275, 20)
        Me.txtItemType.TabIndex = 92
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(12, 93)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(63, 13)
        Me.Label58.TabIndex = 93
        Me.Label58.Text = "Description:"
        '
        'frmScalar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(364, 216)
        Me.Controls.Add(Me.pbIcon1)
        Me.Controls.Add(Me.txtScalarName)
        Me.Controls.Add(Me.Label78)
        Me.Controls.Add(Me.txtScalarItem)
        Me.Controls.Add(Me.Label67)
        Me.Controls.Add(Me.txtItemName)
        Me.Controls.Add(Me.Label66)
        Me.Controls.Add(Me.Label56)
        Me.Controls.Add(Me.txtItemStatus)
        Me.Controls.Add(Me.Label57)
        Me.Controls.Add(Me.txtItemDescription)
        Me.Controls.Add(Me.txtItemType)
        Me.Controls.Add(Me.Label58)
        Me.Name = "frmScalar"
        Me.Text = "Scalar"
        CType(Me.pbIcon1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pbIcon1 As PictureBox
    Friend WithEvents txtScalarName As TextBox
    Friend WithEvents Label78 As Label
    Friend WithEvents txtScalarItem As TextBox
    Friend WithEvents Label67 As Label
    Friend WithEvents txtItemName As TextBox
    Friend WithEvents Label66 As Label
    Friend WithEvents Label56 As Label
    Friend WithEvents txtItemStatus As TextBox
    Friend WithEvents Label57 As Label
    Friend WithEvents txtItemDescription As TextBox
    Friend WithEvents txtItemType As TextBox
    Friend WithEvents Label58 As Label
End Class
