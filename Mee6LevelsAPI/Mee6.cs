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
                Mee6UserInfo user = server.players[i];
                if (user.id.Equals(userID.ToString()))
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
            string imageUrl = $"https://cdn.discordapp.com/avatars/{user.id}/{user.avatar}?size={size}";
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
        public string avatar;
        public int[] detailed_xp;
        public string discriminator;
        public string guild_id;
        public string id;
        public long level;
        public long message_count;
        public string username;
        public long xp;
    }

    public struct Mee6GuildInfo
    {
        public bool allow_join;
        public string icon;
        public string id;
        public bool invite_leaderboard;
        public string leaderboard_url;
        public string name;
        public bool premium;
    }

    public struct Mee6RoleInfo
    {
        public long rank;
        public Mee6Role role;
    }

    public struct Mee6Role
    {
        public long color;
        public bool hoist;
        public string id;
        public bool managed;
        public bool mentionable;
        public string name;
        public long permissions;
        public long position;
    }

    public struct Mee6Server
    {
        public bool admin;
        public string banner_url;
        public Mee6GuildInfo guild;
        public bool is_member;
        public long page;
        public Mee6UserInfo[] players;
        public Mee6RoleInfo[] role_rewards;
        //user_guild_settings not implemented
        public long[] xp_per_message;
        public long xp_rate;
    }
}
