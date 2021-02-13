Public Class MatrixClipboard
    'Stores Matrix information and contains methods for Matrix copy and paste operations.

    Public WithEvents CBMatrix As New MatrixInfo 'Stores Matrix information within the MatrixClipboard.

    Public Sub Copy(ByRef Matrix As MatrixInfo)
        'Copy the information in Matrix to CBMatrix
        CBMatrix.Clear()
        CBMatrix.FileName = Matrix.FileName
        CBMatrix.Name = Matrix.Name
        CBMatrix.Description = Matrix.Description
        CBMatrix.NRows = Matrix.NRows
        CBMatrix.NCols = Matrix.NCols
        CBMatrix.Data = Matrix.Data
        CBMatrix.DataChanged = False
    End Sub
    Public Sub Paste(ByRef Matrix As MatrixInfo)
        'Paste the inforamtion in CBMatrix to Matrix
        Matrix.Clear()
        Matrix.FileName = CBMatrix.FileName
        Matrix.Name = CBMatrix.Name
        Matrix.Description = CBMatrix.Description
        Matrix.NRows = CBMatrix.NRows
        Matrix.NCols = CBMatrix.NCols
        Matrix.Data = CBMatrix.Data
        Matrix.DataChanged = False
    End Sub

    Public Sub Clear()
        'Clear the MatrixClipboard
        CBMatrix.Clear()
    End Sub

    Private Sub CBMatrix_ErrorMessage(Msg As String) Handles CBMatrix.ErrorMessage
        RaiseEvent ErrorMessage(Msg) 'Pass on the error message from CBMatrix
    End Sub

    Event ErrorMessage(ByVal Msg As String) 'Send an error message.
End Class
