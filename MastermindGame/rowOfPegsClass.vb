Public Class rowOfPegsClass

  Public pegOne As Color
  Public pegTwo As Color
  Public pegThree As Color
  Public pegFour As Color

  Public rowOfPegs(3) As pegClass ' array of pegs '

  ' row of pegs constructor, this is only used for the code '
  Public Sub New()
    Dim count As Integer = 0

    Dim temp As New pegClass()

    pegOne = temp.pegColorOne
    pegTwo = temp.pegColorTwo
    pegThree = temp.pegColorThree
    pegFour = temp.pegColorFour
  End Sub

End Class