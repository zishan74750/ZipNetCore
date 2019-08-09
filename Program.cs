using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Zip_Net_Core {

    public static class FileExtensions
{
    public static IEnumerable<FileSystemInfo> AllFilesAndFolders(this DirectoryInfo dir)
    {
        foreach (var f in dir.GetFiles())
            yield return f;
        foreach (var d in dir.GetDirectories())
        {
            yield return d;
            foreach (var o in AllFilesAndFolders(d))
                yield return o;
        }
    }
}
    class Program {
        static void Main (string[] args) {
            // Console.WriteLine("Hello World!");
            string _downloadPath = string.Format ( Environment.CurrentDirectory+ "{0}ZippedFiles", Path.DirectorySeparatorChar);
            DirectoryInfo from = new DirectoryInfo (@"C:\Test");
            // using (FileStream zipToOpen = new FileStream (@"Test.zip", FileMode.Create)) {
               if(!File.Exists(_downloadPath+Path.DirectorySeparatorChar+"Test.zip"))
               {
                  Directory.CreateDirectory( Path.GetDirectoryName( _downloadPath+Path.DirectorySeparatorChar+"Test.zip"));
                //   File.Create(_downloadPath+Path.DirectorySeparatorChar+"Test.zip",);
               }

                 using (FileStream zipToOpen = new FileStream (_downloadPath+Path.DirectorySeparatorChar+"Test.zip", FileMode.Create)) {
                using (ZipArchive archive = new ZipArchive (zipToOpen, ZipArchiveMode.Create)) {
                    foreach (FileInfo file in from.AllFilesAndFolders ().Where (o => o is FileInfo).Cast<FileInfo> ()) {
                        var relPath = file.FullName.Substring (from.FullName.Length + 1);
                        ZipArchiveEntry readmeEntry = archive.CreateEntryFromFile (file.FullName, relPath);
                    }
                }
            }

            // using (ZipFile zip = new ZipFile ()) {
            //     zip.UseUnicodeAsNecessary = true; // utf-8
            //     zip.AddDirectory (@"MyDocuments\ProjectX");
            //     zip.Comment = "This zip was created at " + System.DateTime.Now.ToString ("G");
            //     zip.Save (pathToSaveZipFile);
            // }

            // using (ZipArchive zipArchive = ZipFile.Open (_downloadPath, ZipArchiveMode.Create)) {
            //     // foreach (var thumbnail in thumbnails) {
            //         ZipArchiveEntry zipfileInternal = zipArchive.CreateEntry (thumbnail.Value);
            //         byte[] buffer = File.ReadAllBytes (thumbnail.Key);
            //         using (MemoryStream originalFileMemoryStream = new MemoryStream (buffer))
            //         using (Stream fileStream = zipfileInternal.Open ())
            //         originalFileMemoryStream.CopyTo (fileStream);
            //         File.Delete (thumbnail.Key);
            //     }
            // }

        }
    }
}