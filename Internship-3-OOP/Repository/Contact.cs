namespace Internship_3_OOP.Repository;

public class Contact
{
    public string Name { get; }
    public string PhoneNumber { get; }
    public ContactPreference Preference { get; set; }
    
    public Contact(string name, string phoneNumber, ContactPreference contactPreference)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Preference = contactPreference;
    }

    public (int, Call?) PlaceCall()
    {
        return Preference == ContactPreference.Blocked ? (0, null) :
            new Random().Next(0, 2) == 0 ? (0, new Call(CallStatus.Missed)) :
            (new Random().Next(1, 21), new Call(CallStatus.InProgress));
    }
}