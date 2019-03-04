using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace _17ceBackendFunction.ViewModel
{
    public class UserViewModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "Account")]
        public string Account { get; set; }
        [JsonProperty(PropertyName = "Username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "Remark")]
        public string Remark { get; set; }
        [JsonProperty(PropertyName = "Roles")]
        public List<string> Roles { get; set; }
        [JsonProperty(PropertyName = "CustomerId")]
        public string CustomerId { get; set; }
    }

}