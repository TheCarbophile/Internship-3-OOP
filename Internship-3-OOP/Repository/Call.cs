namespace Internship_3_OOP.Repository;

public class Call
{
    public DateTime TimeOfCall { get; }
    public CallStatus Status { get; private set; }
    
    public Call(CallStatus status)
    {
        TimeOfCall = DateTime.Now;
        Status = status;
    }
    
    public void ConcludeCall()
    {
        if (Status == CallStatus.InProgress)
            Status = CallStatus.Concluded;
    }
}