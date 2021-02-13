Public Class DataColumnInfo
    'Stores information about and Input/Output data column.

    Private _name As String = "" 'The name of the column.
    Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Private _type As String = "Single" 'The data type of the column.
    Property Type As String
        Get
            Return _type
        End Get
        Set(value As String)
            _type = value
        End Set
    End Property

End Class
