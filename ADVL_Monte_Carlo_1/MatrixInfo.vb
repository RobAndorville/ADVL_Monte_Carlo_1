Public Class MatrixInfo
    'Stores data and information for a matrix.

#Region " Variable Declarations - All the variables and class objects used in this form and this application." '===============================================================================

    Public Data(0 To 1, 0 To 1) As Double 'Array used to store the matrix data.

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties" '========================================================================================================================================================================

    Private _fileName As String = "" 'The file name used to store the matrix.
    Property FileName As String
        Get
            Return _fileName
        End Get
        Set(value As String)
            _fileName = value
        End Set
    End Property

    Private _name As String = "" 'The name of the matrix.
    Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
            DataChanged = True
        End Set
    End Property

    Private _description As String = "" 'A description of the matrix.
    Property Description As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
            DataChanged = True
        End Set
    End Property

    Private _format As String = "" 'The format code used to display the matrix values.
    Property Format As String
        Get
            Return _format
        End Get
        Set(value As String)
            _format = value
        End Set
    End Property

    Private _nRows As Integer = 2 'The number of rows in the matrix.
    Property NRows As Integer
        Get
            Return _nRows
        End Get
        Set(value As Integer)
            _nRows = value
            ReDim Data(0 To NRows - 1, 0 To NCols - 1)
            DataChanged = True
        End Set
    End Property

    Private _nCols As Integer = 2 'The number of columns in the matrix.
    Property NCols As Integer
        Get
            Return _nCols
        End Get
        Set(value As Integer)
            _nCols = value
            ReDim Data(0 To NRows - 1, 0 To NCols - 1)
            DataChanged = True
        End Set
    End Property

    Private _dataChanged As Boolean = False 'True if the data has changed since the last save.
    Property DataChanged As Boolean
        Get
            Return _dataChanged
        End Get
        Set(value As Boolean)
            _dataChanged = value
        End Set
    End Property

#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Methods" '===========================================================================================================================================================================

    Public Function MatrixToXDoc() As System.Xml.Linq.XDocument
        'Return an XDocument contining the Matrix.

        Dim XDoc = <?xml version="1.0" encoding="utf-8"?>
                   <Matrix>
                       <FileName><%= FileName %></FileName>
                       <Name><%= Name %></Name>
                       <Description><%= Description %></Description>
                       <Format><%= Format %></Format>
                       <NRows><%= NRows %></NRows>
                       <NCols><%= NCols %></NCols>
                       <Data>
                           <%= From Val In Data
                               Select
                               <Val><%= Val %></Val> %>
                       </Data>
                   </Matrix>
        Return XDoc
    End Function

    Public Sub XDocToMatrix(ByRef XDoc As System.Xml.Linq.XDocument)
        'Read the Matrix information from the XDocument.

        If XDoc Is Nothing Then
            RaiseEvent ErrorMessage("The Matrix XDocument is empty." & vbCrLf)
        Else
            Name = XDoc.<Matrix>.<Name>.Value
            Description = XDoc.<Matrix>.<Description>.Value
            If XDoc.<Matrix>.<Format>.Value <> Nothing Then Format = XDoc.<Matrix>.<Format>.Value
            NRows = XDoc.<Matrix>.<NRows>.Value
            NCols = XDoc.<Matrix>.<NCols>.Value
            Dim MatrixValues = From valueItem In XDoc.<Matrix>.<Data>.<Val>
            Dim I As Integer = 0
            For Each valueItem In MatrixValues
                Data(Int(I / NCols), I Mod NCols) = valueItem
                I += 1
            Next
            DataChanged = False
        End If
    End Sub

    Public Sub Clear()
        'Clear the Matrix data.

        FileName = ""
        Name = ""
        Description = ""
        NRows = 2
        NCols = 2
        ReDim Data(0 To 1, 0 To 1) 'Clear the Data.
    End Sub

#End Region 'Methods --------------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Events - Events raised by this class." '=============================================================================================================================================
    Event ErrorMessage(ByVal Msg As String) 'Send an error message.
    Event Message(ByVal Msg As String) 'Send a normal message.
#End Region 'Events ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class
