using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilt_projekt.api.ViewModel;
public class EventPostViewModel
{
    public string EventName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}