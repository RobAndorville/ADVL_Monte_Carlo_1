Public Class frmChart
    'Chart display form.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    Public WithEvents ChartSettings As frmChartSettings

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _dataSource As Object = Nothing 'DataSource links to the oject containing the Data to be analysed. (Input, Processed, Distribution or MonteCarlo objects in the Main form.)
    'Every DataSource object must contain: Data, ChartList, ChartName
    Property DataSource As Object
        Get
            Return _dataSource
        End Get
        Set(value As Object)
            _dataSource = value
            'txtDatasetName.Text = DataSource.Name
            'lblTableCount.Text = DataSource.Data.Tables.Count
            'If DataSource.Data.Tables.Count > 0 Then
            '    lblTableNo.Text = "1"
            '    cmbDataTable.Items.Clear()
            '    For Each item In DataSource.Data.Tables
            '        cmbDataTable.Items.Add(item.TableName)
            '    Next
            '    cmbDataTable.SelectedIndex = 0
            '    TableName = cmbDataTable.SelectedItem.ToString
            '    NRows = DataSource.Data.Tables(0).Rows.Count
            'Else
            '    lblTableNo.Text = "0"
            'End If

            'Show list of available charts:
            For Each item In _dataSource.ChartList
                cmbChartList.Items.Add(item.Key)
            Next
            If _dataSource.ChartName <> "" Then
                cmbChartList.SelectedIndex = cmbChartList.FindStringExact(_dataSource.ChartName)
            End If
        End Set
    End Property

    'NOTE: Source is being replaced by DataSource!!!
    'Private _source As String = "Input" 'The data source for the chart. (Input, Processed or Distribution)
    'Property Source As String
    '    Get
    '        Return _source
    '    End Get
    '    Set(value As String)
    '        _source = value
    '        'cmbSeriesDataSource.SelectedIndex = cmbSeriesDataSource.FindStringExact(_source)

    '        Select Case Source
    '            Case "Input"
    '                For Each item In Main.Input.ChartList
    '                    cmbChartList.Items.Add(item.Key)
    '                Next
    '                If Main.Input.ChartName <> "" Then
    '                    cmbChartList.SelectedIndex = cmbChartList.FindStringExact(Main.Input.ChartName)
    '                End If
    '            Case "Processed"
    '                For Each item In Main.Processed.ChartList
    '                    cmbChartList.Items.Add(item.Key)
    '                Next
    '                If Main.Processed.ChartName <> "" Then
    '                    cmbChartList.SelectedIndex = cmbChartList.FindStringExact(Main.Processed.ChartName)
    '                End If
    '            Case "Distribution"
    '                For Each item In Main.Distribution.ChartList
    '                    cmbChartList.Items.Add(item.Key)
    '                Next
    '                If Main.Distribution.ChartName <> "" Then
    '                    cmbChartList.SelectedIndex = cmbChartList.FindStringExact(Main.Distribution.ChartName)
    '                End If
    '            Case "MonteCarlo"
    '                For Each item In Main.MonteCarlo.ChartList
    '                    cmbChartList.Items.Add(item.Key)
    '                Next
    '                If Main.MonteCarlo.ChartName <> "" Then
    '                    cmbChartList.SelectedIndex = cmbChartList.FindStringExact(Main.MonteCarlo.ChartName)
    '                End If
    '            Case Else
    '                Main.Message.AddWarning("Unknown data source: " & Source & vbCrLf)
    '        End Select
    '    End Set
    'End Property

    'Private _tableName As String = "DataTable" 'The name of the table containing the data to plot.
    Private _tableName As String = "" 'The name of the table containing the data to plot.
    Property TableName As String
        Get
            Return _tableName
        End Get
        Set(value As String)
            _tableName = value
        End Set
    End Property

    Private _chartName As String = "" 'The name of the chart shown on this form.
    Property ChartName As String
        Get
            Return _chartName
        End Get
        Set(value As String)
            _chartName = value
        End Set
    End Property

    Private _formNo As Integer = -1 'Multiple instances of this form can be displayed. FormNo is the index number of the form in ChartList.
    'If the form is included in Main.ChartList() then FormNo will be > -1 --> when exiting set Main.ClosedFormNo and call Main.ChartClosed(). 
    Public Property FormNo As Integer
        Get
            Return _formNo
        End Get
        Set(ByVal value As Integer)
            _formNo = value
            'Debug.Print("FormNo = " & _formNo)
        End Set
    End Property



