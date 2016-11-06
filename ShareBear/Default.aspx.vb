Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text


Public Class _Default
    Inherits System.Web.UI.Page

    Private clsGlobals As New Globals
    Private GeneratedImageName As String = clsGlobals.GetFromConfig("GeneratedImageName")
    Private BearImageFolderPath As String = clsGlobals.GetFromConfig("BearImageFolderPath")
    Private GeneratedImageExtensions As String = clsGlobals.GetFromConfig("GeneratedImageExtensions")
    Private GeneratedImageFolderPath As String = clsGlobals.GetFromConfig("GeneratedImageFolderPath")
    Private FontName As String = clsGlobals.GetFromConfig("FontName")
    Private FontSize As Integer = CInt(clsGlobals.GetFromConfig("FontSize"))
    Private rectWidth As Integer = CInt(clsGlobals.GetFromConfig("NameRectangleWidth"))
    Private rectHeight As Integer = CInt(clsGlobals.GetFromConfig("NameRectangleHeight"))
    Private IsFontBold As Boolean = CBool(CInt(clsGlobals.GetFromConfig("IsFontBold")))
    Private GeneratedNameYPosition As Integer = CInt(clsGlobals.GetFromConfig("GeneratedNameYPosition"))
    Private GeneratedNameXPosition As Integer = CInt(clsGlobals.GetFromConfig("GeneratedNameXPosition"))
    Private BearHeartImageName As String = clsGlobals.GetFromConfig("BearHeartImageName")
    Private BearHeartXPosition As Integer = CInt(clsGlobals.GetFromConfig("BearHeartXPosition"))
    Private BearHeartYPosition As Integer = CInt(clsGlobals.GetFromConfig("BearHeartYPosition"))
    Private BearHeartWidth As Integer = CInt(clsGlobals.GetFromConfig("BearHeartWidth"))
    Private BearHeartHeight As Integer = CInt(clsGlobals.GetFromConfig("BearHeartHeight"))

    Protected Sub btnGenerateBear_Click(sender As Object, e As System.EventArgs) Handles btnGenerateBear.Click
        Try
            Dim TextColor As Color
            Dim Rand As New Random()
            Dim stringFormat As New StringFormat()
            Dim Name As String = txtName.Text.Replace(" ", vbCrLf)
            Dim RandomNumber As String = String.Concat(clsGlobals.FormatDateTime(Now), Rand.Next(1, 1000))

            'Load the images to be written on.
            Dim bitMapImage As Bitmap = New System.Drawing.Bitmap(Server.MapPath(String.Concat(BearImageFolderPath, "/", ddlBears.SelectedValue)))
            Dim graphicImage As Graphics = Graphics.FromImage(bitMapImage)
            Dim heartImage As Image = Image.FromFile(Server.MapPath(String.Concat(BearImageFolderPath, "/", BearHeartImageName)))
            Dim heartGraphic As Graphics = Graphics.FromImage(heartImage)

            BearHeartYPosition = bitMapImage.Height - BearHeartHeight - BearHeartYPosition
            BearHeartXPosition = bitMapImage.Width - BearHeartWidth - BearHeartXPosition
            graphicImage.DrawImage(heartImage.Clone, BearHeartXPosition, BearHeartYPosition, BearHeartWidth, BearHeartHeight)

            'Write your text.
            Dim fontFamily As FontFamily = New FontFamily(FontName)
            Dim NameStyle As FontStyle = IIf(IsFontBold, FontStyle.Bold, FontStyle.Regular)
            Dim charsize As SizeF = graphicImage.MeasureString(Name, New Font(fontFamily, FontSize, NameStyle))
            Dim rectX As Double = bitMapImage.Width - rectWidth - GeneratedNameXPosition

            TextColor = ColorTranslator.FromHtml(ddlBears.SelectedItem.Attributes("data-Color"))
            stringFormat.Alignment = StringAlignment.Center
            stringFormat.LineAlignment = StringAlignment.Center
            Dim rect1 As Rectangle = New Rectangle(rectX, GeneratedNameYPosition, rectWidth, rectHeight)
            graphicImage.DrawString(Name, New Font(fontFamily, FontSize, NameStyle), New SolidBrush(TextColor), rect1, stringFormat)

            'Smooth graphics is nice.
            graphicImage.SmoothingMode = SmoothingMode.AntiAlias
            graphicImage.InterpolationMode = InterpolationMode.High

            'Drawing an oval around text.
            'graphicImage.DrawArc(New Pen(Color.Red, 3), 90, 235, 150, 50, 0, 360)

            Dim ImageUrl As String = String.Concat(Server.MapPath(""), "\", GeneratedImageFolderPath, "\", GeneratedImageName, RandomNumber, ".", GeneratedImageExtensions)
            bitMapImage.Save(ImageUrl)
            img.ImageUrl = String.Concat(GeneratedImageFolderPath, "/", GeneratedImageName, RandomNumber, ".", GeneratedImageExtensions)
            img.Visible = True

            graphicImage.Dispose()
            bitMapImage.Dispose()

            hfGeneratedImagePath.Value = String.Concat(GeneratedImageFolderPath, "/", GeneratedImageName, RandomNumber, ".", GeneratedImageExtensions)
            btnDownloadGeneratedImage.Visible = True
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnDownloadGeneratedImage_Click(sender As Object, e As EventArgs) Handles btnDownloadGeneratedImage.Click
        Response.ContentType = "image/jpeg"
        Response.AppendHeader("Content-Disposition", "attachment; filename=bear.jpg")
        Dim GeneratedImagePath As String = hfGeneratedImagePath.Value
        Dim FileName As String = GeneratedImagePath.Substring(GeneratedImagePath.LastIndexOf("/") + 1)
        Response.TransmitFile(Server.MapPath(String.Concat(GeneratedImageFolderPath, "/", FileName)))
        Response.End()
    End Sub
End Class