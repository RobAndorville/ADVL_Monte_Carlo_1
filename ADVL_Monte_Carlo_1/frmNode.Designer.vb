<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNode
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
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtItemStatus = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtItemDescription = New System.Windows.Forms.TextBox()
        Me.txtItemType = New System.Windows.Forms.TextBox()
        CType(Me.pbIcon1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbIcon1
        '
        Me.pbIcon1.Location = New System.Drawing.Point(12, 117)
        Me.pbIcon1.Name = "pbIcon1"
        Me.pbIcon1.Size = New System.Drawing.Size(40, 40)
        Me.pbIcon1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIcon1.TabIndex = 96
        Me.pbIcon1.TabStop = False
        '
        'txtItemName
        '
        Me.txtItemName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemName.Location = New System.Drawing.Point(77, 16)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(335, 20)
        Me.txtItemName.TabIndex = 91
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(12, 71)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(40, 13)
        Me.Label66.TabIndex = 95
        Me.Label66.Text = "Status:"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(12, 19)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(59, 13)
        Me.Label56.TabIndex = 89
        Me.Label56.Text = "Item name:"
        '
        'txtItemStatus
        '
        Me.txtItemStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemStatus.Location = New System.Drawing.Point(77, 68)
        Me.txtItemStatus.Name = "txtItemStatus"
        Me.txtItemStatus.Size = New System.Drawing.Size(335, 20)
        Me.txtItemStatus.TabIndex = 94
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(12, 45)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(34, 13)
        Me.Label57.TabIndex = 90
        Me.Label57.Text = "Type:"
        '
        'txtItemDescription
        '
        Me.txtItemDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemDescription.Location = New System.Drawing.Point(77, 94)
        Me.txtItemDescription.Multiline = True
        Me.txtItemDescription.Name = "txtItemDescription"
        Me.txtItemDescription.Size = New System.Drawing.Size(335, 63)
        Me.txtItemDescription.TabIndex = 93
        '
        'txtItemType
        '
        Me.txtItemType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtItemType.Location = New System.Drawing.Point(77, 42)
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.ReadOnly = True
        Me.txtItemType.Size = New System.Drawing.Size(335, 20)
        Me.txtItemType.TabIndex = 92
        '
        'frmNode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(424, 173)
        Me.Controls.Add(Me.pbIcon1)
        Me.Controls.Add(Me.txtItemName)
        Me.Controls.Add(Me.Label66)
        Me.Controls.Add(Me.Label56)
        Me.Controls.Add(Me.txtItemStatus)
        Me.Controls.Add(Me.Label57)
        Me.Controls.Add(Me.txtItemDescription)
        Me.Controls.Add(Me.txtItemType)
        Me.Name = "frmNode"
        Me.Text = "Node"
        CType(Me.pbIcon1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pbIcon1 As PictureBox
    Friend WithEvents txtItemName As TextBox
    Friend WithEvents Label66 As Label
    Friend WithEvents Label56 As Label
    Friend WithEvents txtItemStatus As TextBox
    Friend WithEvents Label57 As Label
    Friend WithEvents txtItemDescription As TextBox
    Friend WithEvents txtItemType As TextBox
End Class
