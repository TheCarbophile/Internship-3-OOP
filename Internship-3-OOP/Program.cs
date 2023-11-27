using Internship_3_OOP.Repository;

var phoneBook = new Dictionary<Contact, List<Call>>();

while (true)
{
    Console.WriteLine("1 - Ispis svih kontakata");
    Console.WriteLine("2 - Dodavanje novih kontakata u imenik");
    Console.WriteLine("3 - Brisanje kontakta iz imenika");
    Console.WriteLine("4 - Editiranje preference kontakta");
    Console.WriteLine("5 - Upravljanje kontaktom");
    Console.WriteLine("6 - Ispis svih poziva");
    Console.WriteLine("0 - Izlaz iz aplikacije");

    switch (Console.ReadLine())
    {
        case "1":
            PrintContacts();
            break;
        case "2":
            AddContacts();
            break;
        case "3":
            RemoveContact();
            break;
        case "4":
            EditContactPreference();
            break;
        case "5":
            ManageContact();
            break;
        case "6":
            PrintCalls();
            break;
        case "0":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Krivi unos!");
            break;
    }
}

void PrintContacts()
{
    //TODO: Implement
}

void AddContacts()
{
    //TODO: Implement
}

void RemoveContact()
{
    //TODO: Implement
}

void EditContactPreference()
{
    //TODO: Implement
}

void ManageContact()
{
    while (true)
    {
        Console.WriteLine("Unesite broj mobitela kontakta kojim želite upravljati:");
        var phoneNumber = Console.ReadLine();
        var contact = phoneBook.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (contact == null)
        {
            Console.WriteLine("Ne postoji kontakt s unesenim brojem mobitela!");
            continue;
        }
        Console.WriteLine("1 - Ispis svih poziva s kontaktaom");
        Console.WriteLine("2 - Pozivanje kontakta");
        Console.WriteLine("0 - Povratak na glavni izbornik");

        switch (Console.ReadLine())
        {
            case "1":
                PrintCalls(contact);
                break;
            case "2":
                contact.PlaceCall();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Krivi unos!");
                break;
        }
    }
}

void PrintCalls(Contact? contact = null)
{
    //TODO: Implement
}