using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerCSharp
{
    internal class FileManager
    {
        private string _currentDir = Directory.GetCurrentDirectory();
        private List<string> _history = new List<string>();

        public string CurrentDir
        {
            get
            {
                return _currentDir;
            }
        }

        public void ShowDir()
        {
            Console.WriteLine();
            Console.WriteLine("Directory of " + _currentDir);
            Console.WriteLine();

            foreach (string d in Directory.GetDirectories(_currentDir))
            {
                Console.WriteLine("<DIR>    " + Path.GetFileName(d));
            }

            foreach (string f in Directory.GetFiles(_currentDir))
            {
                FileInfo file = new FileInfo(f);
                Console.WriteLine("<FILE>   " + file.Name + "   " + file.Length + " bytes");
            }

            Console.WriteLine();
        }

        public void ChangeDir(string path)
        {
            if (path == "..")
            {
                DirectoryInfo parent = Directory.GetParent(_currentDir);
                if (parent != null)
                    _currentDir = parent.FullName;
                else
                    Console.WriteLine("Already at root directory, you cant go higher.");
                return;
            }

            string fullPath = Path.Combine(_currentDir, path);

            if (Directory.Exists(fullPath))
                _currentDir = fullPath;
            else
                Console.WriteLine("Directory not found --> " + path);
        }

        public void CreateDir(string name)
        {
            string path = Path.Combine(_currentDir, name);
            Directory.CreateDirectory(path);
            Console.WriteLine("Directory created --> " + name);
        }

        public void DeleteDir(string name)
        {
            string path = Path.Combine(_currentDir, name);
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Directory not found --> " + name);
                return;
            }
            Directory.Delete(path, true);
            Console.WriteLine("Directory deleted --> " + name);
        }

        public void CreateFile(string name, string text)
        {
            string path = Path.Combine(_currentDir, name);
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.WriteLine(text);
            }
            Console.WriteLine("File created --> " + name);
        }

        public void ReadFile(string name)
        {
            string path = Path.Combine(_currentDir, name);
            if (!File.Exists(path))
            {
                Console.WriteLine("File not found --> " + name);
                return;
            }
            Console.WriteLine();
            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                Console.WriteLine(sr.ReadToEnd());
            }
        }

        public void DeleteFile(string name)
        {
            string path = Path.Combine(_currentDir, name);
            if (!File.Exists(path))
            {
                Console.WriteLine("File not found --> " + name);
                return;
            }
            File.Delete(path);
            Console.WriteLine("File deleted --> " + name);
        }

        public void Copy(string source, string dest)
        {
            string fullSource = Path.Combine(_currentDir, source);
            string fullDest = Path.Combine(_currentDir, dest);

            if (File.Exists(fullSource))
            {
                File.Copy(fullSource, fullDest, true);
                Console.WriteLine("File copied --> " + source + " to " + dest);
            }
            else if (Directory.Exists(fullSource))
            {
                CopyDir(fullSource, fullDest);
                Console.WriteLine("Directory copied --> " + source + " to " + dest);
            }
            else
            {
                Console.WriteLine("Not found --> " + source);
            }
        }

        private void CopyDir(string source, string dest)
        {
            Directory.CreateDirectory(dest);

            foreach (FileInfo file in new DirectoryInfo(source).GetFiles())
            {
                file.CopyTo(Path.Combine(dest, file.Name), true);
            }

            foreach (DirectoryInfo subDir in new DirectoryInfo(source).GetDirectories())
            {
                CopyDir(subDir.FullName, Path.Combine(dest, subDir.Name));
            }
        }

        public void Move(string source, string dest)
        {
            string fullSource = Path.Combine(_currentDir, source);
            string fullDest = Path.Combine(_currentDir, dest);

            if (File.Exists(fullSource))
            {
                File.Move(fullSource, fullDest);
                Console.WriteLine("File moved --> " + source + " to " + dest);
            }
            else if (Directory.Exists(fullSource))
            {
                CopyDir(fullSource, fullDest);
                Directory.Delete(fullSource, true);
                Console.WriteLine("Directory moved --> " + source + " to " + dest);
            }
            else
            {
                Console.WriteLine("Not found --> " + source);
            }
        }

        public void ShowAttr(string name)
        {
            string path = Path.Combine(_currentDir, name);
            if (!File.Exists(path))
            {
                Console.WriteLine("File not found --> " + name);
                return;
            }
            FileInfo info = new FileInfo(path);
            Console.WriteLine();
            Console.WriteLine("Name       --> " + info.Name);
            Console.WriteLine("Size       --> " + info.Length + " bytes");
            Console.WriteLine("Created    --> " + info.CreationTime);
            Console.WriteLine("Modified   --> " + info.LastWriteTime);
            Console.WriteLine("Attributes --> " + info.Attributes);
            Console.WriteLine();
        }

        public void Search(string name)
        {
            Console.WriteLine();
            Console.WriteLine("Searching --> " + name);
            Console.WriteLine();
            int found = 0;
            SearchInDir(new DirectoryInfo(_currentDir), name, ref found);
            Console.WriteLine("Found " + found + " result(s)");
            Console.WriteLine();
        }

        private void SearchInDir(DirectoryInfo dir, string name, ref int found)
        {
            try
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    if (file.Name.Contains(name))
                    {
                        Console.WriteLine(file.FullName);
                        found++;
                    }
                }

                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    if (subDir.Name.Contains(name))
                    {
                        Console.WriteLine(subDir.FullName);
                        found++;
                    }
                    SearchInDir(subDir, name, ref found);
                }
            }
            catch
            {
                Console.WriteLine("Access denied --> " + dir.FullName);
            }
        }

        public void AddHistory(string command)
        {
            _history.Add(command);
        }

        public void ShowHistory()
        {
            if (_history.Count == 0)
            {
                Console.WriteLine("History is empty.");
                return;
            }
            Console.WriteLine();
            for (int i = 0; i < _history.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + _history[i]);
            }
            Console.WriteLine();
        }

        public void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Available commands:");
            Console.WriteLine("-------------------");
            Console.WriteLine("dir      - show files and folders");
            Console.WriteLine("cd       - change directory");
            Console.WriteLine("mkdir    - create directory");
            Console.WriteLine("rmdir    - delete directory");
            Console.WriteLine("create   - create text file");
            Console.WriteLine("read     - read text file");
            Console.WriteLine("del      - delete file");
            Console.WriteLine("copy     - copy file or folder");
            Console.WriteLine("move     - move file or folder");
            Console.WriteLine("attr     - show file attributes");
            Console.WriteLine("search   - search files");
            Console.WriteLine("history  - show command history");
            Console.WriteLine("cls      - clear screen");
            Console.WriteLine("help     - show this help");
            Console.WriteLine("exit     - exit program");
            Console.WriteLine();
        }

        public void ShowHelpDetail(string command)
        {
            Console.WriteLine();
            switch (command.ToLower())
            {
                case "dir":
                    Console.WriteLine("dir - shows all files and folders in current directory");
                    break;
                case "cd":
                    Console.WriteLine("cd - change directory");
                    Console.WriteLine("type .. to go back to base folder");
                    break;
                case "mkdir":
                    Console.WriteLine("mkdir - creates new folder in current directory");
                    break;
                case "rmdir":
                    Console.WriteLine("rmdir - deletes folder with all files");
                    break;
                case "create":
                    Console.WriteLine("create - creates a new text file with text");
                    break;
                case "read":
                    Console.WriteLine("read - shows text of a text file");
                    break;
                case "del":
                    Console.WriteLine("del - deletes a file");
                    break;
                case "copy":
                    Console.WriteLine("copy - copies file or folder to new place");
                    break;
                case "move":
                    Console.WriteLine("move - moves file or folder to new place");
                    break;
                case "attr":
                    Console.WriteLine("attr - shows name, size, dates and attributes of file");
                    break;
                case "search":
                    Console.WriteLine("search - searches files by name in current folder and subfolders");
                    break;
                case "history":
                    Console.WriteLine("history - shows list of all entered commands");
                    break;
                case "cls":
                    Console.WriteLine("cls - clears the screen");
                    break;
                default:
                    Console.WriteLine("No help for --> " + command);
                    break;
            }
            Console.WriteLine();
        }
    }
}