using JsonServer.MVVM.Models.Interfaces;
using JsonServer.Services.Convert;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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
        [MaxLength(int.MaxValue)]
        public string Avatar { get; set; }

        [Ignore]
        public ImageSource AvatarSource { get; set; }


        public User()
        {

        }
    }
}
