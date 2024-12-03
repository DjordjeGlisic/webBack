using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table("Brend")]
    public class Brend{
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Naziv{get;set;}

        public List<Auto>VozilaSaBrendom{get;set;}=new List<Auto>();
        
    }
}