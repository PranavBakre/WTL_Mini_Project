using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackUniverse.Models
{
#nullable enable
    public class Hackathon
    {
        //private MiniProjectContext context;

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public string? ContactMail { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactWebsite { get; set; }
        public Byte[]? CoverPhoto { get; set; }
        public Byte[]? Thumbnail { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }

    }
}
