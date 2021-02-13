<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDistribChart
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
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtUnits = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtParamE = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtParamD = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtParamC = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtParamB = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDescr = New System.Windows.Forms.TextBox()
        Me.chkRevCDF = New System.Windows.Forms.CheckBox()
        Me.chkCDF = New System.Windows.Forms.CheckBox()
        Me.chkPDF = New System.Windows.Forms.CheckBox()
        Me.txtRandVarName = New System.Windows.Forms.TextBox()
        Me.txtParamA = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbDistribution = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.chkLegend = New System.Windows.Forms.CheckBox()
        Me.btnCopyChart = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnFormatHelp2 = New System.Windows.Forms.Button()
        Me.btnUpdateAnnotation = New System.Windows.Forms.Button()
        Me.btnDeleteAnnot = New System.Windows.Forms.Button()
        Me.dgvAnnot = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtRVLowerVal = New System.Windows.Forms.TextBox()
        Me.txtRVUpperVal = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtRVIntervalProb = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.chkShowOnPDF = New System.Windows.Forms.CheckBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtLowerValCDF = New System.Windows.Forms.TextBox()
        Me.txtUpperValCDF = New System.Windows.Forms.TextBox()
        Me.rbDecimal = New System.Windows.Forms.RadioButton()
        Me.rbPercent = New System.Windows.Forms.RadioButton()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtProbFormat = New System.Windows.Forms.TextBox()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvAnnot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(297, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 13)
        Me.Label9.TabIndex = 53
        Me.Label9.Text = "Units:"
        '
        'txtUnits
        '
        Me.txtUnits.Location = New System.Drawing.Point(337, 6)
        Me.txtUnits.Name = "txtUnits"
        Me.txtUnits.Size = New System.Drawing.Size(163, 20)
        Me.txtUnits.TabIndex = 52
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(374, 101)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 13)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "Parameter E:"
        '
        'txtParamE
        '
        Me.txtParamE.Location = New System.Drawing.Point(448, 99)
        Me.txtParamE.Name = "txtParamE"
        Me.txtParamE.Size = New System.Drawing.Size(49, 20)
        Me.txtParamE.TabIndex = 50
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(245, 101)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 13)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "Parameter D:"
        '
        'txtParamD
        '
        Me.txtParamD.Location = New System.Drawing.Point(319, 98)
        Me.txtParamD.Name = "txtParamD"
        Me.txtParamD.Size = New System.Drawing.Size(49, 20)
        Me.txtParamD.TabIndex = 48
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(116, 102)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 13)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Parameter C:"
        '
        'txtParamC
        '
        Me.txtParamC.Location = New System.Drawing.Point(190, 99)
        Me.txtParamC.Name = "txtParamC"
        Me.txtParamC.Size = New System.Drawing.Size(49, 20)
        Me.txtParamC.TabIndex = 46
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(374, 75)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Parameter B:"
        '
        'txtParamB
        '
        Me.txtParamB.Location = New System.Drawing.Point(448, 73)
        Me.txtParamB.Name = "txtParamB"
        Me.txtParamB.Size = New System.Drawing.Size(49, 20)
        Me.txtParamB.TabIndex = 44
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(245, 75)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Parameter A:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Description:"
        '
        'txtDescr
        '
        Me.txtDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescr.Location = New System.Drawing.Point(90, 32)
        Me.txtDescr.Multiline = True
        Me.txtDescr.Name = "txtDescr"
        Me.txtDescr.Size = New System.Drawing.Size(520, 34)
        Me.txtDescr.TabIndex = 41
        '
        'chkRevCDF
        '
        Me.chkRevCDF.AutoSize = True
        Me.chkRevCDF.Location = New System.Drawing.Point(118, 16)
        Me.chkRevCDF.Name = "chkRevCDF"
        Me.chkRevCDF.Size = New System.Drawing.Size(90, 17)
        Me.chkRevCDF.TabIndex = 40
        Me.chkRevCDF.Text = "Reverse CDF"
        Me.chkRevCDF.UseVisualStyleBackColor = True
        '
        'chkCDF
        '
        Me.chkCDF.AutoSize = True
        Me.chkCDF.Location = New System.Drawing.Point(65, 16)
        Me.chkCDF.Name = "chkCDF"
        Me.chkCDF.Size = New System.Drawing.Size(47, 17)
        Me.chkCDF.TabIndex = 39
        Me.chkCDF.Text = "CDF"
        Me.chkCDF.UseVisualStyleBackColor = True
        '
        'chkPDF
        '
        Me.chkPDF.AutoSize = True
        Me.chkPDF.Location = New System.Drawing.Point(12, 16)
        Me.chkPDF.Name = "chkPDF"
        Me.chkPDF.Size = New System.Drawing.Size(47, 17)
        Me.chkPDF.TabIndex = 38
        Me.chkPDF.Text = "PDF"
        Me.chkPDF.UseVisualStyleBackColor = True
        '
        'txtRandVarName
        '
        Me.txtRandVarName.Location = New System.Drawing.Point(90, 6)
        Me.txtRandVarName.Name = "txtRandVarName"
        Me.txtRandVarName.Size = New System.Drawing.Size(201, 20)
        Me.txtRandVarName.TabIndex = 37
        '
        'txtParamA
        '
        Me.txtParamA.Location = New System.Drawing.Point(319, 72)
        Me.txtParamA.Name = "txtParamA"
        Me.txtParamA.Size = New System.Drawing.Size(49, 20)
        Me.txtParamA.TabIndex = 36
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Variable name:"
        '
        'cmbDistribution
        '
        Me.cmbDistribution.FormattingEnabled = True
        Me.cmbDistribution.Location = New System.Drawing.Point(76, 72)
        Me.cmbDistribution.Name = "cmbDistribution"
        Me.cmbDistribution.Size = New System.Drawing.Size(163, 21)
        Me.cmbDistribution.TabIndex = 34
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "Distribution:"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(588, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 22)
        Me.btnExit.TabIndex = 32
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Chart1
        '
        Me.Chart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea3.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend3)
        Me.Chart1.Location = New System.Drawing.Point(3, 125)
        Me.Chart1.Name = "Chart1"
        Series3.ChartArea = "ChartArea1"
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Me.Chart1.Series.Add(Series3)
        Me.Chart1.Size = New System.Drawing.Size(610, 510)
        Me.Chart1.TabIndex = 54
        Me.Chart1.Text = "Chart1"
        '
        'chkLegend
        '
        Me.chkLegend.AutoSize = True
        Me.chkLegend.Location = New System.Drawing.Point(209, 16)
        Me.chkLegend.Name = "chkLegend"
        Me.chkLegend.Size = New System.Drawing.Size(62, 17)
        Me.chkLegend.TabIndex = 55
        Me.chkLegend.Text = "Legend"
        Me.chkLegend.UseVisualStyleBackColor = True
        '
        'btnCopyChart
        '
        Me.btnCopyChart.Location = New System.Drawing.Point(277, 12)
        Me.btnCopyChart.Name = "btnCopyChart"
        Me.btnCopyChart.Size = New System.Drawing.Size(69, 22)
        Me.btnCopyChart.TabIndex = 56
        Me.btnCopyChart.Text = "Copy Chart"
        Me.btnCopyChart.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 40)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(624, 664)
        Me.TabControl1.TabIndex = 57
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Chart1)
        Me.TabPage1.Controls.Add(Me.txtUnits)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.cmbDistribution)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.txtParamA)
        Me.TabPage1.Controls.Add(Me.txtParamE)
        Me.TabPage1.Controls.Add(Me.txtRandVarName)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.txtDescr)
        Me.TabPage1.Controls.Add(Me.txtParamD)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.txtParamC)
        Me.TabPage1.Controls.Add(Me.txtParamB)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(616, 638)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Chart"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.btnFormatHelp2)
        Me.TabPage2.Controls.Add(Me.btnUpdateAnnotation)
        Me.TabPage2.Controls.Add(Me.btnDeleteAnnot)
        Me.TabPage2.Controls.Add(Me.dgvAnnot)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(616, 638)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Annotation"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnFormatHelp2
        '
        Me.btnFormatHelp2.Location = New System.Drawing.Point(128, 323)
        Me.btnFormatHelp2.Name = "btnFormatHelp2"
        Me.btnFormatHelp2.Size = New System.Drawing.Size(79, 22)
        Me.btnFormatHelp2.TabIndex = 294
        Me.btnFormatHelp2.Text = "Format Help"
        Me.btnFormatHelp2.UseVisualStyleBackColor = True
        '
        'btnUpdateAnnotation
        '
        Me.btnUpdateAnnotation.Location = New System.Drawing.Point(67, 323)
        Me.btnUpdateAnnotation.Name = "btnUpdateAnnotation"
        Me.btnUpdateAnnotation.Size = New System.Drawing.Size(55, 22)
        Me.btnUpdateAnnotation.TabIndex = 293
        Me.btnUpdateAnnotation.Text = "Update"
        Me.btnUpdateAnnotation.UseVisualStyleBackColor = True
        '
        'btnDeleteAnnot
        '
        Me.btnDeleteAnnot.Location = New System.Drawing.Point(6, 323)
        Me.btnDeleteAnnot.Name = "btnDeleteAnnot"
        Me.btnDeleteAnnot.Size = New System.Drawing.Size(55, 22)
        Me.btnDeleteAnnot.TabIndex = 292
        Me.btnDeleteAnnot.Text = "Delete"
        Me.btnDeleteAnnot.UseVisualStyleBackColor = True
        '
        'dgvAnnot
        '
        Me.dgvAnnot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvAnnot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAnnot.Location = New System.Drawing.Point(6, 6)
        Me.dgvAnnot.Name = "dgvAnnot"
        Me.dgvAnnot.Size = New System.Drawing.Size(604, 311)
        Me.dgvAnnot.TabIndex = 291
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtProbFormat)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.rbPercent)
        Me.GroupBox1.Controls.Add(Me.rbDecimal)
        Me.GroupBox1.Controls.Add(Me.txtUpperValCDF)
        Me.GroupBox1.Controls.Add(Me.txtLowerValCDF)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.chkShowOnPDF)
        Me.GroupBox1.Controls.Add(Me.txtRVIntervalProb)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtRVUpperVal)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtRVLowerVal)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 351)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(460, 149)
        Me.GroupBox1.TabIndex = 295
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Interval Probability"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Lower value:"
        '
        'txtRVLowerVal
        '
        Me.txtRVLowerVal.Location = New System.Drawing.Point(130, 41)
        Me.txtRVLowerVal.Name = "txtRVLowerVal"
        Me.txtRVLowerVal.Size = New System.Drawing.Size(150, 20)
        Me.txtRVLowerVal.TabIndex = 1
        '
        'txtRVUpperVal
        '
        Me.txtRVUpperVal.Location = New System.Drawing.Point(130, 67)
        Me.txtRVUpperVal.Name = "txtRVUpperVal"
        Me.txtRVUpperVal.Size = New System.Drawing.Size(150, 20)
        Me.txtRVUpperVal.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 70)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 13)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "Upper value:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 25)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(126, 13)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Random Variable Range:"
        '
        'txtRVIntervalProb
        '
        Me.txtRVIntervalProb.Location = New System.Drawing.Point(130, 93)
        Me.txtRVIntervalProb.Name = "txtRVIntervalProb"
        Me.txtRVIntervalProb.ReadOnly = True
        Me.txtRVIntervalProb.Size = New System.Drawing.Size(150, 20)
        Me.txtRVIntervalProb.TabIndex = 6
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 96)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(118, 13)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "Probability within range:"
        '
        'chkShowOnPDF
        '
        Me.chkShowOnPDF.AutoSize = True
        Me.chkShowOnPDF.Location = New System.Drawing.Point(9, 120)
        Me.chkShowOnPDF.Name = "chkShowOnPDF"
        Me.chkShowOnPDF.Size = New System.Drawing.Size(92, 17)
        Me.chkShowOnPDF.TabIndex = 39
        Me.chkShowOnPDF.Text = "Show on PDF"
        Me.chkShowOnPDF.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(286, 44)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(70, 13)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "Probability <="
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(286, 70)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 13)
        Me.Label15.TabIndex = 41
        Me.Label15.Text = "Probability <="
        '
        'txtLowerValCDF
        '
        Me.txtLowerValCDF.Location = New System.Drawing.Point(362, 41)
        Me.txtLowerValCDF.Name = "txtLowerValCDF"
        Me.txtLowerValCDF.ReadOnly = True
        Me.txtLowerValCDF.Size = New System.Drawing.Size(84, 20)
        Me.txtLowerValCDF.TabIndex = 42
        '
        'txtUpperValCDF
        '
        Me.txtUpperValCDF.Location = New System.Drawing.Point(362, 67)
        Me.txtUpperValCDF.Name = "txtUpperValCDF"
        Me.txtUpperValCDF.ReadOnly = True
        Me.txtUpperValCDF.Size = New System.Drawing.Size(84, 20)
        Me.txtUpperValCDF.TabIndex = 43
        '
        'rbDecimal
        '
        Me.rbDecimal.AutoSize = True
        Me.rbDecimal.Location = New System.Drawing.Point(130, 119)
        Me.rbDecimal.Name = "rbDecimal"
        Me.rbDecimal.Size = New System.Drawing.Size(63, 17)
        Me.rbDecimal.TabIndex = 44
        Me.rbDecimal.TabStop = True
        Me.rbDecimal.Text = "Decimal"
        Me.rbDecimal.UseVisualStyleBackColor = True
        '
        'rbPercent
        '
        Me.rbPercent.AutoSize = True
        Me.rbPercent.Location = New System.Drawing.Point(199, 119)
        Me.rbPercent.Name = "rbPercent"
        Me.rbPercent.Size = New System.Drawing.Size(62, 17)
        Me.rbPercent.TabIndex = 45
        Me.rbPercent.TabStop = True
        Me.rbPercent.Text = "Percent"
        Me.rbPercent.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(286, 96)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(42, 13)
        Me.Label16.TabIndex = 46
        Me.Label16.Text = "Format:"
        '
        'txtProbFormat
        '
        Me.txtProbFormat.Location = New System.Drawing.Point(362, 93)
        Me.txtProbFormat.Name = "txtProbFormat"
        Me.txtProbFormat.Size = New System.Drawing.Size(84, 20)
        Me.txtProbFormat.TabIndex = 47
        '
        'frmDistribChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 717)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnCopyChart)
        Me.Controls.Add(Me.chkLegend)
        Me.Controls.Add(Me.chkRevCDF)
        Me.Controls.Add(Me.chkCDF)
        Me.Controls.Add(Me.chkPDF)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmDistribChart"
        Me.Text = "Random Variable Distribution Chart"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvAnnot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label9 As Label
    Friend WithEvents txtUnits As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtParamE As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtParamD As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtParamC As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtParamB As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtDescr As TextBox
    Friend WithEvents chkRevCDF As CheckBox
    Friend WithEvents chkCDF As CheckBox
    Friend WithEvents chkPDF As CheckBox
    Friend WithEvents txtRandVarName As TextBox
    Friend WithEvents txtParamA As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbDistribution As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents chkLegend As CheckBox
    Friend WithEvents btnCopyChart As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents btnFormatHelp2 As Button
    Friend WithEvents btnUpdateAnnotation As Button
    Friend WithEvents btnDeleteAnnot As Button
    Friend WithEvents dgvAnnot As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtRVUpperVal As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtRVLowerVal As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents chkShowOnPDF As CheckBox
    Friend WithEvents txtRVIntervalProb As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents rbPercent As RadioButton
    Friend WithEvents rbDecimal As RadioButton
    Friend WithEvents txtUpperValCDF As TextBox
    Friend WithEvents txtLowerValCDF As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents txtProbFormat As TextBox
    Friend WithEvents Label16 As Label
End Class
