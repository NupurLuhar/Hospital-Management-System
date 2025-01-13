Imports System.Data.SqlClient

Public Class PatientForm
    Dim connString As String = "Data Source=.;Initial Catalog=HospitalDB;Integrated Security=True"
    Dim conn As New SqlConnection(connString)

    Private Sub PatientForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPatients()
    End Sub

    Private Sub LoadPatients()
        Dim query As String = "SELECT * FROM Patients"
        Dim adapter As New SqlDataAdapter(query, conn)
        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView1.DataSource = table
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim query As String = "INSERT INTO Patients (FirstName, LastName, DOB, Gender, Contact, Address) VALUES (@FirstName, @LastName, @DOB, @Gender, @Contact, @Address)"
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@FirstName", TxtFirstName.Text)
        cmd.Parameters.AddWithValue("@LastName", TxtLastName.Text)
        cmd.Parameters.AddWithValue("@DOB", DtpDOB.Value)
        cmd.Parameters.AddWithValue("@Gender", CmbGender.Text)
        cmd.Parameters.AddWithValue("@Contact", TxtContact.Text)
        cmd.Parameters.AddWithValue("@Address", TxtAddress.Text)

        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()

        MessageBox.Show("Patient record added successfully!")
        LoadPatients()
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Dim query As String = "UPDATE Patients SET FirstName=@FirstName, LastName=@LastName, DOB=@DOB, Gender=@Gender, Contact=@Contact, Address=@Address WHERE PatientID=@PatientID"
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@FirstName", TxtFirstName.Text)
        cmd.Parameters.AddWithValue("@LastName", TxtLastName.Text)
        cmd.Parameters.AddWithValue("@DOB", DtpDOB.Value)
        cmd.Parameters.AddWithValue("@Gender", CmbGender.Text)
        cmd.Parameters.AddWithValue("@Contact", TxtContact.Text)
        cmd.Parameters.AddWithValue("@Address", TxtAddress.Text)
        cmd.Parameters.AddWithValue("@PatientID", DataGridView1.SelectedRows(0).Cells("PatientID").Value)

        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()

        MessageBox.Show("Patient record updated successfully!")
        LoadPatients()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Dim query As String = "DELETE FROM Patients WHERE PatientID=@PatientID"
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@PatientID", DataGridView1.SelectedRows(0).Cells("PatientID").Value)

        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()

        MessageBox.Show("Patient record deleted successfully!")
        LoadPatients()
    End Sub
End Class
