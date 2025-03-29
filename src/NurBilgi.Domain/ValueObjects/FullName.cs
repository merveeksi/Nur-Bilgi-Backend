using System.Text.RegularExpressions;

namespace NurBilgi.Domain.ValueObjects;

public sealed record FullName
{
    private const string Pattern = @"^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$";
    private const int MinLength = 2;
    private const int MaxLength = 100;

    public string FirstName { get; init; }
    public string LastName { get; init; }

    public FullName(string firstName, string lastName)
    {
        if (!IsValid(firstName))
            throw new ArgumentException("Geçersiz ad formatı.");

        if (!IsValid(lastName))
            throw new ArgumentException("Geçersiz soyad formatı.");

        FirstName = firstName;
        LastName = lastName;
    }

    public static bool IsValid(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return Regex.IsMatch(value, Pattern) && value.Length >= MinLength && value.Length <= MaxLength;
    }

    public static FullName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Ad ve soyad boş olamaz.");

        var parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
            throw new ArgumentException("Geçersiz ad soyad formatı. Beklenen format: 'Ad Soyad'");

        string firstName = parts[0];
        string lastName = parts[1];

        return new FullName(firstName, lastName);
    }

    public static implicit operator string(FullName fullName) => fullName.ToString();

    public static implicit operator FullName(string value) => Create(value);

    public override string ToString() => $"{FirstName} {LastName}";
    
    public string GetInitials() => $"{FirstName[0]}.{LastName[0]}";
}