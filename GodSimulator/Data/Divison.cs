using System.ComponentModel.DataAnnotations;

namespace GodSimulator.Data
{
    public class Division
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}