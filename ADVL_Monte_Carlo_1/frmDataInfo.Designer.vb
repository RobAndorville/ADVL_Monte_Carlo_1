<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDataInfo
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
        Me.components = New System.ComponentModel.Container()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.rbBuiltIn = New System.Windows.Forms.RadioButton()
        Me.btnFormatHelp = New System.Windows.Forms.Button()
        Me.txtVarFormat = New System.Windows.Forms.TextBox()
        Me.txtStdDevFormat = New System.Windows.Forms.TextBox()
        Me.txtAvgFormat = New System.Windows.Forms.TextBox()
        Me.txtSumFormat = New System.Windows.Forms.TextBox()
        Me.txtMaxFormat = New System.Windows.Forms.TextBox()
        Me.txtMinFormat = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.rbPopulation = New System.Windows.Forms.RadioButton()
        Me.rbSample = New System.Windows.Forms.RadioButton()
        Me.txtNRows = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDatasetName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTableCount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblTableNo = New System.Windows.Forms.Label()
        Me.cmbDataTable = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDataSource = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvStatistics = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnCalcAllCorrs = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.rbTransCorrChol = New System.Windows.Forms.RadioButton()
        Me.rbCorrChol = New System.Windows.Forms.RadioButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnFormatHelp1 = New System.Windows.Forms.Button()
        Me.txtCorrMatFormat = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dgvCorrMatrix = New System.Windows.Forms.DataGridView()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.btnFormatHelp2 = New System.Windows.Forms.Button()
        Me.txtCorrCholFormat = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dgvCorrCholeski = New System.Windows.Forms.DataGridView()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnCalcCorrs = New System.Windows.Forms.Button()
        Me.txtNCorrVars = New System.Windows.Forms.TextBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.btnCalcAllCov = New System.Windows.Forms.Button()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnFormatHelp3 = New System.Windows.Forms.Button()
        Me.txtCovMatFormat = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dgvCovMatrix = New System.Windows.Forms.DataGridView()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnFormatHelp4 = New System.Windows.Forms.Button()
        Me.txtCovCholFormat = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dgvCovCholeski = New System.Windows.Forms.DataGridView()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.rbTransCovChol = New System.Windows.Forms.RadioButton()
        Me.rbCovChol = New System.Windows.Forms.RadioButton()
        Me.btnCalcCov = New System.Windows.Forms.Button()
        Me.txtNCovVars = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dgvStatistics, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvCorrMatrix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCorrCholeski, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.dgvCovMatrix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCovCholeski, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 40)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(808, 504)
        Me.TabControl1.TabIndex = 11
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.rbBuiltIn)
        Me.TabPage1.Controls.Add(Me.btnFormatHelp)
        Me.TabPage1.Controls.Add(Me.txtVarFormat)
        Me.TabPage1.Controls.Add(Me.txtStdDevFormat)
        Me.TabPage1.Controls.Add(Me.txtAvgFormat)
        Me.TabPage1.Controls.Add(Me.txtSumFormat)
        Me.TabPage1.Controls.Add(Me.txtMaxFormat)
        Me.TabPage1.Controls.Add(Me.txtMinFormat)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.rbPopulation)
        Me.TabPage1.Controls.Add(Me.rbSample)
        Me.TabPage1.Controls.Add(Me.txtNRows)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.txtDatasetName)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.lblTableCount)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.lblTableNo)
        Me.TabPage1.Controls.Add(Me.cmbDataTable)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.txtDataSource)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.dgvStatistics)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(800, 478)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Statistics"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'rbBuiltIn
        '
        Me.rbBuiltIn.AutoSize = True
        Me.rbBuiltIn.Location = New System.Drawing.Point(701, 36)
        Me.rbBuiltIn.Name = "rbBuiltIn"
        Me.rbBuiltIn.Size = New System.Drawing.Size(56, 17)
        Me.rbBuiltIn.TabIndex = 310
        Me.rbBuiltIn.TabStop = True
        Me.rbBuiltIn.Text = "Built-in"
        Me.rbBuiltIn.UseVisualStyleBackColor = True
        '
        'btnFormatHelp
        '
        Me.btnFormatHelp.Location = New System.Drawing.Point(615, 58)
        Me.btnFormatHelp.Name = "btnFormatHelp"
        Me.btnFormatHelp.Size = New System.Drawing.Size(21, 22)
        Me.btnFormatHelp.TabIndex = 309
        Me.btnFormatHelp.Text = "?"
        Me.btnFormatHelp.UseVisualStyleBackColor = True
        '
        'txtVarFormat
        '
        Me.txtVarFormat.Location = New System.Drawing.Point(521, 58)
        Me.txtVarFormat.Name = "txtVarFormat"
        Me.txtVarFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtVarFormat.TabIndex = 308
        '
        'txtStdDevFormat
        '
        Me.txtStdDevFormat.Location = New System.Drawing.Point(427, 58)
        Me.txtStdDevFormat.Name = "txtStdDevFormat"
        Me.txtStdDevFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtStdDevFormat.TabIndex = 307
        '
        'txtAvgFormat
        '
        Me.txtAvgFormat.Location = New System.Drawing.Point(333, 58)
        Me.txtAvgFormat.Name = "txtAvgFormat"
        Me.txtAvgFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtAvgFormat.TabIndex = 306
        '
        'txtSumFormat
        '
        Me.txtSumFormat.Location = New System.Drawing.Point(239, 58)
        Me.txtSumFormat.Name = "txtSumFormat"
        Me.txtSumFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtSumFormat.TabIndex = 305
        '
        'txtMaxFormat
        '
        Me.txtMaxFormat.Location = New System.Drawing.Point(145, 58)
        Me.txtMaxFormat.Name = "txtMaxFormat"
        Me.txtMaxFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtMaxFormat.TabIndex = 304
        '
        'txtMinFormat
        '
        Me.txtMinFormat.Location = New System.Drawing.Point(51, 58)
        Me.txtMinFormat.Name = "txtMinFormat"
        Me.txtMinFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtMinFormat.TabIndex = 303
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 61)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(39, 13)
        Me.Label15.TabIndex = 302
        Me.Label15.Text = "Format"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(505, 38)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(43, 13)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Data is:"
        '
        'rbPopulation
        '
        Me.rbPopulation.AutoSize = True
        Me.rbPopulation.Location = New System.Drawing.Point(620, 36)
        Me.rbPopulation.Name = "rbPopulation"
        Me.rbPopulation.Size = New System.Drawing.Size(75, 17)
        Me.rbPopulation.TabIndex = 13
        Me.rbPopulation.TabStop = True
        Me.rbPopulation.Text = "Population"
        Me.rbPopulation.UseVisualStyleBackColor = True
        '
        'rbSample
        '
        Me.rbSample.AutoSize = True
        Me.rbSample.Location = New System.Drawing.Point(554, 36)
        Me.rbSample.Name = "rbSample"
        Me.rbSample.Size = New System.Drawing.Size(60, 17)
        Me.rbSample.TabIndex = 12
        Me.rbSample.TabStop = True
        Me.rbSample.Text = "Sample"
        Me.rbSample.UseVisualStyleBackColor = True
        '
        'txtNRows
        '
        Me.txtNRows.Location = New System.Drawing.Point(374, 33)
        Me.txtNRows.Name = "txtNRows"
        Me.txtNRows.ReadOnly = True
        Me.txtNRows.Size = New System.Drawing.Size(125, 20)
        Me.txtNRows.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(311, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "No. Rows:"
        '
        'txtDatasetName
        '
        Me.txtDatasetName.Location = New System.Drawing.Point(92, 32)
        Me.txtDatasetName.Name = "txtDatasetName"
        Me.txtDatasetName.ReadOnly = True
        Me.txtDatasetName.Size = New System.Drawing.Size(207, 20)
        Me.txtDatasetName.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 34)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Dataset Name:"
        '
        'lblTableCount
        '
        Me.lblTableCount.AutoSize = True
        Me.lblTableCount.Location = New System.Drawing.Point(667, 9)
        Me.lblTableCount.Name = "lblTableCount"
        Me.lblTableCount.Size = New System.Drawing.Size(35, 13)
        Me.lblTableCount.TabIndex = 7
        Me.lblTableCount.Text = "Count"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(645, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "of"
        '
        'lblTableNo
        '
        Me.lblTableNo.AutoSize = True
        Me.lblTableNo.Location = New System.Drawing.Point(618, 9)
        Me.lblTableNo.Name = "lblTableNo"
        Me.lblTableNo.Size = New System.Drawing.Size(21, 13)
        Me.lblTableNo.TabIndex = 5
        Me.lblTableNo.Text = "No"
        '
        'cmbDataTable
        '
        Me.cmbDataTable.FormattingEnabled = True
        Me.cmbDataTable.Location = New System.Drawing.Point(374, 6)
        Me.cmbDataTable.Name = "cmbDataTable"
        Me.cmbDataTable.Size = New System.Drawing.Size(238, 21)
        Me.cmbDataTable.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(305, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Table Name:"
        '
        'txtDataSource
        '
        Me.txtDataSource.Location = New System.Drawing.Point(92, 6)
        Me.txtDataSource.Name = "txtDataSource"
        Me.txtDataSource.ReadOnly = True
        Me.txtDataSource.Size = New System.Drawing.Size(207, 20)
        Me.txtDataSource.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Data Source:"
        '
        'dgvStatistics
        '
        Me.dgvStatistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStatistics.Location = New System.Drawing.Point(6, 84)
        Me.dgvStatistics.Name = "dgvStatistics"
        Me.dgvStatistics.Size = New System.Drawing.Size(788, 388)
        Me.dgvStatistics.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnCalcAllCorrs)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.rbTransCorrChol)
        Me.TabPage2.Controls.Add(Me.rbCorrChol)
        Me.TabPage2.Controls.Add(Me.SplitContainer1)
        Me.TabPage2.Controls.Add(Me.btnCalcCorrs)
        Me.TabPage2.Controls.Add(Me.txtNCorrVars)
        Me.TabPage2.Controls.Add(Me.Label52)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(800, 478)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Correlations"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnCalcAllCorrs
        '
        Me.btnCalcAllCorrs.Location = New System.Drawing.Point(260, 6)
        Me.btnCalcAllCorrs.Name = "btnCalcAllCorrs"
        Me.btnCalcAllCorrs.Size = New System.Drawing.Size(31, 22)
        Me.btnCalcAllCorrs.TabIndex = 299
        Me.btnCalcAllCorrs.Text = "All"
        Me.btnCalcAllCorrs.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(297, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 13)
        Me.Label7.TabIndex = 298
        Me.Label7.Text = "Show"
        '
        'rbTransCorrChol
        '
        Me.rbTransCorrChol.AutoSize = True
        Me.rbTransCorrChol.Location = New System.Drawing.Point(481, 9)
        Me.rbTransCorrChol.Name = "rbTransCorrChol"
        Me.rbTransCorrChol.Size = New System.Drawing.Size(124, 17)
        Me.rbTransCorrChol.TabIndex = 297
        Me.rbTransCorrChol.TabStop = True
        Me.rbTransCorrChol.Text = "Transposed Choleski"
        Me.rbTransCorrChol.UseVisualStyleBackColor = True
        '
        'rbCorrChol
        '
        Me.rbCorrChol.AutoSize = True
        Me.rbCorrChol.Location = New System.Drawing.Point(337, 9)
        Me.rbCorrChol.Name = "rbCorrChol"
        Me.rbCorrChol.Size = New System.Drawing.Size(138, 17)
        Me.rbCorrChol.TabIndex = 296
        Me.rbCorrChol.TabStop = True
        Me.rbCorrChol.Text = "Choleski Decomposition"
        Me.rbCorrChol.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 34)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnFormatHelp1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCorrMatFormat)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvCorrMatrix)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label53)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnFormatHelp2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtCorrCholFormat)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label14)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvCorrCholeski)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label6)
        Me.SplitContainer1.Size = New System.Drawing.Size(733, 441)
        Me.SplitContainer1.SplitterDistance = 220
        Me.SplitContainer1.TabIndex = 295
        '
        'btnFormatHelp1
        '
        Me.btnFormatHelp1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFormatHelp1.Location = New System.Drawing.Point(709, 1)
        Me.btnFormatHelp1.Name = "btnFormatHelp1"
        Me.btnFormatHelp1.Size = New System.Drawing.Size(21, 22)
        Me.btnFormatHelp1.TabIndex = 303
        Me.btnFormatHelp1.Text = "?"
        Me.btnFormatHelp1.UseVisualStyleBackColor = True
        '
        'txtCorrMatFormat
        '
        Me.txtCorrMatFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCorrMatFormat.Location = New System.Drawing.Point(615, 3)
        Me.txtCorrMatFormat.Name = "txtCorrMatFormat"
        Me.txtCorrMatFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtCorrMatFormat.TabIndex = 301
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(570, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(39, 13)
        Me.Label13.TabIndex = 300
        Me.Label13.Text = "Format"
        '
        'dgvCorrMatrix
        '
        Me.dgvCorrMatrix.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCorrMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCorrMatrix.Location = New System.Drawing.Point(3, 29)
        Me.dgvCorrMatrix.Name = "dgvCorrMatrix"
        Me.dgvCorrMatrix.Size = New System.Drawing.Size(727, 188)
        Me.dgvCorrMatrix.TabIndex = 286
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(3, 0)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(91, 13)
        Me.Label53.TabIndex = 287
        Me.Label53.Text = "Correlation Matrix:"
        '
        'btnFormatHelp2
        '
        Me.btnFormatHelp2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFormatHelp2.Location = New System.Drawing.Point(709, 1)
        Me.btnFormatHelp2.Name = "btnFormatHelp2"
        Me.btnFormatHelp2.Size = New System.Drawing.Size(21, 22)
        Me.btnFormatHelp2.TabIndex = 304
        Me.btnFormatHelp2.Text = "?"
        Me.btnFormatHelp2.UseVisualStyleBackColor = True
        '
        'txtCorrCholFormat
        '
        Me.txtCorrCholFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCorrCholFormat.Location = New System.Drawing.Point(615, 3)
        Me.txtCorrCholFormat.Name = "txtCorrCholFormat"
        Me.txtCorrCholFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtCorrCholFormat.TabIndex = 302
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(570, 6)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(39, 13)
        Me.Label14.TabIndex = 301
        Me.Label14.Text = "Format"
        '
        'dgvCorrCholeski
        '
        Me.dgvCorrCholeski.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCorrCholeski.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCorrCholeski.Location = New System.Drawing.Point(3, 29)
        Me.dgvCorrCholeski.Name = "dgvCorrCholeski"
        Me.dgvCorrCholeski.Size = New System.Drawing.Size(727, 185)
        Me.dgvCorrCholeski.TabIndex = 289
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(182, 13)
        Me.Label6.TabIndex = 288
        Me.Label6.Text = "Transposed Choleski Decomposition:"
        '
        'btnCalcCorrs
        '
        Me.btnCalcCorrs.Location = New System.Drawing.Point(190, 6)
        Me.btnCalcCorrs.Name = "btnCalcCorrs"
        Me.btnCalcCorrs.Size = New System.Drawing.Size(64, 22)
        Me.btnCalcCorrs.TabIndex = 294
        Me.btnCalcCorrs.Text = "Calculate"
        Me.btnCalcCorrs.UseVisualStyleBackColor = True
        '
        'txtNCorrVars
        '
        Me.txtNCorrVars.Location = New System.Drawing.Point(96, 6)
        Me.txtNCorrVars.Name = "txtNCorrVars"
        Me.txtNCorrVars.Size = New System.Drawing.Size(88, 20)
        Me.txtNCorrVars.TabIndex = 293
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(6, 9)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(84, 13)
        Me.Label52.TabIndex = 292
        Me.Label52.Text = "No. of variables:"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btnCalcAllCov)
        Me.TabPage3.Controls.Add(Me.SplitContainer2)
        Me.TabPage3.Controls.Add(Me.Label8)
        Me.TabPage3.Controls.Add(Me.rbTransCovChol)
        Me.TabPage3.Controls.Add(Me.rbCovChol)
        Me.TabPage3.Controls.Add(Me.btnCalcCov)
        Me.TabPage3.Controls.Add(Me.txtNCovVars)
        Me.TabPage3.Controls.Add(Me.Label9)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(800, 478)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Covariance"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'btnCalcAllCov
        '
        Me.btnCalcAllCov.Location = New System.Drawing.Point(260, 6)
        Me.btnCalcAllCov.Name = "btnCalcAllCov"
        Me.btnCalcAllCov.Size = New System.Drawing.Size(31, 22)
        Me.btnCalcAllCov.TabIndex = 306
        Me.btnCalcAllCov.Text = "All"
        Me.btnCalcAllCov.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 34)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnFormatHelp3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtCovMatFormat)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label16)
        Me.SplitContainer2.Panel1.Controls.Add(Me.dgvCovMatrix)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label10)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnFormatHelp4)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtCovCholFormat)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label17)
        Me.SplitContainer2.Panel2.Controls.Add(Me.dgvCovCholeski)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label11)
        Me.SplitContainer2.Size = New System.Drawing.Size(733, 441)
        Me.SplitContainer2.SplitterDistance = 220
        Me.SplitContainer2.TabIndex = 305
        '
        'btnFormatHelp3
        '
        Me.btnFormatHelp3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFormatHelp3.Location = New System.Drawing.Point(709, 1)
        Me.btnFormatHelp3.Name = "btnFormatHelp3"
        Me.btnFormatHelp3.Size = New System.Drawing.Size(21, 22)
        Me.btnFormatHelp3.TabIndex = 306
        Me.btnFormatHelp3.Text = "?"
        Me.btnFormatHelp3.UseVisualStyleBackColor = True
        '
        'txtCovMatFormat
        '
        Me.txtCovMatFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCovMatFormat.Location = New System.Drawing.Point(615, 3)
        Me.txtCovMatFormat.Name = "txtCovMatFormat"
        Me.txtCovMatFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtCovMatFormat.TabIndex = 305
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(570, 6)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(39, 13)
        Me.Label16.TabIndex = 304
        Me.Label16.Text = "Format"
        '
        'dgvCovMatrix
        '
        Me.dgvCovMatrix.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCovMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCovMatrix.Location = New System.Drawing.Point(3, 29)
        Me.dgvCovMatrix.Name = "dgvCovMatrix"
        Me.dgvCovMatrix.Size = New System.Drawing.Size(727, 188)
        Me.dgvCovMatrix.TabIndex = 286
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 13)
        Me.Label10.TabIndex = 287
        Me.Label10.Text = "Covariance Matrix:"
        '
        'btnFormatHelp4
        '
        Me.btnFormatHelp4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFormatHelp4.Location = New System.Drawing.Point(709, 1)
        Me.btnFormatHelp4.Name = "btnFormatHelp4"
        Me.btnFormatHelp4.Size = New System.Drawing.Size(21, 22)
        Me.btnFormatHelp4.TabIndex = 306
        Me.btnFormatHelp4.Text = "?"
        Me.btnFormatHelp4.UseVisualStyleBackColor = True
        '
        'txtCovCholFormat
        '
        Me.txtCovCholFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCovCholFormat.Location = New System.Drawing.Point(615, 3)
        Me.txtCovCholFormat.Name = "txtCovCholFormat"
        Me.txtCovCholFormat.Size = New System.Drawing.Size(88, 20)
        Me.txtCovCholFormat.TabIndex = 305
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(570, 6)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(39, 13)
        Me.Label17.TabIndex = 304
        Me.Label17.Text = "Format"
        '
        'dgvCovCholeski
        '
        Me.dgvCovCholeski.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvCovCholeski.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCovCholeski.Location = New System.Drawing.Point(3, 29)
        Me.dgvCovCholeski.Name = "dgvCovCholeski"
        Me.dgvCovCholeski.Size = New System.Drawing.Size(727, 185)
        Me.dgvCovCholeski.TabIndex = 289
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(182, 13)
        Me.Label11.TabIndex = 288
        Me.Label11.Text = "Transposed Choleski Decomposition:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(297, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(34, 13)
        Me.Label8.TabIndex = 304
        Me.Label8.Text = "Show"
        '
        'rbTransCovChol
        '
        Me.rbTransCovChol.AutoSize = True
        Me.rbTransCovChol.Location = New System.Drawing.Point(481, 9)
        Me.rbTransCovChol.Name = "rbTransCovChol"
        Me.rbTransCovChol.Size = New System.Drawing.Size(124, 17)
        Me.rbTransCovChol.TabIndex = 303
        Me.rbTransCovChol.TabStop = True
        Me.rbTransCovChol.Text = "Transposed Choleski"
        Me.rbTransCovChol.UseVisualStyleBackColor = True
        '
        'rbCovChol
        '
        Me.rbCovChol.AutoSize = True
        Me.rbCovChol.Location = New System.Drawing.Point(337, 9)
        Me.rbCovChol.Name = "rbCovChol"
        Me.rbCovChol.Size = New System.Drawing.Size(138, 17)
        Me.rbCovChol.TabIndex = 302
        Me.rbCovChol.TabStop = True
        Me.rbCovChol.Text = "Choleski Decomposition"
        Me.rbCovChol.UseVisualStyleBackColor = True
        '
        'btnCalcCov
        '
        Me.btnCalcCov.Location = New System.Drawing.Point(190, 6)
        Me.btnCalcCov.Name = "btnCalcCov"
        Me.btnCalcCov.Size = New System.Drawing.Size(64, 22)
        Me.btnCalcCov.TabIndex = 301
        Me.btnCalcCov.Text = "Calculate"
        Me.btnCalcCov.UseVisualStyleBackColor = True
        '
        'txtNCovVars
        '
        Me.txtNCovVars.Location = New System.Drawing.Point(96, 6)
        Me.txtNCovVars.Name = "txtNCovVars"
        Me.txtNCovVars.Size = New System.Drawing.Size(88, 20)
        Me.txtNCovVars.TabIndex = 300
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 13)
        Me.Label9.TabIndex = 299
        Me.Label9.Text = "No. of variables:"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(772, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 22)
        Me.btnExit.TabIndex = 10
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmDataInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 556)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmDataInfo"
        Me.Text = "Data Information"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.dgvStatistics, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvCorrMatrix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCorrCholeski, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.dgvCovMatrix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCovCholeski, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents rbBuiltIn As RadioButton
    Friend WithEvents btnFormatHelp As Button
    Friend WithEvents txtVarFormat As TextBox
    Friend WithEvents txtStdDevFormat As TextBox
    Friend WithEvents txtAvgFormat As TextBox
    Friend WithEvents txtSumFormat As TextBox
    Friend WithEvents txtMaxFormat As TextBox
    Friend WithEvents txtMinFormat As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents rbPopulation As RadioButton
    Friend WithEvents rbSample As RadioButton
    Friend WithEvents txtNRows As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtDatasetName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lblTableCount As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblTableNo As Label
    Friend WithEvents cmbDataTable As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtDataSource As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvStatistics As DataGridView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents btnCalcAllCorrs As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents rbTransCorrChol As RadioButton
    Friend WithEvents rbCorrChol As RadioButton
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnFormatHelp1 As Button
    Friend WithEvents txtCorrMatFormat As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents dgvCorrMatrix As DataGridView
    Friend WithEvents Label53 As Label
    Friend WithEvents btnFormatHelp2 As Button
    Friend WithEvents txtCorrCholFormat As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents dgvCorrCholeski As DataGridView
    Friend WithEvents Label6 As Label
    Friend WithEvents btnCalcCorrs As Button
    Friend WithEvents txtNCorrVars As TextBox
    Friend WithEvents Label52 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents btnCalcAllCov As Button
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents btnFormatHelp3 As Button
    Friend WithEvents txtCovMatFormat As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents dgvCovMatrix As DataGridView
    Friend WithEvents Label10 As Label
    Friend WithEvents btnFormatHelp4 As Button
    Friend WithEvents txtCovCholFormat As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents dgvCovCholeski As DataGridView
    Friend WithEvents Label11 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents rbTransCovChol As RadioButton
    Friend WithEvents rbCovChol As RadioButton
    Friend WithEvents btnCalcCov As Button
    Friend WithEvents txtNCovVars As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
