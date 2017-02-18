#r "Newtonsoft.Json"
using Newtonsoft.Json;

static HttpClient httpClient = new HttpClient();
static MemberComparer comparer = new MemberComparer();

public static async Task<string> Run(TimerInfo myTimer, string previousUsersJson, TraceWriter log)
{
    var currentUsersJson = await httpClient.GetStringAsync($"https://slack.com/api/users.list?token={Env("SlackApiToken")}&pretty=1");
    
    if (!string.IsNullOrEmpty(previousUsersJson))
    {
        var currentUsers = DeserializeMembers(currentUsersJson);
        var previousUsers = DeserializeMembers(previousUsersJson);
        var deletedUsers = previousUsers.Except(currentUsers, comparer).ToList();
        var newUsers = currentUsers.Except(previousUsers, comparer).ToList();

        await SendMessage("These users just got deleted: ", deletedUsers, log);
        await SendMessage("These users just got added: ", newUsers, log);
    }

    return currentUsersJson;
}

private static async Task SendMessage(string message, IEnumerable<Member> users, TraceWriter log)
{
    if (users.Any())
    {
        var slackMessage = message + string.Join(", ", users.Select(u => $"{u.Name}({u.Real_name ?? ""})"));
        log.Info($"Sending to {Env("ChannelsToNotify")}:\n{slackMessage}");

        var channels = Env("ChannelsToNotify").Split(',');
        foreach (var channel in channels)
        {
            await httpClient.PostAsync($"{Env("SlackbotUrl")}&channel={channel.Trim()}", new StringContent(slackMessage));
        }
    }
}

private static IEnumerable<Member> DeserializeMembers(string json) =>
    JsonConvert.DeserializeObject<UserCollection>(json).Members.Where(m => !m.Deleted);

private static string Env(string name) => System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);

public class UserCollection
{
    public IEnumerable<Member> Members { get; set; } = new List<Member>();
}

public class Member
{
    public string Name { get; set; }
    public string Real_name { get; set; }
    public bool Deleted { get; set; }
}

public class MemberComparer : IEqualityComparer<Member>
{
    public bool Equals(Member x, Member y) => x.Name == y.Name;
    public int GetHashCode(Member m) => 0;
}