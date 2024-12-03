using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.JSInterop;
namespace Models{
[Table("Korisnik")]
public class Korisnik{
    [Key]
    public int Id{get;set;}
    [Required]
    [StringLength(20)]
    public string Ime{get;set;}
    [Required]
    [StringLength(20)]
    public string Prezime{get;set;}
    [Required]
    public long Jmbg{get;set;}
    [JsonIgnore]
    public List<Iznajmljena>Iznajmljene=new List<Iznajmljena> ();
    
}
}