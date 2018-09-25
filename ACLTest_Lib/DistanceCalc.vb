Public Class DistanceCalc
    Public Enum DistanceType
        StatuteMiles
        NauticalMiles
        Kilometers
    End Enum

    Public Function Calculate(ByVal fromLatitude As String, ByVal FromLongitude As String, ByVal toLatitude As String, ByVal toLongitude As String, ByVal iDistanceType As DistanceType, ByVal isAcuqwik As Boolean) As Decimal
        Dim vVal As Decimal, dFromLat As Decimal, dFromLong As Decimal, dToLat As Decimal, dToLong As Decimal
        Dim r As Decimal

        If (isAcuqwik) Then
            dFromLat = CalculateDecimalsAQ(fromLatitude) * (Math.PI / 180)
            dFromLong = CalculateDecimalsAQ(FromLongitude) * (Math.PI / 180)
            dToLat = CalculateDecimalsAQ(toLatitude) * (Math.PI / 180)
            dToLong = CalculateDecimalsAQ(toLongitude) * (Math.PI / 180)
        Else
            dFromLat = CalculateDecimalsUV(fromLatitude) * (Math.PI / 180)
            dFromLong = CalculateDecimalsUV(FromLongitude) * (Math.PI / 180)
            dToLat = CalculateDecimalsUV(toLatitude) * (Math.PI / 180)
            dToLong = CalculateDecimalsUV(toLongitude) * (Math.PI / 180)
        End If


        'radius of the earth:
        Select Case iDistanceType
            Case DistanceType.StatuteMiles  ' statute miles
                r = 3956.4895272
            Case DistanceType.NauticalMiles  ' nautical miles
                r = 3438.034
            Case DistanceType.Kilometers  ' kilometers
                r = 6367.238968
        End Select

        Return Convert.ToDecimal(Math.Acos(Math.Cos(dFromLat) * Math.Cos(dFromLong) * Math.Cos(dToLat) * Math.Cos(dToLong) + Math.Cos(dFromLat) * Math.Sin(dFromLong) * Math.Cos(dToLat) * Math.Sin(dToLong) + Math.Sin(dFromLat) * Math.Sin(dToLat)) * r)
    End Function

    Shared Function CalculateDecimalsAQ(ByVal sFormatted As String) As Decimal
        Dim vVal As Decimal, b As Integer, a As Integer
        a = InStrRev(sFormatted, "-")
        If a > 0 And IsNumeric(Mid(sFormatted, a + 1)) Then
            vVal = Mid(sFormatted, a + 1) / 60
            vVal = vVal + Mid(sFormatted, 2, a - 2)
            Select Case UCase(Left(sFormatted, 1))
                Case "W", "S"
                    vVal = vVal * -1
            End Select
            Return vVal
        End If
        Return vVal
    End Function

    Shared Function CalculateDecimalsUV(ByVal sFormatted As String) As Decimal
        Dim vVal As Decimal, b As Integer, a As Integer
        a = InStrRev(sFormatted, "-")
        If a > 0 Then
            vVal = Val(Mid$(sFormatted, a + 1)) / 60
            b = InStrRev(sFormatted, "-", a - 1)
            If b > 0 Then
                vVal += Val(Mid$(sFormatted, b + 1, a - b))
                vVal /= 60
                vVal += Val(Left$(sFormatted, b - 1))
                Select Case UCase$(Right$(sFormatted, 1))
                    Case "W", "S"
                        vVal *= -1
                End Select
                Return vVal
            End If
        End If
        Return vVal
    End Function
End Class
