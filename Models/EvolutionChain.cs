namespace APIPoke.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class EvolutionChain
    {
        //[JsonProperty("baby_trigger_item")]
        //public object BabyTriggerItem { get; set; }

        [JsonProperty("chain")]
        public Chain? Chain { get; set; }

        //[JsonProperty("id")]
        //public int Id { get; set; }
    }

    public partial class Chain
    {
        //[JsonProperty("evolution_details")]
        //public EvolutionDetail[] EvolutionDetails { get; set; }


        //[JsonProperty("is_baby")]
        //public bool IsBaby { get; set; }

        [JsonProperty("species")]
        public Species? Species { get; set; }
        [JsonProperty("evolves_to")]
        public Chain[]? EvolvesTo { get; set; }
    }

    //public partial class EvolutionDetail
    //{
    //    [JsonProperty("gender")]
    //    public object Gender { get; set; }

    //    [JsonProperty("held_item")]
    //    public object HeldItem { get; set; }

    //    [JsonProperty("item")]
    //    public object Item { get; set; }

    //    [JsonProperty("known_move")]
    //    public object KnownMove { get; set; }

    //    [JsonProperty("known_move_type")]
    //    public object KnownMoveType { get; set; }

    //    [JsonProperty("location")]
    //    public object Location { get; set; }

    //    [JsonProperty("min_affection")]
    //    public object MinAffection { get; set; }

    //    [JsonProperty("min_beauty")]
    //    public object MinBeauty { get; set; }

    //    [JsonProperty("min_happiness")]
    //    public object MinHappiness { get; set; }

    //    [JsonProperty("min_level")]
    //    public long MinLevel { get; set; }

    //    [JsonProperty("needs_overworld_rain")]
    //    public bool NeedsOverworldRain { get; set; }

    //    [JsonProperty("party_species")]
    //    public object PartySpecies { get; set; }

    //    [JsonProperty("party_type")]
    //    public object PartyType { get; set; }

    //    [JsonProperty("relative_physical_stats")]
    //    public object RelativePhysicalStats { get; set; }

    //    [JsonProperty("time_of_day")]
    //    public string TimeOfDay { get; set; }

    //    [JsonProperty("trade_species")]
    //    public object TradeSpecies { get; set; }

    //    [JsonProperty("trigger")]
    //    public Species Trigger { get; set; }

    //    [JsonProperty("turn_upside_down")]
    //    public bool TurnUpsideDown { get; set; }
    //}

}
