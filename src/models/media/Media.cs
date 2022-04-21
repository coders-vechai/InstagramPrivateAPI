using InstagramPrivateAPI.src.models.user;
using static InstagramPrivateAPI.src.models.media.timeline.Comment;

namespace InstagramPrivateAPI.src.models.media
{
    internal class Media
    {
        public long pk { get; set; }
        public String id { get; set; }
        public long taken_at { get; set; }
        public long device_timestamp { get; set; }
        public String media_type { get; set; }
        public String code { get; set; }
        public String client_cache_key { get; set; }
        public int filter_type { get; set; }
        public User user { get; set; }
        public Caption caption { get; set; }
        public bool can_viewer_reshare { get; set; }
        public bool photo_of_you { get; set; }
        public bool can_viewer_save { get; set; }
    }
}
