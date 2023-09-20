using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EventApi.Models;



public class AttendentModel : Person {

    [Required]
    [Key]
    public int AttendentId {get;set;}


    // En medlem kan vara anmäld till många event
    public ICollection<EventModel> EventList {get;set;}

    // Främmande nyckel
    [ForeignKey("EventId")]
    public int EventId {get;set;}


}