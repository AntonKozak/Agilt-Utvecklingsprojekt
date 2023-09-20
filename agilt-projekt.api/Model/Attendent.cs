using System.ComponentModel.DataAnnotations;


namespace EventApi.Models;



public class Attendent : Person {

    [Required]
    [Key]
    public int AttendentId {get;set;}




}