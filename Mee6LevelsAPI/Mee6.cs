using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Mee6LevelsAPI
{
    public static class Mee6
    {
        const string Url = "https://mee6.xyz/api/plugins/levels/leaderboard/";
        public static int Limit = 1000;
        public static Mee6UserInfo GetUserInfo(long guildID, long userID)
        {
            Mee6Server server = GetServer(guildID);

            for(int i = 0; i < Limit; i++)
            {
                Mee6UserInfo user = server.Users[i];
                if (user.Id.Equals(userID.ToString()))
                {
                    return user;
                }
            }
            return new Mee6UserInfo();
        }

        public static Mee6Server GetServer(long guildID)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(Url + $"{guildID}?limit={Limit}").Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            Mee6Server server = JsonConvert.DeserializeObject<Mee6Server>(responseBody);
            return server;
        }

        public static async Task<Mee6Server> GetServerAsync(long guildID)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Url + $"{guildID}?limit={Limit}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Mee6Server server = JsonConvert.DeserializeObject<Mee6Server>(responseBody);
            return server;
        }

        public static Mee6Server GetServer(long guildID, string fileName)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(Url + $"{guildID}?limit={Limit}").Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            Mee6Server server = JsonConvert.DeserializeObject<Mee6Server>(responseBody);
            string json = JsonConvert.SerializeObject(server, Formatting.Indented);
            File.WriteAllText(fileName, json);
            return server;
        }

        public static Image GetAvatar(Mee6UserInfo user, int size)
        {
            string imageUrl = $"https://cdn.discordapp.com/avatars/{user.Id}/{user.Avatar}?size={size}";
            using (System.Net.WebClient webClient = new System.Net.WebClient())
            {
                using (Stream stream = webClient.OpenRead(imageUrl))
                {
                    return Image.FromStream(stream);
                }
            }
        }
    }

    public struct Mee6UserInfo
    {
        [JsonProperty("avatar")]
        public string Avatar;
        [JsonProperty("detailed_xp")]
        public int[] DetailedXp;
        [JsonProperty("discriminator")]
        public string Discriminator;
        [JsonProperty("guild_id")]
        public string GuildId;
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("level")]
        public long Level;
        [JsonProperty("message_count")]
        public long MessageCount;
        [JsonProperty("username")]
        public string Username;
        [JsonProperty("xp")]
        public long Xp;
    }

    public struct Mee6GuildInfo
    {
        [JsonProperty("allow_join")]
        public bool AllowJoin;
        [JsonProperty("icon")]
        public string Icon;
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("invite_leaderboard")]
        public bool InviteLeaderboard;
        [JsonProperty("leaderboard_url")]
        public string LeaderboardURL;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("premium")]
        public bool Premium;
    }

    public struct Mee6RoleInfo
    {
        [JsonProperty("rank")]
        public long Rank;
        [JsonProperty("role")]
        public Mee6Role Role;
    }

    public struct Mee6Role
    {
        [JsonProperty("color")]
        public long Color;
        [JsonProperty("hoist")]
        public bool Hoist;
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("managed")]
        public bool Managed;
        [JsonProperty("mentionable")]
        public bool Mentionable;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("permissions")]
        public long Permissions;
        [JsonProperty("position")]
        public long Position;
    }

    public struct Mee6Server
    {
        [JsonProperty("admin")]
        public bool Admin;
        [JsonProperty("banner_url")]
        public string BannerURL;
        [JsonProperty("guild")]
        public Mee6GuildInfo Guild;
        [JsonProperty("is_member")]
        public bool IsMember;
        [JsonProperty("page")]
        public long Page;
        [JsonProperty("players")]
        public Mee6UserInfo[] Users;
        [JsonProperty("role_rewards")]
        public Mee6RoleInfo[] RewardRoles;
        //user_guild_settings not implemented
        [JsonProperty("xp_per_message")]
        public long[] XPPerMessage;
        [JsonProperty("xp_rate")]
        public long XPRate;
    }
}
