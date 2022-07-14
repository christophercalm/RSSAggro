﻿using Microsoft.AspNetCore.Mvc;
using RSSAggro.Models;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RSSAggro.Controllers
{
    public class FeedController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Hello";
            ViewData["NumTimes"] = 5;

            var rssFeeds = new List<Uri>
            {
                new Uri("https://thoughtscollected.tech/feed.xml"),
                new Uri("http://feeds.hanselman.com/ScottHanselman"),
                new Uri("https://allenpike.com/feed/"),
                new Uri("https://weedon.blogspot.com/feeds/posts/default")

            };
            var client = new HttpClient();

            List<SyndicationItem> rssItems = new List<SyndicationItem>();
            List<RSSArticle> ArticleList = new List<RSSArticle>();


            foreach (var rssFeed in rssFeeds)
            {
                var result = client.GetStreamAsync(rssFeed).Result;

                // todo: implement the rest
                using (var xmlReader = XmlReader.Create(result))
                {
                    SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
                    feed.Items = feed.Items.Reverse();

                    if (feed != null)
                    {
                        foreach (SyndicationItem item in feed.Items)
                        {
                            rssItems.Add(item);
                      
                        }
                        // todo: loop over feed.Items?
                    }  
                }
            }

            //fix invalid years
            foreach (SyndicationItem item in rssItems)
            {
                //invalid year
                if (item.PublishDate.Year == 1)
                {
                    item.PublishDate = item.LastUpdatedTime;
                }
            }
            rssItems.Sort((x, y) => x.PublishDate.CompareTo(x.PublishDate));

            foreach (SyndicationItem item in rssItems)
            {
                //{1/1/0001 12:00:00 AM +00:00}

                var article = new RSSArticle((item?.Content as TextSyndicationContent)?.Text ?? item?.Summary?.Text ?? "no content", item?.Title.Text ?? "No Title", item?.Id ?? "No slug");
                ArticleList.Add(article);

            }

            // ViewData["FeedItems"] = ArticleList;

            return View(ArticleList);
        }
    }
}