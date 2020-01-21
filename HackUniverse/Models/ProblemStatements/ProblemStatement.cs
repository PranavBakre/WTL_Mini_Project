using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackUniverse.Models.ProblemStatements
{
    public class ProblemStatement
    {
        public int? Id { get; set; }
        public int? HackathonId { get; set; }
        public string? problemStatement { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }


    }
}
