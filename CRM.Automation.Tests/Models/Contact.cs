namespace CRM.Automation.Tests.Models;

public sealed class Contact : IEquatable<Contact>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public List<string> Categories { get; set; }
    
    public bool Equals(Contact? other)
    {
        if (other == null)
            return false;

        return FirstName == other.FirstName 
               && LastName == other.LastName 
               && Role == other.Role
               && Categories.SequenceEqual(other.Categories);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals(obj as Contact);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (FirstName?.GetHashCode() ?? 0);
            hash = hash * 23 + (LastName?.GetHashCode() ?? 0);
            hash = hash * 23 + (Role?.GetHashCode() ?? 0);
            foreach (var category in Categories)
            {
                hash = hash * 23 + (category?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }
}