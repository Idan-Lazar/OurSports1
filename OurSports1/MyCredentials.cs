using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace OurSports1
{
    public static class MyCredentials
    {
        public static string CONSUMER_KEY = "m1BPGspPDIiJKYl50GKOt9rRF";
        public static string CONSUMER_SECRET = "m73kL1R8txxc0XXxLBzPSSi0NKzkjuC0e7qHqpkhP9w369LX4l";
        public static string ACCESS_TOKEN = "1023346832997531648-ZZK9EwiZcsJXjbxPAbN8wu7UPGYB5r";
        public static string ACCESS_TOKEN_SECRET = "Fm8Mf6nhpKdkt007fXOcZEd9jwqEYG4iJDzZgjwlFLpgb";

        public static ITwitterCredentials GenerateCredentials()
        {
            return new TwitterCredentials(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
        }
    }
}