#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML files - Read and write XML files." '=====================================================================================================================================

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <!---->
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & FormNo & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & FormNo & ".xml"

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

            CheckFormPos()
        End If
    End Sub

    Private Sub CheckFormPos()
        'Public Sub CheckFormPos()
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

        'Main.Message.Add("CheckFormPos() run." & vbCrLf)

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

        RestoreFormSettings()   'Restore the form settings

        Chart1.SuppressExceptions = True


    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form

        If FormNo > -1 Then
            Main.ClosedFormNo = FormNo 'The Main form property ClosedFormNo is set to this form number. This is used in the ChartFormClosed method to select the correct form to set to nothing.
        End If

        Me.Close() 'Close the form
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        Else
            'Dont save settings if the form is minimised.
        End If
    End Sub

    Private Sub frmChart_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If FormNo > -1 Then
            Main.ChartClosed()
        End If
    End Sub

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

    Private Sub cmbSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        'Open the Chart Settings form:
        If IsNothing(ChartSettings) Then
            ChartSettings = New frmChartSettings
            ChartSettings.ChartFormNo = FormNo
            ChartSettings.myParent = Me

            ChartSettings.Show()

            ChartSettings.DataSource = DataSource
            ChartSettings.TableName = TableName
            ChartSettings.ChartName = ChartName

        Else
            ChartSettings.Show()
        End If
    End Sub

    Private Sub ChartSettings_FormClosed(sender As Object, e As FormClosedEventArgs) Handles ChartSettings.FormClosed
        ChartSettings = Nothing
    End Sub

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Private Sub btnPlotChart_Click(sender As Object, e As EventArgs) Handles btnPlotChart.Click
        'Plot the chart.
        Plot()
    End Sub

    Public Sub Plot()
        'Plot the selected chart.
        If cmbChartList.SelectedIndex = -1 Then
            Main.Message.AddWarning("A chart has not been selected for plotting" & vbCrLf)
        Else
            'Open the chart.
            PlotChart(ChartName)
        End If
    End Sub

    'NOTE: This new version uses DataSource
    Private Sub PlotChart(ByVal ChartName As String)
        'Plot the data chart.

        If DataSource.ChartList.ContainsKey(ChartName) Then
            'Apply Chart Areas:
            Dim ChartXml As System.Xml.Linq.XDocument
            ChartXml = DataSource.ChartList(ChartName)

            If ChartXml.<ChartSettings>.<TableName>.Value <> Nothing Then TableName = ChartXml.<ChartSettings>.<TableName>.Value

            If ChartXml.<ChartSettings>.<FormHeight>.Value <> Nothing Then Me.Height = ChartXml.<ChartSettings>.<FormHeight>.Value
            If ChartXml.<ChartSettings>.<FormWidth>.Value <> Nothing Then Me.Width = ChartXml.<ChartSettings>.<FormWidth>.Value
            If ChartXml.<ChartSettings>.<FormTop>.Value <> Nothing Then Me.Top = ChartXml.<ChartSettings>.<FormTop>.Value
            If ChartXml.<ChartSettings>.<FormLeft>.Value <> Nothing Then Me.Left = ChartXml.<ChartSettings>.<FormLeft>.Value

            Dim AreaInfo = From item In ChartXml.<ChartSettings>.<ChartAreasCollection>.<ChartArea>
            ApplyChartAreas(AreaInfo)
            'Apply Chart Labels:
            Dim TitleInfo = From item In ChartXml.<ChartSettings>.<TitlesCollection>.<Title>
            ApplyChartLabels(TitleInfo)
            'Apply Chart Series:
            Dim SeriesInfo = From item In ChartXml.<ChartSettings>.<SeriesCollection>.<Series>
            ApplyChartSeries(SeriesInfo)
        Else
            Main.Message.AddWarning("The Input Data chart named " & ChartName & " was not found in the Chart list." & vbCrLf)
        End If
    End Sub

    ''NOTE: This old version has been replaced by PlotChart
    'Private Sub PlotInputChart(ByVal ChartName As String)
    '    'Plot the Input Data chart.

    '    If Main.Input.ChartList.ContainsKey(ChartName) Then
    '        Main.Input.ChartName = ChartName 'Update the selected chart in Main.Input.
    '        'Apply Chart Areas:
    '        Dim AreaInfo = From item In Main.Input.ChartList(ChartName).<ChartSettings>.<ChartAreasCollection>.<ChartArea>
    '        ApplyChartAreas(AreaInfo)
    '        'Apply Chart Labels:
    '        Dim TitleInfo = From item In Main.Input.ChartList(ChartName).<ChartSettings>.<TitlesCollection>.<Title>
    '        ApplyChartLabels(TitleInfo)
    '        'Apply Chart Series:
    '        Dim SeriesInfo = From item In Main.Input.ChartList(ChartName).<ChartSettings>.<SeriesCollection>.<Series>
    '        ApplyChartSeries(SeriesInfo)
    '    Else
    '        Main.Message.AddWarning("The Input Data chart named " & ChartName & " was not found in the Chart list." & vbCrLf)
    '    End If
    'End Sub

    ''NOTE: This old version has been replaced by PlotChart
    'Private Sub PlotProcessedChart(ByVal ChartName As String)
    '    'Plot the Processed Data chart.

    '    If Main.Processed.ChartList.ContainsKey(ChartName) Then
    '        Main.Processed.ChartName = ChartName 'Update the selected chart in Main.Processed.
    '        'Apply Chart Areas:
    '        Dim AreaInfo = From item In Main.Processed.ChartList(ChartName).<ChartSettings>.<ChartAreasCollection>.<ChartArea>
    '        ApplyChartAreas(AreaInfo)
    '        'Apply Chart Labels:
    '        Dim TitleInfo = From item In Main.Processed.ChartList(ChartName).<ChartSettings>.<TitlesCollection>.<Title>
    '        ApplyChartLabels(TitleInfo)
    '        'Apply Chart Series:
    '        Dim SeriesInfo = From item In Main.Processed.ChartList(ChartName).<ChartSettings>.<SeriesCollection>.<Series>
    '        ApplyChartSeries(SeriesInfo)
    '    Else
    '        Main.Message.AddWarning("The Processed Data chart named " & ChartName & " was not found in the Chart list." & vbCrLf)
    '    End If
    'End Sub

    ''NOTE: This old version has been replaced by PlotChart
    'Private Sub PlotDistributionChart(ByVal ChartName As String)
    '    'Plot the Distribution Data chart.

    '    If Main.Distribution.ChartList.ContainsKey(ChartName) Then
    '        Main.Distribution.ChartName = ChartName 'Update the selected chart in Main.Distribution.
    '        'Apply Chart Areas:
    '        Dim AreaInfo = From item In Main.Distribution.ChartList(ChartName).<ChartSettings>.<ChartAreasCollection>.<ChartArea>
    '        ApplyChartAreas(AreaInfo)
    '        'Apply Chart Labels:
    '        Dim TitleInfo = From item In Main.Distribution.ChartList(ChartName).<ChartSettings>.<TitlesCollection>.<Title>
    '        ApplyChartLabels(TitleInfo)
    '        'Apply Chart Series:
    '        Dim SeriesInfo = From item In Main.Distribution.ChartList(ChartName).<ChartSettings>.<SeriesCollection>.<Series>
    '        ApplyChartSeries(SeriesInfo)
    '    Else
    '        Main.Message.AddWarning("The Distribution Data chart named " & ChartName & " was not found in the Chart list." & vbCrLf)
    '    End If
    'End Sub

    'NOTE: This old version has been replaced by PlotChart
    Private Sub PlotMonteCarloChart(ByVal ChartName As String)
        'Plot the Monte Carlo Data chart.

        If Main.MonteCarlo.ChartList.ContainsKey(ChartName) Then
            Main.MonteCarlo.ChartName = ChartName 'Update the selected chart in Main.Distribution.
            'Apply Chart Areas:
            Dim AreaInfo = From item In Main.MonteCarlo.ChartList(ChartName).<ChartSettings>.<ChartAreasCollection>.<ChartArea>
            ApplyChartAreas(AreaInfo)
            'Apply Chart Labels:
            Dim TitleInfo = From item In Main.MonteCarlo.ChartList(ChartName).<ChartSettings>.<TitlesCollection>.<Title>
            ApplyChartLabels(TitleInfo)
            'Apply Chart Series:
            Dim SeriesInfo = From item In Main.MonteCarlo.ChartList(ChartName).<ChartSettings>.<SeriesCollection>.<Series>
            ApplyChartSeries(SeriesInfo)
        Else
            Main.Message.AddWarning("The Distribution Data chart named " & ChartName & " was not found in the Chart list." & vbCrLf)
        End If
    End Sub


    Private Sub ApplyChartLabels(ByRef TitleInfo As IEnumerable(Of XElement))
        'Apply the Chart Labels in TitleInfo to the chart.
        Dim NTitles As Integer = TitleInfo.Count
        Dim TitleName As String
        Dim myFontStyle As FontStyle
        Dim myFontSize As Single
        Chart1.Titles.Clear()
        For Each item In TitleInfo
            TitleName = item.<Name>.Value
            Chart1.Titles.Add(TitleName).Text = item.<Text>.Value

            Chart1.Titles(TitleName).IsDockedInsideChartArea = False
            If item.<ChartArea>.Value <> Nothing Then Chart1.Titles(TitleName).DockedToChartArea = item.<ChartArea>.Value

            Chart1.Titles(TitleName).Text = item.<Text>.Value
            Chart1.Titles(TitleName).TextOrientation = [Enum].Parse(GetType(DataVisualization.Charting.TextOrientation), item.<TextOrientation>.Value)

            Try
                Chart1.Titles(TitleName).Alignment = [Enum].Parse(GetType(ContentAlignment), item.<Alignment>.Value)
            Catch ex As Exception
                Main.Message.AddWarning("Chart title alighment: " & ex.Message & vbCrLf)
            End Try

            Chart1.Titles(TitleName).ForeColor = Color.FromArgb(item.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<Font>.<Size>.Value
            Chart1.Titles(TitleName).Font = New Font(item.<Font>.<Name>.Value, myFontSize, myFontStyle)
        Next
    End Sub

    Private Sub ApplyChartAreas(ByRef AreaInfo As IEnumerable(Of XElement))
        'Apply the Chart Areas in AreaInfo to the chart.
        Dim NAreas As Integer = AreaInfo.Count
        Dim AreaName As String
        Dim myFontStyle As FontStyle
        Dim myFontSize As Single
        Chart1.ChartAreas.Clear()
        For Each item In AreaInfo
            AreaName = item.<Name>.Value
            Chart1.ChartAreas.Add(AreaName)

            If item.<CursorXIsUserEnabled>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorX.IsUserEnabled = item.<CursorXIsUserEnabled>.Value
            If item.<CursorYIsUserEnabled>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorY.IsUserEnabled = item.<CursorYIsUserEnabled>.Value
            If item.<CursorXInterval>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorX.Interval = item.<CursorXInterval>.Value
            If item.<CursorYInterval>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorY.Interval = item.<CursorYInterval>.Value
            If item.<CursorXIsUserSelectionEnabled>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorX.IsUserSelectionEnabled = item.<CursorXIsUserSelectionEnabled>.Value
            If item.<CursorYIsUserSelectionEnabled>.Value <> Nothing Then Chart1.ChartAreas(AreaName).CursorY.IsUserSelectionEnabled = item.<CursorYIsUserSelectionEnabled>.Value

            'AxisX Properties:
            Chart1.ChartAreas(AreaName).AxisX.Title = item.<AxisX>.<Title>.<Text>.Value
            Chart1.ChartAreas(AreaName).AxisX.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisX>.<Title>.<Alignment>.Value)
            Chart1.ChartAreas(AreaName).AxisX.TitleForeColor = Color.FromArgb(item.<AxisX>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisX>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisX>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisX>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisX>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisX>.<Title>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisX.TitleFont = New Font(item.<AxisX>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)
            If item.<AxisX>.<LabelStyleFormat>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisX.LabelStyle.Format = item.<AxisX>.<LabelStyleFormat>.Value
            If item.<AxisX>.<AutoMinimum>.Value = True Then
                Chart1.ChartAreas(AreaName).AxisX.Minimum = Double.NaN
            Else
                If item.<AxisX>.<Minimum>.Value <> Nothing Then
                    Chart1.ChartAreas(AreaName).AxisX.Minimum = item.<AxisX>.<Minimum>.Value
                Else
                    Chart1.ChartAreas(AreaName).AxisX.Minimum = Double.NaN
                End If
            End If

            If item.<AxisX>.<AutoMaximum>.Value = True Then
                Chart1.ChartAreas(AreaName).AxisX.Maximum = Double.NaN
            Else
                If item.<AxisX>.<Maximum>.Value <> Nothing Then
                    Chart1.ChartAreas(AreaName).AxisX.Maximum = item.<AxisX>.<Maximum>.Value
                Else
                    Chart1.ChartAreas(AreaName).AxisX.Maximum = Double.NaN
                End If
            End If
            Chart1.ChartAreas(AreaName).AxisX.LineWidth = item.<AxisX>.<LineWidth>.Value
            Chart1.ChartAreas(AreaName).AxisX.Interval = item.<AxisX>.<Interval>.Value
            Chart1.ChartAreas(AreaName).AxisX.IntervalOffset = item.<AxisX>.<IntervalOffset>.Value
            Chart1.ChartAreas(AreaName).AxisX.Crossing = item.<AxisX>.<Crossing>.Value
            If item.<AxisX>.<AutoInterval>.Value = True Then Chart1.ChartAreas(AreaName).AxisX.Interval = Double.NaN
            If item.<AxisX>.<ScaleViewZoomable>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisX.ScaleView.Zoomable = item.<AxisX>.<ScaleViewZoomable>.Value

            If item.<AxisX>.<RoundAxisValues>.Value <> Nothing Then
                If item.<AxisX>.<RoundAxisValues>.Value = True Then Chart1.ChartAreas(AreaName).AxisX.RoundAxisValues()
            End If

            'AxisX2 Properties:
            Chart1.ChartAreas(AreaName).AxisX2.Title = item.<AxisX2>.<Title>.<Text>.Value
            Chart1.ChartAreas(AreaName).AxisX2.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisX2>.<Title>.<Alignment>.Value)
            Chart1.ChartAreas(AreaName).AxisX2.TitleForeColor = Color.FromArgb(item.<AxisX2>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisX2>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisX2>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisX2>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisX2>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisX2>.<Title>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisX2.TitleFont = New Font(item.<AxisX2>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)
            If item.<AxisX2>.<LabelStyleFormat>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisX2.LabelStyle.Format = item.<AxisX2>.<LabelStyleFormat>.Value
            Chart1.ChartAreas(AreaName).AxisX2.Minimum = item.<AxisX2>.<Minimum>.Value
            If item.<AxisX2>.<AutoMinimum>.Value = True Then Chart1.ChartAreas(AreaName).AxisX2.Minimum = Double.NaN
            Chart1.ChartAreas(AreaName).AxisX2.Maximum = item.<AxisX2>.<Maximum>.Value
            If item.<AxisX2>.<AutoMaximum>.Value = True Then Chart1.ChartAreas(AreaName).AxisX2.Maximum = Double.NaN
            Chart1.ChartAreas(AreaName).AxisX2.LineWidth = item.<AxisX2>.<LineWidth>.Value
            Chart1.ChartAreas(AreaName).AxisX2.Interval = item.<AxisX2>.<Interval>.Value
            Chart1.ChartAreas(AreaName).AxisX2.IntervalOffset = item.<AxisX2>.<IntervalOffset>.Value
            Chart1.ChartAreas(AreaName).AxisX2.Crossing = item.<AxisX2>.<Crossing>.Value
            If item.<AxisX2>.<AutoInterval>.Value <> Nothing Then If item.<AxisX2>.<AutoInterval>.Value = True Then Chart1.ChartAreas(AreaName).AxisX2.Interval = Double.NaN

            If item.<AxisX2>.<ScaleViewZoomable>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisX2.ScaleView.Zoomable = item.<AxisX2>.<ScaleViewZoomable>.Value

            If item.<AxisX2>.<RoundAxisValues>.Value <> Nothing Then
                If item.<AxisX2>.<RoundAxisValues>.Value = True Then Chart1.ChartAreas(AreaName).AxisX2.RoundAxisValues()
            End If

            'AxisY Properties:
            Chart1.ChartAreas(AreaName).AxisY.Title = item.<AxisY>.<Title>.<Text>.Value
            Chart1.ChartAreas(AreaName).AxisY.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisY>.<Title>.<Alignment>.Value)
            Chart1.ChartAreas(AreaName).AxisY.TitleForeColor = Color.FromArgb(item.<AxisY>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisY>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisY>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisY>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisY>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisY>.<Title>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisY.TitleFont = New Font(item.<AxisY>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)
            If item.<AxisY>.<LabelStyleFormat>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisY.LabelStyle.Format = item.<AxisY>.<LabelStyleFormat>.Value
            Chart1.ChartAreas(AreaName).AxisY.Minimum = item.<AxisY>.<Minimum>.Value
            If item.<AxisY>.<AutoMinimum>.Value = True Then
                Chart1.ChartAreas(AreaName).AxisY.Minimum = Double.NaN
            Else
                If item.<AxisY>.<Maximum>.Value <> Nothing Then
                    Chart1.ChartAreas(AreaName).AxisY.Minimum = item.<AxisY>.<Minimum>.Value
                Else
                    Chart1.ChartAreas(AreaName).AxisY.Minimum = Double.NaN
                End If
            End If

            If item.<AxisY>.<AutoMaximum>.Value = True Then
                Chart1.ChartAreas(AreaName).AxisY.Maximum = Double.NaN
            Else
                If item.<AxisY>.<Maximum>.Value <> Nothing Then
                    Chart1.ChartAreas(AreaName).AxisY.Maximum = item.<AxisY>.<Maximum>.Value
                Else
                    Chart1.ChartAreas(AreaName).AxisY.Maximum = Double.NaN
                End If
            End If

            Chart1.ChartAreas(AreaName).AxisY.LineWidth = item.<AxisY>.<LineWidth>.Value
            Chart1.ChartAreas(AreaName).AxisY.Interval = item.<AxisY>.<Interval>.Value
            Chart1.ChartAreas(AreaName).AxisY.IntervalOffset = item.<AxisY>.<IntervalOffset>.Value
            Chart1.ChartAreas(AreaName).AxisY.Crossing = item.<AxisY>.<Crossing>.Value
            If item.<AxisY>.<AutoInterval>.Value <> Nothing Then If item.<AxisY>.<AutoInterval>.Value = True Then Chart1.ChartAreas(AreaName).AxisY.Interval = Double.NaN
            If item.<AxisY>.<ScaleViewZoomable>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisY.ScaleView.Zoomable = item.<AxisY>.<ScaleViewZoomable>.Value

            'AxisY2 Properties:
            Chart1.ChartAreas(AreaName).AxisY2.Title = item.<AxisY2>.<Title>.<Text>.Value
            Chart1.ChartAreas(AreaName).AxisY2.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisY2>.<Title>.<Alignment>.Value)
            Chart1.ChartAreas(AreaName).AxisY2.TitleForeColor = Color.FromArgb(item.<AxisY2>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisY2>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisY2>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisY2>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisY2>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisY2>.<Title>.<Font>.<Size>.Value
            Chart1.ChartAreas(AreaName).AxisY2.TitleFont = New Font(item.<AxisY2>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)
            If item.<AxisY2>.<LabelStyleFormat>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisY2.LabelStyle.Format = item.<AxisY2>.<LabelStyleFormat>.Value
            Chart1.ChartAreas(AreaName).AxisY2.Minimum = item.<AxisY2>.<Minimum>.Value
            If item.<AxisY2>.<AutoMinimum>.Value = True Then Chart1.ChartAreas(AreaName).AxisY2.Minimum = Double.NaN
            Chart1.ChartAreas(AreaName).AxisY2.Maximum = item.<AxisY2>.<Maximum>.Value
            If item.<AxisY2>.<AutoMaximum>.Value = True Then Chart1.ChartAreas(AreaName).AxisY2.Maximum = Double.NaN
            Chart1.ChartAreas(AreaName).AxisY2.LineWidth = item.<AxisY2>.<LineWidth>.Value
            Chart1.ChartAreas(AreaName).AxisY2.Interval = item.<AxisY2>.<Interval>.Value
            Chart1.ChartAreas(AreaName).AxisY2.IntervalOffset = item.<AxisY2>.<IntervalOffset>.Value
            Chart1.ChartAreas(AreaName).AxisY2.Crossing = item.<AxisY2>.<Crossing>.Value
            If item.<AxisY2>.<AutoInterval>.Value <> Nothing Then If item.<AxisY2>.<AutoInterval>.Value = True Then Chart1.ChartAreas(AreaName).AxisY2.Interval = Double.NaN
            If item.<AxisY2>.<ScaleViewZoomable>.Value <> Nothing Then Chart1.ChartAreas(AreaName).AxisY2.ScaleView.Zoomable = item.<AxisY2>.<ScaleViewZoomable>.Value

        Next
    End Sub

    Private Sub ApplyChartSeries(ByRef SeriesInfo As IEnumerable(Of XElement))
        'Apply the Chart Series in SeriesInfo to the chart.
        Dim NSeries As Integer = SeriesInfo.Count
        Dim SeriesName As String
        Dim myFontStyle As FontStyle
        Dim myFontSize As Single
        Chart1.Series.Clear()
        For Each item In SeriesInfo
            SeriesName = item.<Name>.Value
            Chart1.Series.Add(SeriesName)
            Chart1.Series(SeriesName).ChartType = [Enum].Parse(GetType(DataVisualization.Charting.SeriesChartType), item.<ChartType>.Value)
            If item.<ChartArea>.Value <> Nothing Then Chart1.Series(SeriesName).ChartArea = item.<ChartArea>.Value
            Chart1.Series(SeriesName).Legend = item.<Legend>.Value

            'Point Chart custom properties
            If item.<EmptyPointValue>.Value <> Nothing Then Chart1.Series(SeriesName).SetCustomProperty("EmptyPointValue", item.<EmptyPointValue>.Value)
            If item.<LabelStyle>.Value <> Nothing Then Chart1.Series(SeriesName).SetCustomProperty("LabelStyle", item.<LabelStyle>.Value)
            If item.<PixelPointDepth>.Value <> Nothing Then Chart1.Series(SeriesName).SetCustomProperty("PixelPointDepth", item.<PixelPointDepth>.Value)
            If item.<PixelPointGapDepth>.Value <> Nothing Then Chart1.Series(SeriesName).SetCustomProperty("PixelPointGapDepth", item.<PixelPointGapDepth>.Value)
            If item.<ShowMarkerLines>.Value <> Nothing Then Chart1.Series(SeriesName).SetCustomProperty("ShowMarkerLines", item.<ShowMarkerLines>.Value)

            Chart1.Series(SeriesName).AxisLabel = item.<AxisLabel>.Value
            Chart1.Series(SeriesName).XAxisType = [Enum].Parse(GetType(DataVisualization.Charting.AxisType), item.<XAxisType>.Value)
            Chart1.Series(SeriesName).YAxisType = [Enum].Parse(GetType(DataVisualization.Charting.AxisType), item.<YAxisType>.Value)
            If item.<XValueType>.Value <> Nothing Then Chart1.Series(SeriesName).XValueType = [Enum].Parse(GetType(DataVisualization.Charting.ChartValueType), item.<XValueType>.Value)
            If item.<YValueType>.Value <> Nothing Then Chart1.Series(SeriesName).YValueType = [Enum].Parse(GetType(DataVisualization.Charting.ChartValueType), item.<YValueType>.Value)
            If item.<Marker>.<BorderColor>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerBorderColor = Color.FromArgb(item.<Marker>.<BorderColor>.Value)
            If item.<Marker>.<BorderWidth>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerBorderWidth = item.<Marker>.<BorderWidth>.Value
            If item.<Marker>.<Color>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerColor = Color.FromArgb(item.<Marker>.<Color>.Value)
            If item.<Marker>.<Size>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerSize = item.<Marker>.<Size>.Value
            If item.<Marker>.<Step>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerStep = item.<Marker>.<Step>.Value
            If item.<Marker>.<Style>.Value <> Nothing Then Chart1.Series(SeriesName).MarkerStyle = [Enum].Parse(GetType(DataVisualization.Charting.MarkerStyle), item.<Marker>.<Style>.Value)
            If item.<Color>.Value <> Nothing Then Chart1.Series(SeriesName).Color = Color.FromArgb(item.<Color>.Value)
            If item.<ToolTip>.Value <> Nothing Then Chart1.Series(SeriesName).ToolTip = item.<ToolTip>.Value

            'Load the data points:
            Try
                Chart1.Series(SeriesName).Points.DataBindXY(DataSource.Data.Tables(0).DefaultView, item.<XFieldName>.Value, DataSource.Data.Tables(0).DefaultView, item.<YFieldName>.Value)
            Catch ex As Exception
                Main.Message.AddWarning(ex.Message & vbCrLf)
            End Try
        Next
    End Sub


    Private Sub cmbChartList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChartList.SelectedIndexChanged
        If cmbChartList.SelectedIndex = -1 Then

        Else
            Dim SelChartName As String = cmbChartList.SelectedItem.ToString
            ChartName = SelChartName
        End If
    End Sub

    Private Sub frmChart_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If IsNothing(ChartSettings) Then

        Else
            ChartSettings.txtHeight.Text = Me.Height
            ChartSettings.txtWidth.Text = Me.Width
            CheckFormPos()
        End If
    End Sub

    Private Sub frmChart_Move(sender As Object, e As EventArgs) Handles Me.Move
        If IsNothing(ChartSettings) Then

        Else
            ChartSettings.txtTop.Text = Me.Top
            ChartSettings.txtLeft.Text = Me.Left
            CheckFormPos()
        End If
    End Sub

#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------


End Class