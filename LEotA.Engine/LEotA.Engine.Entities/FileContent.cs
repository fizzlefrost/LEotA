using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class FileContent : Identity
    {
        public byte[] Content { get; set; }
        public string MimeType { get; set; }
        public Guid MasterId { get; set; }
        public string? Author { get; set; }
        public FileType FileType { get; set; }
    }

    public enum FileType
    {
        Image, Publication, MiniImage, AlbumMainImage
    }
}