﻿using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace UnpakSipaksi.Common.Presentation.FileManager
{
    public class FileProvider : IFileProvider
    {
        // Karakter berbahaya yang harus difilter
        private static readonly char[] DangerousCharacters =
        {
            '\0', '\u202E', '\u202D', '\u202A', '\u202B', '\u202C', '\u2066', '\u2067', '\u2068', '\u2069', // Unicode Spoofing
            '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\u0007', '\u0008', '\u0009', '\u000A', // ASCII Control
            '\u000B', '\u000C', '\u000D', '\u000E', '\u000F', '\u0010', '\u0011', '\u0012', '\u0013', '\u0014',
            '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001A', '\u001B', '\u001C', '\u001D', '\u001E', '\u001F', '\u007F',
            '\u200B', '\u200C', '\u200D', '\u2060' // Zero-width dan whitespace tersembunyi
        };

        private static readonly string[] AllowedMimeTypes = {
            "image/jpeg", 
            "image/png",
            "application/pdf",
            "application/msword",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "application/vnd.ms-powerpoint",
            "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            "application/vnd.ms-excel",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        };

        private static readonly Dictionary<string, string[]> MimeTypeAllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            { "image/jpeg", new[] { "jpg", "jpeg" } },
            { "image/png", new[] { "png" } },
            { "application/pdf", new[] { "pdf" } },
            { "application/msword", new[] { "doc" } },
            { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", new[] { "docx" } },
            { "application/vnd.ms-powerpoint", new[] { "ppt" } },
            { "application/vnd.openxmlformats-officedocument.presentationml.presentation", new[] { "pptx" } },
            { "application/vnd.ms-excel", new[] { "xls" } },
            { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", new[] { "xlsx" } }
        };

        public FileProvider() { }

        public string GenerateFileName(IFormFile file)
        {
            //string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file.FileName);
            string safeExtension = GetSafeExtension(file);

            return $"{Guid.NewGuid()}.{safeExtension}";
        }

        public string GetSafeExtension(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName).TrimStart('.').ToLowerInvariant();
            return FilterExtension(extension);
        }

        private static string FilterExtension(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
            {
                return "unknown"; // Hindari ekstensi kosong yang bisa menyebabkan masalah keamanan
            }

            StringBuilder filtered = new(extension.Length);
            foreach (char c in extension)
            {
                if (!DangerousCharacters.Contains(c) && !char.IsControl(c)) // Hilangkan karakter kontrol & RLTO/LRTO
                {
                    filtered.Append(c);
                }
            }

            return filtered.ToString();
        }

        public bool IsSafeFileExtension(string extension)
        {
            string[] allowedExtensions = { "jpg", "jpeg", "png", "pdf", "doc", "docx", "xlsx" };
            return Array.Exists(allowedExtensions, ext => ext.Equals(extension, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsSafeMimeType(IFormFile file)
        {
            return AllowedMimeTypes.Contains(file.ContentType, StringComparer.OrdinalIgnoreCase);
        }

        public bool IsValidMimeTypeAllowedExtension(string mimeType, string extension)
        {
            if (MimeTypeAllowedExtensions.TryGetValue(mimeType, out var allowedExtensions))
            {
                return allowedExtensions.Any(ext => string.Equals(ext, extension, StringComparison.OrdinalIgnoreCase));
            }

            return false;
        }
    }
}
