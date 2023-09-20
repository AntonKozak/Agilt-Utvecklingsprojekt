using System.ComponentModel.DataAnnotations;


namespace EventApi.Models;


public class Event {

    [Key]
    public int EventId {get;set;}
    [Required]
    public string EventName {get;set;} = string.Empty;

    public int EventNumber {get;set;}

    public DateTime StartDate {get;set;}

    public DateTime EndDate {get;set;}


}
