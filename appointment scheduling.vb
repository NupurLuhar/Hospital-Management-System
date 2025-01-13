Imports System.Data.SqlClient

Public Class AppointmentForm
    Dim connString As String = "Data Source=.;Initial Catalog=HospitalDB;Integrated Security=True"
    Dim conn As New SqlConnection(connString)

    Private Sub AppointmentForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPatients()
        LoadDoctors()
    End Sub

    Private Sub LoadPatients()
        Dim query As String = "SELECT PatientID, FirstName + ' ' + LastName AS FullName FROM Patients"
        Dim adapter As New SqlDataAdapter(query, conn)
        Dim table As New DataTable()
        adapter.Fill(table)
        CmbPatient.DataSource = table
        CmbPatient.DisplayMember = "FullName"
        CmbPatient.ValueMember = "PatientID"
    End Sub

    Private Sub LoadDoctors()
        Dim query As String = "SELECT DoctorID, Name FROM Doctors"
        Dim adapter As New SqlDataAdapter(query, conn)
        Dim table As New DataTable()
        adapter.Fill(table)
        CmbDoctor.DataSource = table
        CmbDoctor.DisplayMember = "Name"
        CmbDoctor.ValueMember = "DoctorID"
    End Sub

    Private Sub BtnSchedule_Click(sender As Object, e As EventArgs) Handles BtnSchedule.Click
        Dim query As String = "INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, Status) VALUES (@PatientID, @DoctorID, @AppointmentDate, 'Scheduled')"
        Dim cmd As New SqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@PatientID", CmbPatient.SelectedValue)
        cmd.Parameters.AddWithValue("@DoctorID", CmbDoctor.SelectedValue)
        cmd.Parameters.AddWithValue("@AppointmentDate", DtpAppointment.Value)

        conn.Open()
        cmd.ExecuteNonQuery()
        conn.Close()

        MessageBox.Show("Appointment scheduled successfully!")
    End Sub
End Class
