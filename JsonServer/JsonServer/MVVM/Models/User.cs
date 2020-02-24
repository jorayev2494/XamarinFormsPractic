using JsonServer.MVVM.Models.Interfaces;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonServer.MVVM.Models
{
    [Table("users")]
    public class User : IModel
    {
        [JsonProperty("id")]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonProperty("name")]
        [MaxLength(255), NotNull]
        public string Name { get; set; }

        [JsonProperty("last_name")]
        [MaxLength(255), NotNull]
        public string LastName { get; set; }

        [JsonProperty("email")]
        [MaxLength(250), Unique, NotNull]
        public string Email { get; set; }

        [JsonProperty("phone")]
        [MaxLength(250)]
        public string Phone { get; set; }

        [JsonProperty("avatar")]
        [MaxLength(250)]
        public string Avatar { get; set; }

        public User()
        {

        }
    }
}
