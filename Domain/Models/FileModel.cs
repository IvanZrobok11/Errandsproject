using System;
using System.Collections.Generic;
using System.Text;

namespace Errands.Domain.Models
{
    public class FileModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public TypeFile Type { get; set; }

        public Guid ErrandId { get; set; }
        public Errand Errand { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

    }
    public enum TypeFile
    {
        Image,
        File
    }
}
