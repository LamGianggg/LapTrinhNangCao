Public Class Form1
    Private Sub txtB_TextChanged(sender As Object, e As EventArgs) Handles txtB.TextChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnMultiply.Click

        Dim a As Double = Double.Parse(txtA.Text)
        Dim b As Double = Double.Parse(txtB.Text)

        Dim result As Double = a * b

        txtResult.Text = result.ToString()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnDivide.Click

        Dim a As Double = Double.Parse(txtA.Text)
        Dim b As Double = Double.Parse(txtB.Text)

        If b = 0 Then
            MessageBox.Show("Không thể chia cho 0!")
            Return
        End If

        Dim result As Double = a / b

        txtResult.Text = result.ToString()

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Dim a As Double = Double.Parse(txtA.Text)
        Dim b As Double = Double.Parse(txtB.Text)

        Dim result As Double = a + b

        txtResult.Text = result.ToString()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSubtract.Click

        Dim a As Double = Double.Parse(txtA.Text)
        Dim b As Double = Double.Parse(txtB.Text)

        Dim result As Double = a - b

        txtResult.Text = result.ToString()

    End Sub
End Class
