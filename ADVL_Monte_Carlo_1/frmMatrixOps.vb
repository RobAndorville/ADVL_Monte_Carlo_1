Public Class frmMatrixOps
    'Form used to test and apply matrix operations.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    'One Matrix Operations:
    Dim InputMatrix As New MatrixInfo  'The Input Matrix on the One Matrix Operations tab.
    Dim OutputMatrix As New MatrixInfo 'The Output Matrix on the One Matrix Operations tab.

    'Two Matrix Operations:
    Dim InputMatrix1 As New MatrixInfo     'The Input1 Matrix on the Two Matrix Operations tab.
    Dim InputMatrix2 As New MatrixInfo     'The Input2 Matrix on the Two Matrix Operations tab.
    Dim OutputCalcMatrix As New MatrixInfo 'The Output Calc Matrix on the Two Matrix Operations tab.

    'Information:
    Dim InfoMatrix As New MatrixInfo 'The Info Matrix on the Information tab.

    Dim WithEvents Zip As ADVL_Utilities_Library_1.ZipComp

    Dim OpInfo As New Dictionary(Of String, MatrixOperationInfo) 'Dictionary of Matrix Operation Information
    Dim ScalarData As New Dictionary(Of String, Double) 'Dictionary of Scalar data
    Dim MatrixData As New Dictionary(Of String, MatrixInfo) 'Dictionary of Matrix data.

    Dim CutNode As TreeNode 'Used for cutting and pasting nodes
    Dim SelNode As TreeNode 'The node selected on trvMatrixOps
    Dim SelItemName As String = "" 'The name of the item selected on trvMatrixOps
    Dim SelDataName As String = "" 'The name of the data corresponding to the item selected. This will be the same as the SelItemName unless it is a Scalar Copy or Matrix Copy node.

    Public WithEvents Matrix As frmMatrix
    'Public MatrixList As New ArrayList 'Used for displaying multiple matrices
    'Public MatrixList As New Dictionary(Of String, frmMatrix) 'Used for displaying multiple matrices
    'Public WithEvents MatrixList As New Dictionary(Of String, frmMatrix) 'Used for displaying multiple matrices
    Public MatrixList As New Dictionary(Of String, frmMatrix) 'Used for displaying multiple matrices

    Public WithEvents Scalar As frmScalar
    Public ScalarList As New Dictionary(Of String, frmScalar) 'Used for displaying multiple scalars

    Public WithEvents Node As frmNode
    Public NodeList As New Dictionary(Of String, frmNode) 'Used for displaying multiple no value nodes

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
                               <InputMatrixFormat><%= txtInputMatrixFormat.Text %></InputMatrixFormat>
                               <OutputMatrixFormat><%= txtOutputMatrixFormat.Text %></OutputMatrixFormat>
                               <InputMatrix1Format><%= txtInputMatrix1Format.Text %></InputMatrix1Format>
                               <InputMatrix2Format><%= txtInputMatrix2Format.Text %></InputMatrix2Format>
                               <OutputCalcMatrixFormat><%= txtOutputCalcMatrixFormat.Text %></OutputCalcMatrixFormat>
                               <MatrixFormat><%= txtMatrixFormat.Text %></MatrixFormat>
                               <SelectedTab><%= TabControl1.SelectedIndex %></SelectedTab>
                               <SelectedSequenceTab><%= TabControl2.SelectedIndex %></SelectedSequenceTab>
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
            If Settings.<FormSettings>.<InputMatrixFormat>.Value <> Nothing Then txtInputMatrixFormat.Text = Settings.<FormSettings>.<InputMatrixFormat>.Value
            If Settings.<FormSettings>.<OutputMatrixFormat>.Value <> Nothing Then txtOutputMatrixFormat.Text = Settings.<FormSettings>.<OutputMatrixFormat>.Value
            If Settings.<FormSettings>.<InputMatrix1Format>.Value <> Nothing Then txtInputMatrix1Format.Text = Settings.<FormSettings>.<InputMatrix1Format>.Value
            If Settings.<FormSettings>.<InputMatrix2Format>.Value <> Nothing Then txtInputMatrix2Format.Text = Settings.<FormSettings>.<InputMatrix2Format>.Value
            If Settings.<FormSettings>.<OutputCalcMatrixFormat>.Value <> Nothing Then txtOutputCalcMatrixFormat.Text = Settings.<FormSettings>.<OutputCalcMatrixFormat>.Value
            If Settings.<FormSettings>.<MatrixFormat>.Value <> Nothing Then txtMatrixFormat.Text = Settings.<FormSettings>.<MatrixFormat>.Value

            If Settings.<FormSettings>.<SelectedTab>.Value <> Nothing Then TabControl1.SelectedIndex = Settings.<FormSettings>.<SelectedTab>.Value
            If Settings.<FormSettings>.<SelectedSequenceTab>.Value <> Nothing Then TabControl2.SelectedIndex = Settings.<FormSettings>.<SelectedSequenceTab>.Value

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

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'RestoreFormSettings()   'Restore the form settings

        dgvInputMatrix.AllowUserToAddRows = False
        dgvOutputMatrix.AllowUserToAddRows = False

        cmbOneMatrixOps.Items.Add("Transpose")
        cmbOneMatrixOps.Items.Add("Inverse")
        cmbOneMatrixOps.Items.Add("Cholesky Factorization")
        cmbOneMatrixOps.Items.Add("Cholesky Factorization Transposed")
        cmbOneMatrixOps.SelectedIndex = 0

        cmbTwoMatrixOp.Items.Add("Multiply")
        cmbTwoMatrixOp.SelectedIndex = 0

        UpdateMatrixList()

        pbIconMatrixPreDefScalar.Image = ImageList1.Images(2)
        pbIconMatrix.Image = ImageList1.Images(4)
        pbIconMatrixOpen.Image = ImageList1.Images(6)
        pbIconMatrixUserDefScalar.Image = ImageList1.Images(8)
        pbIconMatrixUserDef.Image = ImageList1.Images(10)
        pbIconProcess.Image = ImageList1.Images(12)
        pbIconScalarProcess.Image = ImageList1.Images(14)
        pbIconMatrixProcess.Image = ImageList1.Images(16)

        pbIconMatrixTranspose.Image = ImageList1.Images(18)
        pbIconMatrixInverse.Image = ImageList1.Images(20)
        pbIconMatrixCholesky.Image = ImageList1.Images(22)
        pbIconMatrixTransChol.Image = ImageList1.Images(24)
        pbIconMatrixAddScalar.Image = ImageList1.Images(26)
        pbIconMatrixMultScalar.Image = ImageList1.Images(28)
        pbIconMatrixDivScalar.Image = ImageList1.Images(30)
        pbIconCovariance.Image = ImageList1.Images(42)
        pbIconCorrelation.Image = ImageList1.Images(44)

        pbIconMatrixAddMatrix.Image = ImageList1.Images(32)
        pbIconMatrixMultMatrix.Image = ImageList1.Images(34)

        pbIconCollection.Image = ImageList1.Images(36)
        pbIconScalarCopy.Image = ImageList1.Images(38)
        pbIconMatrixCopy.Image = ImageList1.Images(40)

        rbScalar.Checked = True
        rbTranspose.Checked = True
        rbAddMatrix.Checked = True
        rbCollection.Checked = True

        trvMatrixOps.ImageList = ImageList1


        RestoreFormSettings()   'Restore the form settings


    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form

        If FormNo > -1 Then
            Main.ClosedFormNo = FormNo 'The Main form property ClosedFormNo is set to this form number. This is used in the MatrixOpsClosed method to select the correct form to set to nothing.
        End If

        Me.Close() 'Close the form
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()

            'Close all the Matrix windows:
            While MatrixList.Count > 0
                MatrixList(MatrixList.Keys(0)).Close()
            End While

            'Close all the Scalar windows:
            While ScalarList.Count > 0
                ScalarList(ScalarList.Keys(0)).Close()
            End While

            'Close all the (no data) Node windows
            While NodeList.Count > 0
                NodeList(NodeList.Keys(0)).Close()
            End While
        Else
            'Dont save settings if the form is minimised.
        End If
    End Sub

    Private Sub frmMatrixOps_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If FormNo > -1 Then
            'Main.MatrixOpsClosed()
            Main.MatrixOpsClosed()


        End If
    End Sub



