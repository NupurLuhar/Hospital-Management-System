Imports System.Data.SqlClient

Public Class BillingForm
    Dim connString As String = "Data Source=.;Initial Catalog=HospitalDB;Integrated Security=True"
    Dim conn As New SqlConnection(connString)

    Private Sub BtnGenerateInvoice_Click(sender As Object, e As EventArgs) Handles BtnGenerateInvoice.Click
        Dim query As String = "INSERT INTO Billing (PatientID, TotalAmount, BillingDate) VALUES (@PatientID, @TotalAmount, GETDATE())"
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@PatientID", TxtPatientID.Text)
        cmd.Parameters.AddWithValue("@TotalAmount", TxtTotalAmount.Text)

        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()

        MessageBox.Show("Invoice generated successfully!")
    End Sub
End Class
