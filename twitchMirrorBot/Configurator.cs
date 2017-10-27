using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitchMirrorBot
{
    internal class Configurator
    {

        /* 
         Put the name of your bot in below. The bot must be a moderator of your
         subreddit and must only post in this speicifiy subreddit. Otherwise, 
         it could eddit posts in other subreddits.
         
         Your new bot account must first gain some karma to be able to post. Make some posts with
         the bot account in some other subreddit. But make sure that the last post is
         in the subreddit you want the bot to post before you run it.
         */
        public const string BotName = "YourBotName";

        // Enter the password of your bot here:
        public const string BotPassword = "YourBotPaasword";

        // Name of your subreddit:
        public const string SubredditName = "YourSubreddit";

        // Name of the twitch chanannel you want to watch:
        public const string TwitchChannelName = "TwitchChennelName";

        /*Enter your personal client secret below. 
         If you don't have one, login to twitch 
         and go to https://dev.twitch.tv/dashboard/apps/create.
         Chose a name for your bot, use localhost as redirect and chose a catecory.*/
        public const string ClientSecret = "ClientSecre0000000";

        public const string OnlineText = "###Swifty is online right now."; // Text in the sidebar if the streamer is online
        public const string OfflineText = "###Swifty is offline right now."; // Text in the sidebar if the streamer is offline. One of those two textes must be initially in the sidebar to make it work.

        public const string OfflinePostText = "##OFFLINE\n\n#This stream is now offline.\n\n---  \n\n"; // Will be added at the top of the post once the streamer is offline

    }
}
