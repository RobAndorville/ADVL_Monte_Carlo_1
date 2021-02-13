Public Class InputInfo
    'Contains information about a Calculation Tree Input Variable

    Private _name As String = "" 'The name of the Input Variable
    Property Name
        Get
            Return _name
        End Get
        Set(value)
            _name = value
        End Set
    End Property

    Private _columnName As String = "" 'The name of the Column in the Calculations table that contains the Input Variable values
    Property ColumnName As String
        Get
            Return _columnName
        End Get
        Set(value As String)
            _columnName = value
        End Set
    End Property
End Class
