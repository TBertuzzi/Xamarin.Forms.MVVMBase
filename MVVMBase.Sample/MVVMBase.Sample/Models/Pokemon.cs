using System;
using Newtonsoft.Json;

namespace MVVMBase.Sample.Models
{
    public class Pokemon
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("sprites")]
        public Sprites Sprites { get; set; }

        public byte[] Image { get; set; }

        public byte[] ImageBack { get; set; }

        [JsonProperty("types")]
        public TypeElement[] Types { get; set; }

        [JsonProperty("base_experience")]
        public long BaseExperience { get; set; }

        public string AllTypes { get; set; }
    }

    public class Sprites
    {
        [JsonProperty("front_default")]
        public Uri FrontDefault { get; set; }

        [JsonProperty("back_default")]
        public Uri BackDefault { get; set; }
    }

    public partial class TypeElement
    {
        [JsonProperty("slot")]
        public long Slot { get; set; }

        [JsonProperty("type")]
        public Species Type { get; set; }
    }

    public partial class Species
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

}
