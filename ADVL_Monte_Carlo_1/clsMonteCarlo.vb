Public Class clsMonteCarlo
    'The MonteCarlo class stores Monte Carlo models and runs simulations.

#Region " Variable Declarations - All the variables and class objects used in this form and this application." '===============================================================================
    Public Data As New DataSet  'Dataset used to hold the data values. The Monte Carlo input data and results are stored in the table named 'DataTable'. Other temporary tables are used for calculations.
    Public Correlations As New Dictionary(Of String, CorrInfo)
    Public SelCorrMatName As String = "" 'The name of the selected Correlation Matrix ("" if none selected)

    Public DataInfo As New List(Of DataInformation) 'Information about Random Variables and other data set types including Trial Number, Probability Samples, Imported Data, Normal Scores and Data Table.

    Public ChartList As New Dictionary(Of String, Xml.Linq.XDocument) 'Stores the settings used to chart the data. Each entry contains a different chart display. The Chart name is used as the key.
    Public ChartName As String = "" 'The name of the Chart selected for display.

    'Variables used in the Iman-Conover method of generating required correlations:
    Public RandScoreCov(0 To 1, 0 To 1) As Double 'Stores the Covariance of the Randomized Scores.
    Public TransCholRandScoreCov(0 To 1, 0 To 1) As Double 'Stores the Transposed Cholesky Decomposition of the Covariance of the Randomized Scores.
    Public InvTransCholRandScoreCov(0 To 1, 0 To 1) As Double 'Stores the Inverse of the Transposed Cholesky Decomposition of the Covariance of the Randomized Scores.
    'The target correlation(s) are stored in the Correlations dictionary.
    Public TransCholTargetCorr(0 To 1, 0 To 1) As Double 'Stores the Transposed Cholesky Decomposition of the Target Correlation matrix.
    Public ToCorrScores(0 To 1, 0 To 1) As Double 'Multiplying the randomized scores with this matrix produces the correlated scores. ToCorrScores = InvTransCholRandScoreCov x TransCholTargetCorr

    Dim ParamA As Double
    Dim ParamB As Double
    Dim ParamC As Double
    Dim ParamD As Double
    Dim ParamE As Double
    Dim Seed As Integer
    Dim myRandom As New Random


#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Properties" '========================================================================================================================================================================
    Private _name As String = "" 'The name of the Monte Carlo model
    Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Private _label As String = "" 'The Monte Carlo model label. A shortened version of the name in display annotations and in settings file names.
    Property Label As String
        Get
            Return _label
        End Get
        Set(value As String)
            _label = value
        End Set
    End Property

    Private _description As String = "" 'A description of the Monte Carlo model.
    Property Description As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
        End Set
    End Property

    Private _fileName As String = "" 'The name of the file used to store the Monte Carlo model. The file extension will be .MonteCarlo
    Property FileName As String
        Get
            Return _fileName
        End Get
        Set(value As String)
            _fileName = value
        End Set
    End Property

    Private _modified As Boolean = False 'If True, the DataTable has been modified and the file should be updated.
    Property Modified As Boolean
        Get
            Return _modified
        End Get
        Set(value As Boolean)
            _modified = value
        End Set
    End Property

    Private _nTrials As Integer = 10000 'The number of trials in a Monte Carlo simulation.
    Property NTrials As Integer
        Get
            Return _nTrials
        End Get
        Set(value As Integer)
            _nTrials = value
        End Set
    End Property

    Private _selVarIndex As Integer = -1 'The selected Variable index number. (-1 for none selected)
    Property SelVarIndex As Integer
        Get
            Return _selVarIndex
        End Get
        Set(value As Integer)
            _selVarIndex = value
        End Set
    End Property

    Private _calcSeqFile As String = "" 'The name of the Calculation Sequence file.
    Property CalcSeqFile As String
        Get
            Return _calcSeqFile
        End Get
        Set(value As String)
            _calcSeqFile = value
        End Set
    End Property


