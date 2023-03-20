using Microsoft.AspNetCore.Mvc;

namespace api.multitracks.com.Models
{
    public class PagingParams
    {
        [FromHeader]
        public int Page { get; set; }

        [FromHeader]
        public int Items { get; set; }

        [FromHeader]
        public string? Order { get; set; } = "ASC";

        [FromHeader]
        public int TotalItems { get; set; }

    }
}
