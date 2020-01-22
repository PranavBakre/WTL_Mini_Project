using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackUniverse.Models.User
{
    public class Profile
    {
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Occupation { get; set; }
        public string? Organization { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? ContactPhone { get; set; }
        public char? Type { get; set; }
    }
}
