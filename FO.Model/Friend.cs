using System.ComponentModel.DataAnnotations;

namespace FO.Model
{
    public class Friend
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!; //https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
    }
}