using System.Collections.Generic;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Application.Output
{
    public class GeneralResponse
    {
        public bool Success { get; }

        public List<string> Errors { get; set; }

        public Distributor? Distributor { get; set; }

        public Product? Product { get; set; }

        public Sale? Sale { get; set; }

        public string Error
        {
            set
            {
                if (value == null) return;
                Errors = new List<string>(){value};
            }
        }

        public GeneralResponse(bool success)
        {
            Success = success;
        }
    }
}