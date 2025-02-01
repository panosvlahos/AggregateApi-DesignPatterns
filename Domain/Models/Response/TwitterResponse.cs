using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Response
{
    public class TwitterResponse
    {

        [JsonProperty("data")]
        public List<TweetData> Data { get; set; }

        [JsonProperty("includes")]
        public Includes Includes { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }

        public class TweetData
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("created_at")]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("author_id")]
            public string AuthorId { get; set; }

            // Add more properties if you need (like geolocation, public metrics, etc.)
        }

        public class Includes
        {
            [JsonProperty("users")]
            public List<User> Users { get; set; }

            // This could include other things like media, places, etc.
        }

        public class User
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("username")]
            public string Username { get; set; }

            // Add other user-related fields if necessary
        }

        public class Meta
        {
            [JsonProperty("result_count")]
            public int ResultCount { get; set; }

            [JsonProperty("next_token")]
            public string NextToken { get; set; }

            // Add more metadata if necessary (like pagination info, etc.)
        }
    }



