using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSpaceLeftOnDevice
{
    public enum FileType
    {
        Directory = 1,
        File = 2
    }

    public class FileSystemEntry
    {
        /// <summary>
        /// Hide default constructor
        /// </summary>
        private FileSystemEntry()
        {
            FileSystemEntries = new List<FileSystemEntry>();
            Parent = new FileSystemEntry();
            Name = "";
            Type = FileType.File;
            _FileSize = 0;
        }

        public FileSystemEntry(FileType type, string name, int fileSize, FileSystemEntry? parent)
        {
            Type = type;

            // set file size to 0 if this is a directory
            if (Type == FileType.Directory)
                FileSize = 0;
            else
                FileSize = fileSize;

            Name = name;

            // top level directory will reference itself as the parent
            if (parent == null)
                Parent = this;
            else
                Parent = parent;

            FileSystemEntries = new List<FileSystemEntry>();
        }

        public FileType Type { get; set; }
        public string Name { get; set; }
        public int FileSize { 
            get {
                // if the type is file, return the private file size value.
                // if it is a directory, return the sum of all files in the directory
                if (Type == FileType.File)
                {
                    return _FileSize;
                }
                else
                {
                    var sum = 0;
                    foreach (var entry in FileSystemEntries)
                    {
                        sum += entry.FileSize;
                    }
                    return sum;
                }
            } 
            set { 
                _FileSize = value;
            } 
        }

        /// <summary>
        /// Adds an entry to the file system.
        /// </summary>
        /// <param name="Entry">File system entry to be added</param>
        /// <returns>True if the entry was added successfully. Will return false if this entry is not a directory</returns>
        public bool Add(FileSystemEntry Entry)
        {
            if(Type == FileType.Directory)
            {
                FileSystemEntries.Add(Entry);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the file system entry with the matching name
        /// </summary>
        /// <param name="name">Name of the entry</param>
        /// <returns>The file system entry, or null if it doesn't exist</returns>
        public FileSystemEntry? GetEntry(string name)
        {
            if (name == "..")
                return Parent;
            else
                return FileSystemEntries.Where(x => x.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Calculates the disk usage for this entry (includes subdirectories)
        /// </summary>
        /// <returns>Total disk usage for this directory</returns>
        public int DiskUsage()
        {
            var usage = 0;
            
            usage += FileSize;

            // if the usage exceeds 100000, exclude it from the calculation
            if (usage > 100000)
                usage = 0;

            // add the usage of any subdirectories which don't exceed 100000
            foreach(var i in FileSystemEntries.Where(x => x.Type == FileType.Directory))
            {
                usage += i.DiskUsage();
            }

            return usage;
        }

        /// <summary>
        /// Finds the smallest possible directory to delete which will free up enough space
        /// </summary>
        /// <param name="sizeNeeded">The minimum amount of space that needs to be freed</param>
        /// <returns>The size of the directory to delete</returns>
        public int DirectoryToDelete(int sizeNeeded)
        {
            var size = FileSize;

            // find the smallest subdirectory which can free up neough space
            foreach(var i in FileSystemEntries.Where(x => x.Type == FileType.Directory))
            {
                var subDirSize = i.DirectoryToDelete(sizeNeeded);
                if (subDirSize >= sizeNeeded && subDirSize < size)
                    size = subDirSize;
            }

            // return the size
            return size;
        }

        public List<FileSystemEntry> FileSystemEntries;

        private FileSystemEntry Parent;

        private int _FileSize;
    }
}
