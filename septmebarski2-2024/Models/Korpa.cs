using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Korpa")]
    public class Korpa
    {
        [Key]
        public int Id{ get; set; }  
        public List<Proizvod> Proizvodi{get;set;}=new List<Proizvod>();
      
        
    }
}