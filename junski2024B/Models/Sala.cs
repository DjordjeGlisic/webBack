using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Models;

[Table("Sala")]
public class Sala
{
    [Key]
    public int Id{get;set;}
    
    [Required]
    [Range(100,5000)]
    public int Kapacitet{get;set;}
    [Required]
    [Range(300,5000)]
    public float Cena{get;set;}
    [Required]
    [StringLength(50)]
    public string Adresa{get;set;}
   
    [JsonIgnore]
    public List<Iznajmljena> Iznajmljivaci{get;set;}=new List<Iznajmljena>();
    
    
}