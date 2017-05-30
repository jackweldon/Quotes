using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Newtonsoft.Json;
using Quotes.Core.Services;

namespace Quotes
{

    [Activity(Label = "Quote Cube", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private bool mShowingBack;
        public GestureDetector mGestureDetector;
        public List<QuoteModel> _listOfQuotes;



        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.card_front);

            var happyButton = (Button)FindViewById<Button>(Resource.Id.btn_Happy);
            happyButton.Click += QuoteButtonOnClick;

            var funnyButton = (Button)FindViewById<Button>(Resource.Id.btn_Funny);
            funnyButton.Click += QuoteButtonOnClick;


            var loveButton = (Button)FindViewById<Button>(Resource.Id.btn_love);
            loveButton.Click += QuoteButtonOnClick;

            var lifeButton = (Button)FindViewById<Button>(Resource.Id.btn_life);
            lifeButton.Click += QuoteButtonOnClick;


            var id = "ca-app-pub-1925531025157688~3263264655";
            MobileAds.Initialize(ApplicationContext, id);

            var adView = FindViewById<AdView>(Resource.Id.adView);
            var adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);
        }

        private void QuoteButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(this, typeof(BackCardActivity));
            Button btn = (Button)sender;
            switch (btn.Id)
            {
                case Resource.Id.btn_Happy:
                    intent.PutExtra("Quote", JsonConvert.SerializeObject(GetQuote(QuoteType.Happy)));
                    break;
                case Resource.Id.btn_life:
                    intent.PutExtra("Quote", JsonConvert.SerializeObject(GetQuote(QuoteType.Life)));
                    break;
                case Resource.Id.btn_love:
                    intent.PutExtra("Quote", JsonConvert.SerializeObject(GetQuote(QuoteType.Love)));
                    break;
                default:
                    intent.PutExtra("Quote", JsonConvert.SerializeObject(GetQuote(QuoteType.Funny)));
                    break;
            }

            this.StartActivity(intent);
            OverridePendingTransition(Resource.Animation.fade_in, Resource.Animation.fade_out);
        }

        private QuoteModel GetQuote(QuoteType type)
        {

            string url = "http://quoteapi.esy.es/quotes/" + (int)type;
            if (_listOfQuotes == null)
            {
                _listOfQuotes = new List<QuoteModel>();

                _listOfQuotes.AddRange(QuotesApi.FetchQuotesAsync(url).Result);
            }
            if (_listOfQuotes.All(m => m.QuoteType != type))
            {
                _listOfQuotes.AddRange(QuotesApi.FetchQuotesAsync(url).Result);

            }
            var first = _listOfQuotes.OrderByDescending(m => Guid.NewGuid()).FirstOrDefault(m => m.QuoteType == type);
            return first;

        }

    }

}

