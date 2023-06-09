using Newtonsoft.Json;
using SQLite;

public sealed class Users
{
    [PrimaryKey]
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("userName")]
    public string UserName { get; set; }
    [JsonProperty("dateTo")]
    public DateTime DateTo { get; set; }
    [JsonProperty("fullName")]
    public string FullName { get; set; }
    [JsonProperty("stringToken")]
    public string StringToken { get; set; }


}