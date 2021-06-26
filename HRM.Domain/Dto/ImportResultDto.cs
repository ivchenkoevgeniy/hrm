using System.Collections;
using System.Collections.Generic;

namespace HRM.Domain.DTO
{
    public class ImportResultDto
    {
        public List<string> Created { get; set; }
        public List<string> Modified { get; set; }
        public List<string> Errors { get; set; }

        public ImportResultDto()
        {
            Created = new List<string>();
            Modified = new List<string>();
            Errors = new List<string>();
        }
    }
}