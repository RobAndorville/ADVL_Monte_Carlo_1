Public Class OutputInfo
    'Contains information about a Calculation Tree Ouput Value

    Private _name As String = "" 'The name of the Output Value
    Property Name
        Get
            Return _name
        End Get
        Set(value)
            _name = value
        End Set
    End Property

    Private _columnName As String = "" 'The name of the Column in the Calculations table that stores the Output Values
    Property ColumnName As String
        Get
            Return _columnName
        End Get
        Set(value As String)
            _columnName = value
        End Set
    End Property

    Private _node As TreeNode 'The node in the Calculation Tree corresponding to the Output Value
    Property Node As TreeNode
        Get
            Return _node
        End Get
        Set(value As TreeNode)
            _node = value
        End Set
    End Property
End Class
