Public Class gameForm
  Public rowOfPegs As New rowOfPegsClass()
  Public GetPegColor As New pegClass()
  Dim rowCount As Integer = 10
  Dim buttonPerRowCount As Integer = 4
  Dim numberOfTurns As Integer
  Dim buttonNumber As Integer = 1
  Dim twoDArrayOfButtons(rowCount - 1, buttonPerRowCount - 1) As Button
  Dim twoDArrayOfLabels(rowCount - 1, buttonPerRowCount - 1) As Label
  Dim cheatRow(3) As Color
  Dim playGame As Boolean = True
  Dim timesButtonPressed As Integer = 0
  Dim guessButtonX As Integer

  Private Sub Fill2dArrays()
    For x = 0 To rowCount - 1
      For y = 0 To buttonPerRowCount - 1
        twoDArrayOfButtons(x, y) = CType(Me.Controls("Button" & buttonNumber), Button)
        twoDArrayOfLabels(x, y) = CType(Me.Controls("Label" & buttonNumber), Label)
        buttonNumber = buttonNumber + 1
      Next
    Next
  End Sub

  Private Sub gameForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    cheatButton1.BackColor = rowOfPegs.pegOne
    cheatButton2.BackColor = rowOfPegs.pegTwo
    cheatButton3.BackColor = rowOfPegs.pegThree
    cheatButton4.BackColor = rowOfPegs.pegFour

    Fill2dArrays()
    For xCount = 0 To rowCount - 1
      For yCount = 0 To buttonPerRowCount - 1
        AddHandler twoDArrayOfButtons(xCount, yCount).Click, AddressOf Buttons_Clicked
      Next
    Next
    numberOfTurns = 10
    guessButtonX = 0
  End Sub

  Private Sub ShowCodeButton_Click(sender As Object, e As EventArgs) Handles ShowCodeButton.Click
    If cheatButton1.Visible = True Then
      HideCode()
    Else
      ShowCode()
    End If
  End Sub

  Private Sub HideCode()
    cheatButton1.Visible = False
    cheatButton2.Visible = False
    cheatButton3.Visible = False
    cheatButton4.Visible = False
  End Sub

  Private Sub ShowCode()
    cheatButton1.Visible = True
    cheatButton2.Visible = True
    cheatButton3.Visible = True
    cheatButton4.Visible = True
  End Sub

  Private Sub guessButton_Click(sender As Object, e As EventArgs) Handles guessButton.Click

    cheatRow(0) = rowOfPegs.pegOne
    cheatRow(1) = rowOfPegs.pegTwo
    cheatRow(2) = rowOfPegs.pegThree
    cheatRow(3) = rowOfPegs.pegFour

    Dim tempColor(3) As Color
    Dim tempButtons(3) As Button
    Dim tempLabels(3) As Label
    Dim checkButton As Boolean = False

    Dim y As Integer = 0

    ' playing the game '
    If (numberOfTurns = 0) Then
      MsgBox("You Lost")
    End If

    'checking which buttons are enabled '
    While (checkButton = False)
      If (twoDArrayOfButtons(guessButtonX, y).Enabled = True) Then
        tempButtons(y) = twoDArrayOfButtons(guessButtonX, y)
        tempLabels(y) = twoDArrayOfLabels(guessButtonX, y)
        twoDArrayOfButtons(guessButtonX, y).Enabled = False
        y += 1
        tempButtons(y) = twoDArrayOfButtons(guessButtonX, y)
        tempLabels(y) = twoDArrayOfLabels(guessButtonX, y)
        twoDArrayOfButtons(guessButtonX, y).Enabled = False
        y += 1
        tempButtons(y) = twoDArrayOfButtons(guessButtonX, y)
        tempLabels(y) = twoDArrayOfLabels(guessButtonX, y)
        twoDArrayOfButtons(guessButtonX, y).Enabled = False
        y += 1
        tempButtons(y) = twoDArrayOfButtons(guessButtonX, y)
        tempLabels(y) = twoDArrayOfLabels(guessButtonX, y)
        twoDArrayOfButtons(guessButtonX, y).Enabled = False
        y = 0
        guessButtonX += 1
        For count = 0 To 3
          twoDArrayOfButtons(guessButtonX, count).Enabled = True
        Next
        checkButton = True
      End If
    End While

    For count = 0 To 3
      tempColor(count) = tempButtons(count).BackColor
    Next

    Dim numberOfPegsInRightSpot As Integer = 0

    If (tempColor(0) = cheatRow(0)) Then
      numberOfPegsInRightSpot += 1
      cheatRow(0) = Color.White
    End If
    If (tempColor(1) = cheatRow(1)) Then
      numberOfPegsInRightSpot += 1
      cheatRow(1) = Color.White
    End If
    If (tempColor(2) = cheatRow(2)) Then
      numberOfPegsInRightSpot += 1
      cheatRow(2) = Color.White
    End If
    If (tempColor(3) = cheatRow(3)) Then
      numberOfPegsInRightSpot += 1
      cheatRow(3) = Color.White
    End If

    Dim numberOfWhiteInRow As Integer = 0
    Dim numberOfPegsInWrongSpot As Integer = 0
    Dim index As Integer = 0

    If (numberOfPegsInRightSpot < 4) Then
      For count = 0 To 3
        If (cheatRow(count) = Color.White) Then
          numberOfWhiteInRow += 1
        Else
          For countTwo = 0 To 3
            Dim currentColor As Color = tempButtons(countTwo).BackColor
            If (cheatRow.Contains(currentColor)) Then
              index = Array.FindIndex(cheatRow, Function(c) (c.Name = currentColor.Name))
              cheatRow(index) = Color.White
              numberOfPegsInWrongSpot += 1
            End If
          Next
        End If
      Next
    End If

    ' you win here '
    If (numberOfPegsInRightSpot = 4) Then
      playGame = False
    End If

    Dim hintIndex As Integer = 0
    While (numberOfPegsInRightSpot > 0)
      tempLabels(hintIndex).BackColor = Color.Black
      numberOfPegsInRightSpot -= 1
      hintIndex += 1
    End While
    While (numberOfPegsInWrongSpot > 0)
      tempLabels(hintIndex).BackColor = Color.Red
      numberOfPegsInWrongSpot -= 1
      hintIndex += 1
    End While
    While (hintIndex <= 3)
      tempLabels(hintIndex).BackColor = Color.White
      hintIndex += 1
    End While

    numberOfTurns = numberOfTurns - 1

    If (playGame = False) Then
      MsgBox("You win!")
      guessButton.Enabled = False

      For xCount = 0 To rowCount - 1
        For yCount = 0 To buttonPerRowCount - 1
          twoDArrayOfButtons(xCount, yCount).Enabled = False
        Next
      Next
    End If

    guessButton.Enabled = False
    timesButtonPressed = 0
  End Sub

  Private Sub Buttons_Clicked(sender As Object, e As EventArgs)

    timesButtonPressed += 1
    Dim B As Button = sender
    B.BackColor = GetPegColor.GetNextColor()
    If (timesButtonPressed >= 4) Then
      guessButton.Enabled = True
    End If

  End Sub

  Private Sub PlayAgainButton_Click(sender As Object, e As EventArgs) Handles PlayAgainButton.Click
    MsgBox("Game Restarting")
    Application.Restart()
  End Sub
End Class
