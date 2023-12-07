Imports System.IO

Module Program

    Public CopyDest As String = Nothing
    Sub Main(args As String())

        Dim FI_Src As FileInfo
        Dim FI_Dst As FileInfo
        Dim FSW As FileSystemWatcher

        Dim sName As String
        Dim FullOutputPath As String

        Try

#If CONFIG = "Debug" Then
            ReDim args(1)
            args(0) = "C:\Temp\Replay obsReplay.txt"
            args(1) = "C:\Temp"
#End If

            If args.Length < 2 Then
                Console.WriteLine("MonitorFile, Simple FileSystemWatcher example")
                Console.WriteLine("")
                Console.WriteLine("Example:")
                Console.WriteLine("")
                Console.WriteLine("     C:\> MonitorFile " &
                                  """" & "Replay obsReplay.mp4" & """" &
                                  "C:\ReplayArchive")
                End
            End If

            FI_Src = New FileInfo(args(0))
            sName = FI_Src.Name.Split(".")(0) ' Name of file sans extension
            Console.WriteLine("Monitoring: " & FI_Src.FullName)

            FI_Dst = New FileInfo(args(1))
            CopyDest = FI_Dst.FullName

            Do
                FSW = New FileSystemWatcher(FI_Src.DirectoryName, FI_Src.Name)
                FSW.NotifyFilter = IO.NotifyFilters.LastWrite
                FSW.WaitForChanged(WatcherChangeTypes.Changed)

                FI_Src.Refresh()
                FullOutputPath =
                    String.Join("\", {CopyDest, sName}) & "_" &
                    FI_Src.LastWriteTime.Year.ToString &
                    FI_Src.LastWriteTime.Month.ToString("D2") &
                    FI_Src.LastWriteTime.Day.ToString("D2") &
                    FI_Src.LastWriteTime.Hour.ToString("D2") &
                    FI_Src.LastWriteTime.Minute.ToString("D2") &
                    FI_Src.LastWriteTime.Second.ToString("D2") &
                    FI_Src.Extension

                FI_Src.CopyTo(FullOutputPath, True)
                Console.WriteLine("Copied: " & FullOutputPath)

            Loop

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Exit Sub
        End Try

    End Sub

End Module
