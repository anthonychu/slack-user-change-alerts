public class UserCollection
{
    public IEnumerable<Member> Members { get; set; } = new List<Member>();
}

public class Member
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool Deleted { get; set; }
    public Profile Profile { get; set; }
}

public class Profile
{
    public string Real_name { get; set; }
}

public class MemberComparer : IEqualityComparer<Member>
{
    public bool Equals(Member x, Member y) => x.Id == y.Id;
    public int GetHashCode(Member m) => 0;
}
