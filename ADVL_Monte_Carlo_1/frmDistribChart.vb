Imports System.Windows.Forms.DataVisualization.Charting

Public Class frmDistribChart
    'This form displays a distribution chart.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    ''The distribution parameters:
    'Dim ParamA As Double
    'Dim ParamB As Double
    'Dim ParamC As Double
    'Dim ParamD As Double
    'Dim ParamE As Double

    Public Data As New DataSet  'Dataset used to hold the distrbution data values



#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _formNo As Integer = 0 'Multiple instances of this form can be displayed. FormNo is the index number of the form in RtfDisplayFormList.
    Public Property FormNo As Integer
        Get
            Return _formNo
        End Get
        Set(ByVal value As Integer)
            _formNo = value
        End Set
    End Property

    Private _indexNo As Integer = -1 'The index number of the Variable in MonteCarlo.DataInfo()
    Property IndexNo As Integer
        Get
            Return _indexNo
        End Get
        Set(value As Integer)
            _indexNo = value
        End Set
    End Property

    Private _variableName As String = "" 'The name of the random variable.
    Public Property VariableName As String
        Get
            Return _variableName
        End Get
        Set(value As String)
            _variableName = value
            'txtFileName.Text = _fileName
            txtRandVarName.Text = _variableName
        End Set
    End Property

    Private _tableName As String = "" 'The name of the table that contains the Variable named VariableName
    Public Property TableName As String
        Get
            Return _tableName
        End Get
        Set(value As String)
            _tableName = value
        End Set
    End Property

    Private _units As String = "" 'The units of the random variable
    Property Units As String
        Get
            Return _units
        End Get
        Set(value As String)
            _units = value
            txtUnits.Text = _units
        End Set
    End Property

    Private _unitsAbbrev As String = "" 'The units abbreviation of the random variable
    Property UnitsAbbrev As String
        Get
            Return _unitsAbbrev
        End Get
        Set(value As String)
            _unitsAbbrev = value
        End Set
    End Property

    Private _description As String = "" 'A description of the random variable
    Property Description As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
            txtDescr.Text = _description
        End Set
    End Property

    Private _distributionName As String = "" 'The name of the selected distribution.
    Property DistributionName As String
        Get
            Return _distributionName
        End Get
        Set(value As String)
            _distributionName = value
            'txtDistributionName.Text = _distributionName
            'ShowDistributionInfo(_distributionName)
            UpdateDistribInfo()
        End Set
    End Property

    Private _showPDF As Boolean = True 'If true, the probability density distribution is shown.
    Property ShowPDF As Boolean
        Get
            Return _showPDF
        End Get
        Set(value As Boolean)
            _showPDF = value
            If chkPDF.Focused Then

            Else
                chkPDF.Checked = _showPDF
            End If
        End Set
    End Property

    Private _pdfLineColor As Color = Color.Black 'The color of the PDF line.
    Property PDFLineColor As Color
        Get
            Return _pdfLineColor
        End Get
        Set(value As Color)
            _pdfLineColor = value
        End Set
    End Property

    Private _pdfLineThickness As Integer = 1 'The width of the PDF line.
    Property PDFLineThickness As Integer
        Get
            Return _pdfLineThickness
        End Get
        Set(value As Integer)
            _pdfLineThickness = value
        End Set
    End Property

    Private _showCDF As Boolean = False 'If true, the cumulative distribution function is shown.
    Property ShowCDF As Boolean
        Get
            Return _showCDF
        End Get
        Set(value As Boolean)
            _showCDF = value
            If chkCDF.Focused Then

            Else
                chkCDF.Checked = _showCDF
            End If
        End Set
    End Property

    Private _cdfLineColor As Color = Color.Black 'The color of the CDF line.
    Property CDFLineColor As Color
        Get
            Return _cdfLineColor
        End Get
        Set(value As Color)
            _cdfLineColor = value
        End Set
    End Property

    Private _cdfLineThickness As Integer = 1 'The width of the CDF line.
    Property CDFLineThickness As Integer
        Get
            Return _cdfLineThickness
        End Get
        Set(value As Integer)
            _cdfLineThickness = value
        End Set
    End Property

    Private _showRevCDF As Boolean = False 'If true, the reverse cumulative distribution function is shown.
    Property ShowRevCDF As Boolean
        Get
            Return _showRevCDF
        End Get
        Set(value As Boolean)
            _showRevCDF = value
            If chkRevCDF.Focused Then

            Else
                chkRevCDF.Checked = _showRevCDF
            End If
        End Set
    End Property

    Private _revCdfLineColor As Color = Color.Black 'The color of the reverse CDF line.
    Property RevCDFLineColor As Color
        Get
            Return _revCdfLineColor
        End Get
        Set(value As Color)
            _revCdfLineColor = value
        End Set
    End Property

    Private _revCdfLineThickness As Integer = 1 'The width of the reverse CDF line.
    Property RevCDFLineThickness As Integer
        Get
            Return _revCdfLineThickness
        End Get
        Set(value As Integer)
            _revCdfLineThickness = value
        End Set
    End Property

    Private _showLegend As Boolean = False 'If True, the series legend is shown on the chart
    Property ShowLegend As Boolean
        Get
            Return _showLegend
        End Get
        Set(value As Boolean)
            _showLegend = value
            If _showLegend = True Then
                chkLegend.Checked = True
            Else
                chkLegend.Checked = False
            End If
        End Set
    End Property

    Private _nPoints As Integer = 100 'The number of points in the distribution data.
    Property NPoints As Integer
        Get
            Return _nPoints
        End Get
        Set(value As Integer)
            _nPoints = value
        End Set
    End Property

    Private _xMin As Double = -5 'The minimum x (random variable) value in the distribution data.
    Property XMin As Double
        Get
            Return _xMin
        End Get
        Set(value As Double)
            _xMin = value
        End Set
    End Property

    Private _xMax As Double = 5 'The maximum x (random variable) value in the distribution data.
    Property XMax As Double
        Get
            Return _xMax
        End Get
        Set(value As Double)
            _xMax = value
        End Set
    End Property

    Private _autoXMin As Boolean = True 'If True, XMin and XMax will be updated automatically.
    Property AutoXMin As Boolean
        Get
            Return _autoXMin
        End Get
        Set(value As Boolean)
            _autoXMin = value
        End Set
    End Property

    Private _autoXMax As Boolean = True 'If True, XMin and XMax will be updated automatically.
    Property AutoXMax As Boolean
        Get
            Return _autoXMax
        End Get
        Set(value As Boolean)
            _autoXMax = value
        End Set
    End Property

    Private _yMax As Double = 5 'The maximum Y (probability density) value in the distribution data.
    Property YMax As Double
        Get
            Return _yMax
        End Get
        Set(value As Double)
            _yMax = value
        End Set
    End Property

    Private _autoYMax As Boolean = True 'If True, and YMax will be updated automatically.
    Property AutoYMax As Boolean
        Get
            Return _autoYMax
        End Get
        Set(value As Boolean)
            _autoYMax = value
        End Set
    End Property


    Private _gridInterval As Single = 0 'The chart X grid interval
    Property GridInterval As Single
        Get
            Return _gridInterval
        End Get
        Set(value As Single)
            _gridInterval = value
        End Set
    End Property

    Private _paramA As Double = 1 'Probability distribution Parameter A
    Property ParamA As Double
        Get
            Return _paramA
        End Get
        Set(value As Double)
            _paramA = value
            If txtParamA.Focused Then

            Else
                If txtParamA.Enabled Then
                    txtParamA.Text = _paramA
                    UpdateChartRange()
                Else
                    txtParamA.Text = ""
                End If
            End If
        End Set
    End Property

    Private _paramB As Double = 1 'Probability distribution Parameter B
    Property ParamB As Double
        Get
            Return _paramB
        End Get
        Set(value As Double)
            _paramB = value
            If txtParamB.Focused Then

            Else
                'txtParamB.Text = _paramB
                If txtParamB.Enabled Then
                    txtParamB.Text = _paramB
                    UpdateChartRange()
                Else
                    txtParamB.Text = ""
                End If
            End If
        End Set
    End Property

    Private _paramC As Double = 1 'Probability distribution Parameter C
    Property ParamC As Double
        Get
            Return _paramC
        End Get
        Set(value As Double)
            _paramC = value
            If txtParamC.Focused Then

            Else
                'txtParamC.Text = _paramC
                If txtParamC.Enabled Then
                    txtParamC.Text = _paramC
                    UpdateChartRange()
                Else
                    txtParamC.Text = ""
                End If
            End If
        End Set
    End Property

    Private _paramD As Double = 1 'Probability distribution Parameter D
    Property ParamD As Double
        Get
            Return _paramD
        End Get
        Set(value As Double)
            _paramD = value
            If txtParamD.Focused Then

            Else
                'txtParamD.Text = _paramD
                If txtParamD.Enabled Then
                    txtParamD.Text = _paramD
                    UpdateChartRange()
                Else
                    txtParamD.Text = ""
                End If
            End If
        End Set
    End Property

    Private _paramE As Double = 1 'Probability distribution Parameter E
    Property ParamE As Double
        Get
            Return _paramE
        End Get
        Set(value As Double)
            _paramE = value
            If txtParamE.Focused Then

            Else
                If txtParamE.Enabled Then
                    txtParamE.Text = _paramE
                    UpdateChartRange()
                Else
                    txtParamE.Text = ""
                End If
            End If
        End Set
    End Property

    Private _isContinuous As Boolean = True
    Property IsContinuous As Boolean
        Get
            Return _isContinuous
        End Get
        Set(value As Boolean)
            _isContinuous = value
            If _isContinuous Then
                chkPDF.Text = "PDF" 'Probability Distribution Function
            Else
                chkPDF.Text = "PMF" 'Probability Mass Function
            End If
        End Set
    End Property

    Private _dataMean As Double 'The Mean of the data used to plot the PDF (or PMF)
    Property DataMean As Double
        Get
            Return _dataMean
        End Get
        Set(value As Double)
            _dataMean = value
        End Set
    End Property

    Private _dataStdDev As Double 'The Standard Deviation of the data used to plot the PDF (or PMF)
    Property DataStdDev As Double
        Get
            Return _dataStdDev
        End Get
        Set(value As Double)
            _dataStdDev = value
        End Set
    End Property

    Private _rVLowerVal As Double = 0 'The lower value of the Random Variable for an interval
    Property RVLowerVal As Double
        Get
            Return _rVLowerVal
        End Get
        Set(value As Double)
            _rVLowerVal = value
            If txtRVLowerVal.Focused Then
            Else
                txtRVLowerVal.Text = _rVLowerVal
            End If
            UpdateIntervalProb()
        End Set
    End Property

    Private _rVUpperVal As Double = 0 'The upper value of the Random Variable for an interval
    Property RVUpperVal As Double
        Get
            Return _rVUpperVal
        End Get
        Set(value As Double)
            _rVUpperVal = value
            If txtRVUpperVal.Focused Then
            Else
                txtRVUpperVal.Text = _rVUpperVal
            End If
            UpdateIntervalProb()
        End Set
    End Property

    Private _lowerValCDF As Double = 0 'The probability that the Random Variable value is less than or equal to the Lower Value
    Property LowerValCDF As Double
        Get
            Return _lowerValCDF
        End Get
        Set(value As Double)
            _lowerValCDF = value
            'txtLowerValCDF.Text = Format(_lowerValCDF, _probFormat)
        End Set
    End Property

    Private _upperValCDF As Double = 0 'The probability that the Random Variable value is less than or equal to the Upper Value
    Property UpperValCDF As Double
        Get
            Return _upperValCDF
        End Get
        Set(value As Double)
            _upperValCDF = value
            'txtUpperValCDF.Text = Format(_upperValCDF, _probFormat)
        End Set
    End Property

    Private _rVIntervalProb As Double = 0 'The probability that the Random Varialbe value lies between RVLowerVal and RVUpperVal
    Property RVIntervalProb As Double
        Get
            Return _rVIntervalProb
        End Get
        Set(value As Double)
            _rVIntervalProb = value
            'txtRVIntervalProb.Text = Format(_rVIntervalProb, _probFormat)
        End Set
    End Property

    Property _showProbPercent As Boolean = True 'If True the probability is displayed as a percent
    Property ShowProbPercent As Boolean
        Get
            Return _showProbPercent
        End Get
        Set(value As Boolean)
            _showProbPercent = value
            If _showProbPercent Then
                If rbPercent.Focused Then
                Else
                    rbPercent.Checked = True
                End If
            Else
                If rbDecimal.Focused Then
                Else
                    rbDecimal.Checked = True
                End If
            End If
            UpdateIntervalProb()
        End Set
    End Property

    Private _probFormat As String = "N2" 'The format string used to display the probabilisty values
    Property ProbFormat As String
        Get
            Return _probFormat
        End Get
        Set(value As String)
            _probFormat = value
            If txtProbFormat.Focused Then
            Else
                txtProbFormat.Text = _probFormat
            End If
        End Set
    End Property

    Private _showPDFIntervalProb As Boolean = False 'If True the Random Variable interval and probability is shown on the PDF chart.
    Property ShowPDFIntervalProb As Boolean
        Get
            Return _showPDFIntervalProb
        End Get
        Set(value As Boolean)
            _showPDFIntervalProb = value
            If _showPDFIntervalProb Then
                If chkShowOnPDF.Focused Then
                Else
                    chkShowOnPDF.Checked = True
                End If
            Else
                If chkShowOnPDF.Focused Then
                Else
                    chkShowOnPDF.Checked = False
                End If
            End If
        End Set
    End Property



#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML files - Read and write XML files." '=====================================================================================================================================

    'Private Sub SaveFormSettings()
    Public Sub SaveFormSettings()
        'Save the form settings in an XML document.

        dgvAnnot.AllowUserToAddRows = False 'This removed the last blank line

        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <!---->
                               <Annotations>
                                   <%= From item In dgvAnnot.Rows
                                       Select
                                       <Item>
                                           <CdfChartChecked><%= item.Cells(0).Value %></CdfChartChecked>
                                           <HistogramChecked><%= item.Cells(1).Value %></HistogramChecked>
                                           <Type><%= item.Cells(2).Value %></Type>
                                           <Parameter><%= item.Cells(3).Value %></Parameter>
                                           <Label><%= item.Cells(4).Value %></Label>
                                           <Format><%= item.Cells(9).Value %></Format>
                                       </Item> %>
                               </Annotations>
                               <!---->
                               <RVLowerVal><%= RVLowerVal %></RVLowerVal>
                               <RVUpperVal><%= RVUpperVal %></RVUpperVal>
                               <ProbFormat><%= ProbFormat %></ProbFormat>
                               <ShowProbPercent><%= ShowProbPercent %></ShowProbPercent>
                               <ShowPDFIntervalProb><%= ShowPDFIntervalProb %></ShowPDFIntervalProb>
                           </FormSettings>

        dgvAnnot.AllowUserToAddRows = True

        'Add code to include other settings to save after the comment line <!---->

        ' <Format><%= item.Cells(7).Value %></Format>

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & VariableName & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    'Private Sub RestoreFormSettings()
    Public Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & VariableName & ".xml"

        If Main.Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Main.Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore form position and size:
            If Settings.<FormSettings>.<Left>.Value <> Nothing Then Me.Left = Settings.<FormSettings>.<Left>.Value
            If Settings.<FormSettings>.<Top>.Value <> Nothing Then Me.Top = Settings.<FormSettings>.<Top>.Value
            If Settings.<FormSettings>.<Height>.Value <> Nothing Then Me.Height = Settings.<FormSettings>.<Height>.Value
            If Settings.<FormSettings>.<Width>.Value <> Nothing Then Me.Width = Settings.<FormSettings>.<Width>.Value

            'Add code to read other saved setting here:

            'Restore annotations:
            dgvAnnot.Rows.Clear()
            Dim Annotations = From item In Settings.<FormSettings>.<Annotations>.<Item>
            For Each Item In Annotations
                If Item.<Format>.Value = Nothing Then
                    'dgvAnnot.Rows.Add(Item.<CdfChartChecked>.Value, Item.<HistogramChecked>.Value, Item.<Type>.Value, Item.<Parameter>.Value, Item.<Label>.Value, 0, 0, "")
                    dgvAnnot.Rows.Add(Item.<CdfChartChecked>.Value, Item.<HistogramChecked>.Value, Item.<Type>.Value, Item.<Parameter>.Value, Item.<Label>.Value, 0, 0, 0, 0, "")
                Else
                    'dgvAnnot.Rows.Add(Item.<CdfChartChecked>.Value, Item.<HistogramChecked>.Value, Item.<Type>.Value, Item.<Parameter>.Value, Item.<Label>.Value, 0, 0, Item.<Format>.Value)
                    dgvAnnot.Rows.Add(Item.<CdfChartChecked>.Value, Item.<HistogramChecked>.Value, Item.<Type>.Value, Item.<Parameter>.Value, Item.<Label>.Value, 0, 0, 0, 0, Item.<Format>.Value)
                End If
            Next

            'Restore Interval Probability settings:
            If Settings.<FormSettings>.<RVLowerVal>.Value <> Nothing Then RVLowerVal = Settings.<FormSettings>.<RVLowerVal>.Value
            If Settings.<FormSettings>.<RVUpperVal>.Value <> Nothing Then RVUpperVal = Settings.<FormSettings>.<RVUpperVal>.Value
            If Settings.<FormSettings>.<ProbFormat>.Value <> Nothing Then ProbFormat = Settings.<FormSettings>.<ProbFormat>.Value
            If Settings.<FormSettings>.<ShowProbPercent>.Value <> Nothing Then ShowProbPercent = Settings.<FormSettings>.<ShowProbPercent>.Value
            If Settings.<FormSettings>.<ShowPDFIntervalProb>.Value <> Nothing Then ShowPDFIntervalProb = Settings.<FormSettings>.<ShowPDFIntervalProb>.Value
            'UpdateIntervalProb()

            CheckFormPos()
        End If
    End Sub

    'Private Sub CheckFormPos()
    Public Sub CheckFormPos()
        'Check that the form can be seen on a screen.

        Dim MinWidthVisible As Integer = 192 'Minimum number of X pixels visible. The form will be moved if this many form pixels are not visible.
        Dim MinHeightVisible As Integer = 64 'Minimum number of Y pixels visible. The form will be moved if this many form pixels are not visible.

        Dim FormRect As New Rectangle(Me.Left, Me.Top, Me.Width, Me.Height)
        Dim WARect As Rectangle = Screen.GetWorkingArea(FormRect) 'The Working Area rectangle - the usable area of the screen containing the form.

        'Check if the top of the form is above the top of the Working Area:
        If Me.Top < WARect.Top Then
            Me.Top = WARect.Top
        End If

        'Check if the top of the form is too close to the bottom of the Working Area:
        If (Me.Top + MinHeightVisible) > (WARect.Top + WARect.Height) Then
            Me.Top = WARect.Top + WARect.Height - MinHeightVisible
        End If

        'Check if the left edge of the form is too close to the right edge of the Working Area:
        If (Me.Left + MinWidthVisible) > (WARect.Left + WARect.Width) Then
            Me.Left = WARect.Left + WARect.Width - MinWidthVisible
        End If

        'Check if the right edge of the form is too close to the left edge of the Working Area:
        If (Me.Left + Me.Width - MinWidthVisible) < WARect.Left Then
            Me.Left = WARect.Left - Me.Width + MinWidthVisible
        End If

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message) 'Save the form settings before the form is minimised:
        If m.Msg = &H112 Then 'SysCommand
            If m.WParam.ToInt32 = &HF020 Then 'Form is being minimised
                SaveFormSettings()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

