using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSpaceLeftOnDevice
{
    public class FileSystem
    {
        public FileSystem()
        {
            TopLevelDirectory = new FileSystemEntry(FileType.Directory, "/", 0, null);
            ActiveDirectory = TopLevelDirectory;
        }

        /// <summary>
        /// Change the current directory
        /// </summary>
        /// <param name="directory">Directory to change to</param>
        public void ChangeDirectory(string directory)
        {
            // get the directory 
            if (directory == "/")
                ActiveDirectory = TopLevelDirectory;
            else
            {
                var dir = ActiveDirectory.GetEntry(directory);

                // if it is not null, update the active directory
                if (dir != null)
                    ActiveDirectory = dir;
                else
                    Console.WriteLine($"{directory} is not a valid directory");
            }
        }

        /// <summary>
        /// Adds a file to the current directory
        /// </summary>
        /// <param name="type">File type</param>
        /// <param name="name">File name</param>
        /// <param name="fileSize">File size (use 0 if directory)</param>
        public void AddFile(FileType type, string name, int fileSize)
        {
            // add the file to the current directory
            ActiveDirectory.Add(new FileSystemEntry(type, name, fileSize, ActiveDirectory));
        }

        /// <summary>
        /// Displays the amount of space used by the disk by
        /// adding up all the file sizes within each directory.
        /// Directories exceeding 100000 bytes will be ignored
        /// </summary>
        public void ShowDiskUsage()
        {
            var usage = 0;
            foreach (var i in TopLevelDirectory.FileSystemEntries.Where(x => x.Type == FileType.Directory))
                usage += i.DiskUsage();
            Console.WriteLine($"Disk Usage: {usage}");
        }

        public void ShowDirectoryToDelete()
        {
            const int totalSize = 70000000, spaceNeeded = 30000000;

            // first, determine the current file size
            var size = TopLevelDirectory.FileSize;

            // calculate the true amount of space needed
            var actualNeeded = spaceNeeded - (totalSize - size);
            Console.WriteLine($"Size Needed:\t{actualNeeded}\nSize to Delete:\t{TopLevelDirectory.DirectoryToDelete(actualNeeded)}");
        }

        private readonly FileSystemEntry TopLevelDirectory;
        private FileSystemEntry ActiveDirectory;
    }
}
