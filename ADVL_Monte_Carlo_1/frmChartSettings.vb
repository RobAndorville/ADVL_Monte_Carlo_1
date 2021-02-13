Public Class frmChartSettings
    'This form is used to specify the chart settings.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    Dim cboArea As New DataGridViewComboBoxColumn 'Used for selecting the ChartArea for a Series
    Dim cboType As New DataGridViewComboBoxColumn 'Used for selecting the Type of series to plot (Point, Line, Bar or Column)
    Dim cboXField As New DataGridViewComboBoxColumn 'Used for selecting the X coordinate Field to plot
    Dim cboYField As New DataGridViewComboBoxColumn 'Used for selecting the Y coordinate Field to plot

    Public myParent As Form
    Dim myChart As DataVisualization.Charting.Chart

    Dim ChartInfo As Xml.Linq.XDocument 'Stores the chart information.

    Dim TitleInfo As IEnumerable(Of XElement)
    Dim SeriesInfo As IEnumerable(Of XElement)
    Dim AreaInfo As IEnumerable(Of XElement)

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _chartFormNo As Integer = -1 'The FormNo of the parent chart form.
    Public Property ChartFormNo As Integer
        Get
            Return _chartFormNo
        End Get
        Set(ByVal value As Integer)
            _chartFormNo = value
        End Set
    End Property

    Private _dataSource As Object = Nothing 'DataSource links to the oject containing the Data to be analysed. (Input, Processed, Distribution or MonteCarlo objects in the Main form.)
    Property DataSource As Object
        Get
            Return _dataSource
        End Get
        Set(value As Object)
            _dataSource = value
            cmbChartList.Items.Clear()
            For Each item In _dataSource.ChartList
                cmbChartList.Items.Add(item.Key)
            Next
        End Set
    End Property

    Private _tableName As String = "" 'The name of the table containing the data to plot.
    Property TableName As String
        Get
            Return _tableName
        End Get
        Set(value As String)
            _tableName = value
            If DataSource Is Nothing Then
                Main.Message.AddWarning("The data source is empty." & vbCrLf)
            Else
                If DataSource.Data.Tables.Contains(TableName) Then
                    UpdateFieldList()
                Else
                    Main.Message.AddWarning("A table named " & TableName & " was not found." & vbCrLf)
                End If
            End If
        End Set
    End Property

    Private _chartName As String = "" 'The name of the selected chart 
    Property ChartName As String
        Get
            Return _chartName
        End Get
        Set(value As String)
            _chartName = value
            cmbChartList.SelectedIndex = cmbChartList.FindStringExact(ChartName)

            If _dataSource.ChartList.ContainsKey(_chartName) Then
                Dim ChartXml As System.Xml.Linq.XDocument = _dataSource.ChartList(ChartName)
                txtChartDescr.Text = ChartXml.<ChartSettings>.<Description>.Value
                SelectChart(ChartName)
            End If
        End Set
    End Property

    Private _titleNo As Integer = -1 'The selected Title number in the Chart title collection.
    Property TitleNo As Integer
        Get
            Return _titleNo
        End Get
        Set(value As Integer)
            _titleNo = value
            txtTitlesRecordNo.Text = _titleNo + 1
        End Set
    End Property

    Private _seriesNo As Integer = -1  'The selected Series number in the Chart series collection.
    Property SeriesNo As Integer
        Get
            Return _seriesNo
        End Get
        Set(value As Integer)
            _seriesNo = value
        End Set
    End Property

    Private _areaNo As Integer = -1  'The selected Area number in the Chart area collection.
    Property AreaNo As Integer
        Get
            Return _areaNo
        End Get
        Set(value As Integer)
            _areaNo = value
            txtAreaRecordNo.Text = _areaNo + 1
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

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & ChartFormNo & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & "_" & ChartFormNo & ".xml"

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
        RestoreFormSettings()   'Restore the form settings

        myChart = myParent.Controls("Chart1")
        myChart.SuppressExceptions = True

        UpdateFieldList() 'This populates the items in cboField

        DataGridView1.ColumnCount = 9

        DataGridView1.Columns(0).HeaderText = "Series Name"

        cboType.Items.Clear()
        cboType.Items.Add("Point")
        cboType.Items.Add("Line")
        cboType.Items.Add("Bar") 'A Bar chart can only be combined with a Bar or Stacked Bar chart. 'A Bar chart can only be combined with a Bar or Stacked Bar chart.
        cboType.Items.Add("Column")

        DataGridView1.Columns.Insert(1, cboType) 'Insert the combo box column used to select the type of chart to plot.
        DataGridView1.Columns(1).HeaderText = "Chart Type"

        DataGridView1.Columns.Insert(2, cboArea)
        DataGridView1.Columns(2).HeaderText = "Chart Area"

        DataGridView1.Columns.Insert(3, cboXField) 'Insert the combo box column used to select the data field to plot.
        DataGridView1.Columns(3).HeaderText = "X Field Name"

        Dim cboXAxis As New DataGridViewComboBoxColumn 'Used for selecting the X Axis to use (X or X2)
        cboXAxis.Items.Add("Primary")
        cboXAxis.Items.Add("Secondary")
        DataGridView1.Columns.Insert(4, cboXAxis) 'Insert the combo box column used to select the X Axis to use.
        DataGridView1.Columns(4).HeaderText = "X Axis Type"

        Dim cboXAxisValueType As New DataGridViewComboBoxColumn 'Used for selecting the X Axis value type
        For Each valueType In [Enum].GetNames(GetType(DataVisualization.Charting.ChartValueType))
            cboXAxisValueType.Items.Add(valueType)
        Next
        DataGridView1.Columns.Insert(5, cboXAxisValueType) 'Insert the combo box column used to select the X Axis value type
        DataGridView1.Columns(5).HeaderText = "X Value Type"

        DataGridView1.Columns.Insert(6, cboYField) 'Insert the combo box column used to select the data field to plot.
        DataGridView1.Columns(6).HeaderText = "Y Field Name"

        Dim cboYAxis As New DataGridViewComboBoxColumn 'Used for selecting the Y Axis to use (Y or Y2)
        cboYAxis.Items.Add("Primary")
        cboYAxis.Items.Add("Secondary")
        DataGridView1.Columns.Insert(7, cboYAxis) 'Insert the combo box column used to select the X Axis to use.
        DataGridView1.Columns(7).HeaderText = "Y Axis Type"

        Dim cboYAxisValueType As New DataGridViewComboBoxColumn 'Used for selecting the Y Axis value type
        For Each valueType In [Enum].GetNames(GetType(DataVisualization.Charting.ChartValueType))
            cboYAxisValueType.Items.Add(valueType)
        Next
        DataGridView1.Columns.Insert(8, cboYAxisValueType) 'Insert the combo box column used to select the Y Axis value type
        DataGridView1.Columns(8).HeaderText = "Y Value Type"

        Dim cboMarkerFill As New DataGridViewComboBoxColumn 'Used for selecting a transparent marker (No Fill)
        cboMarkerFill.Items.Add("Yes")
        cboMarkerFill.Items.Add("No")
        DataGridView1.Columns.Insert(9, cboMarkerFill) 'Insert the combo box column used to select the Marker Fill
        DataGridView1.Columns(9).HeaderText = "Marker Fill"

        DataGridView1.Columns(10).HeaderText = "Marker Color"

        DataGridView1.Columns(11).HeaderText = "Border Color"

        DataGridView1.Columns(12).HeaderText = "Border Width"

        Dim cboMarkerStyle As New DataGridViewComboBoxColumn 'Used for selecting the Marker Style
        For Each marker In [Enum].GetNames(GetType(DataVisualization.Charting.MarkerStyle))
            cboMarkerStyle.Items.Add(marker)
        Next
        DataGridView1.Columns.Insert(13, cboMarkerStyle)
        DataGridView1.Columns(13).HeaderText = "Marker Style"

        DataGridView1.Columns(14).HeaderText = "Marker Size"

        DataGridView1.Columns(15).HeaderText = "Marker Step"

        DataGridView1.Columns(16).HeaderText = "Line Color"

        DataGridView1.Columns(17).HeaderText = "Line Width"

        DataGridView1.Columns(18).HeaderText = "Tool Tip"

        'Add in initial row:
        Dim Field1 As String '= cboXField.Items(0)
        Dim Field2 As String '= cboYField.Items(1)
        If cboXField.Items.Count > 0 Then
            Field1 = cboXField.Items(0)
        Else
            Field1 = ""
        End If
        If cboYField.Items.Count > 1 Then
            Field2 = cboYField.Items(1)
        Else
            Field2 = ""
        End If

        DataGridView1.Rows.Add("Series1", "Point", "ChartArea1", Field1, "X", "", Field2, "Y", "", "Yes", "", "", "1", "Circle", "5", "1", "", "1", "0")

        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()

        DataGridView1.AllowUserToAddRows = False


        '  Titles Tab
        txtTitlesRecordNo.Text = "0"
        txtNTitlesRecords.Text = "0"

        Dim items As Array
        items = System.Enum.GetNames(GetType(ContentAlignment))
        Dim item As String
        For Each item In items
            cmbAlignment.Items.Add(item)
        Next

        items = System.Enum.GetNames(GetType(DataVisualization.Charting.TextOrientation))
        For Each item In items
            cmbOrientation.Items.Add(item)
        Next

        '  Areas Tab
        txtAreaRecordNo.Text = "0"
        txtNAreaRecords.Text = "0"

        cmbXAxisTitleAlignment.Items.Add("Center")
        cmbXAxisTitleAlignment.Items.Add("Far")
        cmbXAxisTitleAlignment.Items.Add("Near")

        cmbX2AxisTitleAlignment.Items.Add("Center")
        cmbX2AxisTitleAlignment.Items.Add("Far")
        cmbX2AxisTitleAlignment.Items.Add("Near")

        cmbYAxisTitleAlignment.Items.Add("Center")
        cmbYAxisTitleAlignment.Items.Add("Far")
        cmbYAxisTitleAlignment.Items.Add("Near")

        cmbY2AxisTitleAlignment.Items.Add("Center")
        cmbY2AxisTitleAlignment.Items.Add("Far")
        cmbY2AxisTitleAlignment.Items.Add("Near")

        RefreshChartList()

        txtHeight.Text = myParent.Height
        txtWidth.Text = myParent.Width
        txtTop.Text = myParent.Top
        txtLeft.Text = myParent.Left

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form
        Me.Close() 'Close the form
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

    Private Sub UpdateFieldList()
        'Update the file list in DataGridVIew1.
        If Main.ChartList(ChartFormNo) IsNot Nothing Then
            cboXField.Items.Clear()
            cboYField.Items.Clear()
            If DataSource Is Nothing Then

            Else
                If DataSource.Data.Tables.Contains(TableName) Then
                    For Each Col As DataColumn In DataSource.Data.Tables(TableName).Columns
                        cboXField.Items.Add(Col.ColumnName)
                        cboYField.Items.Add(Col.ColumnName)
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub UpdateChartTypeList(ByVal ChartType As String)
        'Only chart types compatible with ChartType will be added.

        Select Case ChartType
            Case "" 'Show all chart types
                cboType.Items.Clear()
                cboType.Items.Add("Point")
                cboType.Items.Add("Line")
                cboType.Items.Add("Bar")
                cboType.Items.Add("Column")

            Case "Point" 'Show chart types compatible with Point charts.
                cboType.Items.Clear()
                cboType.Items.Add("Point")
                cboType.Items.Add("Line")
            Case "Line"  'Show chart types compatible with Line charts.
                cboType.Items.Clear()
                cboType.Items.Add("Point")
                cboType.Items.Add("Line")
            Case "Bar"  'Show chart types compatible with Bar charts.
                cboType.Items.Clear()
                cboType.Items.Add("Bar")
            Case "Column"  'Show chart types compatible with Column charts.
                cboType.Items.Clear()
                cboType.Items.Add("Column")
        End Select

    End Sub

    Private Sub btnResetSeries_Click(sender As Object, e As EventArgs) Handles btnResetSeries.Click
        ResetSeries()
    End Sub

    Private Sub ResetSeries()

        UpdateChartTypeList("")
        DataGridView1.Rows.Clear()

        'Add in initial row:
        Dim Field1 As String = cboXField.Items(0)
        Dim Field2 As String = cboYField.Items(1)
        If cboXField.Items.Count > 0 Then
            Field1 = cboXField.Items(0)
        Else
            Field1 = ""
        End If
        If cboYField.Items.Count > 1 Then
            Field2 = cboYField.Items(1)
        Else
            Field2 = ""
        End If
        DataGridView1.Rows.Add("Point", Field1, "X", Field2, "Y")

        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()

    End Sub

    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError

        If e.Exception.Message = "DataGridViewComboBoxCell value is not valid." Then
            'Ignore the error
        Else
            Main.Message.AddWarning(e.Exception.Message & vbCrLf)
        End If
    End Sub


    Private Sub AddSeries(ByRef myChart As DataVisualization.Charting.Chart, ByVal RowNo As Integer)
        Try
            'Add a series to the chart.
            myChart.Series.Add(DataGridView1.Rows(RowNo).Cells(3).Value)

            'Chart Type:
            Select Case DataGridView1.Rows(RowNo).Cells(0).Value
                Case "Point"
                    myChart.Series(RowNo).ChartType = DataVisualization.Charting.SeriesChartType.Point
                Case "Line"
                    myChart.Series(RowNo).ChartType = DataVisualization.Charting.SeriesChartType.Line
                Case "Bar"
                    myChart.Series(RowNo).ChartType = DataVisualization.Charting.SeriesChartType.Bar
                Case Else
                    Main.Message.AddWarning("Unknown Chart Type in row " & RowNo & " : " & DataGridView1.Rows(RowNo).Cells(0).Value & vbCrLf)
                    Exit Sub
            End Select

            If DataSource Is Nothing Then
                Main.Message.AddWarning("The chart data source has not been specified." & vbCrLf)
            Else
                myChart.Series(RowNo).Points.DataBindXY(DataSource.Data.Tables(TableName).DefaultView, DataGridView1.Rows(RowNo).Cells(2).Value, DataSource.Data.Tables(TableName).DefaultView, DataGridView1.Rows(RowNo).Cells(4).Value)
            End If

            'Select Axes
            If DataGridView1.Rows(RowNo).Cells(3).Value = "X2" Then
                myChart.Series(RowNo).XAxisType = DataVisualization.Charting.AxisType.Secondary
            Else
                myChart.Series(RowNo).XAxisType = DataVisualization.Charting.AxisType.Primary
            End If

            If DataGridView1.Rows(RowNo).Cells(5).Value = "Y2" Then
                myChart.Series(RowNo).YAxisType = DataVisualization.Charting.AxisType.Secondary
            Else
                myChart.Series(RowNo).YAxisType = DataVisualization.Charting.AxisType.Primary
            End If

            'Add other settings:

            If DataGridView1.Rows(RowNo).Cells(6).Value = "No" Then 'No marker fill
                myChart.Series(RowNo).MarkerColor = Color.Transparent
            Else
                myChart.Series(RowNo).MarkerColor = DataGridView1.Rows(RowNo).Cells(7).Style.BackColor
            End If

            myChart.Series(RowNo).MarkerBorderColor = DataGridView1.Rows(RowNo).Cells(8).Style.BackColor
            myChart.Series(RowNo).MarkerBorderWidth = DataGridView1.Rows(RowNo).Cells(9).Value
            myChart.Series(RowNo).MarkerStyle = [Enum].Parse(GetType(DataVisualization.Charting.MarkerStyle), DataGridView1.Rows(RowNo).Cells(10).Value)
            myChart.Series(RowNo).MarkerSize = DataGridView1.Rows(RowNo).Cells(11).Value
            myChart.Series(RowNo).MarkerStep = DataGridView1.Rows(RowNo).Cells(12).Value
            myChart.Series(RowNo).Color = DataGridView1.Rows(RowNo).Cells(13).Style.BackColor
            myChart.Series(RowNo).BorderWidth = DataGridView1.Rows(RowNo).Cells(14).Value

            myChart.ChartAreas(0).AxisX.RoundAxisValues()
            myChart.ChartAreas(0).AxisX2.RoundAxisValues()
            myChart.SuppressExceptions = True
        Catch ex As Exception
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try

    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim CellCol As Integer = e.ColumnIndex

        If CellCol = 10 Then 'Marker color changed.
            Dim CellRow As Integer = e.RowIndex
            ColorDialog1.ShowDialog()
            DataGridView1.Rows(CellRow).Cells(CellCol).Style.BackColor = ColorDialog1.Color
            SeriesInfo(CellRow).<Marker>.<Fill>.Value = DataGridView1.Rows(CellRow).Cells(9).Value
            SeriesInfo(CellRow).<Marker>.<Color>.Value = DataGridView1.Rows(CellRow).Cells(10).Style.BackColor.ToArgb.ToString
            If DataGridView1.Rows(CellRow).Cells(9).Value = "Yes" Then
                myChart.Series(CellRow).MarkerColor = DataGridView1.Rows(CellRow).Cells(10).Style.BackColor
            Else
                myChart.Series(CellRow).MarkerColor = Color.Transparent
            End If

        ElseIf CellCol = 11 Then 'Marker border color changed
            Dim CellRow As Integer = e.RowIndex
            ColorDialog1.ShowDialog()
            DataGridView1.Rows(CellRow).Cells(CellCol).Style.BackColor = ColorDialog1.Color
            SeriesInfo(CellRow).<Marker>.<BorderColor>.Value = DataGridView1.Rows(CellRow).Cells(11).Style.BackColor.ToArgb.ToString
            myChart.Series(CellRow).MarkerBorderColor = DataGridView1.Rows(CellRow).Cells(11).Style.BackColor

        ElseIf CellCol = 16 Then 'Line color changed
            Dim CellRow As Integer = e.RowIndex
            ColorDialog1.ShowDialog()
            DataGridView1.Rows(CellRow).Cells(CellCol).Style.BackColor = ColorDialog1.Color
            SeriesInfo(CellRow).<Color>.Value = DataGridView1.Rows(CellRow).Cells(16).Style.BackColor.ToArgb.ToString
            myChart.Series(CellRow).Color = DataGridView1.Rows(CellRow).Cells(16).Style.BackColor

        End If

    End Sub

    Private Sub btnAddTitle_Click(sender As Object, e As EventArgs) Handles btnAddTitle.Click
        'Add a new Chart Title:

        If ChartName = "" Then
            Main.Message.AddWarning("Please open a chart." & vbCrLf)
            Exit Sub
        End If

        Dim NewTitleNo As Integer = myChart.Titles.Count

        Dim NewTitleName As String = "Title" & NewTitleNo + 1
        myChart.Titles.Add(NewTitleName)

        txtTitleName.Text = NewTitleName
        TitleNo = NewTitleNo
        txtNTitlesRecords.Text = myChart.Titles.Count
        txtChartTitle.Text = "Title"
        Dim myFontStyle As FontStyle
        myFontStyle = FontStyle.Regular
        myFontStyle = myFontStyle Or FontStyle.Bold
        txtChartTitle.Font = New Font("Microsoft Sans Serif", 12, myFontStyle)

        Dim NewTitle As New XElement("Title")
        Dim TitleName As New XElement("Name", NewTitleName)
        NewTitle.Add(TitleName)
        Dim TitleArea As New XElement("ChartArea", "ChartArea1")
        NewTitle.Add(TitleArea)
        Dim TitleText As New XElement("Text", "Title")
        NewTitle.Add(TitleText)
        Dim TitleOrientation As New XElement("TextOrientation", "Auto")
        NewTitle.Add(TitleOrientation)
        Dim TitleAlignment As New XElement("Alignment", "TopCenter")
        NewTitle.Add(TitleAlignment)
        Dim TitleForeColor As New XElement("ForeColor", "-16777216")
        NewTitle.Add(TitleForeColor)
        Dim TitleFont As New XElement("Font")
        Dim FontName As New XElement("Name", "Microsoft Sans Serif")
        TitleFont.Add(FontName)
        Dim FontSize As New XElement("Size", "12")
        TitleFont.Add(FontSize)
        Dim FontBold As New XElement("Bold", "true")
        TitleFont.Add(FontBold)
        Dim FontItalic As New XElement("Italic", "false")
        TitleFont.Add(FontItalic)
        Dim FontStrikeout As New XElement("Strikeout", "false")
        TitleFont.Add(FontStrikeout)
        Dim FontUnderline As New XElement("Underline", "false")
        TitleFont.Add(FontUnderline)
        NewTitle.Add(TitleFont)

        TitleInfo(NewTitleNo - 1).AddAfterSelf(NewTitle)

        myChart.Titles(TitleNo).Font = txtChartTitle.Font
        myChart.Titles(TitleNo).Text = txtChartTitle.Text

    End Sub

    Private Sub btnDelTitle_Click(sender As Object, e As EventArgs) Handles btnDelTitle.Click
        'Delete the title.
        myChart.Titles.RemoveAt(TitleNo)
        TitleInfo(TitleNo).Remove()
        UpdateTitlesTabSettings()
    End Sub

    Private Sub btnNextTitle_Click(sender As Object, e As EventArgs) Handles btnNextTitle.Click
        'Show the next title.
        If TitleNo + 1 = myChart.Titles.Count Then
            Main.Message.Add("Already at the last title." & vbCrLf)
        Else
            TitleNo += 1
            ShowTitle()
        End If
    End Sub

    Private Sub btnPrevTitle_Click(sender As Object, e As EventArgs) Handles btnPrevTitle.Click
        'Show the previous title.
        If TitleNo = 0 Then
            Main.Message.Add("Already at the first title." & vbCrLf)
        Else
            TitleNo -= 1
            ShowTitle()
        End If
    End Sub

    Private Sub ShowTitle()
        'Show the Title information corresponding to TitleNo.
        If TitleNo + 1 > myChart.Titles.Count Then TitleNo = myChart.Titles.Count - 1
        If TitleNo < 0 Then TitleNo = 0

        Dim NTitles As Integer = TitleInfo.Count
        txtNTitlesRecords.Text = NTitles
        Dim TitleName As String

        If NTitles = 0 Then
            TitleNo = -1
            txtTitleName.Text = ""
            txtChartTitle.Text = ""
            cmbAlignment.SelectedIndex = 0
            cmbOrientation.SelectedIndex = 0
        Else
            txtTitleName.Text = TitleInfo(TitleNo).<Name>.Value
            If TitleInfo(TitleNo).<ChartArea>.Value = Nothing Then
                cmbTitleChartArea.SelectedIndex = -1
            Else
                cmbTitleChartArea.SelectedIndex = cmbTitleChartArea.FindStringExact(TitleInfo(TitleNo).<ChartArea>.Value)
            End If
            txtChartTitle.Text = TitleInfo(TitleNo).<Text>.Value
            txtChartTitle.ForeColor = Color.FromArgb(TitleInfo(TitleNo).<ForeColor>.Value)
            Dim myFontStyle As FontStyle
            Dim myFontSize As Single = TitleInfo(TitleNo).<Font>.<Size>.Value
            myFontStyle = FontStyle.Regular
            If TitleInfo(TitleNo).<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If TitleInfo(TitleNo).<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If TitleInfo(TitleNo).<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If TitleInfo(TitleNo).<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            txtChartTitle.Font = New Font(TitleInfo(TitleNo).<Font>.<Name>.Value, myFontSize, myFontStyle)
            cmbAlignment.SelectedIndex = cmbAlignment.FindStringExact(TitleInfo(TitleNo).<Alignment>.Value)
            cmbOrientation.SelectedIndex = cmbOrientation.FindStringExact(TitleInfo(TitleNo).<TextOrientation>.Value)
        End If
    End Sub

    'Titles ========================================================================================================================

    Private Sub cmbTitleChartArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTitleChartArea.SelectedIndexChanged
        'Update the Title ChartArea.
        If cmbTitleChartArea.SelectedIndex = -1 Then
            'No item selected
        Else
            myChart.Titles(TitleNo).DockedToChartArea = cmbTitleChartArea.SelectedItem.ToString
            myChart.Titles(TitleNo).IsDockedInsideChartArea = False
            myChart.Titles(TitleNo).Docking = DataVisualization.Charting.Docking.Top

            TitleInfo(TitleNo).<ChartArea>.Value = cmbTitleChartArea.SelectedItem.ToString
        End If

    End Sub

    Private Sub txtChartTitle_LostFocus(sender As Object, e As EventArgs) Handles txtChartTitle.LostFocus
        'Update the Title text

        myChart.Titles(TitleNo).Text = txtChartTitle.Text    'Update the Chart display
        TitleInfo(TitleNo).<Text>.Value = txtChartTitle.Text 'Update the ChartInfo XML
    End Sub

    Private Sub cmbAlignment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlignment.SelectedIndexChanged
        'Update the Title alignment:

        If cmbAlignment.SelectedItem IsNot Nothing Then
            Try
                myChart.Titles(TitleNo).Alignment = [Enum].Parse(GetType(ContentAlignment), cmbAlignment.SelectedItem.ToString) 'Update the Chart
                TitleInfo(TitleNo).<Alignment>.Value = cmbAlignment.SelectedItem.ToString 'Update the ChartInfo XDocument (AreaInfo() refers to the Areas in ChartInfo)
            Catch ex As Exception
                Main.Message.AddWarning("Chart title alignment: " & ex.Message & vbCrLf)
            End Try
        End If

    End Sub

    Private Sub cmbOrientation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOrientation.SelectedIndexChanged
        'Update the Title orientation:

        If cmbOrientation.SelectedItem IsNot Nothing Then
            Try
                myChart.Titles(TitleNo).TextOrientation = [Enum].Parse(GetType(DataVisualization.Charting.TextOrientation), cmbOrientation.SelectedItem.ToString) 'Update the Chart
                TitleInfo(TitleNo).<TextOrientation>.Value = cmbOrientation.SelectedItem.ToString 'Update the ChartInfo XDocument (AreaInfo() refers to the Areas in ChartInfo)
            Catch ex As Exception
                Main.Message.AddWarning(ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Private Sub btnChartTitleFont_Click(sender As Object, e As EventArgs) Handles btnChartTitleFont.Click
        'Edit chart title font
        FontDialog1.Font = txtChartTitle.Font
        FontDialog1.ShowDialog()
        txtChartTitle.Font = FontDialog1.Font
        myChart.Titles(TitleNo).Font = FontDialog1.Font 'Update the Chart

        'Update the ChartInfo XDocument 
        TitleInfo(TitleNo).<Font>.<Name>.Value = FontDialog1.Font.Name
        TitleInfo(TitleNo).<Font>.<Size>.Value = FontDialog1.Font.Size
        TitleInfo(TitleNo).<Font>.<Bold>.Value = FontDialog1.Font.Bold
        TitleInfo(TitleNo).<Font>.<Italic>.Value = FontDialog1.Font.Italic
        TitleInfo(TitleNo).<Font>.<Strikeout>.Value = FontDialog1.Font.Strikeout
        TitleInfo(TitleNo).<Font>.<Underline>.Value = FontDialog1.Font.Underline
    End Sub

    Private Sub btnChartTitleColor_Click(sender As Object, e As EventArgs) Handles btnChartTitleColor.Click
        ColorDialog1.Color = txtChartTitle.ForeColor
        ColorDialog1.ShowDialog()
        txtChartTitle.ForeColor = ColorDialog1.Color
        Dim TitleNo As Integer = Val(txtTitlesRecordNo.Text) - 1
        myChart.Titles(TitleNo).ForeColor = ColorDialog1.Color 'Update the Chart display
        TitleInfo(TitleNo).<ForeColor>.Value = ColorDialog1.Color.ToArgb.ToString 'Update the ChartInfo XML
    End Sub

    '-------------------------------------------------------------------------------------------------------------------------------

    'Chart Area - X Axis ===========================================================================================================

    Private Sub txtXAxisTitle_LostFocus(sender As Object, e As EventArgs) Handles txtXAxisTitle.LostFocus
        'Update the X Axis title.

        myChart.ChartAreas(AreaNo).AxisX.Title = txtXAxisTitle.Text 'Update the Chart
        AreaInfo(AreaNo).<AxisX>.<Title>.<Text>.Value = txtXAxisTitle.Text 'Update the ChartInfo XDocument (AreaInfo() refers to the Areas in ChartInfo)
    End Sub

    Private Sub btnXAxisTitleFont_Click(sender As Object, e As EventArgs) Handles btnXAxisTitleFont.Click
        'The XAxis title font has changed.
        FontDialog1.Font = txtXAxisTitle.Font
        FontDialog1.ShowDialog()
        txtXAxisTitle.Font = FontDialog1.Font
        'Update the Chart:
        myChart.ChartAreas(AreaNo).AxisX.TitleFont = FontDialog1.Font
        'Update the ChartInfo XDocument 
        AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Name>.Value = FontDialog1.Font.Name
        AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Size>.Value = FontDialog1.Font.Size
        AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Bold>.Value = FontDialog1.Font.Bold
        AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Italic>.Value = FontDialog1.Font.Italic
        AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Strikeout>.Value = FontDialog1.Font.Strikeout
        AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Underline>.Value = FontDialog1.Font.Underline
    End Sub

    Private Sub btnXAxisTitleColor_Click(sender As Object, e As EventArgs) Handles btnXAxisTitleColor.Click
        'The XAxis title color has changed.
        ColorDialog1.Color = txtXAxisTitle.ForeColor
        ColorDialog1.ShowDialog()
        txtXAxisTitle.ForeColor = ColorDialog1.Color
        'Update the Chart:
        myChart.ChartAreas(AreaNo).AxisX.TitleForeColor = ColorDialog1.Color
        AreaInfo(AreaNo).<AxisX>.<Title>.<ForeColor>.Value = ColorDialog1.Color.ToArgb.ToString 'Update the ChartInfo XDocument 
    End Sub

    Private Sub cmbXAxisTitleAlignment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbXAxisTitleAlignment.SelectedIndexChanged
        'The ChartArea XAxis Alignment has changed.
        'Update the Chart:
        If cmbXAxisTitleAlignment.SelectedItem IsNot Nothing Then
            myChart.ChartAreas(AreaNo).AxisX.TitleAlignment = [Enum].Parse(GetType(StringAlignment), cmbXAxisTitleAlignment.SelectedItem.ToString)
            AreaInfo(AreaNo).<AxisX>.<Title>.<Alignment>.Value = cmbXAxisTitleAlignment.SelectedItem.ToString 'Update the ChartInfo XDocument (AreaInfo() refers to the Areas in ChartInfo)
        End If
    End Sub

    Private Sub txtXAxisMin_LostFocus(sender As Object, e As EventArgs) Handles txtXAxisMin.LostFocus
        'The ChartArea XAxis Minimum has changed.
        'Update the Chart:
        If chkXAxisAutoMin.Checked Then
            'Leave the AxisX Minimum unchanged - AutoMinimum is in use.
        Else
            myChart.ChartAreas(AreaNo).AxisX.Minimum = Val(txtXAxisMin.Text)
        End If
        AreaInfo(AreaNo).<AxisX>.<Minimum>.Value = txtXAxisMin.Text 'Update the ChartInfo XDocument
    End Sub

    Private Sub chkXAxisAutoMin_CheckedChanged(sender As Object, e As EventArgs) Handles chkXAxisAutoMin.CheckedChanged
        'The ChartArea XAxis Auto Minimum has changed.
        'Update the Chart:
        If chkXAxisAutoMin.Checked Then myChart.ChartAreas(AreaNo).AxisX.Minimum = Double.NaN 'Auto minimum.
        AreaInfo(AreaNo).<AxisX>.<AutoMinimum>.Value = chkXAxisAutoMin.Checked 'Update the ChartInfo XDocument 
    End Sub

    Private Sub txtXAxisMax_LostFocus(sender As Object, e As EventArgs) Handles txtXAxisMax.LostFocus
        'The ChartArea XAxis Maximum has changed.
        'Update the Chart:
        If chkXAxisAutoMax.Checked Then
            'Leave the AxisX Maximum unchanged - AutoMaximum is in use.
        Else
            myChart.ChartAreas(AreaNo).AxisX.Maximum = Val(txtXAxisMax.Text)
        End If
        AreaInfo(AreaNo).<AxisX>.<Maximum>.Value = txtXAxisMax.Text 'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkXAxisAutoMax_CheckedChanged(sender As Object, e As EventArgs) Handles chkXAxisAutoMax.CheckedChanged
        'The ChartArea XAxis Auto Maximum has changed.
        'Update the Chart:
        If chkXAxisAutoMax.Checked Then myChart.ChartAreas(AreaNo).AxisX.Maximum = Double.NaN 'Auto maximum.
        AreaInfo(AreaNo).<AxisX>.<AutoMaximum>.Value = chkXAxisAutoMax.Checked 'Update the ChartInfo XDocument
    End Sub

    Private Sub txtXAxisAnnotInt_LostFocus(sender As Object, e As EventArgs) Handles txtXAxisAnnotInt.LostFocus
        'The ChartArea XAxis Annotation Interval has changed.
        myChart.ChartAreas(AreaNo).AxisX.Interval = Val(txtXAxisAnnotInt.Text) 'Update the Chart
        AreaInfo(AreaNo).<AxisX>.<Interval>.Value = txtXAxisAnnotInt.Text 'Update the ChartInfo XDocument 
    End Sub

    Private Sub txtXAxisIntervalOffset_LostFocus(sender As Object, e As EventArgs) Handles txtXAxisIntervalOffset.LostFocus
        'The ChartArea XAxis Annotation Interval offset has changed.
        myChart.ChartAreas(AreaNo).AxisX.IntervalOffset = Val(txtXAxisIntervalOffset.Text) 'Update the Chart
        If AreaInfo(AreaNo).<AxisX>.<IntervalOffset>.Value <> Nothing Then AreaInfo(AreaNo).<AxisX>.<IntervalOffset>.Value = Val(txtXAxisIntervalOffset.Text)  'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkXAxisAutoAnnotInt_CheckedChanged(sender As Object, e As EventArgs) Handles chkXAxisAutoAnnotInt.CheckedChanged
        'The ChartArea XAxis Auto Annotation Interval has changed.
        'Update the Chart:
        If chkXAxisAutoAnnotInt.Checked Then myChart.ChartAreas(AreaNo).AxisX.Interval = 0 'Zero indicates Auto mode.
        AreaInfo(AreaNo).<AxisX>.<AutoInterval>.Value = chkXAxisAutoAnnotInt.Checked 'Update the ChartInfo XDocument 
    End Sub

    Private Sub txtXAxisLabelStyleFormat_LostFocus(sender As Object, e As EventArgs) Handles txtXAxisLabelStyleFormat.LostFocus
        'The ChartArea XAxis Auto Annotation Interval has changed.
        myChart.ChartAreas(AreaNo).AxisX.LabelStyle.Format = txtXAxisLabelStyleFormat.Text 'Update the Chart
        AreaInfo(AreaNo).<AxisX>.<LabelStyleFormat>.Value = txtXAxisLabelStyleFormat.Text 'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkXAxisScrollBar_CheckedChanged(sender As Object, e As EventArgs) Handles chkXAxisScrollBar.CheckedChanged
        'The ChartArea XAxis Scrollbar selection has changed.
        'Update the Chart:
        If chkXAxisScrollBar.Checked Then
            myChart.ChartAreas(AreaNo).AxisX.ScrollBar.Enabled = True
            myChart.ChartAreas(AreaNo).AxisX.ScrollBar.Size = 16
        Else
            myChart.ChartAreas(AreaNo).AxisX.ScrollBar.Enabled = False
        End If
        AreaInfo(AreaNo).<AxisX>.<Scrollbar>.Value = chkXAxisScrollBar.Checked  'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkLogXAxis_CheckedChanged(sender As Object, e As EventArgs) Handles chkLogXAxis.CheckedChanged
        'The ChartArea XAxis Log Scale selection has changed.
        'Update the Chart:
        myChart.ChartAreas(AreaNo).AxisX.IsLogarithmic = chkLogXAxis.Checked
        AreaInfo(AreaNo).<AxisX>.<Logarithmic>.Value = chkLogXAxis.Checked 'Update the ChartInfo XDocument 
    End Sub

    '-------------------------------------------------------------------------------------------------------------------------------

    'Chart Area - X2 Axis ===========================================================================================================

    Private Sub txtX2AxisTitle_LostFocus(sender As Object, e As EventArgs) Handles txtX2AxisTitle.LostFocus
        'Update the X2 Axis title.
        myChart.ChartAreas(AreaNo).AxisX2.Title = txtX2AxisTitle.Text 'Update the Chart
        AreaInfo(AreaNo).<AxisX2>.<Title>.<Text>.Value = txtX2AxisTitle.Text 'Update the ChartInfo XDocument (AreaInfo() refers to the Areas in ChartInfo)
    End Sub

    Private Sub btnX2AxisTitleFont_Click(sender As Object, e As EventArgs) Handles btnX2AxisTitleFont.Click
        'The X2Axis title font has changed.
        FontDialog1.Font = txtX2AxisTitle.Font
        FontDialog1.ShowDialog()
        txtX2AxisTitle.Font = FontDialog1.Font

        myChart.ChartAreas(AreaNo).AxisX2.TitleFont = FontDialog1.Font  'Update the Chart
        'Update the ChartInfo XDocument: 
        AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Name>.Value = FontDialog1.Font.Name
        AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Size>.Value = FontDialog1.Font.Size
        AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Bold>.Value = FontDialog1.Font.Bold
        AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Italic>.Value = FontDialog1.Font.Italic
        AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Strikeout>.Value = FontDialog1.Font.Strikeout
        AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Underline>.Value = FontDialog1.Font.Underline
    End Sub

    Private Sub btnX2AxisTitleColor_Click(sender As Object, e As EventArgs) Handles btnX2AxisTitleColor.Click
        'The XAxis title color has changed.
        ColorDialog1.Color = txtX2AxisTitle.ForeColor
        ColorDialog1.ShowDialog()
        txtX2AxisTitle.ForeColor = ColorDialog1.Color

        myChart.ChartAreas(AreaNo).AxisX2.TitleForeColor = ColorDialog1.Color 'Update the Chart
        AreaInfo(AreaNo).<AxisX2>.<Title>.<ForeColor>.Value = ColorDialog1.Color.ToArgb.ToString 'Update the ChartInfo XDocument 
    End Sub

    Private Sub cmbX2AxisTitleAlignment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbX2AxisTitleAlignment.SelectedIndexChanged
        'The ChartArea XAxis Alignment has changed.
        'Update the Chart:
        If cmbX2AxisTitleAlignment.SelectedItem IsNot Nothing Then
            myChart.ChartAreas(AreaNo).AxisX2.TitleAlignment = [Enum].Parse(GetType(StringAlignment), cmbX2AxisTitleAlignment.SelectedItem.ToString)
            AreaInfo(AreaNo).<AxisX2>.<Title>.<Alignment>.Value = cmbX2AxisTitleAlignment.SelectedItem.ToString  'Update the ChartInfo XDocument
        End If
    End Sub

    Private Sub txtX2AxisMin_LostFocus(sender As Object, e As EventArgs) Handles txtX2AxisMin.LostFocus
        'The ChartArea XAxis Minimum has changed.
        'Update the Chart:
        If chkX2AxisAutoMin.Checked Then
            'Leave the AxisX2 Minimum unchanged - AutoMinimum is in use.
        Else
            myChart.ChartAreas(AreaNo).AxisX2.Minimum = Val(txtX2AxisMin.Text)
        End If
        AreaInfo(AreaNo).<AxisX2>.<Minimum>.Value = txtX2AxisMin.Text  'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkX2AxisAutoMin_CheckedChanged(sender As Object, e As EventArgs) Handles chkX2AxisAutoMin.CheckedChanged
        'The ChartArea X2Axis Auto Minimum has changed.
        'Update the Chart:
        If chkX2AxisAutoMin.Checked Then myChart.ChartAreas(AreaNo).AxisX2.Minimum = Double.NaN 'Auto minimum.
        AreaInfo(AreaNo).<AxisX2>.<AutoMinimum>.Value = chkX2AxisAutoMin.Checked 'Update the ChartInfo XDocument 
    End Sub

    Private Sub txtX2AxisMax_LostFocus(sender As Object, e As EventArgs) Handles txtX2AxisMax.LostFocus
        'The ChartArea X2Axis Maximum has changed.
        'Update the Chart:
        If chkX2AxisAutoMax.Checked Then
            'Leave the AxisX Maximum unchanged - AutoMaximum is in use.
        Else
            myChart.ChartAreas(AreaNo).AxisX2.Maximum = Val(txtX2AxisMax.Text)
        End If
        AreaInfo(AreaNo).<AxisX2>.<Maximum>.Value = txtX2AxisMax.Text 'Update the ChartInfo XDocument
    End Sub

    Private Sub chkX2AxisAutoMax_CheckedChanged(sender As Object, e As EventArgs) Handles chkX2AxisAutoMax.CheckedChanged
        'The ChartArea X2Axis Auto Maximum has changed.
        'Update the Chart:
        If chkX2AxisAutoMax.Checked Then myChart.ChartAreas(AreaNo).AxisX2.Maximum = Double.NaN 'Auto maximum.
        AreaInfo(AreaNo).<AxisX2>.<AutoMaximum>.Value = chkX2AxisAutoMax.Checked 'Update the ChartInfo XDocument
    End Sub

    Private Sub txtX2AxisAnnotInt_LostFocus(sender As Object, e As EventArgs) Handles txtX2AxisAnnotInt.LostFocus
        'The ChartArea X2Axis Annotation Interval has changed.
        myChart.ChartAreas(AreaNo).AxisX2.Interval = Val(txtX2AxisAnnotInt.Text) 'Update the Chart
        AreaInfo(AreaNo).<AxisX2>.<Interval>.Value = txtX2AxisAnnotInt.Text 'Update the ChartInfo XDocument
    End Sub

    Private Sub chkX2AxisAutoAnnotInt_CheckedChanged(sender As Object, e As EventArgs) Handles chkX2AxisAutoAnnotInt.CheckedChanged
        'The ChartArea XAxis Auto Annotation Interval has changed.
        'Update the Chart:
        If chkX2AxisAutoAnnotInt.Checked Then myChart.ChartAreas(AreaNo).AxisX2.Interval = 0 'Zero indicates Auto mode.
        AreaInfo(AreaNo).<AxisX2>.<AutoInterval>.Value = chkX2AxisAutoAnnotInt.Checked 'Update the ChartInfo XDocument
    End Sub

    Private Sub txtX2AxisLabelStyleFormat_LostFocus(sender As Object, e As EventArgs) Handles txtX2AxisLabelStyleFormat.LostFocus
        'The ChartArea X2Axis Auto Annotation Interval has changed.
        myChart.ChartAreas(AreaNo).AxisX2.LabelStyle.Format = txtX2AxisLabelStyleFormat.Text 'Update the Chart
        AreaInfo(AreaNo).<AxisX2>.<LabelStyleFormat>.Value = txtX2AxisLabelStyleFormat.Text 'Update the ChartInfo XDocument
    End Sub

    Private Sub chkX2AxisScrollBar_CheckedChanged(sender As Object, e As EventArgs) Handles chkX2AxisScrollBar.CheckedChanged
        'The ChartArea X2Axis Scrollbar selection has changed.
        'Update the Chart:
        If chkX2AxisScrollBar.Checked Then
            myChart.ChartAreas(AreaNo).AxisX2.ScrollBar.Enabled = True
            myChart.ChartAreas(AreaNo).AxisX2.ScrollBar.Size = 16
        Else
            myChart.ChartAreas(AreaNo).AxisX2.ScrollBar.Enabled = False
        End If
        AreaInfo(AreaNo).<AxisX2>.<Scrollbar>.Value = chkX2AxisScrollBar.Checked  'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkLogX2Axis_CheckedChanged(sender As Object, e As EventArgs) Handles chkLogX2Axis.CheckedChanged
        'The ChartArea X2Axis Log Scale selection has changed.
        'Update the Chart:
        myChart.ChartAreas(AreaNo).AxisX2.IsLogarithmic = chkLogX2Axis.Checked
        AreaInfo(AreaNo).<AxisX2>.<Logarithmic>.Value = chkLogX2Axis.Checked 'Update the ChartInfo XDocument 
    End Sub

    '-------------------------------------------------------------------------------------------------------------------------------

    'Chart Area - Y Axis ===========================================================================================================

    Private Sub txtYAxisTitle_LostFocus(sender As Object, e As EventArgs) Handles txtYAxisTitle.LostFocus
        'Update the Y Axis title.
        '(Only ChartArea1 is used in this application.)

        myChart.ChartAreas(AreaNo).AxisY.Title = txtYAxisTitle.Text 'Update the Chart
        AreaInfo(AreaNo).<AxisY>.<Title>.<Text>.Value = txtYAxisTitle.Text 'Update the ChartInfo XDocument 
    End Sub

    Private Sub btnYAxisTitleFont_Click(sender As Object, e As EventArgs) Handles btnYAxisTitleFont.Click
        FontDialog1.Font = txtYAxisTitle.Font
        FontDialog1.ShowDialog()
        txtYAxisTitle.Font = FontDialog1.Font
        myChart.ChartAreas(AreaNo).AxisY.TitleFont = FontDialog1.Font  'Update the Chart
        'Update the ChartInfo XDocument:
        AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Name>.Value = FontDialog1.Font.Name
        AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Size>.Value = FontDialog1.Font.Size
        AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Bold>.Value = FontDialog1.Font.Bold
        AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Italic>.Value = FontDialog1.Font.Italic
        AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Strikeout>.Value = FontDialog1.Font.Strikeout
        AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Underline>.Value = FontDialog1.Font.Underline
    End Sub

    Private Sub btnYAxisTitleColor_Click(sender As Object, e As EventArgs) Handles btnYAxisTitleColor.Click
        'The ChartArea YAxis Title color has been changed.
        ColorDialog1.Color = txtYAxisTitle.ForeColor
        ColorDialog1.ShowDialog()
        txtYAxisTitle.ForeColor = ColorDialog1.Color
        myChart.ChartAreas(AreaNo).AxisY.TitleForeColor = ColorDialog1.Color 'Update the Chart
        AreaInfo(AreaNo).<AxisY>.<Title>.<ForeColor>.Value = ColorDialog1.Color.ToArgb.ToString 'Update the ChartInfo XDocument 
    End Sub

    Private Sub cmbYAxisTitleAlignment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYAxisTitleAlignment.SelectedIndexChanged
        'The ChartArea YAxis Alignment has changed.
        If cmbYAxisTitleAlignment.SelectedItem IsNot Nothing Then
            myChart.ChartAreas(AreaNo).AxisY.TitleAlignment = [Enum].Parse(GetType(StringAlignment), cmbYAxisTitleAlignment.SelectedItem.ToString) 'Update the Chart
            AreaInfo(AreaNo).<AxisY>.<Title>.<Alignment>.Value = cmbYAxisTitleAlignment.SelectedItem.ToString 'Update the ChartInfo XDocument 
        End If
    End Sub

    Private Sub txtYAxisMin_LostFocus(sender As Object, e As EventArgs) Handles txtYAxisMin.LostFocus
        'The ChartArea YAxis Minimum has changed.

        'Update the Chart:
        If chkYAxisAutoMin.Checked Then
            'Leave the AxisY Minimum unchanged - AutoMinimum is in use.
        Else
            myChart.ChartAreas(AreaNo).AxisY.Minimum = Val(txtYAxisMin.Text)
        End If
        AreaInfo(AreaNo).<AxisY>.<Minimum>.Value = txtYAxisMin.Text 'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkYAxisAutoMin_CheckedChanged(sender As Object, e As EventArgs) Handles chkYAxisAutoMin.CheckedChanged
        'Update the Chart:
        If chkYAxisAutoMin.Checked Then myChart.ChartAreas(0).AxisY.Minimum = Double.NaN 'Auto minimum.
        AreaInfo(AreaNo).<AxisY>.<AutoMinimum>.Value = chkYAxisAutoMin.Checked 'Update the ChartInfo XDocument 
    End Sub

    Private Sub txtYAxisMax_LostFocus(sender As Object, e As EventArgs) Handles txtYAxisMax.LostFocus
        'The ChartArea YAxis Maximum has changed.

        'Update the Chart:
        If chkYAxisAutoMax.Checked Then
            'Leave the AxisX Maximum unchanged - AutoMaximum is in use.
        Else
            myChart.ChartAreas(AreaNo).AxisY.Maximum = Val(txtYAxisMax.Text)
        End If
        AreaInfo(AreaNo).<AxisY>.<Maximum>.Value = txtYAxisMax.Text  'Update the ChartInfo XDocument 

    End Sub

    Private Sub chkYAxisAutoMax_CheckedChanged(sender As Object, e As EventArgs) Handles chkYAxisAutoMax.CheckedChanged
        'The ChartArea XAxis Auto Maximum has changed.
        'Update the Chart:
        If chkYAxisAutoMax.Checked Then myChart.ChartAreas(AreaNo).AxisY.Maximum = Double.NaN 'Auto maximum.
        AreaInfo(AreaNo).<AxisY>.<AutoMaximum>.Value = chkYAxisAutoMax.Checked 'Update the ChartInfo XDocument 
    End Sub

    Private Sub txtYAxisAnnotInt_LostFocus(sender As Object, e As EventArgs) Handles txtYAxisAnnotInt.LostFocus
        'The ChartArea YAxis Annotation Interval has changed.
        myChart.ChartAreas(AreaNo).AxisY.Interval = Val(txtYAxisAnnotInt.Text) 'Update the Chart
        AreaInfo(AreaNo).<AxisY>.<Interval>.Value = txtYAxisAnnotInt.Text  'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkYAxisAutoAnnotInt_CheckedChanged(sender As Object, e As EventArgs) Handles chkYAxisAutoAnnotInt.CheckedChanged
        'The ChartArea YAxis Auto Annotation Interval has changed.
        'Update the Chart:
        If chkYAxisAutoAnnotInt.Checked Then myChart.ChartAreas(AreaNo).AxisY.Interval = 0 'Zero indicates Auto mode.
        AreaInfo(AreaNo).<AxisY>.<AutoInterval>.Value = chkYAxisAutoAnnotInt.Checked 'Update the ChartInfo XDocument 
    End Sub


    Private Sub txtYAxisLabelStyleFormat_LostFocus(sender As Object, e As EventArgs) Handles txtYAxisLabelStyleFormat.LostFocus
        'The ChartArea XAxis Auto Annotation Interval has changed.
        myChart.ChartAreas(AreaNo).AxisY.LabelStyle.Format = txtYAxisLabelStyleFormat.Text 'Update the Chart
        AreaInfo(AreaNo).<AxisY>.<LabelStyleFormat>.Value = txtYAxisLabelStyleFormat.Text 'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkYAxisScrollBar_CheckedChanged(sender As Object, e As EventArgs) Handles chkYAxisScrollBar.CheckedChanged
        'The ChartArea YAxis Scrollbar selection has changed.
        'Update the Chart:
        If chkYAxisScrollBar.Checked Then
            myChart.ChartAreas(AreaNo).AxisY.ScrollBar.Enabled = True
            myChart.ChartAreas(AreaNo).AxisY.ScrollBar.Size = 16
        Else
            myChart.ChartAreas(AreaNo).AxisY.ScrollBar.Enabled = False
        End If
        AreaInfo(AreaNo).<AxisY>.<Scrollbar>.Value = chkYAxisScrollBar.Checked 'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkLogYAxis_CheckedChanged(sender As Object, e As EventArgs) Handles chkLogYAxis.CheckedChanged
        'The ChartArea YAxis Log Scale selection has changed.
        'Update the Chart:
        If chkLogYAxis.Checked Then
            myChart.ChartAreas(AreaNo).AxisY.IsLogarithmic = True
        Else
            myChart.ChartAreas(AreaNo).AxisY.IsLogarithmic = False
        End If
        AreaInfo(AreaNo).<AxisY>.<Logarithmic>.Value = chkLogYAxis.Checked 'Update the ChartInfo XDocument 
    End Sub


    'Chart Area - Y2 Axis ===========================================================================================================

    Private Sub txtY2AxisTitle_LostFocus(sender As Object, e As EventArgs) Handles txtY2AxisTitle.LostFocus
        'Update the Y2 Axis title.
        '(Only ChartArea1 is used in this application.)

        myChart.ChartAreas(AreaNo).AxisY2.Title = txtY2AxisTitle.Text 'Update the Chart
        AreaInfo(AreaNo).<AxisY2>.<Title>.<Text>.Value = txtY2AxisTitle.Text 'Update the ChartInfo XDocument 
    End Sub

    Private Sub btnY2AxisTitleFont_Click(sender As Object, e As EventArgs) Handles btnY2AxisTitleFont.Click
        FontDialog1.Font = txtY2AxisTitle.Font
        FontDialog1.ShowDialog()
        txtY2AxisTitle.Font = FontDialog1.Font
        myChart.ChartAreas(AreaNo).AxisY2.TitleFont = FontDialog1.Font  'Update the Chart
        'Update the ChartInfo XDocument:
        AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Name>.Value = FontDialog1.Font.Name
        AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Size>.Value = FontDialog1.Font.Size
        AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Bold>.Value = FontDialog1.Font.Bold
        AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Italic>.Value = FontDialog1.Font.Italic
        AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Strikeout>.Value = FontDialog1.Font.Strikeout
        AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Underline>.Value = FontDialog1.Font.Underline
    End Sub

    Private Sub btnY2AxisTitleColor_Click(sender As Object, e As EventArgs) Handles btnY2AxisTitleColor.Click
        ColorDialog1.Color = txtY2AxisTitle.ForeColor
        ColorDialog1.ShowDialog()
        txtY2AxisTitle.ForeColor = ColorDialog1.Color
        myChart.ChartAreas(AreaNo).AxisY2.TitleForeColor = ColorDialog1.Color 'Update the Chart
        AreaInfo(AreaNo).<AxisY2>.<Title>.<ForeColor>.Value = ColorDialog1.Color.ToArgb.ToString 'Update the ChartInfo XDocument 
    End Sub

    Private Sub cmbY2AxisTitleAlignment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbY2AxisTitleAlignment.SelectedIndexChanged
        'The ChartArea Y2Axis Alignment has changed.
        If cmbY2AxisTitleAlignment.SelectedItem IsNot Nothing Then
            myChart.ChartAreas(AreaNo).AxisY2.TitleAlignment = [Enum].Parse(GetType(StringAlignment), cmbY2AxisTitleAlignment.SelectedItem.ToString) 'Update the Chart
            AreaInfo(AreaNo).<AxisY2>.<Title>.<Alignment>.Value = cmbY2AxisTitleAlignment.SelectedItem.ToString 'Update the ChartInfo XDocument 
        End If

    End Sub

    Private Sub txtY2AxisMin_LostFocus(sender As Object, e As EventArgs) Handles txtY2AxisMin.LostFocus
        'The ChartArea Y2Axis Minimum has changed.

        'Update the Chart:
        If chkY2AxisAutoMin.Checked Then
            'Leave the AxisY Minimum unchanged - AutoMinimum is in use.
        Else
            myChart.ChartAreas(AreaNo).AxisY2.Minimum = Val(txtY2AxisMin.Text)
        End If
        AreaInfo(AreaNo).<AxisY2>.<Minimum>.Value = txtY2AxisMin.Text 'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkY2AxisAutoMin_CheckedChanged(sender As Object, e As EventArgs) Handles chkY2AxisAutoMin.CheckedChanged
        'Update the Chart:
        If chkY2AxisAutoMin.Checked Then myChart.ChartAreas(AreaNo).AxisY2.Minimum = Double.NaN 'Auto minimum.
        AreaInfo(AreaNo).<AxisY2>.<AutoMinimum>.Value = chkY2AxisAutoMin.Checked 'Update the ChartInfo XDocument 
    End Sub

    Private Sub txtY2AxisMax_LostFocus(sender As Object, e As EventArgs) Handles txtY2AxisMax.LostFocus
        'The ChartArea Y2Axis Maximum has changed.

        'Update the Chart:
        If chkY2AxisAutoMax.Checked Then
            'Leave the AxisX Maximum unchanged - AutoMaximum is in use.
        Else
            myChart.ChartAreas(AreaNo).AxisY2.Maximum = Val(txtY2AxisMax.Text)
        End If
        AreaInfo(AreaNo).<AxisY2>.<Maximum>.Value = txtY2AxisMax.Text  'Update the ChartInfo XDocument 

    End Sub

    Private Sub chkY2AxisAutoMax_CheckedChanged(sender As Object, e As EventArgs) Handles chkY2AxisAutoMax.CheckedChanged
        'The ChartArea XAxis Auto Maximum has changed.
        'Update the Chart:
        If chkY2AxisAutoMax.Checked Then myChart.ChartAreas(AreaNo).AxisY2.Maximum = Double.NaN 'Auto maximum.
        AreaInfo(AreaNo).<AxisY2>.<AutoMaximum>.Value = chkY2AxisAutoMax.Checked 'Update the ChartInfo XDocument 
    End Sub

    Private Sub txtY2AxisAnnotInt_LostFocus(sender As Object, e As EventArgs) Handles txtY2AxisAnnotInt.LostFocus
        'The ChartArea Y2Axis Annotation Interval has changed.
        myChart.ChartAreas(AreaNo).AxisY2.Interval = Val(txtY2AxisAnnotInt.Text) 'Update the Chart
        AreaInfo(AreaNo).<AxisY2>.<Interval>.Value = txtY2AxisAnnotInt.Text  'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkY2AxisAutoAnnotInt_CheckedChanged(sender As Object, e As EventArgs) Handles chkY2AxisAutoAnnotInt.CheckedChanged
        'The ChartArea Y2Axis Auto Annotation Interval has changed.
        'Update the Chart:
        If chkY2AxisAutoAnnotInt.Checked Then myChart.ChartAreas(AreaNo).AxisY2.Interval = 0 'Zero indicates Auto mode.
        AreaInfo(AreaNo).<AxisY2>.<AutoInterval>.Value = chkY2AxisAutoAnnotInt.Checked 'Update the ChartInfo XDocument 
    End Sub

    Private Sub txtY2AxisLabelStyleFormat_LostFocus(sender As Object, e As EventArgs) Handles txtY2AxisLabelStyleFormat.LostFocus
        'The ChartArea X2Axis Auto Annotation Interval has changed.
        myChart.ChartAreas(AreaNo).AxisY2.LabelStyle.Format = txtY2AxisLabelStyleFormat.Text 'Update the Chart
        AreaInfo(AreaNo).<AxisY2>.<LabelStyleFormat>.Value = txtY2AxisLabelStyleFormat.Text 'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkY2AxisScrollBar_CheckedChanged(sender As Object, e As EventArgs) Handles chkY2AxisScrollBar.CheckedChanged
        'The ChartArea Y2Axis Scrollbar selection has changed.
        'Update the Chart:
        If chkY2AxisScrollBar.Checked Then
            myChart.ChartAreas(AreaNo).AxisY2.ScrollBar.Enabled = True
            myChart.ChartAreas(AreaNo).AxisY2.ScrollBar.Size = 16
        Else
            myChart.ChartAreas(AreaNo).AxisY2.ScrollBar.Enabled = False
        End If
        AreaInfo(AreaNo).<AxisY2>.<Scrollbar>.Value = chkY2AxisScrollBar.Checked 'Update the ChartInfo XDocument 
    End Sub

    Private Sub chkLogY2Axis_CheckedChanged(sender As Object, e As EventArgs) Handles chkLogY2Axis.CheckedChanged
        'The ChartArea Y2Axis Log Scale selection has changed.
        'Update the Chart:
        If chkLogY2Axis.Checked Then
            myChart.ChartAreas(AreaNo).AxisY2.IsLogarithmic = True
        Else
            myChart.ChartAreas(AreaNo).AxisY2.IsLogarithmic = False
        End If
        AreaInfo(AreaNo).<AxisY2>.<Logarithmic>.Value = chkLogY2Axis.Checked 'Update the ChartInfo XDocument 
    End Sub

    '-------------------------------------------------------------------------------------------------------------------------------

    Private Sub btnAddArea_Click(sender As Object, e As EventArgs) Handles btnAddArea.Click
        'Add a new Chart Area:

        If ChartName = "" Then
            Main.Message.AddWarning("Please open a chart." & vbCrLf)
            Exit Sub
        End If

        Dim NewAreaNo As Integer = myChart.ChartAreas.Count

        Dim NewAreaName As String = "ChartArea" & NewAreaNo + 1
        myChart.ChartAreas.Add(NewAreaName)

        txtAreaName.Text = NewAreaName

        AreaNo = NewAreaNo

        txtNAreaRecords.Text = myChart.ChartAreas.Count

        'myChart.ChartAreas(AreaNo).Position.Height = 50

        'Add default ChartArea properties:
        'AxisX:
        myChart.ChartAreas(AreaNo).AxisX.Title = "X Axis"
        myChart.ChartAreas(AreaNo).AxisX.TitleAlignment = StringAlignment.Center
        myChart.ChartAreas(AreaNo).AxisX.TitleForeColor = Color.Black
        Dim myFontStyle As FontStyle
        myFontStyle = FontStyle.Regular
        myFontStyle = myFontStyle Or FontStyle.Bold
        myChart.ChartAreas(AreaNo).AxisX.TitleFont = New Font("Microsoft Sans Serif", 12, myFontStyle)
        myChart.ChartAreas(AreaNo).AxisX.Minimum = Double.NaN 'Auto Minimum
        myChart.ChartAreas(AreaNo).AxisX.Maximum = Double.NaN 'Auto Maximum
        myChart.ChartAreas(AreaNo).AxisX.LineWidth = 1
        myChart.ChartAreas(AreaNo).AxisX.Interval = 0 'Auto Interval
        myChart.ChartAreas(AreaNo).AxisX.IntervalOffset = Double.NaN 'Auto interval offset
        myChart.ChartAreas(AreaNo).AxisX.Crossing = Double.NaN 'Auto
        myChart.ChartAreas(AreaNo).AxisX.ScrollBar.Enabled = False
        myChart.ChartAreas(AreaNo).AxisX.IsLogarithmic = False
        myChart.ChartAreas(AreaNo).AxisX.RoundAxisValues()
        'AxisX2:
        myChart.ChartAreas(AreaNo).AxisX2.Title = "X2 Axis"
        myChart.ChartAreas(AreaNo).AxisX2.TitleAlignment = StringAlignment.Center
        myChart.ChartAreas(AreaNo).AxisX2.TitleForeColor = Color.Black
        myFontStyle = FontStyle.Regular
        myFontStyle = myFontStyle Or FontStyle.Bold
        myChart.ChartAreas(AreaNo).AxisX2.TitleFont = New Font("Microsoft Sans Serif", 12, myFontStyle)
        myChart.ChartAreas(AreaNo).AxisX2.Minimum = Double.NaN 'Auto Minimum
        myChart.ChartAreas(AreaNo).AxisX2.Maximum = Double.NaN 'Auto Maximum
        myChart.ChartAreas(AreaNo).AxisX2.LineWidth = 1
        myChart.ChartAreas(AreaNo).AxisX2.Interval = 0 'Auto Interval
        myChart.ChartAreas(AreaNo).AxisX2.IntervalOffset = Double.NaN 'Auto interval offset
        myChart.ChartAreas(AreaNo).AxisX2.Crossing = Double.NaN 'Auto
        myChart.ChartAreas(AreaNo).AxisX2.ScrollBar.Enabled = False
        myChart.ChartAreas(AreaNo).AxisX2.IsLogarithmic = False
        myChart.ChartAreas(AreaNo).AxisX2.RoundAxisValues()
        'AxisY:
        myChart.ChartAreas(AreaNo).AxisY.Title = "Y Axis"
        myChart.ChartAreas(AreaNo).AxisY.TitleAlignment = StringAlignment.Center
        myChart.ChartAreas(AreaNo).AxisY.TitleForeColor = Color.Black
        myFontStyle = FontStyle.Regular
        myFontStyle = myFontStyle Or FontStyle.Bold
        myChart.ChartAreas(AreaNo).AxisY.TitleFont = New Font("Microsoft Sans Serif", 12, myFontStyle)
        myChart.ChartAreas(AreaNo).AxisY.Minimum = Double.NaN 'Auto Minimum
        myChart.ChartAreas(AreaNo).AxisY.Maximum = Double.NaN 'Auto Maximum
        myChart.ChartAreas(AreaNo).AxisY.LineWidth = 1
        myChart.ChartAreas(AreaNo).AxisY.Interval = 0 'Auto Interval
        myChart.ChartAreas(AreaNo).AxisY.IntervalOffset = Double.NaN 'Auto interval offset
        myChart.ChartAreas(AreaNo).AxisY.Crossing = Double.NaN 'Auto
        myChart.ChartAreas(AreaNo).AxisY.ScrollBar.Enabled = False
        myChart.ChartAreas(AreaNo).AxisY.IsLogarithmic = False
        myChart.ChartAreas(AreaNo).AxisY.RoundAxisValues()
        'AxisY2:
        myChart.ChartAreas(AreaNo).AxisY2.Title = "Y2 Axis"
        myChart.ChartAreas(AreaNo).AxisY2.TitleAlignment = StringAlignment.Center
        myChart.ChartAreas(AreaNo).AxisY2.TitleForeColor = Color.Black
        myFontStyle = FontStyle.Regular
        myFontStyle = myFontStyle Or FontStyle.Bold
        myChart.ChartAreas(AreaNo).AxisY2.TitleFont = New Font("Microsoft Sans Serif", 12, myFontStyle)
        myChart.ChartAreas(AreaNo).AxisY2.Minimum = Double.NaN 'Auto Minimum
        myChart.ChartAreas(AreaNo).AxisY2.Maximum = Double.NaN 'Auto Maximum
        myChart.ChartAreas(AreaNo).AxisY2.LineWidth = 1
        myChart.ChartAreas(AreaNo).AxisY2.Interval = 0 'Auto Interval
        myChart.ChartAreas(AreaNo).AxisY2.IntervalOffset = Double.NaN 'Auto interval offset
        myChart.ChartAreas(AreaNo).AxisY2.Crossing = Double.NaN 'Auto
        myChart.ChartAreas(AreaNo).AxisY2.ScrollBar.Enabled = False
        myChart.ChartAreas(AreaNo).AxisY2.IsLogarithmic = False
        myChart.ChartAreas(AreaNo).AxisY2.RoundAxisValues()


        'Add the new ChartArea to the ChartInfo XDocument:
        Dim NewArea As New XElement("ChartArea")
        Dim areaName As New XElement("Name", NewAreaName)
        NewArea.Add(areaName)

        'AxisX
        Dim axisX As New XElement("AxisX")

        Dim axisXTitle As New XElement("Title")
        Dim axisXTitleText As New XElement("Text", "X Axis")
        axisXTitle.Add(axisXTitleText)
        Dim axisXTitleAlignment As New XElement("Alignment", "Center")
        axisXTitle.Add(axisXTitleAlignment)
        Dim axisXTitleForeColor As New XElement("ForeColor", "-16777216")
        axisXTitle.Add(axisXTitleForeColor)

        Dim axisXTitleFont As New XElement("Font")
        Dim axisXTitleFontName As New XElement("Name", "Microsoft Sans Serif")
        axisXTitleFont.Add(axisXTitleFontName)
        Dim axisXTitleFontSize As New XElement("Size", "12")
        axisXTitleFont.Add(axisXTitleFontSize)
        Dim axisXTitleFontBold As New XElement("Bold", "true")
        axisXTitleFont.Add(axisXTitleFontBold)
        Dim axisXTitleFontItalic As New XElement("Italic", "false")
        axisXTitleFont.Add(axisXTitleFontItalic)
        Dim axisXTitleFontStrikeout As New XElement("Strikeout", "false")
        axisXTitleFont.Add(axisXTitleFontStrikeout)
        Dim axisXTitleFontUnderline As New XElement("Underline", "false")
        axisXTitleFont.Add(axisXTitleFontUnderline)
        axisXTitle.Add(axisXTitleFont)
        axisX.Add(axisXTitle)

        Dim axisXLabelStyleFormat As New XElement("LabelStyleFormat", "")
        axisX.Add(axisXLabelStyleFormat)
        Dim axisXMinimum As New XElement("Minimum", "-20")
        axisX.Add(axisXMinimum)
        Dim axisXAutoMinimum As New XElement("AutoMinimum", "true")
        axisX.Add(axisXAutoMinimum)
        Dim axisXMaximum As New XElement("Maximum", "20")
        axisX.Add(axisXMaximum)
        Dim axisXAutoMaximum As New XElement("AutoMaximum", "true")
        axisX.Add(axisXAutoMaximum)
        Dim axisXLineWidth As New XElement("LineWidth", "1")
        axisX.Add(axisXLineWidth)
        Dim axisXInterval As New XElement("Interval", "0")
        axisX.Add(axisXInterval)
        Dim axisXAutoInterval As New XElement("AutoInterval", "true")
        axisX.Add(axisXAutoInterval)
        Dim axisXIntervalOffset As New XElement("IntervalOffset", "0")
        axisX.Add(axisXIntervalOffset)
        Dim axisXCrossing As New XElement("Crossing", "NaN")
        axisX.Add(axisXCrossing)
        Dim axisXScrollbar As New XElement("Scrollbar", "false")
        axisX.Add(axisXScrollbar)
        Dim axisXLogarithmic As New XElement("Logarithmic", "false")
        axisX.Add(axisXLogarithmic)
        Dim axisXRoundAxisValues As New XElement("RoundAxisValues", "True")
        axisX.Add(axisXRoundAxisValues)
        NewArea.Add(axisX)

        'AxisX2
        Dim axisX2 As New XElement("AxisX2")

        Dim axisX2Title As New XElement("Title")
        Dim axisX2TitleText As New XElement("Text", "X2 Axis")
        axisX2Title.Add(axisX2TitleText)
        Dim axisX2TitleAlignment As New XElement("Alignment", "Center")
        axisX2Title.Add(axisX2TitleAlignment)
        Dim axisX2TitleForeColor As New XElement("ForeColor", "-16777216")
        axisX2Title.Add(axisX2TitleForeColor)

        Dim axisX2TitleFont As New XElement("Font")
        Dim axisX2TitleFontName As New XElement("Name", "Microsoft Sans Serif")
        axisX2TitleFont.Add(axisX2TitleFontName)
        Dim axisX2TitleFontSize As New XElement("Size", "12")
        axisX2TitleFont.Add(axisX2TitleFontSize)
        Dim axisX2TitleFontBold As New XElement("Bold", "true")
        axisX2TitleFont.Add(axisX2TitleFontBold)
        Dim axisX2TitleFontItalic As New XElement("Italic", "false")
        axisX2TitleFont.Add(axisX2TitleFontItalic)
        Dim axisX2TitleFontStrikeout As New XElement("Strikeout", "false")
        axisX2TitleFont.Add(axisX2TitleFontStrikeout)
        Dim axisX2TitleFontUnderline As New XElement("Underline", "false")
        axisX2TitleFont.Add(axisX2TitleFontUnderline)
        axisX2Title.Add(axisX2TitleFont)
        axisX2.Add(axisX2Title)

        Dim axisX2LabelStyleFormat As New XElement("LabelStyleFormat", "")
        axisX2.Add(axisX2LabelStyleFormat)
        Dim axisX2Minimum As New XElement("Minimum", "-20")
        axisX2.Add(axisX2Minimum)
        Dim axisX2AutoMinimum As New XElement("AutoMinimum", "true")
        axisX2.Add(axisX2AutoMinimum)
        Dim axisX2Maximum As New XElement("Maximum", "20")
        axisX2.Add(axisX2Maximum)
        Dim axisX2AutoMaximum As New XElement("AutoMaximum", "true")
        axisX2.Add(axisX2AutoMaximum)
        Dim axisX2LineWidth As New XElement("LineWidth", "1")
        axisX2.Add(axisX2LineWidth)
        Dim axisX2Interval As New XElement("Interval", "0")
        axisX2.Add(axisX2Interval)
        Dim axisX2AutoInterval As New XElement("AutoInterval", "true")
        axisX.Add(axisX2AutoInterval)
        Dim axisX2IntervalOffset As New XElement("IntervalOffset", "0")
        axisX2.Add(axisX2IntervalOffset)
        Dim axisX2Crossing As New XElement("Crossing", "NaN")
        axisX2.Add(axisX2Crossing)
        Dim axisX2Scrollbar As New XElement("Scrollbar", "false")
        axisX2.Add(axisX2Scrollbar)
        Dim axisX2Logarithmic As New XElement("Logarithmic", "false")
        axisX2.Add(axisX2Logarithmic)
        Dim axisX2RoundAxisValues As New XElement("RoundAxisValues", "True")
        axisX2.Add(axisX2RoundAxisValues)
        NewArea.Add(axisX2)

        'AxisY
        Dim axisY As New XElement("AxisY")

        Dim axisYTitle As New XElement("Title")
        Dim axisYTitleText As New XElement("Text", "Y Axis")
        axisYTitle.Add(axisYTitleText)
        Dim axisYTitleAlignment As New XElement("Alignment", "Center")
        axisYTitle.Add(axisYTitleAlignment)
        Dim axisYTitleForeColor As New XElement("ForeColor", "-16777216")
        axisYTitle.Add(axisYTitleForeColor)

        Dim axisYTitleFont As New XElement("Font")
        Dim axisYTitleFontName As New XElement("Name", "Microsoft Sans Serif")
        axisYTitleFont.Add(axisYTitleFontName)
        Dim axisYTitleFontSize As New XElement("Size", "12")
        axisYTitleFont.Add(axisYTitleFontSize)
        Dim axisYTitleFontBold As New XElement("Bold", "true")
        axisYTitleFont.Add(axisYTitleFontBold)
        Dim axisYTitleFontItalic As New XElement("Italic", "false")
        axisYTitleFont.Add(axisYTitleFontItalic)
        Dim axisYTitleFontStrikeout As New XElement("Strikeout", "false")
        axisYTitleFont.Add(axisYTitleFontStrikeout)
        Dim axisYTitleFontUnderline As New XElement("Underline", "false")
        axisYTitleFont.Add(axisYTitleFontUnderline)
        axisYTitle.Add(axisYTitleFont)
        axisY.Add(axisYTitle)

        Dim axisYLabelStyleFormat As New XElement("LabelStyleFormat", "")
        axisY.Add(axisYLabelStyleFormat)
        Dim axisYMinimum As New XElement("Minimum", "-20")
        axisY.Add(axisYMinimum)
        Dim axisYAutoMinimum As New XElement("AutoMinimum", "true")
        axisY.Add(axisYAutoMinimum)
        Dim axisYMaximum As New XElement("Maximum", "20")
        axisY.Add(axisYMaximum)
        Dim axisYAutoMaximum As New XElement("AutoMaximum", "true")
        axisY.Add(axisYAutoMaximum)
        Dim axisYLineWidth As New XElement("LineWidth", "1")
        axisY.Add(axisYLineWidth)
        Dim axisYInterval As New XElement("Interval", "0")
        axisY.Add(axisYInterval)
        Dim axisYAutoInterval As New XElement("AutoInterval", "true")
        axisY.Add(axisYAutoInterval)
        Dim axisYIntervalOffset As New XElement("IntervalOffset", "0")
        axisY.Add(axisYIntervalOffset)
        Dim axisYCrossing As New XElement("Crossing", "NaN")
        axisY.Add(axisYCrossing)
        Dim axisYScrollbar As New XElement("Scrollbar", "false")
        axisY.Add(axisYScrollbar)
        Dim axisYLogarithmic As New XElement("Logarithmic", "false")
        axisY.Add(axisYLogarithmic)
        Dim axisYRoundAxisValues As New XElement("RoundAxisValues", "True")
        axisY.Add(axisYRoundAxisValues)
        NewArea.Add(axisY)

        'AxisY2
        Dim axisY2 As New XElement("AxisY2")

        Dim axisY2Title As New XElement("Title")
        Dim axisY2TitleText As New XElement("Text", "Y2 Axis")
        axisY2Title.Add(axisY2TitleText)
        Dim axisY2TitleAlignment As New XElement("Alignment", "Center")
        axisY2Title.Add(axisY2TitleAlignment)
        Dim axisY2TitleForeColor As New XElement("ForeColor", "-16777216")
        axisY2Title.Add(axisYTitleForeColor)

        Dim axisY2TitleFont As New XElement("Font")
        Dim axisY2TitleFontName As New XElement("Name", "Microsoft Sans Serif")
        axisY2TitleFont.Add(axisY2TitleFontName)
        Dim axisY2TitleFontSize As New XElement("Size", "12")
        axisY2TitleFont.Add(axisY2TitleFontSize)
        Dim axisY2TitleFontBold As New XElement("Bold", "true")
        axisY2TitleFont.Add(axisY2TitleFontBold)
        Dim axisY2TitleFontItalic As New XElement("Italic", "false")
        axisY2TitleFont.Add(axisY2TitleFontItalic)
        Dim axisY2TitleFontStrikeout As New XElement("Strikeout", "false")
        axisY2TitleFont.Add(axisY2TitleFontStrikeout)
        Dim axisY2TitleFontUnderline As New XElement("Underline", "false")
        axisY2TitleFont.Add(axisY2TitleFontUnderline)
        axisY2Title.Add(axisY2TitleFont)
        axisY2.Add(axisY2Title)

        Dim axisY2LabelStyleFormat As New XElement("LabelStyleFormat", "")
        axisY2.Add(axisY2LabelStyleFormat)
        Dim axisY2Minimum As New XElement("Minimum", "-20")
        axisY2.Add(axisY2Minimum)
        Dim axisY2AutoMinimum As New XElement("AutoMinimum", "true")
        axisY2.Add(axisY2AutoMinimum)
        Dim axisY2Maximum As New XElement("Maximum", "20")
        axisY2.Add(axisY2Maximum)
        Dim axisY2AutoMaximum As New XElement("AutoMaximum", "true")
        axisY2.Add(axisY2AutoMaximum)
        Dim axisY2LineWidth As New XElement("LineWidth", "1")
        axisY2.Add(axisY2LineWidth)
        Dim axisY2Interval As New XElement("Interval", "0")
        axisY2.Add(axisY2Interval)
        Dim axisY2AutoInterval As New XElement("AutoInterval", "true")
        axisY2.Add(axisY2AutoInterval)
        Dim axisY2IntervalOffset As New XElement("IntervalOffset", "0")
        axisY2.Add(axisY2IntervalOffset)
        Dim axisY2Crossing As New XElement("Crossing", "NaN")
        axisY2.Add(axisY2Crossing)
        Dim axisY2Scrollbar As New XElement("Scrollbar", "false")
        axisY2.Add(axisY2Scrollbar)
        Dim axisY2Logarithmic As New XElement("Logarithmic", "false")
        axisY2.Add(axisY2Logarithmic)
        Dim axisY2RoundAxisValues As New XElement("RoundAxisValues", "True")
        axisY2.Add(axisY2RoundAxisValues)
        NewArea.Add(axisY2)

        AreaInfo(NewAreaNo - 1).AddAfterSelf(NewArea)

        'Update cboArea
        cboArea.Items.Add(NewAreaName)
        'Update cmbTitleChartArea
        cmbTitleChartArea.Items.Add(NewAreaName)
    End Sub

    Private Sub btnDeleteArea_Click(sender As Object, e As EventArgs) Handles btnDeleteArea.Click
        'Delete the ChartArea.
        If myChart.ChartAreas.Count > 1 Then
            myChart.ChartAreas.RemoveAt(AreaNo)
            AreaInfo(AreaNo).Remove()
            UpdateAreasTabSettings()
        Else
            Main.Message.AddWarning("The chart must have at least one chart area." & vbCrLf)
        End If
    End Sub

    Private Sub btnPrevArea_Click(sender As Object, e As EventArgs) Handles btnPrevArea.Click
        'Show the previous Chart Area.
        If AreaNo = 0 Then
            Main.Message.Add("Already at the first Chart Area." & vbCrLf)
        Else
            AreaNo -= 1
            ShowArea()
        End If

    End Sub

    Private Sub btnNextArea_Click(sender As Object, e As EventArgs) Handles btnNextArea.Click
        'Show the next tChart Area.
        If AreaNo + 1 = myChart.ChartAreas.Count Then
            Main.Message.Add("Already at the last Chart Area." & vbCrLf)
        Else
            AreaNo += 1
            ShowArea()
        End If

    End Sub

    Private Sub ShowArea()
        'Show the Chart Area information corresponding to AreaNo.
        If AreaNo + 1 > myChart.ChartAreas.Count Then AreaNo = myChart.ChartAreas.Count - 1
        If AreaNo < 0 Then AreaNo = 0

        Dim NAreas As Integer = AreaInfo.Count
        txtNAreaRecords.Text = NAreas
        Dim AreaName As String

        If NAreas = 0 Then
            AreaNo = -1
            txtAreaName.Text = ""

        Else
            txtAreaName.Text = AreaInfo(AreaNo).<Name>.Value

            Dim myFontStyle As FontStyle
            Dim myFontName As String
            Dim myFontSize As Single

            'AxisX:
            txtXAxisTitle.Text = AreaInfo(AreaNo).<AxisX>.<Title>.<Text>.Value
            cmbXAxisTitleAlignment.SelectedIndex = cmbXAxisTitleAlignment.FindStringExact(AreaInfo(AreaNo).<AxisX>.<Title>.<Alignment>.Value)
            txtXAxisTitle.ForeColor = Color.FromArgb(AreaInfo(AreaNo).<AxisX>.<Title>.<ForeColor>.Value)
            myFontName = AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Name>.Value
            myFontStyle = FontStyle.Regular
            If AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = AreaInfo(AreaNo).<AxisX>.<Title>.<Font>.<Size>.Value
            txtXAxisTitle.Font = New Font(myFontName, myFontSize, myFontStyle)
            txtXAxisMin.Text = AreaInfo(AreaNo).<AxisX>.<Minimum>.Value
            chkXAxisAutoMin.Checked = AreaInfo(AreaNo).<AxisX>.<AutoMinimum>.Value
            txtXAxisMax.Text = AreaInfo(AreaNo).<AxisX>.<Maximum>.Value
            chkXAxisAutoMax.Checked = AreaInfo(AreaNo).<AxisX>.<AutoMaximum>.Value
            txtXAxisAnnotInt.Text = AreaInfo(AreaNo).<AxisX>.<Interval>.Value
            chkXAxisAutoAnnotInt.Checked = AreaInfo(AreaNo).<AxisX>.<AutoInterval>.Value
            If AreaInfo(AreaNo).<AxisX>.<IntervalOffset>.Value <> Nothing Then
                txtXAxisIntervalOffset.Text = AreaInfo(AreaNo).<AxisX>.<IntervalOffset>.Value
            Else
                txtXAxisIntervalOffset.Text = ""
            End If
            txtXAxisLabelStyleFormat.Text = AreaInfo(AreaNo).<AxisX>.<LabelStyleFormat>.Value
            chkXAxisScrollBar.Checked = AreaInfo(AreaNo).<AxisX>.<Scrollbar>.Value
            chkLogXAxis.Checked = AreaInfo(AreaNo).<AxisX>.<Logarithmic>.Value
            If AreaInfo(AreaNo).<AxisX>.<RoundAxisValues>.Value <> Nothing Then chkRoundXAxisValues.Checked = AreaInfo(AreaNo).<AxisX>.<RoundAxisValues>.Value

            txtX2AxisTitle.Text = AreaInfo(AreaNo).<AxisX2>.<Title>.<Text>.Value
            cmbX2AxisTitleAlignment.SelectedIndex = cmbX2AxisTitleAlignment.FindStringExact(AreaInfo(AreaNo).<AxisX2>.<Title>.<Alignment>.Value)
            txtX2AxisTitle.ForeColor = Color.FromArgb(AreaInfo(AreaNo).<AxisX2>.<Title>.<ForeColor>.Value)
            myFontName = AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Name>.Value
            myFontStyle = FontStyle.Regular
            If AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = AreaInfo(AreaNo).<AxisX2>.<Title>.<Font>.<Size>.Value
            txtX2AxisTitle.Font = New Font(myFontName, myFontSize, myFontStyle)
            txtX2AxisMin.Text = AreaInfo(AreaNo).<AxisX2>.<Minimum>.Value
            chkX2AxisAutoMin.Checked = AreaInfo(AreaNo).<AxisX2>.<AutoMinimum>.Value
            txtX2AxisMax.Text = AreaInfo(AreaNo).<AxisX2>.<Maximum>.Value
            chkX2AxisAutoMax.Checked = AreaInfo(AreaNo).<AxisX2>.<AutoMaximum>.Value
            txtX2AxisAnnotInt.Text = AreaInfo(AreaNo).<AxisX2>.<Interval>.Value
            If AreaInfo(AreaNo).<AxisX2>.<AutoInterval>.Value <> Nothing Then chkX2AxisAutoAnnotInt.Checked = AreaInfo(AreaNo).<AxisX2>.<AutoInterval>.Value
            txtX2AxisLabelStyleFormat.Text = AreaInfo(AreaNo).<AxisX2>.<LabelStyleFormat>.Value
            chkX2AxisScrollBar.Checked = AreaInfo(AreaNo).<AxisX2>.<Scrollbar>.Value
            chkLogX2Axis.Checked = AreaInfo(AreaNo).<AxisX2>.<Logarithmic>.Value
            If AreaInfo(AreaNo).<AxisX2>.<RoundAxisValues>.Value <> Nothing Then chkRoundX2AxisValues.Checked = AreaInfo(AreaNo).<AxisX2>.<RoundAxisValues>.Value

            txtYAxisTitle.Text = AreaInfo(AreaNo).<AxisY>.<Title>.<Text>.Value
            cmbYAxisTitleAlignment.SelectedIndex = cmbXAxisTitleAlignment.FindStringExact(AreaInfo(AreaNo).<AxisY>.<Title>.<Alignment>.Value)
            txtYAxisTitle.ForeColor = Color.FromArgb(AreaInfo(AreaNo).<AxisY>.<Title>.<ForeColor>.Value)
            myFontName = AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Name>.Value
            myFontStyle = FontStyle.Regular
            If AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = AreaInfo(AreaNo).<AxisY>.<Title>.<Font>.<Size>.Value
            txtYAxisTitle.Font = New Font(myFontName, myFontSize, myFontStyle)
            txtYAxisMin.Text = AreaInfo(AreaNo).<AxisY>.<Minimum>.Value
            chkYAxisAutoMin.Checked = AreaInfo(AreaNo).<AxisY>.<AutoMinimum>.Value
            txtYAxisMax.Text = AreaInfo(AreaNo).<AxisY>.<Maximum>.Value
            chkYAxisAutoMax.Checked = AreaInfo(AreaNo).<AxisY>.<AutoMaximum>.Value
            txtYAxisAnnotInt.Text = AreaInfo(AreaNo).<AxisY>.<Interval>.Value
            If AreaInfo(AreaNo).<AxisY>.<AutoInterval>.Value <> Nothing Then chkYAxisAutoAnnotInt.Checked = AreaInfo(AreaNo).<AxisY>.<AutoInterval>.Value
            txtYAxisLabelStyleFormat.Text = AreaInfo(AreaNo).<AxisY>.<LabelStyleFormat>.Value
            chkYAxisScrollBar.Checked = AreaInfo(AreaNo).<AxisY>.<Scrollbar>.Value
            chkLogYAxis.Checked = AreaInfo(AreaNo).<AxisY>.<Logarithmic>.Value

            txtY2AxisTitle.Text = AreaInfo(AreaNo).<AxisY2>.<Title>.<Text>.Value
            cmbY2AxisTitleAlignment.SelectedIndex = cmbXAxisTitleAlignment.FindStringExact(AreaInfo(AreaNo).<AxisY2>.<Title>.<Alignment>.Value)
            txtY2AxisTitle.ForeColor = Color.FromArgb(AreaInfo(AreaNo).<AxisY2>.<Title>.<ForeColor>.Value)
            myFontName = AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Name>.Value
            myFontStyle = FontStyle.Regular
            If AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = AreaInfo(AreaNo).<AxisY2>.<Title>.<Font>.<Size>.Value
            txtY2AxisTitle.Font = New Font(myFontName, myFontSize, myFontStyle)
            txtY2AxisMin.Text = AreaInfo(AreaNo).<AxisY2>.<Minimum>.Value
            chkY2AxisAutoMin.Checked = AreaInfo(AreaNo).<AxisY2>.<AutoMinimum>.Value
            txtY2AxisMax.Text = AreaInfo(AreaNo).<AxisY2>.<Maximum>.Value
            chkY2AxisAutoMax.Checked = AreaInfo(AreaNo).<AxisY2>.<AutoMaximum>.Value
            txtY2AxisAnnotInt.Text = AreaInfo(AreaNo).<AxisY2>.<Interval>.Value
            If AreaInfo(AreaNo).<AxisY2>.<AutoInterval>.Value <> Nothing Then chkY2AxisAutoAnnotInt.Checked = AreaInfo(AreaNo).<AxisY2>.<AutoInterval>.Value
            txtY2AxisLabelStyleFormat.Text = AreaInfo(AreaNo).<AxisY2>.<LabelStyleFormat>.Value
            chkY2AxisScrollBar.Checked = AreaInfo(AreaNo).<AxisY2>.<Scrollbar>.Value
            chkLogY2Axis.Checked = AreaInfo(AreaNo).<AxisY2>.<Logarithmic>.Value
        End If
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'Save the Chart settings.

        'ChartInfo contains the XML chart definition.

        ChartName = txtSelChartName.Text.Trim

        'Update the Chart form position in ChartInfo - Dont try to update old ChartInfo formats without these elements
        If ChartInfo.<ChartSettings>.<FormHeight>.Value <> Nothing Then ChartInfo.<ChartSettings>.<FormHeight>.Value = myParent.Height
        If ChartInfo.<ChartSettings>.<FormWidth>.Value <> Nothing Then ChartInfo.<ChartSettings>.<FormWidth>.Value = myParent.Width
        If ChartInfo.<ChartSettings>.<FormTop>.Value <> Nothing Then ChartInfo.<ChartSettings>.<FormTop>.Value = myParent.Top
        If ChartInfo.<ChartSettings>.<FormLeft>.Value <> Nothing Then ChartInfo.<ChartSettings>.<FormLeft>.Value = myParent.Left

        If DataSource Is Nothing Then
            Main.Message.AddWarning("Unknown data source: " & vbCrLf)
        Else
            DataSource.ChartName = ChartName
            If DataSource.ChartList.ContainsKey(ChartName) Then
                DataSource.ChartList(ChartName) = ChartInfo
            Else
                DataSource.ChartList.Add(ChartName, ChartInfo)
            End If
        End If

        RefreshChartList()
    End Sub

    Private Sub RefreshChartList()

        If DataSource Is Nothing Then
        Else
            cmbChartList.Items.Clear()
            For Each item In DataSource.ChartList.Keys
                cmbChartList.Items.Add(item.ToString)
            Next
        End If

    End Sub

    Private Sub SaveChart_Old()

        'Dim ChartName As String = txtSelChartName.Text.Trim
        ChartName = txtSelChartName.Text.Trim

        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <!---->
                   <!--Data Table Chart Settings-->
                   <ChartSettings>
                       <Description><%= txtSelChartDescr.Text %></Description>
                       <TitlesCollection>
                           <%= From item In myChart.Titles
                               Select
                               <Title>
                                   <Name><%= item.Name %></Name>
                                   <Text><%= item.Text %></Text>
                                   <TextOrientation><%= item.TextOrientation %></TextOrientation>
                                   <Alignment><%= item.Alignment %></Alignment>
                                   <ForeColor><%= item.ForeColor.ToArgb.ToString %></ForeColor>
                                   <Font>
                                       <Name><%= item.Font.Name %></Name>
                                       <Size><%= item.Font.Size %></Size>
                                       <Bold><%= item.Font.Bold %></Bold>
                                       <Italic><%= item.Font.Italic %></Italic>
                                       <Strikeout><%= item.Font.Strikeout %></Strikeout>
                                       <Underline><%= item.Font.Underline %></Underline>
                                   </Font>
                               </Title> %>
                       </TitlesCollection>
                       <SeriesCollection>
                           <%= From item In myChart.Series
                               Select
                               <Series>
                                   <Name><%= item.Name %></Name>
                                   <ChartType><%= item.ChartType %></ChartType>
                                   <ChartArea><%= item.ChartArea %></ChartArea>
                                   <Legend><%= item.Legend %></Legend>
                                   <AxisLabel><%= item.AxisLabel %></AxisLabel>
                                   <XAxisType><%= item.XAxisType %></XAxisType>
                                   <XValueType><%= item.XValueType %></XValueType>
                                   <YAxisType><%= item.YAxisType %></YAxisType>
                                   <YValueType><%= item.YValueType %></YValueType>
                                   <Marker>
                                       <BorderColor><%= item.MarkerBorderColor %></BorderColor>
                                       <BorderWidth><%= item.MarkerBorderWidth %></BorderWidth>
                                       <Color><%= item.MarkerColor.ToArgb.ToString %></Color>
                                       <Size><%= item.MarkerSize %></Size>
                                       <Step><%= item.MarkerStep %></Step>
                                       <Style><%= item.MarkerStyle %></Style>
                                   </Marker>
                                   <Color><%= item.Color.ToArgb.ToString %></Color>
                                   <Width><%= item.BorderWidth %></Width>
                                   <ToolTip><%= item.ToolTip %></ToolTip>
                               </Series> %>
                       </SeriesCollection>
                       <ChartAreasCollection>
                           <%= From item In myChart.ChartAreas
                               Select
                               <ChartArea>
                                   <Name><%= item.Name %></Name>
                                   <CursorXIsUserEnabled><%= item.CursorX.IsUserEnabled %></CursorXIsUserEnabled>
                                   <CursorYIsUserEnabled><%= item.CursorY.IsUserEnabled %></CursorYIsUserEnabled>
                                   <CursorXInterval><%= item.CursorX.Interval %></CursorXInterval>
                                   <CursorYInterval><%= item.CursorY.Interval %></CursorYInterval>
                                   <CursorXIsUserSelectionEnabled><%= item.CursorX.IsUserSelectionEnabled %></CursorXIsUserSelectionEnabled>
                                   <CursorYIsUserSelectionEnabled><%= item.CursorY.IsUserSelectionEnabled %></CursorYIsUserSelectionEnabled>
                                   <AxisX>
                                       <Title>
                                           <Text><%= item.AxisX.Title %></Text>
                                           <Alignment><%= item.AxisX.TitleAlignment %></Alignment>
                                           <ForeColor><%= item.AxisX.TitleForeColor.ToArgb.ToString %></ForeColor>
                                           <Font>
                                               <Name><%= item.AxisX.TitleFont.Name %></Name>
                                               <Size><%= item.AxisX.TitleFont.Size %></Size>
                                               <Bold><%= item.AxisX.TitleFont.Bold %></Bold>
                                               <Italic><%= item.AxisX.TitleFont.Italic %></Italic>
                                               <Strikeout><%= item.AxisX.TitleFont.Strikeout %></Strikeout>
                                               <Underline><%= item.AxisX.TitleFont.Underline %></Underline>
                                           </Font>
                                       </Title>
                                       <LabelStyleFormat><%= item.AxisX.LabelStyle.Format %></LabelStyleFormat>
                                       <Minimum><%= item.AxisX.Minimum %></Minimum>
                                       <Maximum><%= item.AxisX.Maximum %></Maximum>
                                       <LineWidth><%= item.AxisX.LineWidth %></LineWidth>
                                       <Interval><%= item.AxisX.Interval %></Interval>
                                       <IntervalOffset><%= item.AxisX.IntervalOffset %></IntervalOffset>
                                       <Crossing><%= item.AxisX.Crossing %></Crossing>
                                       <MajorGrid>
                                           <Interval><%= item.AxisX.MajorGrid.Interval %></Interval>
                                           <IntervalOffset><%= item.AxisX.MajorGrid.IntervalOffset %></IntervalOffset>
                                       </MajorGrid>
                                       <ScaleViewZoomable><%= item.AxisX.ScaleView.Zoomable %></ScaleViewZoomable>
                                   </AxisX>
                                   <AxisX2>
                                       <Title>
                                           <Text><%= item.AxisX2.Title %></Text>
                                           <Alignment><%= item.AxisX2.TitleAlignment %></Alignment>
                                           <ForeColor><%= item.AxisX2.TitleForeColor.ToArgb.ToString %></ForeColor>
                                           <Font>
                                               <Name><%= item.AxisX2.TitleFont.Name %></Name>
                                               <Size><%= item.AxisX2.TitleFont.Size %></Size>
                                               <Bold><%= item.AxisX2.TitleFont.Bold %></Bold>
                                               <Italic><%= item.AxisX2.TitleFont.Italic %></Italic>
                                               <Strikeout><%= item.AxisX2.TitleFont.Strikeout %></Strikeout>
                                               <Underline><%= item.AxisX2.TitleFont.Underline %></Underline>
                                           </Font>
                                       </Title>
                                       <LabelStyleFormat><%= item.AxisX2.LabelStyle.Format %></LabelStyleFormat>
                                       <Minimum><%= item.AxisX2.Minimum %></Minimum>
                                       <Maximum><%= item.AxisX2.Maximum %></Maximum>
                                       <LineWidth><%= item.AxisX2.LineWidth %></LineWidth>
                                       <Interval><%= item.AxisX2.Interval %></Interval>
                                       <IntervalOffset><%= item.AxisX2.IntervalOffset %></IntervalOffset>
                                       <Crossing><%= item.AxisX2.Crossing %></Crossing>
                                       <MajorGrid>
                                           <Interval><%= item.AxisX2.MajorGrid.Interval %></Interval>
                                           <IntervalOffset><%= item.AxisX2.MajorGrid.IntervalOffset %></IntervalOffset>
                                       </MajorGrid>
                                       <ScaleViewZoomable><%= item.AxisX2.ScaleView.Zoomable %></ScaleViewZoomable>
                                   </AxisX2>
                                   <AxisY>
                                       <Title>
                                           <Text><%= item.AxisY.Title %></Text>
                                           <Alignment><%= item.AxisY.TitleAlignment %></Alignment>
                                           <ForeColor><%= item.AxisY.TitleForeColor.ToArgb.ToString %></ForeColor>
                                           <Font>
                                               <Name><%= item.AxisY.TitleFont.Name %></Name>
                                               <Size><%= item.AxisY.TitleFont.Size %></Size>
                                               <Bold><%= item.AxisY.TitleFont.Bold %></Bold>
                                               <Italic><%= item.AxisY.TitleFont.Italic %></Italic>
                                               <Strikeout><%= item.AxisY.TitleFont.Strikeout %></Strikeout>
                                               <Underline><%= item.AxisY.TitleFont.Underline %></Underline>
                                           </Font>
                                       </Title>
                                       <LabelStyleFormat><%= item.AxisY.LabelStyle.Format %></LabelStyleFormat>
                                       <Minimum><%= item.AxisY.Minimum %></Minimum>
                                       <Maximum><%= item.AxisY.Maximum %></Maximum>
                                       <LineWidth><%= item.AxisY.LineWidth %></LineWidth>
                                       <Interval><%= item.AxisY.Interval %></Interval>
                                       <IntervalOffset><%= item.AxisY.IntervalOffset %></IntervalOffset>
                                       <Crossing><%= item.AxisY.Crossing %></Crossing>
                                       <MajorGrid>
                                           <Interval><%= item.AxisY.MajorGrid.Interval %></Interval>
                                           <IntervalOffset><%= item.AxisY.MajorGrid.IntervalOffset %></IntervalOffset>
                                       </MajorGrid>
                                       <ScaleViewZoomable><%= item.AxisY.ScaleView.Zoomable %></ScaleViewZoomable>
                                   </AxisY>
                                   <AxisY2>
                                       <Title>
                                           <Text><%= item.AxisY2.Title %></Text>
                                           <Alignment><%= item.AxisY2.TitleAlignment %></Alignment>
                                           <ForeColor><%= item.AxisY2.TitleForeColor.ToArgb.ToString %></ForeColor>
                                           <Font>
                                               <Name><%= item.AxisY2.TitleFont.Name %></Name>
                                               <Size><%= item.AxisY2.TitleFont.Size %></Size>
                                               <Bold><%= item.AxisY2.TitleFont.Bold %></Bold>
                                               <Italic><%= item.AxisY2.TitleFont.Italic %></Italic>
                                               <Strikeout><%= item.AxisY2.TitleFont.Strikeout %></Strikeout>
                                               <Underline><%= item.AxisY2.TitleFont.Underline %></Underline>
                                           </Font>
                                       </Title>
                                       <LabelStyleFormat><%= item.AxisY2.LabelStyle.Format %></LabelStyleFormat>
                                       <Minimum><%= item.AxisY2.Minimum %></Minimum>
                                       <Maximum><%= item.AxisY2.Maximum %></Maximum>
                                       <LineWidth><%= item.AxisY2.LineWidth %></LineWidth>
                                       <Interval><%= item.AxisY2.Interval %></Interval>
                                       <IntervalOffset><%= item.AxisY2.IntervalOffset %></IntervalOffset>
                                       <Crossing><%= item.AxisY2.Crossing %></Crossing>
                                       <MajorGrid>
                                           <Interval><%= item.AxisY2.MajorGrid.Interval %></Interval>
                                           <IntervalOffset><%= item.AxisY2.MajorGrid.IntervalOffset %></IntervalOffset>
                                       </MajorGrid>
                                       <ScaleViewZoomable><%= item.AxisY2.ScaleView.Zoomable %></ScaleViewZoomable>
                                   </AxisY2>
                               </ChartArea> %>
                       </ChartAreasCollection>
                   </ChartSettings>

        If DataSource Is Nothing Then
            Main.Message.AddWarning("Unknown data source" & vbCrLf)
        Else
            DataSource.ChartName = ChartName
            ChartInfo = XDoc
            If DataSource.ChartList.ContainsKey(ChartName) Then
                DataSource.ChartList(ChartName) = XDoc
            Else
                DataSource.ChartList.Add(ChartName, XDoc)
            End If
        End If
    End Sub

    Private Sub btnSelectChart_Click(sender As Object, e As EventArgs) Handles btnSelectChart.Click
        'Select a Chart.

        If cmbChartList.SelectedIndex = -1 Then
            Main.Message.AddWarning("Please select a chart from the list." & vbCrLf)
        Else
            ChartName = cmbChartList.SelectedItem.ToString
            SelectChart(ChartName)
        End If

    End Sub

    Public Sub SelectChart(ByVal ChartName As String)
        'Select the Chart named ChartName.

        If DataSource Is Nothing Then

        Else
            If DataSource.ChartList.ContainsKey(ChartName) Then
                ChartInfo = DataSource.ChartList(ChartName)
                txtSelChartName.Text = ChartName
                txtSelChartDescr.Text = ChartInfo.<ChartSettings>.<Description>.Value
                AreaInfo = From item In ChartInfo.<ChartSettings>.<ChartAreasCollection>.<ChartArea>
                SeriesInfo = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series>
                TitleInfo = From item In ChartInfo.<ChartSettings>.<TitlesCollection>.<Title>
                UpdateAreaOptions()
                UpdateTitlesTabSettings()
                UpdateAreasTabSettings()
                UpdateSeriesTabSettings()
                LoadChartInfo()
            Else
                Main.Message.AddWarning("Chart not found in the list: " & ChartName & vbCrLf)
            End If
        End If
    End Sub

    Public Sub LoadChartInfo()
        'Load the information in ChartInfo into the Chart and set up the Chart design tabs.

        If ChartInfo Is Nothing Then
            Main.Message.AddWarning("There is no chart information to load." & vbCrLf)
            Exit Sub
        End If

        'Restore the Chart Titles:
        Dim TitleInfo = From item In ChartInfo.<ChartSettings>.<TitlesCollection>.<Title>

        Dim TitleName As String
        Dim myFontStyle As FontStyle
        Dim myFontSize As Single
        myChart.Titles.Clear()
        For Each item In TitleInfo
            TitleName = item.<Name>.Value
            myChart.Titles.Add(TitleName).Text = item.<Text>.Value

            myChart.Titles(TitleName).DockedToChartArea = item.<ChartArea>.Value
            myChart.Titles(TitleName).IsDockedInsideChartArea = False
            myChart.Titles(TitleName).Docking = DataVisualization.Charting.Docking.Top

            myChart.Titles(TitleName).Text = item.<Text>.Value
            myChart.Titles(TitleName).TextOrientation = [Enum].Parse(GetType(DataVisualization.Charting.TextOrientation), item.<TextOrientation>.Value)
            Try
                myChart.Titles(TitleName).Alignment = [Enum].Parse(GetType(ContentAlignment), item.<Alignment>.Value)
            Catch ex As Exception
                Main.Message.AddWarning("Loading chart. Chart title alignment: " & ex.Message & vbCrLf)
            End Try

            myChart.Titles(TitleName).ForeColor = Color.FromArgb(item.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<Font>.<Size>.Value
            myChart.Titles(TitleName).Font = New Font(item.<Font>.<Name>.Value, myFontSize, myFontStyle)
        Next

        'Restore Chart Areas:
        Dim Areas = From item In ChartInfo.<ChartSettings>.<ChartAreasCollection>.<ChartArea>
        Dim AreaName As String
        myChart.ChartAreas.Clear()
        For Each item In Areas
            AreaName = item.<Name>.Value
            myChart.ChartAreas.Add(AreaName)
            If item.<CursorXIsUserEnabled>.Value <> Nothing Then myChart.ChartAreas(AreaName).CursorX.IsUserEnabled = item.<CursorXIsUserEnabled>.Value
            If item.<CursorYIsUserEnabled>.Value <> Nothing Then myChart.ChartAreas(AreaName).CursorY.IsUserEnabled = item.<CursorYIsUserEnabled>.Value
            If item.<CursorXInterval>.Value <> Nothing Then myChart.ChartAreas(AreaName).CursorX.Interval = item.<CursorXInterval>.Value
            If item.<CursorYInterval>.Value <> Nothing Then myChart.ChartAreas(AreaName).CursorY.Interval = item.<CursorYInterval>.Value
            If item.<CursorXIsUserSelectionEnabled>.Value <> Nothing Then myChart.ChartAreas(AreaName).CursorX.IsUserSelectionEnabled = item.<CursorXIsUserSelectionEnabled>.Value
            If item.<CursorYIsUserSelectionEnabled>.Value <> Nothing Then myChart.ChartAreas(AreaName).CursorY.IsUserSelectionEnabled = item.<CursorYIsUserSelectionEnabled>.Value

            'AxisX Properties:
            myChart.ChartAreas(AreaName).AxisX.Title = item.<AxisX>.<Title>.<Text>.Value
            myChart.ChartAreas(AreaName).AxisX.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisX>.<Title>.<Alignment>.Value)
            myChart.ChartAreas(AreaName).AxisX.TitleForeColor = Color.FromArgb(item.<AxisX>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisX>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisX>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisX>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisX>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisX>.<Title>.<Font>.<Size>.Value
            myChart.ChartAreas(AreaName).AxisX.TitleFont = New Font(item.<AxisX>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)
            If item.<AxisX>.<LabelStyleFormat>.Value <> Nothing Then myChart.ChartAreas(AreaName).AxisX.LabelStyle.Format = item.<AxisX>.<LabelStyleFormat>.Value

            myChart.ChartAreas(AreaName).AxisX.Minimum = item.<AxisX>.<Minimum>.Value
            If item.<AxisX>.<AutoMinimum>.Value = True Then myChart.ChartAreas(AreaName).AxisX.Minimum = Double.NaN

            myChart.ChartAreas(AreaName).AxisX.Maximum = item.<AxisX>.<Maximum>.Value
            If item.<AxisX>.<AutoMaximum>.Value = True Then myChart.ChartAreas(AreaName).AxisX.Maximum = Double.NaN

            myChart.ChartAreas(AreaName).AxisX.LineWidth = item.<AxisX>.<LineWidth>.Value
            myChart.ChartAreas(AreaName).AxisX.Interval = item.<AxisX>.<Interval>.Value
            myChart.ChartAreas(AreaName).AxisX.IntervalOffset = item.<AxisX>.<IntervalOffset>.Value
            myChart.ChartAreas(AreaName).AxisX.Crossing = item.<AxisX>.<Crossing>.Value

            If item.<AxisX>.<AutoInterval>.Value = True Then myChart.ChartAreas(AreaName).AxisX.Interval = Double.NaN
            If item.<AxisX>.<ScaleViewZoomable>.Value <> Nothing Then myChart.ChartAreas(AreaName).AxisX.ScaleView.Zoomable = item.<AxisX>.<ScaleViewZoomable>.Value
            If item.<AxisX>.<RoundAxisValues>.Value <> Nothing Then
                If item.<AxisX>.<RoundAxisValues>.Value = True Then myChart.ChartAreas(AreaName).AxisX.RoundAxisValues()
            End If

            'AxisX2 Properties:
            myChart.ChartAreas(AreaName).AxisX2.Title = item.<AxisX2>.<Title>.<Text>.Value
            myChart.ChartAreas(AreaName).AxisX2.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisX2>.<Title>.<Alignment>.Value)
            myChart.ChartAreas(AreaName).AxisX2.TitleForeColor = Color.FromArgb(item.<AxisX2>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisX2>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisX2>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisX2>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisX2>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisX2>.<Title>.<Font>.<Size>.Value
            myChart.ChartAreas(AreaName).AxisX2.TitleFont = New Font(item.<AxisX2>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)
            If item.<AxisX2>.<LabelStyleFormat>.Value <> Nothing Then myChart.ChartAreas(AreaName).AxisX2.LabelStyle.Format = item.<AxisX2>.<LabelStyleFormat>.Value

            myChart.ChartAreas(AreaName).AxisX2.Minimum = item.<AxisX2>.<Minimum>.Value
            If item.<AxisX2>.<AutoMinimum>.Value = True Then myChart.ChartAreas(AreaName).AxisX2.Minimum = Double.NaN

            myChart.ChartAreas(AreaName).AxisX2.Maximum = item.<AxisX2>.<Maximum>.Value
            If item.<AxisX2>.<AutoMaximum>.Value = True Then myChart.ChartAreas(AreaName).AxisX2.Maximum = Double.NaN

            myChart.ChartAreas(AreaName).AxisX2.LineWidth = item.<AxisX2>.<LineWidth>.Value
            myChart.ChartAreas(AreaName).AxisX2.Interval = item.<AxisX2>.<Interval>.Value
            myChart.ChartAreas(AreaName).AxisX2.IntervalOffset = item.<AxisX2>.<IntervalOffset>.Value
            myChart.ChartAreas(AreaName).AxisX2.Crossing = item.<AxisX2>.<Crossing>.Value

            If item.<AxisX2>.<AutoInterval>.Value <> Nothing Then If item.<AxisX2>.<AutoInterval>.Value = True Then myChart.ChartAreas(AreaName).AxisX2.Interval = Double.NaN
            If item.<AxisX2>.<ScaleViewZoomable>.Value <> Nothing Then myChart.ChartAreas(AreaName).AxisX2.ScaleView.Zoomable = item.<AxisX2>.<ScaleViewZoomable>.Value

            'AxisY Properties:
            myChart.ChartAreas(AreaName).AxisY.Title = item.<AxisY>.<Title>.<Text>.Value
            myChart.ChartAreas(AreaName).AxisY.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisY>.<Title>.<Alignment>.Value)
            myChart.ChartAreas(AreaName).AxisY.TitleForeColor = Color.FromArgb(item.<AxisY>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisY>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisY>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisY>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisY>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisY>.<Title>.<Font>.<Size>.Value
            myChart.ChartAreas(AreaName).AxisY.TitleFont = New Font(item.<AxisY>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)
            If item.<AxisY>.<LabelStyleFormat>.Value <> Nothing Then myChart.ChartAreas(AreaName).AxisY.LabelStyle.Format = item.<AxisY>.<LabelStyleFormat>.Value

            myChart.ChartAreas(AreaName).AxisY.Minimum = item.<AxisY>.<Minimum>.Value
            If item.<AxisY>.<AutoMinimum>.Value = True Then myChart.ChartAreas(AreaName).AxisY.Minimum = Double.NaN

            myChart.ChartAreas(AreaName).AxisY.Maximum = item.<AxisY>.<Maximum>.Value
            If item.<AxisY>.<AutoMaximum>.Value = True Then myChart.ChartAreas(AreaName).AxisY.Maximum = Double.NaN

            myChart.ChartAreas(AreaName).AxisY.LineWidth = item.<AxisY>.<LineWidth>.Value
            myChart.ChartAreas(AreaName).AxisY.Interval = item.<AxisY>.<Interval>.Value
            myChart.ChartAreas(AreaName).AxisY.IntervalOffset = item.<AxisY>.<IntervalOffset>.Value
            myChart.ChartAreas(AreaName).AxisY.Crossing = item.<AxisY>.<Crossing>.Value

            If item.<AxisY>.<AutoInterval>.Value <> Nothing Then If item.<AxisY>.<AutoInterval>.Value = True Then myChart.ChartAreas(AreaName).AxisY.Interval = Double.NaN
            If item.<AxisY>.<ScaleViewZoomable>.Value <> Nothing Then myChart.ChartAreas(AreaName).AxisY.ScaleView.Zoomable = item.<AxisY>.<ScaleViewZoomable>.Value

            'AxisY2 Properties:
            myChart.ChartAreas(AreaName).AxisY2.Title = item.<AxisY2>.<Title>.<Text>.Value
            myChart.ChartAreas(AreaName).AxisY2.TitleAlignment = [Enum].Parse(GetType(StringAlignment), item.<AxisY2>.<Title>.<Alignment>.Value)
            myChart.ChartAreas(AreaName).AxisY2.TitleForeColor = Color.FromArgb(item.<AxisY2>.<Title>.<ForeColor>.Value)
            myFontStyle = FontStyle.Regular
            If item.<AxisY2>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If item.<AxisY2>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If item.<AxisY2>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If item.<AxisY2>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = item.<AxisY2>.<Title>.<Font>.<Size>.Value
            myChart.ChartAreas(AreaName).AxisY2.TitleFont = New Font(item.<AxisY2>.<Title>.<Font>.<Name>.Value, myFontSize, myFontStyle)
            If item.<AxisY2>.<LabelStyleFormat>.Value <> Nothing Then myChart.ChartAreas(AreaName).AxisY2.LabelStyle.Format = item.<AxisY2>.<LabelStyleFormat>.Value

            myChart.ChartAreas(AreaName).AxisY2.Minimum = item.<AxisY2>.<Minimum>.Value
            If item.<AxisY2>.<AutoMinimum>.Value = True Then myChart.ChartAreas(AreaName).AxisY2.Minimum = Double.NaN

            myChart.ChartAreas(AreaName).AxisY2.Maximum = item.<AxisY2>.<Maximum>.Value
            If item.<AxisY2>.<AutoMaximum>.Value = True Then myChart.ChartAreas(AreaName).AxisY2.Maximum = Double.NaN

            myChart.ChartAreas(AreaName).AxisY2.LineWidth = item.<AxisY2>.<LineWidth>.Value
            myChart.ChartAreas(AreaName).AxisY2.Interval = item.<AxisY2>.<Interval>.Value
            myChart.ChartAreas(AreaName).AxisY2.IntervalOffset = item.<AxisY2>.<IntervalOffset>.Value
            myChart.ChartAreas(AreaName).AxisY2.Crossing = item.<AxisY2>.<Crossing>.Value

            If item.<AxisY2>.<AutoInterval>.Value <> Nothing Then If item.<AxisY2>.<AutoInterval>.Value = True Then myChart.ChartAreas(AreaName).AxisY2.Interval = Double.NaN
            If item.<AxisY2>.<ScaleViewZoomable>.Value <> Nothing Then myChart.ChartAreas(AreaName).AxisY2.ScaleView.Zoomable = item.<AxisY2>.<ScaleViewZoomable>.Value
        Next

        'Restore Chart Series:
        Dim Series = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series>
        Dim SeriesName As String
        myChart.Series.Clear()
        For Each item In Series
            SeriesName = item.<Name>.Value
            myChart.Series.Add(SeriesName)
            myChart.Series(SeriesName).ChartType = [Enum].Parse(GetType(DataVisualization.Charting.SeriesChartType), item.<ChartType>.Value)
            If item.<ChartArea>.Value <> Nothing Then myChart.Series(SeriesName).ChartArea = item.<ChartArea>.Value
            myChart.Series(SeriesName).Legend = item.<Legend>.Value

            'Point Chart custom properties
            If item.<EmptyPointValue>.Value <> Nothing Then myChart.Series(SeriesName).SetCustomProperty("EmptyPointValue", item.<EmptyPointValue>.Value)
            If item.<LabelStyle>.Value <> Nothing Then myChart.Series(SeriesName).SetCustomProperty("LabelStyle", item.<LabelStyle>.Value)
            If item.<PixelPointDepth>.Value <> Nothing Then myChart.Series(SeriesName).SetCustomProperty("PixelPointDepth", item.<PixelPointDepth>.Value)
            If item.<PixelPointGapDepth>.Value <> Nothing Then myChart.Series(SeriesName).SetCustomProperty("PixelPointGapDepth", item.<PixelPointGapDepth>.Value)
            If item.<ShowMarkerLines>.Value <> Nothing Then myChart.Series(SeriesName).SetCustomProperty("ShowMarkerLines", item.<ShowMarkerLines>.Value)

            myChart.Series(SeriesName).AxisLabel = item.<AxisLabel>.Value
            myChart.Series(SeriesName).XAxisType = [Enum].Parse(GetType(DataVisualization.Charting.AxisType), item.<XAxisType>.Value)
            myChart.Series(SeriesName).YAxisType = [Enum].Parse(GetType(DataVisualization.Charting.AxisType), item.<YAxisType>.Value)
            'If item.<XValueType>.Value <> Nothing Then myChart.Series(SeriesName).XValueType = [Enum].Parse(GetType(DataVisualization.Charting.ChartValueType), item.<XValueType>.Value)

            If item.<XValueType>.Value = Nothing Then
                item.Add(New XElement("XValueType", "Auto"))
                myChart.Series(SeriesName).XValueType = DataVisualization.Charting.ChartValueType.Auto
            Else
                myChart.Series(SeriesName).XValueType = [Enum].Parse(GetType(DataVisualization.Charting.ChartValueType), item.<XValueType>.Value)
            End If

            If item.<YValueType>.Value = Nothing Then
                item.Add(New XElement("YValueType", "Auto"))
                myChart.Series(SeriesName).YValueType = DataVisualization.Charting.ChartValueType.Auto
            Else
                myChart.Series(SeriesName).YValueType = [Enum].Parse(GetType(DataVisualization.Charting.ChartValueType), item.<YValueType>.Value)
            End If

            If item.<Marker>.<BorderColor>.Value <> Nothing Then myChart.Series(SeriesName).MarkerBorderColor = Color.FromArgb(item.<Marker>.<BorderColor>.Value)
            If item.<Marker>.<BorderWidth>.Value <> Nothing Then myChart.Series(SeriesName).MarkerBorderWidth = item.<Marker>.<BorderWidth>.Value
            If item.<Marker>.<Color>.Value <> Nothing Then myChart.Series(SeriesName).MarkerColor = Color.FromArgb(item.<Marker>.<Color>.Value)
            If item.<Marker>.<Size>.Value <> Nothing Then myChart.Series(SeriesName).MarkerSize = item.<Marker>.<Size>.Value
            If item.<Marker>.<Step>.Value <> Nothing Then myChart.Series(SeriesName).MarkerStep = item.<Marker>.<Step>.Value
            If item.<Marker>.<Style>.Value <> Nothing Then myChart.Series(SeriesName).MarkerStyle = [Enum].Parse(GetType(DataVisualization.Charting.MarkerStyle), item.<Marker>.<Style>.Value)
            If item.<Color>.Value <> Nothing Then myChart.Series(SeriesName).Color = Color.FromArgb(item.<Color>.Value)
            If item.<ToolTip>.Value <> Nothing Then myChart.Series(SeriesName).ToolTip = item.<ToolTip>.Value

            Try
                'Load the data points:

                If DataSource Is Nothing Then

                Else
                    myChart.Series(SeriesName).Points.DataBindXY(DataSource.Data.Tables(TableName).DefaultView, item.<XFieldName>.Value, DataSource.Data.Tables(TableName).DefaultView, item.<YFieldName>.Value)
                End If


            Catch ex As Exception
                Main.Message.AddWarning("Error loading Chart information:" & vbCrLf & ex.Message & vbCrLf)
            End Try

        Next

    End Sub

    Private Sub UpdateTitlesTabSettings()
        'Update the Titles tab settings from ChartInfo.

        Dim NTitles As Integer = TitleInfo.Count
        txtNTitlesRecords.Text = NTitles
        Dim TitleName As String

        If NTitles = 0 Then
            TitleNo = -1
            txtTitleName.Text = ""
            txtChartTitle.Text = ""
            cmbAlignment.SelectedIndex = -1
            cmbOrientation.SelectedIndex = -1
        Else
            TitleNo = 0
            If TitleInfo(0).<ChartArea>.Value <> Nothing Then cmbTitleChartArea.SelectedIndex = cmbTitleChartArea.FindStringExact(TitleInfo(0).<ChartArea>.Value)
            txtTitleName.Text = TitleInfo(0).<Name>.Value
            txtChartTitle.Text = TitleInfo(0).<Text>.Value
            txtChartTitle.ForeColor = Color.FromArgb(TitleInfo(0).<ForeColor>.Value)
            Dim myFontStyle As FontStyle
            Dim myFontSize As Single = TitleInfo(0).<Font>.<Size>.Value
            myFontStyle = FontStyle.Regular
            If TitleInfo(0).<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If TitleInfo(0).<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If TitleInfo(0).<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If TitleInfo(0).<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            txtChartTitle.Font = New Font(TitleInfo(0).<Font>.<Name>.Value, myFontSize, myFontStyle)

            cmbAlignment.SelectedIndex = cmbAlignment.FindStringExact(TitleInfo(0).<Alignment>.Value)
            cmbOrientation.SelectedIndex = cmbOrientation.FindStringExact(TitleInfo(0).<TextOrientation>.Value)
        End If
    End Sub
    Private Sub UpdateSeriesTabSettings()
        'Update the Series tab settings from ChartInfo.

        Dim NSeries As Integer = SeriesInfo.Count
        Dim SeriesName As String

        DataGridView1.Rows.Clear()
        DataGridView1.Rows.Add(NSeries)

        If NSeries = 0 Then

        Else
            Dim RowNo As Integer = 0
            For Each item In SeriesInfo
                DataGridView1.Rows(RowNo).Cells(0).Value = item.<Name>.Value
                DataGridView1.Rows(RowNo).Cells(1).Value = item.<ChartType>.Value
                DataGridView1.Rows(RowNo).Cells(2).Value = item.<ChartArea>.Value
                DataGridView1.Rows(RowNo).Cells(3).Value = item.<XFieldName>.Value
                DataGridView1.Rows(RowNo).Cells(4).Value = item.<XAxisType>.Value

                If item.<XValueType>.Value = Nothing Then
                    item.Add(New XElement("XValueType", "Auto"))
                    DataGridView1.Rows(RowNo).Cells(5).Value = "Auto"
                Else
                    DataGridView1.Rows(RowNo).Cells(5).Value = item.<XValueType>.Value
                End If

                DataGridView1.Rows(RowNo).Cells(6).Value = item.<YFieldName>.Value
                DataGridView1.Rows(RowNo).Cells(7).Value = item.<YAxisType>.Value

                If item.<YValueType>.Value = Nothing Then
                    item.Add(New XElement("YValueType", "Auto"))
                    DataGridView1.Rows(RowNo).Cells(8).Value = "Auto"
                Else
                    DataGridView1.Rows(RowNo).Cells(8).Value = item.<YValueType>.Value
                End If

                DataGridView1.Rows(RowNo).Cells(9).Value = item.<Marker>.<Fill>.Value
                DataGridView1.Rows(RowNo).Cells(10).Style.BackColor = Color.FromArgb(item.<Marker>.<Color>.Value)
                DataGridView1.Rows(RowNo).Cells(11).Style.BackColor = Color.FromArgb(item.<Marker>.<BorderColor>.Value)
                DataGridView1.Rows(RowNo).Cells(12).Value = item.<Marker>.<BorderWidth>.Value
                DataGridView1.Rows(RowNo).Cells(13).Value = item.<Marker>.<Style>.Value
                DataGridView1.Rows(RowNo).Cells(14).Value = item.<Marker>.<Size>.Value
                DataGridView1.Rows(RowNo).Cells(15).Value = item.<Marker>.<Step>.Value
                DataGridView1.Rows(RowNo).Cells(16).Style.BackColor = Color.FromArgb(item.<Color>.Value)
                DataGridView1.Rows(RowNo).Cells(17).Value = item.<Width>.Value
                DataGridView1.Rows(RowNo).Cells(18).Value = item.<ToolTip>.Value
                RowNo += 1
            Next
        End If
    End Sub

    Private Sub UpdateAreaOptions()
        'Update the Chart Area selection options.
        cboArea.Items.Clear()
        cmbTitleChartArea.Items.Clear()
        For Each item In AreaInfo
            cboArea.Items.Add(item.<Name>.Value)
            cmbTitleChartArea.Items.Add(item.<Name>.Value)
        Next
    End Sub

    Private Sub UpdateAreasTabSettings()
        'Update the Areas tab settings from ChartInfo.

        Dim NAreas As Integer = AreaInfo.Count
        txtNAreaRecords.Text = NAreas
        Dim AreaName As String

        If NAreas = 0 Then
            AreaNo = -1
            txtAreaName.Text = ""

            txtXAxisTitle.Text = ""
            txtXAxisMin.Text = ""
            chkXAxisAutoMin.Checked = True
            txtXAxisMax.Text = ""
            chkXAxisAutoMax.Checked = True
            txtXAxisAnnotInt.Text = ""
            chkXAxisAutoAnnotInt.Checked = True
            txtXAxisLabelStyleFormat.Text = ""
            chkXAxisScrollBar.Checked = True
            chkLogXAxis.Text = False

            txtX2AxisTitle.Text = ""
            txtX2AxisMin.Text = ""
            chkX2AxisAutoMin.Checked = True
            txtX2AxisMax.Text = ""
            chkX2AxisAutoMax.Checked = True
            txtX2AxisAnnotInt.Text = ""
            chkX2AxisAutoAnnotInt.Checked = True
            txtX2AxisLabelStyleFormat.Text = ""
            chkX2AxisScrollBar.Checked = True
            chkLogX2Axis.Text = False

            txtYAxisTitle.Text = ""
            txtYAxisMin.Text = ""
            chkYAxisAutoMin.Checked = True
            txtYAxisMax.Text = ""
            chkYAxisAutoMax.Checked = True
            txtYAxisAnnotInt.Text = ""
            chkYAxisAutoAnnotInt.Checked = True
            txtYAxisLabelStyleFormat.Text = ""
            chkYAxisScrollBar.Checked = True
            chkLogYAxis.Text = False

            txtY2AxisTitle.Text = ""
            txtY2AxisMin.Text = ""
            chkY2AxisAutoMin.Checked = True
            txtY2AxisMax.Text = ""
            chkY2AxisAutoMax.Checked = True
            txtY2AxisAnnotInt.Text = ""
            chkY2AxisAutoAnnotInt.Checked = True
            txtY2AxisLabelStyleFormat.Text = ""
            chkY2AxisScrollBar.Checked = True
            chkLogY2Axis.Text = False
        Else
            Dim myFontStyle As FontStyle
            Dim myFontName As String
            Dim myFontSize As Single

            AreaNo = 0
            txtAreaName.Text = AreaInfo(0).<Name>.Value

            txtXAxisTitle.Text = AreaInfo(0).<AxisX>.<Title>.<Text>.Value
            cmbXAxisTitleAlignment.SelectedIndex = cmbXAxisTitleAlignment.FindStringExact(AreaInfo(0).<AxisX>.<Title>.<Alignment>.Value)
            txtXAxisTitle.ForeColor = Color.FromArgb(AreaInfo(0).<AxisX>.<Title>.<ForeColor>.Value)
            myFontName = AreaInfo(0).<AxisX>.<Title>.<Font>.<Name>.Value
            myFontStyle = FontStyle.Regular
            If AreaInfo(0).<AxisX>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If AreaInfo(0).<AxisX>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If AreaInfo(0).<AxisX>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If AreaInfo(0).<AxisX>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = AreaInfo(0).<AxisX>.<Title>.<Font>.<Size>.Value
            txtXAxisTitle.Font = New Font(myFontName, myFontSize, myFontStyle)
            txtXAxisMin.Text = AreaInfo(0).<AxisX>.<Minimum>.Value
            chkXAxisAutoMin.Checked = AreaInfo(0).<AxisX>.<AutoMinimum>.Value
            txtXAxisMax.Text = AreaInfo(0).<AxisX>.<Maximum>.Value
            chkXAxisAutoMax.Checked = AreaInfo(0).<AxisX>.<AutoMaximum>.Value
            txtXAxisAnnotInt.Text = AreaInfo(0).<AxisX>.<Interval>.Value
            chkXAxisAutoAnnotInt.Checked = AreaInfo(0).<AxisX>.<AutoInterval>.Value
            txtXAxisLabelStyleFormat.Text = AreaInfo(0).<AxisX>.<LabelStyleFormat>.Value
            chkXAxisScrollBar.Checked = AreaInfo(0).<AxisX>.<Scrollbar>.Value
            chkLogXAxis.Checked = AreaInfo(0).<AxisX>.<Logarithmic>.Value
            If AreaInfo(0).<AxisX>.<RoundAxisValues>.Value <> Nothing Then chkRoundXAxisValues.Checked = AreaInfo(0).<AxisX>.<RoundAxisValues>.Value

            txtX2AxisTitle.Text = AreaInfo(0).<AxisX2>.<Title>.<Text>.Value
            cmbX2AxisTitleAlignment.SelectedIndex = cmbX2AxisTitleAlignment.FindStringExact(AreaInfo(0).<AxisX2>.<Title>.<Alignment>.Value)
            txtX2AxisTitle.ForeColor = Color.FromArgb(AreaInfo(0).<AxisX2>.<Title>.<ForeColor>.Value)
            myFontName = AreaInfo(0).<AxisX2>.<Title>.<Font>.<Name>.Value
            myFontStyle = FontStyle.Regular
            If AreaInfo(0).<AxisX2>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If AreaInfo(0).<AxisX2>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If AreaInfo(0).<AxisX2>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If AreaInfo(0).<AxisX2>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = AreaInfo(0).<AxisX2>.<Title>.<Font>.<Size>.Value
            txtX2AxisTitle.Font = New Font(myFontName, myFontSize, myFontStyle)
            txtX2AxisMin.Text = AreaInfo(0).<AxisX2>.<Minimum>.Value
            chkX2AxisAutoMin.Checked = AreaInfo(0).<AxisX2>.<AutoMinimum>.Value
            txtX2AxisMax.Text = AreaInfo(0).<AxisX2>.<Maximum>.Value
            chkX2AxisAutoMax.Checked = AreaInfo(0).<AxisX2>.<AutoMaximum>.Value
            txtX2AxisAnnotInt.Text = AreaInfo(0).<AxisX2>.<Interval>.Value
            chkX2AxisAutoAnnotInt.Checked = AreaInfo(0).<AxisX2>.<AutoInterval>.Value
            txtX2AxisLabelStyleFormat.Text = AreaInfo(0).<AxisX2>.<LabelStyleFormat>.Value
            chkX2AxisScrollBar.Checked = AreaInfo(0).<AxisX2>.<Scrollbar>.Value
            chkLogX2Axis.Checked = AreaInfo(0).<AxisX2>.<Logarithmic>.Value
            If AreaInfo(0).<AxisX2>.<RoundAxisValues>.Value <> Nothing Then chkRoundX2AxisValues.Checked = AreaInfo(0).<AxisX2>.<RoundAxisValues>.Value

            txtYAxisTitle.Text = AreaInfo(0).<AxisY>.<Title>.<Text>.Value
            cmbYAxisTitleAlignment.SelectedIndex = cmbXAxisTitleAlignment.FindStringExact(AreaInfo(0).<AxisY>.<Title>.<Alignment>.Value)
            txtYAxisTitle.ForeColor = Color.FromArgb(AreaInfo(0).<AxisY>.<Title>.<ForeColor>.Value)
            myFontName = AreaInfo(0).<AxisY>.<Title>.<Font>.<Name>.Value
            myFontStyle = FontStyle.Regular
            If AreaInfo(0).<AxisY>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If AreaInfo(0).<AxisY>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If AreaInfo(0).<AxisY>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If AreaInfo(0).<AxisY>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = AreaInfo(0).<AxisY>.<Title>.<Font>.<Size>.Value
            txtYAxisTitle.Font = New Font(myFontName, myFontSize, myFontStyle)
            txtYAxisMin.Text = AreaInfo(0).<AxisY>.<Minimum>.Value
            chkYAxisAutoMin.Checked = AreaInfo(0).<AxisY>.<AutoMinimum>.Value
            txtYAxisMax.Text = AreaInfo(0).<AxisY>.<Maximum>.Value
            chkYAxisAutoMax.Checked = AreaInfo(0).<AxisY>.<AutoMaximum>.Value
            txtYAxisAnnotInt.Text = AreaInfo(0).<AxisY>.<Interval>.Value
            chkYAxisAutoAnnotInt.Checked = AreaInfo(0).<AxisY>.<AutoInterval>.Value
            txtYAxisLabelStyleFormat.Text = AreaInfo(0).<AxisY>.<LabelStyleFormat>.Value
            chkYAxisScrollBar.Checked = AreaInfo(0).<AxisY>.<Scrollbar>.Value
            chkLogYAxis.Checked = AreaInfo(0).<AxisY>.<Logarithmic>.Value

            txtY2AxisTitle.Text = AreaInfo(0).<AxisY2>.<Title>.<Text>.Value
            cmbY2AxisTitleAlignment.SelectedIndex = cmbXAxisTitleAlignment.FindStringExact(AreaInfo(0).<AxisY2>.<Title>.<Alignment>.Value)
            txtY2AxisTitle.ForeColor = Color.FromArgb(AreaInfo(0).<AxisY2>.<Title>.<ForeColor>.Value)
            myFontName = AreaInfo(0).<AxisY2>.<Title>.<Font>.<Name>.Value
            myFontStyle = FontStyle.Regular
            If AreaInfo(0).<AxisY2>.<Title>.<Font>.<Bold>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Bold
            If AreaInfo(0).<AxisY2>.<Title>.<Font>.<Italic>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Italic
            If AreaInfo(0).<AxisY2>.<Title>.<Font>.<Strikeout>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Strikeout
            If AreaInfo(0).<AxisY2>.<Title>.<Font>.<Underline>.Value = True Then myFontStyle = myFontStyle Or FontStyle.Underline
            myFontSize = AreaInfo(0).<AxisY2>.<Title>.<Font>.<Size>.Value
            txtY2AxisTitle.Font = New Font(myFontName, myFontSize, myFontStyle)
            txtY2AxisMin.Text = AreaInfo(0).<AxisY2>.<Minimum>.Value
            chkY2AxisAutoMin.Checked = AreaInfo(0).<AxisY2>.<AutoMinimum>.Value
            txtY2AxisMax.Text = AreaInfo(0).<AxisY2>.<Maximum>.Value
            chkY2AxisAutoMax.Checked = AreaInfo(0).<AxisY2>.<AutoMaximum>.Value
            txtY2AxisAnnotInt.Text = AreaInfo(0).<AxisY2>.<Interval>.Value
            chkY2AxisAutoAnnotInt.Checked = AreaInfo(0).<AxisY2>.<AutoInterval>.Value
            txtY2AxisLabelStyleFormat.Text = AreaInfo(0).<AxisY2>.<LabelStyleFormat>.Value
            chkY2AxisScrollBar.Checked = AreaInfo(0).<AxisY2>.<Scrollbar>.Value
            chkLogY2Axis.Checked = AreaInfo(0).<AxisY2>.<Logarithmic>.Value
        End If
    End Sub



    Private Sub btnDisplaySeriesXml_Click(sender As Object, e As EventArgs) Handles btnDisplaySeriesXml.Click
        Dim SeriesInfo = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series>

        Dim RowNo As Integer = DataGridView1.SelectedCells(0).RowIndex
        Main.Message.AddXml(SeriesInfo(RowNo).ToString)
    End Sub

    Private Sub btnNewChart_Click(sender As Object, e As EventArgs) Handles btnNewChart.Click
        'Create a new chart

        If DataSource Is Nothing Then
            Main.Message.AddWarning("No Data Source selected." & vbCrLf)
        Else

            'Get the new Chart Name and Description:
            Dim EntryForm As New ADVL_Utilities_Library_1.frmNewDataNameModal
            EntryForm.EntryName = "NewChart"
            EntryForm.Title = "New Chart"
            EntryForm.GetDataName = True
            EntryForm.GetDataDescription = True
            EntryForm.SettingsLocn = Main.Project.SettingsLocn
            EntryForm.DataLocn = Main.Project.DataLocn
            EntryForm.ApplicationName = Main.ApplicationInfo.Name
            Dim NewChartName As String
            If EntryForm.ShowDialog() = DialogResult.OK Then
                NewChartName = EntryForm.DataName

                If DataSource.ChartList.ContainsKey(NewChartName) Then
                    Dim Result As DialogResult = MessageBox.Show("Overwrite existing chart?", "Warning", MessageBoxButtons.YesNo)
                    If Result = DialogResult.No Then Exit Sub
                End If

                txtSelChartName.Text = NewChartName
                txtSelChartDescr.Text = EntryForm.DataDescription
            Else
                Exit Sub
            End If

            If DataSource.Data.Tables.Contains(TableName) Then
                If DataSource.Data.Tables(TableName).Columns.Count < 2 Then
                    Main.Message.AddWarning("The data table does not contain enough columns to chart." & vbCrLf)
                Else
                    ChartName = NewChartName
                    NewChartInfo(DataSource.Data.Tables(TableName).Columns(0).ColumnName, DataSource.Data.Tables(TableName).Columns(1).ColumnName)
                End If
            Else
                Main.Message.AddWarning("The data table is empty." & vbCrLf)
            End If
        End If
    End Sub

    Private Sub btnAddSeries_Click(sender As Object, e As EventArgs) Handles btnAddSeries.Click
        'Add a new series to the chart.

        If ChartName = "" Then
            Main.Message.AddWarning("Please open a chart." & vbCrLf)
            Exit Sub
        End If

        Dim NRows As Integer = DataGridView1.Rows.Count
        Dim Field1 As String = cboXField.Items(0)
        Dim YFieldItems As Integer = cboYField.Items.Count
        Dim Field2 As String
        If NRows + 2 > YFieldItems Then 'There are more series rows than Fields.
            Field2 = cboYField.Items(YFieldItems - 1) 'Re-plot the last avaialble Field.
        Else
            Field2 = cboYField.Items(NRows) 'Plot the next available Field.
        End If
        Dim NewSeriesName As String = "Series" & NRows + 1
        DataGridView1.Rows.Add(NewSeriesName, "Point", "ChartArea1", Field1, "Primary", "Auto", Field2, "Primary", "Auto", "Yes", "", "", "1", "Circle", "5", "1", "", "1", "0") 'Add a new Series row to the grid.
        DataGridView1.Rows(NRows).Cells(10).Style.BackColor = Color.Red 'Marker Color
        DataGridView1.Rows(NRows).Cells(11).Style.BackColor = Color.Black 'Border Color
        DataGridView1.Rows(NRows).Cells(16).Style.BackColor = Color.Blue 'Line Color
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()

        myChart.Series.Add(NewSeriesName) 'Add the new Series to the Chart.

        'Add the new ChartArea to the ChartInfo XDocument:
        Dim NewSeries As New XElement("Series")
        Dim seriesName As New XElement("Name", NewSeriesName)
        NewSeries.Add(seriesName)
        Dim seriesChartType As New XElement("ChartType", "Point")
        NewSeries.Add(seriesChartType)
        Dim seriesChartArea As New XElement("ChartArea", "ChartArea1")
        NewSeries.Add(seriesChartArea)
        Dim seriesLegend As New XElement("Legend", "Legend1")
        NewSeries.Add(seriesLegend)
        Dim seriesAxisLabel As New XElement("AxisLabel", "")
        NewSeries.Add(seriesAxisLabel)
        Dim seriesXFieldName As New XElement("XFieldName", Field1)
        NewSeries.Add(seriesXFieldName)
        Dim seriesXAxisType As New XElement("XAxisType", "Primary")
        NewSeries.Add(seriesXAxisType)

        Dim seriesXValueType As New XElement("XValueType", "Auto")
        NewSeries.Add(seriesXValueType)

        Dim seriesYFieldName As New XElement("YFieldName", Field2)
        NewSeries.Add(seriesYFieldName)
        Dim seriesYAxisType As New XElement("YAxisType", "Primary")
        NewSeries.Add(seriesYAxisType)

        Dim seriesMarker As New XElement("Marker")
        Dim seriesMarkerBorderColor As New XElement("BorderColor", "-16777216")
        seriesMarker.Add(seriesMarkerBorderColor)
        Dim seriesMarkerBorderWidth As New XElement("BorderWidth", "1")
        seriesMarker.Add(seriesMarkerBorderWidth)
        Dim seriesMarkerFill As New XElement("Fill", "true")
        seriesMarker.Add(seriesMarkerFill)
        Dim seriesMarkerColor As New XElement("Color", "-65536")
        seriesMarker.Add(seriesMarkerColor)
        Dim seriesMarkerSize As New XElement("Size", "6")
        seriesMarker.Add(seriesMarkerSize)
        Dim seriesMarkerStep As New XElement("Step", "1")
        seriesMarker.Add(seriesMarkerStep)
        Dim seriesMarkerStyle As New XElement("Style", "Circle")
        seriesMarker.Add(seriesMarkerStyle)
        NewSeries.Add(seriesMarker)

        Dim seriesColor As New XElement("Color", "-16776961")
        NewSeries.Add(seriesColor)
        Dim seriesWidth As New XElement("Width", "1")
        NewSeries.Add(seriesWidth)
        Dim seriesToolTip As New XElement("ToolTip", "1")
        NewSeries.Add(seriesToolTip)

        SeriesInfo(NRows - 1).AddAfterSelf(NewSeries)

    End Sub

    Private Sub btnDeleteSeries_Click(sender As Object, e As EventArgs) Handles btnDeleteSeries.Click
        'Deleted the selected Series.
        Dim RowNo As Integer = DataGridView1.SelectedCells(0).RowIndex
        myChart.Series.RemoveAt(RowNo)
        SeriesInfo(RowNo).Remove()
        UpdateSeriesTabSettings()

    End Sub

    Private Function PreferredInterval(ByVal RawInterval As Double) As Double
        'Return a preferred interval value from a raw interval.
        'Preferred intervals are rounded to a number containing fewer significant figures.
        'Examples: Raw    Preferred
        '          0.234  0.25
        '          497    500
        '          89.4   100  
        '          18.1   20

        'Convert the RawInterval to scientific notation Coeff x 10 ^ Exponent
        Dim Coeff As Double
        Dim Exponent As Integer

        Dim Log10RawInt As Double = Math.Log10(RawInterval)
        Exponent = Math.Floor(Log10RawInt)
        Coeff = RawInterval / 10 ^ Exponent

        Dim PreferredCoeff = NearestPrefCoeff(Coeff, {1, 2, 2.5, 5, 10}) 'Select the coefficient from the preferred coefficient list - the one nearest to the raw coefficient
        Return PreferredCoeff * 10 ^ Exponent 'The preferred interval is reconstructed from the preferred coefficient and the exponent
    End Function

    Private Function NearestPrefCoeff(ByVal RawCoeff As Double, ByVal PrefCoeff() As Double) As Double
        'Returns the nearest preferred coefficient to the Raw Coefficient

        If PrefCoeff.Count > 0 Then
            Dim Nearest As Double = PrefCoeff(0) 'The current nearest preferred coefficent
            Dim NearestAbsDiff As Double = Math.Abs(RawCoeff - PrefCoeff(0)) 'The current nearest absolute difference between the Raw Coefficient and the Preferred Coefficient
            Dim AbsDiff As Double 'The absolute difference between the Raw Coefficient and the Preferred Coefficient
            For Each item In PrefCoeff
                AbsDiff = Math.Abs(RawCoeff - item)
                If AbsDiff < NearestAbsDiff Then
                    Nearest = item
                    NearestAbsDiff = AbsDiff
                End If
            Next
            Return Nearest
        Else
            Main.Message.AddWarning("There are no preferred coefficents in the list." & vbCrLf)
            Return RawCoeff
        End If
    End Function

    Private Sub NewChartInfo(ByVal XFieldName As String, ByVal YFieldName As String)
        'Create a new ChartInfo XDocument.

        'The values plotted on the X Axis are in the XFieldName column.
        'The values plotted on the Y Axis are in the YFieldName column.

        'Calculate the X Axis display settings:
        Dim XMinSeriesVal As Double = DataSource.Data.Tables(TableName).Compute("Min(" & XFieldName & ")", "")
        Dim XMaxSeriesVal As Double = DataSource.Data.Tables(TableName).Compute("Max(" & XFieldName & ")", "")
        Dim XRawPixelsPerInterval As Integer = 100 'Default number of pixels between each annotated axis label along the axis
        Dim XAxisLength As Single = myChart.ChartAreas(0).InnerPlotPosition.Width 'Width in relative coordinates, which range from 0 to 100.
        Dim ChartPixelWidth As Integer = myChart.Width 'The width of the entire chart image in pixels.
        Dim XAxisPixelLength As Integer = Int(ChartPixelWidth * XAxisLength / 100)
        Dim XRawNIntervals As Integer = Int(XAxisPixelLength / XRawPixelsPerInterval) 'The Raw number of axis annotation intervals based on the Raw pixels per interval specification.

        Dim XRawInterval As Double = (XMaxSeriesVal - XMinSeriesVal) / XRawNIntervals 'First calculate the Raw Interval
        Dim XPrefInterval As Double = PreferredInterval(XRawInterval) 'The preferred interval has the significant digits: 1, 2, 2.5, 5, 10
        Dim XPrefMin As Double = Math.Floor(XMinSeriesVal / XPrefInterval) * XPrefInterval 'The preferred Axis Minimum for the Chart display
        Dim XPrefMax As Double = Math.Ceiling(XMaxSeriesVal / XPrefInterval) * XPrefInterval 'The preferred Axis Maximum for the Chart display

        Dim XAxisTitle As String = XFieldName

        'Calculate the Y Axis display settings:
        Dim YMinSeriesVal As Double = DataSource.Data.Tables(TableName).Compute("Min(" & YFieldName & ")", "")
        Dim YMaxSeriesVal As Double = DataSource.Data.Tables(TableName).Compute("Max(" & YFieldName & ")", "")
        Dim YRawPixelsPerInterval As Integer = 100 'Default number of pixels between each annotated axis label along the axis
        Dim YAxisLength As Single = myChart.ChartAreas(0).InnerPlotPosition.Height 'Height in relative coordinates, which range from 0 to 100.
        Dim ChartPixelHeight As Integer = myChart.Height 'The width of the entire chart image in pixels.
        Dim YAxisPixelLength As Integer = Int(ChartPixelHeight * YAxisLength / 100)
        Dim YRawNIntervals As Integer = Int(YAxisPixelLength / YRawPixelsPerInterval) 'The Raw number of axis annotation intervals based on the Raw pixels per interval specification.

        Dim YRawInterval As Double = (YMaxSeriesVal - YMinSeriesVal) / YRawNIntervals 'First calculate the Raw Interval
        Dim YPrefInterval As Double = PreferredInterval(YRawInterval) 'The preferred interval has the significant digits: 1, 2, 2.5, 5, 10
        Dim YPrefMin As Double = Math.Floor(YMinSeriesVal / YPrefInterval) * YPrefInterval 'The preferred Axis Minimum for the Chart display
        Dim YPrefMax As Double = Math.Ceiling(YMaxSeriesVal / YPrefInterval) * YPrefInterval 'The preferred Axis Maximum for the Chart display

        Dim YAxisTitle As String = YFieldName

        ChartInfo = <?xml version="1.0" encoding="utf-8"?>
                    <!---->
                    <!--Data Table Chart Settings-->
                    <ChartSettings>
                        <Description><%= txtSelChartDescr.Text %></Description>
                        <TableName><%= TableName %></TableName>
                        <FormHeight><%= myParent.Height %></FormHeight>
                        <FormWidth><%= myParent.Width %></FormWidth>
                        <FormTop><%= myParent.Top %></FormTop>
                        <FormLeft><%= myParent.Left %></FormLeft>
                        <TitlesCollection>
                            <Title>
                                <Name>Title1</Name>
                                <ChartArea>ChartArea1</ChartArea>
                                <Text><%= txtSelChartName.Text %></Text>
                                <TextOrientation>Auto</TextOrientation>
                                <Alignment>TopCenter</Alignment>
                                <ForeColor>-16777216</ForeColor>
                                <Font>
                                    <Name>Microsoft Sans Serif</Name>
                                    <Size>14.25</Size>
                                    <Bold>true</Bold>
                                    <Italic>false</Italic>
                                    <Strikeout>false</Strikeout>
                                    <Underline>false</Underline>
                                </Font>
                            </Title>
                        </TitlesCollection>
                        <SeriesCollection>
                            <Series>
                                <Name>Series1</Name>
                                <ChartType>Point</ChartType>
                                <ChartArea>ChartArea1</ChartArea>
                                <Legend>Legend1</Legend>
                                <AxisLabel/>
                                <XFieldName><%= XFieldName %></XFieldName>
                                <XAxisType>Primary</XAxisType>
                                <XValueType>Auto</XValueType>
                                <YFieldName><%= YFieldName %></YFieldName>
                                <YAxisType>Primary</YAxisType>
                                <YValueType>Auto</YValueType>
                                <Marker>
                                    <BorderColor><%= Color.Black.ToArgb.ToString %></BorderColor>
                                    <BorderWidth>1</BorderWidth>
                                    <Fill>Yes</Fill>
                                    <Color><%= Color.Red.ToArgb.ToString %></Color>
                                    <Size>6</Size>
                                    <Step>1</Step>
                                    <Style>Circle</Style>
                                </Marker>
                                <Color><%= Color.Blue.ToArgb.ToString %></Color>
                                <Width>1</Width>
                                <ToolTip/>
                            </Series>
                        </SeriesCollection>
                        <ChartAreasCollection>
                            <ChartArea>
                                <Name>ChartArea1</Name>
                                <AxisX>
                                    <Title>
                                        <Text><%= XAxisTitle %></Text>
                                        <Alignment>Center</Alignment>
                                        <ForeColor>-16777216</ForeColor>
                                        <Font>
                                            <Name>Microsoft Sans Serif</Name>
                                            <Size>12</Size>
                                            <Bold>true</Bold>
                                            <Italic>false</Italic>
                                            <Strikeout>false</Strikeout>
                                            <Underline>false</Underline>
                                        </Font>
                                    </Title>
                                    <LabelStyleFormat/>
                                    <Minimum><%= XPrefMin %></Minimum>
                                    <AutoMinimum>false</AutoMinimum>
                                    <Maximum><%= XPrefMax %></Maximum>
                                    <AutoMaximum>false</AutoMaximum>
                                    <LineWidth>1</LineWidth>
                                    <Interval><%= XPrefInterval %></Interval>
                                    <AutoInterval>false</AutoInterval>
                                    <IntervalOffset>0</IntervalOffset>
                                    <Crossing>NaN</Crossing>
                                    <Scrollbar>false</Scrollbar>
                                    <Logarithmic>false</Logarithmic>
                                    <RoundAxisValues>true</RoundAxisValues>
                                </AxisX>
                                <AxisX2>
                                    <Title>
                                        <Text/>
                                        <Alignment>Center</Alignment>
                                        <ForeColor>-16777216</ForeColor>
                                        <Font>
                                            <Name>Microsoft Sans Serif</Name>
                                            <Size>8</Size>
                                            <Bold>false</Bold>
                                            <Italic>false</Italic>
                                            <Strikeout>false</Strikeout>
                                            <Underline>false</Underline>
                                        </Font>
                                    </Title>
                                    <LabelStyleFormat/>
                                    <Minimum>-10</Minimum>
                                    <AutoMinimum>true</AutoMinimum>
                                    <Maximum>10</Maximum>
                                    <AutoMaximum>true</AutoMaximum>
                                    <LineWidth>1</LineWidth>
                                    <Interval>0</Interval>
                                    <AutoInterval>true</AutoInterval>
                                    <IntervalOffset>0</IntervalOffset>
                                    <Crossing>NaN</Crossing>
                                    <Scrollbar>false</Scrollbar>
                                    <Logarithmic>false</Logarithmic>
                                    <RoundAxisValues>true</RoundAxisValues>
                                </AxisX2>
                                <AxisY>
                                    <Title>
                                        <Text><%= YAxisTitle %></Text>
                                        <Alignment>Center</Alignment>
                                        <ForeColor>-16777216</ForeColor>
                                        <Font>
                                            <Name>Microsoft Sans Serif</Name>
                                            <Size>12</Size>
                                            <Bold>true</Bold>
                                            <Italic>false</Italic>
                                            <Strikeout>false</Strikeout>
                                            <Underline>false</Underline>
                                        </Font>
                                    </Title>
                                    <LabelStyleFormat/>
                                    <Minimum><%= YPrefMin %></Minimum>
                                    <AutoMinimum>false</AutoMinimum>
                                    <Maximum><%= YPrefMax %></Maximum>
                                    <AutoMaximum>false</AutoMaximum>
                                    <LineWidth>1</LineWidth>
                                    <Interval><%= YPrefInterval %></Interval>
                                    <AutoInterval>false</AutoInterval>
                                    <IntervalOffset>0</IntervalOffset>
                                    <Crossing>NaN</Crossing>
                                    <Scrollbar>false</Scrollbar>
                                    <Logarithmic>false</Logarithmic>
                                </AxisY>
                                <AxisY2>
                                    <Title>
                                        <Text/>
                                        <Alignment>Center</Alignment>
                                        <ForeColor>-16777216</ForeColor>
                                        <Font>
                                            <Name>Microsoft Sans Serif</Name>
                                            <Size>8</Size>
                                            <Bold>false</Bold>
                                            <Italic>false</Italic>
                                            <Strikeout>false</Strikeout>
                                            <Underline>false</Underline>
                                        </Font>
                                    </Title>
                                    <LabelStyleFormat/>
                                    <Minimum>-20</Minimum>
                                    <AutoMinimum>true</AutoMinimum>
                                    <Maximum>20</Maximum>
                                    <AutoMaximum>true</AutoMaximum>
                                    <LineWidth>1</LineWidth>
                                    <Interval>0</Interval>
                                    <AutoInterval>true</AutoInterval>
                                    <IntervalOffset>0</IntervalOffset>
                                    <Crossing>NaN</Crossing>
                                    <Scrollbar>false</Scrollbar>
                                    <Logarithmic>false</Logarithmic>
                                </AxisY2>
                            </ChartArea>
                        </ChartAreasCollection>
                    </ChartSettings>

        txtSelChartName.Text = ChartName
        txtSelChartDescr.Text = ChartInfo.<ChartSettings>.<Description>.Value
        AreaInfo = From item In ChartInfo.<ChartSettings>.<ChartAreasCollection>.<ChartArea>
        SeriesInfo = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series>
        TitleInfo = From item In ChartInfo.<ChartSettings>.<TitlesCollection>.<Title>
        UpdateAreaOptions()

        LoadChartInfo() 'Update the Chart Display.

        UpdateTitlesTabSettings()
        UpdateAreasTabSettings()
        UpdateSeriesTabSettings()

    End Sub

    Private Sub NewChartInfo_Old(ByVal XFieldName As String, ByVal YFieldName As String)
        'Create a new ChartInfo XDocument.

        ChartInfo = <?xml version="1.0" encoding="utf-8"?>
                    <!---->
                    <!--Data Table Chart Settings-->
                    <ChartSettings>
                        <Description><%= txtSelChartDescr.Text %></Description>
                        <TitlesCollection>
                            <Title>
                                <Name>Title1</Name>
                                <ChartArea>ChartArea1</ChartArea>
                                <Text>New Chart</Text>
                                <TextOrientation>Auto</TextOrientation>
                                <Alignment>TopCenter</Alignment>
                                <ForeColor>-16777216</ForeColor>
                                <Font>
                                    <Name>Microsoft Sans Serif</Name>
                                    <Size>14.25</Size>
                                    <Bold>true</Bold>
                                    <Italic>false</Italic>
                                    <Strikeout>false</Strikeout>
                                    <Underline>false</Underline>
                                </Font>
                            </Title>
                        </TitlesCollection>
                        <SeriesCollection>
                            <Series>
                                <Name>Series1</Name>
                                <ChartType>Point</ChartType>
                                <ChartArea>ChartArea1</ChartArea>
                                <Legend>Legend1</Legend>
                                <AxisLabel/>
                                <XFieldName><%= XFieldName %></XFieldName>
                                <XAxisType>Primary</XAxisType>
                                <XValueType>Auto</XValueType>
                                <YFieldName><%= YFieldName %></YFieldName>
                                <YAxisType>Primary</YAxisType>
                                <YValueType>Auto</YValueType>
                                <Marker>
                                    <BorderColor><%= Color.Black.ToArgb.ToString %></BorderColor>
                                    <BorderWidth>1</BorderWidth>
                                    <Fill>Yes</Fill>
                                    <Color><%= Color.Red.ToArgb.ToString %></Color>
                                    <Size>6</Size>
                                    <Step>1</Step>
                                    <Style>Circle</Style>
                                </Marker>
                                <Color><%= Color.Blue.ToArgb.ToString %></Color>
                                <Width>1</Width>
                                <ToolTip/>
                            </Series>
                        </SeriesCollection>
                        <ChartAreasCollection>
                            <ChartArea>
                                <Name>ChartArea1</Name>
                                <AxisX>
                                    <Title>
                                        <Text>X Axis</Text>
                                        <Alignment>Center</Alignment>
                                        <ForeColor>-16777216</ForeColor>
                                        <Font>
                                            <Name>Microsoft Sans Serif</Name>
                                            <Size>12</Size>
                                            <Bold>true</Bold>
                                            <Italic>false</Italic>
                                            <Strikeout>false</Strikeout>
                                            <Underline>false</Underline>
                                        </Font>
                                    </Title>
                                    <LabelStyleFormat/>
                                    <Minimum>-20</Minimum>
                                    <AutoMinimum>true</AutoMinimum>
                                    <Maximum>20</Maximum>
                                    <AutoMaximum>true</AutoMaximum>
                                    <LineWidth>1</LineWidth>
                                    <Interval>0</Interval>
                                    <AutoInterval>true</AutoInterval>
                                    <IntervalOffset>0</IntervalOffset>
                                    <Crossing>NaN</Crossing>
                                    <Scrollbar>false</Scrollbar>
                                    <Logarithmic>false</Logarithmic>
                                    <RoundAxisValues>true</RoundAxisValues>
                                </AxisX>
                                <AxisX2>
                                    <Title>
                                        <Text/>
                                        <Alignment>Center</Alignment>
                                        <ForeColor>-16777216</ForeColor>
                                        <Font>
                                            <Name>Microsoft Sans Serif</Name>
                                            <Size>8</Size>
                                            <Bold>false</Bold>
                                            <Italic>false</Italic>
                                            <Strikeout>false</Strikeout>
                                            <Underline>false</Underline>
                                        </Font>
                                    </Title>
                                    <LabelStyleFormat/>
                                    <Minimum>-10</Minimum>
                                    <AutoMinimum>true</AutoMinimum>
                                    <Maximum>10</Maximum>
                                    <AutoMaximum>true</AutoMaximum>
                                    <LineWidth>1</LineWidth>
                                    <Interval>0</Interval>
                                    <AutoInterval>true</AutoInterval>
                                    <IntervalOffset>0</IntervalOffset>
                                    <Crossing>NaN</Crossing>
                                    <Scrollbar>false</Scrollbar>
                                    <Logarithmic>false</Logarithmic>
                                    <RoundAxisValues>true</RoundAxisValues>
                                </AxisX2>
                                <AxisY>
                                    <Title>
                                        <Text>Y Axis</Text>
                                        <Alignment>Center</Alignment>
                                        <ForeColor>-16777216</ForeColor>
                                        <Font>
                                            <Name>Microsoft Sans Serif</Name>
                                            <Size>12</Size>
                                            <Bold>true</Bold>
                                            <Italic>false</Italic>
                                            <Strikeout>false</Strikeout>
                                            <Underline>false</Underline>
                                        </Font>
                                    </Title>
                                    <LabelStyleFormat/>
                                    <Minimum>-20</Minimum>
                                    <AutoMinimum>true</AutoMinimum>
                                    <Maximum>20</Maximum>
                                    <AutoMaximum>true</AutoMaximum>
                                    <LineWidth>1</LineWidth>
                                    <Interval>0</Interval>
                                    <AutoInterval>true</AutoInterval>
                                    <IntervalOffset>0</IntervalOffset>
                                    <Crossing>NaN</Crossing>
                                    <Scrollbar>false</Scrollbar>
                                    <Logarithmic>false</Logarithmic>
                                </AxisY>
                                <AxisY2>
                                    <Title>
                                        <Text/>
                                        <Alignment>Center</Alignment>
                                        <ForeColor>-16777216</ForeColor>
                                        <Font>
                                            <Name>Microsoft Sans Serif</Name>
                                            <Size>8</Size>
                                            <Bold>false</Bold>
                                            <Italic>false</Italic>
                                            <Strikeout>false</Strikeout>
                                            <Underline>false</Underline>
                                        </Font>
                                    </Title>
                                    <LabelStyleFormat/>
                                    <Minimum>-20</Minimum>
                                    <AutoMinimum>true</AutoMinimum>
                                    <Maximum>20</Maximum>
                                    <AutoMaximum>true</AutoMaximum>
                                    <LineWidth>1</LineWidth>
                                    <Interval>0</Interval>
                                    <AutoInterval>true</AutoInterval>
                                    <IntervalOffset>0</IntervalOffset>
                                    <Crossing>NaN</Crossing>
                                    <Scrollbar>false</Scrollbar>
                                    <Logarithmic>false</Logarithmic>
                                </AxisY2>
                            </ChartArea>
                        </ChartAreasCollection>
                    </ChartSettings>

        txtSelChartName.Text = ChartName
        txtSelChartDescr.Text = ChartInfo.<ChartSettings>.<Description>.Value
        AreaInfo = From item In ChartInfo.<ChartSettings>.<ChartAreasCollection>.<ChartArea>
        SeriesInfo = From item In ChartInfo.<ChartSettings>.<SeriesCollection>.<Series>
        TitleInfo = From item In ChartInfo.<ChartSettings>.<TitlesCollection>.<Title>
        UpdateAreaOptions()

        LoadChartInfo() 'Update the Chart Display.

        UpdateTitlesTabSettings()
        UpdateAreasTabSettings()
        UpdateSeriesTabSettings()

    End Sub

    Private Sub btnDisplayXml_Click(sender As Object, e As EventArgs) Handles btnDisplayXml.Click
        'Display the Chart XML document in the Messages window.

        Main.Message.AddText(vbCrLf & "Chart XML Document:" & vbCrLf, "Bold")
        Main.Message.AddXml(ChartInfo.ToString & vbCrLf)
    End Sub

    Private Sub ChartProperties()
        'This subroutine shows all the Chart Control properties.


        myChart.BackColor = Color.White
        myChart.BorderlineColor = Color.Black
        myChart.SuppressExceptions = True


        Dim AnnotationsCount As Integer = myChart.Annotations.Count

        Dim LegendsCount As Integer = myChart.Legends.Count

        Dim TitlesCount As Integer = myChart.Titles.Count
        myChart.Titles(0).Name = "Title1"
        myChart.Titles(0).Text = "Chart Title"
        myChart.Titles(0).TextOrientation = DataVisualization.Charting.TextOrientation.Auto
        myChart.Titles(0).Alignment = ContentAlignment.TopCenter
        myChart.Titles(0).ForeColor = Color.Black
        myChart.Titles(0).TextStyle = DataVisualization.Charting.TextStyle.Default
        myChart.Titles(0).Font = New Font("Microsoft Sans Serif", 12, FontStyle.Regular)

        Dim SeriesCount As Integer = myChart.Series.Count
        myChart.Series(0).Name = "Series1"
        myChart.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Point
        myChart.Series(0).ChartArea = "ChartArea1"
        myChart.Series(0).Legend = "Legend1"
        myChart.Series(0).AxisLabel = "Axis Label 1"
        myChart.Series(0).IsVisibleInLegend = True
        myChart.Series(0).XAxisType = DataVisualization.Charting.AxisType.Primary
        myChart.Series(0).XValueType = DataVisualization.Charting.ChartValueType.Single
        myChart.Series(0).YAxisType = DataVisualization.Charting.AxisType.Primary
        myChart.Series(0).YValueType = DataVisualization.Charting.ChartValueType.Single
        myChart.Series(0).MarkerBorderColor = Color.Black
        myChart.Series(0).MarkerBorderWidth = 1
        myChart.Series(0).MarkerColor = Color.Transparent 'Use Transparent for No Fill
        myChart.Series(0).MarkerColor = Color.Red
        myChart.Series(0).MarkerSize = 6
        myChart.Series(0).MarkerStep = 1
        myChart.Series(0).MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
        myChart.Series(0).Color = Color.Blue
        myChart.Series(0).BorderWidth = 1
        myChart.Series(0).ToolTip = ""

        Dim AreasCount As Integer = myChart.ChartAreas.Count
        myChart.ChartAreas(0).Name = "ChartArea1"
        myChart.ChartAreas(0).AxisX.Title = "X Axis"
        myChart.ChartAreas(0).AxisX.TitleAlignment = StringAlignment.Center
        myChart.ChartAreas(0).AxisX.TitleForeColor = Color.Black
        myChart.ChartAreas(0).AxisX.TitleFont = New Font("Microsoft Sans Serif", 12, FontStyle.Regular)
        myChart.ChartAreas(0).AxisX.LabelStyle.Format = ""
        myChart.ChartAreas(0).AxisX.Minimum = 0 'Set to NaN for Auto Minimum.
        myChart.ChartAreas(0).AxisX.Maximum = 10 'Set to NaN for Auto Maximum
        myChart.ChartAreas(0).AxisX.LineWidth = 1
        myChart.ChartAreas(0).AxisX.IsLogarithmic = False
        myChart.ChartAreas(0).AxisX.Interval = 1 'Interval of the Labels and Major tick marks. Set to 0 for Auto Interval.
        myChart.ChartAreas(0).AxisX.IntervalType = DataVisualization.Charting.DateTimeIntervalType.Number  'Auto, NotSet, Number, Years, Months, Weeks, Days, Hours, Minutes, Seconds, Milliseconds 
        myChart.ChartAreas(0).AxisX.IntervalOffset = 0 'Use the Zero value for the Auto Interval Offset.
        myChart.ChartAreas(0).AxisX.Crossing = Single.NaN

    End Sub

    Private Sub Label36_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnUpdateChart_Click(sender As Object, e As EventArgs) Handles btnUpdateChart.Click
        'Update the Chart display with the new Series settings:

        If DataGridView1.SelectedCells.Count = 0 Then
            Main.Message.AddWarning("No Series rows have been selected." & vbCrLf)
        Else
            Dim RowNo As Integer = DataGridView1.SelectedCells(0).RowIndex
            If myChart.Series.Count < RowNo + 1 Then
                'The selected Series number is too high. 
                Main.Message.AddWarning("The selected Series number is too high. There are only " & myChart.Series.Count & " series in the chart." & vbCrLf)
            Else

                If DataSource Is Nothing Then

                Else
                    Try
                        myChart.Series(RowNo).Points.DataBindXY(DataSource.Data.Tables(TableName).DefaultView, DataGridView1.Rows(RowNo).Cells(3).Value, DataSource.Data.Tables(TableName).DefaultView, DataGridView1.Rows(RowNo).Cells(6).Value)
                    Catch ex As Exception
                        Main.Message.AddWarning(ex.Message & vbCrLf)
                    End Try
                End If

            End If
        End If

    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        'A cell in DataGridView1 has been edited.

        Dim RowNo As Integer = e.RowIndex
        UpdateSeries(RowNo)
    End Sub

    Private Sub UpdateSeries(ByVal RowNo As Integer)
        'Update the series settings using the DataGridView1 settings in row number RowNo.

        If RowNo = -1 Then Exit Sub

        SeriesInfo(RowNo).<Name>.Value = DataGridView1.Rows(RowNo).Cells(0).Value
        myChart.Series(RowNo).Name = DataGridView1.Rows(RowNo).Cells(0).Value

        SeriesInfo(RowNo).<ChartType>.Value = DataGridView1.Rows(RowNo).Cells(1).Value
        Select Case DataGridView1.Rows(RowNo).Cells(1).Value
            Case "Line"
                myChart.Series(RowNo).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Case "Point"
                myChart.Series(RowNo).ChartType = DataVisualization.Charting.SeriesChartType.Point
            Case "Bar"
                myChart.Series(RowNo).ChartType = DataVisualization.Charting.SeriesChartType.Bar
            Case "Column"
                myChart.Series(RowNo).ChartType = DataVisualization.Charting.SeriesChartType.Column
        End Select
        myChart.Series(RowNo).ChartArea = DataGridView1.Rows(RowNo).Cells(2).Value
        SeriesInfo(RowNo).<ChartArea>.Value = DataGridView1.Rows(RowNo).Cells(2).Value

        If SeriesInfo(RowNo).<XFieldName>.Value = DataGridView1.Rows(RowNo).Cells(3).Value Then
            'The X Field Name has not been changed.
        Else
            Dim FieldName As String = DataGridView1.Rows(RowNo).Cells(3).Value
            SeriesInfo(RowNo).<XFieldName>.Value = FieldName
            'Update XMin and XMax:
            Dim AreaName As String = SeriesInfo(RowNo).<ChartArea>.Value
            For Each item In AreaInfo
                If item.<Name>.Value = AreaName Then
                    Dim Min As Single = Main.MonteCarlo.Data.Tables("Calculations").Compute("Min(" & FieldName & ")", "")
                    Dim Max As Single = Main.MonteCarlo.Data.Tables("Calculations").Compute("Max(" & FieldName & ")", "")
                    Dim NIntervals As Integer = 5
                    Dim RawInterval As Double = (Max - Min) / NIntervals 'First calculate the Raw Interval
                    Dim PrefInterval As Double = PreferredInterval(RawInterval)
                    Dim PrefMin As Double = Math.Floor(Min / PrefInterval) * PrefInterval
                    Dim PrefMax As Double = Math.Ceiling(Max / PrefInterval) * PrefInterval

                    item.<AxisX>.<Minimum>.Value = PrefMin
                    myChart.ChartAreas(AreaName).AxisX.Minimum = PrefMin

                    item.<AxisX>.<Maximum>.Value = PrefMax
                    myChart.ChartAreas(AreaName).AxisX.Maximum = PrefMax
                    item.<AxisX>.<Interval>.Value = PrefInterval
                    item.<AxisX>.<AutoInterval>.Value = "false"
                    myChart.ChartAreas(AreaName).AxisX.Interval = PrefInterval
                    item.<AxisX>.<Title>.<Text>.Value = FieldName
                    myChart.ChartAreas(AreaName).AxisX.Title = FieldName
                    Exit For
                End If
            Next
        End If

        SeriesInfo(RowNo).<XAxisType>.Value = DataGridView1.Rows(RowNo).Cells(4).Value
        If DataGridView1.Rows(RowNo).Cells(4).Value = "Primary" Then
            myChart.Series(RowNo).XAxisType = DataVisualization.Charting.AxisType.Primary
        ElseIf DataGridView1.Rows(RowNo).Cells(4).Value = "Secondary" Then
            myChart.Series(RowNo).XAxisType = DataVisualization.Charting.AxisType.Secondary
        Else
            Main.Message.AddWarning("Unknown X Axis type specified: " & DataGridView1.Rows(RowNo).Cells(4).Value & vbCrLf)
        End If

        SeriesInfo(RowNo).<XValueType>.Value = DataGridView1.Rows(RowNo).Cells(5).Value

        If DataGridView1.Rows(RowNo).Cells(5).Value <> Nothing Then myChart.Series(RowNo).XValueType = [Enum].Parse(GetType(DataVisualization.Charting.ChartValueType), DataGridView1.Rows(RowNo).Cells(5).Value)

        If SeriesInfo(RowNo).<YFieldName>.Value = DataGridView1.Rows(RowNo).Cells(6).Value Then
            'The Y Field Name has not been changed.
        Else
            Dim FieldName As String = DataGridView1.Rows(RowNo).Cells(6).Value
            SeriesInfo(RowNo).<YFieldName>.Value = FieldName
            'Update XMin and XMax:
            Dim AreaName As String = SeriesInfo(RowNo).<ChartArea>.Value
            For Each item In AreaInfo
                If item.<Name>.Value = AreaName Then
                    Dim Min As Single = Main.MonteCarlo.Data.Tables("Calculations").Compute("Min(" & FieldName & ")", "")
                    Dim Max As Single = Main.MonteCarlo.Data.Tables("Calculations").Compute("Max(" & FieldName & ")", "")
                    Dim NIntervals As Integer = 5
                    Dim RawInterval As Double = (Max - Min) / NIntervals 'First calculate the Raw Interval
                    Dim PrefInterval As Double = PreferredInterval(RawInterval)
                    Dim PrefMin As Double = Math.Floor(Min / PrefInterval) * PrefInterval
                    Dim PrefMax As Double = Math.Ceiling(Max / PrefInterval) * PrefInterval

                    item.<AxisY>.<Minimum>.Value = PrefMin
                    myChart.ChartAreas(AreaName).AxisY.Minimum = PrefMin
                    item.<AxisY>.<Maximum>.Value = PrefMax
                    myChart.ChartAreas(AreaName).AxisY.Maximum = PrefMax
                    item.<AxisY>.<Interval>.Value = PrefInterval
                    item.<AxisY>.<AutoInterval>.Value = "false"
                    myChart.ChartAreas(AreaName).AxisY.Interval = PrefInterval
                    item.<AxisY>.<Title>.<Text>.Value = FieldName
                    myChart.ChartAreas(AreaName).AxisY.Title = FieldName
                    Exit For
                End If
            Next
        End If

        SeriesInfo(RowNo).<YAxisType>.Value = DataGridView1.Rows(RowNo).Cells(7).Value
        If DataGridView1.Rows(RowNo).Cells(7).Value = "Primary" Then
            myChart.Series(RowNo).YAxisType = DataVisualization.Charting.AxisType.Primary
        ElseIf DataGridView1.Rows(RowNo).Cells(7).Value = "Secondary" Then
            myChart.Series(RowNo).YAxisType = DataVisualization.Charting.AxisType.Secondary
        Else
            Main.Message.AddWarning("Unknown Y Axis type specified: " & DataGridView1.Rows(RowNo).Cells(7).Value & vbCrLf)
        End If

        SeriesInfo(RowNo).<YValueType>.Value = DataGridView1.Rows(RowNo).Cells(8).Value

        If DataGridView1.Rows(RowNo).Cells(8).Value <> Nothing Then myChart.Series(RowNo).YValueType = [Enum].Parse(GetType(DataVisualization.Charting.ChartValueType), DataGridView1.Rows(RowNo).Cells(8).Value)

        myChart.ChartAreas(0).AxisY.IntervalAutoMode = DataVisualization.Charting.IntervalAutoMode.VariableCount 'ADDED 23Jul20

        SeriesInfo(RowNo).<Marker>.<Fill>.Value = DataGridView1.Rows(RowNo).Cells(9).Value
        SeriesInfo(RowNo).<Marker>.<Color>.Value = DataGridView1.Rows(RowNo).Cells(10).Style.BackColor.ToArgb.ToString
        If DataGridView1.Rows(RowNo).Cells(9).Value = "Yes" Then
            myChart.Series(RowNo).MarkerColor = DataGridView1.Rows(RowNo).Cells(10).Style.BackColor
        Else
            myChart.Series(RowNo).MarkerColor = Color.Transparent
        End If

        SeriesInfo(RowNo).<Marker>.<BorderColor>.Value = DataGridView1.Rows(RowNo).Cells(11).Style.BackColor.ToArgb.ToString
        myChart.Series(RowNo).MarkerBorderColor = DataGridView1.Rows(RowNo).Cells(11).Style.BackColor

        SeriesInfo(RowNo).<Marker>.<BorderWidth>.Value = DataGridView1.Rows(RowNo).Cells(12).Value
        myChart.Series(RowNo).MarkerBorderWidth = DataGridView1.Rows(RowNo).Cells(12).Value

        SeriesInfo(RowNo).<Marker>.<Style>.Value = DataGridView1.Rows(RowNo).Cells(13).Value
        If DataGridView1.Rows(RowNo).Cells(13).Value <> Nothing Then myChart.Series(RowNo).MarkerStyle = [Enum].Parse(GetType(DataVisualization.Charting.MarkerStyle), DataGridView1.Rows(RowNo).Cells(13).Value)

        SeriesInfo(RowNo).<Marker>.<Size>.Value = DataGridView1.Rows(RowNo).Cells(14).Value
        myChart.Series(RowNo).MarkerSize = DataGridView1.Rows(RowNo).Cells(14).Value

        SeriesInfo(RowNo).<Marker>.<Step>.Value = DataGridView1.Rows(RowNo).Cells(15).Value
        myChart.Series(RowNo).MarkerStep = DataGridView1.Rows(RowNo).Cells(15).Value

        SeriesInfo(RowNo).<Color>.Value = DataGridView1.Rows(RowNo).Cells(16).Style.BackColor.ToArgb.ToString
        myChart.Series(RowNo).Color = DataGridView1.Rows(RowNo).Cells(16).Style.BackColor

        SeriesInfo(RowNo).<Width>.Value = DataGridView1.Rows(RowNo).Cells(17).Value
        myChart.Series(RowNo).BorderWidth = DataGridView1.Rows(RowNo).Cells(17).Value

        SeriesInfo(RowNo).<ToolTip>.Value = DataGridView1.Rows(RowNo).Cells(18).Value
        myChart.Series(RowNo).ToolTip = DataGridView1.Rows(RowNo).Cells(18).Value
    End Sub

    Private Sub txtChartTitle_TextChanged(sender As Object, e As EventArgs) Handles txtChartTitle.TextChanged

    End Sub

    Private Sub btnSaveChanges_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click
        'Save the changes made to the chart.

        If DataSource Is Nothing Then

        Else
            If DataSource.ChartList.ContainsKey(ChartName) Then
                DataSource.ChartList(ChartName) = ChartInfo
                DataSource.ChartName = ChartName
            Else
                Try
                    DataSource.ChartList.Add(ChartName, ChartInfo)
                    cmbChartList.Items.Add(ChartName)
                    'myParent.cmbChartList.Items.Add(ChartName) 'ERROR

                    Main.cmbChartList.Items.Add(ChartName)
                    Main.MonteCarlo.Modified = True
                    Main.ChartList(ChartFormNo).cmbChartList.Items.Add(ChartName) 'Public member 'cmbChartList' on type 'frmChart' not found.
                Catch ex As Exception
                    Main.Message.AddWarning(ex.Message & vbCrLf)
                End Try
            End If
        End If

    End Sub

    Private Sub chkRoundXAxisValues_CheckedChanged(sender As Object, e As EventArgs) Handles chkRoundXAxisValues.CheckedChanged

        If AreaInfo(AreaNo).<AxisX>.<RoundAxisValues>.Value = Nothing Then
            Dim roundAxisValues As New XElement("RoundAxisValues", chkRoundXAxisValues.Checked)

            AreaInfo(AreaNo).Element("AxisX").Add(roundAxisValues)

        Else
            AreaInfo(AreaNo).<AxisX>.<RoundAxisValues>.Value = chkRoundXAxisValues.Checked
        End If
    End Sub

    Private Sub btnDisplayChartSettings_Click(sender As Object, e As EventArgs) Handles btnDisplayChartSettings.Click
        'Display the Chart Settings XML file.
        If ChartInfo Is Nothing Then
            Main.Message.AddWarning("No chart settings have been loaded." & vbCrLf)
        Else
            Main.Message.AddText(vbCrLf & "Chart Settings XML Document:" & vbCrLf, "Bold")
            Main.Message.AddXml(ChartInfo.ToString & vbCrLf)
        End If

    End Sub



    Private Sub txtAreaRecordNo_TextChanged(sender As Object, e As EventArgs) Handles txtAreaRecordNo.TextChanged

    End Sub

    Private Sub cmbChartList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChartList.SelectedIndexChanged
        'The selected Chart in the Browse Chart List has been changed.
        'Display the chart description.

        Dim BrowseChart As String
        If cmbChartList.SelectedIndex = -1 Then
            BrowseChart = ""
            Exit Sub
        Else
            BrowseChart = cmbChartList.SelectedItem.ToString
        End If

        If DataSource Is Nothing Then
            Main.Message.AddWarning("Unknown data source" & vbCrLf)
        Else
            If DataSource.ChartList.ContainsKey(BrowseChart) Then
                Dim ChartXml As System.Xml.Linq.XDocument
                ChartXml = DataSource.ChartList(ChartName)
                txtChartDescr.Text = ChartXml.<ChartSettings>.<Description>.Value
            Else
                txtChartDescr.Text = ""
                Main.Message.AddWarning("The Chart: " & BrowseChart & " was not found in the Monte Carlo list." & vbCrLf)
            End If
        End If

    End Sub

    Private Sub txtTitlesRecordNo_TextChanged(sender As Object, e As EventArgs) Handles txtTitlesRecordNo.TextChanged

    End Sub

    Private Sub txtXAxisTitle_TextChanged(sender As Object, e As EventArgs) Handles txtXAxisTitle.TextChanged

    End Sub

    Private Sub txtYAxisTitle_TextChanged(sender As Object, e As EventArgs) Handles txtYAxisTitle.TextChanged

    End Sub

    Private Sub txtX2AxisTitle_TextChanged(sender As Object, e As EventArgs) Handles txtX2AxisTitle.TextChanged

    End Sub

    Private Sub txtSelChartName_TextChanged(sender As Object, e As EventArgs) Handles txtSelChartName.TextChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub btnApplySize_Click(sender As Object, e As EventArgs) Handles btnApplySize.Click
        'Apply the Chart form Height and Width settings.

        Dim Height As Integer = Int(txtHeight.Text)
        Dim Width As Integer = Int(txtWidth.Text)
        Dim Top As Integer = Int(txtTop.Text)
        Dim Left As Integer = Int(txtLeft.Text)

        If Height < 200 Then Height = 200 '200 pixels minimum height
        If Width < 200 Then Width = 200 '200 pixels minimum width

        myParent.Height = Height
        myParent.Width = Width
        myParent.Top = Top
        myParent.Left = Left

    End Sub

    Private Sub txtXAxisAnnotInt_TextChanged(sender As Object, e As EventArgs) Handles txtXAxisAnnotInt.TextChanged

    End Sub

    Private Sub txtXAxisIntervalOffset_TextChanged(sender As Object, e As EventArgs) Handles txtXAxisIntervalOffset.TextChanged

    End Sub

    Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter

    End Sub

#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class