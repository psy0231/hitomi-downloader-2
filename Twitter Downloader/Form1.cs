using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace Twitter_Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // https://twitter.com/otonajyojifuku/media
            
            RateLimit.RateLimitTrackerMode = RateLimitTrackerMode.TrackAndAwait;

            RateLimit.QueryAwaitingForRateLimit += (s, args) =>
            {
                Console.WriteLine($"Query : {args.Query} is awaiting for rate limits!");
            };

            long userId = 1706386254;
            var lastTweets = Timeline.GetUserTimeline(userId, 200).ToArray();

            var allTweets = new List<ITweet>(lastTweets);
            var beforeLast = allTweets;

            while (lastTweets.Length > 0 && allTweets.Count <= 3200)
            {
                var idOfOldestTweet = lastTweets.Select(x => x.Id).Min();
                Console.WriteLine($"Oldest Tweet Id = {idOfOldestTweet}");

                var numberOfTweetsToRetrieve = allTweets.Count > 3000 ? 3200 - allTweets.Count : 200;
                var timelineRequestParameters = new UserTimelineParameters
                {
                    // MaxId ensures that we only get tweets that have been posted 
                    // BEFORE the oldest tweet we received
                    MaxId = idOfOldestTweet - 1,
                    MaximumNumberOfTweetsToRetrieve = numberOfTweetsToRetrieve
                };

                lastTweets = Timeline.GetUserTimeline(userId, timelineRequestParameters).ToArray();
                allTweets.AddRange(lastTweets);
            }
        }
    }
}
