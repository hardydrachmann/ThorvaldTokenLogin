using System.ComponentModel.DataAnnotations;

namespace Client.BE
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
