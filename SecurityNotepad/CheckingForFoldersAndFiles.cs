using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IOPath = System.IO.Path;


namespace SecurityNotepad
{
    class CheckingForFoldersAndFiles
    {
        public string DocumentsPath { get; set; }
        public static string FolderPath { get; set; }
        public static string FilePath { get; set; }


        public CheckingForFoldersAndFiles()
        {
            DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // получение пути к папке Document
            FolderPath = IOPath.Combine(DocumentsPath, "NotepadAccount");
            FilePath = IOPath.Combine(FolderPath, "Account.json");
        }

        public void CreateFoldersForStrongAccounts()
        {
            if (!CheckingForFileAvaliability())
            {
                Directory.CreateDirectory(FolderPath);
                File.Create(FilePath);
            }
        }
        public bool CheckingForFileAvaliability() => Directory.Exists(IOPath.Combine(DocumentsPath, "NotepadAccount"));

    }
}
