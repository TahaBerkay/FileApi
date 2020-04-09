using System.Collections.Generic;
using FileApi.Models;

namespace FileApi.Extensions
{
    public static class FileExtension
    {
        public static void WithoutCriticalData(this List<File> files)
        {
            files.ForEach(file => file.WithoutCriticalData());
        }

        public static File WithoutCriticalData(this File file)
        {
            return file.WithoutContent().WithoutFileNameInFs();
        }

        private static File WithoutContent(this File file)
        {
            file.FileContent = null;
            return file;
        }

        private static File WithoutFileNameInFs(this File file)
        {
            file.FileNameInFs = null;
            return file;
        }
    }
}