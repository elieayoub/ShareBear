Imports System.Drawing
Imports System.Drawing.Text
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Configuration

Public Class Globals

    Public Function FormatDateTime(ByVal dt As DateTime) As String
        Return Format(dt, "yyyyMdd_hhmmss")
    End Function

    Public Function FormatDate(ByVal dt As DateTime) As String
        Return Format(dt, "yyyyMdd")
    End Function

    Public Function GetFromConfig(ByVal Key As String) As String
        Return ConfigurationManager.AppSettings(Key).ToString()
    End Function

    Public Function GetFont(ByVal FontName As String) As PrivateFontCollection
        Dim fonts As New PrivateFontCollection()
        Dim asm As Assembly = Me.[GetType]().Assembly

        Dim stm As Stream = asm.GetManifestResourceStream("ShareBear." & FontName)

        Dim buffer As Byte() = New Byte(stm.Length - 1) {}
        stm.Read(buffer, 0, CInt(stm.Length))
        Dim pi As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Byte)) * buffer.Length)
        Marshal.Copy(buffer, 0, pi, buffer.Length)
        fonts.AddMemoryFont(pi, buffer.Length)
        Marshal.FreeHGlobal(pi)

        Return fonts
    End Function
End Class
