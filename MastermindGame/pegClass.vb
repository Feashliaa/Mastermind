Public Class pegClass
  Public changeColorCount As Integer = 0
  Public pegColorOne As Color
  Public pegColorTwo As Color
  Public pegColorThree As Color
  Public pegColorFour As Color

  Dim listOfNumbers As List(Of Integer) = {1, 2, 3, 4, 5, 6}.ToList

  Private Function RandomColor(ByRef RandomNumber As Integer) As Color
    Select Case RandomNumber
      Case 1
        RandomColor = Color.Blue
      Case 2
        RandomColor = Color.Green
      Case 3
        RandomColor = Color.Orange
      Case 4
        RandomColor = Color.Purple
      Case 5
        RandomColor = Color.Red
      Case 6
        RandomColor = Color.Yellow
    End Select
    Return RandomColor
  End Function

  Public Sub New()
    pegColorOne = GenerateCodeColors()
    pegColorTwo = GenerateCodeColors()
    pegColorThree = GenerateCodeColors()
    pegColorFour = GenerateCodeColors()
  End Sub

  Private Function GenerateCodeColors() As Color
    Dim ColorRandom As Color

    Static Random_Number As New Random()
    Dim randomInt As Integer = Random_Number.Next(1, 6)
    Dim boolFlag As Boolean = False

    While boolFlag = False
      If (listOfNumbers.Contains(randomInt)) Then
        listOfNumbers.Remove(randomInt)
        ColorRandom = RandomColor(randomInt)
        boolFlag = True
      Else
        randomInt = Random_Number.Next(1, 6)
      End If
    End While

    Return ColorRandom

  End Function


  Public Function GetNextColor() As Color

    Dim tempColor As Color

    If (changeColorCount > 5) Then
      changeColorCount = 0
    End If

    changeColorCount = changeColorCount + 1
    tempColor = RandomColor(changeColorCount)
    Return tempColor

  End Function

End Class