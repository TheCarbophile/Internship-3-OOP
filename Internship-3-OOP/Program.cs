using Internship_3_OOP.Repository;

var phoneBook = new Dictionary<Contact, List<Call>>();

Console.WriteLine("Dobrodošli u telefosnki imenik! Da bi ste otkazali unos u bilo kojem trenutku, unesite Ctrl + Z!");
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
    foreach (var contact in phoneBook.Keys)
        Console.WriteLine(
            $"Ime: {contact.Name}, Broj mobitela: {contact.PhoneNumber}, Preferenca: {contact.Preference}");
}

void AddContacts()
{
    string? name;
    while (true)
    {
        Console.WriteLine("Unesite ime kontakta");
        name = Console.ReadLine();

        if (name == null) return;

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Ime ne može biti prazno!");
            continue;
        }

        break;
    }

    string? phoneNumber;
    while (true)
    {
        var breakLoop = false;
        Console.WriteLine("Unesite broj mobitela kontakta");
        phoneNumber = Console.ReadLine();

        if (phoneNumber == null) return;

        if (string.IsNullOrEmpty(phoneNumber))
        {
            Console.WriteLine("Broj mobitela ne može biti prazan!");
            continue;
        }

        if (phoneNumber.Length != 13)
        {
            Console.WriteLine("Unesite broj mobitela u internacionalnom formatu (+385910000000)!");
            continue;
        }

        var i = 0;
        foreach (var digit in phoneNumber)
        {
            i++;
            if (char.IsDigit(digit) || digit == '+' && i == 1) continue;
            Console.WriteLine("Broj mobitela može sadržavati samo brojeve (s iznikom plusa na početuku)!");
            breakLoop = true;
            break;
        }

        if (breakLoop)
            continue;

        var number = phoneNumber;
        if (phoneBook.Keys.Any(c => c.PhoneNumber == number))
        {
            Console.WriteLine("Broj mobitela već postoji!");
            continue;
        }

        break;
    }
    
    ContactPreference preference;
    while (true)
    {
        Console.WriteLine("Unesite preferencu kontakta (1 - Favorit, 2 - Normalan, 3 - Blokiran)");
        var preferenceInput = Console.ReadLine();

        if (preferenceInput == null) return;

        switch (preferenceInput)
        {
            case "1":
                preference = ContactPreference.Favorite;
                break;
            case "2":
                preference = ContactPreference.Regular;
                break;
            case "3":
                preference = ContactPreference.Blocked;
                break;
            default:
                Console.WriteLine("Krivi unos!");
                continue;
        }

        break;
    }
    
    phoneBook.Add(new Contact(name, phoneNumber, preference), new List<Call>());
}

void RemoveContact()
{
    while (true)
    {
        Console.WriteLine("Unesite broj mobitela kontakta kojeg želite izbrisati:");
        var phoneNumber = Console.ReadLine();
        if (phoneNumber == null) return;
        var contact = phoneBook.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (contact == null)
        {
            Console.WriteLine("Ne postoji kontakt s unesenim brojem mobitela!");
            continue;
        }
        phoneBook.Remove(contact);
        break;
    }
}

void EditContactPreference()
{
    Contact? contact;
    while (true)
    {
        Console.WriteLine("Unesite broj mobitela kontakta kojeg želite editirati:");
        var phoneNumber = Console.ReadLine();
        if (phoneNumber == null) return;
        
        contact = phoneBook.Keys.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        if (contact == null)
        {
            Console.WriteLine("Ne postoji kontakt s unesenim brojem mobitela!");
            continue;
        }
        break;
    }
    
    while (true)
    {
        Console.WriteLine("Unesite novu preferencu kontakta (1 - Favorit, 2 - Normalan, 3 - Blokiran)");
        var preferenceInput = Console.ReadLine();

        if (preferenceInput == null) return;

        switch (preferenceInput)
        {
            case "1":
                contact.Preference = ContactPreference.Favorite;
                break;
            case "2":
                contact.Preference = ContactPreference.Regular;
                break;
            case "3":
                contact.Preference = ContactPreference.Blocked;
                break;
            default:
                Console.WriteLine("Krivi unos!");
                continue;
        }

        break;
    }
}

void ManageContact()
{
    while (true)
    {
        Console.WriteLine("Unesite broj mobitela kontakta kojim želite upravljati:");
        var phoneNumber = Console.ReadLine();
        if (phoneNumber == null) return;
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
                var status = contact.PlaceCall();
                if (status.Item2 == null)
                {
                    Console.WriteLine("Kontakt je blokiran!");
                }
                else if (status.Item1 == 0)
                {
                    Console.WriteLine(status.Item1);
                    Console.WriteLine("Poziv nije odgovoren!");
                    phoneBook[contact].Add(status.Item2);
                }
                else
                {
                    Console.WriteLine("Poziv je u tijeku!");
                    Thread.Sleep(status.Item1 * 1000);
                    Console.WriteLine($"Poziv je završen! Trajanje poziva: {status.Item1} sekundi");
                    status.Item2.ConcludeCall();
                    phoneBook[contact].Add(status.Item2);
                }
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
    if (contact == null)
    {
        foreach (var call in phoneBook.Values.SelectMany(calls => calls))
            Console.WriteLine(
                $"Vrijeme poziva: {call.TimeOfCall}, Status poziva: {call.Status}");
    }
    else
    {
        foreach (var call in phoneBook[contact])
            Console.WriteLine(
                $"Vrijeme poziva: {call.TimeOfCall}, Status poziva: {call.Status}");
    }
}