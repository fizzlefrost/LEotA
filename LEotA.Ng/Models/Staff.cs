using System;
using System.Text.Json.Serialization;

namespace LEotA.Models
{
    public class Staff
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public CultureBase Name { get; set; }
        [JsonPropertyName("degree")]
        public CultureBase Degree { get; set; }
        [JsonPropertyName("title")]
        public CultureBase Title { get; set; }
        [JsonPropertyName("position")]
        public CultureBase Position { get; set; }
        [JsonPropertyName("role")]
        public StaffRoles Role { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("text")]
        public CultureBase Text { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }

    public enum StaffRoles
    {
        Director = 1, KeyMember = 2, Member = 3
    }

    public class StaffGetModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("degree")]
        public string Degree { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("position")]
        public string Position { get; set; }
        [JsonPropertyName("role")]
        public StaffRoles Role { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        // public Engine.Entities.StaffRoles GetStaffRole()
        // {
        //     switch (this.Role)
        //     {
        //         case "1":
        //             return Engine.Entities.StaffRoles.Director;
        //         case "2":
        //             re
        //     }
        // }
    }
    
    public class StaffCreateModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("degree")]
        public string Degree { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("position")]
        public string Position { get; set; }
        [JsonPropertyName("role")]
        public StaffRoles Role { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
    
    public class StaffUpdateModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("degree")]
        public string Degree { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("position")]
        public string Position { get; set; }
        [JsonPropertyName("role")]
        public StaffRoles Role { get; set; }
        [JsonPropertyName("embedLink")]
        public string EmbedLink { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("newId")]
        public string NewId { get; set; }
    }
}