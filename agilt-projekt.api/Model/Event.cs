using System.ComponentModel.DataAnnotations;


namespace EventApi.Models;


public class EventModel {

    [Key]
    public int EventId {get;set;}
    [Required]
    public string EventName {get;set;} = string.Empty;

    public string Description {get;set;} = string.Empty;

     [DataType(DataType.Date)]
    public DateTime StartDate {get;set;}

    [DataType(DataType.Date)]
    public DateTime EndDate {get;set;}



    public List<AttendentModel> Attendents {get;set;} = new List<AttendentModel>();
}