#End Region 'Process XML Files ----------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Display Methods - Code used to display this form." '============================================================================================================================

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Continuous distribution options:
        cmbDistribution.Items.Add("C2 - Beta")
        cmbDistribution.Items.Add("C4 - Beta Scaled")
        'cmbDistribution.Items.Add("C3 - Burr")
        'cmbDistribution.Items.Add("Categorical")
        cmbDistribution.Items.Add("C2 - Cauchy")
        cmbDistribution.Items.Add("C1 - Chi Squared")
        cmbDistribution.Items.Add("C2 - Continuous Uniform")
        cmbDistribution.Items.Add("C1 - Exponential")
        cmbDistribution.Items.Add("C2 - Fisher-Snedecor")
        cmbDistribution.Items.Add("C2 - Gamma")
        cmbDistribution.Items.Add("C2 - Inverse Gaussian")
        cmbDistribution.Items.Add("C2 - Log Normal")
        cmbDistribution.Items.Add("C2 - Normal")
        cmbDistribution.Items.Add("C2 - Pareto")
        cmbDistribution.Items.Add("C1 - Rayleigh")
        cmbDistribution.Items.Add("C4 - Skewed Generalized Error")
        cmbDistribution.Items.Add("C5 - Skewed Generalized T")
        cmbDistribution.Items.Add("C3 - Student's T")
        cmbDistribution.Items.Add("C3 - Triangular")
        cmbDistribution.Items.Add("C3 - Truncated Pareto")

        'Discrete distribution options:
        cmbDistribution.Items.Add("D1 - Bernoulli")
        cmbDistribution.Items.Add("D2 - Binomial")
        cmbDistribution.Items.Add("D1 - Categorical")
        cmbDistribution.Items.Add("D2 - Conway-Maxwell-Poisson")
        cmbDistribution.Items.Add("D2 - Discrete Uniform")
        cmbDistribution.Items.Add("D1 - Geometric")
        cmbDistribution.Items.Add("D3 - Hypergeometric")
        cmbDistribution.Items.Add("D2 - Negative Binomial")
        cmbDistribution.Items.Add("D1 - Poisson")
        cmbDistribution.Items.Add("D2 - Zipf")



        'dgvAnnot.ColumnCount = 5
        dgvAnnot.ColumnCount = 7

        Dim chkAnnotCdf As New DataGridViewCheckBoxColumn
        dgvAnnot.Columns.Insert(0, chkAnnotCdf)
        dgvAnnot.Columns(0).HeaderText = "CDF Chart"
        dgvAnnot.Columns(0).Width = 40

        Dim chkAnnotHist As New DataGridViewCheckBoxColumn
        dgvAnnot.Columns.Insert(1, chkAnnotHist)
        'dgvAnnot.Columns(1).HeaderText = "Hist Chart"
        dgvAnnot.Columns(1).HeaderText = "PDF Chart"
        dgvAnnot.Columns(1).Width = 40

        Dim cboAnnotType As New DataGridViewComboBoxColumn
        cboAnnotType.Items.Add("Probability")
        cboAnnotType.Items.Add("Value")
        cboAnnotType.Items.Add("Mean")
        cboAnnotType.Items.Add("Standard Deviation")
        dgvAnnot.Columns.Insert(2, cboAnnotType)
        dgvAnnot.Columns(2).HeaderText = "Annotation Type"
        dgvAnnot.Columns(2).Width = 120

        dgvAnnot.Columns(3).HeaderText = "Parameter"
        dgvAnnot.Columns(3).Width = 80
        dgvAnnot.Columns(4).HeaderText = "Label"
        dgvAnnot.Columns(4).Width = 80
        'dgvAnnot.Columns(5).HeaderText = "Probability"
        dgvAnnot.Columns(5).HeaderText = "Probability Density"
        dgvAnnot.Columns(5).Width = 80

        dgvAnnot.Columns(6).HeaderText = "Cumulative Probability"
        dgvAnnot.Columns(6).Width = 80
        dgvAnnot.Columns(7).HeaderText = "Reverse Cumulative Probability"
        dgvAnnot.Columns(7).Width = 80




        'dgvAnnot.Columns(6).HeaderText = "Value"
        'dgvAnnot.Columns(6).Width = 80
        'dgvAnnot.Columns(7).HeaderText = "Format"
        'dgvAnnot.Columns(7).Width = 80

        dgvAnnot.Columns(8).HeaderText = "Value"
        dgvAnnot.Columns(8).Width = 80
        dgvAnnot.Columns(9).HeaderText = "Format"
        dgvAnnot.Columns(9).Width = 80






        ShowPDF = True
        ShowCDF = False
        ShowRevCDF = False

        RestoreFormSettings()   'Restore the form settings

        Chart1.SuppressExceptions = True

        'ShowPdfInterval() 'ERROR

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form

        Main.ClosedFormNo = FormNo

        Me.Close() 'Close the form
    End Sub

    Private Sub frmDistribChart_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Main.DistribChartFormClosed()
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        Else
            'Dont save settings if the form is minimised.
        End If
    End Sub



