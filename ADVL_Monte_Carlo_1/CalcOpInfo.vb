Public Class CalcOpInfo
    'Information about a Calculation Operation item.

    'The Item Name is used as the Key in the CalcInfo dictionary. It is not repeated in the CalcOpInfo class.

    Public CopyList As New List(Of String) 'A list of nodes that are a copy of this node

    Private _text As String = "" 'The node text. This is also the used as the ScalarData key. The Text is the same as the Name unless the node is a value Copy , where the Text is the data key for ScalarData of the copied data.
    Property Text As String
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
        End Set
    End Property

    Private _units As String = "" 'The units of the node value (eg metres, seconds)

    Property Units As String
        Get
            Return _units
        End Get
        Set(value As String)
            _units = value
        End Set
    End Property

    Private _unitsAbbrev As String = "" 'An abbreviation of the units (eg m, s)
    Property UnitsAbbrev As String
        Get
            Return _unitsAbbrev
        End Get
        Set(value As String)
            _unitsAbbrev = value
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

    Private _type As String = "" 'The type of calculation node (Calculation Sequence, Input Variable, Input Variable User Defined, Output Value, Process, Value Process, Collection, Value Copy, Constant Value E, Constant Value T, Constant Value Pi, Constant Value User Defined, 
    'Add, Subtract, Multiply, Divide, Sum, Product, Sine, Cosine, Tangent, ArcSine, ArcCosine, ArcTangent, Degrees To Radians, Radians To Degrees,
    'Power Of E, Natural Logarithm, Power Of Ten, Logarithm, Square, Square Root, Cube, Cube Root, Exponentiate, Root, Negate, Invert, Absolute, Round, Round Up, Round Down.)
    Property Type As String
        Get
            Return _type
        End Get
        Set(value As String)
            _type = value
        End Set
    End Property

    Private _status As String = "New" 'The status of the Calculation Operation.
    Property Status As String
        Get
            Return _status
        End Get
        Set(value As String)
            _status = value
        End Set
    End Property

    Private _statusCode As Integer = 0 'The status code of the Calculation Operation
    Property StatusCode As Integer
        Get
            Return _statusCode
        End Get
        Set(value As Integer)
            _statusCode = value
        End Set
    End Property
End Class
