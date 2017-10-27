using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace twitchMirrorBot.models
{
    class ChannelInfo
    {
        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("stream")]
        public Stream Stream { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }

    public partial class Stream
    {
        [JsonProperty("channel")]
        public Channel Channel { get; set; }

        [JsonProperty("is_playlist")]
        public bool IsPlaylist { get; set; }

        [JsonProperty("_links")]
        public OtherLinks Links { get; set; }

        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("average_fps")]
        public long AverageFps { get; set; }

        [JsonProperty("delay")]
        public long Delay { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("stream_type")]
        public string StreamType { get; set; }

        [JsonProperty("preview")]
        public Preview Preview { get; set; }

        [JsonProperty("video_height")]
        public long VideoHeight { get; set; }

        [JsonProperty("viewers")]
        public long Viewers { get; set; }
    }

    public partial class Channel
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("banner")]
        public object Banner { get; set; }

        [JsonProperty("_links")]
        public OtherOtherLinks Links { get; set; }

        [JsonProperty("_id")]
        public long Id { get; set; }

        [JsonProperty("background")]
        public object Background { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("broadcaster_language")]
        public string BroadcasterLanguage { get; set; }

        [JsonProperty("delay")]
        public object Delay { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("profile_banner")]
        public string ProfileBanner { get; set; }

        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("followers")]
        public long Followers { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mature")]
        public bool Mature { get; set; }

        [JsonProperty("partner")]
        public bool Partner { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("profile_banner_background_color")]
        public string ProfileBannerBackgroundColor { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("video_banner")]
        public string VideoBanner { get; set; }

        [JsonProperty("views")]
        public long Views { get; set; }
    }

    public partial class OtherOtherLinks
    {
        [JsonProperty("features")]
        public string Features { get; set; }

        [JsonProperty("commercial")]
        public string Commercial { get; set; }

        [JsonProperty("chat")]
        public string Chat { get; set; }

        [JsonProperty("editors")]
        public string Editors { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("subscriptions")]
        public string Subscriptions { get; set; }

        [JsonProperty("follows")]
        public string Follows { get; set; }

        [JsonProperty("stream_key")]
        public string StreamKey { get; set; }

        [JsonProperty("teams")]
        public string Teams { get; set; }

        [JsonProperty("videos")]
        public string Videos { get; set; }
    }

    public partial class OtherLinks
    {
        [JsonProperty("self")]
        public string Self { get; set; }
    }

    public partial class Preview
    {
        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("small")]
        public string Small { get; set; }

        [JsonProperty("template")]
        public string Template { get; set; }
    }

}