#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Private Sub txtParamA_TextChanged(sender As Object, e As EventArgs) Handles txtParamA.TextChanged
        ParamA = Val(txtParamA.Text)
    End Sub

    Private Sub txtParamB_TextChanged(sender As Object, e As EventArgs) Handles txtParamB.TextChanged
        ParamB = Val(txtParamB.Text)
    End Sub

    Private Sub txtParamC_TextChanged(sender As Object, e As EventArgs) Handles txtParamC.TextChanged
        ParamC = Val(txtParamC.Text)
    End Sub

    Private Sub txtParamD_TextChanged(sender As Object, e As EventArgs) Handles txtParamD.TextChanged
        ParamD = Val(txtParamD.Text)
    End Sub

    Private Sub txtParamE_TextChanged(sender As Object, e As EventArgs) Handles txtParamE.TextChanged
        ParamE = Val(txtParamE.Text)
    End Sub

    Private Sub cmbDistribution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDistribution.SelectedIndexChanged
        If cmbDistribution.SelectedIndex = -1 Then
            Main.Message.AddWarning("The distribution type has not been selected." & vbCrLf)
        Else
            DistributionName = cmbDistribution.SelectedItem.ToString
        End If
    End Sub

    Private Sub UpdateDistribInfo()
        'Update the Distribution information.

        If cmbDistribution.Focused Then

        Else
            cmbDistribution.SelectedIndex = cmbDistribution.FindStringExact(DistributionName)
        End If

        Select Case DistributionName
            Case "D1 - Bernoulli"
                SetNParams(1)
                Label4.Text = "Prob"
                IsContinuous = False

            Case "C2 - Beta"
                SetNParams(2)
                Label4.Text = "Alpha"
                Label5.Text = "Beta"
                IsContinuous = True

            Case "C4 - Beta Scaled"
                SetNParams(4)
                Label4.Text = "Alpha"
                Label5.Text = "Beta"
                Label6.Text = "Mu"
                Label7.Text = "Sigma"
                IsContinuous = True

            Case "D2 - Binomial"
                SetNParams(2)
                Label4.Text = "P Success"
                Label5.Text = "N Trials"
                IsContinuous = False

            'Case "Burr"
            '    SetNParams(3)
            '    Label4.Text = "Alpha"
            '    Label5.Text = "c"
            '    Label6.Text = "k"

            Case "D1 - Categorical"
                SetNParams(1)
                Label4.Text = "Prob()"
                IsContinuous = False

            Case "C2 - Cauchy"
                SetNParams(2)
                SetNParams(3)
                Label4.Text = "x0"
                Label5.Text = "Gamma"
                IsContinuous = True

            'Case "Chi"
            '    SetNParams(1)
            '    Label4.Text = "Freedom"

            Case "C1 - Chi Squared"
                SetNParams(1)
                Label4.Text = "Freedom"
                IsContinuous = True

            Case "C2 - Continuous Uniform"
                SetNParams(2)
                Label4.Text = "Lower"
                Label5.Text = "Upper"
                IsContinuous = True

            Case "D2 - Conway-Maxwell-Poisson"
                SetNParams(2)
                Label4.Text = "Lambda"
                Label5.Text = "Nu"
                IsContinuous = False

            Case "D2 - Discrete Uniform"
                SetNParams(2)
                Label4.Text = "Lower"
                Label5.Text = "Upper"
                IsContinuous = False

            'Case "Erlang"
            '    SetNParams(2)
            '    Label4.Text = "Shape"
            '    Label5.Text = "Rate"

            Case "C1 - Exponential"
                SetNParams(1)
                Label4.Text = "Rate"
                IsContinuous = True

            Case "C2 - Fisher-Snedecor"
                SetNParams(2)
                IsContinuous = True

            Case "C2 - Gamma"
                SetNParams(2)
                IsContinuous = True

            Case "D1 - Geometric"
                SetNParams(1)
                IsContinuous = False

            Case "D3 - Hypergeometric"
                SetNParams(3)
                IsContinuous = False

            'Case "Inverse Gamma"
            '    SetNParams(2)

            Case "C2 - Inverse Gaussian"
                SetNParams(2)
                IsContinuous = True

            'Case "Laplace"
            '    SetNParams(2)

            Case "C2 - Log Normal"
                SetNParams(2)
                IsContinuous = True

            Case "D2 - Negative Binomial"
                SetNParams(2)
                IsContinuous = False

            Case "C2 - Normal"
                SetNParams(2)
                Label4.Text = "Mean" '& " (" & Chr(230) & ")" '(Mu)
                Label5.Text = "Std Dev" '& " (" & Chr(236) & ")" '(Sigma)
                IsContinuous = True

            Case "C2 - Pareto"
                SetNParams(2)
                IsContinuous = True

            Case "D1 - Poisson"
                SetNParams(1)
                IsContinuous = False

            Case "C1 - Rayleigh"
                SetNParams(1)
                IsContinuous = True

            Case "C4 - Skewed Generalized Error"
                SetNParams(4)
                IsContinuous = True

            Case "C5 - Skewed Generalized T"
                SetNParams(5)

            'Case "Stable"
            '    SetNParams(4)

            Case "C3 - Student's T"
                SetNParams(3)
                IsContinuous = True

            Case "C3 - Triangular"
                SetNParams(3)
                IsContinuous = True

            Case "C3 - Truncated Pareto"
                SetNParams(3)
                IsContinuous = True

            'Case "Weibull"
            '    SetNParams(2)

            Case "D2 - Zipf"
                SetNParams(2)
                IsContinuous = False

        End Select
    End Sub



    Private Sub SetNParams(ByVal NParams As Integer)
        'Set the number of parameters for the selected distribution.

        Select Case NParams
            Case 1
                Label4.Enabled = True
                txtParamA.Enabled = True
                Label5.Enabled = False
                txtParamB.Enabled = False
                Label6.Enabled = False
                txtParamC.Enabled = False
                Label7.Enabled = False
                txtParamD.Enabled = False
                Label8.Enabled = False
                txtParamE.Enabled = False
            Case 2
                Label4.Enabled = True
                txtParamA.Enabled = True
                Label5.Enabled = True
                txtParamB.Enabled = True
                Label6.Enabled = False
                txtParamC.Enabled = False
                Label7.Enabled = False
                txtParamD.Enabled = False
                Label8.Enabled = False
                txtParamE.Enabled = False
            Case 3
                Label4.Enabled = True
                txtParamA.Enabled = True
                Label5.Enabled = True
                txtParamB.Enabled = True
                Label6.Enabled = True
                txtParamC.Enabled = True
                Label7.Enabled = False
                txtParamD.Enabled = False
                Label8.Enabled = False
                txtParamE.Enabled = False
            Case 4
                Label4.Enabled = True
                txtParamA.Enabled = True
                Label5.Enabled = True
                txtParamB.Enabled = True
                Label6.Enabled = True
                txtParamC.Enabled = True
                Label7.Enabled = True
                txtParamD.Enabled = True
                Label8.Enabled = False
                txtParamE.Enabled = False
            Case 5
                Label4.Enabled = True
                txtParamA.Enabled = True
                Label5.Enabled = True
                txtParamB.Enabled = True
                Label6.Enabled = True
                txtParamC.Enabled = True
                Label7.Enabled = True
                txtParamD.Enabled = True
                Label8.Enabled = True
                txtParamE.Enabled = True
            Case Else
                Main.Message.AddWarning("Unknown number of distribution parameters: " & NParams & vbCrLf)
        End Select
    End Sub


    Private Sub UpdateChartRange()
        'Update XMin and XMax.

        If AutoXMin Or AutoXMax Then
            Select Case DistributionName

                Case "D1 - Bernoulli" 'Not currently Random Variable option.
                    'The Bernoulli Distribution is defined between 0 and 1
                    If AutoXMin Then XMin = 0
                    If AutoXMax Then XMax = 1

                Case "C2 - Beta"
                    'The Beta Distribution is defined between 0 and 1
                    If AutoXMin Then XMin = 0
                    If AutoXMax Then XMax = 1

                Case "C4 - Beta Scaled"
                    'ParamA = , ParamB = , ParamC = , ParamD = 
                    'To Do

                Case "D2 - Binomial" 'Not currently Random Variable option.

                'Case "Burr"


                Case "D1 - Categorical" 'Not currently Random Variable option.

                Case "C2 - Cauchy"
                    'ParamA = x0 (Location), ParamB = Gamma (Scale) (Half-width at half-maximum
                    If AutoXMin Then XMin = ParamA - 3 * ParamB
                    If AutoXMax Then XMax = ParamA + 3 * ParamB

                    'Case "Chi" 'Not currently Random Variable option.

                Case "C1 - Chi Squared"
                    'ParamA = k (Freedom) (Degrees of freedom)
                    If AutoXMin Then XMin = 0
                    If AutoXMax Then XMax = 3 * ParamB 'FIRST ATTEMPT - CHECK FOR BETTER AUTO MAX 

                Case "C2 - Continuous Uniform"
                    'ParamA = a (Lower value), ParamB = b (Upper value)
                    If AutoXMin Then XMin = ParamA - (ParamB - ParamA) / 4 'This is just less than the lower value
                    If AutoXMax Then XMin = ParamB + (ParamB - ParamA) / 4 'This is just more than the upper value

                Case "D2 - Conway-Maxwell-Poisson" 'Not currently Random Variable option.

                Case "D2 - Discrete Uniform" 'Not currently Random Variable option.

                Case "Erlang" 'Not currently Random Variable option.

                Case "C1 - Exponential"
                    'ParamA = Lambda (Rate)
                    If AutoXMin Then XMin = 0
                    If AutoXMax Then XMax = 10 'TO BE IMPROVED

                Case "C2 - Fisher-Snedecor"
                    'ParamA = d1 (First degree of freedom), ParamB = d2 (Second degree of freedom)
                    If AutoXMin Then XMin = 0
                    If AutoXMax Then XMax = 5 'TO BE IMPROVED

                Case "C2 - Gamma"
                    'ParamA = Alpha (Shape), ParamB = Beta (Rate)
                    If AutoXMin Then XMin = 0
                    If AutoXMax Then XMax = 20 'TO BE IMPROVED

                Case "D1 - Geometric" 'Not currently Random Variable option.

                Case "D3 - Hypergeometric" 'Not currently Random Variable option.

                Case "Inverse Gamma" 'Not currently Random Variable option.

                Case "C2 - Inverse Gaussian"
                    'ParamA = Mu (Mean), ParamB = Lambda (Shape)
                    If AutoXMin Then XMin = 0
                    If AutoXMax Then XMax = 3 'TO BE IMPROVED

                Case "Laplace" 'Not currently Random Variable option.

                Case "C2 - Log Normal"
                    'ParamA = Mu (Scale), ParamB = Sigma (Shape)
                    If AutoXMin Then XMin = 0
                    If AutoXMax Then XMax = 3 'TO BE IMPROVED

                Case "D2 - Negative Binomial" 'Not currently Random Variable option.

                Case "C2 - Normal"
                    'ParamA = Mean, ParamB = Std Dev
                    If AutoXMin Then XMin = ParamA - 5 * ParamB 'NOTE: This sets the minimum Random Variable range over which the distribution chart data will be generated
                    If AutoXMax Then XMax = ParamA + 5 * ParamB 'NOTE: This sets the maximum Random Variable range over which the distribution chart data will be generated

                Case "C2 - Pareto"
                    'ParamA = xm (Scale), ParamB = Alpha (Shape)
                    If AutoXMin Then XMin = 0
                    If AutoXMax Then XMax = 5 'TO BE IMPROVED

                Case "D1 - Poisson" 'Not currently Random Variable option.

                Case "C1 - Rayleigh"
                    'ParamA = Sigma (Scale)
                    If AutoXMin Then XMin = 0
                    If AutoXMax Then XMax = 10'TO BE IMPROVED

                Case "C4 - Skewed Generalized Error"
                    'ParamA = Mu (Location), ParamB = Sigma (Scale), ParamC = Lambda (Skew), ParamD = p (Kurtosis)
                    'To do

                Case "C5 - Skewed Generalized T"
                      'ParamA = Mu (Location), ParamB = Sigma (Scale), ParamC = Lambda (Skew), ParamD = p (First kurtosis parameter), ParamE = q (Second kurtosis parameter)
                      'To do

                Case "Stable" 'Not currently Random Variable option.

                Case "C3 - Student's T"
                    'ParamA = Mu (Location), ParamB = Sigma (Scale), ParamC = Nu (Freedom)
                    If AutoXMin Then XMin = -5 'TO BE IMPROVED
                    If AutoXMax Then XMax = 5 'TO BE IMPROVED

                Case "C3 - Triangular"
                    'ParamA = a (Minimum value), ParamB = b (Maximum value), ParamC = c (Peak value)
                    If AutoXMin Then XMin = ParamA - (ParamB - ParamA) / 4 'This is just less than the lower value
                    If AutoXMax Then XMax = ParamB + (ParamB - ParamA) / 4 'This is just more than the upper value

                Case "C3 - Truncated Pareto"
                    'ParamA = xm (Scale), ParamB = Alpha (Shape), ParamC = T (Truncation)
                    If AutoXMin Then XMin = ParamA - (ParamC - ParamA) / 4 'This is just less than the lower value
                    If AutoXMax Then XMax = ParamC + (ParamC - ParamA) / 4 'This is just more than the upper value

                Case "Weibull" 'Not currently Random Variable option.

                Case "D2 - Zipf" 'Not currently Random Variable option.

                Case Else
                    'Unknown distribution.
            End Select
        End If
    End Sub

    Public Sub GenerateDistribution()
        'Generate the distribution data in the Data table

        Data.Clear()
        Data.Reset()

        Data.Tables.Add("DataTable")
        Data.Tables("DataTable").Columns.Add("Random_Variable", System.Type.GetType("System.Double"))

        UpdateChartRange() 'This sets automatic values for XMin or XMax if AutoXMin or AutoXMax are True

        Dim SampleInt As Double = (XMax - XMin) / (NPoints - 1)

        Dim I As Integer
        For I = 0 To NPoints - 1
            Data.Tables("DataTable").Rows.Add(XMin + I * SampleInt)
        Next

        'If ShowPDF Then GeneratePDF()
        GenerateData()

        Chart1.ChartAreas(0).AxisX.Minimum = XMin
        Chart1.ChartAreas(0).AxisX.Maximum = XMax
        Chart1.ChartAreas(0).AxisX.MajorGrid.Interval = GridInterval
        Chart1.ChartAreas(0).AxisX.Interval = GridInterval
        Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.##"
        Chart1.ChartAreas(0).AxisX.RoundAxisValues()
        If UnitsAbbrev = "" Then
            Chart1.ChartAreas(0).AxisX.Title = VariableName & " (" & Units & ")"
        Else
            Chart1.ChartAreas(0).AxisX.Title = VariableName & " (" & UnitsAbbrev & ")"
        End If
        Chart1.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

        If ShowPDF Then
            Try
                If IsContinuous Then
                    Chart1.Series(0).Name = "PDF"
                    Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Line
                Else
                    Chart1.Series(0).Name = "PMF"
                    Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Column
                End If

                Chart1.Series(0).Color = PDFLineColor
                Chart1.Series(0).BorderWidth = PDFLineThickness

                Chart1.Series(0).YAxisType = DataVisualization.Charting.AxisType.Primary

                If chkLegend.Checked Then
                    Chart1.Series(0).IsVisibleInLegend = True
                Else
                    Chart1.Series(0).IsVisibleInLegend = False
                End If

                If AutoYMax = True Then
                    Chart1.ChartAreas(0).AxisY.Maximum = Double.NaN
                Else
                    Chart1.ChartAreas(0).AxisY.Maximum = YMax
                End If

                'Chart1.ChartAreas(0).AxisX.Minimum = XMin
                'Chart1.ChartAreas(0).AxisX.Maximum = XMax
                'Chart1.ChartAreas(0).AxisX.MajorGrid.Interval = GridInterval
                'Chart1.ChartAreas(0).AxisX.Interval = GridInterval
                'Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.##"
                'Chart1.ChartAreas(0).AxisX.RoundAxisValues()
                'If UnitsAbbrev = "" Then
                '    Chart1.ChartAreas(0).AxisX.Title = VariableName & " (" & Units & ")"
                'Else
                '    Chart1.ChartAreas(0).AxisX.Title = VariableName & " (" & UnitsAbbrev & ")"
                'End If
                'Chart1.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

                If IsContinuous Then
                    Chart1.Series("PDF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "PDF")
                    'Chart1.ChartAreas(0).AxisY.Title = "Probability" & vbCrLf & "Density"
                    Chart1.ChartAreas(0).AxisY.Title = "Probability Density"
                    Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
                Else
                    Chart1.Series("PMF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "PMF")
                    'Chart1.ChartAreas(0).AxisY.Title = "Probability" & vbCrLf & "Mass"
                    Chart1.ChartAreas(0).AxisY.Title = "Probability Mass"
                    Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
                End If



            Catch ex As Exception
                Main.Message.AddWarning(ex.Message & vbCrLf)
            End Try
        End If


        If ShowCDF Then
            Dim CdfSeriesNo As Integer
            If Chart1.Series.Count > 0 Then
                If ShowPDF Then
                    If Chart1.Series.Count > 1 Then
                        CdfSeriesNo = 1
                        Chart1.Series(1).Name = "CDF"
                    Else
                        Chart1.Series.Add("CDF")
                    End If
                Else
                    CdfSeriesNo = 0
                    Chart1.Series(0).Name = "CDF"
                End If
            End If

            Chart1.Series(CdfSeriesNo).ChartType = DataVisualization.Charting.SeriesChartType.Line

            Chart1.Series(CdfSeriesNo).Color = CDFLineColor
            Chart1.Series(CdfSeriesNo).BorderWidth = CDFLineThickness

            Chart1.Series(CdfSeriesNo).YAxisType = DataVisualization.Charting.AxisType.Secondary

            If chkLegend.Checked Then
                Chart1.Series(CdfSeriesNo).IsVisibleInLegend = True
            Else
                Chart1.Series(CdfSeriesNo).IsVisibleInLegend = False
            End If

            Chart1.ChartAreas(0).AxisY2.Minimum = 0
            Chart1.ChartAreas(0).AxisY2.Maximum = 1

            Chart1.Series("CDF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "CDF")
            Chart1.ChartAreas(0).AxisY.Title = "Cumulative Probability"
            Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
        End If

        If ShowRevCDF Then
            Dim RevCdfSeriesNo As Integer
            If Chart1.Series.Count > 0 Then
                If ShowPDF Then
                    If ShowCDF Then
                        'Showing PDF and CDF and RevCDF
                        If Chart1.Series.Count > 2 Then
                            RevCdfSeriesNo = 2
                            Chart1.Series(2).Name = "Reverse_CDF"
                        Else
                            Chart1.Series.Add("Reverse_CDF")
                        End If
                    Else
                        'Showing PDF and RevCDF
                        If Chart1.Series.Count > 1 Then
                            RevCdfSeriesNo = 1
                            Chart1.Series(1).Name = "Reverse_CDF"
                        Else
                            Chart1.Series.Add("Reverse_CDF")
                        End If
                    End If

                Else
                    RevCdfSeriesNo = 0
                    Chart1.Series(0).Name = "Reverse_CDF"
                End If
            End If

            Chart1.Series(RevCdfSeriesNo).ChartType = DataVisualization.Charting.SeriesChartType.Line

            Chart1.Series(RevCdfSeriesNo).Color = RevCDFLineColor
            Chart1.Series(RevCdfSeriesNo).BorderWidth = RevCDFLineThickness

            Chart1.Series(RevCdfSeriesNo).YAxisType = DataVisualization.Charting.AxisType.Secondary

            If chkLegend.Checked Then
                Chart1.Series(RevCdfSeriesNo).IsVisibleInLegend = True
            Else
                Chart1.Series(RevCdfSeriesNo).IsVisibleInLegend = False
            End If

            Chart1.ChartAreas(0).AxisY2.Minimum = 0
            Chart1.ChartAreas(0).AxisY2.Maximum = 1

            Chart1.Series("Reverse_CDF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "Reverse_CDF")
            Chart1.ChartAreas(0).AxisY.Title = "Cumulative Probability"
            Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
        End If


    End Sub

    Public Sub ShowCharts()
        GenerateData()
        'UpdateIntervalProb()
        DisplayCharts()
        'UpdateIntervalProb()
    End Sub

    Private Sub DisplayCharts()
        'Display the probability distribution charts

        Chart1.Series.Clear()

        Chart1.Legends(0).Docking = DataVisualization.Charting.Docking.Bottom

        Chart1.ChartAreas(0).AxisX.Minimum = XMin
        Chart1.ChartAreas(0).AxisX.Maximum = XMax
        Chart1.ChartAreas(0).AxisX.MajorGrid.Interval = GridInterval
        Chart1.ChartAreas(0).AxisX.Interval = GridInterval
        Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.##"
        Chart1.ChartAreas(0).AxisX.RoundAxisValues()
        If UnitsAbbrev = "" Then
            Chart1.ChartAreas(0).AxisX.Title = VariableName & " (" & Units & ")"
        Else
            Chart1.ChartAreas(0).AxisX.Title = VariableName & " (" & UnitsAbbrev & ")"
        End If
        Chart1.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
        Chart1.ChartAreas(0).AxisY2.Minimum = 0
        Chart1.ChartAreas(0).AxisY2.Maximum = 1
        Chart1.ChartAreas(0).AxisY2.MajorGrid.Enabled = False
        Chart1.ChartAreas(0).AxisY2.Title = "Cumulative Probability"
        Chart1.ChartAreas(0).AxisY2.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)


        'Chart1.ChartAreas.Clear()
        Chart1.Series.Clear()

        'Generate the PDF chart:
        If IsContinuous Then
            Chart1.Series.Add("PDF")
            Chart1.Series("PDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series("PDF").Color = PDFLineColor
            Chart1.Series("PDF").BorderWidth = PDFLineThickness
            If chkLegend.Checked Then
                Chart1.Series("PDF").IsVisibleInLegend = True
            Else
                Chart1.Series("PDF").IsVisibleInLegend = False
            End If
            If AutoYMax = True Then
                'Chart1.ChartAreas("PDF").AxisY.Maximum = Double.NaN
                Chart1.ChartAreas(0).AxisY.Maximum = Double.NaN
            Else
                'Chart1.ChartAreas("PDF").AxisY.Maximum = YMax
                Chart1.ChartAreas(0).AxisY.Maximum = YMax
            End If
            'Chart1.Series("PDF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "PDF")
            'Chart1.ChartAreas(0).AxisY.Title = "Probability Density"
            'Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
            If ShowPDF Then
                Chart1.Series("PDF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "PDF")
                Chart1.ChartAreas(0).AxisY.Title = "Probability Density"
                Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
                Chart1.Series("PDF").Enabled = True
            Else
                Chart1.ChartAreas(0).AxisY.Minimum = 0
                Chart1.ChartAreas(0).AxisY.Maximum = 1
                Chart1.ChartAreas(0).AxisY.MajorGrid.Enabled = False
                Chart1.ChartAreas(0).AxisY.Title = "Cumulative Probability"
                Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

                Chart1.Series("PDF").Enabled = False
            End If
        Else
            Chart1.Series.Add("PMF")
            Chart1.Series("PMF").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("PMF").Color = PDFLineColor
            Chart1.Series("PMF").BorderWidth = PDFLineThickness
            If chkLegend.Checked Then
                Chart1.Series("PMF").IsVisibleInLegend = True
            Else
                Chart1.Series("PMF").IsVisibleInLegend = False
            End If
            If AutoYMax = True Then
                'Chart1.ChartAreas("PMF").AxisY.Maximum = Double.NaN
                Chart1.ChartAreas(0).AxisY.Maximum = Double.NaN
            Else
                'Chart1.ChartAreas("PMF").AxisY.Maximum = YMax
                Chart1.ChartAreas(0).AxisY.Maximum = YMax
            End If
            'Chart1.Series("PMF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "PMF")
            'Chart1.ChartAreas(0).AxisY.Title = "Probability Mass"
            'Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
            If ShowPDF Then
                Chart1.Series("PMF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "PMF")
                Chart1.ChartAreas(0).AxisY.Title = "Probability Mass"
                Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
                Chart1.Series("PMF").Enabled = True
            Else
                Chart1.ChartAreas(0).AxisY.Minimum = 0
                Chart1.ChartAreas(0).AxisY.Maximum = 1
                Chart1.ChartAreas(0).AxisY.MajorGrid.Enabled = False
                Chart1.ChartAreas(0).AxisY.Title = "Cumulative Probability"
                Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
                Chart1.Series("PMF").Enabled = False
            End If
        End If

        'Generate the CDF chart:
        Chart1.Series.Add("CDF")
        Chart1.Series("CDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
        Chart1.Series("CDF").Color = CDFLineColor
        Chart1.Series("CDF").BorderWidth = CDFLineThickness
        If ShowPDF Then
            Chart1.Series("CDF").YAxisType = DataVisualization.Charting.AxisType.Secondary
        Else
            Chart1.Series("CDF").YAxisType = DataVisualization.Charting.AxisType.Primary
        End If
        'Chart1.Series("CDF").YAxisType = DataVisualization.Charting.AxisType.Secondary
        If chkLegend.Checked Then
            Chart1.Series("CDF").IsVisibleInLegend = True
        Else
            Chart1.Series("CDF").IsVisibleInLegend = False
        End If
        Chart1.Series("CDF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "CDF")



        If ShowCDF Then
            Chart1.Series("CDF").Enabled = True
        Else
            Chart1.Series("CDF").Enabled = False
        End If

        'Generate the Reverse CDF chart:
        Chart1.Series.Add("Reverse_CDF")
        Chart1.Series("Reverse_CDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
        Chart1.Series("Reverse_CDF").Color = RevCDFLineColor
        Chart1.Series("Reverse_CDF").BorderWidth = RevCDFLineThickness
        If ShowPDF Then
            Chart1.Series("Reverse_CDF").YAxisType = DataVisualization.Charting.AxisType.Secondary
        Else
            Chart1.Series("Reverse_CDF").YAxisType = DataVisualization.Charting.AxisType.Primary
        End If
        'Chart1.Series("Reverse_CDF").YAxisType = DataVisualization.Charting.AxisType.Secondary
        If chkLegend.Checked Then
            Chart1.Series("Reverse_CDF").IsVisibleInLegend = True
        Else
            Chart1.Series("Reverse_CDF").IsVisibleInLegend = False
        End If
        Chart1.Series("Reverse_CDF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "Reverse_CDF")

        If ShowRevCDF Then
            Chart1.Series("Reverse_CDF").Enabled = True
        Else
            Chart1.Series("Reverse_CDF").Enabled = False
        End If

        'Add a series used to plot vertical bars on the CDF chart:
        Dim IndexNo As Integer = Chart1.Series.IndexOf("CdfVertBar")
        If IndexNo = -1 Then 'Series named CdfVerBar does not exist
            Chart1.Series.Add("CdfVertBar")
            Chart1.Series("CdfVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            'Chart1.Series("CdfVertBar").Color = Color.Orange
            Chart1.Series("CdfVertBar").Color = Color.DarkGray
            Chart1.Series("CdfVertBar").ChartArea = Chart1.ChartAreas(0).Name
            Chart1.Series("CdfVertBar").SetCustomProperty("PixelPointWidth", "3")
            Chart1.Series("CdfVertBar").IsVisibleInLegend = False
            Chart1.Series("CdfVertBar").YAxisType = DataVisualization.Charting.AxisType.Secondary
        Else

        End If

        'Add a series used to plot vertical bars on the Reverse CDF chart:
        IndexNo = Chart1.Series.IndexOf("RevCdfVertBar")
        If IndexNo = -1 Then 'Series named RevCdfVerBar does not exist
            Chart1.Series.Add("RevCdfVertBar")
            Chart1.Series("RevCdfVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("RevCdfVertBar").Color = Color.DarkGray
            Chart1.Series("RevCdfVertBar").ChartArea = Chart1.ChartAreas(0).Name
            Chart1.Series("RevCdfVertBar").SetCustomProperty("PixelPointWidth", "3")
            Chart1.Series("RevCdfVertBar").IsVisibleInLegend = False
            Chart1.Series("RevCdfVertBar").YAxisType = DataVisualization.Charting.AxisType.Secondary
        Else

        End If

        'Add a series used to plot vertical bars on the PDF chart:
        'Dim IndexNo As Integer = Chart1.Series.IndexOf("CdfVertBar")
        IndexNo = Chart1.Series.IndexOf("PdfVertBar")
        If IndexNo = -1 Then 'Series named PdfVerBar does not exist
            Chart1.Series.Add("PdfVertBar")
            Chart1.Series("PdfVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            'Chart1.Series("PdfVertBar").Color = Color.CadetBlue
            'Chart1.Series("PdfVertBar").Color = Color.Red
            Chart1.Series("PdfVertBar").Color = Color.Orange
            Chart1.Series("PdfVertBar").ChartArea = Chart1.ChartAreas(0).Name
            Chart1.Series("PdfVertBar").SetCustomProperty("PixelPointWidth", "5")
            Chart1.Series("PdfVertBar").IsVisibleInLegend = False
        Else

        End If

        'Add a series used to shade in a selected interval on the PDF chart:
        IndexNo = Chart1.Series.IndexOf("PdfShade")
        If IndexNo = -1 Then 'Series named PdfShade does not exist
            Chart1.Series.Add("PdfShade")
            Chart1.Series("PdfShade").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("PdfShade").Color = Color.LightGray
            Chart1.Series("PdfShade").ChartArea = Chart1.ChartAreas(0).Name
            Chart1.Series("PdfShade").SetCustomProperty("PixelPointWidth", "1")
            Chart1.Series("PdfShade").IsVisibleInLegend = False
        End If

        UpdateAnnotation()

    End Sub

    Private Sub GenerateData()
        'Generate the PDF, CDF and Reverse CDF data.

        'If IsContinuous Then
        '    If Data.Tables("DataTable").Columns.Contains("PDF") Then

        '    Else
        '        Data.Tables("DataTable").Columns.Add("PDF") 'Add Probability Density Function column
        '    End If
        'Else
        '    If Data.Tables("DataTable").Columns.Contains("PMF") Then

        '    Else
        '        Data.Tables("DataTable").Columns.Add("PMF") 'Add Probability Mass Function column
        '    End If
        'End If

        'If Data.Tables("DataTable").Columns.Contains("CDF") Then

        'Else
        '    Data.Tables("DataTable").Columns.Add("CDF") 'Add the Cumulative Distribution Function column
        'End If

        'If Data.Tables("DataTable").Columns.Contains("Reverse_CDF") Then

        'Else
        '    Data.Tables("DataTable").Columns.Add("Reverse_CDF") 'Add the Reverse Cumulative Distribution Function column
        'End If

        Data.Clear()
        Data.Reset()

        Data.Tables.Add("DataTable")
        Data.Tables("DataTable").Columns.Add("Random_Variable", System.Type.GetType("System.Double"))

        UpdateChartRange() 'This sets automatic values for XMin or XMax if AutoXMin or AutoXMax are True

        Dim SampleInt As Double = (XMax - XMin) / (NPoints - 1)

        Dim I As Integer
        For I = 0 To NPoints - 1
            Data.Tables("DataTable").Rows.Add(XMin + I * SampleInt)
        Next

        If IsContinuous Then
            Data.Tables("DataTable").Columns.Add("PDF") 'Add Probability Density Function column
        Else
            Data.Tables("DataTable").Columns.Add("PMF") 'Add Probability Mass Function column
        End If
        Data.Tables("DataTable").Columns.Add("CDF") 'Add the Cumulative Distribution Function column
        Data.Tables("DataTable").Columns.Add("Reverse_CDF") 'Add the Reverse Cumulative Distribution Function column

        Dim RandVar As Double
        Dim PDF As Double
        Dim Sum As Double = 0
        Dim NVals As Double = 0

        Try
            Dim CdfVal As Double
            Select Case DistributionName

                Case "C2 - Beta"
                    Dim DistVal As Double 'The calculated distribution value
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        DistVal = MathNet.Numerics.Distributions.Beta.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        If DistVal = Double.PositiveInfinity Then
                            Main.Message.Add("The Beta PDF is + infinity for a Random Variable value of " & Row.Item("Random_Variable") & " for parameters Alpha = " & ParamA & " and Beta = " & ParamB & "Random Variable" & vbCrLf)
                        ElseIf DistVal = Double.NegativeInfinity Then
                            Main.Message.Add("The Beta PDF is - infinity for a Random Variable value of " & Row.Item("Random_Variable") & " for parameters Alpha = " & ParamA & " and Beta = " & ParamB & vbCrLf)
                        ElseIf DistVal = Double.NaN Then
                            Main.Message.Add("The Beta PDF is not valid for a Random Variable value of " & Row.Item("Random_Variable") & " for parameters Alpha = " & ParamA & " and Beta = " & ParamB & vbCrLf)
                        Else
                            Row.Item("PDF") = DistVal
                        End If
                    Next

                Case "C4 - Beta Scaled"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.BetaScaled.PDF(ParamA, ParamB, ParamC, ParamD, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.BetaScaled.CDF(ParamA, ParamB, ParamC, ParamD, Row.Item("Random_Variable"))
                        'Row.Item("CDF") = MathNet.Numerics.Distributions.BetaScaled.CDF(ParamA, ParamB, ParamC, ParamD, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                'Case "Burr"

                Case "C2 - Cauchy"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Cauchy.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Cauchy.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C1 - Chi Squared"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.ChiSquared.PDF(ParamA, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.ChiSquared.CDF(ParamA, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C2 - Continuous Uniform"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.ContinuousUniform.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.ContinuousUniform.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C1 - Exponential"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Exponential.PDF(ParamA, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Exponential.CDF(ParamA, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C2 - Fisher-Snedecor"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.FisherSnedecor.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.FisherSnedecor.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C2 - Gamma"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Gamma.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Gamma.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C2 - Inverse Gaussian"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.InverseGaussian.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.InverseGaussian.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C2 - Log Normal"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.LogNormal.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.LogNormal.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C2 - Normal"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        'Row.Item("PDF") = MathNet.Numerics.Distributions.Normal.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        'PDF = MathNet.Numerics.Distributions.Normal.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        RandVar = Row.Item("Random_Variable")
                        PDF = MathNet.Numerics.Distributions.Normal.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        Row.Item("PDF") = PDF
                        Sum += PDF * RandVar
                        NVals += PDF
                        CdfVal = MathNet.Numerics.Distributions.Normal.CDF(ParamA, ParamB, RandVar)
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C2 - Pareto"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Pareto.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Pareto.CDF(ParamA, ParamB, Row.Item("Random_Variable"))

                    Next

                Case "C1 - Rayleigh"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Rayleigh.PDF(ParamA, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Rayleigh.CDF(ParamA, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C4 - Skewed Generalized Error"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.SkewedGeneralizedError.PDF(ParamA, ParamB, ParamC, ParamD, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.SkewedGeneralizedError.CDF(ParamA, ParamB, ParamC, ParamD, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C5 - Skewed Generalized T"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.SkewedGeneralizedT.PDF(ParamA, ParamB, ParamC, ParamD, ParamE, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.SkewedGeneralizedT.CDF(ParamA, ParamB, ParamC, ParamD, ParamE, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C3 - Student's T"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.StudentT.PDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.StudentT.CDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C3 - Triangular"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Triangular.PDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Triangular.CDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "C3 - Truncated Pareto"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.TruncatedPareto.PDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.TruncatedPareto.CDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "D1 - Bernoulli"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Bernoulli.PMF(ParamA, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Bernoulli.CDF(ParamA, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "D2 - Binomial"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Binomial.PMF(ParamA, ParamB, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Binomial.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                'Case "D1 - Categorical"


                Case "D2 - Discrete Uniform"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.DiscreteUniform.PMF(ParamA, ParamB, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.DiscreteUniform.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "D1 - Geometric"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Geometric.PMF(ParamA, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Geometric.CDF(ParamA, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "D3 - Hypergeometric"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Hypergeometric.PMF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Hypergeometric.CDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "D2 - Negative Binomial"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.NegativeBinomial.PMF(ParamA, ParamB, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.NegativeBinomial.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "D1 - Poisson"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Poisson.PMF(ParamA, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Poisson.CDF(ParamA, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case "D2 - Zipf"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Zipf.PMF(ParamA, ParamB, Row.Item("Random_Variable"))
                        CdfVal = MathNet.Numerics.Distributions.Zipf.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        Row.Item("CDF") = CdfVal
                        Row.Item("Reverse_CDF") = 1 - CdfVal
                    Next

                Case Else
                    Main.Message.AddWarning("Unknown distribution: " & DistributionName & vbCrLf)
            End Select

            DataMean = Sum / NVals
            Dim DiffSq As Double = 0
            For Each Row As DataRow In Data.Tables("DataTable").Rows
                'DiffSq += (Row.Item("Random_Variable") - DataMean) ^ 2
                'DiffSq += (Row.Item("Random_Variable") * Row.Item("PDF") - DataMean) ^ 2
                'DiffSq += ((Row.Item("Random_Variable") - DataMean) * Row.Item("PDF")) ^ 2
                DiffSq += ((Row.Item("Random_Variable") - DataMean) ^ 2) * Row.Item("PDF")
            Next

            'DataStdDev = Math.Sqrt(DiffSq / (NVals - 1)) 'Sample Standard Deviation
            DataStdDev = Math.Sqrt(DiffSq / NVals)  'Population Standard Deviation

        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try


    End Sub

    Private Sub GeneratePDF()
        'Generate the PDF data.

        If IsContinuous Then
            Data.Tables("DataTable").Columns.Add("PDF") 'Add Probability Density Function column
        Else
            Data.Tables("DataTable").Columns.Add("PMF") 'Add Probability Mass Function column
        End If

        Try
            Select Case DistributionName

                Case "C2 - Beta"
                    Dim DistVal As Double 'The calculated distribution value
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        DistVal = MathNet.Numerics.Distributions.Beta.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        If DistVal = Double.PositiveInfinity Then
                            Main.Message.Add("The Beta PDF is + infinity for a Random Variable value of " & Row.Item("Random_Variable") & " for parameters Alpha = " & ParamA & " and Beta = " & ParamB & "Random Variable" & vbCrLf)
                        ElseIf DistVal = Double.NegativeInfinity Then
                            Main.Message.Add("The Beta PDF is - infinity for a Random Variable value of " & Row.Item("Random_Variable") & " for parameters Alpha = " & ParamA & " and Beta = " & ParamB & vbCrLf)
                        ElseIf DistVal = Double.NaN Then
                            Main.Message.Add("The Beta PDF is not valid for a Random Variable value of " & Row.Item("Random_Variable") & " for parameters Alpha = " & ParamA & " and Beta = " & ParamB & vbCrLf)
                        Else
                            Row.Item("PDF") = DistVal
                        End If
                    Next

                Case "C4 - Beta Scaled"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.BetaScaled.PDF(ParamA, ParamB, ParamC, ParamD, Row.Item("Random_Variable"))
                    Next

                'Case "Burr"

                Case "C2 - Cauchy"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Cauchy.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C1 - Chi Squared"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.ChiSquared.PDF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Continuous Uniform"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.ContinuousUniform.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C1 - Exponential"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Exponential.PDF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Fisher-Snedecor"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.FisherSnedecor.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Gamma"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Gamma.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Inverse Gaussian"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.InverseGaussian.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Log Normal"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.LogNormal.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Normal"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Normal.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Pareto"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Pareto.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C1 - Rayleigh"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Rayleigh.PDF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "C4 - Skewed Generalized Error"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.SkewedGeneralizedError.PDF(ParamA, ParamB, ParamC, ParamD, Row.Item("Random_Variable"))
                    Next

                Case "C5 - Skewed Generalized T"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.SkewedGeneralizedT.PDF(ParamA, ParamB, ParamC, ParamD, ParamE, Row.Item("Random_Variable"))
                    Next

                Case "C3 - Student's T"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.StudentT.PDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                    Next

                Case "C3 - Triangular"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.Triangular.PDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                    Next

                Case "C3 - Truncated Pareto"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PDF") = MathNet.Numerics.Distributions.TruncatedPareto.PDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                    Next

                Case "D1 - Bernoulli"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Bernoulli.PMF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "D2 - Binomial"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Binomial.PMF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                'Case "D1 - Categorical"


                Case "D2 - Discrete Uniform"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.DiscreteUniform.PMF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "D1 - Geometric"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Geometric.PMF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "D3 - Hypergeometric"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Hypergeometric.PMF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                    Next

                Case "D2 - Negative Binomial"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.NegativeBinomial.PMF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "D1 - Poisson"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Poisson.PMF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "D2 - Zipf"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("PMF") = MathNet.Numerics.Distributions.Zipf.PMF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case Else
                    Main.Message.AddWarning("Unknown distribution: " & DistributionName & vbCrLf)
            End Select
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try

    End Sub

    Private Sub GenerateCDF()
        'Generate the CDF data.

        Data.Tables("DataTable").Columns.Add("CDF") 'Add the Cumulative Distribution Function column

        Try
            Select Case DistributionName

                Case "C2 - Beta"
                    Dim DistVal As Double 'The calculated distribution value
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        DistVal = MathNet.Numerics.Distributions.Beta.PDF(ParamA, ParamB, Row.Item("Random_Variable"))
                        If DistVal = Double.PositiveInfinity Then
                            Main.Message.Add("The Beta PDF is + infinity for a Random Variable value of " & Row.Item("Random_Variable") & " for parameters Alpha = " & ParamA & " and Beta = " & ParamB & "Random Variable" & vbCrLf)
                        ElseIf DistVal = Double.NegativeInfinity Then
                            Main.Message.Add("The Beta PDF is - infinity for a Random Variable value of " & Row.Item("Random_Variable") & " for parameters Alpha = " & ParamA & " and Beta = " & ParamB & vbCrLf)
                        ElseIf DistVal = Double.NaN Then
                            Main.Message.Add("The Beta PDF is not valid for a Random Variable value of " & Row.Item("Random_Variable") & " for parameters Alpha = " & ParamA & " and Beta = " & ParamB & vbCrLf)
                        Else
                            Row.Item("PDF") = DistVal
                        End If
                    Next

                Case "C4 - Beta Scaled"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.BetaScaled.CDF(ParamA, ParamB, ParamC, ParamD, Row.Item("Random_Variable"))
                    Next

                'Case "Burr"

                Case "C2 - Cauchy"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Cauchy.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C1 - Chi Squared"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.ChiSquared.CDF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Continuous Uniform"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.ContinuousUniform.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C1 - Exponential"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Exponential.CDF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Fisher-Snedecor"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.FisherSnedecor.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Gamma"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Gamma.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Inverse Gaussian"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.InverseGaussian.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Log Normal"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.LogNormal.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Normal"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Normal.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C2 - Pareto"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Pareto.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "C1 - Rayleigh"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Rayleigh.CDF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "C4 - Skewed Generalized Error"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.SkewedGeneralizedError.CDF(ParamA, ParamB, ParamC, ParamD, Row.Item("Random_Variable"))
                    Next

                Case "C5 - Skewed Generalized T"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.SkewedGeneralizedT.CDF(ParamA, ParamB, ParamC, ParamD, ParamE, Row.Item("Random_Variable"))
                    Next

                Case "C3 - Student's T"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.StudentT.CDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                    Next

                Case "C3 - Triangular"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Triangular.CDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                    Next

                Case "C3 - Truncated Pareto"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.TruncatedPareto.CDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                    Next

                Case "D1 - Bernoulli"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Bernoulli.CDF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "D2 - Binomial"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Binomial.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                'Case "D1 - Categorical"


                Case "D2 - Discrete Uniform"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.DiscreteUniform.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "D1 - Geometric"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Geometric.CDF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "D3 - Hypergeometric"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Hypergeometric.CDF(ParamA, ParamB, ParamC, Row.Item("Random_Variable"))
                    Next

                Case "D2 - Negative Binomial"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.NegativeBinomial.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case "D1 - Poisson"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Poisson.CDF(ParamA, Row.Item("Random_Variable"))
                    Next

                Case "D2 - Zipf"
                    For Each Row As DataRow In Data.Tables("DataTable").Rows
                        Row.Item("CDF") = MathNet.Numerics.Distributions.Zipf.CDF(ParamA, ParamB, Row.Item("Random_Variable"))
                    Next

                Case Else
                    Main.Message.AddWarning("Unknown distribution: " & DistributionName & vbCrLf)
            End Select

        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub



    Private Sub frmDistribChart_Move(sender As Object, e As EventArgs) Handles Me.Move
        If IndexNo = -1 Then

        Else
            Main.MonteCarlo.DataInfo(IndexNo).Top = Me.Top
            Main.MonteCarlo.DataInfo(IndexNo).Left = Me.Left
            If Main.MonteCarlo.SelVarIndex = IndexNo Then Main.ShowRVChartSettings(IndexNo)
        End If
    End Sub

    Private Sub frmDistribChart_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If IndexNo = -1 Then

        Else
            Main.MonteCarlo.DataInfo(IndexNo).Height = Me.Height
            Main.MonteCarlo.DataInfo(IndexNo).Width = Me.Width
            If Main.MonteCarlo.SelVarIndex = IndexNo Then Main.ShowRVChartSettings(IndexNo)
        End If
    End Sub

    Private Sub btnCopyChart_Click(sender As Object, e As EventArgs) Handles btnCopyChart.Click
        'Copy the chart image to the clipboard:

        'Method 1:
        'Dim ChartPic As New Bitmap(Chart1.Width, Chart1.Height)
        'Dim myGraphics As Graphics = Graphics.FromImage(ChartPic)
        'myGraphics.Clear(Color.White)
        'Chart1.Printing.PrintPaint(myGraphics, New Rectangle(Point.Empty, Chart1.Size))
        'Clipboard.SetImage(ChartPic)

        'Method 2:
        Dim myStream As New System.IO.MemoryStream()
        Chart1.SaveImage(myStream, Imaging.ImageFormat.Png)
        Dim ChartPic As New Bitmap(myStream)
        Clipboard.SetDataObject(ChartPic)

    End Sub

    Private Sub chkLegend_CheckedChanged(sender As Object, e As EventArgs) Handles chkLegend.CheckedChanged
        If chkLegend.Focused Then 'The Show Legend checkbox has been clicked.
            If chkLegend.Checked Then
                _showLegend = True
            Else
                _showLegend = False
            End If

            If IsContinuous Then
                Chart1.Series("PDF").IsVisibleInLegend = _showLegend
            Else
                Chart1.Series("PMF").IsVisibleInLegend = _showLegend
            End If
            Chart1.Series("CDF").IsVisibleInLegend = _showLegend
            Chart1.Series("Reverse_CDF").IsVisibleInLegend = _showLegend

            Main.MonteCarlo.DataInfo(IndexNo).ShowLegend = _showLegend
        End If
    End Sub

    Private Sub chkPDF_CheckedChanged(sender As Object, e As EventArgs) Handles chkPDF.CheckedChanged
        If chkPDF.Focused Then
            If IndexNo = -1 Then

            Else
                If chkPDF.Checked Then
                    Main.MonteCarlo.DataInfo(IndexNo).ShowPDF = True
                    _showPDF = True

                    Chart1.ChartAreas(0).AxisY2.Minimum = 0
                    Chart1.ChartAreas(0).AxisY2.Maximum = 1
                    Chart1.ChartAreas(0).AxisY2.MajorGrid.Enabled = False
                    Chart1.ChartAreas(0).AxisY2.Title = "Cumulative Probability"
                    Chart1.ChartAreas(0).AxisY2.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

                    Chart1.Series("CDF").YAxisType = DataVisualization.Charting.AxisType.Secondary
                    Chart1.Series("Reverse_CDF").YAxisType = DataVisualization.Charting.AxisType.Secondary

                    If IsContinuous Then
                        'Chart1.Series("PDF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "PDF")
                        If AutoYMax = True Then
                            Chart1.ChartAreas(0).AxisY.Maximum = Double.NaN
                        Else
                            Chart1.ChartAreas(0).AxisY.Maximum = YMax
                        End If

                        Chart1.ChartAreas(0).AxisY.Title = "Probability Density"
                        Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

                        Chart1.Series("PDF").Enabled = True
                    Else
                        'Chart1.Series("PMF").Points.DataBindXY(Data.Tables(0).DefaultView, "Random_Variable", Data.Tables(0).DefaultView, "PMF")
                        Chart1.ChartAreas(0).AxisY.Title = "Probability Mass"
                        Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

                        Chart1.Series("PMF").Enabled = True
                    End If
                    UpdateAnnotation()

                Else
                    Main.MonteCarlo.DataInfo(IndexNo).ShowPDF = False
                    _showPDF = False

                    Chart1.ChartAreas(0).AxisY.Minimum = 0
                    Chart1.ChartAreas(0).AxisY.Maximum = 1
                    'Chart1.ChartAreas(0).AxisY.MajorGrid.Enabled = False
                    Chart1.ChartAreas(0).AxisY.MajorGrid.Enabled = True
                    Chart1.ChartAreas(0).AxisY.Title = "Cumulative Probability"
                    Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

                    Chart1.Series("CDF").YAxisType = DataVisualization.Charting.AxisType.Primary
                    Chart1.Series("Reverse_CDF").YAxisType = DataVisualization.Charting.AxisType.Primary

                    If IsContinuous Then
                        Chart1.Series("PDF").Enabled = False
                    Else
                        Chart1.Series("PMF").Enabled = False
                    End If
                    UpdateAnnotation()

                End If
            End If
        End If
    End Sub

    Private Sub chkCDF_CheckedChanged(sender As Object, e As EventArgs) Handles chkCDF.CheckedChanged
        If chkCDF.Focused Then
            If IndexNo = -1 Then

            Else
                If chkCDF.Checked Then
                    Main.MonteCarlo.DataInfo(IndexNo).ShowCDF = True
                    _showCDF = True
                    Chart1.Series("CDF").Enabled = True
                    UpdateAnnotation()
                Else
                    Main.MonteCarlo.DataInfo(IndexNo).ShowCDF = False
                    _showCDF = False
                    Chart1.Series("CDF").Enabled = False
                    UpdateAnnotation()
                End If
            End If
        End If
    End Sub

    Private Sub chkRevCDF_CheckedChanged(sender As Object, e As EventArgs) Handles chkRevCDF.CheckedChanged
        If chkRevCDF.Focused Then
            If IndexNo = -1 Then

            Else
                If chkRevCDF.Checked Then
                    Main.MonteCarlo.DataInfo(IndexNo).ShowRevCDF = True
                    _showRevCDF = True
                    Chart1.Series("Reverse_CDF").Enabled = True
                    UpdateAnnotation()
                Else
                    Main.MonteCarlo.DataInfo(IndexNo).ShowRevCDF = False
                    _showRevCDF = False
                    Chart1.Series("Reverse_CDF").Enabled = False
                    UpdateAnnotation()
                End If
            End If
        End If
    End Sub

    Private Sub btnDeleteAnnot_Click(sender As Object, e As EventArgs) Handles btnDeleteAnnot.Click
        'Delete the selected annotation.

        Try
            If dgvAnnot.SelectedRows.Count > 0 Then
                dgvAnnot.Rows.RemoveAt(dgvAnnot.SelectedRows(0).Index)
            Else
                Main.Message.AddWarning("Select an annotation entry to delete." & vbCrLf)
            End If
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try


    End Sub

    Private Sub btnUpdateAnnotation_Click(sender As Object, e As EventArgs) Handles btnUpdateAnnotation.Click
        'Update the Annotations
        'Chart1.Annotations.Clear()
        'Chart1.Series("CdfVertBar").Points.Clear()
        ''Chart1.Series("HistPoints").Points.Clear()

        'UpdateAnnotationValues()

        'dgvAnnot.AllowUserToAddRows = False
        'Dim I As Integer
        'For I = 0 To dgvAnnot.RowCount - 1
        '    If dgvAnnot.Rows(I).Cells(0).Value = True Then 'Display annotation on the CDF chart
        '        'Add the vertical bar:
        '        Dim CdfPoint As New DataVisualization.Charting.DataPoint
        '        CdfPoint.XValue = dgvAnnot.Rows(I).Cells(6).Value
        '        CdfPoint.SetValueY(dgvAnnot.Rows(I).Cells(5).Value)
        '        Chart1.Series("CdfVertBar").Points.Add(CdfPoint)
        '        'Add the label:
        '        Dim CdfAnnot As New DataVisualization.Charting.TextAnnotation
        '        CdfAnnot.AxisX = Chart1.ChartAreas(0).AxisX
        '        'CdfAnnot.AxisY = Chart1.ChartAreas(0).AxisY
        '        CdfAnnot.AxisY = Chart1.ChartAreas(0).AxisY2
        '        CdfAnnot.AnchorX = dgvAnnot.Rows(I).Cells(6).Value
        '        CdfAnnot.AnchorY = dgvAnnot.Rows(I).Cells(5).Value
        '        CdfAnnot.AnchorAlignment = ContentAlignment.MiddleRight
        '        CdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(6).Value, dgvAnnot.Rows(I).Cells(7).Value) & ")"
        '        CdfAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
        '        Chart1.Annotations.Add(CdfAnnot)
        '    End If
        '    If dgvAnnot.Rows(I).Cells(1).Value = True Then 'Display annotation on the PDF
        '        ''Add the circle symbol to the histogram
        '        'Dim HistPoint As New DataVisualization.Charting.DataPoint
        '        'HistPoint.XValue = dgvAnnot.Rows(I).Cells(6).Value
        '        'Dim YValue As Double = GetHistProb(dgvAnnot.Rows(I).Cells(6).Value) / HistIntervalWidth 'This is the Probability Density
        '        'HistPoint.SetValueY(YValue)
        '        'Chart1.Series("HistPoints").Points.Add(HistPoint)

        '        'Add the vertical bar:
        '        Dim PdfPoint As New DataVisualization.Charting.DataPoint
        '        PdfPoint.XValue = dgvAnnot.Rows(I).Cells(6).Value
        '        PdfPoint.SetValueY(dgvAnnot.Rows(I).Cells(5).Value)
        '        Chart1.Series("PdfVertBar").Points.Add(PdfPoint)
        '        'Add the label:
        '        Dim PdfAnnot As New DataVisualization.Charting.TextAnnotation
        '        PdfAnnot.AxisX = Chart1.ChartAreas(0).AxisX
        '        PdfAnnot.AxisY = Chart1.ChartAreas(0).AxisY
        '        PdfAnnot.AnchorX = dgvAnnot.Rows(I).Cells(6).Value
        '        PdfAnnot.AnchorY = dgvAnnot.Rows(I).Cells(5).Value
        '        PdfAnnot.AnchorAlignment = ContentAlignment.MiddleRight
        '        PdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(6).Value, dgvAnnot.Rows(I).Cells(7).Value) & ")"
        '        PdfAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
        '        Chart1.Annotations.Add(PdfAnnot)

        '    End If
        'Next

        'dgvAnnot.AllowUserToAddRows = True

        UpdateAnnotation()
    End Sub

    Private Sub UpdateAnnotation()
        Chart1.Annotations.Clear()
        Chart1.Series("CdfVertBar").Points.Clear()
        Chart1.Series("RevCdfVertBar").Points.Clear()
        Chart1.Series("PdfVertBar").Points.Clear()
        'Chart1.Series("HistPoints").Points.Clear()

        UpdateAnnotationValues()

        dgvAnnot.AllowUserToAddRows = False
        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            If dgvAnnot.Rows(I).Cells(0).Value = True Then 'Display annotation on the CDF chart
                If ShowCDF Then
                    'Add the vertical bar:
                    Dim CdfPoint As New DataVisualization.Charting.DataPoint
                    'CdfPoint.XValue = dgvAnnot.Rows(I).Cells(6).Value
                    CdfPoint.XValue = dgvAnnot.Rows(I).Cells(8).Value
                    'CdfPoint.SetValueY(dgvAnnot.Rows(I).Cells(5).Value)
                    CdfPoint.SetValueY(dgvAnnot.Rows(I).Cells(6).Value)
                    Chart1.Series("CdfVertBar").Points.Add(CdfPoint)
                    'Add the label:
                    Dim CdfAnnot As New DataVisualization.Charting.TextAnnotation
                    CdfAnnot.AxisX = Chart1.ChartAreas(0).AxisX
                    'CdfAnnot.AxisY = Chart1.ChartAreas(0).AxisY
                    CdfAnnot.AxisY = Chart1.ChartAreas(0).AxisY2
                    'CdfAnnot.AnchorX = dgvAnnot.Rows(I).Cells(6).Value
                    CdfAnnot.AnchorX = dgvAnnot.Rows(I).Cells(8).Value
                    'CdfAnnot.AnchorY = dgvAnnot.Rows(I).Cells(5).Value
                    CdfAnnot.AnchorY = dgvAnnot.Rows(I).Cells(6).Value
                    CdfAnnot.AnchorAlignment = ContentAlignment.MiddleRight
                    'CdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(6).Value, dgvAnnot.Rows(I).Cells(7).Value) & ")"
                    'CdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(6).Value, dgvAnnot.Rows(I).Cells(9).Value) & ")"
                    CdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(8).Value, dgvAnnot.Rows(I).Cells(9).Value) & ")"
                    CdfAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
                    Chart1.Annotations.Add(CdfAnnot)
                End If

                If ShowRevCDF Then
                    'Add the vertical bar:
                    Dim RevCdfPoint As New DataVisualization.Charting.DataPoint
                    RevCdfPoint.XValue = dgvAnnot.Rows(I).Cells(8).Value
                    'RevCdfPoint.SetValueY(dgvAnnot.Rows(I).Cells(6).Value)
                    RevCdfPoint.SetValueY(dgvAnnot.Rows(I).Cells(7).Value)
                    Chart1.Series("RevCdfVertBar").Points.Add(RevCdfPoint)
                    'Add the label:
                    Dim RevCdfAnnot As New DataVisualization.Charting.TextAnnotation
                    RevCdfAnnot.AxisX = Chart1.ChartAreas(0).AxisX
                    RevCdfAnnot.AxisY = Chart1.ChartAreas(0).AxisY2
                    RevCdfAnnot.AnchorX = dgvAnnot.Rows(I).Cells(8).Value
                    'RevCdfAnnot.AnchorY = dgvAnnot.Rows(I).Cells(6).Value
                    RevCdfAnnot.AnchorY = dgvAnnot.Rows(I).Cells(7).Value
                    RevCdfAnnot.AnchorAlignment = ContentAlignment.MiddleRight
                    RevCdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(8).Value, dgvAnnot.Rows(I).Cells(9).Value) & ")"
                    RevCdfAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
                    Chart1.Annotations.Add(RevCdfAnnot)
                End If
            End If
            If dgvAnnot.Rows(I).Cells(1).Value = True Then 'Display annotation on the PDF
                ''Add the circle symbol to the histogram
                'Dim HistPoint As New DataVisualization.Charting.DataPoint
                'HistPoint.XValue = dgvAnnot.Rows(I).Cells(6).Value
                'Dim YValue As Double = GetHistProb(dgvAnnot.Rows(I).Cells(6).Value) / HistIntervalWidth 'This is the Probability Density
                'HistPoint.SetValueY(YValue)
                'Chart1.Series("HistPoints").Points.Add(HistPoint)
                If ShowPDF Then
                    'Add the vertical bar:
                    Dim PdfPoint As New DataVisualization.Charting.DataPoint
                    'PdfPoint.XValue = dgvAnnot.Rows(I).Cells(6).Value
                    PdfPoint.XValue = dgvAnnot.Rows(I).Cells(8).Value
                    PdfPoint.SetValueY(dgvAnnot.Rows(I).Cells(5).Value)
                    Chart1.Series("PdfVertBar").Points.Add(PdfPoint)
                    'Add the label:
                    Dim PdfAnnot As New DataVisualization.Charting.TextAnnotation
                    PdfAnnot.AxisX = Chart1.ChartAreas(0).AxisX
                    PdfAnnot.AxisY = Chart1.ChartAreas(0).AxisY
                    'PdfAnnot.AnchorX = dgvAnnot.Rows(I).Cells(6).Value
                    PdfAnnot.AnchorX = dgvAnnot.Rows(I).Cells(8).Value
                    PdfAnnot.AnchorY = dgvAnnot.Rows(I).Cells(5).Value
                    PdfAnnot.AnchorAlignment = ContentAlignment.MiddleRight
                    'PdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(6).Value, dgvAnnot.Rows(I).Cells(7).Value) & ")"
                    'PdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(6).Value, dgvAnnot.Rows(I).Cells(9).Value) & ")"
                    PdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(8).Value, dgvAnnot.Rows(I).Cells(9).Value) & ")"
                    PdfAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
                    Chart1.Annotations.Add(PdfAnnot)
                End If
            End If
        Next

        If ShowPDFIntervalProb Then
            If ShowPDF Then
                UpdateIntervalProb()
                'Add probability annotation:
                Dim ProbAnnot As New DataVisualization.Charting.TextAnnotation
                ProbAnnot.AxisX = Chart1.ChartAreas(0).AxisX
                ProbAnnot.AxisY = Chart1.ChartAreas(0).AxisY
                ProbAnnot.AnchorX = (RVLowerVal + RVUpperVal) / 2
                'ProbAnnot.AnchorY = Chart1.ChartAreas(0).AxisY.Minimum + (Chart1.ChartAreas(0).AxisY.Maximum - Chart1.ChartAreas(0).AxisY.Minimum) / 16
                'ProbAnnot.AnchorY = Chart1.ChartAreas(0).AxisY.Minimum 'NaN !!!
                ProbAnnot.AnchorY = 0
                ProbAnnot.AnchorAlignment = ContentAlignment.BottomCenter
                ProbAnnot.Text = "Probability: " & txtRVIntervalProb.Text
                ProbAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
                Chart1.Annotations.Add(ProbAnnot)
            End If
        End If
        'ShowPdfInterval() 'ERROR

        dgvAnnot.AllowUserToAddRows = True
    End Sub

    Private Sub UpdateAnnotationValues()
        'Update the annotation values using the current CDF and Histogram data.

        dgvAnnot.AllowUserToAddRows = False

        'Dim Average As Double = Data.Tables("DataTable").Compute("Avg(Random_Variable)", "")
        'Dim StdDev As Double = Math.Sqrt(Variance(True))
        Dim CDFVal As Double

        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            Select Case dgvAnnot.Rows(I).Cells(2).Value
                Case "Probability"
                    'dgvAnnot.Rows(I).Cells(5).Value = dgvAnnot.Rows(I).Cells(3).Value
                    dgvAnnot.Rows(I).Cells(6).Value = dgvAnnot.Rows(I).Cells(3).Value 'CDF = Prob Value
                    dgvAnnot.Rows(I).Cells(7).Value = 1 - dgvAnnot.Rows(I).Cells(3).Value 'Reverse CDF = 1 - Prob Value

                    'dgvAnnot.Rows(I).Cells(6).Value = GetValue(dgvAnnot.Rows(I).Cells(5).Value)
                    'dgvAnnot.Rows(I).Cells(8).Value = GetValue(dgvAnnot.Rows(I).Cells(5).Value)
                    dgvAnnot.Rows(I).Cells(8).Value = GetValue(dgvAnnot.Rows(I).Cells(3).Value) 'Value = InvCDF(Prob)
                    dgvAnnot.Rows(I).Cells(5).Value = GetProbDens(dgvAnnot.Rows(I).Cells(8).Value)

                Case "Value"
                    'dgvAnnot.Rows(I).Cells(6).Value = dgvAnnot.Rows(I).Cells(3).Value
                    dgvAnnot.Rows(I).Cells(8).Value = dgvAnnot.Rows(I).Cells(3).Value
                    'dgvAnnot.Rows(I).Cells(5).Value = GetProb(dgvAnnot.Rows(I).Cells(6).Value)
                    'dgvAnnot.Rows(I).Cells(5).Value = GetProbDens(dgvAnnot.Rows(I).Cells(6).Value)
                    dgvAnnot.Rows(I).Cells(5).Value = GetProbDens(dgvAnnot.Rows(I).Cells(8).Value)
                Case "Mean"
                    dgvAnnot.Rows(I).Cells(3).Value = ""
                    'dgvAnnot.Rows(I).Cells(6).Value = Val(txtAverage.Text)
                    'dgvAnnot.Rows(I).Cells(6).Value = Average
                    'dgvAnnot.Rows(I).Cells(6).Value = DataMean
                    dgvAnnot.Rows(I).Cells(8).Value = DataMean 'Value
                    'dgvAnnot.Rows(I).Cells(5).Value = GetProb(txtAverage.Text)
                    'dgvAnnot.Rows(I).Cells(5).Value = GetProb(Average)
                    'dgvAnnot.Rows(I).Cells(5).Value = GetProb(DataMean)
                    dgvAnnot.Rows(I).Cells(5).Value = GetProbDens(DataMean) 'Probability Density
                    CDFVal = GetCDFValue(DataMean)
                    dgvAnnot.Rows(I).Cells(6).Value = CDFVal 'Cumulative Distribution Function
                    dgvAnnot.Rows(I).Cells(7).Value = 1 - CDFVal 'Reverse Cumulative Distribution Function
                Case "Standard Deviation"
                    'dgvAnnot.Rows(I).Cells(6).Value = txtAverage.Text + dgvAnnot.Rows(I).Cells(3).Value * txtStdDev.Text
                    'dgvAnnot.Rows(I).Cells(6).Value = Average + dgvAnnot.Rows(I).Cells(3).Value * StdDev
                    'dgvAnnot.Rows(I).Cells(6).Value = DataMean + dgvAnnot.Rows(I).Cells(3).Value * DataStdDev
                    dgvAnnot.Rows(I).Cells(8).Value = DataMean + dgvAnnot.Rows(I).Cells(3).Value * DataStdDev
                    'dgvAnnot.Rows(I).Cells(5).Value = GetProb(dgvAnnot.Rows(I).Cells(6).Value)
                    'dgvAnnot.Rows(I).Cells(5).Value = GetProbDens(dgvAnnot.Rows(I).Cells(6).Value)
                    dgvAnnot.Rows(I).Cells(5).Value = GetProbDens(dgvAnnot.Rows(I).Cells(8).Value)
                    CDFVal = GetCDFValue(dgvAnnot.Rows(I).Cells(8).Value)
                    dgvAnnot.Rows(I).Cells(6).Value = CDFVal 'Cumulative Distribution Function
                    dgvAnnot.Rows(I).Cells(7).Value = 1 - CDFVal 'Reverse Cumulative Distribution Function
                Case Else
                    Main.Message.AddWarning("Unknown annotation type: " & dgvAnnot.Rows(I).Cells(2).Value & vbCrLf)
            End Select
        Next
        dgvAnnot.AllowUserToAddRows = True
    End Sub

    'Private Function Variance(ByVal IsSample As Boolean) As Double
    '    'Calculates the variance of the data in column Random_Variable.
    '    'If IsSample is True, the Sample variance is calculated, else the Population variance is calculated.

    '    'Dim Mean As Double = DataSource.Data.Tables(SourceTableName).Compute("Avg(" & SourceColumnName & ")", "")
    '    Dim Mean As Double = Data.Tables("DataTable").Compute("Avg(Random_Variable)", "")
    '    Dim DiffSq As Double = 0
    '    'For Each Row As DataRow In DataSource.Data.Tables(SourceTableName).Rows
    '    For Each Row As DataRow In Data.Tables("DataTable").Rows
    '        'DiffSq += (Row.Item(SourceColumnName) - Mean) ^ 2
    '        DiffSq += (Row.Item("Random_Variable") - Mean) ^ 2
    '    Next
    '    If IsSample Then
    '        'Return DiffSq / (NRows - 1)
    '        Return DiffSq / (Data.Tables("DataTable").Rows.Count - 1)
    '    Else
    '        'Return DiffSq / NRows
    '        Return DiffSq / (Data.Tables("DataTable").Rows.Count)
    '    End If
    'End Function

    'Private Function GetProb(ByVal RVValue As Double) As Double
    Private Function GetProbDens(ByVal RVValue As Double) As Double
        'Returns the Probability Density value for the specified Random Variable value
        'OLD: Returns the CDF value for the specified Random Variable value

        Try
            Select Case DistributionName

                Case "C2 - Beta"
                    Return MathNet.Numerics.Distributions.Beta.PDF(ParamA, ParamB, RVValue)

                Case "C4 - Beta Scaled"
                    Return MathNet.Numerics.Distributions.BetaScaled.PDF(ParamA, ParamB, ParamC, ParamD, RVValue)

                'Case "Burr"

                Case "C2 - Cauchy"
                    Return MathNet.Numerics.Distributions.Cauchy.PDF(ParamA, ParamB, RVValue)

                Case "C1 - Chi Squared"
                    Return MathNet.Numerics.Distributions.ChiSquared.PDF(ParamA, RVValue)

                Case "C2 - Continuous Uniform"
                    Return MathNet.Numerics.Distributions.ContinuousUniform.PDF(ParamA, ParamB, RVValue)

                Case "C1 - Exponential"
                    Return MathNet.Numerics.Distributions.Exponential.PDF(ParamA, RVValue)

                Case "C2 - Fisher-Snedecor"
                    Return MathNet.Numerics.Distributions.FisherSnedecor.PDF(ParamA, ParamB, RVValue)

                Case "C2 - Gamma"
                    Return MathNet.Numerics.Distributions.Gamma.PDF(ParamA, ParamB, RVValue)

                Case "C2 - Inverse Gaussian"
                    Return MathNet.Numerics.Distributions.InverseGaussian.PDF(ParamA, ParamB, RVValue)

                Case "C2 - Log Normal"
                    Return MathNet.Numerics.Distributions.LogNormal.PDF(ParamA, ParamB, RVValue)

                Case "C2 - Normal"
                    Return MathNet.Numerics.Distributions.Normal.PDF(ParamA, ParamB, RVValue)

                Case "C2 - Pareto"
                    Return MathNet.Numerics.Distributions.Pareto.PDF(ParamA, ParamB, RVValue)

                Case "C1 - Rayleigh"
                    Return MathNet.Numerics.Distributions.Rayleigh.PDF(ParamA, RVValue)

                Case "C4 - Skewed Generalized Error"
                    Return MathNet.Numerics.Distributions.SkewedGeneralizedError.PDF(ParamA, ParamB, ParamC, ParamD, RVValue)

                Case "C5 - Skewed Generalized T"
                    Return MathNet.Numerics.Distributions.SkewedGeneralizedT.PDF(ParamA, ParamB, ParamC, ParamD, ParamE, RVValue)

                Case "C3 - Student's T"
                    Return MathNet.Numerics.Distributions.StudentT.PDF(ParamA, ParamB, ParamC, RVValue)

                Case "C3 - Triangular"
                    Return MathNet.Numerics.Distributions.Triangular.PDF(ParamA, ParamB, ParamC, RVValue)

                Case "C3 - Truncated Pareto"
                    Return MathNet.Numerics.Distributions.TruncatedPareto.PDF(ParamA, ParamB, ParamC, RVValue)

                Case "D1 - Bernoulli"
                    Return MathNet.Numerics.Distributions.Bernoulli.PMF(ParamA, RVValue)

                Case "D2 - Binomial"
                    Return MathNet.Numerics.Distributions.Binomial.PMF(ParamA, ParamB, RVValue)

                'Case "D1 - Categorical"

                Case "D2 - Discrete Uniform"
                    Return MathNet.Numerics.Distributions.DiscreteUniform.PMF(ParamA, ParamB, RVValue)

                Case "D1 - Geometric"
                    Return MathNet.Numerics.Distributions.Geometric.PMF(ParamA, RVValue)

                    Return MathNet.Numerics.Distributions.Hypergeometric.PMF(ParamA, ParamB, ParamC, RVValue)

                Case "D2 - Negative Binomial"
                    Return MathNet.Numerics.Distributions.NegativeBinomial.PMF(ParamA, ParamB, RVValue)

                Case "D1 - Poisson"
                    Return MathNet.Numerics.Distributions.Poisson.PMF(ParamA, RVValue)

                Case "D2 - Zipf"
                    Return MathNet.Numerics.Distributions.Zipf.PMF(ParamA, ParamB, RVValue)

                Case Else
                    Main.Message.AddWarning("Unknown distribution: " & DistributionName & vbCrLf)
            End Select
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Function

    'Private Function GetValue(ByVal ProbValue As Double) As Double
    Private Function GetValue(ByVal ProbValue As Double) As Double
        'Returns the value corrensponding the specified Probability value

        Try
            Select Case DistributionName

                Case "C2 - Beta"
                    'Return MathNet.Numerics.Distributions.Beta.CDF(ParamA, ParamB, ProbValue)
                    Return MathNet.Numerics.Distributions.Beta.InvCDF(ParamA, ParamB, ProbValue)

                Case "C4 - Beta Scaled"
                    Return MathNet.Numerics.Distributions.BetaScaled.InvCDF(ParamA, ParamB, ParamC, ParamD, ProbValue)

                'Case "Burr"

                Case "C2 - Cauchy"
                    Return MathNet.Numerics.Distributions.Cauchy.InvCDF(ParamA, ParamB, ProbValue)

                Case "C1 - Chi Squared"
                    Return MathNet.Numerics.Distributions.ChiSquared.InvCDF(ParamA, ProbValue)

                Case "C2 - Continuous Uniform"
                    Return MathNet.Numerics.Distributions.ContinuousUniform.InvCDF(ParamA, ParamB, ProbValue)

                Case "C1 - Exponential"
                    Return MathNet.Numerics.Distributions.Exponential.InvCDF(ParamA, ProbValue)

                Case "C2 - Fisher-Snedecor"
                    Return MathNet.Numerics.Distributions.FisherSnedecor.InvCDF(ParamA, ParamB, ProbValue)

                Case "C2 - Gamma"
                    Return MathNet.Numerics.Distributions.Gamma.InvCDF(ParamA, ParamB, ProbValue)

                'Case "C2 - Inverse Gaussian"
                '    Return MathNet.Numerics.Distributions.InverseGaussian.InvCDF(ParamA, ParamB, ProbValue) 'ERROR

                Case "C2 - Log Normal"
                    Return MathNet.Numerics.Distributions.LogNormal.InvCDF(ParamA, ParamB, ProbValue)

                Case "C2 - Normal"
                    'Return MathNet.Numerics.Distributions.Normal.CDF(ParamA, ParamB, ProbValue)
                    Return MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, ProbValue)

                Case "C2 - Pareto"
                    Return MathNet.Numerics.Distributions.Pareto.InvCDF(ParamA, ParamB, ProbValue)

                Case "C1 - Rayleigh"
                    Return MathNet.Numerics.Distributions.Rayleigh.InvCDF(ParamA, ProbValue)

                Case "C4 - Skewed Generalized Error"
                    Return MathNet.Numerics.Distributions.SkewedGeneralizedError.InvCDF(ParamA, ParamB, ParamC, ParamD, ProbValue)

                Case "C5 - Skewed Generalized T"
                    Return MathNet.Numerics.Distributions.SkewedGeneralizedT.InvCDF(ParamA, ParamB, ParamC, ParamD, ParamE, ProbValue)

                Case "C3 - Student's T"
                    Return MathNet.Numerics.Distributions.StudentT.InvCDF(ParamA, ParamB, ParamC, ProbValue)

                Case "C3 - Triangular"
                    Return MathNet.Numerics.Distributions.Triangular.InvCDF(ParamA, ParamB, ParamC, ProbValue)

                    'Case "C3 - Truncated Pareto"
                    '    Return MathNet.Numerics.Distributions.TruncatedPareto.InvCDF(ParamA, ParamB, ParamC, ProbValue) 'ERROR

                    'TO DO: Get InvCDF:
                    'Case "D1 - Bernoulli"
                    '    Return MathNet.Numerics.Distributions.Bernoulli.InvCDF(ParamA, ProbValue)

                    'Case "D2 - Binomial"
                    '    Return MathNet.Numerics.Distributions.Binomial.InvCDF(ParamA, ParamB, ProbValue)

                    ''Case "D1 - Categorical"

                    'Case "D2 - Discrete Uniform"
                    '    Return MathNet.Numerics.Distributions.DiscreteUniform.InvCDF(ParamA, ParamB, ProbValue)

                    'Case "D1 - Geometric"
                    '    Return MathNet.Numerics.Distributions.Geometric.InvCDF(ParamA, ProbValue)

                    '    Return MathNet.Numerics.Distributions.Hypergeometric.InvCDF(ParamA, ParamB, ParamC, ProbValue)

                    'Case "D2 - Negative Binomial"
                    '    Return MathNet.Numerics.Distributions.NegativeBinomial.InvCDF(ParamA, ParamB, ProbValue)

                    'Case "D1 - Poisson"
                    '    Return MathNet.Numerics.Distributions.Poisson.InvCDF(ParamA, ProbValue)

                    'Case "D2 - Zipf"
                    '    Return MathNet.Numerics.Distributions.Zipf.InvCDF(ParamA, ParamB, ProbValue)

                Case Else
                    Main.Message.AddWarning("Unknown distribution: " & DistributionName & vbCrLf)
            End Select
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try

    End Function


    'Private Function GetCDFValue(ByVal ProbValue As Double) As Double
    Private Function GetCDFValue(ByVal RVValue As Double) As Double
        'Returns the probability that a value sampled from the distribution is less than or equal to the specified Random Variable value

        Try
            Select Case DistributionName

                Case "C2 - Beta"
                    'Return MathNet.Numerics.Distributions.Beta.CDF(ParamA, ParamB, ProbValue)
                    Return MathNet.Numerics.Distributions.Beta.CDF(ParamA, ParamB, RVValue)

                Case "C4 - Beta Scaled"
                    Return MathNet.Numerics.Distributions.BetaScaled.CDF(ParamA, ParamB, ParamC, ParamD, RVValue)

                'Case "Burr"

                Case "C2 - Cauchy"
                    Return MathNet.Numerics.Distributions.Cauchy.CDF(ParamA, ParamB, RVValue)

                Case "C1 - Chi Squared"
                    Return MathNet.Numerics.Distributions.ChiSquared.CDF(ParamA, RVValue)

                Case "C2 - Continuous Uniform"
                    Return MathNet.Numerics.Distributions.ContinuousUniform.CDF(ParamA, ParamB, RVValue)

                Case "C1 - Exponential"
                    Return MathNet.Numerics.Distributions.Exponential.CDF(ParamA, RVValue)

                Case "C2 - Fisher-Snedecor"
                    Return MathNet.Numerics.Distributions.FisherSnedecor.CDF(ParamA, ParamB, RVValue)

                Case "C2 - Gamma"
                    Return MathNet.Numerics.Distributions.Gamma.CDF(ParamA, ParamB, RVValue)

                Case "C2 - Inverse Gaussian"
                    Return MathNet.Numerics.Distributions.InverseGaussian.CDF(ParamA, ParamB, RVValue)

                Case "C2 - Log Normal"
                    Return MathNet.Numerics.Distributions.LogNormal.CDF(ParamA, ParamB, RVValue)

                Case "C2 - Normal"
                    Return MathNet.Numerics.Distributions.Normal.CDF(ParamA, ParamB, RVValue)

                Case "C2 - Pareto"
                    Return MathNet.Numerics.Distributions.Pareto.CDF(ParamA, ParamB, RVValue)

                Case "C1 - Rayleigh"
                    Return MathNet.Numerics.Distributions.Rayleigh.CDF(ParamA, RVValue)

                Case "C4 - Skewed Generalized Error"
                    Return MathNet.Numerics.Distributions.SkewedGeneralizedError.CDF(ParamA, ParamB, ParamC, ParamD, RVValue)

                Case "C5 - Skewed Generalized T"
                    Return MathNet.Numerics.Distributions.SkewedGeneralizedT.CDF(ParamA, ParamB, ParamC, ParamD, ParamE, RVValue)

                Case "C3 - Student's T"
                    Return MathNet.Numerics.Distributions.StudentT.CDF(ParamA, ParamB, ParamC, RVValue)

                Case "C3 - Triangular"
                    Return MathNet.Numerics.Distributions.Triangular.CDF(ParamA, ParamB, ParamC, RVValue)

                Case "C3 - Truncated Pareto"
                    Return MathNet.Numerics.Distributions.TruncatedPareto.CDF(ParamA, ParamB, ParamC, RVValue)

                Case "D1 - Bernoulli"
                    Return MathNet.Numerics.Distributions.Bernoulli.CDF(ParamA, RVValue)

                Case "D2 - Binomial"
                    Return MathNet.Numerics.Distributions.Binomial.CDF(ParamA, ParamB, RVValue)

                'Case "D1 - Categorical"

                Case "D2 - Discrete Uniform"
                    Return MathNet.Numerics.Distributions.DiscreteUniform.CDF(ParamA, ParamB, RVValue)

                Case "D1 - Geometric"
                    Return MathNet.Numerics.Distributions.Geometric.CDF(ParamA, RVValue)

                    Return MathNet.Numerics.Distributions.Hypergeometric.CDF(ParamA, ParamB, ParamC, RVValue)

                Case "D2 - Negative Binomial"
                    Return MathNet.Numerics.Distributions.NegativeBinomial.CDF(ParamA, ParamB, RVValue)

                Case "D1 - Poisson"
                    Return MathNet.Numerics.Distributions.Poisson.CDF(ParamA, RVValue)

                Case "D2 - Zipf"
                    Return MathNet.Numerics.Distributions.Zipf.CDF(ParamA, ParamB, RVValue)

                Case Else
                    Main.Message.AddWarning("Unknown distribution: " & DistributionName & vbCrLf)
            End Select
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try

    End Function

    Private Sub btnFormatHelp2_Click(sender As Object, e As EventArgs) Handles btnFormatHelp2.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub dgvAnnot_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvAnnot.EditingControlShowing

        'If dgvAnnot.CurrentCell.ColumnIndex = 0 Then 'Annotation Type selected
        If dgvAnnot.CurrentCell.ColumnIndex = 2 Then 'Annotation Type selected
            Dim combo As ComboBox = CType(e.Control, ComboBox)
            If (combo IsNot Nothing) Then
                combo.Name = "cboAnnotType"
                'Remove current handler:
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
                'Add the event handler:
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
            End If
        End If
    End Sub

    Private Sub ComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim combo As ComboBox = CType(sender, ComboBox)

        If combo.Name = "cboAnnotType" Then
            Main.Message.Add("Selected annotation type: " & combo.SelectedItem.ToString & vbCrLf)

            Dim RowNo As Integer = dgvAnnot.SelectedCells(0).RowIndex
            Main.Message.Add("Selected row: " & RowNo & vbCrLf)

            Select Case combo.SelectedItem.ToString
                Case "Probability"
                    AddProbAnnot(RowNo)
                Case "Value"
                    AddValueAnnot(RowNo)
                Case "Mean"
                    AddMeanAnnot(RowNo)
                Case "Standard Deviation"
                    AddStdDevAnnot(RowNo)
                Case Else

            End Select
        Else
            Main.Message.AddWarning("Unknown combo box: " & combo.Name & vbCrLf)
        End If
    End Sub


    Private Sub AddProbAnnot(ByVal RowNo As Integer)
        'Add a probability annotation entry at the specified row number.

        Dim P1 As Boolean = False 'If True then the P1 annotation is in the list
        Dim P10 As Boolean = False 'If True then the P10 annotation is in the list
        Dim P50 As Boolean = False 'If True then the P50 annotation is in the list
        Dim P90 As Boolean = False 'If True then the P90 annotation is in the list
        Dim P99 As Boolean = False 'If True then the P99 annotation is in the list

        'Dim ProbVal As Double 'The selected probability value

        dgvAnnot.AllowUserToAddRows = False

        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            If dgvAnnot.Rows(I).Cells(2).Value = "Probability" Then
                'If dgvAnnot.Rows(I).Cells(5).Value = 0.01 Then P1 = True
                'If dgvAnnot.Rows(I).Cells(5).Value = 0.1 Then P10 = True
                'If dgvAnnot.Rows(I).Cells(5).Value = 0.5 Then P50 = True
                'If dgvAnnot.Rows(I).Cells(5).Value = 0.9 Then P90 = True
                'If dgvAnnot.Rows(I).Cells(5).Value = 0.99 Then P99 = True
                If dgvAnnot.Rows(I).Cells(3).Value = 0.01 Then P1 = True
                If dgvAnnot.Rows(I).Cells(3).Value = 0.1 Then P10 = True
                If dgvAnnot.Rows(I).Cells(3).Value = 0.5 Then P50 = True
                If dgvAnnot.Rows(I).Cells(3).Value = 0.9 Then P90 = True
                If dgvAnnot.Rows(I).Cells(3).Value = 0.99 Then P99 = True
            End If
        Next

        If P10 = False Then 'Enter P10 probability annotation settings:
            Dim P10Value As Double = GetValue(0.1)
            If RowNo > dgvAnnot.RowCount - 1 Then
                'dgvAnnot.Rows.Add({True, True, "Probability", 0.1, "P10", 0.1, P10Value})
                'dgvAnnot.Rows.Add({True, True, "Probability", 0.1, "P10", GetProbDens(0.1), 0.1, 0.9, P10Value})
                dgvAnnot.Rows.Add({True, False, "Probability", 0.1, "P10", GetProbDens(0.1), 0.1, 0.9, P10Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.1
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P10"
                dgvAnnot.Rows(RowNo).Cells(5).Value = GetProbDens(0.1) 'Probability Density
                'dgvAnnot.Rows(RowNo).Cells(5).Value = 0.1
                dgvAnnot.Rows(RowNo).Cells(6).Value = 0.1 'CDF
                dgvAnnot.Rows(RowNo).Cells(7).Value = 0.9 'Reverse CDF
                'dgvAnnot.Rows(RowNo).Cells(6).Value = P10Value
                dgvAnnot.Rows(RowNo).Cells(8).Value = P10Value
            End If
            DisplayCDFAnnot(0.1, P10Value, "P10")
        ElseIf P50 = False Then 'Enter P50 probability annotation settings:
            Dim P50Value As Double = GetValue(0.5)
            If RowNo > dgvAnnot.RowCount - 1 Then
                'dgvAnnot.Rows.Add({True, True, "Probability", 0.5, "P50", GetProbDens(0.5), 0.5, 0.5, P50Value})
                dgvAnnot.Rows.Add({True, False, "Probability", 0.5, "P50", GetProbDens(0.5), 0.5, 0.5, P50Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.5
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P50"
                dgvAnnot.Rows(RowNo).Cells(5).Value = GetProbDens(0.5) 'Probability Density
                'dgvAnnot.Rows(RowNo).Cells(5).Value = 0.5
                dgvAnnot.Rows(RowNo).Cells(6).Value = 0.5 'CDF
                dgvAnnot.Rows(RowNo).Cells(7).Value = 0.5 'Reverse CDF
                dgvAnnot.Rows(RowNo).Cells(8).Value = P50Value
            End If
            DisplayCDFAnnot(0.5, P50Value, "P50")
        ElseIf P90 = False Then 'Enter P90 probability annotation settings:
            Dim P90Value As Double = GetValue(0.9)
            If RowNo > dgvAnnot.RowCount - 1 Then
                'dgvAnnot.Rows.Add({True, True, "Probability", 0.9, "P90", GetProbDens(0.9), 0.9, 0.1, P90Value})
                dgvAnnot.Rows.Add({True, False, "Probability", 0.9, "P90", GetProbDens(0.9), 0.9, 0.1, P90Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.9
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P90"
                dgvAnnot.Rows(RowNo).Cells(5).Value = GetProbDens(0.9) 'Probability Density
                'dgvAnnot.Rows(RowNo).Cells(5).Value = 0.9
                dgvAnnot.Rows(RowNo).Cells(6).Value = 0.9 'CDF
                dgvAnnot.Rows(RowNo).Cells(7).Value = 0.01 'Reverse CDF
                dgvAnnot.Rows(RowNo).Cells(8).Value = P90Value
            End If
            DisplayCDFAnnot(0.9, P90Value, "P90")
        ElseIf P1 = False Then 'Enter P1 probability annotation settings:
            Dim P1Value As Double = GetValue(0.01)
            If RowNo > dgvAnnot.RowCount - 1 Then
                'dgvAnnot.Rows.Add({True, True, "Probability", 0.01, "P1", GetProbDens(0.01), 0.01, 0.99, P1Value})
                dgvAnnot.Rows.Add({True, False, "Probability", 0.01, "P1", GetProbDens(0.01), 0.01, 0.99, P1Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.01
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P1"
                dgvAnnot.Rows(RowNo).Cells(5).Value = GetProbDens(0.01) 'Probability Density
                'dgvAnnot.Rows(RowNo).Cells(5).Value = 0.01
                dgvAnnot.Rows(RowNo).Cells(6).Value = 0.01 'CDF
                dgvAnnot.Rows(RowNo).Cells(7).Value = 0.99 'Reverse CDF
                dgvAnnot.Rows(RowNo).Cells(8).Value = P1Value
            End If
            DisplayCDFAnnot(0.01, P1Value, "P1")
        ElseIf P99 = False Then 'Enter P99 probability annotation settings:
            Dim P99Value As Double = GetValue(0.99)
            If RowNo > dgvAnnot.RowCount - 1 Then
                'dgvAnnot.Rows.Add({True, True, "Probability", 0.99, "P99", GetProbDens(0.99), 0.99, 0.01, P99Value})
                dgvAnnot.Rows.Add({True, False, "Probability", 0.99, "P99", GetProbDens(0.99), 0.99, 0.01, P99Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.99
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P99"
                dgvAnnot.Rows(RowNo).Cells(5).Value = GetProbDens(0.99) 'Probability Density
                'dgvAnnot.Rows(RowNo).Cells(5).Value = 0.99
                dgvAnnot.Rows(RowNo).Cells(6).Value = 0.99 'CDF
                dgvAnnot.Rows(RowNo).Cells(7).Value = 0.01 'Reverse CDF
                dgvAnnot.Rows(RowNo).Cells(8).Value = P99Value
            End If
            DisplayCDFAnnot(0.99, P99Value, "P99")
        Else
            'User defined probability annotation.
        End If
        dgvAnnot.AllowUserToAddRows = True
    End Sub




    Private Sub AddMeanAnnot(ByVal RowNo As Integer)
        'Add a mean annotation entry at the specified row number.

        Dim MeanAnnotated As Boolean = False 'If True then the Mean annotation is in the list

        'Dim MeanVal As Double 'The Mean value
        'Dim Prob As Double 'The corresponding probability value
        Dim ProbDens As Double 'The corresponding probability value
        Dim CDFVal As Double 'The corresponding CDF value

        dgvAnnot.AllowUserToAddRows = False

        'Check if the Mean value is already annotated:
        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            If dgvAnnot.Rows(I).Cells(2).Value = "Mean" Then MeanAnnotated = True
        Next

        If MeanAnnotated = False Then
            'MeanVal = txtAverage.Text
            'MeanVal = Data.Tables("DataTable").Compute("Avg(Random_Variable)", "")
            'Prob = GetProb(MeanVal)
            'Prob = GetProb(DataMean)
            'Prob = GetProbDens(DataMean)
            ProbDens = GetProbDens(DataMean)
            CDFVal = GetCDFValue(DataMean)
            If RowNo > dgvAnnot.RowCount - 1 Then
                'dgvAnnot.Rows.Add({True, True, "Mean", "", "Mean", Prob, MeanVal})
                'dgvAnnot.Rows.Add({True, True, "Mean", "", "Mean", Prob, DataMean})
                'dgvAnnot.Rows.Add({True, True, "Mean", "", "Mean", ProbDens, DataMean})
                dgvAnnot.Rows.Add({True, True, "Mean", "", "Mean", ProbDens, CDFVal, 1 - CDFVal, DataMean})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Mean"
                dgvAnnot.Rows(RowNo).Cells(3).Value = ""
                dgvAnnot.Rows(RowNo).Cells(4).Value = "Mean"
                'dgvAnnot.Rows(RowNo).Cells(5).Value = Prob
                dgvAnnot.Rows(RowNo).Cells(5).Value = ProbDens
                dgvAnnot.Rows(RowNo).Cells(6).Value = CDFVal
                dgvAnnot.Rows(RowNo).Cells(7).Value = 1 - CDFVal
                'dgvAnnot.Rows(RowNo).Cells(6).Value = MeanVal
                dgvAnnot.Rows(RowNo).Cells(8).Value = DataMean
            End If
            'DisplayAnnot(Prob, MeanVal, "Mean")
            'DisplayAnnot(Prob, DataMean, "Mean (" & DataMean & ")")
            'DisplayPDFAnnot(Prob, DataMean, "Mean (" & Format(DataMean, dgvAnnot.Rows(RowNo).Cells(7).Value) & ")")
            'DisplayPDFAnnot(Prob, DataMean, "Mean (" & Format(DataMean, dgvAnnot.Rows(RowNo).Cells(9).Value) & ")")
            DisplayPDFAnnot(ProbDens, DataMean, "Mean (" & Format(DataMean, dgvAnnot.Rows(RowNo).Cells(9).Value) & ")")
        Else
            'The Mean annotation is already in the list.
        End If

        dgvAnnot.AllowUserToAddRows = True
    End Sub



    Private Sub AddValueAnnot(ByVal RowNo As Integer)
        'Add a value annotation entry at the specified row number.

        Dim Value As Double = dgvAnnot.Rows(RowNo).Cells(3).Value  'The value to be annotated
        'Dim Prob As Double = GetProb(Value) 'The corresponding probability value
        'Dim Prob As Double = GetProbDens(Value) 'The corresponding probability value
        Dim ProbDens As Double = GetProbDens(Value) 'The corresponding probability value
        Dim CDFVal As Double = GetCDFValue(Value)

        dgvAnnot.AllowUserToAddRows = False
        If RowNo > dgvAnnot.RowCount - 1 Then
            'dgvAnnot.Rows.Add({True, True, "Value", Prob, "Value", Prob, Value})
            'dgvAnnot.Rows.Add({True, True, "Value", Value, "Value", ProbDens, Value})
            dgvAnnot.Rows.Add({True, True, "Value", Value, "Value", ProbDens, CDFVal, 1 - CDFVal, Value})
        Else
            dgvAnnot.Rows(RowNo).Cells(0).Value = True
            dgvAnnot.Rows(RowNo).Cells(1).Value = True
            dgvAnnot.Rows(RowNo).Cells(2).Value = "Value"
            'dgvAnnot.Rows(RowNo).Cells(3).Value = Prob
            dgvAnnot.Rows(RowNo).Cells(3).Value = Value
            dgvAnnot.Rows(RowNo).Cells(4).Value = "Value"
            'dgvAnnot.Rows(RowNo).Cells(5).Value = Prob
            dgvAnnot.Rows(RowNo).Cells(5).Value = ProbDens
            dgvAnnot.Rows(RowNo).Cells(6).Value = CDFVal
            dgvAnnot.Rows(RowNo).Cells(7).Value = 1 - CDFVal
            dgvAnnot.Rows(RowNo).Cells(8).Value = Value
        End If

        dgvAnnot.AllowUserToAddRows = True
    End Sub

    Private Sub AddStdDevAnnot(ByVal RowNo As Integer)
        'Add a standard deviation annotation entry at the specified row number.

        dgvAnnot.AllowUserToAddRows = False

        Dim SDev1 As Boolean = False 'Corresonds to a Standard Deviation parameter of 1
        Dim SDevN1 As Boolean = False 'Corresonds to a Standard Deviation parameter of -1
        Dim SDev2 As Boolean = False 'Corresonds to a Standard Deviation parameter of 2
        Dim SDevN2 As Boolean = False 'Corresonds to a Standard Deviation parameter of -2
        Dim SDev3 As Boolean = False 'Corresonds to a Standard Deviation parameter of 3
        Dim SDevN3 As Boolean = False 'Corresonds to a Standard Deviation parameter of -3
        Dim CDFVal As Double 'The Cumulative Distribution Value

        'Check if any Standard Deviation values are already annotated:
        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            If dgvAnnot.Rows(I).Cells(2).Value = "Standard Deviation" Then
                If dgvAnnot.Rows(I).Cells(3).Value = 1 Then SDev1 = True
                If dgvAnnot.Rows(I).Cells(3).Value = -1 Then SDevN1 = True
                If dgvAnnot.Rows(I).Cells(3).Value = 2 Then SDev2 = True
                If dgvAnnot.Rows(I).Cells(3).Value = -2 Then SDevN2 = True
                If dgvAnnot.Rows(I).Cells(3).Value = 3 Then SDev3 = True
                If dgvAnnot.Rows(I).Cells(3).Value = -3 Then SDevN3 = True
            End If
        Next

        'Dim StdDev As Double = txtStdDev.Text 'The Standard Deviation of the series
        'Dim Mean As Double = txtAverage.Text ' The Mean value of the series
        'Dim StdDev As Double = Math.Sqrt(Variance(True)) 'The Standard Deviation of the series
        'Dim Mean As Double = Data.Tables("DataTable").Compute("Avg(Random_Variable)", "") ' The Mean value of the series

        If SDev1 = False Then
            'Dim SDev1Value As Double = Mean + StdDev
            Dim SDev1Value As Double = DataMean + DataStdDev
            'Dim SDev1Prob As Double = GetProb(SDev1Value)
            Dim SDev1Prob As Double = GetProbDens(SDev1Value)
            CDFVal = GetCDFValue(SDev1Value)
            Dim Label As String = "1" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                'dgvAnnot.Rows.Add({True, True, "Standard Deviation", 1, Label, SDev1Prob, SDev1Value})
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 1, Label, SDev1Prob, CDFVal, 1 - CDFVal, SDev1Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 1
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev1Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = CDFVal
                dgvAnnot.Rows(RowNo).Cells(7).Value = 1 - CDFVal
                dgvAnnot.Rows(RowNo).Cells(8).Value = SDev1Value
            End If
            'DisplayAnnot(SDev1Prob, SDev1Value, Label)
            'DisplayPDFAnnot(SDev1Prob, SDev1Value, Label & " (" & Format(SDev1Value, dgvAnnot.Rows(RowNo).Cells(7).Value) & ")")
            DisplayPDFAnnot(SDev1Prob, SDev1Value, Label & " (" & Format(SDev1Value, dgvAnnot.Rows(RowNo).Cells(9).Value) & ")")
        ElseIf SDevN1 = False Then
            'Dim SDevN1Value As Double = Mean - StdDev
            Dim SDevN1Value As Double = DataMean - DataStdDev
            'Dim SDevN1Prob As Double = GetProb(SDevN1Value)
            Dim SDevN1Prob As Double = GetProbDens(SDevN1Value)
            CDFVal = GetCDFValue(SDevN1Value)
            Dim Label As String = "-1" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", -1, Label, SDevN1Prob, CDFVal, 1 - CDFVal, SDevN1Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 1
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDevN1Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = CDFVal
                dgvAnnot.Rows(RowNo).Cells(7).Value = 1 - CDFVal
                dgvAnnot.Rows(RowNo).Cells(8).Value = SDevN1Value
            End If
            'DisplayAnnot(SDevN1Prob, SDevN1Value, Label)
            'DisplayPDFAnnot(SDevN1Prob, SDevN1Value, Label & " (" & Format(SDevN1Value, dgvAnnot.Rows(RowNo).Cells(7).Value) & ")")
            DisplayPDFAnnot(SDevN1Prob, SDevN1Value, Label & " (" & Format(SDevN1Value, dgvAnnot.Rows(RowNo).Cells(9).Value) & ")")
        ElseIf SDev2 = False Then
            'Dim SDev2Value As Double = Mean + 2 * StdDev
            Dim SDev2Value As Double = DataMean + 2 * DataStdDev
            'Dim SDev2Prob As Double = GetProb(SDev2Value)
            Dim SDev2Prob As Double = GetProbDens(SDev2Value)
            CDFVal = GetCDFValue(SDev2Value)
            Dim Label As String = "2" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 2, Label, SDev2Prob, SDev2Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 2
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev2Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = CDFVal
                dgvAnnot.Rows(RowNo).Cells(7).Value = 1 - CDFVal
                dgvAnnot.Rows(RowNo).Cells(8).Value = SDev2Value
            End If
            'DisplayAnnot(SDev2Prob, SDev2Value, Label)
            'DisplayPDFAnnot(SDev2Prob, SDev2Value, Label & " (" & Format(SDev2Value, dgvAnnot.Rows(RowNo).Cells(7).Value) & ")")
            DisplayPDFAnnot(SDev2Prob, SDev2Value, Label & " (" & Format(SDev2Value, dgvAnnot.Rows(RowNo).Cells(9).Value) & ")")
        ElseIf SDevN2 = False Then
            'Dim SDevN2Value As Double = Mean - 2 * StdDev
            Dim SDevN2Value As Double = DataMean - 2 * DataStdDev
            'Dim SDevN2Prob As Double = GetProb(SDevN2Value)
            Dim SDevN2Prob As Double = GetProbDens(SDevN2Value)
            CDFVal = GetCDFValue(SDevN2Value)
            Dim Label As String = "-2" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", -2, Label, SDevN2Prob, CDFVal, 1 - CDFVal, SDevN2Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = -2
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDevN2Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = CDFVal
                dgvAnnot.Rows(RowNo).Cells(7).Value = 1 - CDFVal
                dgvAnnot.Rows(RowNo).Cells(8).Value = SDevN2Value
            End If
            'DisplayAnnot(SDevN2Prob, SDevN2Value, Label)
            'DisplayPDFAnnot(SDevN2Prob, SDevN2Value, Label & " (" & Format(SDevN2Value, dgvAnnot.Rows(RowNo).Cells(7).Value) & ")")
            DisplayPDFAnnot(SDevN2Prob, SDevN2Value, Label & " (" & Format(SDevN2Value, dgvAnnot.Rows(RowNo).Cells(9).Value) & ")")
        ElseIf SDev3 = False Then
            'Dim SDev3Value As Double = Mean + 3 * StdDev
            Dim SDev3Value As Double = DataMean + 3 * DataStdDev
            'Dim SDev3Prob As Double = GetProb(SDev3Value)
            Dim SDev3Prob As Double = GetProbDens(SDev3Value)
            CDFVal = GetCDFValue(SDev3Value)
            Dim Label As String = "3" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 3, Label, SDev3Prob, CDFVal, 1 - CDFVal, SDev3Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 3
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev3Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = CDFVal
                dgvAnnot.Rows(RowNo).Cells(7).Value = 1 - CDFVal
                dgvAnnot.Rows(RowNo).Cells(8).Value = SDev3Value
            End If
            'DisplayAnnot(SDev3Prob, SDev3Value, Label)
            'DisplayPDFAnnot(SDev3Prob, SDev3Value, Label & " (" & Format(SDev3Value, dgvAnnot.Rows(RowNo).Cells(7).Value) & ")")
            DisplayPDFAnnot(SDev3Prob, SDev3Value, Label & " (" & Format(SDev3Value, dgvAnnot.Rows(RowNo).Cells(9).Value) & ")")
        ElseIf SDevN3 = False Then
            'Dim SDevN3Value As Double = Mean - 3 * StdDev
            Dim SDevN3Value As Double = DataMean - 3 * DataStdDev
            'Dim SDevN3Prob As Double = GetProb(SDevN3Value)
            Dim SDevN3Prob As Double = GetProbDens(SDevN3Value)
            CDFVal = GetCDFValue(SDevN3Value)
            Dim Label As String = "-3" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", -3, Label, SDevN3Prob, CDFVal, 1 - CDFVal, SDevN3Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = -3
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDevN3Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = CDFVal
                dgvAnnot.Rows(RowNo).Cells(7).Value = 1 - CDFVal
                dgvAnnot.Rows(RowNo).Cells(8).Value = SDevN3Value
            End If
            'DisplayAnnot(SDevN3Prob, SDevN3Value, Label)
            'DisplayPDFAnnot(SDevN3Prob, SDevN3Value, Label & " (" & Format(SDevN3Value, dgvAnnot.Rows(RowNo).Cells(7).Value) & ")")
            DisplayPDFAnnot(SDevN3Prob, SDevN3Value, Label & " (" & Format(SDevN3Value, dgvAnnot.Rows(RowNo).Cells(9).Value) & ")")
        Else
            'Dim SDev0Value As Double = Mean
            Dim SDev0Value As Double = DataMean
            'Dim SDev0Prob As Double = GetProb(SDev0Value)
            Dim SDev0Prob As Double = GetProbDens(SDev0Value)
            CDFVal = GetCDFValue(SDev0Value)
            Dim Label As String = "0" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 0, Label, SDev0Prob, CDFVal, 1 - CDFVal, SDev0Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev0Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = CDFVal
                dgvAnnot.Rows(RowNo).Cells(7).Value = 1 - CDFVal
                dgvAnnot.Rows(RowNo).Cells(8).Value = SDev0Value
            End If
            'DisplayAnnot(SDev0Prob, SDev0Value, Label)
            'DisplayPDFAnnot(SDev0Prob, SDev0Value, Label & " (" & Format(SDev0Value, dgvAnnot.Rows(RowNo).Cells(7).Value) & ")")
            DisplayPDFAnnot(SDev0Prob, SDev0Value, Label & " (" & Format(SDev0Value, dgvAnnot.Rows(RowNo).Cells(9).Value) & ")")
        End If

        dgvAnnot.AllowUserToAddRows = True
    End Sub



    'Private Sub DisplayAnnot(ByVal Prob As Double, ByVal Value As Double, ByVal Text As String)
    Private Sub DisplayCDFAnnot(ByVal Prob As Double, ByVal Value As Double, ByVal Text As String)
        'Display the probability annotation on the Series Analysis charts.

        'Display the annotation on the CDF chart: ========================================================

        'Add the vertical bar:
        Dim CDFPoint As New DataVisualization.Charting.DataPoint
        CDFPoint.XValue = Value
        CDFPoint.SetValueY(Prob)
        Chart1.Series("CdfVertBar").Points.Add(CDFPoint)

        'Add the label:
        Dim Annot As New DataVisualization.Charting.TextAnnotation
        Annot.AxisX = Chart1.ChartAreas(0).AxisX
        'Annot.AxisY = Chart1.ChartAreas(0).AxisY
        Annot.AxisY = Chart1.ChartAreas(0).AxisY2
        Annot.AnchorX = Value
        Annot.AnchorY = Prob

        Annot.AnchorAlignment = ContentAlignment.MiddleRight

        Annot.Text = Text
        Annot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)

        Chart1.Annotations.Add(Annot)

        'Display the annotation on the Histogram: ====================================================

        'Dim HistPoint As New DataVisualization.Charting.DataPoint
        'HistPoint.XValue = Value
        'Dim YValue As Double = GetHistProb(Value)
        'HistPoint.SetValueY(YValue)
        'Chart1.Series("HistPoints").Points.Add(HistPoint)

    End Sub

    Private Sub DisplayPDFAnnot(ByVal Prob As Double, ByVal Value As Double, ByVal Text As String)
        'Display the probability annotation on the Series Analysis charts.

        'Display the annotation on the PDF chart: ========================================================

        'Add the vertical bar:
        Dim CDFPoint As New DataVisualization.Charting.DataPoint
        CDFPoint.XValue = Value
        CDFPoint.SetValueY(Prob)
        Chart1.Series("PdfVertBar").Points.Add(CDFPoint)

        'Add the label:
        Dim Annot As New DataVisualization.Charting.TextAnnotation
        Annot.AxisX = Chart1.ChartAreas(0).AxisX
        Annot.AxisY = Chart1.ChartAreas(0).AxisY
        Annot.AnchorX = Value
        Annot.AnchorY = Prob

        Annot.AnchorAlignment = ContentAlignment.MiddleRight

        Annot.Text = Text
        Annot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)

        Chart1.Annotations.Add(Annot)

    End Sub

    Private Sub dgvAnnot_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAnnot.CellContentClick

    End Sub

    Private Sub dgvAnnot_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvAnnot.DataError
        Main.Message.AddWarning(e.Exception.Message & vbCrLf)
    End Sub

    Private Sub UpdateIntervalProb()
        'Update the interval probability

        LowerValCDF = GetCDFValue(RVLowerVal)
        UpperValCDF = GetCDFValue(RVUpperVal)
        RVIntervalProb = UpperValCDF - LowerValCDF

        If ShowProbPercent Then
            'txtLowerValCDF.Text = LowerValCDF * 100
            txtLowerValCDF.Text = Format(LowerValCDF * 100, ProbFormat) & " %"
            'txtUpperValCDF.Text = UpperValCDF * 100
            txtUpperValCDF.Text = Format(UpperValCDF * 100, ProbFormat) & " %"
            'txtRVIntervalProb.Text = RVIntervalProb * 100
            txtRVIntervalProb.Text = Format(RVIntervalProb * 100, ProbFormat) & " %"
        Else
            'txtLowerValCDF.Text = LowerValCDF
            txtLowerValCDF.Text = Format(LowerValCDF, ProbFormat)
            'txtUpperValCDF.Text = UpperValCDF
            txtUpperValCDF.Text = Format(UpperValCDF, ProbFormat)
            'txtRVIntervalProb.Text = RVIntervalProb
            txtRVIntervalProb.Text = Format(RVIntervalProb, ProbFormat)
        End If


        If RVUpperVal > RVLowerVal Then

        ElseIf RVUpperVal = RVLowerVal Then

        Else

        End If

        'If ShowPDFIntervalProb Then ShowPdfInterval()





    End Sub

    Private Sub txtRVLowerVal_TextChanged(sender As Object, e As EventArgs) Handles txtRVLowerVal.TextChanged
        Try
            If txtRVLowerVal.Focused Then
                RVLowerVal = txtRVLowerVal.Text
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtRVUpperVal_TextChanged(sender As Object, e As EventArgs) Handles txtRVUpperVal.TextChanged
        Try
            If txtRVUpperVal.Focused Then
                RVUpperVal = txtRVUpperVal.Text
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rbDecimal_CheckedChanged(sender As Object, e As EventArgs) Handles rbDecimal.CheckedChanged
        Try
            If rbDecimal.Checked Then
                ShowProbPercent = False
            Else
                ShowProbPercent = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtProbFormat_TextChanged(sender As Object, e As EventArgs) Handles txtProbFormat.TextChanged
        If txtProbFormat.Focused Then
            ProbFormat = txtProbFormat.Text
            UpdateIntervalProb()
        End If
    End Sub


    'Private Sub ShowPdfInterval()
    Public Sub ShowPdfInterval()
        'Show the selected Random Variable interval on the PDF chart:

        Chart1.Series("PdfShade").Points.Clear()

        'The lower RV value is RVLowerVal
        'The upper RV value is RVUpperVal

        If ShowPDFIntervalProb Then
            If ShowPDF Then
                Dim LowerPixel As Double = Chart1.ChartAreas(0).AxisX.ValueToPixelPosition(RVLowerVal)
                Dim UpperPixel As Double = Chart1.ChartAreas(0).AxisX.ValueToPixelPosition(RVUpperVal)
                Dim PixelRange As Double = UpperPixel - LowerPixel
                Dim PixelInterval As Integer = 4 'The interval in pixels between shade lines.
                Dim NShadeLines As Integer = PixelRange / PixelInterval

                Dim ValueInterval As Double = (RVUpperVal - RVLowerVal) / NShadeLines

                Dim I As Integer
                Dim ShadeVal As Double
                Dim ShadeProbDens As Double
                For I = 0 To NShadeLines
                    ShadeVal = RVLowerVal + I * ValueInterval
                    ShadeProbDens = GetProbDens(ShadeVal)
                    Dim PDFShadePoint As New DataVisualization.Charting.DataPoint
                    PDFShadePoint.XValue = ShadeVal
                    PDFShadePoint.SetValueY(ShadeProbDens)
                    Chart1.Series("PdfShade").Points.Add(PDFShadePoint)
                Next

                ''Add probability annotation:
                'Dim ProbAnnot As New DataVisualization.Charting.TextAnnotation
                'ProbAnnot.AxisX = Chart1.ChartAreas(0).AxisX
                'ProbAnnot.AxisY = Chart1.ChartAreas(0).AxisY
                'ProbAnnot.AnchorX = (RVLowerVal + RVUpperVal) / 2
                'ProbAnnot.AnchorY = Chart1.ChartAreas(0).AxisY.Minimum + (Chart1.ChartAreas(0).AxisY.Maximum - Chart1.ChartAreas(0).AxisY.Minimum) / 8
                'ProbAnnot.AnchorAlignment = ContentAlignment.BottomCenter
                'ProbAnnot.Text = txtRVIntervalProb.Text
                'ProbAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
                'Chart1.Annotations.Add(ProbAnnot)

            End If
        End If
    End Sub

    Private Sub chkShowOnPDF_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowOnPDF.CheckedChanged
        If chkShowOnPDF.Focused Then
            If chkShowOnPDF.Checked Then
                ShowPDFIntervalProb = True
                ShowPdfInterval()
            Else
                ShowPDFIntervalProb = False
                ShowPdfInterval()
            End If
        End If
    End Sub

    Private Sub Chart1_PostPaint(sender As Object, e As ChartPaintEventArgs) Handles Chart1.PostPaint
        ShowPdfInterval()

        'If ShowPDFIntervalProb Then
        '    If ShowPDF Then
        '        UpdateIntervalProb()
        '        'Add probability annotation:
        '        Dim ProbAnnot As New DataVisualization.Charting.TextAnnotation
        '        ProbAnnot.AxisX = Chart1.ChartAreas(0).AxisX
        '        ProbAnnot.AxisY = Chart1.ChartAreas(0).AxisY
        '        ProbAnnot.AnchorX = (RVLowerVal + RVUpperVal) / 2
        '        'ProbAnnot.AnchorY = Chart1.ChartAreas(0).AxisY.Minimum + (Chart1.ChartAreas(0).AxisY.Maximum - Chart1.ChartAreas(0).AxisY.Minimum) / 16
        '        ProbAnnot.AnchorY = Chart1.ChartAreas(0).AxisY.Minimum
        '        ProbAnnot.AnchorAlignment = ContentAlignment.BottomCenter
        '        ProbAnnot.Text = txtRVIntervalProb.Text
        '        ProbAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
        '        Chart1.Annotations.Add(ProbAnnot)
        '    End If
        'End If

    End Sub



#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class