Public Class frmDataInfo
    'Form for displaying information about a dataset.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    'Public DataSource As Object 'DataSource links to the oject containing the Data to be analysed. (Input, Processed, Distribution or MonteCarlo objects in the Main form.)

    Dim cboCorrVariables As New DataGridViewComboBoxColumn 'Used to select Random Variables in a correlation matrix.
    Dim cboCovVariables As New DataGridViewComboBoxColumn 'Used to select Random Variables in a covariance matrix.

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

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

    Private _dataSource As Object = Nothing 'DataSource links to the oject containing the Data to be analysed. (Input, Processed, Distribution or MonteCarlo objects in the Main form.)
    Property DataSource As Object
        Get
            Return _dataSource
        End Get
        Set(value As Object)
            _dataSource = value
            txtDatasetName.Text = DataSource.Name
            lblTableCount.Text = DataSource.Data.Tables.Count
            If DataSource.Data.Tables.Count > 0 Then
                lblTableNo.Text = "1"
                cmbDataTable.Items.Clear()
                For Each item In DataSource.Data.Tables
                    cmbDataTable.Items.Add(item.TableName)
                Next
                cmbDataTable.SelectedIndex = 0
                TableName = cmbDataTable.SelectedItem.ToString
                NRows = DataSource.Data.Tables(0).Rows.Count
                'ShowAllFieldStats()
            Else
                lblTableNo.Text = "0"
            End If
        End Set
    End Property


    Private _dataSourceDescription As String = "" 'A description of the data source object.
    Property DataSourceDescription As String
        Get
            Return _dataSourceDescription
        End Get
        Set(value As String)
            _dataSourceDescription = value
            txtDataSource.Text = _dataSourceDescription
        End Set
    End Property

    Private _tableName As String = "" 'The name of the selected table.
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
                    cmbDataTable.SelectedIndex = cmbDataTable.FindStringExact(TableName)
                    lblTableNo.Text = cmbDataTable.SelectedIndex + 1
                    'TableName = SelectTableName
                    ShowAllFieldStats()
                Else
                    Main.Message.AddWarning("A table named " & TableName & " was not found." & vbCrLf)
                End If
            End If

            'ShowAllFieldStats()
        End Set
    End Property

    Private _nRows As Integer = 0 'The number of rows in the selected table.
    Property NRows As Integer
        Get
            Return _nRows
        End Get
        Set(value As Integer)
            _nRows = value
            txtNRows.Text = _nRows
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
                               <!--Statistics Tab Number Formats-->
                               <MinFormat><%= txtMinFormat.Text %></MinFormat>
                               <MaxFormat><%= txtMaxFormat.Text %></MaxFormat>
                               <SumFormat><%= txtSumFormat.Text %></SumFormat>
                               <AvgFormat><%= txtAvgFormat.Text %></AvgFormat>
                               <StdDevFormat><%= txtStdDevFormat.Text %></StdDevFormat>
                               <VarFormat><%= txtVarFormat.Text %></VarFormat>
                               <!--Correlations Tab Number Formats-->
                               <CorrelationsFormat><%= txtCorrMatFormat.Text %></CorrelationsFormat>
                               <CorrelationsCholeskyFormat><%= txtCorrCholFormat.Text %></CorrelationsCholeskyFormat>
                               <!--Covariance Tab Number Formats-->
                               <CovarianceFormat><%= txtCovMatFormat.Text %></CovarianceFormat>
                               <CovarianceCholeskyFormat><%= txtCovCholFormat.Text %></CovarianceCholeskyFormat>
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"

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
            If Settings.<FormSettings>.<MinFormat>.Value <> Nothing Then txtMinFormat.Text = Settings.<FormSettings>.<MinFormat>.Value
            If Settings.<FormSettings>.<MaxFormat>.Value <> Nothing Then txtMaxFormat.Text = Settings.<FormSettings>.<MaxFormat>.Value
            If Settings.<FormSettings>.<SumFormat>.Value <> Nothing Then txtSumFormat.Text = Settings.<FormSettings>.<SumFormat>.Value
            If Settings.<FormSettings>.<AvgFormat>.Value <> Nothing Then txtAvgFormat.Text = Settings.<FormSettings>.<AvgFormat>.Value
            If Settings.<FormSettings>.<StdDevFormat>.Value <> Nothing Then txtStdDevFormat.Text = Settings.<FormSettings>.<StdDevFormat>.Value
            If Settings.<FormSettings>.<VarFormat>.Value <> Nothing Then txtVarFormat.Text = Settings.<FormSettings>.<VarFormat>.Value

            If Settings.<FormSettings>.<CorrelationsFormat>.Value <> Nothing Then txtCorrMatFormat.Text = Settings.<FormSettings>.<CorrelationsFormat>.Value
            If Settings.<FormSettings>.<CorrelationsCholeskyFormat>.Value <> Nothing Then txtCorrCholFormat.Text = Settings.<FormSettings>.<CorrelationsCholeskyFormat>.Value

            If Settings.<FormSettings>.<CovarianceFormat>.Value <> Nothing Then txtCovMatFormat.Text = Settings.<FormSettings>.<CovarianceFormat>.Value
            If Settings.<FormSettings>.<CovarianceCholeskyFormat>.Value <> Nothing Then txtCovCholFormat.Text = Settings.<FormSettings>.<CovarianceCholeskyFormat>.Value

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

        dgvStatistics.ColumnCount = 7
        dgvStatistics.Columns(0).HeaderText = "Field Name"
        dgvStatistics.Columns(1).HeaderText = "Minimum"
        dgvStatistics.Columns(2).HeaderText = "Maximum"
        dgvStatistics.Columns(3).HeaderText = "Sum"
        dgvStatistics.Columns(4).HeaderText = "Average"
        dgvStatistics.Columns(5).HeaderText = "Standard Deviation"
        dgvStatistics.Columns(6).HeaderText = "Variance"
        dgvStatistics.AllowUserToAddRows = False

        dgvCorrMatrix.ColumnCount = 2
        dgvCorrMatrix.Columns.Insert(0, cboCorrVariables)
        dgvCorrMatrix.Columns(0).HeaderText = "Random Variable"
        dgvCorrMatrix.Columns(1).HeaderText = "Correlated Variable 1"
        dgvCorrMatrix.Columns(2).HeaderText = "Correlated Variable 2"
        dgvCorrMatrix.Rows.Add(3)
        dgvCorrMatrix.AllowUserToAddRows = False
        'dgvCorrMatrix.Rows(0).Cells(0).Value = "Variable Name:" 'ERROR
        dgvCorrMatrix.Rows(0).Cells(0).Style.BackColor = Color.LightGray 'Unused combobox
        dgvCorrMatrix.Rows(0).Cells(0).ReadOnly = True
        dgvCorrMatrix.Rows(0).Cells(1).ReadOnly = True 'The correlated variable 1 name
        dgvCorrMatrix.Rows(0).Cells(2).ReadOnly = True 'The correlated variable 2 name
        dgvCorrMatrix.Rows(1).Cells(1).Style.BackColor = Color.LightGray 'The unit diagonal
        dgvCorrMatrix.Rows(1).Cells(1).Value = 1
        dgvCorrMatrix.Rows(1).Cells(1).ReadOnly = True
        dgvCorrMatrix.Rows(2).Cells(1).ReadOnly = True 'The calculated correlation value
        dgvCorrMatrix.Rows(2).Cells(2).Style.BackColor = Color.LightGray 'The unit diagonal
        dgvCorrMatrix.Rows(2).Cells(2).Value = 1
        dgvCorrMatrix.Rows(2).Cells(2).ReadOnly = True
        dgvCorrMatrix.Rows(1).Cells(2).Style.BackColor = Color.WhiteSmoke 'The calculated correlation value
        dgvCorrMatrix.Rows(1).Cells(2).ReadOnly = True

        txtNCorrVars.Text = "2"
        SetUpCorrMat(2)

        rbTransCorrChol.Checked = True

        dgvCovMatrix.ColumnCount = 2
        dgvCovMatrix.Columns.Insert(0, cboCovVariables)
        dgvCovMatrix.Columns(0).HeaderText = "Random Variable"
        dgvCovMatrix.Columns(1).HeaderText = "Correlated Variable 1"
        dgvCovMatrix.Columns(2).HeaderText = "Correlated Variable 2"
        dgvCovMatrix.Rows.Add(3)
        dgvCovMatrix.AllowUserToAddRows = False
        'dgvCovMatrix.Rows(0).Cells(0).Value = "Variable Name:" 'ERROR
        dgvCovMatrix.Rows(0).Cells(0).Style.BackColor = Color.LightGray 'Unused combobox
        dgvCovMatrix.Rows(0).Cells(0).ReadOnly = True
        dgvCovMatrix.Rows(0).Cells(1).ReadOnly = True 'The correlated variable 1 name
        dgvCovMatrix.Rows(0).Cells(2).ReadOnly = True 'The correlated variable 2 name
        dgvCovMatrix.Rows(1).Cells(1).Style.BackColor = Color.LightGray 'The unit diagonal
        dgvCovMatrix.Rows(1).Cells(1).Value = 1
        dgvCovMatrix.Rows(1).Cells(1).ReadOnly = True
        dgvCovMatrix.Rows(2).Cells(1).ReadOnly = True 'The calculated covariance value
        dgvCovMatrix.Rows(2).Cells(2).Style.BackColor = Color.LightGray 'The unit diagonal
        dgvCovMatrix.Rows(2).Cells(2).Value = 1
        dgvCovMatrix.Rows(2).Cells(2).ReadOnly = True
        dgvCovMatrix.Rows(1).Cells(2).Style.BackColor = Color.WhiteSmoke 'The calculated covariance value
        dgvCovMatrix.Rows(1).Cells(2).ReadOnly = True

        txtNCovVars.Text = "2"
        SetUpCovMat(2)

        rbTransCovChol.Checked = True

        rbSample.Checked = True

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form

        If FormNo > -1 Then
            Main.ClosedFormNo = FormNo 'The Main form property ClosedFormNo is set to this form number. This is used in the DataInfoFormClosed method to select the correct form to set to nothing.
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

    Private Sub frmDataInfo_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If FormNo > -1 Then
            Main.DataInfoClosed()
        End If
    End Sub

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Public Sub ShowDataInfo_OLD()
        'A new data source has been specified.
        'Show the data information.

        If DataSource Is Nothing Then
            Main.Message.AddWarning("The data source is empty." & vbCrLf)
        Else
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
            ShowAllFieldStats()
            'Else
            '    lblTableNo.Text = "0"
            'End If
        End If

    End Sub

    'UPDATE: Just set the TableName property to dipslay the Table Information.
    'Public Sub ShowDataInfo(ByVal SelectTableName As String)
    'Public Sub SelectTable(ByVal SelectTableName As String)
    '    'Show the data information for the table named TableName.
    '    If DataSource Is Nothing Then
    '        Main.Message.AddWarning("The data source is empty." & vbCrLf)
    '    Else
    '        If DataSource.Data.Tables.Contains(TableName) Then
    '            TableName = SelectTableName
    '        Else
    '            Main.Message.AddWarning("A table named " & SelectTableName & " was not found." & vbCrLf)
    '        End If
    '    End If
    'End Sub



    Public Sub ShowAllFieldStats()
        'Show the statistics for all of the fields in the selected table.

        If DataSource.Data.Tables(TableName) Is Nothing Then
            Main.Message.AddWarning("There is no table named " & TableName & vbCrLf)
        Else
            Dim RowNo As Integer
            Dim Var As Double
            cboCorrVariables.Items.Clear()
            cboCovVariables.Items.Clear()
            dgvStatistics.Rows.Clear()

            For Each item In DataSource.Data.Tables(TableName).Columns
                dgvStatistics.Rows.Add()
                RowNo = dgvStatistics.Rows.Count - 1
                dgvStatistics.Rows(RowNo).Cells(0).Value = item.ColumnName
                dgvStatistics.Rows(RowNo).Cells(1).Value = DataSource.Data.Tables(TableName).Compute("Min(" & item.ColumnName & ")", "")
                dgvStatistics.Rows(RowNo).Cells(2).Value = DataSource.Data.Tables(TableName).Compute("Max(" & item.ColumnName & ")", "")
                dgvStatistics.Rows(RowNo).Cells(3).Value = DataSource.Data.Tables(TableName).Compute("Sum(" & item.ColumnName & ")", "")
                dgvStatistics.Rows(RowNo).Cells(4).Value = DataSource.Data.Tables(TableName).Compute("Avg(" & item.ColumnName & ")", "")
                If rbSample.Checked Then
                    Var = Variance(item.ColumnName, True)
                    dgvStatistics.Rows(RowNo).Cells(6).Value = Var
                    dgvStatistics.Rows(RowNo).Cells(5).Value = Math.Sqrt(Var)
                ElseIf rbPopulation.Checked Then
                    Var = Variance(item.ColumnName, False)
                    dgvStatistics.Rows(RowNo).Cells(6).Value = Var
                    dgvStatistics.Rows(RowNo).Cells(5).Value = Math.Sqrt(Var)
                Else
                    dgvStatistics.Rows(RowNo).Cells(5).Value = DataSource.Data.Tables(TableName).Compute("StDev(" & item.ColumnName & ")", "")
                    dgvStatistics.Rows(RowNo).Cells(6).Value = DataSource.Data.Tables(TableName).Compute("Var(" & item.ColumnName & ")", "")
                End If
                'dgvStatistics.Rows(RowNo).Cells(6).Value = Var
                'dgvStatistics.Rows(RowNo).Cells(5).Value = Math.Sqrt(Var)
                'dgvStatistics.Rows(RowNo).Cells(5).Value = DataSource.Data.Tables(TableName).Compute("StDev(" & item.ColumnName & ")", "")
                'dgvStatistics.Rows(RowNo).Cells(6).Value = DataSource.Data.Tables(TableName).Compute("Var(" & item.ColumnName & ")", "")
                cboCorrVariables.Items.Add(item.ColumnName)
                cboCovVariables.Items.Add(item.ColumnName)
            Next

            dgvStatistics.Columns(1).DefaultCellStyle.Format = txtMinFormat.Text
            dgvStatistics.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvStatistics.Columns(2).DefaultCellStyle.Format = txtMaxFormat.Text
            dgvStatistics.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvStatistics.Columns(3).DefaultCellStyle.Format = txtSumFormat.Text
            dgvStatistics.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvStatistics.Columns(4).DefaultCellStyle.Format = txtAvgFormat.Text
            dgvStatistics.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvStatistics.Columns(5).DefaultCellStyle.Format = txtStdDevFormat.Text
            dgvStatistics.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvStatistics.Columns(6).DefaultCellStyle.Format = txtVarFormat.Text
            dgvStatistics.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'dgvStatistics.AutoResizeColumns()
            dgvStatistics.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        End If
    End Sub

    Private Function Variance(ByVal ColumnName As String, ByVal IsSample As Boolean) As Double
        'Calculates the variance of the data in column ColumnName.
        'If IsSample is True, the Sample variance is calculated, else the Population variance is calculated.

        'Dim Mean As Double = DataSource.Data.Tables(TableName).Compute("Avg(" & DataSource.Data.Tables(TableName).Columns(ColumnName) & ")", "")
        Dim Mean As Double = DataSource.Data.Tables(TableName).Compute("Avg(" & ColumnName & ")", "")
        'Dim Var As Double = 0
        Dim DiffSq As Double = 0
        For Each Row As DataRow In DataSource.Data.Tables(TableName).Rows
            'Var += (Row.Item(ColumnName) - Mean) ^ 2
            DiffSq += (Row.Item(ColumnName) - Mean) ^ 2
        Next
        If IsSample Then
            'Var /= NRows - 1
            Return DiffSq / (NRows - 1)
        Else
            'Var /= NRows
            Return DiffSq / NRows
        End If
        'Return Var
    End Function

    Private Function Correlation(ByVal Col1 As String, ByVal Col2 As String) As Double
        'Calculate the correlation coefficient between Col1 and Col2 columns in the table TableName.
        'https://www.mathsisfun.com/data/correlation.html

        If DataSource.Data.Tables(TableName) Is Nothing Then
            Main.Message.AddWarning("There is no table named " & TableName & vbCrLf)
        Else
            If DataSource.Data.Tables(TableName).Columns.Contains(Col1) Then
                If DataSource.Data.Tables(TableName).Columns.Contains(Col2) Then
                    Dim Mean1 As Double = DataSource.Data.Tables(TableName).Compute("Avg(" & Col1 & ")", "") 'Col1 Mean
                    Dim Mean2 As Double = DataSource.Data.Tables(TableName).Compute("Avg(" & Col2 & ")", "") 'Col2 Mean
                    Dim A As Double 'A = Col1 - Mean1
                    Dim B As Double 'B = Col2 - Mean2
                    Dim SumAB As Double = 0 'AB = (Col1 - Mean1) x (Col2 - Mean2)
                    Dim SumAA As Double = 0 'AA = (Col1 - Mean1) Squared
                    Dim SumBB As Double = 0 'BB = (Col2 - Mean2) Squared

                    'For Each rowItem In DataSource.Data.Tables(TableName).Columns
                    For Each Row As DataRow In DataSource.Data.Tables(TableName).Rows
                        A = Row.Item(Col1) - Mean1
                        B = Row.Item(Col2) - Mean2
                        SumAB += A * B
                        SumAA += A * A
                        SumBB += B * B
                    Next
                    Return SumAB / Math.Sqrt(SumAA * SumBB)
                Else
                    Main.Message.AddWarning("There is no column named " & Col2 & " in table " & TableName & vbCrLf)
                End If
            Else
                Main.Message.AddWarning("There is no column named " & Col1 & " in table " & TableName & vbCrLf)
            End If
        End If
    End Function

    Private Sub txtNCorrVars_TextChanged(sender As Object, e As EventArgs) Handles txtNCorrVars.TextChanged

    End Sub

    Private Sub txtNCorrVars_LostFocus(sender As Object, e As EventArgs) Handles txtNCorrVars.LostFocus
        'The number of random variables in the correlation matrix has changed.

        SetUpCorrMat(Val(txtNCorrVars.Text))
    End Sub

    Private Sub SetUpCorrMat(ByVal NVars As Integer)
        'Set up the correlation matrix with the number of random variables = NVars.

        dgvCorrMatrix.ColumnCount = NVars + 1
        dgvCorrMatrix.RowCount = NVars + 1

        Dim I As Integer
        Dim J As Integer
        'Dim CorrMatName As String = txtCorrelationName.Text
        For I = 1 To NVars
            dgvCorrMatrix.Rows(I).Cells(0).Value = "" 'Set the Uncorrelated random variable names to ""
            dgvCorrMatrix.Rows(0).Cells(I).Value = "" 'Set the Correlated random variable names to ""
            dgvCorrMatrix.Columns(I).HeaderText = "Correlated Variable " & I
            dgvCorrMatrix.Rows(I).Cells(I).Value = "1"
            dgvCorrMatrix.Rows(I).Cells(I).ReadOnly = True
            dgvCorrMatrix.Rows(I).Cells(I).Style.BackColor = Color.LightGray
            For J = 1 To I - 1
                dgvCorrMatrix.Rows(I).Cells(J).Value = ""
            Next
            For J = I + 1 To NVars
                dgvCorrMatrix.Rows(I).Cells(J).Value = ""
                dgvCorrMatrix.Rows(I).Cells(J).ReadOnly = True
                dgvCorrMatrix.Rows(I).Cells(J).Style.BackColor = Color.WhiteSmoke
            Next
        Next

        dgvCorrCholeski.ColumnCount = NVars
        dgvCorrCholeski.RowCount = NVars

    End Sub

    Private Sub dgvCorrMatrix_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCorrMatrix.CellEndEdit
        'An item in the Correlation Matrix has been changed.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        If ColNo = 0 Then 'A Random Variable has been selected
            'Copy the Random Variable name to the corresponding location in the top row.
            dgvCorrMatrix.Rows(0).Cells(RowNo).Value = dgvCorrMatrix.Rows(RowNo).Cells(ColNo).Value

        End If
    End Sub

    Private Sub CalculateCorrelations()
        'Calculate the correlation coefficients for the columns selected in the correlation matrix.

        Dim NVars As Integer = Val(txtNCorrVars.Text)
        Dim I As Integer
        Dim J As Integer
        Dim Col1 As String
        Dim Col2 As String

        For I = 1 To NVars - 1
            Col1 = dgvCorrMatrix.Rows(I).Cells(0).Value 'Get the name of the first column
            For J = I + 1 To NVars
                Col2 = dgvCorrMatrix.Rows(J).Cells(0).Value 'Get the name of the second column
                dgvCorrMatrix.Rows(I).Cells(J).Value = Correlation(Col1, Col2)
                dgvCorrMatrix.Rows(J).Cells(I).Value = dgvCorrMatrix.Rows(I).Cells(J).Value
            Next
        Next
    End Sub

    Private Sub btnCalcCorrs_Click(sender As Object, e As EventArgs) Handles btnCalcCorrs.Click
        CalculateCorrelations()
        dgvCorrMatrix.AutoResizeColumns()
        If rbCorrChol.Checked Then
            CholeskiCorrMatrix()
        Else
            TransCholeskiCorrMatrix()
        End If
    End Sub

    Private Sub btnCalcAllCorrs_Click(sender As Object, e As EventArgs) Handles btnCalcAllCorrs.Click
        'Calculate the Correlation between all columns

        If DataSource.Data.Tables.Contains(TableName) Then
            Dim NCols As Integer = DataSource.Data.Tables(TableName).Columns.Count
            txtNCorrVars.Text = NCols
            SetUpCorrMat(NCols)
            Dim I As Integer
            For I = 0 To NCols - 1
                dgvCorrMatrix.Rows(I + 1).Cells(0).Value = DataSource.Data.Tables(TableName).Columns(I).ColumnName
                dgvCorrMatrix.Rows(0).Cells(I + 1).Value = DataSource.Data.Tables(TableName).Columns(I).ColumnName
                dgvCorrMatrix.Columns(I + 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Next
            dgvCorrMatrix.DefaultCellStyle.Format = txtCorrMatFormat.Text
            dgvCorrCholeski.DefaultCellStyle.Format = txtCorrCholFormat.Text
            CalculateCorrelations()
            'dgvCorrMatrix.AutoResizeColumns()
            'dgvCorrMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
            If rbCorrChol.Checked Then
                CholeskiCorrMatrix()
            Else
                TransCholeskiCorrMatrix()
            End If
            'dgvCorrMatrix.AutoResizeColumns()
            dgvCorrMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Else
            Main.Message.AddWarning("A table named " & TableName & " was not found." & vbCrLf)
        End If
    End Sub

    Private Sub CholeskiCorrMatrix()
        'Generate the Choleski Decomposition of the Correlation matrix

        Dim NCorrVars As Integer = txtNCorrVars.Text

        Dim Array(NCorrVars - 1, NCorrVars - 1) As Double

        Dim I As Integer
        Dim J As Integer

        For I = 1 To NCorrVars
            For J = 1 To NCorrVars
                Array(I - 1, J - 1) = Val(dgvCorrMatrix.Rows(I).Cells(J).Value)
            Next
        Next

        Dim CorrMat = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(Array)

        Try
            Dim Chol = CorrMat.Cholesky
            For I = 0 To NCorrVars - 1
                For J = 0 To NCorrVars - 1
                    dgvCorrCholeski.Rows(I).Cells(J).Value = Chol.Factor(I, J)
                Next
            Next
            Label6.Text = "Choleski Decomposition:"
            dgvCorrCholeski.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvCorrCholeski.AutoResizeColumns()
        Catch ex As Exception
            Main.Message.AddWarning("Choleski decomposition error:" & vbCrLf & ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub TransCholeskiCorrMatrix()
        'Generate the Transposed Choleski Decomposition

        Dim NCorrVars As Integer = txtNCorrVars.Text
        Dim Array(NCorrVars - 1, NCorrVars - 1) As Double

        Dim I As Integer
        Dim J As Integer

        For I = 1 To NCorrVars
            For J = 1 To NCorrVars
                Array(I - 1, J - 1) = Val(dgvCorrMatrix.Rows(I).Cells(J).Value)
            Next
        Next

        Dim CorrMat = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(Array)

        Try
            Dim Chol = CorrMat.Cholesky
            For I = 0 To NCorrVars - 1
                For J = 0 To NCorrVars - 1
                    'dgvCholeski.Rows(I).Cells(J).Value = Chol.Factor(I, J)
                    dgvCorrCholeski.Rows(J).Cells(I).Value = Chol.Factor(I, J)
                Next
            Next
            Label6.Text = "Transposed Choleski Decomposition:"
            dgvCorrCholeski.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvCorrCholeski.AutoResizeColumns()
        Catch ex As Exception
            Main.Message.AddWarning("Choleski decomposition error:" & vbCrLf & ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtNCovVars_TextChanged(sender As Object, e As EventArgs) Handles txtNCovVars.TextChanged

    End Sub

    Private Sub txtNCovVars_LostFocus(sender As Object, e As EventArgs) Handles txtNCovVars.LostFocus
        SetUpCovMat(Val(txtNCovVars.Text))
    End Sub

    Private Sub SetUpCovMat(ByVal NVars As Integer)
        'Set up the correlation matrix with the number of random variables = NVars.

        dgvCovMatrix.ColumnCount = NVars + 1
        dgvCovMatrix.RowCount = NVars + 1

        Dim I As Integer
        Dim J As Integer
        'Dim CorrMatName As String = txtCorrelationName.Text
        For I = 1 To NVars
            dgvCovMatrix.Rows(I).Cells(0).Value = "" 'Set the random variable names to ""
            dgvCovMatrix.Rows(0).Cells(I).Value = "" 'Set the Covariance random variable names to ""
            dgvCovMatrix.Columns(I).HeaderText = "Covariance Variable " & I
            dgvCovMatrix.Rows(I).Cells(I).Value = "1"
            dgvCovMatrix.Rows(I).Cells(I).ReadOnly = True
            dgvCovMatrix.Rows(I).Cells(I).Style.BackColor = Color.LightGray
            For J = 1 To I - 1
                dgvCovMatrix.Rows(I).Cells(J).Value = ""
            Next
            For J = I + 1 To NVars
                dgvCovMatrix.Rows(I).Cells(J).Value = ""
                dgvCovMatrix.Rows(I).Cells(J).ReadOnly = True
                dgvCovMatrix.Rows(I).Cells(J).Style.BackColor = Color.WhiteSmoke
            Next
        Next

        dgvCovCholeski.ColumnCount = NVars
        dgvCovCholeski.RowCount = NVars

    End Sub

    Private Sub btnCalcCov_Click(sender As Object, e As EventArgs) Handles btnCalcCov.Click
        CalculateCovariance()
        dgvCovMatrix.AutoResizeColumns()
        If rbCovChol.Checked Then
            CholeskiCovMatrix()
        Else
            TransCholeskiCovMatrix()
        End If
    End Sub

    Private Sub btnCalcAllCov_Click(sender As Object, e As EventArgs) Handles btnCalcAllCov.Click
        'Calculate the Covariance between all columns

        If DataSource.Data.Tables.Contains(TableName) Then
            Dim NCols As Integer = DataSource.Data.Tables(TableName).Columns.Count
            txtNCovVars.Text = NCols
            SetUpCovMat(NCols)
            Dim I As Integer
            For I = 0 To NCols - 1
                dgvCovMatrix.Rows(I + 1).Cells(0).Value = DataSource.Data.Tables(TableName).Columns(I).ColumnName
                dgvCovMatrix.Rows(0).Cells(I + 1).Value = DataSource.Data.Tables(TableName).Columns(I).ColumnName
                dgvCovMatrix.Columns(I + 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Next
            dgvCovMatrix.DefaultCellStyle.Format = txtCovMatFormat.Text
            dgvCovCholeski.DefaultCellStyle.Format = txtCovCholFormat.Text
            CalculateCovariance()

            If rbCovChol.Checked Then
                CholeskiCovMatrix()
            Else
                TransCholeskiCovMatrix()
            End If
            dgvCovMatrix.AutoResizeColumns()
            dgvCovMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader
        Else
            Main.Message.AddWarning("A table named " & TableName & " was not found." & vbCrLf)
        End If
    End Sub

    Private Sub dgvCovMatrix_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCovMatrix.CellEndEdit
        'An item in the Covariance Matrix has been changed.

        Dim RowNo As Integer = e.RowIndex
        Dim ColNo As Integer = e.ColumnIndex

        If ColNo = 0 Then 'A Random Variable has been selected
            'Copy the Random Variable name to the corresponding location in the top row.
            dgvCovMatrix.Rows(0).Cells(RowNo).Value = dgvCovMatrix.Rows(RowNo).Cells(ColNo).Value

        End If
    End Sub

    Private Sub CalculateCovariance()
        'Calculate the covariance between the columns selected in the covariance matrix.

        Dim NVars As Integer = Val(txtNCovVars.Text)
        Dim I As Integer
        Dim J As Integer
        Dim Col1 As String
        Dim Col2 As String

        For I = 1 To NVars - 1
            Col1 = dgvCovMatrix.Rows(I).Cells(0).Value 'Get the name of the first column
            For J = I + 1 To NVars
                Col2 = dgvCovMatrix.Rows(J).Cells(0).Value 'Get the name of the second column
                dgvCovMatrix.Rows(I).Cells(J).Value = Covariance(Col1, Col2)
                dgvCovMatrix.Rows(J).Cells(I).Value = dgvCovMatrix.Rows(I).Cells(J).Value
            Next
        Next
    End Sub

    Private Function Covariance(ByVal Col1 As String, ByVal Col2 As String) As Double
        'Calculate the covariance between Col1 and Col2 columns in the table TableName.


        If DataSource.Data.Tables(TableName) Is Nothing Then
            Main.Message.AddWarning("There is no table named " & TableName & vbCrLf)
            Return 0
        Else
            If DataSource.Data.Tables(TableName).Columns.Contains(Col1) Then
                If DataSource.Data.Tables(TableName).Columns.Contains(Col2) Then
                    Dim Mean1 As Double = DataSource.Data.Tables(TableName).Compute("Avg(" & Col1 & ")", "") 'Col1 Mean
                    Dim Mean2 As Double = DataSource.Data.Tables(TableName).Compute("Avg(" & Col2 & ")", "") 'Col2 Mean
                    Dim A As Double 'A = Col1 - Mean1
                    Dim B As Double 'B = Col2 - Mean2
                    Dim SumAB As Double = 0 'AB = (Col1 - Mean1) x (Col2 - Mean2)

                    'For Each rowItem In DataSource.Data.Tables(TableName).Columns
                    For Each Row As DataRow In DataSource.Data.Tables(TableName).Rows
                        A = Row.Item(Col1) - Mean1
                        B = Row.Item(Col2) - Mean2
                        SumAB += A * B
                    Next
                    'Return SumAB / (DataSource.Data.Tables(TableName).Rows.Count - 1)
                    Return SumAB / (DataSource.Data.Tables(TableName).Rows.Count)
                Else
                    Main.Message.AddWarning("There is no column named " & Col2 & " in table " & TableName & vbCrLf)
                    Return 0
                End If
            Else
                Main.Message.AddWarning("There is no column named " & Col1 & " in table " & TableName & vbCrLf)
                Return 0
            End If
        End If
    End Function

    Private Sub CholeskiCovMatrix()
        'Generate the Choleski Decomposition of the Covariance matrix

        Dim NCovVars As Integer = txtNCovVars.Text

        Dim Array(NCovVars - 1, NCovVars - 1) As Double

        Dim I As Integer
        Dim J As Integer

        For I = 1 To NCovVars
            For J = 1 To NCovVars
                Array(I - 1, J - 1) = Val(dgvCovMatrix.Rows(I).Cells(J).Value)
            Next
        Next

        Dim CovMat = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(Array)

        Try
            Dim Chol = CovMat.Cholesky
            For I = 0 To NCovVars - 1
                For J = 0 To NCovVars - 1
                    dgvCovCholeski.Rows(I).Cells(J).Value = Chol.Factor(I, J)
                Next
            Next
            Label11.Text = "Choleski Decomposition:"
            dgvCovCholeski.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvCovCholeski.AutoResizeColumns()
        Catch ex As Exception
            Main.Message.AddWarning("Choleski decomposition error:" & vbCrLf & ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub TransCholeskiCovMatrix()
        'Generate the Transposed Choleski Decomposition of the Covariance matric

        Dim NCovVars As Integer = txtNCovVars.Text
        Dim Array(NCovVars - 1, NCovVars - 1) As Double

        Dim I As Integer
        Dim J As Integer

        For I = 1 To NCovVars
            For J = 1 To NCovVars
                Array(I - 1, J - 1) = Val(dgvCovMatrix.Rows(I).Cells(J).Value)
            Next
        Next

        Dim CovMat = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(Array)

        Try
            Dim Chol = CovMat.Cholesky
            For I = 0 To NCovVars - 1
                For J = 0 To NCovVars - 1
                    'dgvCholeski.Rows(I).Cells(J).Value = Chol.Factor(I, J)
                    dgvCovCholeski.Rows(J).Cells(I).Value = Chol.Factor(I, J)
                Next
            Next
            Label11.Text = "Transposed Choleski Decomposition:"
            dgvCovCholeski.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            dgvCovCholeski.AutoResizeColumns()
        Catch ex As Exception
            Main.Message.AddWarning("Choleski decomposition error:" & vbCrLf & ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub rbSample_CheckedChanged(sender As Object, e As EventArgs) Handles rbSample.CheckedChanged
        If rbSample.Focused Then
            ShowAllFieldStats()
        End If
    End Sub

    Private Sub rbPopulation_CheckedChanged(sender As Object, e As EventArgs) Handles rbPopulation.CheckedChanged
        If rbPopulation.Focused Then
            ShowAllFieldStats()
        End If
    End Sub

    Private Sub rbBuiltIn_CheckedChanged(sender As Object, e As EventArgs) Handles rbBuiltIn.CheckedChanged
        If rbBuiltIn.Focused Then
            ShowAllFieldStats()
        End If
    End Sub

    Private Sub cmbDataTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDataTable.SelectedIndexChanged
        If cmbDataTable.SelectedIndex > -1 Then
            TableName = cmbDataTable.SelectedItem.ToString
        End If
    End Sub

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs) Handles btnFormatHelp.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub btnFormatHelp1_Click(sender As Object, e As EventArgs) Handles btnFormatHelp1.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub btnFormatHelp2_Click(sender As Object, e As EventArgs) Handles btnFormatHelp2.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub btnFormatHelp3_Click(sender As Object, e As EventArgs) Handles btnFormatHelp3.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub btnFormatHelp4_Click(sender As Object, e As EventArgs) Handles btnFormatHelp4.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub txtMinFormat_LostFocus(sender As Object, e As EventArgs) Handles txtMinFormat.LostFocus
        dgvStatistics.Columns(1).DefaultCellStyle.Format = txtMinFormat.Text
    End Sub

    Private Sub txtMaxFormat_LostFocus(sender As Object, e As EventArgs) Handles txtMaxFormat.LostFocus
        dgvStatistics.Columns(2).DefaultCellStyle.Format = txtMaxFormat.Text
    End Sub

    Private Sub txtSumFormat_LostFocus(sender As Object, e As EventArgs) Handles txtSumFormat.LostFocus
        dgvStatistics.Columns(3).DefaultCellStyle.Format = txtSumFormat.Text
    End Sub

    Private Sub txtAvgFormat_LostFocus(sender As Object, e As EventArgs) Handles txtAvgFormat.LostFocus
        dgvStatistics.Columns(4).DefaultCellStyle.Format = txtAvgFormat.Text
    End Sub

    Private Sub txtStdDevFormat_LostFocus(sender As Object, e As EventArgs) Handles txtStdDevFormat.LostFocus
        dgvStatistics.Columns(5).DefaultCellStyle.Format = txtStdDevFormat.Text
    End Sub

    Private Sub txtVarFormat_LostFocus(sender As Object, e As EventArgs) Handles txtVarFormat.LostFocus
        dgvStatistics.Columns(6).DefaultCellStyle.Format = txtVarFormat.Text
    End Sub

    Private Sub dgvStatistics_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStatistics.CellContentClick

    End Sub

    Private Sub dgvStatistics_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvStatistics.DataError
        If e.Exception.Message = "DataGridViewComboBoxCell value is not valid." Then
            'Ignore the error
        Else
            Main.Message.AddWarning(e.Exception.Message & vbCrLf)
        End If

    End Sub

    Private Sub txtMinFormat_TextChanged(sender As Object, e As EventArgs) Handles txtMinFormat.TextChanged

    End Sub


#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class