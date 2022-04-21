using InstagramPrivateAPI.src.responses;
using InstagramPrivateAPI.src.responses.friendships;
using InstagramPrivateAPI.src.responses.news;
using InstagramPrivateAPI.src.responses.users;
using Newtonsoft.Json.Linq;

namespace InstagramPrivateAPI.src.requests
{
    internal class People
    {
        public UserResponse GetInfoByName(string username) => Request.Send<UserResponse>("users/" + username + "/usernameinfo/");

        public UserResponse GetInfoById(string userId) => Request.Send<UserResponse>("users/" + userId + "/info/");

        public UserResponse GetFullInfoById(string userId) => Request.Send<UserResponse>("users/" + userId + "/full_detail_info/");

        public UserResponse GetSelfInfo() => GetInfoById(Globals.username_id);

        public NewsInboxResponse GetRecentActivityInbox() => Request.Send<NewsInboxResponse>("news/inbox/?activity_module=all&show_su=true");

        public NewsInboxResponse GetFollowingRecentActivity(string maxId = null) => Request.Send<NewsInboxResponse>("news/" + (maxId == null ? "" : "?max_id=" + maxId));

        public FriendshipsShowResponse GetFriendship(string userId) => Request.Send<FriendshipsShowResponse>("friendships/show/" + userId);

        public FriendshipStatusResponse RemoveFollower(string userId)
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data._csrftoken = Globals.token;
            data.user_id = userId;

            return Request.Send<FriendshipStatusResponse>("friendships/remove_follower/" + userId + "/", data.ToString());
        }

        public Response GetFollowing(string userId, string maxId = null)
        {
            maxId = maxId != null ? maxId : "";

            return Request.Send<Response>("/following/?rank_token=" + Globals.rank_token + "&max_id=" + maxId);
        }

        public Response GetFollowers(string userId, string maxId = null) => Request.Send<Response>("friendships/" + userId + "/followers/?rank_token=" + Globals.rank_token + "&max_id=" + (maxId != null ? maxId : ""));

        // query = username or name
        public UsersSearchResponse Search(string query) => Request.Send<UsersSearchResponse>("users/search/?rank_token=" + Globals.rank_token + "query" + query);

        public FriendshipStatusResponse Follow(string userId, string mediaId = null)
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data.user_id = userId;
            data._csrftoken = Globals.csrftoken;
            data.radio_type = "wifi-none";
            data.device_id = Globals.device_id;

            if (mediaId != null)
                data.media_id_attribution = mediaId;


            return Request.Send<FriendshipStatusResponse>("friendships/create/" + userId + "/", data.ToString());
        }

        public FriendshipStatusResponse Unfollow(string userId)
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data.user_id = userId;
            data._csrftoken = Globals.csrftoken;
            data.radio_type = "wifi-none";
            data.device_id = Globals.device_id;

            return Request.Send<FriendshipStatusResponse>("friendships/destroy/" + userId + "/", data.ToString());
        }

        public FriendshipStatusResponse TurnOnUserNotification(string userId)
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data._csrftoken = Globals.token;

            return Request.Send<FriendshipStatusResponse>("friendships/favorite/" + userId + "/", data.ToString());
        }

        public FriendshipStatusResponse TurnOffUserNotification(string userId)
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data._csrftoken = Globals.token;

            return Request.Send<FriendshipStatusResponse>("friendships/unfavorite/" + userId + "/", data.ToString());
        }

        public FriendshipStatusResponse Block(string userId)
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data.user_id = userId;
            data._csrftoken = Globals.token;

            return Request.Send<FriendshipStatusResponse>("friendships/block/" + userId + "/", data.ToString());
        }

        public FriendshipStatusResponse Unblock(string userId)
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data.user_id = userId;
            data._csrftoken = Globals.token;

            return Request.Send<FriendshipStatusResponse>("friendships/unblock/" + userId + "/", data.ToString());
        }

        public UsersBlockedListResponse GetBlockedList() => Request.Send<UsersBlockedListResponse>("users/blocked_list/");

        public FriendshipStatusResponse BlockStory(string userId)
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data.source = "profile";
            data._csrftoken = Globals.token;

            return Request.Send<FriendshipStatusResponse>("friendships/block_friend_reel/" + userId + "/", data.ToString());
        }

        public FriendshipStatusResponse UnblockStory(string userId)
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data.source = "profile";
            data._csrftoken = Globals.token;

            return Request.Send<FriendshipStatusResponse>("friendships/unblock_friend_reel/" + userId + "/", data.ToString());
        }

        public UserResponse GetBlockedStoryList()
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data._csrftoken = Globals.token;

            return Request.Send<UserResponse>("friendships/blocked_reels/", data.ToString());
        }

        public FriendshipStatusResponse muteFriendStory(string userId)
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data._csrftoken = Globals.token;

            return Request.Send<FriendshipStatusResponse>($"friendships/mute_friend_reel/{userId}/", data.ToString());
        }

        public FriendshipStatusResponse unmuteFriendStory(string userId)
        {
            dynamic data = new JObject();
            data._uuid = Globals.uuid;
            data._uid = Globals.username_id;
            data._csrftoken = Globals.token;

            return Request.Send<FriendshipStatusResponse>($"friendships/unmute_friend_reel/{userId}/", data.ToString());
        }

        public string GetUserId(string username) => GetInfoByName(username).user.pk.ToString();
    }
}