#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Methods" '===========================================================================================================================================================================

    Public Sub CreateNewTable(ByVal TableName As String, ByVal Description As String)
        'Create a new table in Data.
        If Data.Tables.Contains(TableName) Then
            RaiseEvent ErrorMessage("A Table named " & TableName & " already exists." & vbCrLf)
        Else
            Data.Tables.Add(TableName)
        End If
    End Sub

    Public Sub CreateNewColumn(ByVal TableName As String, ByVal ColumnName As String, ByVal DataType As String)
        'Create a new Column named ColumnName in the Table named TableName.
        If Data.Tables.Contains(TableName) Then
            If Data.Tables(TableName).Columns.Contains(ColumnName) Then
                RaiseEvent ErrorMessage("The table " & TableName & " already contains a column named " & ColumnName & vbCrLf)
            Else
                Select Case DataType
                    Case "Boolean"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.Boolean"))
                    Case "Byte"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.Byte"))
                    Case "Char"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.Char"))
                    Case "DateTime"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.DateTime"))
                    Case "Decimal"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.Decimal"))
                    Case "Double"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.Double"))
                    Case "Int16"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.Int16"))
                    Case "Int32"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.Int32"))
                    Case "Int64"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.Int64"))
                    Case "SByte"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.SByte"))
                    Case "Single"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.Single"))
                    Case "String"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.String"))
                    Case "TimeSpan"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.TimeSpan"))
                    Case "UInt16"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.UInt16"))
                    Case "UInt32"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.UInt32"))
                    Case "UInt64"
                        Data.Tables(TableName).Columns.Add(ColumnName, System.Type.GetType("System.UInt64"))
                    Case Else
                        RaiseEvent ErrorMessage("Unknown column data type: " & DataType & vbCrLf)
                End Select
            End If
        Else
            RaiseEvent ErrorMessage("The table does not exist: " & TableName & vbCrLf)
        End If
    End Sub

    Public Sub Clear()
        'Clear the current DataTable information.
        Data.Clear()
        Data.Reset()
        DataInfo.Clear()
        Correlations.Clear()
        ChartList.Clear()
        ChartName = ""
        SelVarIndex = -1 'This indicates that no index number has been selected. This minimum valid index number is 0.
        SelCorrMatName = ""

        Name = ""
        Label = ""
        Description = ""
        FileName = ""
        Modified = False
        NTrials = 1000
        CalcSeqFile = ""

    End Sub

    Public Sub MoveDataInfoEntryUp(ByVal IndexNo As Integer)
        'Move the DataInfo entry at position IndexNo up one index position.
        If IndexNo = 0 Then
            RaiseEvent Message("The DataInfo entry is already in the first position." & vbCrLf)
        Else
            Dim TempDataInfo As New DataInformation
            TempDataInfo = DataInfo(IndexNo - 1)
            DataInfo(IndexNo - 1) = DataInfo(IndexNo)
            DataInfo(IndexNo) = TempDataInfo
        End If
    End Sub

    Public Sub MoveDataInfoEntryDown(ByVal IndexNo As Integer)
        'Move the DataInfo entry at position IndexNo down one index position.
        If IndexNo >= DataInfo.Count - 1 Then
            RaiseEvent Message("The DataInfo entry is already in the last position." & vbCrLf)
        Else
            Dim TempDataInfo As New DataInformation
            TempDataInfo = DataInfo(IndexNo + 1)
            DataInfo(IndexNo + 1) = DataInfo(IndexNo)
            DataInfo(IndexNo) = TempDataInfo
        End If
    End Sub

    Public Function MonteCarloToXDoc() As System.Xml.Linq.XDocument
        'Return an XDocument containing the Monte Carlo model.

        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <!---->
                   <!--Monte Carlo Model File-->
                   <MonteCarloModel>
                       <Name><%= Name %></Name>
                       <Label><%= Label %></Label>
                       <Description><%= Description %></Description>
                       <NTrials><%= NTrials %></NTrials>
                       <SelectedCorrelationMatrix><%= SelCorrMatName %></SelectedCorrelationMatrix>
                       <CalcSeqFile><%= CalcSeqFile %></CalcSeqFile>
                       <ChartName><%= ChartName %></ChartName>
                       <!--Data Information-->
                       <%= DataInfoToXDoc().<DataInfoList> %>
                       <!--Correlation Matrix List-->
                       <%= CorrelationsToXDoc().<CorrelationMatrixList> %>
                       <!--Chart List-->
                       <%= ChartsToXDoc().<ChartList> %>
                   </MonteCarloModel>

        Return XDoc
    End Function

    Public Function DataInfoToXDoc() As System.Xml.Linq.XDocument
        'Return the Data Information in an XDocument
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <DataInfoList>
                       <%= From item In DataInfo
                           Select
                           <DataInfo>
                               <!--General Data Information-->
                               <Name><%= item.Name %></Name>
                               <Row><%= item.Row %></Row>
                               <Units><%= item.Units %></Units>
                               <UnitsAbbrev><%= item.UnitsAbbrev %></UnitsAbbrev>
                               <Description><%= item.Description %></Description>
                               <Label><%= item.Label %></Label>
                               <DataSetType><%= item.DataSetType %></DataSetType>
                               <IsDiscrete><%= item.IsDiscrete %></IsDiscrete>
                               <DataType><%= item.DataType %></DataType>
                               <Sampling><%= item.Sampling %></Sampling>
                               <Table><%= item.Table %></Table>
                               <Owner><%= item.Owner %></Owner>
                               <!--Random Variable Parameters (If applicable)-->
                               <ParameterAName><%= item.ParameterAName %></ParameterAName>
                               <ParameterAValue><%= item.ParameterAValue %></ParameterAValue>
                               <ParameterBName><%= item.ParameterBName %></ParameterBName>
                               <ParameterBValue><%= item.ParameterBValue %></ParameterBValue>
                               <ParameterCName><%= item.ParameterCName %></ParameterCName>
                               <ParameterCValue><%= item.ParameterCValue %></ParameterCValue>
                               <ParameterDName><%= item.ParameterDName %></ParameterDName>
                               <ParameterDValue><%= item.ParameterDValue %></ParameterDValue>
                               <ParameterEName><%= item.ParameterEName %></ParameterEName>
                               <ParameterEValue><%= item.ParameterEValue %></ParameterEValue>
                               <Seed><%= item.Seed %></Seed>
                               <!--Plot Settings-->
                               <ShowPDF><%= item.ShowPDF %></ShowPDF>
                               <PDFLineColor><%= item.PdfLineColor.ToArgb.ToString %></PDFLineColor>
                               <PDFLineThickness><%= item.PdfLineThickness %></PDFLineThickness>
                               <ShowPDFLn><%= item.ShowPDFLn %></ShowPDFLn>
                               <PDFLnLineColor><%= item.PdfLnLineColor.ToArgb.ToString %></PDFLnLineColor>
                               <PDFLnLineThickness><%= item.PdfLnLineThickness %></PDFLnLineThickness>
                               <ShowCDF><%= item.ShowCDF %></ShowCDF>
                               <CDFLineColor><%= item.CDFLineColor.ToArgb.ToString %></CDFLineColor>
                               <CDFLineThickness><%= item.CDFLineThickness %></CDFLineThickness>
                               <ShowRevCDF><%= item.ShowRevCDF %></ShowRevCDF>
                               <RevCDFLineColor><%= item.RevCDFLineColor.ToArgb.ToString %></RevCDFLineColor>
                               <RevCDFLineThickness><%= item.RevCDFLineThickness %></RevCDFLineThickness>
                               <ShowLegend><%= item.ShowLegend %></ShowLegend>
                               <NDisplayPoints><%= item.NDisplayPoints %></NDisplayPoints>
                               <XMin><%= item.XMin %></XMin>
                               <AutoXMin><%= item.AutoXMin %></AutoXMin>
                               <XMax><%= item.XMax %></XMax>
                               <AutoXMax><%= item.AutoXMax %></AutoXMax>
                               <YMax><%= item.YMax %></YMax>
                               <AutoYMax><%= item.AutoYMax %></AutoYMax>
                               <XGridInterval><%= item.XGridInterval %></XGridInterval>
                               <Left><%= item.Left %></Left>
                               <Top><%= item.Top %></Top>
                               <Width><%= item.Width %></Width>
                               <Height><%= item.Height %></Height>
                               <!--Table Display Settings-->
                               <ColumnNo><%= item.ColumnNo %></ColumnNo>
                               <Format><%= item.Format %></Format>
                               <Alignment><%= item.Alignment %></Alignment>
                           </DataInfo> %>
                   </DataInfoList>
        Return XDoc
    End Function

    Public Function CorrelationsToXDoc() As System.Xml.Linq.XDocument
        'Return the Correlation Matrices information in an XDocument
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <CorrelationMatrixList>
                       <%= From item In Correlations
                           Select
                           <CorrelationMatrix>
                               <Name><%= item.Key %></Name>
                               <Description><%= item.Value.Description %></Description>
                               <NRandomVariables><%= item.Value.NVariables %></NRandomVariables>
                               <TableName><%= item.Value.TableName %></TableName>
                               <DisplayFormat><%= item.Value.DisplayFormat %></DisplayFormat>
                               <CholDisplayFormat><%= item.Value.CholDisplayFormat %></CholDisplayFormat>
                               <UncorrelatedVariableList>
                                   <%= From unCorrItem In item.Value.UnCorrRV
                                       Select
                               <Name><%= unCorrItem %></Name> %>
                               </UncorrelatedVariableList>
                               <CorrelatedVariableList>
                                   <%= From corrItem In item.Value.CorrRV
                                       Select
                                       <Name><%= corrItem %></Name> %>
                               </CorrelatedVariableList>
                               <CorrelationCoefficientList>
                                   <%= From coeff In item.Value.Array
                                       Select
                                       <Coeff><%= coeff %></Coeff> %>
                               </CorrelationCoefficientList>
                           </CorrelationMatrix> %>
                   </CorrelationMatrixList>

        Return XDoc
    End Function

    Public Function ChartsToXDoc() As System.Xml.Linq.XDocument
        'Return the Chart information in an XDocument
        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <ChartList>
                       <%= From item In ChartList
                           Select
                               <ChartInfo>
                                   <ChartName><%= item.Key %></ChartName>
                                   <%= item.Value.<ChartSettings> %>
                               </ChartInfo> %>
                   </ChartList>
        Return XDoc
    End Function

    Public Sub XDocToMonteCarlo(ByRef XDoc As System.Xml.Linq.XDocument)
        'Read the Monte Carlo Model information from the XDocument.

        If XDoc Is Nothing Then Exit Sub

        Name = XDoc.<MonteCarloModel>.<Name>.Value

        If XDoc.<MonteCarloModel>.<Label>.Value <> Nothing Then
            Label = XDoc.<MonteCarloModel>.<Label>.Value
        Else
            Label = ""
        End If

        Description = XDoc.<MonteCarloModel>.<Description>.Value
        NTrials = XDoc.<MonteCarloModel>.<NTrials>.Value
        If XDoc.<MonteCarloModel>.<SelectedCorrelationMatrix>.Value <> Nothing Then
            SelCorrMatName = XDoc.<MonteCarloModel>.<SelectedCorrelationMatrix>.Value
        Else
            SelCorrMatName = ""
        End If
        If XDoc.<MonteCarloModel>.<CalcSeqFile>.Value <> Nothing Then
            CalcSeqFile = XDoc.<MonteCarloModel>.<CalcSeqFile>.Value
        Else
            CalcSeqFile = ""
        End If
        If XDoc.<MonteCarloModel>.<ChartName>.Value <> Nothing Then
            ChartName = XDoc.<MonteCarloModel>.<ChartName>.Value
        Else
            ChartName = ""
        End If

        'Restore the DataInfo list:
        Dim DataInfoXml = From item In XDoc.<MonteCarloModel>.<DataInfoList>.<DataInfo>
        DataInfo.Clear()
        For Each item In DataInfoXml
            Dim NewDataInfo As DataInformation = New DataInformation
            NewDataInfo.Name = item.<Name>.Value
            NewDataInfo.Row = item.<Row>.Value
            NewDataInfo.Units = item.<Units>.Value
            If item.<UnitsAbbrev>.Value <> Nothing Then NewDataInfo.UnitsAbbrev = item.<UnitsAbbrev>.Value Else NewDataInfo.UnitsAbbrev = ""
            NewDataInfo.Description = item.<Description>.Value
            NewDataInfo.Label = item.<Label>.Value
            NewDataInfo.DataSetType = item.<DataSetType>.Value
            If item.<IsDiscrete>.Value <> Nothing Then NewDataInfo.IsDiscrete = item.<IsDiscrete>.Value Else NewDataInfo.IsDiscrete = False
            NewDataInfo.DataType = item.<DataType>.Value
            NewDataInfo.Sampling = item.<Sampling>.Value
            NewDataInfo.Table = item.<Table>.Value
            NewDataInfo.Owner = item.<Owner>.Value
            'Random Variable Parameters (If applicable):
            NewDataInfo.ParameterAName = item.<ParameterAName>.Value
            If item.<ParameterAValue>.Value = "" Then NewDataInfo.ParameterAValue = Double.NaN Else NewDataInfo.ParameterAValue = item.<ParameterAValue>.Value
            NewDataInfo.ParameterBName = item.<ParameterBName>.Value
            If item.<ParameterBValue>.Value = "" Then NewDataInfo.ParameterBValue = Double.NaN Else NewDataInfo.ParameterBValue = item.<ParameterBValue>.Value
            NewDataInfo.ParameterCName = item.<ParameterCName>.Value
            If item.<ParameterCValue>.Value = "" Then NewDataInfo.ParameterCValue = Double.NaN Else NewDataInfo.ParameterCValue = item.<ParameterCValue>.Value
            NewDataInfo.ParameterDName = item.<ParameterDName>.Value
            If item.<ParameterDValue>.Value = "" Then NewDataInfo.ParameterDValue = Double.NaN Else NewDataInfo.ParameterDValue = item.<ParameterDValue>.Value
            NewDataInfo.ParameterEName = item.<ParameterEName>.Value
            If item.<ParameterEValue>.Value = "" Then NewDataInfo.ParameterEValue = Double.NaN Else NewDataInfo.ParameterEValue = item.<ParameterEValue>.Value
            NewDataInfo.Seed = CInt(item.<Seed>.Value)
            'Plot Settings:
            NewDataInfo.ShowPDF = item.<ShowPDF>.Value
            If item.<PDFLineColor>.Value <> Nothing Then NewDataInfo.PDFLineColor = Color.FromArgb(item.<PDFLineColor>.Value) Else NewDataInfo.PDFLineColor = Color.Black
            If item.<PDFLineThickness>.Value <> Nothing Then NewDataInfo.PdfLineThickness = item.<PDFLineThickness>.Value Else NewDataInfo.PdfLineThickness = 1
            NewDataInfo.ShowPDFLn = item.<ShowPDFLn>.Value
            If item.<PDFLnLineColor>.Value <> Nothing Then NewDataInfo.PdfLnLineColor = Color.FromArgb(item.<PDFLnLineColor>.Value) Else NewDataInfo.PdfLnLineColor = Color.Black
            If item.<PDFLnLineThickness>.Value <> Nothing Then NewDataInfo.PdfLnLineThickness = item.<PDFLnLineThickness>.Value Else NewDataInfo.PdfLnLineThickness = 1
            NewDataInfo.ShowCDF = item.<ShowCDF>.Value
            If item.<CDFLineColor>.Value <> Nothing Then NewDataInfo.CDFLineColor = Color.FromArgb(item.<CDFLineColor>.Value) Else NewDataInfo.CDFLineColor = Color.Black
            If item.<CDFLineThickness>.Value <> Nothing Then NewDataInfo.CDFLineThickness = item.<CDFLineThickness>.Value Else NewDataInfo.CDFLineThickness = 1
            If item.<ShowRevCDF>.Value <> Nothing Then NewDataInfo.ShowRevCDF = item.<ShowRevCDF>.Value
            If item.<RevCDFLineColor>.Value <> Nothing Then NewDataInfo.RevCDFLineColor = Color.FromArgb(item.<RevCDFLineColor>.Value) Else NewDataInfo.RevCDFLineColor = Color.Black
            If item.<RevCDFLineThickness>.Value <> Nothing Then NewDataInfo.RevCDFLineThickness = item.<RevCDFLineThickness>.Value Else NewDataInfo.RevCDFLineThickness = 1
            If item.<ShowLegend>.Value <> Nothing Then NewDataInfo.ShowLegend = item.<ShowLegend>.Value Else NewDataInfo.ShowLegend = True
            NewDataInfo.NDisplayPoints = item.<NDisplayPoints>.Value
            NewDataInfo.XMin = item.<XMin>.Value
            NewDataInfo.AutoXMin = item.<AutoXMin>.Value
            NewDataInfo.XMax = item.<XMax>.Value
            NewDataInfo.AutoXMax = item.<AutoXMax>.Value
            If item.<YMax>.Value <> Nothing Then NewDataInfo.YMax = item.<YMax>.Value Else NewDataInfo.YMax = 1
            If item.<AutoYMax>.Value <> Nothing Then NewDataInfo.AutoYMax = item.<AutoYMax>.Value Else NewDataInfo.AutoYMax = True
            NewDataInfo.XGridInterval = item.<XGridInterval>.Value
            NewDataInfo.Left = item.<Left>.Value
            NewDataInfo.Top = item.<Top>.Value
            NewDataInfo.Width = item.<Width>.Value
            NewDataInfo.Height = item.<Height>.Value
            'Table Display Settings:
            NewDataInfo.ColumnNo = item.<ColumnNo>.Value
            NewDataInfo.Format = item.<Format>.Value
            NewDataInfo.Alignment = item.<Alignment>.Value
            DataInfo.Add(NewDataInfo)
        Next

        'Restore the Correlation Matrices:
        Dim CorrMatrices = From item In XDoc.<MonteCarloModel>.<CorrelationMatrixList>.<CorrelationMatrix>
        Dim CorrMatName As String = ""
        Dim NVars As Integer = 0
        Correlations.Clear()
        For Each item In CorrMatrices
            CorrMatName = item.<Name>.Value
            Correlations.Add(CorrMatName, New CorrInfo)
            Correlations(CorrMatName).Description = item.<Description>.Value
            NVars = item.<NRandomVariables>.Value
            Correlations(CorrMatName).NVariables = NVars
            Correlations(CorrMatName).TableName = item.<TableName>.Value
            If item.<DisplayFormat>.Value <> Nothing Then Correlations(CorrMatName).DisplayFormat = item.<DisplayFormat>.Value
            If item.<CholDisplayFormat>.Value <> Nothing Then Correlations(CorrMatName).CholDisplayFormat = item.<CholDisplayFormat>.Value

            Dim UnCorrVars = From unCorrVarItem In item.<UncorrelatedVariableList>.<Name>
            Dim I As Integer = 0
            For Each unCorrVarItem In UnCorrVars
                Correlations(CorrMatName).UnCorrRV(I) = unCorrVarItem
                I += 1
            Next

            Dim CorrVars = From corrVarItem In item.<CorrelatedVariableList>.<Name>
            I = 0
            For Each corrVarItem In CorrVars
                Correlations(CorrMatName).CorrRV(I) = corrVarItem
                I += 1
            Next

            Dim Coeffs = From coeffItem In item.<CorrelationCoefficientList>.<Coeff>
            I = 0
            For Each coeffItem In Coeffs
                Correlations(CorrMatName).Array(I Mod NVars, Int(I / NVars)) = coeffItem
                I += 1
            Next
        Next

        'Restore the Charts:
        Dim Charts = From item In XDoc.<MonteCarloModel>.<ChartList>.<ChartInfo>

        Dim NewChartName As String = ""
        ChartList.Clear()
        For Each item In Charts
            NewChartName = item.<ChartName>.Value
            Dim ChartXDoc = <?xml version="1.0" encoding="utf-8"?>
                            <%= item.<ChartSettings> %>

            ChartList.Add(NewChartName, ChartXDoc)
        Next

    End Sub

    Public Sub ShuffleColumn(ByVal TableName As String, ColumnName As String)
        'Randomly shuffle the specified column.
        If Data.Tables.Contains(TableName) Then
            If Data.Tables(TableName).Columns.Contains(ColumnName) Then
                Dim Temp As Double
                Dim RowNo As Integer
                Dim RandRow As Integer
                Dim LastRowNo As Integer = Data.Tables(TableName).Rows.Count - 1
                For RowNo = 0 To LastRowNo - 1
                    Temp = Data.Tables(TableName).Rows(RowNo).Item(ColumnName) 'Temporarily save the ColumnName value in the current RowNo 
                    RandRow = CInt((LastRowNo - RowNo) * myRandom.NextDouble + RowNo) 'Randomly select a row number from the remaining rows. 
                    Data.Tables(TableName).Rows(RowNo).Item(ColumnName) = Data.Tables(TableName).Rows(RandRow).Item(ColumnName) 'Put the value in the Random Row at the current RowNo
                    Data.Tables(TableName).Rows(RandRow).Item(ColumnName) = Temp 'Put the saved RowNo value at the RandRow position
                Next
            Else
                RaiseEvent ErrorMessage("A column named " & ColumnName & " was not found." & vbCrLf)
            End If
        Else
            RaiseEvent ErrorMessage("A table named " & TableName & " was not found." & vbCrLf)
        End If
    End Sub

    Public Function DataInfoNameIndex(ByVal RVName As String) As Integer
        'Returns the DataInfo index of the item with the specified name.
        Return DataInfo.FindIndex(Function(i) i.Name = RVName)
    End Function

    Public Sub GenerateData(ByVal IndexNo As Integer)
        'Generate the data for a single Variable.

        Dim TableName As String 'The destination table for the Random Variable data.
        Dim Trial As Integer 'Loop index

        Dim DataSetType As String = DataInfo(IndexNo).DataSetType

        If DataSetType = "Data Table" Then 'Create the specified Data Table
            TableName = DataInfo(IndexNo).Name
            If Data.Tables.Contains(TableName) Then
            Else
                Data.Tables.Add(TableName) 'Create the table to contain the Monte Carlo data.
                For Trial = 1 To NTrials
                    Data.Tables(TableName).Rows.Add()
                Next
            End If

        Else
            TableName = DataInfo(IndexNo).Table
            Dim ColumnName As String = DataInfo(IndexNo).Name
            Dim PVal As Double 'The probability value passed the an InvCDF function.

            'Check if the Table exists to contain the data.
            If Data.Tables.Contains(TableName) Then
            Else
                Data.Tables.Add(TableName) 'Create the table to contain the Monte Carlo data.
                For Trial = 1 To NTrials
                    Data.Tables(TableName).Rows.Add()
                Next
            End If

            'Check if the Column exists to contain the data.
            If Data.Tables(TableName).Columns.Contains(ColumnName) Then
                If Data.Tables(TableName).Columns(ColumnName).DataType = System.Type.GetType("System." & DataInfo(IndexNo).DataType) Then
                    'The existing column is the correct type.
                Else
                    'The existing column is the wrong type. Delete it an create a new one.
                    Data.Tables(TableName).Columns.Remove(ColumnName)
                    CreateNewColumn(TableName, ColumnName, DataInfo(IndexNo).DataType) 'Create the Column to contain the new data.
                End If
            Else
                CreateNewColumn(TableName, ColumnName, DataInfo(IndexNo).DataType) 'Create the Column to contain the new data.
            End If

            Select Case DataSetType
                Case "Trial Number"
                    For Trial = 1 To NTrials
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = Trial
                    Next

                Case "Probability Samples"
                    Select Case DataInfo(IndexNo).Sampling
                        Case "N/A"
                            RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)
                        Case "Random"
                            For Trial = 1 To NTrials
                                Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = myRandom.NextDouble
                            Next

                        Case "Latin Hypercube"
                            For Trial = 1 To NTrials
                                PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                                Data.Tables(TableName).Rows(IndexNo - 1).Item(ColumnName) = PVal
                            Next
                            ShuffleColumn(TableName, ColumnName)

                        Case "Sorted Latin Hypercube"
                            For Trial = 1 To NTrials
                                PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                                Data.Tables(TableName).Rows(IndexNo - 1).Item(ColumnName) = PVal
                            Next
                        Case "Median Latin Hypercube"
                            For Trial = 1 To NTrials
                                PVal = (Trial - 0.5) / NTrials
                                Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = PVal
                            Next
                            ShuffleColumn(TableName, ColumnName)

                        Case "Sorted Median Latin Hypercube"
                            For Trial = 1 To NTrials
                                PVal = (Trial - 0.5) / NTrials
                                Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = PVal
                            Next
                        Case Else
                            RaiseEvent ErrorMessage("Unknown sampling: " & DataInfo(IndexNo).Sampling & vbCrLf)
                    End Select

                Case "Imported Data"
                     'No need to write any data here. It will be imported by the user - by pasting into the application Results table. The Imported Data column is created above.

                Case "Normal Scores"
                    'The Sampling parameter is not used. The Normal Scores are generated in order.
                    Dim StdNormInv As Double
                    Dim SumSq As Double = 0
                    For Trial = 1 To NTrials
                        StdNormInv = MathNet.Numerics.Distributions.Normal.InvCDF(0, 1, Trial / (NTrials + 1)) 'The Normal distribution with a Mean of 0 and Std Dev of 1 is the Standard Normal distribution.
                        SumSq += StdNormInv ^ 2 'Calculate the sum of the squared Standard Normal Inverse CDF values. This is used later to rescale the Normal Scores.
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = StdNormInv 'Save the unscaled Normal Scores.
                    Next
                    Dim Scale As Double = Math.Sqrt(SumSq / NTrials) 'Calculate the scale factor
                    For Trial = 1 To NTrials
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) /= Scale
                    Next

                'Case "Beta"
                Case "C2 - Beta"
                    GenerateBetaData(TableName, ColumnName, DataInfo(IndexNo).Sampling)

                'Case "Beta Scaled"
                Case "C4 - Beta Scaled"
                    GenerateBetaScaledData(TableName, ColumnName, DataInfo(IndexNo).Sampling)

                'Case "Burr"

                'Case "Cauchy"
                Case "C2 - Cauchy"
                    GenerateCauchyData(TableName, ColumnName, DataInfo(IndexNo).Sampling)

                Case "Chi Squared"

                Case "Continuous Uniform"

                Case "Exponential"

                Case "Fisher-Snedecor"

                Case "Gamma"

                Case "Inverse Gaussian"

                    'Case "Log Normal"
                Case "C2 - Log Normal"
                    ParamA = DataInfo(IndexNo).ParameterAValue
                    ParamB = DataInfo(IndexNo).ParameterBValue
                    GenerateLogNormalData(TableName, ColumnName, DataInfo(IndexNo).Sampling)
                    'Case "Normal"
                Case "C2 - Normal"
                    ParamA = DataInfo(IndexNo).ParameterAValue
                    ParamB = DataInfo(IndexNo).ParameterBValue
                    GenerateNormalData(TableName, ColumnName, DataInfo(IndexNo).Sampling)
                Case "Pareto"

                Case "Rayleigh"

                Case "Skewed Generalized Error"

                Case "Skewed Generalized T"

                Case "Student's T"

                Case "Triangular"

                Case "Truncated Pareto"

                Case "D1 - Bernoulli"
                    ParamA = DataInfo(IndexNo).ParameterAValue
                    GenerateBernoulliData(TableName, ColumnName, DataInfo(IndexNo).Sampling)

                Case Else
                    RaiseEvent ErrorMessage("Unknown data set type: " & DataSetType & vbCrLf)
            End Select
        End If
    End Sub

    'Public Sub GenerateRandomVariableData()
    Public Sub GenerateData()
        'Generate the Random Variable data.

        'Clear any existing Monte Carlo data:
        Data.Clear()
        Data.Reset()

        'Generate each item in the DataInfo list:
        Dim DataSetType As String

        Dim TableName As String 'The destination table for the Random Variable data.
        Dim ColumnName As String
        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.

        Dim I As Integer
        For I = 0 To DataInfo.Count - 1
            DataSetType = DataInfo(I).DataSetType

            If DataSetType = "Data Table" Then 'Create the specified Data Table
                TableName = DataInfo(I).Name
                If Data.Tables.Contains(TableName) Then
                Else
                    Data.Tables.Add(TableName) 'Create the table to contain the Monte Carlo data.
                    For J = 1 To NTrials
                        Data.Tables(TableName).Rows.Add()
                    Next
                End If

            Else 'Create the specified Data Column
                ParamA = DataInfo(I).ParameterAValue
                ParamB = DataInfo(I).ParameterBValue
                ParamC = DataInfo(I).ParameterCValue
                ParamD = DataInfo(I).ParameterDValue
                ParamE = DataInfo(I).ParameterEValue
                Seed = DataInfo(I).Seed

                'Updated Random code:
                If Seed = -1 Then
                    myRandom = New Random 'This starts a new random sequence using a seed based on the time
                Else
                    myRandom = New Random(Seed) 'This starts a new random sequence using the specified seed.
                End If

                TableName = DataInfo(I).Table 'The destination table for the Random Variable data.
                ColumnName = DataInfo(I).Name

                'Check if the Table exists to contain the data.
                If Data.Tables.Contains(TableName) Then
                Else
                    Data.Tables.Add(TableName) 'Create the table to contain the Monte Carlo data.
                    For J = 1 To NTrials
                        Data.Tables(TableName).Rows.Add()
                    Next
                End If

                'Check if the Column exists to contain the data.
                If Data.Tables(TableName).Columns.Contains(ColumnName) Then
                Else
                    CreateNewColumn(TableName, ColumnName, DataInfo(I).DataType)
                End If

                Select Case DataSetType

                    Case "Trial Number"
                        For Trial = 1 To NTrials
                            Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = Trial
                        Next

                    Case "Probability Samples"
                        Select Case DataInfo(I).Sampling
                            Case "N/A"
                                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)
                            Case "Random"
                                For Trial = 1 To NTrials
                                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = myRandom.NextDouble
                                Next

                            Case "Latin Hypercube"
                                For Trial = 1 To NTrials
                                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                                    Data.Tables(TableName).Rows(I - 1).Item(ColumnName) = PVal
                                Next
                                ShuffleColumn(TableName, ColumnName)

                            Case "Sorted Latin Hypercube"
                                For Trial = 1 To NTrials
                                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                                    Data.Tables(TableName).Rows(I - 1).Item(ColumnName) = PVal
                                Next

                            Case "Median Latin Hypercube"
                                For Trial = 1 To NTrials
                                    PVal = (Trial - 0.5) / NTrials
                                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = PVal
                                Next
                                ShuffleColumn(TableName, ColumnName)

                            Case "Sorted Median Latin Hypercube"
                                For Trial = 1 To NTrials
                                    PVal = (Trial - 0.5) / NTrials
                                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = PVal
                                Next

                            Case Else
                                RaiseEvent ErrorMessage("Unknown sampling: " & DataInfo(I).Sampling & vbCrLf)
                        End Select

                    Case "Imported Data"
                         'No need to write any data here. It will be imported by the user - by pasting into the application Results table. The Imported Data column is created above.

                    Case "Normal Scores"
                        'The Sampling parameter is not used. The Normal Scores are generated in order.
                        Dim StdNormInv As Double
                        Dim SumSq As Double = 0
                        For Trial = 1 To NTrials
                            StdNormInv = MathNet.Numerics.Distributions.Normal.InvCDF(0, 1, Trial / (NTrials + 1)) 'The Normal distribution with a Mean of 0 and Std Dev of 1 is the Standard Normal distribution.
                            SumSq += StdNormInv ^ 2 'Calculate the sum of the squared Standard Normal Inverse CDF values. This is used later to rescale the Normal Scores.
                            Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = StdNormInv 'Save the unscaled Normal Scores.
                        Next
                        Dim Scale As Double = Math.Sqrt(SumSq / NTrials) 'Calculate the scale factor
                        For Trial = 1 To NTrials
                            Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) /= Scale
                        Next

                    Case "C2 - Beta"
                        GenerateBetaData(TableName, ColumnName, DataInfo(I).Sampling)

                    Case "C4 - Beta Scaled"
                        GenerateBetaScaledData(TableName, ColumnName, DataInfo(I).Sampling)

                    'Case "Burr"

                    Case "C2 - Cauchy"
                        GenerateCauchyData(TableName, ColumnName, DataInfo(I).Sampling)

                    Case "C1 - Chi Squared"
                        GenerateChiSquaredData(TableName, ColumnName, DataInfo(I).Sampling)

                    Case "C2 - Continuous Uniform"
                        GenerateContUniformData(TableName, ColumnName, DataInfo(I).Sampling)

                    Case "C1 - Exponential"
                        GenerateExponentialData(TableName, ColumnName, DataInfo(I).Sampling)

                    Case "C2 - Fisher-Snedecor"
                        GenerateFisherSnedecorData(TableName, ColumnName, DataInfo(I).Sampling)

                    Case "C2 - Gamma"
                        GenerateGammaData(TableName, ColumnName, DataInfo(I).Sampling)

                    Case "C2 - Inverse Gaussian"

                    Case "C2 - Log Normal"

                    Case "C2 - Normal"
                        GenerateNormalData(TableName, ColumnName, DataInfo(I).Sampling)

                    Case "C2 - Pareto"

                    Case "C1 - Rayleigh"

                    Case "C4 - Skewed Generalized Error"

                    Case "C5 - Skewed Generalized T"

                    Case "C3 - Student's T"

                    Case "C3 - Triangular"
                        GenerateTriangularData(TableName, ColumnName, DataInfo(I).Sampling)

                    Case "C3 - Truncated Pareto"


                    Case "D1 - Bernoulli"
                        GenerateBernoulliData(TableName, ColumnName, DataInfo(I).Sampling)

                    Case "D2 - Binomial"

                   'Case "D1 - Categorical" 'TO DO!

                    Case "D2 - Conway-Maxwell-Poisson"

                    Case "D2 - Discrete Uniform"

                    Case "D1 - Geometric"

                    Case "D3 - Hypergeometric"
                        GenerateHypergeometricData(TableName, ColumnName, DataInfo(I).Sampling)

                    Case "D2 - Negative Binomial"

                    Case "D1 - Poisson"

                    Case "D2 - Zipf"

                    Case Else
                        RaiseEvent ErrorMessage("Unknown data set type: " & DataSetType & vbCrLf)
                End Select
            End If
        Next
        'RaiseEvent Message("Generate data items complete." & vbCrLf)
    End Sub

    Private Sub GenerateBetaData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Beta distribution data in the Specified Table and Column.
        'The Alpha and Beta parameters arte contained in the ParamA and ParamB variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.Beta.InvCDF(ParamA, ParamB, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception

                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Beta.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Beta.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Beta.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Beta.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Beta.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Beta.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Beta.InvCDF(ParamA, ParamB, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Beta.InvCDF(ParamA, ParamB, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub

    Private Sub GenerateBetaScaledData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Beta Scaled distribution data in the Specified Table and Column.
        'The Alpha, Beta, Mu and Sigma parameters arte contained in the ParamA, ParamB, ParamC and ParamD variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.BetaScaled.InvCDF(ParamA, ParamB, ParamC, ParamD, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception

                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.BetaScaled.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.BetaScaled.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.BetaScaled.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.BetaScaled.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.BetaScaled.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.BetaScaled.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.BetaScaled.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.BetaScaled.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub

    Private Sub GenerateCauchyData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Cauchy distribution data in the Specified Table and Column.
        'The Alpha and Beta parameters are contained in the ParamA and ParamB variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.Cauchy.InvCDF(ParamA, ParamB, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception

                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Cauchy.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Cauchy.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Cauchy.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Cauchy.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Cauchy.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Cauchy.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Cauchy.InvCDF(ParamA, ParamB, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Cauchy.InvCDF(ParamA, ParamB, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub

    Private Sub GenerateChiSquaredData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Chi Squared distribution data in the Specified Table and Column.
        'The k parameter is contained in the ParamA variable.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.ChiSquared.InvCDF(ParamA, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception

                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.ChiSquared.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.ChiSquared.InvCDF(ParamA, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.ChiSquared.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.ChiSquared.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.ChiSquared.InvCDF(ParamA, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.ChiSquared.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.ChiSquared.InvCDF(ParamA, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.ChiSquared.InvCDF(ParamA, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub

    Private Sub GenerateContUniformData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Continuous Uniform distribution data in the Specified Table and Column.
        'The a and b parameters arte contained in the ParamA and ParamB variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            'Row.Item(ColumnName) = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, Rnd())
                            SampVal = MathNet.Numerics.Distributions.ContinuousUniform.InvCDF(ParamA, ParamB, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception

                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.ContinuousUniform.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.ContinuousUniform.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.ContinuousUniform.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.ContinuousUniform.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                        'ElseIf Double.NegativeInfinity Then
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.ContinuousUniform.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.ContinuousUniform.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.ContinuousUniform.InvCDF(ParamA, ParamB, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.ContinuousUniform.InvCDF(ParamA, ParamB, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub

    Private Sub GenerateExponentialData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Exponential distribution data in the Specified Table and Column.
        'The Lambda parameter is contained in the ParamA variable.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            'Row.Item(ColumnName) = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, Rnd())
                            SampVal = MathNet.Numerics.Distributions.Exponential.InvCDF(ParamA, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception

                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Exponential.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                        'ElseIf Double.NegativeInfinity Then
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Exponential.InvCDF(ParamA, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Exponential.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Exponential.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                        'ElseIf Double.NegativeInfinity Then
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Exponential.InvCDF(ParamA, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Exponential.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Exponential.InvCDF(ParamA, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Exponential.InvCDF(ParamA, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub

    Private Sub GenerateFisherSnedecorData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Fisher Snedecor distribution data in the Specified Table and Column.
        'The d1 and d2 parameters arte contained in the ParamA and ParamB variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            'Row.Item(ColumnName) = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, Rnd())
                            SampVal = MathNet.Numerics.Distributions.FisherSnedecor.InvCDF(ParamA, ParamB, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception

                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.FisherSnedecor.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                        'ElseIf Double.NegativeInfinity Then
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.FisherSnedecor.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.FisherSnedecor.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.FisherSnedecor.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                        'ElseIf Double.NegativeInfinity Then
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.FisherSnedecor.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.FisherSnedecor.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.FisherSnedecor.InvCDF(ParamA, ParamB, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.FisherSnedecor.InvCDF(ParamA, ParamB, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub

    Private Sub GenerateGammaData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Gamma distribution data in the Specified Table and Column.
        'The Alpha and Beta parameters are contained in the ParamA and ParamB variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)
            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.Gamma.InvCDF(ParamA, ParamB, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception
                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Gamma.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                        'ElseIf Double.NegativeInfinity Then
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Gamma.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Gamma.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Gamma.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Gamma.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Gamma.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Gamma.InvCDF(ParamA, ParamB, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Gamma.InvCDF(ParamA, ParamB, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)

        End Select
    End Sub


    'NOTE: There appears to be an error in the Math.Net Inverse Gaussian InvCDF function!!!
    'Private Sub GenerateInvGaussianData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
    '    'Generate Inverse Gaussian distribution data in the Specified Table and Column.
    '    'The mu and lambda parameters are contained in the ParamA and ParamB variables.

    '    Dim Trial As Integer 'Loop index
    '    Dim PVal As Double 'The probability value passed to an InvCDF function.
    '    Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
    '    Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
    '    Dim Attempts As Integer 'The number of attempts to get a valid value

    '    Select Case Sampling
    '        Case "N/A"
    '            RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)
    '        Case "Random"
    '            'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
    '            For Each Row As DataRow In Data.Tables(TableName).Rows
    '                ValidVal = False
    '                Attempts = 0
    '                While ValidVal = False
    '                    Try
    '                        Attempts += 1
    '                        'Row.Item(ColumnName) = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, Rnd())
    '                        'SampVal = MathNet.Numerics.Distributions.Gamma.InvCDF(ParamA, ParamB, Rnd())
    '                        SampVal = MathNet.Numerics.Distributions.InverseGaussian.InvCDF(ParamA, ParamB, Rnd())
    '                        If SampVal = Double.NaN Then
    '                            ValidVal = False
    '                        ElseIf SampVal = Double.PositiveInfinity Then
    '                            ValidVal = False
    '                        ElseIf SampVal = Double.NegativeInfinity Then
    '                            ValidVal = False
    '                        Else
    '                            Row.Item(ColumnName) = SampVal
    '                            ValidVal = True
    '                        End If
    '                        If Attempts > 100 Then
    '                            RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                            RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
    '                            Exit While
    '                        End If
    '                    Catch ex As Exception
    '                        'If Attempts > 100 Then
    '                        '    RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                        '    RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
    '                        '    Exit While
    '                        'End If
    '                    End Try
    '                End While
    '            Next

    '        Case "Latin Hypercube"
    '            'Get the first value - checking that the InvCDF function returns a valid value
    '            ValidVal = False
    '            Attempts = 0
    '            While ValidVal = False
    '                Try
    '                    Attempts += 1
    '                    PVal = Rnd() / NTrials 'Select a random probability number between 0 and 1 / NTrials
    '                    SampVal = MathNet.Numerics.Distributions.InverseGaussian.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
    '                    ValidVal = True
    '                Catch ex As Exception
    '                    If Attempts > 100 Then
    '                        RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                        RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
    '                        Exit While
    '                    End If
    '                End Try
    '                If SampVal = Double.NaN Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.PositiveInfinity Then
    '                    ValidVal = False
    '                    'ElseIf Double.NegativeInfinity Then
    '                ElseIf SampVal = Double.NegativeInfinity Then
    '                    ValidVal = False
    '                Else
    '                    Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
    '                    ValidVal = True
    '                End If
    '            End While

    '            'Get the intermediate values - the InvCDF function should be able to return valid values for these
    '            For Trial = 2 To NTrials - 1
    '                PVal = (Rnd() + Trial - 1) / NTrials
    '                Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.InverseGaussian.InvCDF(ParamA, ParamB, PVal)
    '            Next

    '            'Get the last value - checking that the InvCDF function returns a valid value
    '            ValidVal = False
    '            Attempts = 0
    '            While ValidVal = False
    '                Try
    '                    Attempts += 1
    '                    PVal = (Rnd() + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
    '                    SampVal = MathNet.Numerics.Distributions.InverseGaussian.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
    '                    ValidVal = True
    '                Catch ex As Exception
    '                    If Attempts > 100 Then
    '                        RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                        RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
    '                        Exit While
    '                    End If
    '                End Try
    '                If SampVal = Double.NaN Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.PositiveInfinity Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.NegativeInfinity Then
    '                    ValidVal = False
    '                Else
    '                    Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
    '                    ValidVal = True
    '                End If
    '            End While
    '            ShuffleColumn(TableName, ColumnName)

    '        Case "Sorted Latin Hypercube"
    '            'Get the first value - checking that the InvCDF function returns a valid value
    '            ValidVal = False
    '            Attempts = 0
    '            While ValidVal = False
    '                Try
    '                    Attempts += 1
    '                    PVal = Rnd() / NTrials 'Select a random probability number between 0 and 1 / NTrials
    '                    SampVal = MathNet.Numerics.Distributions.InverseGaussian.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
    '                    ValidVal = True
    '                Catch ex As Exception
    '                    If Attempts > 100 Then
    '                        RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                        RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
    '                        Exit While
    '                    End If
    '                End Try
    '                If SampVal = Double.NaN Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.PositiveInfinity Then
    '                    ValidVal = False
    '                    'ElseIf Double.NegativeInfinity Then
    '                ElseIf SampVal = Double.NegativeInfinity Then
    '                    ValidVal = False
    '                Else
    '                    Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
    '                    ValidVal = True
    '                End If
    '            End While

    '            'Get the intermediate values - the InvCDF function should be able to return valid values for these
    '            For Trial = 2 To NTrials - 1
    '                PVal = (Rnd() + Trial - 1) / NTrials
    '                Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.InverseGaussian.InvCDF(ParamA, ParamB, PVal)
    '            Next

    '            'Get the last value - checking that the InvCDF function returns a valid value
    '            ValidVal = False
    '            Attempts = 0
    '            While ValidVal = False
    '                Try
    '                    Attempts += 1
    '                    PVal = (Rnd() + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
    '                    SampVal = MathNet.Numerics.Distributions.InverseGaussian.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
    '                    ValidVal = True
    '                Catch ex As Exception
    '                    If Attempts > 100 Then
    '                        RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                        RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
    '                        Exit While
    '                    End If
    '                End Try
    '                If SampVal = Double.NaN Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.PositiveInfinity Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.NegativeInfinity Then
    '                    ValidVal = False
    '                Else
    '                    Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
    '                    ValidVal = True
    '                End If
    '            End While

    '        Case "Median Latin Hypercube"
    '            For Trial = 1 To NTrials
    '                PVal = (Trial - 0.5) / NTrials
    '                Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.InverseGaussian.InvCDF(ParamA, ParamB, PVal)
    '            Next
    '            ShuffleColumn(TableName, ColumnName)

    '        Case "Sorted Median Latin Hypercube"
    '            For Trial = 1 To NTrials
    '                PVal = (Trial - 0.5) / NTrials
    '                Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.InverseGaussian.InvCDF(ParamA, ParamB, PVal)
    '            Next

    '        Case Else
    '            RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)

    '    End Select
    'End Sub


    Private Sub GenerateLogNormalData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Log Normal distribution data in the Specified Table and Column.
        'The Mean and Variance parameters are contained in the ParamA and ParamB variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)
            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.LogNormal.InvCDF(ParamA, ParamB, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception

                        End Try
                    End While
                Next
            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.LogNormal.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    Catch ex As Exception

                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.LogNormal.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.LogNormal.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    Catch ex As Exception

                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.LogNormal.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    Catch ex As Exception

                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.LogNormal.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.LogNormal.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    Catch ex As Exception

                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.LogNormal.InvCDF(ParamA, ParamB, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.LogNormal.InvCDF(ParamA, ParamB, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub
    Private Sub GenerateNormalData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Normal distribution data in the Specified Table and Column.
        'The Mean and Variance parameters are contained in the ParamA and ParamB variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        'If Double.IsNaN(Seed) Then Randomize() Else Randomize(Seed) 'If a Seed is used, the random numbers are repeatable.

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)
            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception
                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                        'ElseIf Double.NegativeInfinity Then
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)

        End Select
    End Sub

    Private Sub GenerateParetoData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Pareto distribution data in the Specified Table and Column.
        'The xm and alpha parameters are contained in the ParamA and ParamB variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)
            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.Pareto.InvCDF(ParamA, ParamB, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception
                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Pareto.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Pareto.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Pareto.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Pareto.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Pareto.InvCDF(ParamA, ParamB, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Pareto.InvCDF(ParamA, ParamB, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Pareto.InvCDF(ParamA, ParamB, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Pareto.InvCDF(ParamA, ParamB, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)

        End Select
    End Sub

    Private Sub GenerateRayleighData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Rayeligh distribution data in the Specified Table and Column.
        'The sigma parameter is contained in the ParamA variable.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.Rayleigh.InvCDF(ParamA, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception

                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Rayleigh.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Rayleigh.InvCDF(ParamA, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Rayleigh.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Rayleigh.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                        'ElseIf Double.NegativeInfinity Then
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Rayleigh.InvCDF(ParamA, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Rayleigh.InvCDF(ParamA, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Rayleigh.InvCDF(ParamA, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Rayleigh.InvCDF(ParamA, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub

    Private Sub GenerateSkewGenErrorData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Skewed Generalized Error distribution data in the Specified Table and Column.
        'The mu, sigma, lambda and p parameters are contained in the ParamA, ParamB, ParamC and ParamD variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.SkewedGeneralizedError.InvCDF(ParamA, ParamB, ParamC, ParamD, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception

                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.SkewedGeneralizedError.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.SkewedGeneralizedError.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.SkewedGeneralizedError.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.SkewedGeneralizedError.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.SkewedGeneralizedError.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.SkewedGeneralizedError.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.SkewedGeneralizedError.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.SkewedGeneralizedError.InvCDF(ParamA, ParamB, ParamC, ParamD, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub

    Private Sub GenerateSkewGenTData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Skewed Generalized T distribution data in the Specified Table and Column.
        'The mu, sigma, lambda, p and q parameters are contained in the ParamA, ParamB, ParamC and ParamD variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.SkewedGeneralizedT.InvCDF(ParamA, ParamB, ParamC, ParamD, ParamE, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception

                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.SkewedGeneralizedT.InvCDF(ParamA, ParamB, ParamC, ParamD, ParamE, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.SkewedGeneralizedT.InvCDF(ParamA, ParamB, ParamC, ParamD, ParamE, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.SkewedGeneralizedT.InvCDF(ParamA, ParamB, ParamC, ParamD, ParamE, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.SkewedGeneralizedT.InvCDF(ParamA, ParamB, ParamC, ParamD, ParamE, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.SkewedGeneralizedT.InvCDF(ParamA, ParamB, ParamC, ParamD, ParamE, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.SkewedGeneralizedT.InvCDF(ParamA, ParamB, ParamC, ParamD, ParamE, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.SkewedGeneralizedT.InvCDF(ParamA, ParamB, ParamC, ParamD, ParamE, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.SkewedGeneralizedT.InvCDF(ParamA, ParamB, ParamC, ParamD, ParamE, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub

    Private Sub GenerateStudentsTData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Students T distribution data in the Specified Table and Column.
        'The mu, sigma and nu parameters are contained in the ParamA and ParamB variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)
            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            'Row.Item(ColumnName) = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, Rnd())
                            SampVal = MathNet.Numerics.Distributions.StudentT.InvCDF(ParamA, ParamB, ParamC, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception
                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.StudentT.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.StudentT.InvCDF(ParamA, ParamB, ParamC, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.StudentT.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.StudentT.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.StudentT.InvCDF(ParamA, ParamB, ParamC, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.StudentT.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.StudentT.InvCDF(ParamA, ParamB, ParamC, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.StudentT.InvCDF(ParamA, ParamB, ParamC, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)

        End Select
    End Sub

    Private Sub GenerateTriangularData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Triangular distribution data in the Specified Table and Column.
        'The a, b and c parameters are contained in the ParamA and ParamB variables.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value passed to an InvCDF function.
        Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
        Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
        Dim Attempts As Integer 'The number of attempts to get a valid value

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)
            Case "Random"
                'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    ValidVal = False
                    Attempts = 0
                    While ValidVal = False
                        Try
                            Attempts += 1
                            SampVal = MathNet.Numerics.Distributions.Triangular.InvCDF(ParamA, ParamB, ParamC, myRandom.NextDouble)
                            If SampVal = Double.NaN Then
                                ValidVal = False
                            ElseIf SampVal = Double.PositiveInfinity Then
                                ValidVal = False
                            ElseIf SampVal = Double.NegativeInfinity Then
                                ValidVal = False
                            Else
                                Row.Item(ColumnName) = SampVal
                                ValidVal = True
                            End If
                            If Attempts > 100 Then
                                RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                                RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
                                Exit While
                            End If
                        Catch ex As Exception
                        End Try
                    End While
                Next

            Case "Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Triangular.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Triangular.InvCDF(ParamA, ParamB, ParamC, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Triangular.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                'Get the first value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = myRandom.NextDouble / NTrials 'Select a random probability number between 0 and 1 / NTrials
                        SampVal = MathNet.Numerics.Distributions.Triangular.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

                'Get the intermediate values - the InvCDF function should be able to return valid values for these
                For Trial = 2 To NTrials - 1
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Triangular.InvCDF(ParamA, ParamB, ParamC, PVal)
                Next

                'Get the last value - checking that the InvCDF function returns a valid value
                ValidVal = False
                Attempts = 0
                While ValidVal = False
                    Try
                        Attempts += 1
                        PVal = (myRandom.NextDouble + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
                        SampVal = MathNet.Numerics.Distributions.Triangular.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
                        ValidVal = True
                    Catch ex As Exception
                        If Attempts > 100 Then
                            RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
                            RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
                            Exit While
                        End If
                    End Try
                    If SampVal = Double.NaN Then
                        ValidVal = False
                    ElseIf SampVal = Double.PositiveInfinity Then
                        ValidVal = False
                    ElseIf SampVal = Double.NegativeInfinity Then
                        ValidVal = False
                    Else
                        Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
                        ValidVal = True
                    End If
                End While

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Triangular.InvCDF(ParamA, ParamB, ParamC, PVal)
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.Triangular.InvCDF(ParamA, ParamB, ParamC, PVal)
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)

        End Select
    End Sub

    'NOTE: There appears to be an error in the Math.Net Truncated Pareto InvCDF function!!!
    'Private Sub GenerateTruncParetoData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
    '    'Generate Truncated Pareto distribution data in the Specified Table and Column.
    '    'The xm, alpha and T parameters are contained in the ParamA and ParamB variables.

    '    Dim Trial As Integer 'Loop index
    '    Dim PVal As Double 'The probability value passed to an InvCDF function.
    '    Dim SampVal As Double 'A sampled value returned by an InvCDF function at a specified PVal probability value.
    '    Dim ValidVal As Boolean 'True if the InvCDF function has returned a valid value.
    '    Dim Attempts As Integer 'The number of attempts to get a valid value

    '    Select Case Sampling
    '        Case "N/A"
    '            RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)
    '        Case "Random"
    '            'This code rejects the rare cases where an invalid  distribution value is produced. Up to 100 attempts will be made at each sample point to obtain a valid sample value.
    '            For Each Row As DataRow In Data.Tables(TableName).Rows
    '                ValidVal = False
    '                Attempts = 0
    '                While ValidVal = False
    '                    Try
    '                        Attempts += 1
    '                        'Row.Item(ColumnName) = MathNet.Numerics.Distributions.Normal.InvCDF(ParamA, ParamB, Rnd())
    '                        SampVal = MathNet.Numerics.Distributions.TruncatedPareto.InvCDF(ParamA, ParamB, ParamC, Rnd())
    '                        If SampVal = Double.NaN Then
    '                            ValidVal = False
    '                        ElseIf SampVal = Double.PositiveInfinity Then
    '                            ValidVal = False
    '                        ElseIf SampVal = Double.NegativeInfinity Then
    '                            ValidVal = False
    '                        Else
    '                            Row.Item(ColumnName) = SampVal
    '                            ValidVal = True
    '                        End If
    '                        If Attempts > 100 Then
    '                            RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                            RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
    '                            Exit While
    '                        End If
    '                    Catch ex As Exception
    '                        'If Attempts > 100 Then
    '                        '    RaiseEvent ErrorMessage("100 attempts to find a valid sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                        '    RaiseEvent ErrorMessage("The value for " & ColumnName & " has not been generated." & vbCrLf)
    '                        '    Exit While
    '                        'End If
    '                    End Try
    '                End While
    '            Next

    '        Case "Latin Hypercube"
    '            'Get the first value - checking that the InvCDF function returns a valid value
    '            ValidVal = False
    '            Attempts = 0
    '            While ValidVal = False
    '                Try
    '                    Attempts += 1
    '                    PVal = Rnd() / NTrials 'Select a random probability number between 0 and 1 / NTrials
    '                    SampVal = MathNet.Numerics.Distributions.TruncatedPareto.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
    '                    ValidVal = True
    '                Catch ex As Exception
    '                    If Attempts > 100 Then
    '                        RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                        RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
    '                        Exit While
    '                    End If
    '                End Try
    '                If SampVal = Double.NaN Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.PositiveInfinity Then
    '                    ValidVal = False
    '                    'ElseIf Double.NegativeInfinity Then
    '                ElseIf SampVal = Double.NegativeInfinity Then
    '                    ValidVal = False
    '                Else
    '                    Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
    '                    ValidVal = True
    '                End If
    '            End While

    '            'Get the intermediate values - the InvCDF function should be able to return valid values for these
    '            For Trial = 2 To NTrials - 1
    '                PVal = (Rnd() + Trial - 1) / NTrials
    '                Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.TruncatedPareto.InvCDF(ParamA, ParamB, ParamC, PVal)
    '            Next

    '            'Get the last value - checking that the InvCDF function returns a valid value
    '            ValidVal = False
    '            Attempts = 0
    '            While ValidVal = False
    '                Try
    '                    Attempts += 1
    '                    PVal = (Rnd() + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
    '                    SampVal = MathNet.Numerics.Distributions.TruncatedPareto.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
    '                    ValidVal = True
    '                Catch ex As Exception
    '                    If Attempts > 100 Then
    '                        RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                        RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
    '                        Exit While
    '                    End If
    '                End Try
    '                If SampVal = Double.NaN Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.PositiveInfinity Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.NegativeInfinity Then
    '                    ValidVal = False
    '                Else
    '                    Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
    '                    ValidVal = True
    '                End If
    '            End While
    '            ShuffleColumn(TableName, ColumnName)

    '        Case "Sorted Latin Hypercube"
    '            'Get the first value - checking that the InvCDF function returns a valid value
    '            ValidVal = False
    '            Attempts = 0
    '            While ValidVal = False
    '                Try
    '                    Attempts += 1
    '                    PVal = Rnd() / NTrials 'Select a random probability number between 0 and 1 / NTrials
    '                    SampVal = MathNet.Numerics.Distributions.TruncatedPareto.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
    '                    ValidVal = True
    '                Catch ex As Exception
    '                    If Attempts > 100 Then
    '                        RaiseEvent ErrorMessage("100 attempts to find a valid first Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                        RaiseEvent ErrorMessage("The first value for " & ColumnName & " has not been generated." & vbCrLf)
    '                        Exit While
    '                    End If
    '                End Try
    '                If SampVal = Double.NaN Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.PositiveInfinity Then
    '                    ValidVal = False
    '                    'ElseIf Double.NegativeInfinity Then
    '                ElseIf SampVal = Double.NegativeInfinity Then
    '                    ValidVal = False
    '                Else
    '                    Data.Tables(TableName).Rows(0).Item(ColumnName) = SampVal
    '                    ValidVal = True
    '                End If
    '            End While

    '            'Get the intermediate values - the InvCDF function should be able to return valid values for these
    '            For Trial = 2 To NTrials - 1
    '                PVal = (Rnd() + Trial - 1) / NTrials
    '                Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.TruncatedPareto.InvCDF(ParamA, ParamB, ParamC, PVal)
    '            Next

    '            'Get the last value - checking that the InvCDF function returns a valid value
    '            ValidVal = False
    '            Attempts = 0
    '            While ValidVal = False
    '                Try
    '                    Attempts += 1
    '                    PVal = (Rnd() + NTrials - 1) / NTrials 'Select a random probability number between (NTrials - 1) / NTrials  and 1 
    '                    SampVal = MathNet.Numerics.Distributions.TruncatedPareto.InvCDF(ParamA, ParamB, ParamC, PVal) 'Attempt to get a valid distribution value corresponding to PVal
    '                    ValidVal = True
    '                Catch ex As Exception
    '                    If Attempts > 100 Then
    '                        RaiseEvent ErrorMessage("100 attempts to find a valid last Latin Hypercube sample value for the variable: " & ColumnName & " in table " & TableName & " have failed." & vbCrLf)
    '                        RaiseEvent ErrorMessage("The last value for " & ColumnName & " has not been generated." & vbCrLf)
    '                        Exit While
    '                    End If
    '                End Try
    '                If SampVal = Double.NaN Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.PositiveInfinity Then
    '                    ValidVal = False
    '                ElseIf SampVal = Double.NegativeInfinity Then
    '                    ValidVal = False
    '                Else
    '                    Data.Tables(TableName).Rows(NTrials - 1).Item(ColumnName) = SampVal
    '                    ValidVal = True
    '                End If
    '            End While

    '        Case "Median Latin Hypercube"
    '            For Trial = 1 To NTrials
    '                PVal = (Trial - 0.5) / NTrials
    '                Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.TruncatedPareto.InvCDF(ParamA, ParamB, ParamC, PVal)
    '            Next
    '            ShuffleColumn(TableName, ColumnName)

    '        Case "Sorted Median Latin Hypercube"
    '            For Trial = 1 To NTrials
    '                PVal = (Trial - 0.5) / NTrials
    '                Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = MathNet.Numerics.Distributions.TruncatedPareto.InvCDF(ParamA, ParamB, ParamC, PVal)
    '            Next

    '        Case Else
    '            RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)

    '    End Select
    'End Sub

    Private Sub GenerateBernoulliData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Bernoulli discrete distribution data in the Specified Table and Column.
        'The P(success) parameter is contained in the ParamA variable.

        Dim Trial As Integer 'Loop index
        Dim PVal As Double 'The probability value used to generate each Bernouli data value.

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                For Each Row As DataRow In Data.Tables(TableName).Rows
                    If myRandom.NextDouble <= ParamA Then
                        Row.Item(ColumnName) = 1
                    Else
                        Row.Item(ColumnName) = 0
                    End If
                Next

            Case "Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    If PVal <= ParamA Then
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = 1
                    Else
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = 0
                    End If
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (myRandom.NextDouble + Trial - 1) / NTrials
                    If PVal <= ParamA Then
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = 1
                    Else
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = 0
                    End If
                Next

            Case "Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    If PVal <= ParamA Then
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = 1
                    Else
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = 0
                    End If
                Next
                ShuffleColumn(TableName, ColumnName)

            Case "Sorted Median Latin Hypercube"
                For Trial = 1 To NTrials
                    PVal = (Trial - 0.5) / NTrials
                    If PVal <= ParamA Then
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = 1
                    Else
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = 0
                    End If
                Next

            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
    End Sub

    Private Sub GenerateBinomialData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Binomial discrete distribution data in the Specified Table and Column.
        'The P(success) and N Trials parameters are contained in the ParamA and ParamB variables.



    End Sub


    'Private Sub GenerateCategoricalData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
    '    'Generate Categorical discrete distribution data in the Specified Table and Column.
    '    'To Do



    'End Sub


    Private Sub GenerateDiscreteUniformData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Discrete Uniform distribution data in the Specified Table and Column.
        'The a and b parameters are contained in the ParamA and ParamB variables.



    End Sub

    Private Sub GenerateGeometricData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Geometric discrete distribution data in the Specified Table and Column.
        'The P(success) parameter is contained in the ParamA variable.



    End Sub


    Private Sub GenerateHypergeometricData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Hypergeometric discrete distribution data in the Specified Table and Column.
        'The Population, Successes and Draws parameters are contained in the ParamA, ParamB and ParamC variables.

        'Step 1 - Generate initial set of Cumulative probabilities.
        Dim ProbArraySize As Integer = 32 'The initial cumulative probability array size.
        Dim MaxArraySize As Integer = 8192 'The maximum probability array size.
        Dim MaxIndexValue As Integer = 65536 'The maximum index value to search.
        Dim ProbCutoff As Double = 0.9999 'The cumulative probability cutoff to used in the cumulative probability array
        Dim CumProb(0 To ProbArraySize - 1) As Double
        Dim I As Integer
        Dim StartValue As Integer = 0 'The start value of the discrete distribution (0 or 1)
        Dim StartCalcIndex As Integer = 0

        'For debugging: ######################################################################################################
        RaiseEvent Message("Generating Hypergeometric Data" & vbCrLf)
        RaiseEvent Message("Index   CDF" & vbCrLf)

        For I = 0 To ProbArraySize - 1
            CumProb(I) = MathNet.Numerics.Distributions.Hypergeometric.CDF(ParamA, ParamB, ParamC, I + StartValue)

            ''For debugging: ##################################################################################################
            'RaiseEvent Message(Format(I, "N5") & "  " & Format(CumProb(I), "N8") & vbCrLf)
        Next

        'Step 2 - If neccessary, expand the Cumulative probability array until the maximum probability cutoff is reached.
        Dim MaxProb As Double = CumProb(ProbArraySize - 1)
        While MaxProb < ProbCutoff
            'Increase the array size and add more cumulatibe probabilities.
            StartCalcIndex = ProbArraySize 'Start calculating new cumulative probabilites from here
            ProbArraySize *= 2 'Double the array size
            ReDim Preserve CumProb(0 To ProbArraySize - 1)


            For I = StartCalcIndex To ProbArraySize - 1
                CumProb(I) = MathNet.Numerics.Distributions.Hypergeometric.CDF(ParamA, ParamB, ParamC, I + StartValue)

                ''For debugging: ##############################################################################################
                'RaiseEvent Message(Format(I, "N5") & "  " & Format(CumProb(I), "N8") & vbCrLf)
            Next
            MaxProb = CumProb(ProbArraySize - 1)
            If ProbArraySize >= MaxArraySize Then
                RaiseEvent Message("Hypergeometric cumulative probability array has reached the maximum size of " & MaxArraySize & " items." & vbCrLf)
                RaiseEvent Message("The maximum probability is " & MaxProb & vbCrLf)
                Exit While
            End If
        End While

        'Step 3 - Generate the Cumulative probability lookup table.
        '         This is an array of 100 pointers (0 to 99)
        '         Each pointer points to a cell in the CumProb array
        '         A probability p (0 to 1) is converted to a 0 to 100 index using the formula Int(p * 100)
        '         The pointer at the corresponding index in the lookup array points to the cell in the CumProb array to start searching for the corresponding cumulative probability.

        ''For debugging: ######################################################################################################
        'RaiseEvent Message("Lookup Table" & vbCrLf)
        'RaiseEvent Message("ProbPct   CumProbIndex" & vbCrLf)

        Dim Lookup(0 To 99) As Integer 'The lookup array
        Lookup(0) = 0
        Dim Prob As Double
        Dim CumProbIndex As Integer = 0 'Points to the current cell in the CumProb array
        Dim MaxIndex As Integer = CumProb.Count - 1 'The maximum index number in the CumProb array
        'For I = 1 To 99
        'For I = 0 To 99
        'For I = 1 To 100
        For I = 0 To 99
            Prob = I / 100
            While CumProb(CumProbIndex) <= Prob
                If CumProbIndex < MaxIndex Then
                    CumProbIndex += 1
                Else
                    Exit While
                End If
            End While
            'Lookup(I) = CumProbIndex - 1
            If CumProbIndex = 0 Then
                Lookup(I) = 0
            Else
                Lookup(I) = CumProbIndex - 1
            End If


            ''For debugging: ##################################################################################################
            'RaiseEvent Message(Format(I, "N5") & "  " & Format(Lookup(I), "N5") & vbCrLf)
        Next

        'Step 4 - Generate the hypergeometric data (NSuccesses) from random numbers (between 0 and 1)
        ' 
        Dim Rand As Double
        Dim RandPct As Integer

        Select Case Sampling
            Case "N/A"
                RaiseEvent ErrorMessage("Selected sampling is N/A." & vbCrLf)

            Case "Random"
                'For debugging: ##############################################################################################
                RaiseEvent Message("Hypergeometric data" & vbCrLf)
                Dim StartTime As Date = Now
                Dim Duration As TimeSpan
                RaiseEvent Message("myRandom.NextDouble    Hypergeometric" & vbCrLf)


                For Trial = 1 To NTrials
                    Rand = myRandom.NextDouble

                    ''For debugging: ##########################################################################################
                    'RaiseEvent Message(Format(Rand, "N5") & "  ")

                    RandPct = Int(Rand * 100)
                    CumProbIndex = Lookup(RandPct) 'Start searching for a CumProb value larger than Rand. LookUp(RandPct) is the index to start the search.

                    If Rand >= CumProb(CumProbIndex) Then 'Search increasing index numbers in CumProb
                        'While Rand <= CumProb(CumProbIndex)
                        While Rand >= CumProb(CumProbIndex)
                            CumProbIndex += 1
                            If CumProbIndex > MaxIndex Then Exit While
                        End While
                        If CumProbIndex > MaxIndex Then 'Continue searching beyond the CumProb values
                            While Rand >= MathNet.Numerics.Distributions.Hypergeometric.CDF(ParamA, ParamB, ParamC, CumProbIndex + StartValue)
                                CumProbIndex += 1
                                If CumProbIndex > MaxIndexValue Then
                                    RaiseEvent ErrorMessage("Error finding HyperGeometric data value. Maximum index value searched: " & MaxIndexValue & vbCrLf)
                                    Exit While
                                End If
                            End While
                        End If
                        Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = CumProbIndex - 1 + StartValue
                        ''For debugging: ######################################################################################
                        'RaiseEvent Message(Format(CumProbIndex - 1 + StartValue, "N5") & vbCrLf)
                    Else 'Search decreasing index numbers in CumProb
                        If CumProbIndex = 0 Then
                            Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = CumProbIndex + StartValue
                        Else
                            While Rand < CumProb(CumProbIndex)
                                CumProbIndex -= 1
                                If CumProbIndex = 0 Then Exit While
                            End While
                            Data.Tables(TableName).Rows(Trial - 1).Item(ColumnName) = CumProbIndex + StartValue
                        End If

                        ''For debugging: ######################################################################################
                        'RaiseEvent Message(Format(CumProbIndex + StartValue, "N5") & vbCrLf)
                    End If
                Next

                RaiseEvent Message("Generated Hypergeometric data: " & " Time taken: " & Duration.TotalMilliseconds & " ms" & vbCrLf)


            Case "Latin Hypercube"


            Case "Sorted Latin Hypercube"



            Case "Median Latin Hypercube"



            Case "Sorted Median Latin Hypercube"



            Case Else
                RaiseEvent ErrorMessage("Unknown sampling: " & Sampling & vbCrLf)
        End Select
        CumProbIndex = Lookup(RandPct) 'Initial estimate of NSuccesses from the Lookup table.


    End Sub


    Private Sub GenerateNegativeBinomialData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate NegativeBinomial discrete distribution data in the Specified Table and Column.
        'The r and P(success) parameters are contained in the ParamA and ParamB variables.


    End Sub

    Private Sub GeneratePoissonData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Poisson discrete distribution data in the Specified Table and Column.
        'The Lambda parameter is contained in the ParamA variable.


    End Sub


    Private Sub GenerateZipfData(ByVal TableName As String, ByVal ColumnName As String, Sampling As String)
        'Generate Zipf discrete distribution data in the Specified Table and Column.
        'The s and n parameters are contained in the ParamA and ParamB variables.


    End Sub




    Public Sub ApplyCorrMatrix(ByVal CorrMatName As String)
        'Apply the specified correlation matrix.
        'This method assumes that the required random variable data has been generated in the Calculations table.

        If Correlations.ContainsKey(CorrMatName) Then
            Try
                If Data.Tables.Contains("Calculations") Then
                    'OK the DataSet contains the Calculations table.
                Else
                    RaiseEvent ErrorMessage("The Calculations table is missing from the Dataset." & vbCrLf)
                    Exit Sub
                End If

                Dim NVars As Integer = Correlations(CorrMatName).NVariables
                Dim MissingVarTable As Boolean = False
                For Each item In Correlations(CorrMatName).UnCorrRV 'UnCorrRV are the input uncorrelated random variables.
                    If Data.Tables("Calculations").Columns.Contains(item) Then
                        'The input random variable table exists.
                    Else
                        MissingVarTable = True
                        RaiseEvent ErrorMessage("Missing random variable table: " & item & vbCrLf)
                    End If
                Next

                If MissingVarTable = True Then
                    RaiseEvent ErrorMessage("The correlation matrix will not be applied because one or more random variable tables are missing." & vbCrLf)
                    Exit Sub
                End If

                RaiseEvent Message("Correlating the Random Variables:" & vbCrLf)

                'Create a new table called Correlations - used for Correlation processing:
                If Data.Tables.Contains("CorrCalcs") Then 'Clear the existing data from the table
                    Data.Tables("CorrCalcs").Clear()
                    Data.Tables("CorrCalcs").Reset()
                Else
                    Data.Tables.Add("CorrCalcs") 'Add the table
                End If

                'Add a RowNo column to the CorrCalcs table:
                Dim I As Integer = 0
                CreateNewColumn("CorrCalcs", "RowNo", "Int16")
                For I = 1 To NTrials 'RowNo ranges from 1 to NTrails
                    Data.Tables("CorrCalcs").Rows.Add()
                    Data.Tables("CorrCalcs").Rows(I - 1).Item("RowNo") = I
                Next

                'Step 1: Generate the Score columns:
                Dim StartTime As Date = Now
                Dim Duration As TimeSpan

                Dim ScoreName As String
                I = 0
                RaiseEvent Message("Generating the Score columns." & vbCrLf)
                For Each item In Correlations(CorrMatName).UnCorrRV
                    Try
                        'RaiseEvent Message("Generating the Score columns." & vbCrLf)
                        ScoreName = "Score_" & I
                        CreateRVScoreColumn("CorrCalcs", ScoreName, "Double")
                        If I > 0 Then ShuffleColumn("CorrCalcs", ScoreName) '(Dont shuffle Score_0)
                        Duration = Now - StartTime
                        RaiseEvent Message("Generated Score column: " & item & " Time taken: " & Duration.TotalMilliseconds & " ms" & vbCrLf)
                        I += 1
                    Catch ex As Exception
                        RaiseEvent ErrorMessage("Error generating the Score columns: " & vbCrLf & ex.Message & vbCrLf)
                    End Try
                Next

                'Step 2: Generate the Ranking Matrix:
                'Genearate the Covariance of the Score columns (Covariance Matrix E):
                Dim CovE = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.Dense(NVars, NVars) 'Create the Covariance matrix directly as a MathNet matrix.
                Dim Col1 As String
                Dim Col2 As String
                Dim J As Integer
                For I = 0 To NVars - 2
                    Col1 = "Score_" & I
                    CovE(I, I) = 1
                    For J = I + 1 To NVars - 1
                        Col2 = "Score_" & J
                        CovE(I, J) = Covariance("CorrCalcs", Col1, Col2)
                        CovE(J, I) = CovE(I, J)
                    Next
                Next
                CovE(NVars - 1, NVars - 1) = 1

                Dim CholE = CovE.Cholesky 'The Cholesky factor of the Covariance matrix
                Dim TransCholE = CholE.Factor.Transpose 'The Transpose Cholesky Factor of the Covariance matrix (Cholesky F)
                Dim InvTransCholE = TransCholE.Inverse 'The inverse of Cholesky F

                Dim TargetCorrS = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(Correlations(CorrMatName).Array)
                Dim CholTargetCorrS = TargetCorrS.Cholesky.Factor
                Dim TransCholTargetCorrS = CholTargetCorrS.Transpose

                Dim RankingMatrix = InvTransCholE.Multiply(TransCholTargetCorrS)

                'Step 3: Generate the Intermediate Matrix T by multiplying the Score columns with the Ranking Matrix:
                Dim NewRow(0 To NVars - 1) As Double 'Stores the new Score values for each row after mutiplication with the RankingMatrix.
                Dim ColIndex(0 To NVars - 1) 'Stores the column index number of each Score column.
                For I = 0 To NVars - 1
                    ColIndex(I) = Data.Tables("CorrCalcs").Columns.IndexOf("Score_" & I)
                Next

                For Each Row As DataRow In Data.Tables("CorrCalcs").Rows
                    For I = 0 To NVars - 1 'Process each Score value
                        NewRow(I) = 0 'Reset each score value
                        For J = 0 To NVars - 1
                            NewRow(I) += Row.Item(ColIndex(J)) * RankingMatrix(J, I) 'Update each score value using the RankingMatrix
                        Next
                    Next
                    'Save the new row of scores to the CorrCalcs table, overwriting the old row of scores.
                    For I = 0 To NVars - 1
                        Row.Item(ColIndex(I)) = NewRow(I)
                    Next
                Next



                'Create a temporary table called Scratch - used for generating sorted random variable data:
                If Data.Tables.Contains("Scratch") Then 'Clear the existing data from the table
                    Data.Tables("Scratch").Clear()
                    Data.Tables("Scratch").Reset()
                Else
                    Data.Tables.Add("Scratch") 'Add the table
                End If

                'Put a sorted version of the first column into CorrCalcs:
                CopyColumn("Calculations", Correlations(CorrMatName).UnCorrRV(0), "Scratch") 'Copy the  first uncorrelated random variable data to the Scratch table
                SortColumn("Scratch", Correlations(CorrMatName).UnCorrRV(0), True) 'Sort the uncorrelated random variable data (sort ascending)
                CopyColumn("Scratch", Correlations(CorrMatName).UnCorrRV(0), "CorrCalcs", Correlations(CorrMatName).CorrRV(0)) 'Copy the first sorted uncorrelated random variable data to the CorrCalcs table - using its Correlated RV name.

                For I = 1 To Correlations(CorrMatName).NVariables - 1
                    'Sort CorrCalcs on the Ith Score column:
                    SortColumn("CorrCalcs", "Score_" & I, True) 'Sort in ascending order
                    CopyColumn("Calculations", Correlations(CorrMatName).UnCorrRV(I), "Scratch") 'Copy the  next uncorrelated random variable data to the Scratch table
                    SortColumn("Scratch", Correlations(CorrMatName).UnCorrRV(I), True) 'Sort the uncorrelated random variable data (sort ascending)
                    CopyColumn("Scratch", Correlations(CorrMatName).UnCorrRV(I), "CorrCalcs", Correlations(CorrMatName).CorrRV(I)) 'Copy the next sorted uncorrelated random variable data to the CorrCalcs table - using its Correlated RV name.
                Next

                Data.Tables.Remove("Scratch")

                'Randomise RowNo if you dont want the first random variable column to be sorted.
                ShuffleColumn("CorrCalcs", "RowNo")

                SortColumn("CorrCalcs", "RowNo", True) 'Sort RowNo ascending - The random variable columns should now be correlated!

                'Copy the correlated random variables back to the Calculations table:
                For Each item In Correlations(CorrMatName).CorrRV
                    If Data.Tables("Calculations").Columns.Contains(item) Then 'Copy the column data in CorrCalcs to the existing column in Calculations
                        CopyColumnData("CorrCalcs", item, "Calculations", item)
                    Else 'Copy the column in CorrCalcs to Calculatons
                        CopyColumn("CorrCalcs", item, "Calculations")
                    End If
                Next

                Data.Tables.Remove("CorrCalcs") 'Remove this - getting error when running a second correlation if this is not removed!

                Duration = Now - StartTime
                RaiseEvent Message("Generation of correlated random variables complete.  Time taken: " & Duration.TotalMilliseconds & " ms" & vbCrLf & vbCrLf)

            Catch ex As Exception
                RaiseEvent ErrorMessage("Error applying Correlation Matrix:" & vbCrLf & ex.Message & vbCrLf)
            End Try
        Else
            RaiseEvent ErrorMessage("Correlation matrix not found: " & CorrMatName & vbCrLf)
        End If
    End Sub

    Public Sub GetSortedVarsRandomScores(ByVal CorrMatName As String)
        'Get the Sorted Random Variables and Random Scores in the CorrCalcs table.
        'This is the first stage of the ApplyCorrMatrix() method above.
        'This method is used to demonstrate the first stage of the Iman-Conover process.

        'This method assumes that the required random variable data has been generated in the Calculations table.

        If Correlations.ContainsKey(CorrMatName) Then
            If Data.Tables.Contains("Calculations") Then
                'OK the DataSet contains the Calculations table.
            Else
                RaiseEvent ErrorMessage("The Calculations table is missing from the DataSet." & vbCrLf)
                Exit Sub
            End If

            Dim NVars As Integer = Correlations(CorrMatName).NVariables
            Dim MissingVarTable As Boolean = False
            For Each item In Correlations(CorrMatName).UnCorrRV 'UnCorrRV are the input uncorrelated random variables.
                If Data.Tables("Calculations").Columns.Contains(item) Then
                    'The input random variable table exists.
                Else
                    MissingVarTable = True
                    RaiseEvent ErrorMessage("Missing random variable table: " & item & vbCrLf)
                End If
            Next
            If MissingVarTable = True Then
                RaiseEvent ErrorMessage("The correlation matrix will not be applied because one or more random variable tables are missing." & vbCrLf)
                Exit Sub
            Else
                For Each item In Correlations(CorrMatName).CorrRV 'If CorrRV names are different from the corresponding UnCorrRV names, the correlated random variable values are saved under the different CorrRV name.en
                    If Data.Tables("Calculations").Columns.Contains(item) Then
                        'The correlated random variable table exists.
                    Else
                        'Create a table to contain the correlated random variable:
                        Data.Tables("Calculations").Columns.Add(item, System.Type.GetType("System.Single"))
                    End If
                Next
            End If

            'Create a new table called Correlations - used for Correlation processing:
            If Data.Tables.Contains("CorrCalcs") Then 'Clear the existing data from the table
                Data.Tables("CorrCalcs").Clear()
                Data.Tables("CorrCalcs").Reset()
            Else
                Data.Tables.Add("CorrCalcs") 'Add the table
            End If

            'Create a temporary table called Scratch - used for generating sorted random variable data:
            If Data.Tables.Contains("Scratch") Then 'Clear the existing data from the table
                Data.Tables("Scratch").Clear()
                Data.Tables("Scratch").Reset()
            Else
                Data.Tables.Add("Scratch") 'Add the table
            End If

            'Step 1: Copy sorted versions of each UnCorrRV to the CorrCalcs table
            Dim StartTime As Date = Now
            Dim Duration As TimeSpan
            Dim I As Integer = 0
            Dim ScoreName As String
            For Each item In Correlations(CorrMatName).UnCorrRV
                Try
                    RaiseEvent Message("Generating a sorted version of random variable: " & item & vbCrLf)
                    CopyColumn("Calculations", item, "Scratch") 'Copy the uncorrelated random variable data to the Scratch table
                    SortColumn("Scratch", item, False) 'Sort the uncorrelated random variable data (sort descending)
                    CopyColumn("Scratch", item, "CorrCalcs") 'Copy the sorted uncorrelated random variable data to the CorrCalcs table
                    Duration = Now - StartTime
                    RaiseEvent Message("Generated sorted random variable: " & item & " Time taken: " & Duration.TotalMilliseconds & " ms" & vbCrLf)

                    'Step 2: Generate a Randomized Normal Score column for each UnCorrRV column
                    ScoreName = "Score_" & I
                    CreateRVScoreColumn("CorrCalcs", ScoreName, "Double")
                    ShuffleColumn("CorrCalcs", ScoreName)
                    Duration = Now - StartTime
                    RaiseEvent Message("Generated Score column: " & item & " Time taken: " & Duration.TotalMilliseconds & " ms" & vbCrLf)
                    I += 1
                Catch ex As Exception
                    RaiseEvent ErrorMessage("Error generating sorted random variables: " & vbCrLf & ex.Message & vbCrLf)
                End Try
            Next
            Data.Tables.Remove("Scratch")

        Else
            RaiseEvent ErrorMessage("Correlation matrix not found: " & CorrMatName & vbCrLf)
        End If
    End Sub

    Public Sub RankScores(ByVal CorrMatName As String)
        'Rank the Random Scores in the CorrCalcs table.
        'This is the second stage of the ApplyCorrMatrix() method above.
        'This method is used to demonstrate the second stage of the Iman-Conover process.

        If Correlations.ContainsKey(CorrMatName) Then
            Try
                Dim NVars As Integer = Correlations(CorrMatName).NVariables

                'Step 3a: Calculate the Covariance of the Randomized Normal Scores (Covariance E).
                Dim CovE = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.Dense(NVars, NVars) 'Create the Covariance matrix directly as a MathNet matrix.
                Dim Col1 As String
                Dim Col2 As String
                Dim J As Integer
                For I = 0 To NVars - 2
                    Col1 = "Score_" & I
                    CovE(I, I) = 1
                    For J = I + 1 To NVars - 1
                        Col2 = "Score_" & J
                        CovE(I, J) = Covariance("CorrCalcs", Col1, Col2)
                        CovE(J, I) = CovE(I, J)
                    Next
                Next
                CovE(NVars - 1, NVars - 1) = 1

                'Step 3b: Calculate the Transposed Cholesky of Covariance E (Cholesky F)
                Dim CholE = CovE.Cholesky.Factor
                Dim TransCholE = CholE.Transpose

                'Step 3c: Calculate the Inverse of Cholesky F (Inverse F)
                Dim InvTransCholE = TransCholE.Inverse

                'Step 4: Calculate the Transposed Cholesky of the Target Correlation (Cholesky C)
                Dim TargetCorrS = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfArray(Correlations(CorrMatName).Array)
                Dim CholTargetCorrS = TargetCorrS.Cholesky.Factor
                Dim TransCholTargetCorrS = CholTargetCorrS.Transpose

                'Step 5: Calculate the Inverse F multiplied by Cholesky C (Inverse F Mult Cholesky C) (Ranking Matrix)
                Dim RankingMatrix = InvTransCholE.Multiply(TransCholTargetCorrS)

                'Step 6: Matrix Multiply the Randomized Normal Scores with the Inverse F Mult Cholesky C - this is done in-place within the CorrCalcs table
                '        The Randomised Normal Scores data is now the Intermediate Matrix T - the rank of each column is the rank of each corresponding random variable needed to achieve the required correlation.
                'Score1, Score2, Score3, Score4 x RankMat(1,1), RankMat(1,2), RankMat(1,3), RankMar(1,4)
                '                                 RankMat(2,1), RankMat(2,2), RankMat(2,3), RankMar(2,4)
                '                                 RankMat(3,1), RankMat(3,2), RankMat(3,3), RankMar(3,4)
                '                                 RankMat(4,1), RankMat(4,2), RankMat(4,3), RankMar(4,4)
                Dim NewRow(0 To NVars - 1) As Double 'Stores the new Score values for each row after mutiplication with the RankingMatrix.
                Dim ColIndex(0 To NVars - 1) 'Stores the column index number of each Score column.
                For I = 0 To NVars - 1
                    ColIndex(I) = Data.Tables("CorrCalcs").Columns.IndexOf("Score_" & I)
                Next

                For Each Row As DataRow In Data.Tables("CorrCalcs").Rows
                    For I = 0 To NVars - 1 'Process each Score value
                        NewRow(I) = 0 'Reset each score value
                        For J = 0 To NVars - 1
                            NewRow(I) += Row.Item(ColIndex(J)) * RankingMatrix(J, I) 'Update each score value using the RankingMatrix
                        Next
                    Next
                    'Save the new row of scores to the table, overwriting the old row of scores.
                    For I = 0 To NVars - 1
                        Row.Item(ColIndex(I)) = NewRow(I)
                    Next
                Next

                'Step 7: For each Random Variable in CorrCalcs, sort on the corresponding Matrix T column and copy the rearranged random variable back into the Calculations table.

            Catch ex As Exception
                RaiseEvent ErrorMessage("Error while Ranking Scores:" & vbCrLf & ex.Message & vbCrLf)
            End Try
        Else
            RaiseEvent ErrorMessage("Correlation matrix not found: " & CorrMatName & vbCrLf)
        End If
    End Sub

    Public Sub RankVariables(ByVal CorrMatName As String)
        'Rank the Random Variables in the CorrCalcs table, using the Ranked Scores, and copy back to the Calculations Table.
        'This is the Ttird stage of the ApplyCorrMatrix() method above.
        'This method is used to demonstrate the third stage of the Iman-Conover process.

        If Correlations.ContainsKey(CorrMatName) Then
            'Step 7: For each Random Variable in CorrCalcs, sort on the corresponding Matrix T column and copy the rearranged random variable back into the Calculations table.
            Dim NVars As Integer = Correlations(CorrMatName).NVariables
            Dim CorrVarName As String 'The name of the Correlated variable
            For I = 0 To NVars - 1
                Data.Tables("CorrCalcs").DefaultView.Sort = "Score_" & I & " ASC" 'Sort the table on each Score column
                CorrVarName = Correlations(CorrMatName).CorrRV(I)
                CopyColumn("CorrCalcs", Correlations(CorrMatName).UnCorrRV(I), "Calculations", Correlations(CorrMatName).CorrRV(I))
            Next
        Else
            RaiseEvent ErrorMessage("Correlation matrix not found: " & CorrMatName & vbCrLf)
        End If
    End Sub

    Private Function Covariance(ByVal TableName As String, ByVal Col1 As String, ByVal Col2 As String) As Double
        'Calculate the Covariance between columns Col1 and Col2 in table TableName.
        If Data.Tables.Contains(TableName) Then
            If Data.Tables(TableName).Columns.Contains(Col1) Then
                If Data.Tables(TableName).Columns.Contains(Col2) Then
                    Dim Mean1 As Double = Data.Tables(TableName).Compute("Avg(" & Col1 & ")", "") 'Col1 Mean
                    Dim Mean2 As Double = Data.Tables(TableName).Compute("Avg(" & Col2 & ")", "") 'Col2 Mean
                    Dim A As Double 'A = Col1 - Mean1
                    Dim B As Double 'B = Col2 - Mean2
                    Dim SumAB As Double = 0 'AB = (Col1 - Mean1) x (Col2 - Mean2)
                    For Each Row As DataRow In Data.Tables(TableName).Rows
                        A = Row.Item(Col1) - Mean1
                        B = Row.Item(Col2) - Mean2
                        SumAB += A * B
                    Next
                    Return SumAB / (Data.Tables(TableName).Rows.Count)
                Else
                    RaiseEvent ErrorMessage("There is no column named " & Col2 & " in table " & TableName & vbCrLf)
                    Return 0
                End If
            Else
                RaiseEvent ErrorMessage("There is no column named " & Col1 & " in table " & TableName & vbCrLf)
                Return 0
            End If
        Else
            RaiseEvent ErrorMessage("Covariance calculation error. There is no table named " & TableName & vbCrLf)
            Return 0
        End If
    End Function

    Public Sub ClearDataTable()
        Data.Clear()
        Data.Reset()
        'Fields.Clear()
    End Sub

    Public Sub CreateRVScoreTable(ByVal TableName As String, ByVal RVName As String, ByVal ScoreName As String)
        'Create a new table named TableName containing a Random Variable column and a Score column.
        'The Random Variable column named RVName is a copy of the corresponding column in DataTable.
        'The Score column named ScoreName is a copy of the corresponding column in DataTable.


    End Sub

    Public Sub CreateRVScoreColumn(ByVal TableName As String, ByVal ScoreName As String, ByVal DataType As String)
        'Create a new Score column named ScoreName in the table named TableName.
        'The Score column is sorted.

        If Data.Tables.Contains(TableName) Then
            Dim NRows As Integer = Data.Tables(TableName).Rows.Count
            If NRows = NTrials Then
                If Data.Tables(TableName).Columns.Contains(ScoreName) Then
                    'The Score column already exists.
                Else 'Create the Score column.
                    CreateNewColumn(TableName, ScoreName, DataType)
                End If
                Dim StdNormInv As Double
                Dim SumSq As Double = 0
                Dim Trial As Integer 'Loop index
                For Trial = 1 To NTrials
                    StdNormInv = MathNet.Numerics.Distributions.Normal.InvCDF(0, 1, Trial / (NTrials + 1)) 'The Normal distribution with a Mean of 0 and Std Dev of 1 is the Standard Normal distribution.
                    SumSq += StdNormInv ^ 2 'Calculate the sum of the squared Standard Normal Inverse CDF values. This is used later to rescale the Normal Scores.
                    Data.Tables(TableName).Rows(Trial - 1).Item(ScoreName) = StdNormInv 'Save the unscaled Normal Scores.
                Next
                Dim Scale As Double = Math.Sqrt(SumSq / NTrials) 'Calculate the scale factor
                For Trial = 1 To NTrials
                    Data.Tables(TableName).Rows(Trial - 1).Item(ScoreName) /= Scale
                Next
            Else
                RaiseEvent ErrorMessage("The number of rows in table " & TableName & " does not match the number of trials in the Monte Carlo model." & vbCrLf)
            End If
        Else
            RaiseEvent ErrorMessage("The table named " & TableName & " does not exist." & vbCrLf)
        End If
    End Sub

    'Public Sub CopyColumn(ByVal FromTableName As String, ByVal ColumnName As String, ByVal ToTableName As String)
    Public Sub CopyColumn(ByVal FromTableName As String, ByRef ColumnList() As String, ByVal ToTableName As String)
        'Copy the Column Names in ColumnList() in the Table named FromTableName to a table named ToTableName.
        'If ToTableName does not exist then create it.

        If Data.Tables.Contains(FromTableName) Then
            If Data.Tables.Contains(ToTableName) Then
                Dim myView As System.Data.DataView = New System.Data.DataView(Data.Tables(FromTableName)) 'CHECK THIS LINE???
                Dim newTable As System.Data.DataTable = myView.ToTable(ToTableName, False, ColumnList)  'CHECK THIS LINE???
                Dim I As Integer
                Try
                    For Each ColName In ColumnList
                        Dim ColType As System.Type
                        ColType = Data.Tables(FromTableName).Columns(ColName).DataType
                        Data.Tables(ToTableName).Columns.Add(ColName, ColType)

                        'Handle the case where ToTableName has less rows than FromTableName
                        If Data.Tables(ToTableName).Rows.Count < Data.Tables(FromTableName).Rows.Count Then
                            For I = 0 To Data.Tables(ToTableName).Rows.Count - 1
                                Data.Tables(ToTableName).Rows(I).Item(ColName) = Data.Tables(FromTableName).Rows(I).Item(ColName)
                            Next
                            For I = Data.Tables(ToTableName).Rows.Count To Data.Tables(FromTableName).Rows.Count - 1
                                Data.Tables(ToTableName).Rows.Add()
                                Data.Tables(ToTableName).Rows(I).Item(ColName) = Data.Tables(FromTableName).Rows(I).Item(ColName)
                            Next
                        Else
                            For I = 0 To Data.Tables(FromTableName).Rows.Count - 1
                                Data.Tables(ToTableName).Rows(I).Item(ColName) = Data.Tables(FromTableName).Rows(I).Item(ColName)
                            Next
                        End If
                    Next
                Catch ex As Exception
                    RaiseEvent ErrorMessage(ex.Message & vbCrLf)
                End Try
            Else
                Try
                    Dim myView As System.Data.DataView = New System.Data.DataView(Data.Tables(FromTableName))
                    Dim newTable As System.Data.DataTable = myView.ToTable(ToTableName, False, ColumnList)
                    Data.Tables.Add(newTable)
                Catch ex As Exception
                    RaiseEvent ErrorMessage(ex.Message & vbCrLf)
                End Try
            End If
            'Else
            '    RaiseEvent ErrorMessage("Copy Column not completed. The column was not found: " & ColumnName & vbCrLf)
            'End If
        Else
            RaiseEvent ErrorMessage("Copy Column not completed. The column source table was not found: " & FromTableName & vbCrLf)
        End If
    End Sub

    Public Sub CopyColumn(ByVal FromTableName As String, ByVal ColumnName As String, ByVal ToTableName As String)
        'Copy the Column named ColumnName in the Table named FromTableName to a table named ToTableName.
        'If ToTableName does not exist then create it.

        If Data.Tables.Contains(FromTableName) Then
            If Data.Tables.Contains(ToTableName) Then
                Dim I As Integer
                Try
                    'For Each ColName In ColumnList
                    Dim ColType As System.Type
                    ColType = Data.Tables(FromTableName).Columns(ColumnName).DataType
                    Data.Tables(ToTableName).Columns.Add(ColumnName, ColType)
                    Dim NRows As Integer = Data.Tables(FromTableName).Rows.Count
                    Dim NDestRows As Integer = Data.Tables(ToTableName).Rows.Count
                    If NDestRows < NRows Then
                        For I = 0 To NRows - NDestRows - 1
                            Data.Tables(ToTableName).Rows.Add()
                        Next
                    End If

                    I = 0
                    For Each Row As DataRowView In Data.Tables(FromTableName).DefaultView
                        Data.Tables(ToTableName).Rows(I).Item(ColumnName) = Row.Item(ColumnName)
                        I += 1
                    Next
                Catch ex As Exception
                    RaiseEvent ErrorMessage("Copy column error: " & ex.Message & vbCrLf)
                End Try
            Else
                Try
                    Dim myView As System.Data.DataView = New System.Data.DataView(Data.Tables(FromTableName))
                    Dim newTable As System.Data.DataTable = myView.ToTable(ToTableName, False, ColumnName)
                    Data.Tables.Add(newTable)
                Catch ex As Exception
                    RaiseEvent ErrorMessage(ex.Message & vbCrLf)
                End Try
            End If
        Else
            RaiseEvent ErrorMessage("Copy Column not completed. The column source table was not found: " & FromTableName & vbCrLf)
        End If
    End Sub

    Public Sub CopyColumn(ByVal FromTableName As String, ByVal FromColumnName As String, ByVal ToTableName As String, ByVal ToColumnName As String)
        'Copy the Column named ColumnName in the Table named FromTableName to a column named ToColumnName in the table named ToTableName.
        'If ToColumnName doesnt exist then it is created.
        'If ToColumnName has a different data type, it is deleted and a nerw version is created with the same data type as FromColumnName.

        If Data.Tables.Contains(FromTableName) Then
            If Data.Tables.Contains(ToTableName) Then
                If Data.Tables(ToTableName).Columns.Contains(ToColumnName) Then
                    If Data.Tables(FromTableName).Columns(FromColumnName).DataType = Data.Tables(ToTableName).Columns(ToColumnName).DataType Then 'The column data types are the same. The data can be copied.
                        Try 'Copy the data
                            Dim I As Integer = 0
                            For Each Row As DataRowView In Data.Tables(FromTableName).DefaultView
                                Data.Tables(ToTableName).DefaultView.Item(I).Item(ToColumnName) = Row.Item(FromColumnName)
                                I += 1
                            Next
                        Catch ex As Exception
                            RaiseEvent ErrorMessage("Copy column data error: " & ex.Message & vbCrLf)
                        End Try
                    Else 'The column data types are not the same.
                        Data.Tables(ToTableName).Columns.Remove(ToColumnName) 'Remove ToColumnName
                        Data.Tables(ToTableName).Columns.Add(ToColumnName, Data.Tables(FromTableName).Columns(FromColumnName).DataType) 'Add a new ToColumnName with the required data type.
                        Try 'Copy the data
                            Dim I As Integer = 0
                            For Each Row As DataRowView In Data.Tables(FromTableName).DefaultView
                                Data.Tables(ToTableName).DefaultView.Item(I).Item(ToColumnName) = Row.Item(FromColumnName)
                                I += 1
                            Next
                        Catch ex As Exception
                            RaiseEvent ErrorMessage("Copy column data error: " & ex.Message & vbCrLf)
                        End Try
                    End If
                Else
                    Data.Tables(ToTableName).Columns.Add(ToColumnName, Data.Tables(FromTableName).Columns(FromColumnName).DataType) 'Add the ToColumnName
                    Try 'Copy the data
                        Dim I As Integer = 0
                        For Each Row As DataRowView In Data.Tables(FromTableName).DefaultView
                            Data.Tables(ToTableName).DefaultView.Item(I).Item(ToColumnName) = Row.Item(FromColumnName)
                            I += 1
                        Next
                    Catch ex As Exception
                        RaiseEvent ErrorMessage("Copy column data error: " & ex.Message & vbCrLf)
                    End Try
                End If
            Else
                Try
                    Dim myView As System.Data.DataView = New System.Data.DataView(Data.Tables(FromTableName))
                    Dim newTable As System.Data.DataTable = myView.ToTable(ToTableName, False, FromColumnName)
                    Data.Tables.Add(newTable) 'Create the new table named ToTableName containing FromColumnName
                    Data.Tables(FromTableName).Columns(FromColumnName).ColumnName = ToColumnName 'Rename FromColumnName to ToColumnName
                Catch ex As Exception
                    RaiseEvent ErrorMessage(ex.Message & vbCrLf)
                End Try
            End If
        Else
            RaiseEvent ErrorMessage("Copy Column not completed. The column source table was not found: " & FromTableName & vbCrLf)
        End If
    End Sub

    Public Sub SortColumn(ByVal TableName As String, ByVal ColumnName As String, ByVal Ascending As Boolean)
        'Sort the selected column in the selected table.

        If Data.Tables.Contains(TableName) Then
            If Data.Tables(TableName).Columns.Contains(ColumnName) Then
                If Ascending = True Then
                    Data.Tables(TableName).DefaultView.Sort = ColumnName & " ASC"
                Else
                    Data.Tables(TableName).DefaultView.Sort = ColumnName & " DESC"
                End If
            Else
                RaiseEvent ErrorMessage("Column sort not completed. The Column was not found: " & ColumnName & vbCrLf)
            End If
        Else
            RaiseEvent ErrorMessage("Column sort not completed. The Table was not found: " & TableName & vbCrLf)
        End If
    End Sub

    Public Sub CopyColumnData(ByVal FromTableName As String, ByVal FromColumnName As String, ByVal ToTableName As String, ByVal ToColumnName As String)
        'Copy the Column Data in the FromColumnName column in the FromTableName table to the TopColumnName column in the ToTableName table.

        If Data.Tables.Contains(FromTableName) Then
            If Data.Tables(FromTableName).Columns.Contains(FromColumnName) Then
                If Data.Tables.Contains(ToTableName) Then
                    If Data.Tables(ToTableName).Columns.Contains(ToColumnName) Then
                        Try
                            Dim I As Integer = 0
                            For Each Row As DataRowView In Data.Tables(FromTableName).DefaultView
                                Data.Tables(ToTableName).Rows(I).Item(ToColumnName) = Row.Item(FromColumnName)
                                I += 1
                            Next
                        Catch ex As Exception
                            RaiseEvent ErrorMessage("Copy column data error: " & ex.Message & vbCrLf)
                        End Try
                    Else
                        RaiseEvent ErrorMessage("Copy Column Data not completed. The data destination Column was not found: " & ToColumnName & vbCrLf)
                    End If
                Else
                    RaiseEvent ErrorMessage("Copy Column Data not completed. The data destination Table was not found: " & ToTableName & vbCrLf)
                End If
            Else
                RaiseEvent ErrorMessage("Copy Column Data not completed. The data source Column was not found: " & FromColumnName & vbCrLf)
            End If
        Else
            RaiseEvent ErrorMessage("Copy Column Data not completed. The data source Table was not found: " & FromTableName & vbCrLf)
        End If
    End Sub

#End Region 'Methods --------------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Events - Events raised by this class." '=============================================================================================================================================
    Event ErrorMessage(ByVal Msg As String) 'Send an error message.
    Event Message(ByVal Msg As String) 'Send a normal message.
    Event DataLoaded() 'Data Loaded event - triggered whnever data is loaded.
#End Region 'Events ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    Class DataInformation
        'Data information
        'Includes information about Random Variables and other data set types including Trial Number, Probability Samples, Imported Data, Normal Scores and Data Table.

        'NOTE: The Name was the RandVars Dictionary Key so was not included in the RVInfo dictionary.
        'RVInfo is now stored in a List, so the Name is included.
        Private _name As String = "" 'The name of the Random Variable
        Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
            End Set
        End Property

        Private _row As Integer = 0 'The Row number of the Random Variable in the Grid View
        Property Row As Integer
            Get
                Return _row
            End Get
            Set(value As Integer)
                _row = value
            End Set
        End Property

        Private _units As String = "" 'The units of the Random Variable
        Property Units As String
            Get
                Return _units
            End Get
            Set(value As String)
                _units = value
            End Set
        End Property

        Private _unitsAbbrev As String = "" 'The abbreviated units of the Random Variable
        Property UnitsAbbrev As String
            Get
                Return _unitsAbbrev
            End Get
            Set(value As String)
                _unitsAbbrev = value
            End Set
        End Property

        Private _description As String = "" 'A description of the Random Variable
        Property Description As String
            Get
                Return _description
            End Get
            Set(value As String)
                _description = value
            End Set
        End Property

        Private _label As String = "" 'A label used when displaying the variable.
        Property Label As String
            Get
                Return _label
            End Get
            Set(value As String)
                _label = value
            End Set
        End Property


        'Private _distribution As String = "" 'The name of the Random Variable distribution
        'Property Distribution As String
        '    Get
        '        Return _distribution
        '    End Get
        '    Set(value As String)
        '        _distribution = value
        '    End Set
        'End Property

        Private _dataSetType As String = "" 'The name of the type of dataset. If the dataset is a distribution, this is the name of the distribution.
        Property DataSetType As String
            Get
                Return _dataSetType
            End Get
            Set(value As String)
                _dataSetType = value
            End Set
        End Property

        Private _isDiscrete As Boolean = False 'If True the random variable is discrete else continuous
        Property IsDiscrete As Boolean
            Get
                Return _isDiscrete
            End Get
            Set(value As Boolean)
                _isDiscrete = value
            End Set
        End Property

        Private _dataType As String = "Single" 'The type of data stored in the dataset. 
        '(Boolean, Byte, Char, DateTime, Decimal, Double, Integer_16, Integer_32, Integer_64, Signed_Byte, Single, String, TimeSpan, Unsigned_Integer_16, Unsigned_Integer_32, Unsigned_Integer_64)
        Property DataType As String
            Get
                Return _dataType
            End Get
            Set(value As String)
                _dataType = value
            End Set
        End Property

        Private _sampling As String = "" 'The sampling used for the distribution (N/A, Random, Latin Hypercube, Sorted Latin Hypercube, Median Latin Hypercube, Sorted Median Latin Hypercube)
        Property Sampling As String
            Get
                Return _sampling
            End Get
            Set(value As String)
                _sampling = value
            End Set
        End Property

        Private _table As String = "Calculations" 'The destination table for the Random Variable (Calculations, New Table, any existing Random Variable name.)
        Property Table As String
            Get
                Return _table
            End Get
            Set(value As String)
                _table = value
            End Set
        End Property

        Private _owner As String = "User" 'The owner of the variable (User or System)
        Property Owner As String
            Get
                Return _owner
            End Get
            Set(value As String)
                _owner = value
            End Set
        End Property

        Private _parameterAName As String = "" 'The name of Parameter A
        Property ParameterAName As String
            Get
                Return _parameterAName
            End Get
            Set(value As String)
                _parameterAName = value
            End Set
        End Property

        Private _parameterAValue As Double = Double.NaN 'The value of Parameter A
        Property ParameterAValue As Double
            Get
                Return _parameterAValue
            End Get
            Set(value As Double)
                _parameterAValue = value
            End Set
        End Property

        Private _parameterBName As String = "" 'The name of Parameter A
        Property ParameterBName As String
            Get
                Return _parameterBName
            End Get
            Set(value As String)
                _parameterBName = value
            End Set
        End Property

        Private _parameterBValue As Double = Double.NaN  'The value of Parameter A
        Property ParameterBValue As Double
            Get
                Return _parameterBValue
            End Get
            Set(value As Double)
                _parameterBValue = value
            End Set
        End Property

        Private _parameterCName As String = "" 'The name of Parameter A
        Property ParameterCName As String
            Get
                Return _parameterCName
            End Get
            Set(value As String)
                _parameterCName = value
            End Set
        End Property

        Private _parameterCValue As Double = Double.NaN  'The value of Parameter A
        Property ParameterCValue As Double
            Get
                Return _parameterCValue
            End Get
            Set(value As Double)
                _parameterCValue = value
            End Set
        End Property

        Private _parameterDName As String = "" 'The name of Parameter A
        Property ParameterDName As String
            Get
                Return _parameterDName
            End Get
            Set(value As String)
                _parameterDName = value
            End Set
        End Property

        Private _parameterDValue As Double = Double.NaN  'The value of Parameter A
        Property ParameterDValue As Double
            Get
                Return _parameterDValue
            End Get
            Set(value As Double)
                _parameterDValue = value
            End Set
        End Property

        Private _parameterEName As String = "" 'The name of Parameter A
        Property ParameterEName As String
            Get
                Return _parameterEName
            End Get
            Set(value As String)
                _parameterEName = value
            End Set
        End Property

        Private _parameterEValue As Double = Double.NaN  'The value of Parameter A
        Property ParameterEValue As Double
            Get
                Return _parameterEValue
            End Get
            Set(value As Double)
                _parameterEValue = value
            End Set
        End Property

        'Private _seed As Double = Double.NaN 'The seed value used to generate random numbers.
        'Property Seed As Double
        '    Get
        '        Return _seed
        '    End Get
        '    Set(value As Double)
        '        _seed = value
        '    End Set
        'End Property

        Private _seed As Integer = -1 'The seed value used to generate random numbers. If Seed it -1 a random seed based on the time will be used. Random objects should not be created within about 15ms of each other to avoid the same seed being used.
        Property Seed As Integer
            Get
                Return _seed
            End Get
            Set(value As Integer)
                _seed = value
            End Set
        End Property

        'Plot Display Settings: -------------------------------------------------------------------------------------------------------------

        Private _showPDF As Boolean = True 'If True, the Probability Density Distribution of the Random Variable will be shown on the plot
        Property ShowPDF As Boolean
            Get
                Return _showPDF
            End Get
            Set(value As Boolean)
                _showPDF = value
            End Set
        End Property

        Private _pdfLineColor As System.Drawing.Color = Color.Black 'The color of the PDF line
        Property PdfLineColor As System.Drawing.Color
            Get
                Return _pdfLineColor
            End Get
            Set(value As System.Drawing.Color)
                _pdfLineColor = value
            End Set
        End Property

        Private _pdfLineThickness As Integer = 1 'The thickness of the PDF line.
        Property PdfLineThickness As Integer
            Get
                Return _pdfLineThickness
            End Get
            Set(value As Integer)
                _pdfLineThickness = value
            End Set
        End Property

        Private _showPDFLn As Boolean = True 'If True, the Natural Logarithm of the Probability Density Distribution of the Random Variable will be shown on the plot
        Property ShowPDFLn As Boolean
            Get
                Return _showPDFLn
            End Get
            Set(value As Boolean)
                _showPDFLn = value
            End Set
        End Property

        Private _pdfLnLineColor As System.Drawing.Color = Color.Black 'The color of the PDF Ln line
        Property PdfLnLineColor As System.Drawing.Color
            Get
                Return _pdfLnLineColor
            End Get
            Set(value As System.Drawing.Color)
                _pdfLnLineColor = value
            End Set
        End Property

        Private _pdLnfLineThickness As Integer = 1 'The thickness of the PDF Ln line.
        Property PdfLnLineThickness As Integer
            Get
                Return _pdLnfLineThickness
            End Get
            Set(value As Integer)
                _pdLnfLineThickness = value
            End Set
        End Property

        Private _showCDF As Boolean = True 'If True, the Cumulative Distribution Function of the Random Variable will be shown on the plot
        Property ShowCDF As Boolean
            Get
                Return _showCDF
            End Get
            Set(value As Boolean)
                _showCDF = value
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


        Private _showRevCDF As Boolean = True 'If True, the Reverse Cumulative Distribution Function of the Random Variable will be shown on the plot
        Property ShowRevCDF As Boolean
            Get
                Return _showRevCDF
            End Get
            Set(value As Boolean)
                _showRevCDF = value
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
            End Set
        End Property

        Private _nDisplayPoints As Integer = 512 'The number of points used to display the Random Variable Distribution plot
        Property NDisplayPoints As Integer
            Get
                Return _nDisplayPoints
            End Get
            Set(value As Integer)
                _nDisplayPoints = value
            End Set
        End Property

        Private _xMin As Single = -5 'The minimum value of the Random Variable to plot
        Property XMin As Single
            Get
                Return _xMin
            End Get
            Set(value As Single)
                _xMin = value
            End Set
        End Property

        Private _autoXMin As Boolean = True 'If True, the XMin value will be chosen automatically
        Property AutoXMin As Boolean
            Get
                Return _autoXMin
            End Get
            Set(value As Boolean)
                _autoXMin = value
            End Set
        End Property

        Private _xMax As Single = 5 'The maximum value of the Random Variable to plot
        Property XMax As Single
            Get
                Return _xMax
            End Get
            Set(value As Single)
                _xMax = value
            End Set
        End Property

        Private _autoXMax As Boolean = True 'If True, the XMax value will be chosen automatically
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

        Private _xGridInterval As Single = 0 'The X grid interval used to plot the Random Variable distribution. If 0, the interval will be selected automatically
        Property XGridInterval As Single
            Get
                Return _xGridInterval
            End Get
            Set(value As Single)
                _xGridInterval = value
            End Set
        End Property

        Private _left As Integer = 0 'The left position of the Plot window
        Property Left As Integer
            Get
                Return _left
            End Get
            Set(value As Integer)
                _left = value
            End Set
        End Property

        Private _top As Integer = 0 'The top of the Plot window
        Property Top As Integer
            Get
                Return _top
            End Get
            Set(value As Integer)
                _top = value
            End Set
        End Property

        Private _width As Integer = 540 'The width of the Plot window
        Property Width As Integer
            Get
                Return _width
            End Get
            Set(value As Integer)
                _width = value
            End Set
        End Property

        Private _height As Integer = 540 'The height of the Plot window
        Property Height As Integer
            Get
                Return _height
            End Get
            Set(value As Integer)
                _height = value
            End Set
        End Property

        'Table Display Settings: ------------------------------------------------------------------------------------------------------------

        Private _columnNo As Integer = -1 'The column position of the data in the table. The first column is 0. ColumnNo of -1 indicates no value provided.
        Property ColumnNo As Integer
            Get
                Return _columnNo
            End Get
            Set(value As Integer)
                _columnNo = value
            End Set
        End Property

        Private _format As String = "" 'A format string used to display the data value in a datagridview.
        Property Format As String
            Get
                Return _format
            End Get
            Set(value As String)
                _format = value
            End Set
        End Property

        Private _alignment As String
        Property Alignment As String
            Get
                Return _alignment
            End Get
            Set(value As String)
                _alignment = value
            End Set
        End Property



    End Class 'RVInfo



    Class CorrInfo
        'Correlation information

        Public UnCorrRV(0 To 1) As String 'The names of the uncorrelated random variable values
        Public CorrRV(0 To 1) As String 'The names of the correlated random variable values
        'The uncorrelated random variable values will be overwritten if the Correlated names are the same as the uncorrelated names.
        Public Array(0 To 1, 0 To 1) As Double 'Array to hold the correlation coefficients.

        'The Name of the Correlation is used as the Correlations dictionary Key (so is not included in CorrInfo)

        Private _description As String 'A description of the correlation.
        Property Description As String
            Get
                Return _description
            End Get
            Set(value As String)
                _description = value
            End Set
        End Property

        Private _nVariables As Integer = 2 'The number of correlated random variables.
        Property NVariables As Integer
            Get
                Return _nVariables
            End Get
            Set(value As Integer)
                _nVariables = value
                ReDim Array(0 To _nVariables - 1, 0 To _nVariables - 1)
                ReDim UnCorrRV(0 To _nVariables - 1)
                ReDim CorrRV(0 To _nVariables - 1)
                Dim I As Integer
                For I = 0 To _nVariables - 1
                    Array(I, I) = 1
                Next
            End Set
        End Property

        Private _tableName As String = "" 'The name of the table containing the Random Variables.
        Property TableName As String
            Get
                Return _tableName
            End Get
            Set(value As String)
                _tableName = value
            End Set
        End Property

        Private _displayFormat As String = "" 'The format string used to display the Correlation Coeffifients.
        Property DisplayFormat As String
            Get
                Return _displayFormat
            End Get
            Set(value As String)
                _displayFormat = value
            End Set
        End Property

        Private _cholDisplayFormat As String = "" 'The format string used to display the Cholesky decomposition of the Correlation Coefficients.
        Property CholDisplayFormat As String
            Get
                Return _cholDisplayFormat
            End Get
            Set(value As String)
                _cholDisplayFormat = value
            End Set
        End Property

    End Class 'CorrInfo

End Class