#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Private Sub btnNewInputMatrix_Click(sender As Object, e As EventArgs) Handles btnNewInputMatrix.Click
        'Create a new matrix.
        Dim NewFileName As String = txtInputMatrixFileName.Text.Trim

        If NewFileName = "" Then
            'Find an unused default Matrix file name: (Matrix nn)
            Dim I As Integer
            For I = 1 To 32
                If Main.Project.DataFileExists("Matrix " & I & ".Matrix") Then

                Else
                    NewFileName = "Matrix " & I & ".Matrix"
                    txtInputMatrixFileName.Text = NewFileName
                    txtInputMatrixDescr.Text = "Matrix" & I
                    If txtInputMatrixNRows.Text.Trim = "" Then txtInputMatrixNRows.Text = "2" 'Use 2 rows as default
                    If txtInputMatrixNCols.Text.Trim = "" Then txtInputMatrixNCols.Text = "2" 'Use 2 columns as default
                    Exit For
                End If
            Next
            If NewFileName = "" Then
                Main.Message.Add("Please enter a name for the new Matrix." & vbCrLf)
            End If
        Else
            If LCase(NewFileName).EndsWith(".matrix") Then
                NewFileName = IO.Path.GetFileNameWithoutExtension(NewFileName) & ".Matrix"
            ElseIf NewFileName.Contains(".") Then
                Main.Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(NewFileName) & vbCrLf)
                Exit Sub
            Else
                NewFileName = NewFileName & ".Matrix"
            End If
        End If

        If Main.Project.DataFileExists(NewFileName) Then
            'If MessageBox.Show("Overwrite existing file?", "Notice") = DialogResult.OK Then
            If MessageBox.Show("Overwrite existing file?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                'NewFileName if OK to use.
                'Continue with creating a new matrix.
                InputMatrix.Clear()
                InputMatrix.FileName = NewFileName
                InputMatrix.Name = System.IO.Path.GetFileNameWithoutExtension(NewFileName)
                UpdateInputMatrixDisplay()
            Else
                Exit Sub
            End If
        Else 'NewFileName if OK to use.
            InputMatrix.Clear()
            InputMatrix.FileName = NewFileName
            InputMatrix.Name = System.IO.Path.GetFileNameWithoutExtension(NewFileName)
            UpdateInputMatrixDisplay()
        End If
    End Sub

    Private Sub UpdateInputMatrixDisplay()
        'Update the InputMatrix data display.

        txtInputMatrixFileName.Text = InputMatrix.FileName
        txtInputMatrixName.Text = InputMatrix.Name
        txtInputMatrixNRows.Text = InputMatrix.NRows
        txtInputMatrixNCols.Text = InputMatrix.NCols
        txtInputMatrixDescr.Text = InputMatrix.Description
        dgvInputMatrix.Rows.Clear()
        dgvInputMatrix.RowCount = InputMatrix.NRows
        dgvInputMatrix.ColumnCount = InputMatrix.NCols

        Dim RowNo As Integer
        Dim ColNo As Integer
        For RowNo = 0 To InputMatrix.NRows - 1
            For ColNo = 0 To InputMatrix.NCols - 1
                dgvInputMatrix.Rows(RowNo).Cells(ColNo).Value = InputMatrix.Data(RowNo, ColNo)
            Next
        Next

        dgvInputMatrix.DefaultCellStyle.Format = txtInputMatrixFormat.Text

        dgvInputMatrix.AutoResizeColumns()
        dgvInputMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub UpdateOutputMatrixDisplay()
        'Update the OutputMatrix data display.

        txtOutputMatrixFileName.Text = OutputMatrix.FileName
        txtOutputMatrixName.Text = OutputMatrix.Name
        txtOutputMatrixNRows.Text = OutputMatrix.NRows
        txtOutputMatrixNCols.Text = OutputMatrix.NCols
        txtOutputMatrixDescr.Text = OutputMatrix.Description
        dgvOutputMatrix.Rows.Clear()
        dgvOutputMatrix.RowCount = OutputMatrix.NRows
        dgvOutputMatrix.ColumnCount = OutputMatrix.NCols


        Dim RowNo As Integer
        Dim ColNo As Integer
        For RowNo = 0 To OutputMatrix.NRows - 1
            For ColNo = 0 To OutputMatrix.NCols - 1
                dgvOutputMatrix.Rows(RowNo).Cells(ColNo).Value = OutputMatrix.Data(RowNo, ColNo)
            Next
        Next

        dgvOutputMatrix.DefaultCellStyle.Format = txtOutputMatrixFormat.Text

        dgvOutputMatrix.AutoResizeColumns()
        dgvOutputMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub UpdateInputMatrix1Display()
        'Update the InputMatrix1 data display.

        txtInputMatrix1FileName.Text = InputMatrix1.FileName
        txtInputMatrix1Name.Text = InputMatrix1.Name
        txtInputMatrix1NRows.Text = InputMatrix1.NRows
        txtInputMatrix1NCols.Text = InputMatrix1.NCols
        txtInputMatrix1Descr.Text = InputMatrix1.Description
        dgvInputMatrix1.Rows.Clear()
        dgvInputMatrix1.RowCount = InputMatrix1.NRows
        dgvInputMatrix1.ColumnCount = InputMatrix1.NCols

        Dim RowNo As Integer
        Dim ColNo As Integer
        For RowNo = 0 To InputMatrix1.NRows - 1
            For ColNo = 0 To InputMatrix1.NCols - 1
                dgvInputMatrix1.Rows(RowNo).Cells(ColNo).Value = InputMatrix1.Data(RowNo, ColNo)
            Next
        Next

        dgvInputMatrix1.DefaultCellStyle.Format = txtInputMatrix1Format.Text

        dgvInputMatrix1.AutoResizeColumns()
        dgvInputMatrix1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub UpdateInputMatrix2Display()
        'Update the InputMatrix2 data display.

        txtInputMatrix2FileName.Text = InputMatrix2.FileName
        txtInputMatrix2Name.Text = InputMatrix2.Name
        txtInputMatrix2NRows.Text = InputMatrix2.NRows
        txtInputMatrix2NCols.Text = InputMatrix2.NCols
        txtInputMatrix2Descr.Text = InputMatrix2.Description
        dgvInputMatrix2.Rows.Clear()
        dgvInputMatrix2.RowCount = InputMatrix2.NRows
        dgvInputMatrix2.ColumnCount = InputMatrix2.NCols

        Dim RowNo As Integer
        Dim ColNo As Integer
        For RowNo = 0 To InputMatrix2.NRows - 1
            For ColNo = 0 To InputMatrix2.NCols - 1
                dgvInputMatrix2.Rows(RowNo).Cells(ColNo).Value = InputMatrix2.Data(RowNo, ColNo)
            Next
        Next

        dgvInputMatrix2.DefaultCellStyle.Format = txtInputMatrix2Format.Text

        dgvInputMatrix2.AutoResizeColumns()
        dgvInputMatrix2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub UpdateOutputCalcMatrixDisplay()
        'Update the OutputCalcMatrix data display.

        txtOutputCalcMatrixFileName.Text = OutputCalcMatrix.FileName
        txtOutputCalcMatrixName.Text = OutputCalcMatrix.Name
        txtOutputCalcMatrixNRows.Text = OutputCalcMatrix.NRows
        txtOutputCalcMatrixNCols.Text = OutputCalcMatrix.NCols
        txtOutputCalcMatrixDescr.Text = OutputCalcMatrix.Description
        dgvOutputCalcMatrix.Rows.Clear()
        dgvOutputCalcMatrix.RowCount = OutputCalcMatrix.NRows
        dgvOutputCalcMatrix.ColumnCount = OutputCalcMatrix.NCols

        Dim RowNo As Integer
        Dim ColNo As Integer
        For RowNo = 0 To OutputCalcMatrix.NRows - 1
            For ColNo = 0 To OutputCalcMatrix.NCols - 1
                dgvOutputCalcMatrix.Rows(RowNo).Cells(ColNo).Value = OutputCalcMatrix.Data(RowNo, ColNo)
            Next
        Next

        dgvOutputCalcMatrix.DefaultCellStyle.Format = txtOutputCalcMatrixFormat.Text

        dgvOutputCalcMatrix.AutoResizeColumns()
        dgvOutputCalcMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub UpdateInfoMatrixDisplay()
        'Update the OutputCalcMatrix data display.

        txtMatrixFileName.Text = InfoMatrix.FileName
        txtMatrixName.Text = InfoMatrix.Name
        txtMatrixNRows.Text = InfoMatrix.NRows
        txtMatrixNCols.Text = InfoMatrix.NCols
        txtMatrixDescr.Text = InfoMatrix.Description
        dgvMatrix.Rows.Clear()
        dgvMatrix.RowCount = InfoMatrix.NRows
        dgvMatrix.ColumnCount = InfoMatrix.NCols

        Dim RowNo As Integer
        Dim ColNo As Integer
        For RowNo = 0 To InfoMatrix.NRows - 1
            For ColNo = 0 To InfoMatrix.NCols - 1
                dgvMatrix.Rows(RowNo).Cells(ColNo).Value = InfoMatrix.Data(RowNo, ColNo)
            Next
        Next
        dgvMatrix.AutoResizeColumns()
        dgvMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub btnSaveInputMatrix_Click(sender As Object, e As EventArgs) Handles btnSaveInputMatrix.Click
        'Save the Input Matrix.

        If InputMatrix.FileName.Trim = "" Then
            Main.Message.AddWarning("The file name is blank." & vbCrLf)
        ElseIf InputMatrix.FileName.EndsWith(".Matrix") Then
            Main.Project.SaveXmlData(InputMatrix.FileName, InputMatrix.MatrixToXDoc)
        Else
            Main.Message.AddWarning("The file name does not have the '.Matrix' extension." & vbCrLf)
        End If
    End Sub

    Private Sub btnOpenInputMatrix_Click(sender As Object, e As EventArgs) Handles btnOpenInputMatrix.Click
        'Open an Input Matrix.

        Select Case Main.Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                'Select a MonteCarlo file from the project Data directory:
                OpenFileDialog1.InitialDirectory = Main.Project.DataLocn.Path
                OpenFileDialog1.Filter = "Matrix files | *.Matrix"
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    Dim FileName As String = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                    'OpenMCModel(FileName)
                    'txtInputMatrixFileName.Text = FileName
                    InputMatrix.FileName = FileName
                    Dim XDoc As New System.Xml.Linq.XDocument
                    Main.Project.ReadXmlData(FileName, XDoc)
                    InputMatrix.XDocToMatrix(XDoc)
                    UpdateInputMatrixDisplay()
                End If
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                Zip = New ADVL_Utilities_Library_1.ZipComp
                Zip.ArchivePath = Main.Project.DataLocn.Path
                Zip.SelectFile() 'Show the SelectFile form.
                Zip.SelectFileForm.ApplicationName = Main.Project.Application.Name
                Zip.SelectFileForm.SettingsLocn = Main.Project.SettingsLocn
                Zip.SelectFileForm.Show()
                Zip.SelectFileForm.RestoreFormSettings()
                Zip.SelectFileForm.FileExtension = ".Matrix" 'Can also use .FileExtensions = {xxxx, xxxx, xxxx} to specify multiple file types.
                Zip.SelectFileForm.GetFileList()
                If Zip.SelectedFile <> "" Then
                    Dim FileName As String = Zip.SelectedFile
                    InputMatrix.FileName = FileName
                    Dim XDoc As New System.Xml.Linq.XDocument
                    Main.Project.ReadXmlData(FileName, XDoc)
                    InputMatrix.XDocToMatrix(XDoc)
                    UpdateInputMatrixDisplay()
                End If
        End Select
    End Sub

    Private Sub txtInputMatrixNRows_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrixNRows.TextChanged

    End Sub

    Private Sub txtInputMatrixNRows_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrixNRows.LostFocus
        'InputMatrix.NRows = txtInputMatrixNRows.Text
        Dim NewNRows As Integer = txtInputMatrixNRows.Text
        If NewNRows = InputMatrix.NRows Then
            'No change - do not update the display.
        Else
            InputMatrix.NRows = NewNRows
            UpdateInputMatrixDisplay()
        End If
    End Sub

    Private Sub txtInputMatrixNCols_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrixNCols.TextChanged

    End Sub

    Private Sub txtInputMatrixNCols_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrixNCols.LostFocus
        'InputMatrix.NCols = txtInputMatrixNCols.Text
        Dim NewNCols As Integer = txtInputMatrixNCols.Text
        If NewNCols = InputMatrix.NCols Then
            'No change - do not update the display.
        Else
            InputMatrix.NCols = NewNCols
            UpdateInputMatrixDisplay()
        End If
        'UpdateInputMatrixDisplay()
    End Sub

    Private Sub txtInputMatrixFormat_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrixFormat.TextChanged

    End Sub

    Private Sub txtInputMatrixFormat_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrixFormat.LostFocus
        dgvInputMatrix.DefaultCellStyle.Format = txtInputMatrixFormat.Text
    End Sub

    Private Sub txtInputMatrixDescr_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrixDescr.TextChanged

    End Sub

    Private Sub txtInputMatrixDescr_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrixDescr.LostFocus
        InputMatrix.Description = txtInputMatrixDescr.Text
    End Sub

    Private Sub dgvInputMatrix_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInputMatrix.CellEndEdit
        InputMatrix.Data(e.RowIndex, e.ColumnIndex) = dgvInputMatrix.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    End Sub

    Private Sub txtInputMatrixName_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrixName.LostFocus
        'The Input Matrix name has been changed.
        InputMatrix.Name = txtInputMatrixName.Text
    End Sub

    Private Sub txtInputMatrixFileName_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrixFileName.LostFocus
        'The Input Matrix file name has been changed.

        Dim NewFileName As String = txtInputMatrixFileName.Text.Trim

        If LCase(NewFileName).EndsWith(".matrix") Then
            NewFileName = IO.Path.GetFileNameWithoutExtension(NewFileName) & ".Matrix"
        ElseIf NewFileName.Contains(".") Then
            Main.Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(NewFileName) & vbCrLf)
            Exit Sub
        Else
            NewFileName = NewFileName & ".Matrix"
        End If
        txtInputMatrixFileName.Text = NewFileName

        If InputMatrix.FileName = NewFileName Then
            'The FileName is unchanged.
        Else
            If InputMatrix.DataChanged Then
                If MessageBox.Show("Save the Matrix changes in the original file name?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                    Main.Project.SaveXmlData(InputMatrix.FileName, InputMatrix.MatrixToXDoc)
                End If
            End If
            InputMatrix.FileName = NewFileName 'The file name has been updated.
            'InputMatrix.Name = System.IO.Path.GetFileNameWithoutExtension(NewFileName) 'The Matrix name has been updated. THIS IS NOW A SEPARATE ENTRY
        End If
    End Sub

    Private Sub MathNetExamples()
        'MathNet Code Examples.

        Dim Array(0 To 2, 0 To 2) As Double

        '1.0 0.5 0.2
        '0.5 1.0 0.3
        '0.2 0.3 1.0

        Array(0, 0) = 1
        Array(0, 1) = 0.5
        Array(0, 2) = 0.2
        Array(1, 0) = 0.5
        Array(1, 1) = 1
        Array(1, 2) = 0.3
        Array(2, 0) = 0.2
        Array(2, 1) = 0.3
        Array(2, 2) = 1

        Dim TestMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(Array)
        Dim Chol = TestMatrix.Cholesky

    End Sub

    Private Sub btnApplyOneMatrixOp_Click(sender As Object, e As EventArgs) Handles btnApplyOneMatrixOp.Click
        'Apply the selected One Matrix Operation.

        Select Case cmbOneMatrixOps.SelectedItem.ToString
            Case "Transpose"
                OutputMatrix.NCols = InputMatrix.NRows
                OutputMatrix.NRows = InputMatrix.NCols
                Dim RowNo As Integer
                Dim ColNo As Integer
                For RowNo = 0 To OutputMatrix.NRows - 1
                    For ColNo = 0 To OutputMatrix.NCols - 1
                        OutputMatrix.Data(RowNo, ColNo) = InputMatrix.Data(ColNo, RowNo)
                    Next
                Next
                OutputMatrix.FileName = ""
                OutputMatrix.Name = "Transpose: " & InputMatrix.Name
                OutputMatrix.Description = "Transpose of " & InputMatrix.Name
                OutputMatrix.DataChanged = False
                UpdateOutputMatrixDisplay()

            Case "Inverse"
                Dim myMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(InputMatrix.Data)
                Try
                    Dim Inverse = myMatrix.Inverse
                    OutputMatrix.NRows = Inverse.RowCount
                    OutputMatrix.NCols = Inverse.ColumnCount
                    Dim RowNo As Integer
                    Dim ColNo As Integer
                    For RowNo = 0 To OutputMatrix.NRows - 1
                        For ColNo = 0 To OutputMatrix.NCols - 1
                            OutputMatrix.Data(RowNo, ColNo) = Inverse(RowNo, ColNo)
                        Next
                    Next
                    OutputMatrix.FileName = ""
                    OutputMatrix.Name = "Inverse: " & InputMatrix.Name
                    OutputMatrix.Description = "Inverse of " & InputMatrix.Name
                    OutputMatrix.DataChanged = False
                    UpdateOutputMatrixDisplay()
                Catch ex As Exception
                    Main.Message.AddWarning(ex.Message & vbCrLf)
                End Try

            Case "Cholesky Factorization"
                Dim myMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(InputMatrix.Data)
                Try
                    Dim Chol = myMatrix.Cholesky
                    OutputMatrix.NRows = Chol.Factor.RowCount
                    OutputMatrix.NCols = Chol.Factor.ColumnCount
                    Dim RowNo As Integer
                    Dim ColNo As Integer
                    For RowNo = 0 To OutputMatrix.NRows - 1
                        For ColNo = 0 To OutputMatrix.NCols - 1
                            OutputMatrix.Data(RowNo, ColNo) = Chol.Factor(RowNo, ColNo)
                        Next
                    Next
                    OutputMatrix.FileName = ""
                    OutputMatrix.Name = "Cholesky: " & InputMatrix.Name
                    OutputMatrix.Description = "Choleski factorization of " & InputMatrix.Name
                    OutputMatrix.DataChanged = False
                    UpdateOutputMatrixDisplay()
                Catch ex As Exception
                    Main.Message.AddWarning(ex.Message & vbCrLf)
                End Try
            Case "Cholesky Factorization Transposed"
                Dim myMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(InputMatrix.Data)
                Try
                    Dim Chol = myMatrix.Cholesky
                    OutputMatrix.NRows = Chol.Factor.ColumnCount
                    OutputMatrix.NCols = Chol.Factor.RowCount
                    Dim RowNo As Integer
                    Dim ColNo As Integer
                    For RowNo = 0 To OutputMatrix.NRows - 1
                        For ColNo = 0 To OutputMatrix.NCols - 1
                            OutputMatrix.Data(RowNo, ColNo) = Chol.Factor(ColNo, RowNo)
                        Next
                    Next
                    OutputMatrix.FileName = ""
                    OutputMatrix.Name = "Transposed Cholesky: " & InputMatrix.Name
                    OutputMatrix.Description = "Transposed Choleski factorization of " & InputMatrix.Name
                    OutputMatrix.DataChanged = False
                    UpdateOutputMatrixDisplay()
                Catch ex As Exception
                    Main.Message.AddWarning(ex.Message & vbCrLf)
                End Try
            Case Else
                Main.Message.AddWarning("Unknown One Matrix operation: " & cmbOneMatrixOps.SelectedItem.ToString & vbCrLf)
        End Select
    End Sub



    Private Sub btnCopyOutputMatrix_Click(sender As Object, e As EventArgs) Handles btnCopyOutputMatrix.Click
        'Main.ClipboardMatrix = OutputMatrix
        Main.MatrixClipboard.Copy(OutputMatrix)
    End Sub

    Private Sub txtOutputMatrixNCols_TextChanged(sender As Object, e As EventArgs) Handles txtOutputMatrixNCols.TextChanged

    End Sub

    Private Sub txtOutputMatrixFormat_TextChanged(sender As Object, e As EventArgs) Handles txtOutputMatrixFormat.TextChanged

    End Sub

    Private Sub txtOutputMatrixFormat_LostFocus(sender As Object, e As EventArgs) Handles txtOutputMatrixFormat.LostFocus
        dgvOutputMatrix.DefaultCellStyle.Format = txtOutputMatrixFormat.Text
    End Sub

    Private Sub btnCopyInputMatrix_Click(sender As Object, e As EventArgs) Handles btnCopyInputMatrix.Click
        'Main.ClipboardMatrix = InputMatrix 'NOTE: THIS SEEMS TO COPY THE OBJECT REFERENCE NOT THE OBJECT VALUES!!!
        Main.MatrixClipboard.Copy(InputMatrix)
    End Sub

    Private Sub btnPasteInputMatrix_Click(sender As Object, e As EventArgs) Handles btnPasteInputMatrix.Click
        If InputMatrix.DataChanged Then
            If MessageBox.Show("Save the Input Matrix changes in the original file name?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                Main.Project.SaveXmlData(InputMatrix.FileName, InputMatrix.MatrixToXDoc)
            End If
        End If
        'InputMatrix = Main.ClipboardMatrix
        Main.MatrixClipboard.Paste(InputMatrix)
        UpdateInputMatrixDisplay()
    End Sub

    Private Sub btnOpenInputMatrix1_Click(sender As Object, e As EventArgs) Handles btnOpenInputMatrix1.Click
        'Open an Input Matrix1.

        Select Case Main.Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                'Select a MonteCarlo file from the project Data directory:
                OpenFileDialog1.InitialDirectory = Main.Project.DataLocn.Path
                OpenFileDialog1.Filter = "Matrix files | *.Matrix"
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    Dim FileName As String = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                    InputMatrix1.FileName = FileName
                    Dim XDoc As New System.Xml.Linq.XDocument
                    Main.Project.ReadXmlData(FileName, XDoc)
                    InputMatrix1.XDocToMatrix(XDoc)
                    UpdateInputMatrix1Display()
                End If
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                Zip = New ADVL_Utilities_Library_1.ZipComp
                Zip.ArchivePath = Main.Project.DataLocn.Path
                Zip.SelectFile() 'Show the SelectFile form.
                Zip.SelectFileForm.ApplicationName = Main.Project.Application.Name
                Zip.SelectFileForm.SettingsLocn = Main.Project.SettingsLocn
                Zip.SelectFileForm.Show()
                Zip.SelectFileForm.RestoreFormSettings()
                Zip.SelectFileForm.FileExtension = ".Matrix" 'Can also use .FileExtensions = {xxxx, xxxx, xxxx} to specify multiple file types.
                Zip.SelectFileForm.GetFileList()
                If Zip.SelectedFile <> "" Then
                    Dim FileName As String = Zip.SelectedFile
                    InputMatrix1.FileName = FileName
                    Dim XDoc As New System.Xml.Linq.XDocument
                    Main.Project.ReadXmlData(FileName, XDoc)
                    InputMatrix1.XDocToMatrix(XDoc)
                    UpdateInputMatrix1Display()
                End If
        End Select
    End Sub

    Private Sub btnNewInputMatrix1_Click(sender As Object, e As EventArgs) Handles btnNewInputMatrix1.Click
        'Create a new Input Matrix 1.
        Dim NewFileName As String = txtInputMatrix1FileName.Text.Trim

        If NewFileName = "" Then
            'Find an unused default Matrix file name: (Matrix nn)
            Dim I As Integer
            For I = 1 To 32
                If Main.Project.DataFileExists("Matrix " & I & ".Matrix") Then

                Else
                    NewFileName = "Matrix " & I & ".Matrix"
                    txtInputMatrix1FileName.Text = NewFileName
                    txtInputMatrix1Descr.Text = "Matrix" & I
                    If txtInputMatrix1NRows.Text.Trim = "" Then txtInputMatrix1NRows.Text = "2" 'Use 2 rows as default
                    If txtInputMatrix1NCols.Text.Trim = "" Then txtInputMatrix1NCols.Text = "2" 'Use 2 columns as default
                    Exit For
                End If
            Next
            If NewFileName = "" Then
                Main.Message.Add("Please enter a name for the new Matrix." & vbCrLf)
            End If
        Else
            If LCase(NewFileName).EndsWith(".matrix") Then
                NewFileName = IO.Path.GetFileNameWithoutExtension(NewFileName) & ".Matrix"
            ElseIf NewFileName.Contains(".") Then
                Main.Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(NewFileName) & vbCrLf)
                Exit Sub
            Else
                NewFileName = NewFileName & ".Matrix"
            End If
        End If

        If Main.Project.DataFileExists(NewFileName) Then
            If MessageBox.Show("Overwrite existing file?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                'NewFileName is OK to use.
                'Continue with creating a new matrix.
                InputMatrix1.Clear()
                InputMatrix1.FileName = NewFileName
                InputMatrix1.Name = System.IO.Path.GetFileNameWithoutExtension(NewFileName)
                UpdateInputMatrix1Display()
            Else
                Exit Sub
            End If
        Else 'NewFileName if OK to use.
            InputMatrix1.Clear()
            InputMatrix1.FileName = NewFileName
            InputMatrix1.Name = System.IO.Path.GetFileNameWithoutExtension(NewFileName)
            UpdateInputMatrix1Display()
        End If
    End Sub

    Private Sub btnSaveInputMatrix1_Click(sender As Object, e As EventArgs) Handles btnSaveInputMatrix1.Click
        'Save the Input Matrix 1.

        If InputMatrix1.FileName.Trim = "" Then
            Main.Message.AddWarning("The file name is blank." & vbCrLf)
        ElseIf InputMatrix1.FileName.EndsWith(".Matrix") Then
            Main.Project.SaveXmlData(InputMatrix1.FileName, InputMatrix1.MatrixToXDoc)
        Else
            Main.Message.AddWarning("The file name does not have the '.Matrix' extension." & vbCrLf)
        End If
    End Sub

    Private Sub btnCopyInputMatrix1_Click(sender As Object, e As EventArgs) Handles btnCopyInputMatrix1.Click
        Main.MatrixClipboard.Copy(InputMatrix1)
    End Sub

    Private Sub btnPasteInputMatrix1_Click(sender As Object, e As EventArgs) Handles btnPasteInputMatrix1.Click
        If InputMatrix1.DataChanged Then
            If MessageBox.Show("Save the Input Matrix 1 changes in the original file name?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                Main.Project.SaveXmlData(InputMatrix1.FileName, InputMatrix1.MatrixToXDoc)
            End If
        End If
        Main.MatrixClipboard.Paste(InputMatrix1)
        UpdateInputMatrix1Display()
    End Sub

    Private Sub txtInputMatrix1FileName_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix1FileName.TextChanged

    End Sub

    Private Sub txtInputMatrix1FileName_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix1FileName.LostFocus
        'The Input Matrix file name has been changed.

        Dim NewFileName As String = txtInputMatrix1FileName.Text.Trim

        If LCase(NewFileName).EndsWith(".matrix") Then
            NewFileName = IO.Path.GetFileNameWithoutExtension(NewFileName) & ".Matrix"
        ElseIf NewFileName.Contains(".") Then
            Main.Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(NewFileName) & vbCrLf)
            Exit Sub
        Else
            NewFileName = NewFileName & ".Matrix"
        End If
        txtInputMatrix1FileName.Text = NewFileName

        If InputMatrix1.FileName = NewFileName Then
            'The FileName is unchanged.
        Else
            If InputMatrix1.DataChanged Then
                If MessageBox.Show("Save the Matrix changes in the original file name?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                    Main.Project.SaveXmlData(InputMatrix1.FileName, InputMatrix1.MatrixToXDoc)
                End If
            End If
            InputMatrix1.FileName = NewFileName 'The file name has been updated.
        End If
    End Sub

    Private Sub txtInputMatrix1Name_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix1Name.TextChanged

    End Sub

    Private Sub txtInputMatrix1Name_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix1Name.LostFocus
        'The Input Matrix name has been changed.
        InputMatrix1.Name = txtInputMatrix1Name.Text
    End Sub

    Private Sub txtInputMatrix1NRows_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix1NRows.TextChanged

    End Sub

    Private Sub txtInputMatrix1NRows_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix1NRows.LostFocus
        'InputMatrix1.NRows = txtInputMatrix1NRows.Text
        'UpdateInputMatrix1Display()
        Dim NewNRows As Integer = txtInputMatrix1NRows.Text
        If NewNRows = InputMatrix1.NRows Then
            'No change - do not update the display.
        Else
            InputMatrix1.NRows = NewNRows
            UpdateInputMatrix1Display()
        End If

    End Sub

    Private Sub txtInputMatrix1NCols_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix1NCols.TextChanged

    End Sub

    Private Sub txtInputMatrix1NCols_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix1NCols.LostFocus
        'InputMatrix1.NCols = txtInputMatrix1NCols.Text
        'UpdateInputMatrix1Display()
        Dim NewNCols As Integer = txtInputMatrix1NCols.Text
        If NewNCols = InputMatrix1.NCols Then
            'No change - do not update the display.
        Else
            InputMatrix1.NCols = NewNCols
            UpdateInputMatrix1Display()
        End If

    End Sub

    Private Sub txtInputMatrix1Format_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix1Format.TextChanged

    End Sub

    Private Sub txtInputMatrix1Format_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix1Format.LostFocus
        dgvInputMatrix1.DefaultCellStyle.Format = txtInputMatrix1Format.Text
    End Sub

    Private Sub txtInputMatrix1Descr_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix1Descr.TextChanged

    End Sub

    Private Sub txtInputMatrix1Descr_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix1Descr.LostFocus
        InputMatrix1.Description = txtInputMatrix1Descr.Text
    End Sub

    Private Sub dgvInputMatrix1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInputMatrix1.CellContentClick

    End Sub

    Private Sub dgvInputMatrix1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInputMatrix1.CellEndEdit
        InputMatrix1.Data(e.RowIndex, e.ColumnIndex) = dgvInputMatrix1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    End Sub

    Private Sub btnOpenInputMatrix2_Click(sender As Object, e As EventArgs) Handles btnOpenInputMatrix2.Click
        'Open an Input Matrix 2.

        Select Case Main.Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                'Select a MonteCarlo file from the project Data directory:
                OpenFileDialog1.InitialDirectory = Main.Project.DataLocn.Path
                OpenFileDialog1.Filter = "Matrix files | *.Matrix"
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    Dim FileName As String = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                    InputMatrix2.FileName = FileName
                    Dim XDoc As New System.Xml.Linq.XDocument
                    Main.Project.ReadXmlData(FileName, XDoc)
                    InputMatrix2.XDocToMatrix(XDoc)
                    UpdateInputMatrix2Display()
                End If
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                Zip = New ADVL_Utilities_Library_1.ZipComp
                Zip.ArchivePath = Main.Project.DataLocn.Path
                Zip.SelectFile() 'Show the SelectFile form.
                Zip.SelectFileForm.ApplicationName = Main.Project.Application.Name
                Zip.SelectFileForm.SettingsLocn = Main.Project.SettingsLocn
                Zip.SelectFileForm.Show()
                Zip.SelectFileForm.RestoreFormSettings()
                Zip.SelectFileForm.FileExtension = ".Matrix" 'Can also use .FileExtensions = {xxxx, xxxx, xxxx} to specify multiple file types.
                Zip.SelectFileForm.GetFileList()
                If Zip.SelectedFile <> "" Then
                    Dim FileName As String = Zip.SelectedFile
                    InputMatrix2.FileName = FileName
                    Dim XDoc As New System.Xml.Linq.XDocument
                    Main.Project.ReadXmlData(FileName, XDoc)
                    InputMatrix2.XDocToMatrix(XDoc)
                    UpdateInputMatrix2Display()
                End If
        End Select
    End Sub

    Private Sub btnNewInputMatrix2_Click(sender As Object, e As EventArgs) Handles btnNewInputMatrix2.Click
        'Create a new Input Matrix 2.
        Dim NewFileName As String = txtInputMatrix2FileName.Text.Trim

        If NewFileName = "" Then
            'Find an unused default Matrix file name: (Matrix nn)
            Dim I As Integer
            For I = 1 To 32
                If Main.Project.DataFileExists("Matrix " & I & ".Matrix") Then

                Else
                    NewFileName = "Matrix " & I & ".Matrix"
                    txtInputMatrix2FileName.Text = NewFileName
                    txtInputMatrix2Descr.Text = "Matrix" & I
                    If txtInputMatrix2NRows.Text.Trim = "" Then txtInputMatrix2NRows.Text = "2" 'Use 2 rows as default
                    If txtInputMatrix2NCols.Text.Trim = "" Then txtInputMatrix2NCols.Text = "2" 'Use 2 columns as default
                    Exit For
                End If
            Next
            If NewFileName = "" Then
                Main.Message.Add("Please enter a name for the new Matrix." & vbCrLf)
            End If
        Else
            If LCase(NewFileName).EndsWith(".matrix") Then
                NewFileName = IO.Path.GetFileNameWithoutExtension(NewFileName) & ".Matrix"
            ElseIf NewFileName.Contains(".") Then
                Main.Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(NewFileName) & vbCrLf)
                Exit Sub
            Else
                NewFileName = NewFileName & ".Matrix"
            End If
        End If

        If Main.Project.DataFileExists(NewFileName) Then
            If MessageBox.Show("Overwrite existing file?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                'NewFileName is OK to use.
                'Continue with creating a new matrix.
                InputMatrix2.Clear()
                InputMatrix2.FileName = NewFileName
                InputMatrix2.Name = System.IO.Path.GetFileNameWithoutExtension(NewFileName)
                UpdateInputMatrix2Display()
            Else
                Exit Sub
            End If
        Else 'NewFileName if OK to use.
            InputMatrix2.Clear()
            InputMatrix2.FileName = NewFileName
            InputMatrix2.Name = System.IO.Path.GetFileNameWithoutExtension(NewFileName)
            UpdateInputMatrix2Display()
        End If
    End Sub

    Private Sub btnSaveInputMatrix2_Click(sender As Object, e As EventArgs) Handles btnSaveInputMatrix2.Click
        'Save the Input Matrix 2.

        If InputMatrix2.FileName.Trim = "" Then
            Main.Message.AddWarning("The file name is blank." & vbCrLf)
        ElseIf InputMatrix2.FileName.EndsWith(".Matrix") Then
            Main.Project.SaveXmlData(InputMatrix2.FileName, InputMatrix2.MatrixToXDoc)
        Else
            Main.Message.AddWarning("The file name does not have the '.Matrix' extension." & vbCrLf)
        End If
    End Sub

    Private Sub btnCopyInputMatrix2_Click(sender As Object, e As EventArgs) Handles btnCopyInputMatrix2.Click
        Main.MatrixClipboard.Copy(InputMatrix2)
    End Sub

    Private Sub btnPasteInputMatrix2_Click(sender As Object, e As EventArgs) Handles btnPasteInputMatrix2.Click
        If InputMatrix2.DataChanged Then
            If MessageBox.Show("Save the Input Matrix 2 changes in the original file name?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                Main.Project.SaveXmlData(InputMatrix2.FileName, InputMatrix2.MatrixToXDoc)
            End If
        End If
        Main.MatrixClipboard.Paste(InputMatrix2)
        UpdateInputMatrix2Display()
    End Sub

    Private Sub txtInputMatrix2FileName_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix2FileName.TextChanged

    End Sub

    Private Sub txtInputMatrix2FileName_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix2FileName.LostFocus
        'The Input Matrix file name has been changed.

        Dim NewFileName As String = txtInputMatrix2FileName.Text.Trim

        If LCase(NewFileName).EndsWith(".matrix") Then
            NewFileName = IO.Path.GetFileNameWithoutExtension(NewFileName) & ".Matrix"
        ElseIf NewFileName.Contains(".") Then
            Main.Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(NewFileName) & vbCrLf)
            Exit Sub
        Else
            NewFileName = NewFileName & ".Matrix"
        End If
        txtInputMatrix2FileName.Text = NewFileName

        If InputMatrix2.FileName = NewFileName Then
            'The FileName is unchanged.
        Else
            If InputMatrix2.DataChanged Then
                If MessageBox.Show("Save the Matrix changes in the original file name?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                    Main.Project.SaveXmlData(InputMatrix2.FileName, InputMatrix2.MatrixToXDoc)
                End If
            End If
            InputMatrix2.FileName = NewFileName 'The file name has been updated.
        End If
    End Sub

    Private Sub txtInputMatrix2Name_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix2Name.TextChanged

    End Sub

    Private Sub txtInputMatrix2Name_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix2Name.LostFocus
        'The Input Matrix 2 name has been changed.
        InputMatrix2.Name = txtInputMatrix2Name.Text
    End Sub

    Private Sub txtInputMatrix2NRows_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix2NRows.TextChanged

    End Sub

    Private Sub txtInputMatrix2NRows_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix2NRows.LostFocus
        'InputMatrix2.NRows = txtInputMatrix2NRows.Text
        'UpdateInputMatrix2Display()
        Dim NewNRows As Integer = txtInputMatrix2NRows.Text
        If NewNRows = InputMatrix2.NRows Then
            'No change - do not update the display.
        Else
            InputMatrix2.NRows = NewNRows
            UpdateInputMatrix2Display()
        End If

    End Sub

    Private Sub txtInputMatrix2NCols_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix2NCols.TextChanged

    End Sub

    Private Sub txtInputMatrix2NCols_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix2NCols.LostFocus
        'InputMatrix2.NCols = txtInputMatrix2NCols.Text
        'UpdateInputMatrix2Display()
        Dim NewNCols As Integer = txtInputMatrix2NCols.Text
        If NewNCols = InputMatrix2.NCols Then
            'No change - do not update the display.
        Else
            InputMatrix2.NCols = NewNCols
            UpdateInputMatrix2Display()
        End If

    End Sub

    Private Sub txtInputMatrix2Format_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix2Format.TextChanged

    End Sub

    Private Sub txtInputMatrix2Format_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix2Format.LostFocus
        dgvInputMatrix2.DefaultCellStyle.Format = txtInputMatrix2Format.Text
    End Sub

    Private Sub txtInputMatrix2Descr_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrix2Descr.TextChanged

    End Sub

    Private Sub txtInputMatrix2Descr_LostFocus(sender As Object, e As EventArgs) Handles txtInputMatrix2Descr.LostFocus
        InputMatrix2.Description = txtInputMatrix2Descr.Text
    End Sub

    Private Sub dgvInputMatrix2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInputMatrix2.CellContentClick

    End Sub

    Private Sub dgvInputMatrix2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInputMatrix2.CellEndEdit
        InputMatrix2.Data(e.RowIndex, e.ColumnIndex) = dgvInputMatrix2.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    End Sub

    Private Sub btnSaveOutputCalcMatrix_Click(sender As Object, e As EventArgs) Handles btnSaveOutputCalcMatrix.Click
        'Save the Output Calculated Matrix.

        If OutputCalcMatrix.FileName.Trim = "" Then
            Main.Message.AddWarning("The file name is blank." & vbCrLf)
        ElseIf OutputCalcMatrix.FileName.EndsWith(".Matrix") Then
            Main.Project.SaveXmlData(OutputCalcMatrix.FileName, OutputCalcMatrix.MatrixToXDoc)
        Else
            Main.Message.AddWarning("The file name does not have the '.Matrix' extension." & vbCrLf)
        End If
    End Sub

    Private Sub btnCopyOutputCalcMatrix_Click(sender As Object, e As EventArgs) Handles btnCopyOutputCalcMatrix.Click
        Main.MatrixClipboard.Copy(OutputCalcMatrix)
    End Sub

    Private Sub txtOutputCalcMatrixFileName_TextChanged(sender As Object, e As EventArgs) Handles txtOutputCalcMatrixFileName.TextChanged

    End Sub

    Private Sub txtOutputCalcMatrixFileName_LostFocus(sender As Object, e As EventArgs) Handles txtOutputCalcMatrixFileName.LostFocus
        'The Output Calculated Matrix file name has been changed.

        Dim NewFileName As String = txtOutputCalcMatrixFileName.Text.Trim

        If LCase(NewFileName).EndsWith(".matrix") Then
            NewFileName = IO.Path.GetFileNameWithoutExtension(NewFileName) & ".Matrix"
        ElseIf NewFileName.Contains(".") Then
            Main.Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(NewFileName) & vbCrLf)
            Exit Sub
        Else
            NewFileName = NewFileName & ".Matrix"
        End If
        txtOutputCalcMatrixFileName.Text = NewFileName

        If OutputCalcMatrix.FileName = NewFileName Then
            'The FileName is unchanged.
        Else
            If OutputCalcMatrix.DataChanged Then
                If MessageBox.Show("Save the Matrix changes in the original file name?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                    Main.Project.SaveXmlData(OutputCalcMatrix.FileName, OutputCalcMatrix.MatrixToXDoc)
                End If
            End If
            OutputCalcMatrix.FileName = NewFileName 'The file name has been updated.
        End If
    End Sub
    Private Sub txtOutputCalcMatrixName_TextChanged(sender As Object, e As EventArgs) Handles txtOutputCalcMatrixName.TextChanged

    End Sub

    Private Sub txtOutputCalcMatrixName_LostFocus(sender As Object, e As EventArgs) Handles txtOutputCalcMatrixName.LostFocus

    End Sub

    Private Sub txtOutputCalcMatrixNRows_TextChanged(sender As Object, e As EventArgs) Handles txtOutputCalcMatrixNRows.TextChanged

    End Sub

    Private Sub txtOutputCalcmatrixNCols_TextChanged(sender As Object, e As EventArgs) Handles txtOutputCalcMatrixNCols.TextChanged

    End Sub

    Private Sub txtOutputCalcMatrixFormat_TextChanged(sender As Object, e As EventArgs) Handles txtOutputCalcMatrixFormat.TextChanged

    End Sub

    Private Sub txtOutputCalcMatrixFormat_LostFocus(sender As Object, e As EventArgs) Handles txtOutputCalcMatrixFormat.LostFocus
        dgvOutputCalcMatrix.DefaultCellStyle.Format = txtOutputCalcMatrixFormat.Text
    End Sub

    Private Sub txtOutputCalcMatrixDescr_TextChanged(sender As Object, e As EventArgs) Handles txtOutputCalcMatrixDescr.TextChanged

    End Sub

    Private Sub btnSaveOutputMatrix_Click(sender As Object, e As EventArgs) Handles btnSaveOutputMatrix.Click
        'Save the Output Matrix.

        If OutputMatrix.FileName.Trim = "" Then
            Main.Message.AddWarning("The file name is blank." & vbCrLf)
        ElseIf OutputMatrix.FileName.EndsWith(".Matrix") Then
            Main.Project.SaveXmlData(OutputMatrix.FileName, OutputMatrix.MatrixToXDoc)
        Else
            Main.Message.AddWarning("The file name does not have the '.Matrix' extension." & vbCrLf)
        End If

    End Sub

    Private Sub txtOutputMatrixFileName_TextChanged(sender As Object, e As EventArgs) Handles txtOutputMatrixFileName.TextChanged

    End Sub


    Private Sub txtOutputMatrixFileName_LostFocus(sender As Object, e As EventArgs) Handles txtOutputMatrixFileName.LostFocus
        'The Output Matrix file name has been changed.

        Dim NewFileName As String = txtOutputMatrixFileName.Text.Trim

        If LCase(NewFileName).EndsWith(".matrix") Then
            NewFileName = IO.Path.GetFileNameWithoutExtension(NewFileName) & ".Matrix"
        ElseIf NewFileName.Contains(".") Then
            Main.Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(NewFileName) & vbCrLf)
            Exit Sub
        Else
            NewFileName = NewFileName & ".Matrix"
        End If
        txtOutputMatrixFileName.Text = NewFileName

        If OutputMatrix.FileName = NewFileName Then
            'The FileName is unchanged.
        Else
            If OutputMatrix.DataChanged Then
                If MessageBox.Show("Save the Matrix changes in the original file name?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                    Main.Project.SaveXmlData(OutputMatrix.FileName, OutputMatrix.MatrixToXDoc)
                End If
            End If
            OutputMatrix.FileName = NewFileName 'The file name has been updated.
        End If
    End Sub

    Private Sub txtMatrixFormat_TextChanged(sender As Object, e As EventArgs) Handles txtMatrixFormat.TextChanged

    End Sub

    Private Sub txtMatrixFormat_LostFocus(sender As Object, e As EventArgs) Handles txtMatrixFormat.LostFocus
        dgvMatrix.DefaultCellStyle.Format = txtMatrixFormat.Text
    End Sub

    Private Sub dgvInputMatrix_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInputMatrix.CellContentClick

    End Sub

    Private Sub UpdateMatrixList()
        'Update the list of Matrices.

        lstMatrices.Items.Clear()
        If Main.Project.DataLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Main.Project.DataLocn.Path, FileIO.SearchOption.SearchTopLevelOnly, "*.Matrix")
                lstMatrices.Items.Add(IO.Path.GetFileName(foundFile))
            Next
        Else
            'To Do: Get list from Archive.

        End If

    End Sub

    Private Sub btnUpdateList_Click(sender As Object, e As EventArgs) Handles btnUpdateList.Click
        UpdateMatrixList()
    End Sub

    Private Sub lstMatrices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMatrices.SelectedIndexChanged
        'A Matrix file has been selected.
        Dim FileName As String
        FileName = lstMatrices.SelectedItem.ToString
        If FileName = "" Then
            txtSelMatrixName.Text = ""
            txtSelMatrixNRows.Text = ""
            txtSelMatrixNCols.Text = ""
            txtSelMatrixDescr.Text = ""
        Else
            txtSelMatrixName.Text = FileName
            Dim XDoc As New System.Xml.Linq.XDocument
            Main.Project.ReadXmlData(FileName, XDoc)
            txtSelMatrixName.Text = XDoc.<Matrix>.<Name>.Value
            txtSelMatrixNRows.Text = XDoc.<Matrix>.<NRows>.Value
            txtSelMatrixNCols.Text = XDoc.<Matrix>.<NCols>.Value
            txtSelMatrixDescr.Text = XDoc.<Matrix>.<Description>.Value
        End If
    End Sub

    Private Sub btnCopyMatrix_Click(sender As Object, e As EventArgs) Handles btnCopyMatrix.Click
        'Copy the selected matrix to the MatrixClipboard.
        Dim FileName As String
        FileName = lstMatrices.SelectedItem.ToString
        If FileName = "" Then
            Main.Message.AddWarning("The selected matrix file name is blank." & vbCrLf)
        Else
            Dim XDoc As New System.Xml.Linq.XDocument
            Main.Project.ReadXmlData(FileName, XDoc)
            Main.MatrixClipboard.CBMatrix.XDocToMatrix(XDoc) 'Load the Matrix XDoc directly into the Matrix Clipboard.
        End If
    End Sub

    Private Sub btnOpenMatrix_Click(sender As Object, e As EventArgs) Handles btnOpenMatrix.Click
        'Open the selected Matrix in the Information Tab.
        If lstMatrices.SelectedIndex = -1 Then
            Main.Message.AddWarning("Please select a matrix to open." & vbCrLf)
        Else
            Dim FileName As String = lstMatrices.SelectedItem.ToString
            If FileName = "" Then
                Main.Message.AddWarning("The selected matrix file name is blank." & vbCrLf)
            Else
                InfoMatrix.FileName = FileName
                Dim XDoc As New System.Xml.Linq.XDocument
                Main.Project.ReadXmlData(FileName, XDoc)
                InfoMatrix.XDocToMatrix(XDoc)
                UpdateInfoMatrixDisplay()

                'Show the matrix properties:
                Dim myMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(InfoMatrix.Data)
                txtRank.Text = myMatrix.Rank
                txtDeterminant.Text = myMatrix.Determinant
                If myMatrix.IsHermitian Then txtHermitian.Text = "Yes" Else txtHermitian.Text = "No"
                If myMatrix.IsSymmetric Then txtSymmetric.Text = "Yes" Else txtSymmetric.Text = "No"
            End If
        End If
    End Sub

    Private Sub btnDeleteMatrix_Click(sender As Object, e As EventArgs) Handles btnDeleteMatrix.Click
        'Delete the selected Matrix.
        Dim FileName As String = lstMatrices.SelectedItem.ToString
        If FileName = "" Then
            Main.Message.AddWarning("The selected matrix file name is blank." & vbCrLf)
        Else
            If MessageBox.Show("Delete Matrix file " & FileName & "?", "Notice", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                Main.Project.DeleteData(FileName)
                UpdateMatrixList()
            End If
        End If

    End Sub

    Private Sub btnApplyTwoMatrixOp_Click(sender As Object, e As EventArgs) Handles btnApplyTwoMatrixOp.Click
        'Apply the selected Two Matrix Operation.

        Select Case cmbTwoMatrixOp.SelectedItem.ToString
            Case "Multiply"
                Dim myMatrix1 = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(InputMatrix1.Data) 'Create a new dense matrix as a copy of the given two-dimensional array.
                Dim myMatrix2 = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(InputMatrix2.Data)
                Dim M1xM2 = myMatrix1.Multiply(myMatrix2)
                OutputCalcMatrix.NRows = M1xM2.RowCount
                OutputCalcMatrix.NCols = M1xM2.ColumnCount
                Dim RowNo As Integer
                Dim ColNo As Integer
                For RowNo = 0 To OutputCalcMatrix.NRows - 1
                    For ColNo = 0 To OutputCalcMatrix.NCols - 1
                        OutputCalcMatrix.Data(RowNo, ColNo) = M1xM2(RowNo, ColNo)
                    Next
                Next
                OutputCalcMatrix.FileName = ""
                OutputCalcMatrix.Name = "Matrix Multiplication"
                OutputCalcMatrix.Description = "Matrix Multiplication of " & InputMatrix1.Name & " and " & InputMatrix2.Name
                OutputCalcMatrix.DataChanged = False
                UpdateOutputCalcMatrixDisplay()


                'myMatrix1.

            Case Else
                Main.Message.AddWarning("Unknown Two Matrix operation: " & cmbTwoMatrixOp.SelectedItem.ToString & vbCrLf)
        End Select
    End Sub

    Private Sub txtInputMatrixName_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrixName.TextChanged

    End Sub

    Private Sub txtInputMatrixFileName_TextChanged(sender As Object, e As EventArgs) Handles txtInputMatrixFileName.TextChanged

    End Sub

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs) Handles btnFormatHelp.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub txtMatrixNCols_TextChanged(sender As Object, e As EventArgs) Handles txtMatrixNCols.TextChanged

    End Sub

    Private Sub txtMatrixFileName_TextChanged(sender As Object, e As EventArgs) Handles txtMatrixFileName.TextChanged

    End Sub

    Private Sub txtOutputMatrixNRows_TextChanged(sender As Object, e As EventArgs) Handles txtOutputMatrixNRows.TextChanged

    End Sub

    Private Sub txtOutputMatrixNRows_LostFocus(sender As Object, e As EventArgs) Handles txtOutputMatrixNRows.LostFocus

    End Sub



    Private Sub btnNewSeq_Click(sender As Object, e As EventArgs) Handles btnNewSeq.Click
        'Crerate a new Matrix Operation Sequence.


        'Dim FileName As String = txtSeqFileName.Text.Trim.Replace(" ", "_") 'Trim leading and trailing spaces from the entered file name and replace any internal spaces with "_".
        Dim FileName As String = txtSeqFileName.Text.Trim 'Trim leading and trailing spaces from the entered file.

        'Check if a file name has been specified:
        If FileName = "" Then
            Main.Message.AddWarning("Please enter a file name." & vbCrLf)
            Exit Sub
        End If

        'Check the fine name extension:
        If LCase(FileName).EndsWith(".matrixopseq") Then
            FileName = IO.Path.GetFileNameWithoutExtension(FileName) & ".MatrixOpSeq"
            txtSeqFileName.Text = FileName
        ElseIf FileName.Contains(".") Then
            Main.Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(FileName) & vbCrLf)
            Exit Sub
        Else
            FileName = FileName & ".MatrixOpSeq"
            txtSeqFileName.Text = FileName
        End If

        'Check if the new file already exists:
        If Main.Project.DataFileExists(FileName) = True Then
            Main.Message.AddWarning("The Matrix Operation Sequence file already exists: " & FileName & vbCrLf)
            Exit Sub
        End If

        'Check the sequence name. Get the sequence name from the file name if necessary.
        Dim SeqName As String = txtSeqName.Text.Trim
        If SeqName = "" Then
            'SeqName = IO.Path.GetFileNameWithoutExtension(FileName).Replace("_", " ")
            SeqName = IO.Path.GetFileNameWithoutExtension(FileName) 'The Sequence Name can have spaces.
            txtSeqName.Text = SeqName
        End If


        OpInfo.Clear()
        ScalarData.Clear()
        MatrixData.Clear()

        OpInfo.Add(SeqName, New MatrixOperationInfo)
        OpInfo(SeqName).Description = txtSeqDescr.Text
        OpInfo(SeqName).Type = "Matrix Operation Sequence"

        trvMatrixOps.Nodes.Clear() 'Clear the nodes in the Matrix Operations tree.
        'Dim Node1 As TreeNode = New TreeNode(SeqName, 16, 16)
        'Dim Node1 As TreeNode = New TreeNode(SeqName, 16, 18)
        Dim Node1 As TreeNode = New TreeNode(SeqName, 0, 1)
        Node1.Name = SeqName
        trvMatrixOps.Nodes.Add(Node1)

    End Sub

    Private Sub pbIconMatrixPreDefScalar_Click(sender As Object, e As EventArgs) Handles pbIconMatrixPreDefScalar.Click
        rbScalar.Checked = True
    End Sub

    Private Sub pbIconMatrixTransChol_Click(sender As Object, e As EventArgs) Handles pbIconMatrixTransChol.Click
        rbMatrix.Checked = True
    End Sub

    Private Sub pbIconMatrixOpen_Click(sender As Object, e As EventArgs) Handles pbIconMatrixOpen.Click
        rbOpenMatrixFile.Checked = True
    End Sub

    Private Sub pbIconMatrixUserDefScalar_Click(sender As Object, e As EventArgs) Handles pbIconMatrixUserDefScalar.Click
        rbUserDefScalar.Checked = True
    End Sub

    Private Sub pbIconMatrixUserDef_Click(sender As Object, e As EventArgs) Handles pbIconMatrixUserDef.Click
        rbUserDefMatrix.Checked = True
    End Sub

    Private Sub pbIconProcess_Click(sender As Object, e As EventArgs) Handles pbIconProcess.Click
        rbProcess.Checked = True
    End Sub

    Private Sub pbIconScalarProcess_Click(sender As Object, e As EventArgs) Handles pbIconScalarProcess.Click
        rbScalarProcess.Checked = True
    End Sub

    Private Sub pbIconMatrixProcess_Click(sender As Object, e As EventArgs) Handles pbIconMatrixProcess.Click
        rbMatrixProcess.Checked = True
    End Sub

    Private Sub pbIconMatrixPreDefScalar_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixPreDefScalar.MouseMove
        txtNodeInfo.Text = "Scalar node. " & vbCrLf & "Contains a single scalar value."
    End Sub

    Private Sub pbIconMatrix_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrix.MouseMove
        txtNodeInfo.Text = "Matrix node. " & vbCrLf & "Contains a single matrix."
    End Sub

    Private Sub pbIconMatrixOpen_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixOpen.MouseMove
        txtNodeInfo.Text = "Open Matrix File node. " & vbCrLf & "When the sequence is run the Matrix is selected using an Open Matrix file dialog."
    End Sub

    Private Sub pbIconMatrixUserDefScalar_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixUserDefScalar.MouseMove
        txtNodeInfo.Text = "User Defined Scalar node. " & vbCrLf & "Contains a single scalar value. The value is defined by the user."
    End Sub

    Private Sub pbIconMatrixUserDef_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixUserDef.MouseMove
        txtNodeInfo.Text = "User Defined Matrix node. " & vbCrLf & "Contains a Matrix. The Matrix is defined by the user."
    End Sub

    Private Sub pbIconProcess_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconProcess.MouseMove
        txtNodeInfo.Text = "Process node. " & vbCrLf & "Contains no data. The child nodes define a matrix process."
    End Sub

    Private Sub pbIconScalarProcess_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconScalarProcess.MouseMove
        txtNodeInfo.Text = "Scalar Process node. " & vbCrLf & "Contains a Scalar. The child nodes define a scalar process."
    End Sub

    'Private Sub rbMatrixProcess_MouseMove(sender As Object, e As MouseEventArgs) Handles rbMatrixProcess.MouseMove
    '    txtNodeInfo.Text = "Matrix Process node. " & vbCrLf & "Contains a Matrix. The child nodes define a matrix process."
    'End Sub

    Private Sub pbIconMatrixProcess_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixProcess.MouseMove
        txtNodeInfo.Text = "Matrix Process node. " & vbCrLf & "Contains a Matrix. The child nodes define a matrix process."
    End Sub

    Private Sub pbIconMatrixTranspose_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixTranspose.MouseMove
        txtNodeInfo.Text = "Matrix Transpose node. " & vbCrLf & "Contains a Matrix. The matrix is the transpose of the child matrix."
    End Sub

    Private Sub pbIconMatrixInverse_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixInverse.MouseMove
        txtNodeInfo.Text = "Matrix Inverse node. " & vbCrLf & "Contains a Matrix. The matrix is the inverse of the child matrix."
    End Sub

    Private Sub pbIconMatrixCholesky_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixCholesky.MouseMove
        txtNodeInfo.Text = "Cholesky Factorization node. " & vbCrLf & "Contains a Matrix. The matrix is the Cholesky factorization of the child matrix."
    End Sub

    Private Sub pbIconMatrixTransChol_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixTransChol.MouseMove
        txtNodeInfo.Text = "Transposed Cholesky Factorization node. " & vbCrLf & "Contains a Matrix. The matrix is the Transposed Cholesky factorization of the child matrix."
    End Sub

    Private Sub pbIconMatrixAddScalar_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixAddScalar.MouseMove
        txtNodeInfo.Text = "Matrix Add Scalar node. " & vbCrLf & "Contains a Matrix. The matrix is addition of the scalar child node to the matrix child node."
    End Sub

    Private Sub pbIconMatrixMultScalar_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixMultScalar.MouseMove
        txtNodeInfo.Text = "Matrix Multiply Scalar node. " & vbCrLf & "Contains a Matrix. The matrix is multiplcation of the matrix child node with the scalar child node."
    End Sub

    Private Sub pbIconMatrixDivScalar_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixDivScalar.MouseMove
        txtNodeInfo.Text = "Matrix Divide Scalar node. " & vbCrLf & "Contains a Matrix. The matrix is division of the matrix child node by the scalar child node."
    End Sub

    Private Sub pbIconMatrixAddMatrix_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixAddMatrix.MouseMove
        txtNodeInfo.Text = "Matrix Add Matrix node. " & vbCrLf & "Contains a Matrix. The matrix is addition of the matrix child nodes."
    End Sub

    Private Sub pbIconMatrixMultMatrix_MouseMove(sender As Object, e As MouseEventArgs) Handles pbIconMatrixMultMatrix.MouseMove
        txtNodeInfo.Text = "Matrix Multiply Matrix node. " & vbCrLf & "Contains a Matrix. The matrix is multiplication of the matrix child nodes."
    End Sub

    Private Sub rbScalar_Click(sender As Object, e As EventArgs) Handles rbScalar.Click
        txtNodeInfo.Text = "Scalar node. " & vbCrLf & "Contains a single scalar value."
    End Sub

    Private Sub rbMatrix_Click(sender As Object, e As EventArgs) Handles rbMatrix.Click
        txtNodeInfo.Text = "Matrix node. " & vbCrLf & "Contains a single matrix."
    End Sub

    Private Sub rbOpenMatrixFile_Click(sender As Object, e As EventArgs) Handles rbOpenMatrixFile.Click
        txtNodeInfo.Text = "Open Matrix File node. " & vbCrLf & "When the sequence is run the Matrix is selected using an Open Matrix file dialog."
    End Sub

    Private Sub rbUserDefScalar_Click(sender As Object, e As EventArgs) Handles rbUserDefScalar.Click
        txtNodeInfo.Text = "User Defined Scalar node. " & vbCrLf & "Contains a single scalar value. The value is defined by the user."
    End Sub

    Private Sub rbUserDefMatrix_Click(sender As Object, e As EventArgs) Handles rbUserDefMatrix.Click
        txtNodeInfo.Text = "User Defined Matrix node. " & vbCrLf & "Contains a Matrix. The Matrix is defined by the user."
    End Sub

    Private Sub rbProcess_Click(sender As Object, e As EventArgs) Handles rbProcess.Click
        txtNodeInfo.Text = "Process node. " & vbCrLf & "Contains no data. The child nodes define a matrix process."
    End Sub

    Private Sub rbScalarProcess_Click(sender As Object, e As EventArgs) Handles rbScalarProcess.Click
        txtNodeInfo.Text = "Scalar Process node. " & vbCrLf & "Contains a Scalar. The child nodes define a scalar process."
    End Sub

    Private Sub rbMatrixProcess_Click(sender As Object, e As EventArgs) Handles rbMatrixProcess.Click
        txtNodeInfo.Text = "Matrix Process node. " & vbCrLf & "Contains a Matrix. The child nodes define a matrix process."
    End Sub

    Private Sub rbTranspose_Click(sender As Object, e As EventArgs) Handles rbTranspose.Click
        txtNodeInfo.Text = "Matrix Transpose node. " & vbCrLf & "Contains a Matrix. The matrix is the transpose of the child matrix."
    End Sub

    Private Sub rbInverse_Click(sender As Object, e As EventArgs) Handles rbInverse.Click
        txtNodeInfo.Text = "Matrix Inverse node. " & vbCrLf & "Contains a Matrix. The matrix is the inverse of the child matrix."
    End Sub

    Private Sub rbCholesky_Click(sender As Object, e As EventArgs) Handles rbCholesky.Click
        txtNodeInfo.Text = "Cholesky Factorization node. " & vbCrLf & "Contains a Matrix. The matrix is the Cholesky factorization of the child matrix."
    End Sub

    Private Sub rbTransCholesky_Click(sender As Object, e As EventArgs) Handles rbTransCholesky.Click
        txtNodeInfo.Text = "Transposed Cholesky Factorization node. " & vbCrLf & "Contains a Matrix. The matrix is the Transposed Cholesky factorization of the child matrix."
    End Sub

    Private Sub rbAddScalar_Click(sender As Object, e As EventArgs) Handles rbAddScalar.Click
        txtNodeInfo.Text = "Matrix Add Scalar node. " & vbCrLf & "Contains a Matrix. The matrix is addition of the scalar child node to the matrix child node."
    End Sub

    Private Sub rbMultScalar_Click(sender As Object, e As EventArgs) Handles rbMultScalar.Click
        txtNodeInfo.Text = "Matrix Multiply Scalar node. " & vbCrLf & "Contains a Matrix. The matrix is multiplcation of the matrix child node with the scalar child node."
    End Sub

    Private Sub rbDivScalar_Click(sender As Object, e As EventArgs) Handles rbDivScalar.Click
        txtNodeInfo.Text = "Matrix Divide Scalar node. " & vbCrLf & "Contains a Matrix. The matrix is division of the matrix child node by the scalar child node."
    End Sub

    Private Sub rbAddMatrix_Click(sender As Object, e As EventArgs) Handles rbAddMatrix.Click
        txtNodeInfo.Text = "Matrix Add Matrix node. " & vbCrLf & "Contains a Matrix. The matrix is addition of the matrix child nodes."
    End Sub

    Private Sub rbMultMatrix_Click(sender As Object, e As EventArgs) Handles rbMultMatrix.Click
        txtNodeInfo.Text = "Matrix Multiply Matrix node. " & vbCrLf & "Contains a Matrix. The matrix is multiplication of the matrix child nodes."
    End Sub

    Private Sub btnAppendData_Click(sender As Object, e As EventArgs) Handles btnAppendData.Click
        'Add a data node:
        Dim DataName As String = txtDataName.Text.Trim
        Dim DataDescr As String = txtDataDescr.Text.Trim

        If DataName = "" Then
            Main.Message.AddWarning("Please enter a name for the Data node." & vbCrLf)
        Else
            If OpInfo.ContainsKey(DataName) Then
                Main.Message.AddWarning("The node name is already used: " & DataName & vbCrLf)
            Else
                If rbScalar.Checked Then 'Add a Scalar node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Scalar"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 2, 3)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(DataName, DataName, 2, 3)
                    End If
                    'ScalarData.Add(DataName, 0)
                    ScalarData.Add(DataName, 1)

                ElseIf rbMatrix.Checked Then 'Add a Matrix node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Matrix"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 4, 5)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(DataName, DataName, 4, 5)
                    End If
                    MatrixData.Add(DataName, New MatrixInfo)
                    MatrixData(DataName).Name = DataName
                    MatrixData(DataName).Description = DataDescr

                ElseIf rbOpenMatrixFile.Checked Then 'Add an Open Matrix File node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Open Matrix File"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 6, 7)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(DataName, DataName, 6, 7)
                    End If
                    MatrixData.Add(DataName, New MatrixInfo)
                    MatrixData(DataName).Name = DataName
                    MatrixData(DataName).Description = DataDescr

                ElseIf rbUserDefScalar.Checked Then 'Add a User Defined Scalar node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "User Defined Scalar"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 8, 9)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(DataName, DataName, 8, 9)
                    End If
                    'ScalarData.Add(DataName, 0)
                    ScalarData.Add(DataName, 1)

                ElseIf rbUserDefMatrix.Checked Then 'Add a User Defined Matrix node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "User Defined Matrix"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 10, 11)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(DataName, DataName, 10, 11)
                    End If
                    MatrixData.Add(DataName, New MatrixInfo)
                    MatrixData(DataName).Name = DataName
                    MatrixData(DataName).Description = DataDescr

                ElseIf rbProcess.Checked Then 'Add a Process node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Process"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 12, 13)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(DataName, DataName, 12, 13)
                    End If

                ElseIf rbScalarProcess.Checked Then 'Add a Scalar Process node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Scalar Process"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 14, 15)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(DataName, DataName, 14, 15)
                    End If
                    'ScalarData.Add(DataName, 0)
                    ScalarData.Add(DataName, 1)

                ElseIf rbMatrixProcess.Checked Then 'Add a Matrix Process node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Matrix Process"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 16, 17)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(DataName, DataName, 16, 17)
                    End If
                    MatrixData.Add(DataName, New MatrixInfo)
                    MatrixData(DataName).Name = DataName
                    MatrixData(DataName).Description = DataDescr

                Else
                    Main.Message.AddWarning("No data type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnInsertData_Click(sender As Object, e As EventArgs) Handles btnInsertData.Click
        'Insert a data node:
        Dim DataName As String = txtDataName.Text.Trim
        Dim DataDescr As String = txtDataDescr.Text.Trim

        If DataName = "" Then
            Main.Message.AddWarning("Please enter a name for the Data node." & vbCrLf)
        Else
            If OpInfo.ContainsKey(DataName) Then
                Main.Message.AddWarning("The node name is already used: " & DataName & vbCrLf)
            Else
                If rbScalar.Checked Then 'Insert a Scalar node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Scalar"
                    ScalarData.Add(DataName, 1)
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 2, 3)
                    Else
                        'trvMatrixOps.SelectedNode.Nodes.Add(DataName, DataName, 3, 3)
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        'trvMatrixOps.SelectedNode.Parent.Nodes.Add(DataName, DataName, 2, 3) 'Add the new node to the parent of the selected node.
                        'SelNode.Parent = trvMatrixOps.Nodes.Find(DataName, True) 'Parent is ReadOnly
                        'Dim NewNode As TreeNode = trvMatrixOps.Nodes.Find(DataName, True)(0) 'Select the node that was just inserted
                        'Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Add(DataName, DataName, 2, 3) 'Add the new node to the parent of the selected node.
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, DataName, DataName, 2, 3) 'Add the new node the the parent of the selected node at the same index position.

                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbMatrix.Checked Then 'Insert a Matrix node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Matrix"
                    MatrixData.Add(DataName, New MatrixInfo)
                    MatrixData(DataName).Name = DataName
                    MatrixData(DataName).Description = DataDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 4, 5)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, DataName, DataName, 4, 5) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbOpenMatrixFile.Checked Then 'Insert an Open Matrix File node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Open Matrix File"
                    MatrixData.Add(DataName, New MatrixInfo)
                    MatrixData(DataName).Name = DataName
                    MatrixData(DataName).Description = DataDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 6, 7)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, DataName, DataName, 6, 7) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbUserDefScalar.Checked Then 'Insert a User Defined Scalar node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "User Defined Scalar"
                    ScalarData.Add(DataName, 1)
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 8, 9)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, DataName, DataName, 8, 9) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbUserDefMatrix.Checked Then 'Insert a User Defined Matrix node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "User Defined Matrix"
                    MatrixData.Add(DataName, New MatrixInfo)
                    MatrixData(DataName).Name = DataName
                    MatrixData(DataName).Description = DataDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 10, 11)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, DataName, DataName, 10, 11) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbProcess.Checked Then 'Insert a Process node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Process"
                    'MatrixData.Add(DataName, New MatrixInfo)
                    'MatrixData(DataName).Name = DataName
                    'MatrixData(DataName).Description = DataDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 12, 13)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, DataName, DataName, 12, 13) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbScalarProcess.Checked Then 'Insert a Scalar Process node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Scalar Process"
                    ScalarData.Add(DataName, 1)
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 14, 15)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, DataName, DataName, 14, 15) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbMatrixProcess.Checked Then 'Insert a Matrix Process node
                    OpInfo.Add(DataName, New MatrixOperationInfo)
                    OpInfo(DataName).Description = DataDescr
                    OpInfo(DataName).Type = "Matrix Process"
                    MatrixData.Add(DataName, New MatrixInfo)
                    MatrixData(DataName).Name = DataName
                    MatrixData(DataName).Description = DataDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(DataName, DataName, 16, 17)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, DataName, DataName, 16, 17) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                Else
                    Main.Message.AddWarning("No data type has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnReplaceData_Click(sender As Object, e As EventArgs) Handles btnReplaceData.Click
        'Replace the selected node with a data node:

        Dim DataName As String = txtDataName.Text.Trim
        Dim DataDescr As String = txtDataDescr.Text.Trim

        If DataName = "" Then
            Main.Message.AddWarning("Please enter a name for the Data node." & vbCrLf)
        Else
            If trvMatrixOps.SelectedNode Is Nothing Then
                Main.Message.AddWarning("Please select a node to replace with a Data node." & vbCrLf)
            Else
                Dim OldDataName As String = trvMatrixOps.SelectedNode.Name
                If OpInfo(OldDataName).CopyList.Count = 0 Then
                    If OpInfo.ContainsKey(DataName) And (DataName <> OldDataName) Then 'If DataName = OldDataName the node name will be reused when Node data or matrix is replaced
                        Main.Message.AddWarning("The node name is already used: " & DataName & vbCrLf)
                    Else
                        If rbScalar.Checked Then 'Replace the selected node with a Scalar node
                            If MatrixData.ContainsKey(OldDataName) Then
                                MatrixData.Remove(OldDataName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(DataName) Then Else OpInfo.Add(DataName, New MatrixOperationInfo)
                            OpInfo(DataName).Description = DataDescr
                            OpInfo(DataName).Type = "Scalar"
                            ScalarData.Add(DataName, 1)
                            trvMatrixOps.SelectedNode.Name = DataName
                            trvMatrixOps.SelectedNode.Text = DataName
                            trvMatrixOps.SelectedNode.ImageIndex = 2
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 3

                        ElseIf rbMatrix.Checked Then 'Replace the selected node with a Matrix node
                            If MatrixData.ContainsKey(OldDataName) Then
                                MatrixData.Remove(OldDataName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(DataName) Then Else OpInfo.Add(DataName, New MatrixOperationInfo)
                            OpInfo(DataName).Description = DataDescr
                            OpInfo(DataName).Type = "Matrix"
                            MatrixData.Add(DataName, New MatrixInfo)
                            MatrixData(DataName).Name = DataName
                            MatrixData(DataName).Description = DataDescr
                            trvMatrixOps.SelectedNode.Name = DataName
                            trvMatrixOps.SelectedNode.Text = DataName
                            trvMatrixOps.SelectedNode.ImageIndex = 4
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 5

                        ElseIf rbOpenMatrixFile.Checked Then 'Replace the selected node with an Open Matrix File node
                            If MatrixData.ContainsKey(OldDataName) Then
                                MatrixData.Remove(OldDataName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(DataName) Then Else OpInfo.Add(DataName, New MatrixOperationInfo)
                            OpInfo(DataName).Description = DataDescr
                            OpInfo(DataName).Type = "Open Matrix File"
                            MatrixData.Add(DataName, New MatrixInfo)
                            MatrixData(DataName).Name = DataName
                            MatrixData(DataName).Description = DataDescr
                            trvMatrixOps.SelectedNode.Name = DataName
                            trvMatrixOps.SelectedNode.Text = DataName
                            trvMatrixOps.SelectedNode.ImageIndex = 6
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 7

                        ElseIf rbUserDefScalar.Checked Then 'Replace the selected node with a User Defined Scalar node
                            If MatrixData.ContainsKey(OldDataName) Then
                                MatrixData.Remove(OldDataName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(DataName) Then Else OpInfo.Add(DataName, New MatrixOperationInfo)
                            OpInfo(DataName).Description = DataDescr
                            OpInfo(DataName).Type = "User Defined Scalar"
                            ScalarData.Add(DataName, 1)
                            trvMatrixOps.SelectedNode.Name = DataName
                            trvMatrixOps.SelectedNode.Text = DataName
                            trvMatrixOps.SelectedNode.ImageIndex = 8
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 9

                        ElseIf rbUserDefMatrix.Checked Then 'Replace the selected node with a User Defined Matrix node
                            If MatrixData.ContainsKey(OldDataName) Then
                                MatrixData.Remove(OldDataName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(DataName) Then Else OpInfo.Add(DataName, New MatrixOperationInfo)
                            OpInfo(DataName).Description = DataDescr
                            OpInfo(DataName).Type = "User Defined Matrix"
                            MatrixData.Add(DataName, New MatrixInfo)
                            MatrixData(DataName).Name = DataName
                            MatrixData(DataName).Description = DataDescr
                            trvMatrixOps.SelectedNode.Name = DataName
                            trvMatrixOps.SelectedNode.Text = DataName
                            trvMatrixOps.SelectedNode.ImageIndex = 10
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 11

                        ElseIf rbProcess.Checked Then 'Replace the selected node with a Process node
                            If MatrixData.ContainsKey(OldDataName) Then
                                MatrixData.Remove(OldDataName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(DataName) Then Else OpInfo.Add(DataName, New MatrixOperationInfo)
                            OpInfo(DataName).Description = DataDescr
                            OpInfo(DataName).Type = "Process"
                            trvMatrixOps.SelectedNode.Name = DataName
                            trvMatrixOps.SelectedNode.Text = DataName
                            trvMatrixOps.SelectedNode.ImageIndex = 12
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 13

                        ElseIf rbScalarProcess.Checked Then 'Replace the selected node with a Scalar Process node
                            If MatrixData.ContainsKey(OldDataName) Then
                                MatrixData.Remove(OldDataName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(DataName) Then Else OpInfo.Add(DataName, New MatrixOperationInfo)
                            OpInfo(DataName).Description = DataDescr
                            OpInfo(DataName).Type = "Scalar Process"
                            ScalarData.Add(DataName, 1)
                            trvMatrixOps.SelectedNode.Name = DataName
                            trvMatrixOps.SelectedNode.Text = DataName
                            trvMatrixOps.SelectedNode.ImageIndex = 14
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 15

                        ElseIf rbMatrixProcess.Checked Then 'Replace the selected node with a Matrix Process node
                            If MatrixData.ContainsKey(OldDataName) Then
                                MatrixData.Remove(OldDataName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldDataName) Then
                                ScalarData.Remove(OldDataName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(DataName) Then Else OpInfo.Add(DataName, New MatrixOperationInfo)
                            OpInfo(DataName).Description = DataDescr
                            OpInfo(DataName).Type = "Matrix  Process"
                            MatrixData.Add(DataName, New MatrixInfo)
                            MatrixData(DataName).Name = DataName
                            MatrixData(DataName).Description = DataDescr
                            trvMatrixOps.SelectedNode.Name = DataName
                            trvMatrixOps.SelectedNode.Text = DataName
                            trvMatrixOps.SelectedNode.ImageIndex = 16
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 17

                        Else
                            Main.Message.AddWarning("No data type has been selected." & vbCrLf)
                        End If
                    End If
                Else
                    Main.Message.AddWarning("This node data is copied in other nodes. Remove the copies before replacing this node." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnAppendOneMatrixOp_Click(sender As Object, e As EventArgs) Handles btnAppendOneMatrixOp.Click
        'Add a One Matrix Operation node.
        Dim OpName As String = txtOneMatrixOpName.Text.Trim
        Dim OpDescr As String = txtOneMatrixOpDescr.Text.Trim

        If OpName = "" Then
            Main.Message.AddWarning("Please enter a name for the One Matrix Operation node." & vbCrLf)
        Else
            If OpInfo.ContainsKey(OpName) Then
                Main.Message.AddWarning("The node name is already used: " & OpName & vbCrLf)
            Else
                If rbTranspose.Checked Then 'Add a matrix transpose node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Transpose"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 18, 19)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 18, 19)
                    End If
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr

                ElseIf rbInverse.Checked Then 'Add a matrix inverse node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Inverse"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 20, 21)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 20, 21)
                    End If
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr

                ElseIf rbCholesky.Checked Then 'Add a Cholesky factorization node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Cholesky"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 22, 23)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 22, 23)
                    End If
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr

                ElseIf rbTransCholesky.Checked Then 'Add a transposed Cholesky factorization node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Transposed Cholesky"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 24, 25)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 24, 25)
                    End If
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr

                ElseIf rbAddScalar.Checked Then 'Add a matrix add scalar node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Add Scalar"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 26, 27)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 26, 27)
                    End If
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr

                ElseIf rbMultScalar.Checked Then 'Add a matrix multiplied by scalar node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Multiply Scalar"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 28, 29)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 28, 29)
                    End If
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr

                ElseIf rbDivScalar.Checked Then 'Add a matrix divided by scalar node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Divide Scalar"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 30, 31)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 30, 31)
                    End If
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr

                ElseIf rbCovariance.Checked Then 'Add (Append) a Covariance node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Covariance"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 42, 43)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 42, 43)
                    End If
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr

                ElseIf rbCorrelation.Checked Then 'Add (Append) a correlation node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Correlation"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 44, 45)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 44, 45)
                    End If
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr

                Else
                    Main.Message.AddWarning("No Matrix Operation has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub


    Private Sub btnInsertOneMatrixOp_Click(sender As Object, e As EventArgs) Handles btnInsertOneMatrixOp.Click
        'Insert a One Matrix Operation node.
        'Dim DataName As String = txtDataName.Text.Trim
        Dim OpName As String = txtOneMatrixOpName.Text.Trim
        'Dim DataDescr As String = txtDataDescr.Text.Trim
        Dim OpDescr As String = txtOneMatrixOpDescr.Text.Trim

        If OpName = "" Then
            Main.Message.AddWarning("Please enter a name for the One Matrix Operation node." & vbCrLf)
        Else
            If OpInfo.ContainsKey(OpName) Then
                Main.Message.AddWarning("The node name is already used: " & OpName & vbCrLf)
            Else
                If rbTranspose.Checked Then 'Insert a matrix transpose node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Transpose"
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 18, 19)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 18, 19) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbInverse.Checked Then 'Insert a matrix inverse node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Inverse"
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 20, 21)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 20, 21) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbCholesky.Checked Then 'Insert a Cholesky factorization node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Cholesky"
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 22, 23)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 22, 23) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbTransCholesky.Checked Then 'Insert a transposed Cholesky factorization node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Transposed Cholesky"
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 24, 25)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 24, 25) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbAddScalar.Checked Then 'Insert a matrix add scalar node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Add Scalar"
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 26, 27)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 26, 27) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbMultScalar.Checked Then 'Insert a matrix multiplied by scalar node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Multiply Scalar"
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 28, 29)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 28, 29) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbDivScalar.Checked Then 'Insert a matrix divided by scalar node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Divide Scalar"
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 30, 31)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 30, 31) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbCovariance.Checked Then 'Insert a Covariance node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Covariance"
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 42, 43)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 42, 43) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbCorrelation.Checked Then 'Insert a correlation node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Correlation"
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 44, 45)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 44, 45) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                Else
                    Main.Message.AddWarning("No Matrix Operation has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnReplaceOneMatrixOp_Click(sender As Object, e As EventArgs) Handles btnReplaceOneMatrixOp.Click
        'Replace the selected node with a One Matrix Operation node:

        Dim OpName As String = txtOneMatrixOpName.Text.Trim
        Dim OpDescr As String = txtOneMatrixOpDescr.Text.Trim

        If OpName = "" Then
            Main.Message.AddWarning("Please enter a name for the One Matrix Operation node." & vbCrLf)
        Else
            If trvMatrixOps.SelectedNode Is Nothing Then
                Main.Message.AddWarning("Please select a node to replace with a One Matrix Operation node." & vbCrLf)
            Else
                Dim OldOpName As String = trvMatrixOps.SelectedNode.Name
                If OpInfo(OldOpName).CopyList.Count = 0 Then

                    If OpInfo.ContainsKey(OpName) And (OpName <> OldOpName) Then 'If DataName = OldDataName the node name will be reused when Node data or matrix is replaced
                        Main.Message.AddWarning("The node name is already used: " & OpName & vbCrLf)
                    Else
                        If rbTranspose.Checked Then 'Replace the selected node with a matrix transpose node
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Transpose"
                            MatrixData.Add(OpName, New MatrixInfo)
                            MatrixData(OpName).Name = OpName
                            MatrixData(OpName).Description = OpDescr
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 18
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 19

                        ElseIf rbInverse.Checked Then 'Replace the selected node with a matrix inverse node
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Inverse"
                            MatrixData.Add(OpName, New MatrixInfo)
                            MatrixData(OpName).Name = OpName
                            MatrixData(OpName).Description = OpDescr
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 20
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 21

                        ElseIf rbCholesky.Checked Then 'Replace the selected node with a Cholesky factorization node
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Cholesky"
                            MatrixData.Add(OpName, New MatrixInfo)
                            MatrixData(OpName).Name = OpName
                            MatrixData(OpName).Description = OpDescr
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 22
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 23

                        ElseIf rbTransCholesky.Checked Then 'Replace the selected node with a transposed Cholesky factorization node
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Transposed Cholesky"
                            MatrixData.Add(OpName, New MatrixInfo)
                            MatrixData(OpName).Name = OpName
                            MatrixData(OpName).Description = OpDescr
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 24
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 25

                        ElseIf rbAddScalar.Checked Then 'Replace the selected node with a matrix add scalar node
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Add Scalar"
                            MatrixData.Add(OpName, New MatrixInfo)
                            MatrixData(OpName).Name = OpName
                            MatrixData(OpName).Description = OpDescr
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 26
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 27

                        ElseIf rbMultScalar.Checked Then 'Replace the selected node with a matrix multiplied by scalar node
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Multiply Scalar"
                            MatrixData.Add(OpName, New MatrixInfo)
                            MatrixData(OpName).Name = OpName
                            MatrixData(OpName).Description = OpDescr
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 28
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 29

                        ElseIf rbDivScalar.Checked Then 'Replace the selected node with a matrix divided by scalar node
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Divide Scalar"
                            MatrixData.Add(OpName, New MatrixInfo)
                            MatrixData(OpName).Name = OpName
                            MatrixData(OpName).Description = OpDescr
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 30
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 31

                        ElseIf rbCovariance.Checked Then 'Replace the selected node with a Covariance node
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Covariance"
                            MatrixData.Add(OpName, New MatrixInfo)
                            MatrixData(OpName).Name = OpName
                            MatrixData(OpName).Description = OpDescr
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 42
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 43

                        ElseIf rbCorrelation.Checked Then 'Replace the selected node with a correlation node
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Correlation"
                            MatrixData.Add(OpName, New MatrixInfo)
                            MatrixData(OpName).Name = OpName
                            MatrixData(OpName).Description = OpDescr
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 44
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 45

                        Else
                            Main.Message.AddWarning("No Matrix Operation has been selected." & vbCrLf)
                        End If
                    End If
                Else
                    Main.Message.AddWarning("This node data is copied in other nodes. Remove the copies before replacing this node." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnAppendTwoMatrixOp_Click(sender As Object, e As EventArgs) Handles btnAppendTwoMatrixOp.Click
        'Add a Two Matrix Operation node.

        Dim OpName As String = txtTwoMatrixOpName.Text.Trim
        Dim OpDescr As String = txtTwoMatrixOpDescr.Text.Trim

        If OpName = "" Then
            Main.Message.AddWarning("Please enter a name for the Two Matrix Operation node." & vbCrLf)
        Else
            If OpInfo.ContainsKey(OpName) Then
                Main.Message.AddWarning("The node name is already used: " & OpName & vbCrLf)
            Else
                If rbAddMatrix.Checked Then
                    'Append an Add Matrix node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Add Matrix"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 32, 33)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 32, 33)
                    End If
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr

                ElseIf rbMultMatrix.Checked Then
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Multiply Matrix"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 34, 35)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 34, 35)
                    End If
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr

                Else
                    Main.Message.AddWarning("No Matrix Operation has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnInsertTwoMatrixOp_Click(sender As Object, e As EventArgs) Handles btnInsertTwoMatrixOp.Click
        'Insert a Two Matrix Operation node.

        Dim OpName As String = txtTwoMatrixOpName.Text.Trim
        Dim OpDescr As String = txtTwoMatrixOpDescr.Text.Trim

        If OpName = "" Then
            Main.Message.AddWarning("Please enter a name for the Two Matrix Operation node." & vbCrLf)
        Else
            If OpInfo.ContainsKey(OpName) Then
                Main.Message.AddWarning("The node name is already used: " & OpName & vbCrLf)
            Else
                If rbAddMatrix.Checked Then 'Insert an Add Matrix operation node.
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Add Matrix"
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 32, 33)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 32, 33) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbMultMatrix.Checked Then 'Insert Multiply Matrix operation node.
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Multiply Matrix"
                    MatrixData.Add(OpName, New MatrixInfo)
                    MatrixData(OpName).Name = OpName
                    MatrixData(OpName).Description = OpDescr
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 34, 35)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 34, 35) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                Else
                    Main.Message.AddWarning("No Matrix Operation has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnReplaceTwoMatrixOp_Click(sender As Object, e As EventArgs) Handles btnReplaceTwoMatrixOp.Click
        'Replace the selected node with a Two Matrix Operation node:

        Dim OpName As String = txtTwoMatrixOpName.Text.Trim
        Dim OpDescr As String = txtTwoMatrixOpDescr.Text.Trim

        If OpName = "" Then
            Main.Message.AddWarning("Please enter a name for the Two Matrix Operation node." & vbCrLf)
        Else
            If trvMatrixOps.SelectedNode Is Nothing Then
                Main.Message.AddWarning("Please select a node to replace with a One Matrix Operation node." & vbCrLf)
            Else
                Dim OldOpName As String = trvMatrixOps.SelectedNode.Name
                If OpInfo(OldOpName).CopyList.Count = 0 Then
                    If OpInfo.ContainsKey(OpName) And (OpName <> OldOpName) Then 'If DataName = OldDataName the node name will be reused when Node data or matrix is replaced
                        Main.Message.AddWarning("The node name is already used: " & OpName & vbCrLf)
                    Else
                        If rbAddMatrix.Checked Then 'Replace the selected node with an Add Matrix operation node.
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Add Matrix"
                            MatrixData.Add(OpName, New MatrixInfo)
                            MatrixData(OpName).Name = OpName
                            MatrixData(OpName).Description = OpDescr
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 32
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 33

                        ElseIf rbMultMatrix.Checked Then 'Replace the selected node with Multiply Matrix operation node.
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Multiply Matrix"
                            MatrixData.Add(OpName, New MatrixInfo)
                            MatrixData(OpName).Name = OpName
                            MatrixData(OpName).Description = OpDescr
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 34
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 35

                        Else
                            Main.Message.AddWarning("No Matrix Operation has been selected." & vbCrLf)
                        End If
                    End If
                Else
                    Main.Message.AddWarning("This node data is copied in other nodes. Remove the copies before replacing this node." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnAppendSpecialNode_Click(sender As Object, e As EventArgs) Handles btnAppendSpecialNode.Click
        'Add a Special node.

        Dim OpName As String = txtSpecialNodeName.Text.Trim
        Dim OpDescr As String = txtSpecialNodeDescr.Text.Trim

        If OpName = "" Then
            Main.Message.AddWarning("Please enter a name for the Special node." & vbCrLf)
        Else
            If OpInfo.ContainsKey(OpName) Then
                Main.Message.AddWarning("The node name is already used: " & OpName & vbCrLf)
            Else
                If rbCollection.Checked Then
                    'Append a Collection node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Collection"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 36, 37)
                    Else
                        trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 36, 37)
                    End If

                ElseIf rbScalarCopy.Checked Then
                    'Append a Scalar Copy node
                    If cmbDataSource.SelectedIndex = -1 Then
                        Main.Message.AddWarning("Select a scalar data source." & vbCrLf)
                    Else
                        OpInfo.Add(OpName, New MatrixOperationInfo)
                        OpInfo(OpName).Description = OpDescr
                        OpInfo(OpName).Type = "Scalar Copy"
                        Dim CopiedScalarName As String = cmbDataSource.SelectedItem.ToString
                        If trvMatrixOps.SelectedNode Is Nothing Then
                            'trvMatrixOps.Nodes.Add(OpName, OpName, 38, 39) 'The text will be the same as the copied node - TO BE UPDATED
                            'trvMatrixOps.Nodes.Add(OpName, cmbDataSource.SelectedItem.ToString, 38, 39) 'The text is the same as the copied node
                            trvMatrixOps.Nodes.Add(OpName, CopiedScalarName, 38, 39) 'The text is the same as the copied node
                        Else
                            'trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 38, 39) 'The text will be the same as the copied node - TO BE UPDATED
                            'trvMatrixOps.SelectedNode.Nodes.Add(OpName, cmbDataSource.SelectedItem.ToString, 38, 39) 'The text is the same as the copied node
                            trvMatrixOps.SelectedNode.Nodes.Add(OpName, CopiedScalarName, 38, 39) 'The text is the same as the copied node
                        End If
                        OpInfo(CopiedScalarName).CopyList.Add(OpName) 'Add the Scalar Copy node name to the CopyList if the copied node.
                        ScalarData.Add(OpName, 1) 'CHECK IF THIS IS NEEDED - SHOULD BE USING THE SCALR AT COPIEDSCALARNAME
                    End If

                ElseIf rbMatrixCopy.Checked Then
                    'Append a Matrix Copy node
                    If cmbDataSource.SelectedIndex = -1 Then
                        Main.Message.AddWarning("Select a matrix data source." & vbCrLf)
                    Else
                        OpInfo.Add(OpName, New MatrixOperationInfo)
                        OpInfo(OpName).Description = OpDescr
                        OpInfo(OpName).Type = "Matrix Copy"
                        Dim CopiedMatrixName As String = cmbDataSource.SelectedItem.ToString
                        If trvMatrixOps.SelectedNode Is Nothing Then
                            'trvMatrixOps.Nodes.Add(OpName, OpName, 40, 41) 'The text will be the same as the copied node - TO BE UPDATED
                            'trvMatrixOps.Nodes.Add(OpName, cmbDataSource.SelectedItem.ToString, 40, 41) 'The text is the same as the copied node
                            trvMatrixOps.Nodes.Add(OpName, CopiedMatrixName, 40, 41) 'The text is the same as the copied node
                        Else
                            'trvMatrixOps.SelectedNode.Nodes.Add(OpName, OpName, 40, 41) 'The text will be the same as the copied node - TO BE UPDATED
                            'trvMatrixOps.SelectedNode.Nodes.Add(OpName, cmbDataSource.SelectedItem.ToString, 40, 41)
                            trvMatrixOps.SelectedNode.Nodes.Add(OpName, CopiedMatrixName, 40, 41)
                        End If
                        OpInfo(CopiedMatrixName).CopyList.Add(OpName) 'Add the Matrix Copy node name to the CopyList if the copied node.
                        MatrixData.Add(OpName, New MatrixInfo) 'CHECK IF THIS IS NEEDED - SHOULD BE USING THE MATRIX AT COPIEDMATRIXNAME
                        MatrixData(OpName).Name = OpName
                        MatrixData(OpName).Description = OpDescr
                    End If

                Else
                    Main.Message.AddWarning("No Special Node has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnInsertSpecialNode_Click(sender As Object, e As EventArgs) Handles btnInsertSpecialNode.Click
        'Insert a Special node.

        Dim OpName As String = txtSpecialNodeName.Text.Trim
        Dim OpDescr As String = txtSpecialNodeDescr.Text.Trim

        If OpName = "" Then
            Main.Message.AddWarning("Please enter a name for the Special node." & vbCrLf)
        Else
            If OpInfo.ContainsKey(OpName) Then
                Main.Message.AddWarning("The node name is already used: " & OpName & vbCrLf)
            Else
                If rbCollection.Checked Then
                    'Insert a Collection node
                    OpInfo.Add(OpName, New MatrixOperationInfo)
                    OpInfo(OpName).Description = OpDescr
                    OpInfo(OpName).Type = "Collection"
                    If trvMatrixOps.SelectedNode Is Nothing Then
                        trvMatrixOps.Nodes.Add(OpName, OpName, 36, 37)
                    Else
                        Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                        Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                        Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 36, 37) 'Add the new node the the parent of the selected node at the same index position.
                        trvMatrixOps.SelectedNode.Remove()
                        NewNode.Nodes.Add(SelNode)
                        trvMatrixOps.SelectedNode = NewNode
                    End If

                ElseIf rbScalarCopy.Checked Then
                    'Insert a Scalar Copy node
                    If cmbDataSource.SelectedIndex = -1 Then
                        Main.Message.AddWarning("Select a scalar data source." & vbCrLf)
                    Else
                        OpInfo.Add(OpName, New MatrixOperationInfo)
                        OpInfo(OpName).Description = OpDescr
                        OpInfo(OpName).Type = "Scalar Copy"
                        'TO DO Add the Scalar Copy node name to the CopyList if the copied node.
                        ScalarData.Add(OpName, 1)
                        If trvMatrixOps.SelectedNode Is Nothing Then
                            'trvMatrixOps.Nodes.Add(OpName, OpName, 38, 39)
                            trvMatrixOps.Nodes.Add(OpName, cmbDataSource.SelectedItem.ToString, 38, 39) 'The text is the same as the copied node
                        Else
                            Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                            Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                            'Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 38, 39) 'Add the new node the the parent of the selected node at the same index position. 'The text will be the same as the copied node - TO BE UPDATED
                            Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, cmbDataSource.SelectedItem.ToString, 38, 39)  'The text is the same as the copied node
                            trvMatrixOps.SelectedNode.Remove()
                            NewNode.Nodes.Add(SelNode)
                            trvMatrixOps.SelectedNode = NewNode
                        End If
                    End If

                ElseIf rbMatrixCopy.Checked Then
                    'Insert a Matrix Copy node
                    If cmbDataSource.SelectedIndex = -1 Then
                        Main.Message.AddWarning("Select a matrix data source." & vbCrLf)
                    Else
                        OpInfo.Add(OpName, New MatrixOperationInfo)
                        OpInfo(OpName).Description = OpDescr
                        OpInfo(OpName).Type = "Matrix Copy"
                        'TO DO Add the Matrix Copy node name to the CopyList if the copied node.
                        MatrixData.Add(OpName, New MatrixInfo)
                        MatrixData(OpName).Name = OpName
                        MatrixData(OpName).Description = OpDescr
                        If trvMatrixOps.SelectedNode Is Nothing Then
                            'trvMatrixOps.Nodes.Add(OpName, OpName, 40, 41)
                            trvMatrixOps.Nodes.Add(OpName, cmbDataSource.SelectedItem.ToString, 40, 41)  'The text is the same as the copied node
                        Else
                            Dim SelNode As TreeNode = trvMatrixOps.SelectedNode 'Save the selected node
                            Dim SelIndex As Integer = trvMatrixOps.SelectedNode.Index
                            'Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, OpName, 40, 41) 'Add the new node the the parent of the selected node at the same index position. 'The text will be the same as the copied node - TO BE UPDATED
                            Dim NewNode As TreeNode = trvMatrixOps.SelectedNode.Parent.Nodes.Insert(SelIndex, OpName, cmbDataSource.SelectedItem.ToString, 40, 41) 'The text is the same as the copied node
                            trvMatrixOps.SelectedNode.Remove()
                            NewNode.Nodes.Add(SelNode)
                            trvMatrixOps.SelectedNode = NewNode
                        End If
                    End If

                Else
                    Main.Message.AddWarning("No Special Node has been selected." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnReplaceSpecialNode_Click(sender As Object, e As EventArgs) Handles btnReplaceSpecialNode.Click
        'Replace the selected node with a Special node:

        Dim OpName As String = txtSpecialNodeName.Text.Trim
        Dim OpDescr As String = txtSpecialNodeDescr.Text.Trim

        If OpName = "" Then
            Main.Message.AddWarning("Please enter a name for the Special node." & vbCrLf)
        Else
            If trvMatrixOps.SelectedNode Is Nothing Then
                Main.Message.AddWarning("Please select a node to replace with a Special node." & vbCrLf)
            Else
                Dim OldOpName As String = trvMatrixOps.SelectedNode.Name
                If OpInfo(OldOpName).CopyList.Count = 0 Then
                    If OpInfo.ContainsKey(OpName) And (OpName <> OldOpName) Then 'If DataName = OldDataName the node name will be reused when Node data or matrix is replaced
                        Main.Message.AddWarning("The node name is already used: " & OpName & vbCrLf)
                    Else
                        If rbCollection.Checked Then 'Replace the selected node with a Collection node.
                            If MatrixData.ContainsKey(OldOpName) Then
                                MatrixData.Remove(OldOpName) 'Remove the old matrix data
                            ElseIf ScalarData.ContainsKey(OldOpName) Then
                                ScalarData.Remove(OldOpName) 'Remove the old scalar data
                            End If
                            If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                            '*** REMOVE OpInfo(OldOpName) ?!!!
                            OpInfo(OpName).Description = OpDescr
                            OpInfo(OpName).Type = "Collection"
                            trvMatrixOps.SelectedNode.Name = OpName
                            trvMatrixOps.SelectedNode.Text = OpName
                            trvMatrixOps.SelectedNode.ImageIndex = 36
                            trvMatrixOps.SelectedNode.SelectedImageIndex = 37

                        ElseIf rbScalarCopy.Checked Then 'Replace the selected node with a Scalar Copy node.
                            If cmbDataSource.SelectedIndex = -1 Then
                                Main.Message.AddWarning("Select a scalar data source." & vbCrLf)
                            Else
                                If MatrixData.ContainsKey(OldOpName) Then
                                    MatrixData.Remove(OldOpName) 'Remove the old matrix data
                                ElseIf ScalarData.ContainsKey(OldOpName) Then
                                    ScalarData.Remove(OldOpName) 'Remove the old scalar data
                                End If
                                If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                                OpInfo(OpName).Description = OpDescr
                                OpInfo(OpName).Type = "Scalar Copy"
                                'TO DO Add the Scalar Copy node name to the CopyList if the copied node.
                                ScalarData.Add(OpName, 1)
                                trvMatrixOps.SelectedNode.Name = OpName
                                'trvMatrixOps.SelectedNode.Text = OpName 'The text will be the same as the copied node - TO BE UPDATED
                                trvMatrixOps.SelectedNode.Text = cmbDataSource.SelectedItem.ToString 'The text is the same as the copied node
                                trvMatrixOps.SelectedNode.ImageIndex = 38
                                trvMatrixOps.SelectedNode.SelectedImageIndex = 39
                            End If

                        ElseIf rbMatrixCopy.Checked Then 'Replace the selected node with a Matrix Copy node.
                            If cmbDataSource.SelectedIndex = -1 Then
                                Main.Message.AddWarning("Select a matrix data source." & vbCrLf)
                            Else
                                If MatrixData.ContainsKey(OldOpName) Then
                                    MatrixData.Remove(OldOpName) 'Remove the old matrix data
                                ElseIf ScalarData.ContainsKey(OldOpName) Then
                                    ScalarData.Remove(OldOpName) 'Remove the old scalar data
                                End If
                                If OpInfo.ContainsKey(OpName) Then Else OpInfo.Add(OpName, New MatrixOperationInfo)
                                OpInfo(OpName).Description = OpDescr
                                OpInfo(OpName).Type = "Matrix Copy"
                                'TO DO Add the Matrix Copy node name to the CopyList if the copied node.
                                MatrixData.Add(OpName, New MatrixInfo)
                                MatrixData(OpName).Name = OpName
                                MatrixData(OpName).Description = OpDescr
                                trvMatrixOps.SelectedNode.Name = OpName
                                'trvMatrixOps.SelectedNode.Text = OpName 'The text will be the same as the copied node - TO BE UPDATED
                                trvMatrixOps.SelectedNode.Text = cmbDataSource.SelectedItem.ToString 'The text is the same as the copied node
                                trvMatrixOps.SelectedNode.ImageIndex = 40
                                trvMatrixOps.SelectedNode.SelectedImageIndex = 41
                            End If

                        Else
                            Main.Message.AddWarning("No Special Node has been selected." & vbCrLf)
                        End If
                    End If
                Else
                    Main.Message.AddWarning("This node data is copied in other nodes. Remove the copies before replacing this node." & vbCrLf)
                End If
            End If
        End If
    End Sub

    Private Sub btnDeleteNode_Click(sender As Object, e As EventArgs) Handles btnDeleteNode.Click
        'Delete the selected node.

        trvMatrixOps.SelectedNode.Remove()
        'If MatrixData.ContainsKey(SelItemName) Then
        '    MatrixData.Remove(SelItemName)
        'ElseIf ScalarData.ContainsKey(SelItemName) Then
        '    ScalarData.Remove(SelItemName)
        'End If
        'SelDataName is the key to use - if the node is a copy, SelDataName is not the same as SelItemName. SelDataName is the correct key to use.
        If MatrixData.ContainsKey(SelDataName) Then
            MatrixData.Remove(SelDataName)
        ElseIf ScalarData.ContainsKey(SelDataName) Then
            ScalarData.Remove(SelDataName)
        End If
        If SelDataName <> SelItemName Then 'This node contains a copy of data from the node named SelDataName
            If OpInfo(SelItemName).CopyList.Contains(SelDataName) Then
                OpInfo(SelItemName).CopyList.Remove(SelDataName)
            End If
        End If
        SelItemName = ""
        SelDataName = ""
        SelNode = Nothing
    End Sub

    Private Sub btnListChildNodes_Click(sender As Object, e As EventArgs) Handles btnListChildNodes.Click
        'List all child nodes.

        'Main.Message.Add("List of child nodes for: " & trvMatrixOps.SelectedNode.Name & vbCrLf)
        ListChildNodes(trvMatrixOps.SelectedNode, 0)
    End Sub

    Private Sub ListChildNodes(ByVal myNode As TreeNode, ByVal Level As Integer)
        'Main.Message.Add(Space(Level * 2) & "List of child nodes for: " & myNode.Name & vbCrLf)
        For Each Node As TreeNode In myNode.Nodes
            Main.Message.Add(Space((Level + 1) * 3) & Node.Name & vbCrLf)
            ListChildNodes(Node, Level + 1)
        Next
    End Sub

    Private Sub btnCutNode_Click(sender As Object, e As EventArgs) Handles btnCutNode.Click
        'Cut the selected node

        If trvMatrixOps.SelectedNode Is Nothing Then
            Main.Message.AddWarning("Please select a node to cut." & vbCrLf)
        Else
            CutNode = trvMatrixOps.SelectedNode
            trvMatrixOps.SelectedNode.Remove()
        End If
    End Sub

    Private Sub btnCloneNode_Click(sender As Object, e As EventArgs) Handles btnCloneNode.Click
        'Clone the selected node

        If trvMatrixOps.SelectedNode Is Nothing Then
            Main.Message.AddWarning("Please select a node to cut." & vbCrLf)
        Else
            CutNode = trvMatrixOps.SelectedNode.Clone
            'trvMatrixOps.SelectedNode.Remove()
        End If

    End Sub

    Private Sub btnPasteNode_Click(sender As Object, e As EventArgs) Handles btnPasteNode.Click
        'Paste the copied node.

        If CutNode Is Nothing Then
            Main.Message.AddWarning("Please cut a node to paste." & vbCrLf)
        Else
            If trvMatrixOps.SelectedNode Is Nothing Then
                Main.Message.AddWarning("Please select a node to paste to." & vbCrLf)
            Else
                trvMatrixOps.SelectedNode.Nodes.Add(CutNode)
            End If
        End If
    End Sub

    Private Sub btnSaveSeq_Click(sender As Object, e As EventArgs) Handles btnSaveSeq.Click
        'Save the Matrix Operation Sequence.

        Dim FileName As String = txtSeqFileName.Text.Trim
        SaveSeq(FileName)
    End Sub



    'Private Sub SaveSeq_Old(ByVal SequenceName As String)
    '    'Save the current Matrix Operation Sequence

    '    If SequenceName.Trim = "" Then
    '        Main.Message.AddWarning("The Matrix Operation Sequence Name is blank." & vbCrLf)
    '    Else
    '        Dim decl As New XDeclaration("1.0", "utf-8", "yes")
    '        Dim XDoc As New XDocument(decl, Nothing)
    '        XDoc.Add(New XComment(""))
    '        XDoc.Add(New XComment("Matrix Operation Sequence"))

    '        Dim mySequence As New XElement("MatrixOperationSequence")

    '    End If
    'End Sub

    Private Sub SaveSeq(ByVal SequenceName As String)
        'Save the current Matrix Operation Sequence

        If SequenceName.Trim = "" Then
            Main.Message.AddWarning("The Matrix Operation Sequence Name is blank." & vbCrLf)
        Else
            Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                       <!---->
                       <!--Matrix Operation Sequence File-->
                       <MatrixOperationSequence>
                           <Name><%= txtSeqName.Text.Trim %></Name>
                           <Description><%= txtSeqDescr.Text.Trim %></Description>
                           <!--Matrix Operation Information-->
                           <%= OpInfoToXDoc().<OperationInfoList> %>
                           <!--Scalar Data-->
                           <%= ScalarDataToXDoc().<ScalarDataList> %>
                           <!--Matrix Data-->
                           <%= MatrixDataToXDoc().<MatrixDataList> %>
                           <!--Matrix Operation Tree-->
                           <%= MatrixOpTreeToXDoc().<MatrixOperationTree> %>
                       </MatrixOperationSequence>
            Main.Project.SaveXmlData(SequenceName, XDoc)
        End If
    End Sub

    'This updated version includes the CopyList -  a list of nodes that are copies of each node. The Scalar Copy and Matrix Copy nodes allow scalar and matrox node values to used in other nodes.
    Private Function OpInfoToXDoc() As System.Xml.Linq.XDocument
        'Return the Operation Information in an XDocument
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <OperationInfoList>
                       <%= From item In OpInfo
                           Select
                           <Operation>
                               <Name><%= item.Key %></Name>
                               <Text><%= item.Value.Text %></Text>
                               <Description><%= item.Value.Description %></Description>
                               <Type><%= item.Value.Type %></Type>
                               <Status><%= item.Value.Status %></Status>
                               <Left><%= item.Value.Left %></Left>
                               <Top><%= item.Value.Top %></Top>
                               <Height><%= item.Value.Height %></Height>
                               <Width><%= item.Value.Width %></Width>
                               <SelectedTab><%= item.Value.SelectedTab %></SelectedTab>
                               <CopyList>
                                   <%= From listItem In item.Value.CopyList
                                       Select
                                       <Name><%= listItem %></Name> %>
                               </CopyList>
                           </Operation> %>
                   </OperationInfoList>
        Return XDoc
    End Function

    'Private Function OpInfoToXDoc() As System.Xml.Linq.XDocument
    '    'Return the Operation Information in an XDocument
    '    Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
    '               <OperationInfoList>
    '                   <%= From item In OpInfo
    '                       Select
    '                       <Operation>
    '                           <Name><%= item.Key %></Name>
    '                           <Description><%= item.Value.Description %></Description>
    '                           <Type><%= item.Value.Type %></Type>
    '                           <Status><%= item.Value.Status %></Status>
    '                       </Operation> %>
    '               </OperationInfoList>
    '    Return XDoc
    'End Function

    Private Function ScalarDataToXDoc() As System.Xml.Linq.XDocument
        'Return the Scalar Data in an XDocument
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <ScalarDataList>
                       <%= From item In ScalarData
                           Select
                           <Scalar>
                               <Name><%= item.Key %></Name>
                               <Value><%= item.Value %></Value>
                           </Scalar> %>
                   </ScalarDataList>
        Return XDoc
    End Function

    Private Function MatrixDataToXDoc() As System.Xml.Linq.XDocument
        'Return the Matrix data in an XDocuement
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <MatrixDataList>
                       <%= From item In MatrixData
                           Select
                           <Data>
                               <Name><%= item.Key %></Name>
                               <%= item.Value.MatrixToXDoc.<Matrix> %>
                           </Data>
                       %>
                   </MatrixDataList>

        '  <Matrix><%= item.Value.MatrixToXDoc %></Matrix>
        Return XDoc
    End Function

    Private Function MatrixOpTreeToXDoc() As System.Xml.Linq.XDocument
        'Return the Matrix Operations Tree as an XDocument

        Dim decl As New XDeclaration("1.0", "utf-8", "yes")
        Dim XDoc As New XDocument(decl, Nothing)
        Dim myMatrixOpTree As New XElement("MatrixOperationTree")

        SaveMatrixOpNode(myMatrixOpTree, "", trvMatrixOps.Nodes)

        XDoc.Add(myMatrixOpTree)

        Return XDoc

    End Function

    Private Sub SaveMatrixOpNode(ByRef myElement As XElement, ByVal Parent As String, ByRef tnc As TreeNodeCollection)
        'Save the nodes in the TreeNodeCollection in the XElement
        'This method calls itself recursively to save all nodes in trvMatrixOps

        If tnc.Count = 0 Then 'Leaf
        Else
            Dim I As Integer
            For I = 0 To tnc.Count - 1
                Dim NodeKey As String = tnc(I).Name
                Dim myNode As New XElement(System.Xml.XmlConvert.EncodeName(NodeKey)) 'A space character os not allowed in an XElement name. Replace spaces with &sp characters.
                Dim myNodeText As New XElement("Text", tnc(I).Text)
                myNode.Add(myNodeText)

                If tnc(I).Nodes.Count > 0 Then
                    'Main.Message.Add("Node name = " & tnc(I).Name & " IsExpanded: " & tnc(I).IsExpanded & vbCrLf)
                    Dim isExpanded As New XElement("IsExpanded", tnc(I).IsExpanded)
                    myNode.Add(isExpanded)
                End If

                If OpInfo.ContainsKey(NodeKey) Then
                    Dim myNodeType As New XElement("Type", OpInfo(NodeKey).Type)
                Else
                    Main.Message.Add("The Node Name: " & NodeKey & " is not in the OpInfo dictionary!" & vbCrLf)
                End If
                SaveMatrixOpNode(myNode, tnc(I).Name, tnc(I).Nodes)
                myElement.Add(myNode)
            Next
        End If
    End Sub


    Private Sub btnOpenSeq_Click(sender As Object, e As EventArgs) Handles btnOpenSeq.Click
        'Open a Matrix Operation Sequence.

        Select Case Main.Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                'Select a MonteCarlo file from the project Data directory:
                OpenFileDialog1.InitialDirectory = Main.Project.DataLocn.Path
                OpenFileDialog1.Filter = "Matrix Operation Sequence | *.MatrixOpSeq"
                If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                    Dim FileName As String = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                    txtSeqFileName.Text = FileName
                    OpenSeq(FileName)
                End If
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                Zip = New ADVL_Utilities_Library_1.ZipComp
                Zip.ArchivePath = Main.Project.DataLocn.Path
                Zip.SelectFile() 'Show the SelectFile form.
                Zip.SelectFileForm.ApplicationName = Main.Project.Application.Name
                Zip.SelectFileForm.SettingsLocn = Main.Project.SettingsLocn
                Zip.SelectFileForm.Show()
                Zip.SelectFileForm.RestoreFormSettings()
                Zip.SelectFileForm.FileExtension = ".MatrixOpSeq" 'Can also use .FileExtensions = {xxxx, xxxx, xxxx} to specify multiple file types.
                Zip.SelectFileForm.GetFileList()
                If Zip.SelectedFile <> "" Then
                    Dim FileName As String = Zip.SelectedFile
                    txtSeqFileName.Text = FileName
                    OpenSeq(FileName)
                End If
        End Select
    End Sub

    Private Sub OpenSeq(ByVal SequenceName As String)
        'Open a Matrix Operation Sequence

        If SequenceName.Trim = "" Then
            Main.Message.AddWarning("The Matrix Operation Sequence Name is blank." & vbCrLf)
        Else
            'Dim XDoc As XDocument
            Dim XDoc As New System.Xml.Linq.XDocument
            Main.Project.ReadXmlData(SequenceName, XDoc)

            txtSeqName.Text = XDoc.<MatrixOperationSequence>.<Name>.Value
            txtSeqDescr.Text = XDoc.<MatrixOperationSequence>.<Description>.Value

            'Restore the OpInfo() dictionary:
            Dim MatrixOpInfoList = From item In XDoc.<MatrixOperationSequence>.<OperationInfoList>.<Operation>
            Dim Name As String
            OpInfo.Clear()
            For Each item In MatrixOpInfoList
                Dim MatOpInfo As MatrixOperationInfo = New MatrixOperationInfo
                Name = item.<Name>.Value
                If item.<Text>.Value <> Nothing Then MatOpInfo.Text = item.<Text>.Value
                MatOpInfo.Description = item.<Description>.Value
                MatOpInfo.Type = item.<Type>.Value
                MatOpInfo.Status = item.<Status>.Value
                If item.<Left>.Value Is Nothing Then Else MatOpInfo.Left = item.<Left>.Value
                If item.<Top>.Value Is Nothing Then Else MatOpInfo.Top = item.<Top>.Value
                If item.<Height>.Value Is Nothing Then Else MatOpInfo.Height = item.<Height>.Value
                If item.<Width>.Value Is Nothing Then Else MatOpInfo.Width = item.<Width>.Value
                If item.<SelectedTab>.Value Is Nothing Then Else MatOpInfo.SelectedTab = item.<SelectedTab>.Value

                Dim Copies = From listItem In item.<CopyList>
                For Each listItem In Copies
                    MatOpInfo.CopyList.Add(listItem)
                Next

                OpInfo.Add(Name, MatOpInfo)

            Next

            'Restore the ScalarData() dictionary:
            Dim ScalarInfoList = From item In XDoc.<MatrixOperationSequence>.<ScalarDataList>.<Scalar>
            ScalarData.Clear()
            For Each item In ScalarInfoList
                ScalarData.Add(item.<Name>.Value, item.<Value>.Value)
            Next

            'Restore the MatrixData() dictionary:
            'Dim MatrixInfoList = From item In XDoc.<MatrixOperationSequence>.<MatrixDataList>.<Matrix>
            Dim MatrixInfoList = From item In XDoc.<MatrixOperationSequence>.<MatrixDataList>.<Data>
            MatrixData.Clear()
            'Dim MatrixXDoc As Xml.XmlDocument
            'Dim MatrixXDoc As System.Xml.Linq.XDocument
            Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
            For Each item In MatrixInfoList
                Name = item.<Name>.Value
                Dim Matrix As MatrixInfo = New MatrixInfo
                'MatrixXDoc.LoadXml(XmlHeader & vbCrLf & item.<Matrix>.ToString)
                'MatrixXDoc = XDocument.Parse(XmlHeader & vbCrLf & item.<Matrix>.ToString) '"System.Xml.Linq.XContainer+<GetElements>d__40"
                'MatrixXDoc = XDocument.Parse(XmlHeader & vbCrLf & item.<Matrix>)

                Dim MatrixXDoc = <?xml version="1.0" encoding="utf-8"?>
                                 <%= item.<Matrix> %>

                Dim MatrixItem As MatrixInfo = New MatrixInfo
                MatrixItem.XDocToMatrix(MatrixXDoc)
                MatrixData.Add(Name, MatrixItem)
            Next

            'Restore the Matrix Operation Tree
            trvMatrixOps.Nodes.Clear()
            Dim I As Integer
            'Dim TreeXDoc As New System.Xml.XmlDocument
            'TreeXDoc.LoadXml(XmlHeader & vbCrLf & XDoc.<MatrixOperationSequence>.<MatrixOperationTree>.ToString)
            'TreeXDoc.LoadXml(XmlHeader & vbCrLf & XDoc.<MatrixOperationSequence>.<MatrixOperationTree>)

            'ProcessMatrixOpNode(TreeXDoc.DocumentElement, trvMatrixOps.Nodes, "", True)
            'ProcessMatrixOpNode(XDoc.<MatrixOperationSequence>.<MatrixOperationTree>.Nodes, trvMatrixOps.Nodes, "", True)

            'TreeXDoc.LoadXml(XDoc.<MatrixOperationSequence>.<MatrixOperationTree>.Nodes.ToString)
            'ProcessMatrixOpNode(TreeXDoc.DocumentElement, trvMatrixOps.Nodes, "", True)

            'Convert he XDocument to an XmlDocument:
            Dim XmlDoc As New System.Xml.XmlDocument
            XmlDoc.LoadXml(XDoc.ToString)
            'ProcessMatrixOpNode(XmlDoc.GetElementById("MatrixOperationTree"), trvMatrixOps.Nodes, "", True)
            ProcessMatrixOpNode(XmlDoc.GetElementsByTagName("MatrixOperationTree").Item(0), trvMatrixOps.Nodes, "", True)

        End If

    End Sub

    Private Sub ProcessMatrixOpNode(ByVal xml_Node As System.Xml.XmlNode, ByVal tnc As TreeNodeCollection, ByVal Spaces As String, ByVal ParentNodeIsExpanded As Boolean)
        'Opening the Matrix Operation Sequence. Process the Child Nodes in the Operation Tree.
        'This subroutine calls itself to process the child node branches.

        Dim NodeInfo As System.Xml.XmlNode
        Dim NodeText As String = ""
        Dim NodeKey As String = ""
        Dim IsExpanded As Boolean = True
        Dim HasNodes As Boolean = True

        Dim NodeType As String = ""

        For Each ChildNode As System.Xml.XmlNode In xml_Node.ChildNodes
            Dim myNodeText As System.Xml.XmlNode
            myNodeText = ChildNode.SelectSingleNode("Text")
            If IsNothing(myNodeText) Then
                '/Text node not found
            Else
                Dim myNodeTextValue As String = myNodeText.InnerText
                NodeKey = System.Xml.XmlConvert.DecodeName(ChildNode.Name)
                If OpInfo.ContainsKey(NodeKey) Then
                    NodeType = OpInfo(NodeKey).Type

                    'Read Operation IsExpanded:
                    NodeInfo = ChildNode.SelectSingleNode("IsExpanded")
                    If NodeInfo Is Nothing Then
                        HasNodes = False
                        IsExpanded = True
                    Else
                        IsExpanded = NodeInfo.InnerText
                    End If

                    OpInfo(NodeKey).Text = myNodeTextValue 'NOTE this is also loaded separately when the OperationInfoList is read.

                    Select Case NodeType
                        Case "Matrix Operation Sequence"
                            'Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 16, 16) 'Add a node to the tree node collection: Key, Text, ImageIndex, SelectedImageIndex.
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 0, 1) 'Add a node to the tree node collection: Key, Text, ImageIndex, SelectedImageIndex.
                            'If IsExpanded Then new_Node.Expand()
                            'If IsExpanded Then
                            '    new_Node.EnsureVisible()
                            'Else
                            '    new_Node.Collapse()
                            'End If
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)
                        Case "Scalar"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 2, 3)
                            'If IsExpanded Then new_Node.Expand()
                            'If IsExpanded Then
                            '    new_Node.EnsureVisible()
                            'Else
                            '    new_Node.Collapse()
                            'End If
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)
                        Case "Matrix"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 4, 5)
                            'If IsExpanded Then new_Node.Expand()
                            'If IsExpanded Then
                            '    new_Node.EnsureVisible()
                            'Else
                            '    new_Node.Collapse()
                            'End If
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)
                        Case "Open Matrix File"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 6, 7)
                            'If IsExpanded Then new_Node.Expand()
                            'If IsExpanded Then
                            '    new_Node.EnsureVisible()
                            'Else
                            '    new_Node.Collapse()
                            'End If
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)
                        Case "User Defined Scalar"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 8, 9)
                            'If IsExpanded Then new_Node.Expand()
                            'If IsExpanded Then
                            '    new_Node.EnsureVisible()
                            'Else
                            '    new_Node.Collapse()
                            'End If
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)
                        Case "User Defined Matrix"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 10, 11)
                            'If IsExpanded Then new_Node.Expand()
                            'If IsExpanded Then
                            '    new_Node.EnsureVisible()
                            'Else
                            '    new_Node.Collapse()
                            'End If
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)
                        Case "Process"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 12, 13)
                            'If IsExpanded Then new_Node.Expand()
                            'If IsExpanded Then
                            '    new_Node.EnsureVisible()
                            'Else
                            '    new_Node.Collapse()
                            'End If
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)
                        Case "Scalar Process"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 14, 15)
                            'If IsExpanded Then new_Node.Expand()
                            'If IsExpanded Then
                            '    new_Node.EnsureVisible()
                            'Else
                            '    new_Node.Collapse()
                            'End If
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)
                        Case "Matrix Process"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 16, 17)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Transpose"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 18, 19)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Inverse"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 20, 21)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Cholesky"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 22, 23)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Transposed Cholesky"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 24, 25)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Add Scalar"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 26, 27)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Multiply Scalar"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 28, 29)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Divide Scalar"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 30, 31)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Covariance"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 42, 43)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Correlation"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 44, 45)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Add Matrix"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 32, 33)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Multiply Matrix"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 34, 35)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Collection"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 36, 37)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Scalar Copy"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 38, 39)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case "Matrix Copy"
                            Dim new_Node As TreeNode = tnc.Add(NodeKey, myNodeTextValue, 40, 41)
                            If HasNodes Then
                                If IsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            Else
                                If ParentNodeIsExpanded Then
                                    new_Node.EnsureVisible()
                                Else
                                    new_Node.Collapse()
                                End If
                            End If
                            ProcessMatrixOpNode(ChildNode, new_Node.Nodes, Spaces, IsExpanded)

                        Case Else
                            Main.Message.AddWarning("Unknown node type: " & NodeType & vbCrLf)
                    End Select

                Else
                    Main.Message.AddWarning("No entry found in the Operation Information dictionary for: " & NodeKey & vbCrLf)
                End If

            End If

        Next

    End Sub

    Private Sub trvMatrixOps_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trvMatrixOps.AfterSelect
        'Show the Matrix Operation information on the Information tab.
        'Dim ItemName As String = e.Node.Text
        'SelItemName = e.Node.Text
        SelItemName = e.Node.Name
        'Dim SelItemText As String = e.Node.Text
        SelDataName = e.Node.Text
        SelNode = e.Node 'Save the selected node - this may be needed later
        txtItemName.Text = SelItemName
        txtEditNodeName.Text = SelItemName
        txtEditNodeText.Text = SelDataName
        If OpInfo.ContainsKey(SelItemName) Then
            txtItemType.Text = OpInfo(SelItemName).Type
            txtEditNodeType.Text = OpInfo(SelItemName).Type
            txtItemStatus.Text = OpInfo(SelItemName).Status
            txtItemDescription.Text = OpInfo(SelItemName).Description
            txtEditNodeDescr.Text = OpInfo(SelItemName).Description
            If OpInfo(SelItemName).Type = "Matrix Operation Sequence" Then
                ShowNoItem()
                txtEditNodeDataType.Text = "None"
            ElseIf OpInfo(SelItemName).Type = "Scalar" Then
                ShowScalarItem(SelDataName)
                txtEditNodeDataType.Text = "Scalar"
            ElseIf OpInfo(SelItemName).Type = "Matrix" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "Open Matrix File" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "User Defined Scalar" Then
                ShowScalarItem(SelDataName)
                txtEditNodeDataType.Text = "Scalar"
            ElseIf OpInfo(SelItemName).Type = "User Defined Matrix" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "Process" Then
                ShowNoItem()
                txtEditNodeDataType.Text = "None"
            ElseIf OpInfo(SelItemName).Type = "Scalar Process" Then
                ShowScalarItem(SelDataName)
                txtEditNodeDataType.Text = "Scalar"
            ElseIf OpInfo(SelItemName).Type = "Matrix Process" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "Transpose" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "Inverse" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "Cholesky" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "Transposed Cholesky" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "Add Scalar" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "Multiply Scalar" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "Divide Scalar" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"

            ElseIf OpInfo(SelItemName).Type = "Covariance" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "Correlation" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"

            ElseIf OpInfo(SelItemName).Type = "Add Matrix" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"
            ElseIf OpInfo(SelItemName).Type = "Multiply Matrix" Then
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"

            ElseIf OpInfo(SelItemName).Type = "Collection" Then
                ShowNoItem()
                txtEditNodeDataType.Text = "None"
            ElseIf OpInfo(SelItemName).Type = "Scalar Copy" Then
                'ShowScalarItem(SelItemName)
                ShowScalarItem(SelDataName)
                txtEditNodeDataType.Text = "Scalar"
            ElseIf OpInfo(SelItemName).Type = "Matrix Copy" Then
                'ShowMatrixItem(SelItemName)
                ShowMatrixItem(SelDataName)
                txtEditNodeDataType.Text = "Matrix"

            Else
                Main.Message.AddWarning("Unknown Matrix Operation Type: " & OpInfo(SelItemName).Type & vbCrLf)
            End If
        Else
            txtItemType.Text = ""
            txtItemStatus.Text = ""
            txtItemDescription.Text = ""
        End If
    End Sub

    Private Sub ShowMatrixItem(ByVal ItemName As String)
        'Show the Matrix Item

        If MatrixData.ContainsKey(ItemName) Then
            'Disable the Scalar display:
            txtScalarName.Enabled = False
            txtScalarItem.Enabled = False
            'Enable the Matrix display:
            btnOpenMatrixItem.Enabled = True
            btnNewMatrixItem.Enabled = True
            btnSaveMatrixItem.Enabled = True
            btnCopyMatrixItem.Enabled = True
            btnPasteMatrixItem.Enabled = True
            txtMatrixItemFileName.Enabled = True
            txtMatrixItemName.Enabled = True
            txtMatrixItemNRows.Enabled = True
            txtMatrixItemNCols.Enabled = True
            txtMatrixItemFormat.Enabled = True
            txtMatrixItemDescr.Enabled = True
            dgvMatrixItem.Enabled = True
            'Un-gray-out the DataGridView:
            dgvMatrixItem.DefaultCellStyle.BackColor = SystemColors.Window
            dgvMatrixItem.DefaultCellStyle.ForeColor = SystemColors.ControlText
            dgvMatrixItem.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Window
            dgvMatrixItem.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText
            dgvMatrixItem.EnableHeadersVisualStyles = True

            'Display the Matrix:
            txtMatrixItemFileName.Text = MatrixData(ItemName).FileName
            txtMatrixItemName.Text = MatrixData(ItemName).Name
            txtMatrixItemNRows.Text = MatrixData(ItemName).NRows
            txtMatrixItemNCols.Text = MatrixData(ItemName).NCols
            txtMatrixItemFormat.Text = MatrixData(ItemName).Format
            txtMatrixItemDescr.Text = MatrixData(ItemName).Description
            Dim RowNo As Integer
            Dim ColNo As Integer
            dgvMatrixItem.Rows.Clear()
            dgvMatrixItem.RowCount = MatrixData(ItemName).NRows
            dgvMatrixItem.ColumnCount = MatrixData(ItemName).NCols
            For RowNo = 0 To MatrixData(ItemName).NRows - 1
                For ColNo = 0 To MatrixData(ItemName).NCols - 1
                    dgvMatrixItem.Rows(RowNo).Cells(ColNo).Value = MatrixData(ItemName).Data(RowNo, ColNo)
                Next
            Next
            dgvMatrixItem.DefaultCellStyle.Format = txtMatrixItemFormat.Text
            dgvMatrixItem.AutoResizeColumns()
            dgvMatrixItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Else
            Main.Message.AddWarning("The Matrix Item was not found: " & ItemName & vbCrLf)
        End If
    End Sub

    Private Sub ShowScalarItem(ByVal ItemName As String)
        'Show the Scalar item

        If ScalarData.ContainsKey(ItemName) Then
            'Disable the Matrix display:
            btnOpenMatrixItem.Enabled = False
            btnNewMatrixItem.Enabled = False
            btnSaveMatrixItem.Enabled = False
            btnCopyMatrixItem.Enabled = False
            btnPasteMatrixItem.Enabled = False
            txtMatrixItemFileName.Enabled = False
            txtMatrixItemName.Enabled = False
            txtMatrixItemNRows.Enabled = False
            txtMatrixItemNCols.Enabled = False
            txtMatrixItemFormat.Enabled = False
            txtMatrixItemDescr.Enabled = False
            dgvMatrixItem.Enabled = False
            'Gray-out the DataGridView:
            dgvMatrixItem.DefaultCellStyle.BackColor = SystemColors.Control
            dgvMatrixItem.DefaultCellStyle.ForeColor = SystemColors.GrayText
            dgvMatrixItem.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control
            dgvMatrixItem.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText
            dgvMatrixItem.EnableHeadersVisualStyles = False

            'Enable the Scalar display:
            txtScalarName.Enabled = True
            txtScalarName.Text = ItemName
            txtScalarItem.Enabled = True
            'Display the Scalar value:
            txtScalarItem.Text = ScalarData(ItemName)
        Else
            Main.Message.AddWarning("The Scalar Item was not found: " & ItemName & vbCrLf)
        End If
    End Sub

    Private Sub ShowNoItem()
        'Show no item value on the Information tab.
        'Disable the Matrix display:
        btnOpenMatrixItem.Enabled = False
        btnNewMatrixItem.Enabled = False
        btnSaveMatrixItem.Enabled = False
        btnCopyMatrixItem.Enabled = False
        btnPasteMatrixItem.Enabled = False
        txtMatrixItemFileName.Enabled = False
        txtMatrixItemName.Enabled = False
        txtMatrixItemNRows.Enabled = False
        txtMatrixItemNCols.Enabled = False
        txtMatrixItemFormat.Enabled = False
        txtMatrixItemDescr.Enabled = False
        dgvMatrixItem.Enabled = False
        'Gray-out the DataGridView:
        dgvMatrixItem.DefaultCellStyle.BackColor = SystemColors.Control
        dgvMatrixItem.DefaultCellStyle.ForeColor = SystemColors.GrayText
        dgvMatrixItem.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control
        dgvMatrixItem.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText
        dgvMatrixItem.EnableHeadersVisualStyles = False

        'Disable the Scalar display:
        txtScalarItem.Enabled = False
    End Sub

    Private Sub ShowItem(ByVal ItemName As String)
        'Show the item with the name ItemName.
        If MatrixData.ContainsKey(ItemName) Then
            ShowMatrixItem(ItemName)
        ElseIf ScalarData.ContainsKey(ItemName) Then
            ShowScalarItem(ItemName)
        Else
            ShowNoItem()
        End If
    End Sub

    Private Sub btnFormatHelp2_Click(sender As Object, e As EventArgs) Handles btnFormatHelp2.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub txtMatrixItemFormat_TextChanged(sender As Object, e As EventArgs) Handles txtMatrixItemFormat.TextChanged

    End Sub

    Private Sub txtMatrixItemFormat_LostFocus(sender As Object, e As EventArgs) Handles txtMatrixItemFormat.LostFocus

        Dim ItemName As String = txtItemName.Text
        If MatrixData.ContainsKey(ItemName) Then
            MatrixData(ItemName).Format = txtMatrixItemFormat.Text
            dgvMatrixItem.DefaultCellStyle.Format = MatrixData(ItemName).Format
        Else
            Main.Message.AddWarning("Matrix item not found: " & ItemName & vbCrLf)
        End If
    End Sub

    Private Sub dgvMatrixItem_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMatrixItem.CellContentClick

    End Sub

    Private Sub dgvMatrixItem_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMatrixItem.CellEndEdit
        Dim ItemName As String = txtItemName.Text
        If MatrixData.ContainsKey(ItemName) Then
            MatrixData(ItemName).Data(e.RowIndex, e.ColumnIndex) = dgvMatrixItem.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        Else
            Main.Message.AddWarning("Matrix item not found: " & ItemName & vbCrLf)
        End If
    End Sub

    Private Sub btnPasteMatrixItem_Click(sender As Object, e As EventArgs) Handles btnPasteMatrixItem.Click
        'Paste the contents of the Main.MatrixClipboard into this matrix

        Dim ItemName As String = txtItemName.Text
        If MatrixData.ContainsKey(ItemName) Then
            Main.MatrixClipboard.Paste(MatrixData(ItemName))
            ShowMatrixItem(ItemName) 'Update the Matrix data display
            UpdateNode(SelNode.Parent)  'Update the Parent node
        Else
            Main.Message.AddWarning("Matrix item not found: " & ItemName & vbCrLf)
        End If
    End Sub

    Private Sub UpdateNode(ByRef myNode As TreeNode)
        'Update the operation in myNode.

        Dim ItemName As String = myNode.Text
        'ApplyOperation(ItemName)

        If OpInfo.ContainsKey(ItemName) Then
            Select Case OpInfo(ItemName).Type
                Case "Matrix Operation Sequence"
                     'No operation to apply
                Case "Scalar"
                    'No operation to apply
                Case "Matrix"
                    'No operation to apply
                Case "Open Matrix File"
                    'No operation to apply
                Case "User Defined Scalar"
                    'No operation to apply
                Case "User Defined Matrix"
                    'No operation to apply
                Case "Process"
                    'No operation to apply
                Case "Collection"
                      'No operation to apply
                Case "Scalar Copy"
                     'No operation to apply
                Case "Matrix Copy"
                     'No operation to apply
                Case "Scalar Process"
                    'This should have a single scalar child node.
                    'Copy this node and update the parent node.
                    If ScalarData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 1 Then
                            If ScalarData.ContainsKey(myNode.Nodes(0).Text) Then
                                ScalarData(ItemName) = ScalarData(myNode.Nodes(0).Text) 'Copy the Child Node scalar data.
                                UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                            Else
                                Main.Message.AddWarning("The Scalar Process node named " & ItemName & " does not have a Scalar child node." & vbCrLf)
                            End If
                        ElseIf myNode.Nodes.Count = 0 Then
                            Main.Message.AddWarning("The Scalar Process node named " & ItemName & " does not have a child node." & vbCrLf)
                        Else
                            Main.Message.AddWarning("The Scalar Process node named " & ItemName & " has more than one child node." & vbCrLf)
                        End If
                    Else
                        Main.Message.AddWarning("The Scalar Process node named " & ItemName & " does not have a Scalar data item." & vbCrLf)
                    End If

                Case "Matrix Process"
                    'This should have a single matrix child node.
                    'Copy this node and update the parent node.
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 1 Then
                            If MatrixData.ContainsKey(myNode.Nodes(0).Text) Then
                                MatrixData(ItemName) = MatrixData(myNode.Nodes(0).Text) 'Copy the Child Node matrix data.
                                UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                            Else
                                Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix child node." & vbCrLf)
                            End If
                        ElseIf myNode.Nodes.Count = 0 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                        Else
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than one child node." & vbCrLf)
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case "Transpose"
                    'This should have  single matrix child node.
                    'Update this node with the transpose of the child node matrix.
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 1 Then
                            Dim ChildNodeText As String = myNode.Nodes(0).Text
                            If MatrixData.ContainsKey(ChildNodeText) Then
                                'Generate the Matrix transpose:
                                MatrixData(ItemName).NCols = MatrixData(ChildNodeText).NRows
                                MatrixData(ItemName).NRows = MatrixData(ChildNodeText).NCols
                                Dim RowNo As Integer
                                Dim ColNo As Integer
                                For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                    For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                        MatrixData(ItemName).Data(RowNo, ColNo) = MatrixData(ChildNodeText).Data(ColNo, RowNo)
                                    Next
                                Next
                                UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                            Else
                                Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix child node." & vbCrLf)
                            End If
                        ElseIf myNode.Nodes.Count = 0 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                        Else
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than one child node." & vbCrLf)
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case "Inverse"
                    'This should have  single matrix child node.
                    'Update this node with the Inverse of the child node matrix.
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 1 Then
                            Dim ChildNodeText As String = myNode.Nodes(0).Text
                            If MatrixData.ContainsKey(ChildNodeText) Then
                                'Generate the Matrix inverse:
                                Dim myMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(MatrixData(ChildNodeText).Data)
                                Try
                                    Dim Inverse = myMatrix.Inverse
                                    MatrixData(ItemName).NCols = Inverse.RowCount
                                    MatrixData(ItemName).NRows = Inverse.ColumnCount
                                    Dim RowNo As Integer
                                    Dim ColNo As Integer
                                    For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                        For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                            MatrixData(ItemName).Data(RowNo, ColNo) = Inverse(RowNo, ColNo)
                                        Next
                                    Next
                                    UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                                Catch ex As Exception
                                    Main.Message.AddWarning("Error generating the matrix inverse: " & ex.Message & vbCrLf)
                                End Try
                            Else
                                Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix child node." & vbCrLf)
                            End If
                        ElseIf myNode.Nodes.Count = 0 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                        Else
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than one child node." & vbCrLf)
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case "Cholesky"
                    'This should have  single matrix child node.
                    'Update this node with the Cholesky factorization of the child node matrix.
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 1 Then
                            Dim ChildNodeText As String = myNode.Nodes(0).Text
                            If MatrixData.ContainsKey(ChildNodeText) Then
                                'Generate the Matrix inverse:
                                Dim myMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(MatrixData(ChildNodeText).Data)
                                Try
                                    Dim Chol = myMatrix.Cholesky
                                    MatrixData(ItemName).NCols = Chol.Factor.RowCount
                                    MatrixData(ItemName).NRows = Chol.Factor.ColumnCount
                                    Dim RowNo As Integer
                                    Dim ColNo As Integer
                                    For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                        For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                            MatrixData(ItemName).Data(RowNo, ColNo) = Chol.Factor(RowNo, ColNo)
                                        Next
                                    Next
                                    UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                                Catch ex As Exception
                                    Main.Message.AddWarning("Error generating the matrix inverse: " & ex.Message & vbCrLf)
                                End Try
                            Else
                                Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix child node." & vbCrLf)
                            End If
                        ElseIf myNode.Nodes.Count = 0 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                        Else
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than one child node." & vbCrLf)
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case "Transposed Cholesky"
                    'This should have a single matrix child node.
                    'Update this node with the Transposed Cholesky factorization of the child node matrix.
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 1 Then
                            Dim ChildNodeText As String = myNode.Nodes(0).Text
                            If MatrixData.ContainsKey(ChildNodeText) Then
                                'Generate the Matrix inverse:
                                Dim myMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(MatrixData(ChildNodeText).Data)
                                Try
                                    Dim Chol = myMatrix.Cholesky
                                    MatrixData(ItemName).NCols = Chol.Factor.ColumnCount
                                    MatrixData(ItemName).NRows = Chol.Factor.RowCount
                                    Dim RowNo As Integer
                                    Dim ColNo As Integer
                                    For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                        For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                            MatrixData(ItemName).Data(RowNo, ColNo) = Chol.Factor(ColNo, RowNo)
                                        Next
                                    Next
                                    UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                                Catch ex As Exception
                                    Main.Message.AddWarning("Error generating the matrix inverse: " & ex.Message & vbCrLf)
                                End Try
                            Else
                                Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix child node." & vbCrLf)
                            End If
                        ElseIf myNode.Nodes.Count = 0 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                        Else
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than one child node." & vbCrLf)
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case "Add Scalar"
                    'This should have a matrix child node and a scalar child node.
                    'Update this node with the scalar added to the matrix.
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 2 Then
                            Dim FirstChildNodeText As String = myNode.Nodes(0).Text
                            Dim SecondChildNodeText As String = myNode.Nodes(1).Text
                            If OpInfo.ContainsKey(FirstChildNodeText) Then
                                If OpInfo.ContainsKey(SecondChildNodeText) Then
                                    If MatrixData.ContainsKey(FirstChildNodeText) Then
                                        If ScalarData.ContainsKey(SecondChildNodeText) Then
                                            'The first child node is the matrix and the second child node is the scalar.
                                            MatrixData(ItemName).NRows = MatrixData(FirstChildNodeText).NRows
                                            MatrixData(ItemName).NCols = MatrixData(FirstChildNodeText).NCols
                                            Dim RowNo As Integer
                                            Dim ColNo As Integer
                                            Dim Scalar As Double = ScalarData(SecondChildNodeText)
                                            For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                                For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                                    MatrixData(ItemName).Data(RowNo, ColNo) = MatrixData(FirstChildNodeText).Data(RowNo, ColNo) + Scalar
                                                Next
                                            Next
                                            UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                                        Else
                                            Main.Message.AddWarning("This Add Scalar node does not have the correct child node types. One matrix child node and one scalar child node are required." & vbCrLf)
                                        End If
                                    Else
                                        If ScalarData.ContainsKey(FirstChildNodeText) Then
                                            If MatrixData.ContainsKey(SecondChildNodeText) Then
                                                'The first child node is the scalar and the second child node is the matrix.
                                                MatrixData(ItemName).NRows = MatrixData(SecondChildNodeText).NRows
                                                MatrixData(ItemName).NCols = MatrixData(SecondChildNodeText).NCols
                                                Dim RowNo As Integer
                                                Dim ColNo As Integer
                                                Dim Scalar As Double = ScalarData(FirstChildNodeText)
                                                For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                                    For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                                        MatrixData(ItemName).Data(RowNo, ColNo) = MatrixData(SecondChildNodeText).Data(RowNo, ColNo) + Scalar
                                                    Next
                                                Next
                                                UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                                            Else
                                                Main.Message.AddWarning("This Add Scalar node does not have the correct child node types. One matrix child node and one scalar child node are required." & vbCrLf)
                                            End If
                                        Else
                                            Main.Message.AddWarning("The first child node is not a scalar or a matrix." & vbCrLf)
                                        End If
                                    End If
                                Else
                                    Main.Message.AddWarning("Unknown Child Node operation name: " & SecondChildNodeText & vbCrLf)
                                End If
                            Else
                                Main.Message.AddWarning("Unknown Child Node operation name: " & FirstChildNodeText & vbCrLf)
                            End If
                        ElseIf myNode.Nodes.Count = 0 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                        ElseIf myNode.Nodes.Count = 1 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " only has one child node." & vbCrLf)
                        Else
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than two child nodes." & vbCrLf)
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case "Multiply Scalar"
                    'This should have a matrix child node and a scalar child node.
                    'Update this node with the scalar added to the matrix.
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 2 Then
                            Dim FirstChildNodeText As String = myNode.Nodes(0).Text
                            Dim SecondChildNodeText As String = myNode.Nodes(1).Text
                            If OpInfo.ContainsKey(FirstChildNodeText) Then
                                If OpInfo.ContainsKey(SecondChildNodeText) Then
                                    If MatrixData.ContainsKey(FirstChildNodeText) Then
                                        If ScalarData.ContainsKey(SecondChildNodeText) Then
                                            'The first child node is the matrix and the second child node is the scalar.
                                            MatrixData(ItemName).NRows = MatrixData(FirstChildNodeText).NRows
                                            MatrixData(ItemName).NCols = MatrixData(FirstChildNodeText).NCols
                                            Dim RowNo As Integer
                                            Dim ColNo As Integer
                                            Dim Scalar As Double = ScalarData(SecondChildNodeText)
                                            For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                                For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                                    MatrixData(ItemName).Data(RowNo, ColNo) = MatrixData(FirstChildNodeText).Data(RowNo, ColNo) * Scalar
                                                Next
                                            Next
                                            UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                                        Else
                                            Main.Message.AddWarning("This Add Scalar node does not have the correct child node types. One matrix child node and one scalar child node are required." & vbCrLf)
                                        End If
                                    Else
                                        If ScalarData.ContainsKey(FirstChildNodeText) Then
                                            If MatrixData.ContainsKey(SecondChildNodeText) Then
                                                'The first child node is the scalar and the second child node is the matrix.
                                                MatrixData(ItemName).NRows = MatrixData(SecondChildNodeText).NRows
                                                MatrixData(ItemName).NCols = MatrixData(SecondChildNodeText).NCols
                                                Dim RowNo As Integer
                                                Dim ColNo As Integer
                                                Dim Scalar As Double = ScalarData(FirstChildNodeText)
                                                For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                                    For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                                        MatrixData(ItemName).Data(RowNo, ColNo) = MatrixData(SecondChildNodeText).Data(RowNo, ColNo) * Scalar
                                                    Next
                                                Next
                                                UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                                            Else
                                                Main.Message.AddWarning("This Add Scalar node does not have the correct child node types. One matrix child node and one scalar child node are required." & vbCrLf)
                                            End If
                                        Else
                                            Main.Message.AddWarning("The first child node is not a scalar or a matrix." & vbCrLf)
                                        End If
                                    End If
                                Else
                                    Main.Message.AddWarning("Unknown Child Node operation name: " & SecondChildNodeText & vbCrLf)
                                End If
                            Else
                                Main.Message.AddWarning("Unknown Child Node operation name: " & FirstChildNodeText & vbCrLf)
                            End If
                        ElseIf myNode.Nodes.Count = 0 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                        ElseIf myNode.Nodes.Count = 1 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " only has one child node." & vbCrLf)
                        Else
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than two child nodes." & vbCrLf)
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case "Divide Scalar"
                    'This should have a matrix child node and a scalar child node.
                    'Update this node with the scalar added to the matrix.
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 2 Then
                            Dim FirstChildNodeName As String = myNode.Nodes(0).Text
                            Dim SecondChildNodeName As String = myNode.Nodes(1).Text
                            If OpInfo.ContainsKey(FirstChildNodeName) Then
                                If OpInfo.ContainsKey(SecondChildNodeName) Then
                                    If MatrixData.ContainsKey(FirstChildNodeName) Then
                                        If ScalarData.ContainsKey(SecondChildNodeName) Then
                                            'The first child node is the matrix and the second child node is the scalar.
                                            MatrixData(ItemName).NRows = MatrixData(FirstChildNodeName).NRows
                                            MatrixData(ItemName).NCols = MatrixData(FirstChildNodeName).NCols
                                            Dim RowNo As Integer
                                            Dim ColNo As Integer
                                            Dim Scalar As Double = ScalarData(SecondChildNodeName)
                                            For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                                For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                                    MatrixData(ItemName).Data(RowNo, ColNo) = MatrixData(FirstChildNodeName).Data(RowNo, ColNo) / Scalar
                                                Next
                                            Next
                                            UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                                        Else
                                            Main.Message.AddWarning("This Add Scalar node does not have the correct child node types. One matrix child node and one scalar child node are required." & vbCrLf)
                                        End If
                                    Else
                                        If ScalarData.ContainsKey(FirstChildNodeName) Then
                                            If MatrixData.ContainsKey(SecondChildNodeName) Then
                                                'The first child node is the scalar and the second child node is the matrix.
                                                MatrixData(ItemName).NRows = MatrixData(SecondChildNodeName).NRows
                                                MatrixData(ItemName).NCols = MatrixData(SecondChildNodeName).NCols
                                                Dim RowNo As Integer
                                                Dim ColNo As Integer
                                                Dim Scalar As Double = ScalarData(FirstChildNodeName)
                                                For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                                    For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                                        MatrixData(ItemName).Data(RowNo, ColNo) = MatrixData(SecondChildNodeName).Data(RowNo, ColNo) / Scalar
                                                    Next
                                                Next
                                                UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                                            Else
                                                Main.Message.AddWarning("This Add Scalar node does not have the correct child node types. One matrix child node and one scalar child node are required." & vbCrLf)
                                            End If
                                        Else
                                            Main.Message.AddWarning("The first child node is not a scalar or a matrix." & vbCrLf)
                                        End If
                                    End If
                                Else
                                    Main.Message.AddWarning("Unknown Child Node operation name: " & SecondChildNodeName & vbCrLf)
                                End If
                            Else
                                Main.Message.AddWarning("Unknown Child Node operation name: " & FirstChildNodeName & vbCrLf)
                            End If
                        ElseIf myNode.Nodes.Count = 0 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                        ElseIf myNode.Nodes.Count = 1 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " only has one child node." & vbCrLf)
                        Else
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than two child nodes." & vbCrLf)
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case "Add Matrix"
                    'This should have two matrix child nodes.
                    'Update this node with the two child node matrices added.
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 2 Then
                            Dim FirstChildNodeText As String = myNode.Nodes(0).Text
                            Dim SecondChildNodeText As String = myNode.Nodes(1).Text
                            If OpInfo.ContainsKey(FirstChildNodeText) Then
                                If OpInfo.ContainsKey(SecondChildNodeText) Then
                                    If MatrixData.ContainsKey(FirstChildNodeText) Then
                                        If MatrixData.ContainsKey(SecondChildNodeText) Then
                                            'The first and second child nodes contain matrices.
                                            If MatrixData(FirstChildNodeText).NRows = MatrixData(SecondChildNodeText).NRows Then
                                                If MatrixData(FirstChildNodeText).NCols = MatrixData(SecondChildNodeText).NCols Then
                                                    'The two child node matrices contain teh same number of rows and columns.
                                                    MatrixData(ItemName).NRows = MatrixData(FirstChildNodeText).NRows
                                                    MatrixData(ItemName).NCols = MatrixData(FirstChildNodeText).NCols
                                                    Dim RowNo As Integer
                                                    Dim ColNo As Integer
                                                    For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                                        For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                                            MatrixData(ItemName).Data(RowNo, ColNo) = MatrixData(FirstChildNodeText).Data(RowNo, ColNo) + MatrixData(SecondChildNodeText).Data(RowNo, ColNo)
                                                        Next
                                                    Next
                                                    UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                                                Else
                                                    Main.Message.AddWarning("The first and second child node matrices do not contain the same number of columns." & FirstChildNodeText & vbCrLf)
                                                End If
                                            Else
                                                Main.Message.AddWarning("The first and second child node matrices do not contain the same number of rows." & FirstChildNodeText & vbCrLf)
                                            End If
                                        Else
                                            Main.Message.AddWarning("The second child node does not contain matrix data: " & FirstChildNodeText & vbCrLf)
                                        End If
                                    Else
                                        Main.Message.AddWarning("The first child node does not contain matrix data: " & FirstChildNodeText & vbCrLf)
                                    End If
                                Else
                                    Main.Message.AddWarning("Unknown Child Node operation name: " & SecondChildNodeText & vbCrLf)
                                End If
                            Else
                                Main.Message.AddWarning("Unknown Child Node operation name: " & FirstChildNodeText & vbCrLf)
                            End If
                        ElseIf myNode.Nodes.Count = 0 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                        ElseIf myNode.Nodes.Count = 1 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " only has one child node." & vbCrLf)
                        Else
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than two child nodes." & vbCrLf)
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case "Multiply Matrix"
                    'This should have two matrix child nodes.
                    'Update this node with the two child node matrices multiplied.
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 2 Then
                            Dim FirstChildNodeText As String = myNode.Nodes(0).Text
                            Dim SecondChildNodeText As String = myNode.Nodes(1).Text
                            If OpInfo.ContainsKey(FirstChildNodeText) Then
                                If OpInfo.ContainsKey(SecondChildNodeText) Then
                                    If MatrixData.ContainsKey(FirstChildNodeText) Then
                                        If MatrixData.ContainsKey(SecondChildNodeText) Then
                                            'The first and second child nodes contain matrices.
                                            'If MatrixData(FirstChildNodeText).NRows = MatrixData(SecondChildNodeText).NCols Then
                                            If MatrixData(FirstChildNodeText).NCols = MatrixData(SecondChildNodeText).NRows Then
                                                Try
                                                    Dim myMatrix1 = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(MatrixData(FirstChildNodeText).Data)
                                                    Dim myMatrix2 = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(MatrixData(SecondChildNodeText).Data)
                                                    Dim M1xM2 = myMatrix1.Multiply(myMatrix2)
                                                    MatrixData(ItemName).NRows = M1xM2.RowCount
                                                    MatrixData(ItemName).NCols = M1xM2.ColumnCount
                                                    Dim RowNo As Integer
                                                    Dim ColNo As Integer
                                                    For RowNo = 0 To MatrixData(ItemName).NRows - 1
                                                        For ColNo = 0 To MatrixData(ItemName).NCols - 1
                                                            MatrixData(ItemName).Data(RowNo, ColNo) = M1xM2(RowNo, ColNo)
                                                        Next
                                                    Next
                                                    UpdateNode(myNode.Parent) 'Propagate the updates up through the tree.
                                                Catch ex As Exception
                                                    Main.Message.AddWarning("Error multiplying matrices: " & vbCrLf & ex.Message & vbCrLf)
                                                End Try
                                            Else
                                                'Main.Message.AddWarning("The matrices can not be multiplied. The number of rows in the first matrix is not the same as the number of columns in the seconds matrix." & vbCrLf)
                                                Main.Message.AddWarning("The matrices can not be multiplied. The number of columns in the first matrix is not the same as the number of rows in the second matrix." & vbCrLf)
                                            End If
                                        Else
                                            Main.Message.AddWarning("The second child node does not contain matrix data: " & FirstChildNodeText & vbCrLf)
                                        End If
                                    Else
                                        Main.Message.AddWarning("The first child node does not contain matrix data: " & FirstChildNodeText & vbCrLf)
                                    End If
                                Else
                                    Main.Message.AddWarning("Unknown Child Node operation name: " & SecondChildNodeText & vbCrLf)
                                End If
                            Else
                                Main.Message.AddWarning("Unknown Child Node operation name: " & FirstChildNodeText & vbCrLf)
                            End If
                        ElseIf myNode.Nodes.Count = 0 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                        ElseIf myNode.Nodes.Count = 1 Then
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " only has one child node." & vbCrLf)
                        Else
                            Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than two child nodes." & vbCrLf)
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case "Covariance"
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 1 Then
                            Dim ChildNodeText As String = myNode.Nodes(0).Text
                            If MatrixData.ContainsKey(ChildNodeText) Then
                                'Calculate the Covariance Matrix
                                MatrixData(ItemName).NRows = MatrixData(ChildNodeText).NCols 'The Covariance Matrix will be NCols x NCols where NCols is the number of columns in the input matrix.
                                MatrixData(ItemName).NCols = MatrixData(ChildNodeText).NCols
                                Covariance(MatrixData(ChildNodeText).Data, MatrixData(ItemName).Data)
                            Else
                                Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix child node." & vbCrLf)
                            End If
                        Else
                            If myNode.Nodes.Count = 0 Then
                                Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                            Else
                                Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than one child node." & vbCrLf)
                            End If
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case "Correlation"
                    If MatrixData.ContainsKey(ItemName) Then
                        If myNode.Nodes.Count = 1 Then
                            Dim ChildNodeText As String = myNode.Nodes(0).Text
                            If MatrixData.ContainsKey(ChildNodeText) Then
                                'Calculate the Correlation Matrix
                                MatrixData(ItemName).NRows = MatrixData(ChildNodeText).NCols 'The Correlation Matrix will be NCols x NCols where NCols is the number of columns in the input matrix.
                                MatrixData(ItemName).NCols = MatrixData(ChildNodeText).NCols
                                Correlation(MatrixData(ChildNodeText).Data, MatrixData(ItemName).Data)
                            Else
                                Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix child node." & vbCrLf)
                            End If
                        Else
                            If myNode.Nodes.Count = 0 Then
                                Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a child node." & vbCrLf)
                            Else
                                Main.Message.AddWarning("The Matrix Process node named " & ItemName & " has more than one child node." & vbCrLf)
                            End If
                        End If
                    Else
                        Main.Message.AddWarning("The Matrix Process node named " & ItemName & " does not have a Matrix data item." & vbCrLf)
                    End If

                Case Else
                    Main.Message.AddWarning("Unknown operation type: " & OpInfo(ItemName).Type & vbCrLf)
            End Select
        Else
            Main.Message.AddWarning("Unknown operation name: " & ItemName & vbCrLf)
        End If
    End Sub

    Private Sub Covariance(ByRef InMat(,) As Double, ByRef CovMat(,) As Double)
        'Calculate the covariance matrix (CovMat) for the Input Matrix (InMat).

        Dim NRows As Integer = InMat.GetLength(0) 'The number of rows in InMat.
        Dim NCols As Integer = InMat.GetLength(1) 'The number of variables to calculate the covariance (Number of columns in InMat)

        Dim Col1 As Integer
        Dim Col2 As Integer
        Dim Row As Integer
        Dim Avg(0 To NCols - 1) As Double 'Array to hold the column averages in InMat.
        Dim A As Double 'A = Col1 - Mean1
        Dim B As Double 'B = Col2 - Mean2
        Dim SumAB As Double 'AB = (Col1 - Mean1) x (Col2 - Mean2)

        'Calculate the column averages
        For Col1 = 0 To NCols - 1
            Avg(Col1) = 0
            For Row = 0 To NRows - 1
                Avg(Col1) += InMat(Row, Col1)
            Next
            Avg(Col1) /= NRows
        Next

        For Col1 = 0 To NCols - 1
            CovMat(Col1, Col1) = 1 'The CovMat contains the unit diagonal - the covariance of a variable with itself has a value of 1.
            For Col2 = Col1 + 1 To NCols - 1
                SumAB = 0
                For Row = 0 To NRows - 1
                    A = InMat(Row, Col1) - Avg(Col1)
                    B = InMat(Row, Col2) - Avg(Col2)
                    SumAB += A * B
                Next
                CovMat(Col1, Col2) = SumAB / NRows
                CovMat(Col2, Col1) = CovMat(Col1, Col2) 'The Covariance matrix is symmetric
            Next
        Next

        'Dim NVars As Integer = InMat.GetLength(1) 'The number of variables to calculate the covariance (Number of columns in InMat)
        'Dim NRows As Integer = InMat.GetLength(0) 'The number of rows in InMat.
        'Dim Var1 As Integer
        'Dim Var2 As Integer
        'Dim Row As Integer
        'Dim Avg(0 To NVars - 1) As Double 'Array to hold the column averages in InMat.
        'Dim A As Double
        'Dim B As Double
        'Dim SumAB As Double

        ''Calculate the column averages
        'For Var1 = 0 To NVars - 1
        '    Avg(Var1) = 0
        '    For Row = 0 To NRows - 1
        '        Avg(Var1) += InMat(Row, Var1)
        '    Next
        '    Avg(Var1) /= NRows
        'Next

        'For Var1 = 0 To NVars - 1
        '    CorMat(Var1, Var1) = 1 'The CorMat contains the unit diagonal - the correlation of a variable with itself has a value of 1.
        '    For Var2 = Var1 + 1 To NVars - 1
        '        SumAB = 0
        '        For Row = 0 To NRows - 1
        '            A = InMat(Row, Var1) - Avg(Var1)
        '            B = InMat(Row, Var2) - Avg(Var2)
        '            SumAB += A * B
        '        Next
        '        CorMat(Var1, Var2) = SumAB / NRows
        '        CorMat(Var2, Var1) = CorMat(Var1, Var2) 'The Correlation matrix is symmetric
        '    Next
        'Next

    End Sub

    Private Sub Correlation(ByRef InMat(,) As Double, ByRef CorMat(,) As Double)
        'Calculate the correlation matrix (CorMat) for the Input Matrix (InMat).

        Dim NRows As Integer = InMat.GetLength(0) 'The number of rows in InMat.
        Dim NCols As Integer = InMat.GetLength(1) 'The number of variables to calculate the covariance (Number of columns in InMat)

        Dim Col1 As Integer
        Dim Col2 As Integer
        Dim Row As Integer
        Dim Avg(0 To NCols - 1) As Double 'Array to hold the column averages in InMat.
        Dim A As Double 'A = Col1 - Mean1
        Dim B As Double 'B = Col2 - Mean2
        Dim SumAB As Double 'AB = (Col1 - Mean1) x (Col2 - Mean2)
        Dim SumAA As Double = 0 'AA = (Col1 - Mean1) Squared
        Dim SumBB As Double = 0 'BB = (Col2 - Mean2) Squared

        'Calculate the column averages
        For Col1 = 0 To NCols - 1
            Avg(Col1) = 0
            For Row = 0 To NRows - 1
                Avg(Col1) += InMat(Row, Col1)
            Next
            Avg(Col1) /= NRows
        Next

        For Col1 = 0 To NCols - 1
            CorMat(Col1, Col1) = 1 'The CorMat contains the unit diagonal - the correlation of a variable with itself has a value of 1.
            For Col2 = Col1 + 1 To NCols - 1
                SumAB = 0
                SumAA = 0
                SumBB = 0
                For Row = 0 To NRows - 1
                    A = InMat(Row, Col1) - Avg(Col1)
                    B = InMat(Row, Col2) - Avg(Col2)
                    SumAB += A * B
                    SumAA += A * A
                    SumBB += B * B
                Next
                CorMat(Col1, Col2) = SumAB / Math.Sqrt(SumAA * SumBB)
                CorMat(Col2, Col1) = CorMat(Col1, Col2) 'The Correlation matrix is symmetric
            Next
        Next
    End Sub

    'Private Sub ApplyOperation(ByVal OpName As String)
    Private Function ApplyOperation(ByVal OpName As String) As Boolean
        'Apply the Operation named OpName.
        'Return True if the Operation was completed OK.
        If OpInfo.ContainsKey(OpName) Then
            Main.Message.AddWarning("The OpInfo dictionary does not contain an operation named: " & OpName & vbCrLf)
            Return False
        Else
            Select Case OpInfo(OpName).Type
                Case "Matrix Operation Sequence"
                    'No operation to apply
                    Return False
                Case "Scalar"
                    'No operation to apply
                    Return False
                Case "Matrix"
                    'No operation to apply
                    Return False
                Case "Open Matrix File"
                    'No operation to apply
                    Return False
                Case "User Defined Scalar"
                    'No operation to apply
                    Return False
                Case "User Defined Matrix"
                    'No operation to apply
                    Return False
                Case "Process"
                    'No operation to apply
                    Return False
                Case "Scalar Process"

                Case "Matrix Process"

                Case "Transpose"

                Case "Inverse"

                Case "Cholesky"

                Case "Transposed Cholesky"

                Case "Add Scalar"

                Case "Multiply Scalar"

                Case "Divide Scalar"

                Case "Add Matrix"

                Case "Multiply Matrix"

                Case Else
                    Main.Message.AddWarning("Unknown opration type: " & OpInfo(OpName).Type & vbCrLf)
                    Return False
            End Select
        End If
    End Function

    Private Sub txtMatrixItemNRows_LostFocus(sender As Object, e As EventArgs) Handles txtMatrixItemNRows.LostFocus

        Dim NewNRows As Integer = txtMatrixItemNRows.Text
        If NewNRows = MatrixData(SelItemName).NRows Then
            'No change - do not update the display.
        Else
            MatrixData(SelItemName).NRows = NewNRows
            ShowMatrixItem(SelItemName)
        End If
    End Sub

    Private Sub txtMatrixItemNCols_LostFocus(sender As Object, e As EventArgs) Handles txtMatrixItemNCols.LostFocus

        Dim NewNCols As Integer = txtMatrixItemNCols.Text
        If NewNCols = MatrixData(SelItemName).NCols Then
            'No change - do not update the display.
        Else
            MatrixData(SelItemName).NCols = NewNCols
            ShowMatrixItem(SelItemName)
        End If
    End Sub

    Private Sub btnCalcMatrixItem_Click(sender As Object, e As EventArgs) Handles btnCalcMatrixItem.Click
        'Recalculate the selected node value
        UpdateNode(SelNode)
        ShowItem(SelItemName)
    End Sub

    Private Sub rbCollection_CheckedChanged(sender As Object, e As EventArgs) Handles rbCollection.CheckedChanged
        If rbCollection.Checked Then cmbDataSource.Enabled = False
    End Sub

    Private Sub rbScalarCopy_CheckedChanged(sender As Object, e As EventArgs) Handles rbScalarCopy.CheckedChanged
        If rbScalarCopy.Checked Then
            cmbDataSource.Enabled = True
            cmbDataSource.Items.Clear()
            For Each item In ScalarData
                cmbDataSource.Items.Add(item.Key)
            Next
        End If
    End Sub

    Private Sub rbMatrixCopy_CheckedChanged(sender As Object, e As EventArgs) Handles rbMatrixCopy.CheckedChanged
        If rbMatrixCopy.Checked Then
            cmbDataSource.Enabled = True
            cmbDataSource.Items.Clear()
            For Each item In MatrixData
                cmbDataSource.Items.Add(item.Key)
            Next
        End If
    End Sub

    Private Sub btnMoveNodeUp_Click(sender As Object, e As EventArgs) Handles btnMoveNodeUp.Click
        'Move the selected item up in the Document Tree.

        If trvMatrixOps.SelectedNode Is Nothing Then
            'No node has been selected.
        Else
            Dim Node As TreeNode
            Node = trvMatrixOps.SelectedNode
            Dim index As Integer = Node.Index
            If index = 0 Then
                'Already at the first node.
                Node.TreeView.Focus()
            Else
                Dim Parent As TreeNode = Node.Parent
                Parent.Nodes.RemoveAt(index)
                Parent.Nodes.Insert(index - 1, Node)
                trvMatrixOps.SelectedNode = Node
                Node.TreeView.Focus()
            End If
        End If
    End Sub

    Private Sub btnMoveNodeDown_Click(sender As Object, e As EventArgs) Handles btnMoveNodeDown.Click
        'Move the selected item down in the Document Tree.

        If trvMatrixOps.SelectedNode Is Nothing Then
            'No node has been selected.
        Else
            Dim Node As TreeNode
            Node = trvMatrixOps.SelectedNode
            Dim index As Integer = Node.Index
            Dim Parent As TreeNode = Node.Parent
            If index < Parent.Nodes.Count - 1 Then
                Parent.Nodes.RemoveAt(index)
                Parent.Nodes.Insert(index + 1, Node)
                trvMatrixOps.SelectedNode = Node
                Node.TreeView.Focus()
            Else
                'Already at the last node.
                Node.TreeView.Focus()
            End If
        End If
    End Sub

    Private Sub btnPasteData_Click(sender As Object, e As EventArgs) Handles btnPasteData.Click
        'Paste data from the clipboard into dgvMatrixItem

        'Dim CBData As String
        Try
            'CBData = Clipboard.GetText
            Dim CBData As String = Clipboard.GetText
            Dim I, J As Integer
            Dim Lines() As String = CBData.Split(ControlChars.NewLine)
            Dim Items() As String
            Dim CC, Row, Col As Integer
            Row = dgvMatrixItem.SelectedCells(0).RowIndex
            Col = dgvMatrixItem.SelectedCells(0).ColumnIndex
            Dim DataVal As Double

            For I = 0 To Lines.Length - 1
                If Lines(I) <> "" Then
                    Items = Lines(I).Split(vbTab)
                    CC = Col
                    For J = 0 To Items.Length - 1
                        If CC > dgvMatrixItem.ColumnCount - 1 Then Exit Sub
                        If Row > dgvMatrixItem.Rows.Count - 1 Then Exit Sub

                        DataVal = Val(Items(J).TrimStart.Replace(",", ""))
                        'dgvMatrixItem.Item(CC, Row).Value = Val(Items(J).TrimStart.Replace(",", ""))
                        dgvMatrixItem.Item(CC, Row).Value = DataVal
                        'Also copy the data to MatrixData:
                        'MatrixData(SelItemName).Data(CC, Row) = DataVal
                        'MatrixData(SelItemName).Data(CC, Row) = Val(Items(J).TrimStart.Replace(",", ""))
                        'MatrixData(SelItemName).Data(Row, CC) = DataVal
                        MatrixData(SelDataName).Data(Row, CC) = DataVal
                        CC += 1
                    Next
                    Row += 1
                End If
            Next

        Catch ex As Exception
            Main.Message.AddWarning("Error pasting data: " & vbCrLf & ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub btnShowItem_Click(sender As Object, e As EventArgs) Handles btnShowItem.Click
        'Show the Node Item in a new window.

        If MatrixData.ContainsKey(SelDataName) Then
            OpenMatrixInNewWindow()
        ElseIf ScalarData.ContainsKey(SelDataName) Then
            OpenScalarInNewWindow()
        Else
            OpenNodeInNewWIndow()
        End If

    End Sub

    Private Sub OpenMatrixInNewWindow()
        'Open the selected Matrix in a new window.

        'SelNode     - the selected node
        'SelItemName - the selected item name
        'SelDataName - the selected data name
        If OpInfo.ContainsKey(SelItemName) Then
            If MatrixData.ContainsKey(SelDataName) Then 'This is a Matrix Data node
                If MatrixList.ContainsKey(SelItemName) Then
                    MatrixList(SelItemName).BringToFront()
                Else
                    Matrix = New frmMatrix
                    MatrixList.Add(SelItemName, Matrix)
                    MatrixList(SelItemName).ItemName = SelItemName
                    MatrixList(SelItemName).DataName = SelDataName
                    'MatrixList(SelItemName).myParent = Me
                    MatrixList(SelItemName).HostFormNo = FormNo 'Needed to run Main.MatrixOpsList(HostFormNo).SaveOpWindowSettings() or .MatrixWindowClosed()
                    MatrixList(SelItemName).Show()
                    MatrixList(SelItemName).ShowNodeInfo(OpInfo(SelItemName))
                    MatrixList(SelItemName).ShowMatrixInfo(MatrixData(SelDataName))
                    'MatrixList(SelItemName).Show()

                    Select Case OpInfo(SelItemName).Type
                        Case "Matrix"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(4)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(4)
                        Case "Open Matrix File"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(6)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(6)
                        Case "User Defined Matrix"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(10)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(10)
                        Case "Matrix Process"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(16)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(16)
                        Case "Transpose"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(18)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(18)
                        Case "Inverse"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(20)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(20)
                        Case "Cholesky"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(22)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(22)
                        Case "Transposed Cholesky"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(24)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(24)
                        Case "Add Scalar"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(26)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(26)
                        Case "Multiply Scalar"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(28)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(28)
                        Case "Divide Scalar"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(30)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(30)
                        Case "Covariance"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(42)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(42)
                        Case "Correlation"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(44)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(44)
                        Case "Add Matrix"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(32)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(32)
                        Case "Multiply Matrix"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(34)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(34)
                        Case "Matrix Copy"
                            MatrixList(SelItemName).pbIcon1.Image = ImageList1.Images(40)
                            MatrixList(SelItemName).pbIcon2.Image = ImageList1.Images(40)
                    End Select
                End If
                'Else
                '    Main.Message.AddWarning("No information was found for the selected node." & vbCrLf)
                'End If
                'ElseIf ScalarData.ContainsKey(SelDataName) Then 'This is a Scalar Data node

            Else 'This node contains no Matrix data.
                'Main.Message.AddWarning("The selected node does not contain matrix data." & vbCrLf)
            End If
        Else
            Main.Message.AddWarning("No information was found for the selected node." & vbCrLf)
        End If
    End Sub

    Private Sub OpenScalarInNewWindow()
        'Open the selected Scalar in a new window.

        'SelNode     - the selected node
        'SelItemName - the selected item name
        'SelDataName - the selected data name

        If OpInfo.ContainsKey(SelItemName) Then
            If ScalarData.ContainsKey(SelDataName) Then
                If ScalarList.ContainsKey(SelItemName) Then
                    ScalarList(SelItemName).BringToFront()
                Else
                    Scalar = New frmScalar
                    ScalarList.Add(SelItemName, Scalar)
                    ScalarList(SelItemName).ItemName = SelItemName
                    ScalarList(SelItemName).DataName = SelDataName
                    ScalarList(SelItemName).HostFormNo = FormNo  'Needed to run Main.MatrixOpsList(HostFormNo).SaveOpWindowSettings() or .ScalarWindowClosed()
                    ScalarList(SelItemName).Show()
                    ScalarList(SelItemName).ShowNodeInfo(OpInfo(SelItemName))
                    ScalarList(SelItemName).txtScalarName.Text = SelDataName
                    ScalarList(SelItemName).txtScalarItem.Text = ScalarData(SelDataName)

                    Select Case OpInfo(SelItemName).Type
                        Case "Scalar"
                            NodeList(SelItemName).pbIcon1.Image = ImageList1.Images(2)

                        Case "User Defined Scalar"
                            NodeList(SelItemName).pbIcon1.Image = ImageList1.Images(8)

                        Case "Scalar Process"
                            NodeList(SelItemName).pbIcon1.Image = ImageList1.Images(14)

                        Case "Scalar Copy"
                            NodeList(SelItemName).pbIcon1.Image = ImageList1.Images(38)

                    End Select
                End If
            Else

            End If
        Else
            Main.Message.AddWarning("No information was found for the selected node." & vbCrLf)
        End If
    End Sub

    Private Sub OpenNodeInNewWIndow()
        'Open te selected no value node in a new window.

        'SelNode     - the selected node
        'SelItemName - the selected item name
        'SelDataName - the selected data name

        If OpInfo.ContainsKey(SelItemName) Then
            If NodeList.ContainsKey(SelItemName) Then
                NodeList(SelItemName).BringToFront()
            Else
                Node = New frmNode
                NodeList.Add(SelItemName, Node)
                NodeList(SelItemName).ItemName = SelItemName
                NodeList(SelItemName).HostFormNo = FormNo 'Needed to run Main.MatrixOpsList(HostFormNo).SaveOpWindowSettings() or .NodeWindowClosed()
                NodeList(SelItemName).Show()
                NodeList(SelItemName).ShowNodeInfo(OpInfo(SelItemName))

                Select Case OpInfo(SelItemName).Type
                    Case "Matrix Operation Sequence"
                        NodeList(SelItemName).pbIcon1.Image = ImageList1.Images(0)

                    Case "Process"
                        NodeList(SelItemName).pbIcon1.Image = ImageList1.Images(14)

                    Case "Collection"
                        NodeList(SelItemName).pbIcon1.Image = ImageList1.Images(36)

                End Select
            End If
        Else
            Main.Message.AddWarning("No information was found for the selected node." & vbCrLf)
        End If

    End Sub

    'Private Sub Matrix_FormHasClosed(NodeName As String, Left As Integer, Top As Integer, Height As Integer, Width As Integer, SelectedTab As Integer) Handles Matrix.FormHasClosed
    '    OpInfo(NodeName).Left = Left
    '    OpInfo(NodeName).Top = Top
    '    OpInfo(NodeName).Height = Height
    '    OpInfo(NodeName).Width = Width
    '    OpInfo(NodeName).SelectedTab = SelectedTab
    'End Sub

    'Private Sub Matrix_SaveSettings(NodeName As String, Left As Integer, Top As Integer, Height As Integer, Width As Integer, SelectedTab As Integer) Handles Matrix.SaveSettings
    '    OpInfo(NodeName).Left = Left
    '    OpInfo(NodeName).Top = Top
    '    OpInfo(NodeName).Height = Height
    '    OpInfo(NodeName).Width = Width
    '    OpInfo(NodeName).SelectedTab = SelectedTab
    'End Sub

    Public Sub SaveOpWindowSettings(ByVal NodeName As String, ByVal Left As Integer, ByVal Top As Integer, ByVal Height As Integer, ByVal Width As Integer, ByVal SelectedTab As Integer)
        OpInfo(NodeName).Left = Left
        OpInfo(NodeName).Top = Top
        OpInfo(NodeName).Height = Height
        OpInfo(NodeName).Width = Width
        OpInfo(NodeName).SelectedTab = SelectedTab
        'Main.Message.Add("SaveOpWindowSettings: NodeName = " & NodeName & "  Left = " & Left & "  Top = " & Top & "  Height = " & Height & "  Width = " & Width & "  SelectedTab = " & SelectedTab & vbCrLf)
    End Sub

    'Private Sub Matrix_FormHasClosed(NodeName As String) Handles Matrix.FormHasClosed
    '    Main.Message.Add("Matrix Form Closed - NodeName:" & NodeName & vbCrLf)
    '    If MatrixList.ContainsKey(NodeName) Then
    '        MatrixList.Remove(NodeName)
    '        Main.Message.Add("MatrixList.Remove(" & NodeName & ")" & vbCrLf)
    '    End If
    'End Sub

    Public Sub MatrixWindowClosed(ByVal NodeName As String)
        'Main.Message.Add("MatrixWindowClosed: NodeName = " & NodeName & vbCrLf)
        If MatrixList.ContainsKey(NodeName) Then
            MatrixList.Remove(NodeName)
            'Main.Message.Add("MatrixList.Remove(NodeName): NodeName = " & NodeName & vbCrLf)
        End If
    End Sub

    Public Sub ScalarWindowClosed(ByVal NodeName As String)
        If ScalarList.ContainsKey(NodeName) Then
            ScalarList.Remove(NodeName)
        End If
    End Sub

    Public Sub NodeWindowClosed(ByVal NodeName As String)
        If NodeList.ContainsKey(NodeName) Then
            NodeList.Remove(NodeName)
        End If
    End Sub





    'Private Sub Matrix_FormSizeChanged(NodeName As String, Height As Integer, Width As Integer) Handles Matrix.FormSizeChanged
    '    OpInfo(NodeName).Height = Height
    '    OpInfo(NodeName).Width = Width
    '    Main.Message.Add("Matrix form size changed: width = " & Width & " height = " & Height & vbCrLf)
    'End Sub

    'Private Sub Matrix_FormMoved(NodeName As String, Left As Integer, Top As Integer) Handles Matrix.FormMoved
    '    OpInfo(NodeName).Left = Left
    '    OpInfo(NodeName).Top = Top
    '    Main.Message.Add("Matrix form position changed: left = " & Left & " top = " & Top & vbCrLf)
    'End Sub

    '

    'Private Function NewMatrixDisplay() As Integer
    '    'Open a new Matrix display window, or reuse an existing one if available.
    '    'The index number of the new form in MatrixList() is returned.

    '    Matrix = New frmMatrix
    '    If MatrixList.Count = 0 Then
    '        MatrixList.Add(Matrix)
    '        MatrixList(0).FormNo = 0
    '        MatrixList(0).Show
    '        Return 0 'The new Matrix is at position 0 in MatrixList()
    '    Else
    '        Dim I As Integer
    '        Dim FormAdded As Boolean = False
    '        For I = 0 To MatrixList.Count - 1 'Check if there are closed forms in MatrixList. They can be re-used.
    '            If IsNothing(MatrixList(I)) Then
    '                MatrixList(I) = Matrix
    '                MatrixList(I).FormNo = I
    '                MatrixList(I).Show
    '                FormAdded = True
    '                Return I 'The new Matrix is at position I in MatrixList()
    '                Exit For
    '            End If
    '        Next
    '        If FormAdded = False Then 'Add a new form to MatrixList
    '            Dim FormNo As Integer
    '            MatrixList.Add(Matrix)
    '            FormNo = MatrixList.Count - 1
    '            MatrixList(FormNo).FormNo = FormNo
    '            MatrixList(FormNo).Show
    '            Return FormNo 'The new Matrix is at position FormNo in MatrixList()
    '        End If
    '    End If
    'End Function

    Private Sub btnCheckNodeCopyLists_Click(sender As Object, e As EventArgs) Handles btnCheckNodeCopyLists.Click
        'Check the Node Copy lists.

        Dim NodeCopyName As String 'The name of a node that is a Scalar Copy or a Matrix Copy
        Dim CopiedNodeName As String 'The name of the node that has been copied.
        Dim I As Integer

        For Each item In OpInfo
            If item.Value.Type = "Scalar Copy" Then 'Check if the Copied Node CopyList is missing the Scalar Copy Node entry.
                NodeCopyName = item.Key
                CopiedNodeName = item.Value.Text
                If OpInfo(CopiedNodeName).CopyList.Contains(NodeCopyName) Then
                    'The Scalar Copy node is in the CopyList of the Copied node.
                Else
                    OpInfo(CopiedNodeName).CopyList.Add(NodeCopyName) 'Add the Scalar Copy node name to the CopyList of the Copied node.
                End If
            ElseIf item.Value.Type = "Matrix Copy" Then  'Check if the Copied Node CopyList is missing the Matrix Copy Node entry.
                NodeCopyName = item.Key
                CopiedNodeName = item.Value.Text
                If OpInfo(CopiedNodeName).CopyList.Contains(NodeCopyName) Then
                    'The Matrix Copy node is in the CopyList of the Copied node.
                Else
                    OpInfo(CopiedNodeName).CopyList.Add(NodeCopyName) 'Add the Matrix Copy node name to the CopyList of the Copied node.
                End If
            End If
            'For Each copyItem In item.Value.CopyList
            '    If OpInfo.ContainsKey(copyItem) Then
            '        'The item in the CopyList exists.
            '    Else
            '        'The item in the CopyList no longer exists.

            '    End If
            'Next

            'Check each CopyList for entries that no longer exist.
            For I = item.Value.CopyList.Count - 1 To 0 Step -1
                If OpInfo.ContainsKey(item.Value.CopyList(I)) Then
                    'The item in the CopyList exists.
                Else
                    'The item at index I no longer exists. Remove it from the list.
                    item.Value.CopyList.RemoveAt(I)
                End If
            Next
        Next
    End Sub

    Private Sub btnApplyChanges_Click(sender As Object, e As EventArgs) Handles btnApplyChanges.Click
        'Apply the changes made to the Node Name and Description

        'SelNode     - the selected node
        'SelItemName - the selected item name
        'SelDataName - the selected data name

        If OpInfo(SelItemName).Description = txtEditNodeDescr.Text Then
            'The Node description has not changed.
        Else
            'Update the Node description:
            OpInfo(SelItemName).Description = txtEditNodeDescr.Text
            If MatrixData.ContainsKey(SelItemName) Then
                MatrixData(SelItemName).Description = txtEditNodeDescr.Text
            End If
        End If

        If SelItemName = txtEditNodeName.Text Then
            'The Node Name has not changed.
        Else
            Dim NewNodeName As String = txtEditNodeName.Text.Trim
            If OpInfo.ContainsKey(NewNodeName) Then
                Main.Message.AddWarning("The new node name is already used: " & NewNodeName & vbCrLf)
            Else
                Dim OldNodeName As String = SelItemName
                OpInfo.Add(NewNodeName, New MatrixOperationInfo)
                OpInfo(NewNodeName) = OpInfo(OldNodeName) 'Copy the old node information to the new node with the new name.
                OpInfo.Remove(OldNodeName) 'Remove the old node entry.
                SelItemName = NewNodeName

                Dim myNode As TreeNode() = trvMatrixOps.Nodes.Find(OldNodeName, True) 'Find all nodes with the Old Node Name
                If myNode.Count = 0 Then
                    Main.Message.AddWarning("The node with the old name: " & OldNodeName & " was not found in the node tree." & vbCrLf)
                ElseIf myNode.Count = 1 Then
                    myNode(0).Name = NewNodeName 'Update the node name.
                Else
                    Main.Message.AddWarning("The node with the old name: " & OldNodeName & " was found " & myNode.Count & " times " & " in the node tree." & vbCrLf)
                    Main.Message.AddWarning("Only one node with this name was expected." & vbCrLf)
                    For Each nodeItem In myNode
                        nodeItem.Name = NewNodeName
                    Next
                    Main.Message.AddWarning("The name of the Copied Node was updated in each." & vbCrLf)
                End If

                If OpInfo(NewNodeName).Type = "Scalar Copy" Then
                    'Keep the Node Text - it is the name of the Copied node.
                ElseIf OpInfo(NewNodeName).Type = "Matrix Copy" Then
                    'Keep the Node Text - it is the name of the Copied node.
                Else
                    OpInfo(NewNodeName).Text = NewNodeName 'Update the Node Text to the new Node Name.
                    SelDataName = NewNodeName
                    txtEditNodeText.Text = NewNodeName
                    If myNode.Count = 0 Then
                    ElseIf myNode.Count = 1 Then
                        myNode(0).Text = NewNodeName 'Update the node name.
                    Else
                        For Each nodeItem In myNode
                            nodeItem.Text = NewNodeName
                        Next
                    End If
                End If

                If ScalarData.ContainsKey(OldNodeName) Then
                    ScalarData.Add(NewNodeName, ScalarData(OldNodeName))
                    ScalarData.Remove(OldNodeName)
                ElseIf MatrixData.ContainsKey(OldNodeName) Then
                    MatrixData.Add(NewNodeName, New MatrixInfo)
                    MatrixData(NewNodeName) = MatrixData(OldNodeName)
                    MatrixData.Remove(OldNodeName)
                    MatrixData(NewNodeName).Name = NewNodeName
                Else

                End If

                'Update any nodes in the CopyList:
                For Each item In OpInfo(NewNodeName).CopyList
                    If item = "" Then
                        'Blank item - ignore
                    Else
                        If OpInfo(item).Type = "Scalar Copy" Then
                            OpInfo(item).Text = NewNodeName 'Update the name of the Node Copy.

                            Dim myNodeCopy As TreeNode() = trvMatrixOps.Nodes.Find(item, True) 'Find all nodes with the name on the CopyList
                            If myNodeCopy.Count = 0 Then
                                Main.Message.AddWarning("The node in the Copy List named " & item & " was not found in the node tree." & vbCrLf)
                            ElseIf myNodeCopy.Count = 1 Then
                                myNodeCopy(0).Text = NewNodeName 'Update the name of the copied node.
                            Else
                                Main.Message.AddWarning("The node in the Copy List named " & item & " was found " & myNodeCopy.Count & " times " & " in the node tree." & vbCrLf)
                                Main.Message.AddWarning("Only one node with this name was expected." & vbCrLf)
                                For Each nodeItem In myNodeCopy
                                    nodeItem.Text = NewNodeName
                                Next
                                Main.Message.AddWarning("The name of the Copied Node was updated in each." & vbCrLf)
                            End If

                        ElseIf OpInfo(item).Type = "Matrix Copy" Then

                            OpInfo(item).Text = NewNodeName 'Update the name of the Node Copy.

                            Dim myNodeCopy As TreeNode() = trvMatrixOps.Nodes.Find(item, True) 'Find all nodes with the name on the CopyList
                            If myNodeCopy.Count = 0 Then
                                Main.Message.AddWarning("The node in the Copy List named " & item & " was not found in the node tree." & vbCrLf)
                            ElseIf myNodeCopy.Count = 1 Then
                                myNodeCopy(0).Text = NewNodeName 'Update the name of the copied node.
                            Else
                                Main.Message.AddWarning("The node in the Copy List named " & item & " was found " & myNodeCopy.Count & " times " & " in the node tree." & vbCrLf)
                                Main.Message.AddWarning("Only one node with this name was expected." & vbCrLf)
                                For Each nodeItem In myNodeCopy
                                    nodeItem.Text = NewNodeName
                                Next
                                Main.Message.AddWarning("The name of the Copied Node was updated in each." & vbCrLf)
                            End If

                        Else
                            Main.Message.AddWarning("The node named " & item & " in the Copy List is not a Scalar or Matrix copy." & vbCrLf)
                        End If
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub btnShowNodeList_Click(sender As Object, e As EventArgs) Handles btnShowNodeList.Click
        'Show the Node List Information in the Messge window.

        For Each item In OpInfo
            Main.Message.Add(vbCrLf & "Node Information: ----------------------------------------------" & vbCrLf)
            Main.Message.Add("Node name: " & item.Key & vbCrLf)
            Main.Message.Add("Node text: " & item.Value.Text & vbCrLf)
            Main.Message.Add("Node text: " & item.Value.Description & vbCrLf)
            Main.Message.Add("Node copy list: " & vbCrLf)
            For Each copyItem In item.Value.CopyList
                Main.Message.Add("  " & copyItem & vbCrLf)
            Next
            Main.Message.Add("END Node Information: ------------------------------------------" & vbCrLf)
        Next
    End Sub

    Private Sub btnCleanupNodeList_Click(sender As Object, e As EventArgs) Handles btnCleanupNodeList.Click
        'Remove unused node entries from the node list

        Dim I As Integer
        Dim NodeName As String
        Dim myNode As TreeNode() '= trvMatrixOps.Nodes.Find(OldNodeName, True) 'Find all nodes with the Old Node Name
        For I = OpInfo.Count - 1 To 0 Step -1
            NodeName = OpInfo.Keys(I)
            myNode = trvMatrixOps.Nodes.Find(NodeName, True)
            If myNode.Count = 0 Then
                OpInfo.Remove(NodeName)
                Main.Message.AddWarning("The Node named " & NodeName & " was not used in the tree view and has been removed from the list." & vbCrLf)
            ElseIf myNode.Count = 1 Then
                'One node found in the Tree View - as expected.
            Else
                Main.Message.AddWarning("The Node named " & NodeName & " has " & myNode.Count & " Nodes in the tree view." & vbCrLf)
            End If
        Next

    End Sub

    Private Sub ToolStripMenuItem1_OpenNode_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1_OpenNode.Click

    End Sub

#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

    Public Class MatrixOperationInfo
        'Information about a Matrix Operation item.

        'The Item Name is used as the Key in the ItemInfo dictionary. It is not repeated in the ItemInfo class.

        'Public CopyList As List(Of String) 'A list of nodes that are a copy of this node
        Public CopyList As New List(Of String) 'A list of nodes that are a copy of this node

        Private _text As String = "" 'The node text. This is also the used as the MatrixData or ScalarData key. The Text is the same as the Name unless the node is a Scalary Copy or a Matrix Copy, where the Text is the data key for MatrixData or ScalarData of the copied data.
        Property Text As String
            Get
                Return _text
            End Get
            Set(value As String)
                _text = value
            End Set
        End Property

        Private _description As String = "" 'A description of the item. (Default value is "".)
        Property Description As String
            Get
                Return _description
            End Get
            Set(value As String)
                _description = value
            End Set
        End Property

        Private _type As String = "" 'The type of item (Scalar, Matrix, Open Matrix File, User Defined Scalar, User Defined Matrix, Process, Scalar Process, Matrix Process, Transpose, Inverse, Cholesky, 
        'Transposed Cholesky, Add Scalar, Multiply Scalar, Divide Scalar, Covariance, Correlation, Add Matrix, Multiply Matrix, Collection, Scalar Copy, Matrix Copy).
        Property Type As String
            Get
                Return _type
            End Get
            Set(value As String)
                _type = value
            End Set
        End Property

        Private _status As String = "New" 'The status of the Matrix Operation.
        Property Status As String
            Get
                Return _status
            End Get
            Set(value As String)
                _status = value
            End Set
        End Property

        Private _left As Integer = 0 'The left position of a window used to display information about this Matrix Operation.
        Property Left As Integer
            Get
                Return _left
            End Get
            Set(value As Integer)
                _left = value
            End Set
        End Property

        Private _top As Integer = 0 'The top position of a window used to display information about this Matrix Operation.
        Property Top As Integer
            Get
                Return _top
            End Get
            Set(value As Integer)
                _top = value
            End Set
        End Property

        Private _height As Integer = 400 'The height of a window used to display information about this Matrix Operation.
        Property Height As Integer
            Get
                Return _height
            End Get
            Set(value As Integer)
                _height = value
            End Set
        End Property

        Private _width As Integer = 400 'The width of a window used to display information about this Matrix Operation.
        Property Width As Integer
            Get
                Return _width
            End Get
            Set(value As Integer)
                _width = value
            End Set
        End Property

        Private _selectedTab As Integer = 0 'The index number of the Selected Tab on the display window.
        Property SelectedTab As Integer
            Get
                Return _selectedTab
            End Get
            Set(value As Integer)
                _selectedTab = value
            End Set
        End Property

    End Class 'MatrixOperationInfo


End Class 'frmMatrixOps