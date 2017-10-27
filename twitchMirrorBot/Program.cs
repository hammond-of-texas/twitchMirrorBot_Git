using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using RedditSharp;
using RedditSharp.Things;
using twitchMirrorBot.models;


namespace twitchMirrorBot
{
    class Program
    {
        static void Main(string[] args)
        {

            /* 
             This bot checks if a twich streamer is online, and if this is the case, makes a sticky post
             in your subreddit and eddits the sidebar to "online". Once the streamer is offline, the post
             is unstickied and the sidebar updated to offline.

             The bot will also continuously update the post with viewers and streaming time. 

             This program runs continuously. You can add it as a scheduled task if you want it to run in the background.
             Check out the configurator class and edit it for your config.

             You must add the original RedditSharp (https://github.com/SirCmpwn/RedditSharp), which is now unmaintained,
             but you can download and it from GitHub and it still works. Compile it and add the dll as a reference.

             Made by /u/Hammond-Of-Texas, F. Kupferschmid
             Licensed under the MIT license.
             */

            while (true) // Busy-waiting
            {
                try // Sometimes reddit or twicht is glitching out, so just try it at the next run.
                {
                    // Connect to reddit
                    var reddit = new Reddit();
                    reddit.LogIn(Configurator.BotName, Configurator.BotPassword, true);

                    var subreddit = reddit.GetSubreddit(Configurator.SubredditName);
                    var settings = subreddit.Settings;

                    // Connect to twitch stream channel
                    string twitchResponse;
                    string url =
                        $"https://api.twitch.tv/kraken/streams/{Configurator.TwitchChannelName}?client_id={Configurator.ClientSecret}";

                    using (WebResponse wr = WebRequest.Create(url).GetResponse())
                    using (StreamReader sr = new StreamReader(wr.GetResponseStream()))
                    {
                        twitchResponse = sr.ReadToEnd();
                    }

                    //  Get channel Info
                    var twitchObject = JsonConvert.DeserializeObject<ChannelInfo>(twitchResponse);

                    // Get last post by bot
                    var lastPost = reddit.User.Posts.FirstOrDefault();

                    // If the bot never posted anything before, create a welcome post. Not sure if it works.
                    if (lastPost == null)
                    {
                        Console.WriteLine("Creating welcome post...");
                        string titleName = "Hello, Im your new twich2reddit bot!";
                        string bodyText = "Hi,\nI will sticky a post whenever your favourite streamer is online!";
                        Uri id = subreddit.SubmitTextPost(titleName, bodyText).Url;
                        var post = reddit.GetPost(id);
                        post.SetFlair("Info", "");
                        post.Distinguished = VotableThing.DistinguishType.Moderator;
                        post.Save();
                        return;
                    }

                    // Stream is offline
                    if (twitchObject.Stream == null)
                    {
                        Console.WriteLine("Not streaming right now");

                        // Unsticky post, update text and flair as offine
                        if (string.IsNullOrEmpty(lastPost.LinkFlairText))
                        {
                            lastPost.EditText($"{Configurator.OfflinePostText}{lastPost.SelfText}");
                            lastPost.SetFlair($"Stream ended {DateTime.Now.ToUniversalTime():R}", "");
                            lastPost.StickyMode(false);
                            lastPost.Save();

                            // Update sidebar
                            if (settings.Sidebar.Contains(Configurator.OnlineText))
                            {
                                Console.WriteLine("Updating sidebar to offline...");
                                string replace =
                                    settings.Sidebar.Replace(Configurator.OnlineText, Configurator.OfflineText);
                                settings.Sidebar = replace;
                                settings.UpdateSettings();
                            }
                        }

                        Console.WriteLine("Updated post to offline.");

                    }
                    else

                        // Stream is online
                    {
                        // Assemble body text and make a new post or update the last one
                        string streamerName = twitchObject.Stream.Channel.DisplayName;
                        string titleName =
                            $"{streamerName} is streaming {twitchObject.Stream.Game} right now [{twitchObject.Stream.CreatedAt.ToUniversalTime():R}]";

                        Console.WriteLine(titleName);

                        string bodyText =
                            $"#{streamerName} is streaming right now! \n\nWatch [here]({twitchObject.Stream.Channel.Url})\n\n";
                        bodyText +=
                            $"---\nCurrent viewers: {twitchObject.Stream.Viewers}\n\nUptime: {Math.Round((DateTime.UtcNow - twitchObject.Stream.CreatedAt.ToUniversalTime()).TotalHours, 2)} hours.\n\n";

                        if (lastPost.Title != titleName)
                        {
                            // New stream (or streamer changed game), create new post
                            Console.WriteLine("Creating new post...");
                            Uri id = subreddit.SubmitTextPost(titleName, bodyText).Url;
                            var post = reddit.GetPost(id);
                            post.StickyMode(true);
                            lastPost.StickyMode(
                                false); // Unsticky old post if streamer is still streaming but changed game
                            post.Save();
                            lastPost.Save();

                            // Update sidebare
                            if (settings.Sidebar.Contains(Configurator.OfflineText))
                            {
                                Console.WriteLine("Updating sidebar to online..");
                                string replace =
                                    settings.Sidebar.Replace(Configurator.OfflineText, Configurator.OnlineText);
                                settings.Sidebar = replace;
                                settings.UpdateSettings();
                            }
                        }

                        else
                        {
                            // Otherwise, update post (viewers, streaming time)
                            Console.WriteLine("Updating post...");
                            lastPost.EditText(bodyText);
                            lastPost.Save();

                            Console.WriteLine("Run complete.");
                            Thread.Sleep(5000);
                        }
                    }
                   
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
