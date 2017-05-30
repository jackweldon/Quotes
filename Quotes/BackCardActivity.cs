using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Quotes
{
    [Activity(Label = "Quote")]
    public class BackCardActivity : Activity
    {
        private QuoteModel mQuote;
        private TextView mQuoteText;
        private TextView mAuthor;

        private LinearLayout view;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.card_back);

            // Create your application here
            mQuote = JsonConvert.DeserializeObject<QuoteModel>(Intent.GetStringExtra("Quote"));
            mQuoteText = FindViewById<TextView>(Resource.Id.quote);
            mAuthor = FindViewById<TextView>(Resource.Id.author);

            view = FindViewById<LinearLayout>(Resource.Id.card_back);
            mQuoteText.Text = mQuote.Quote;
            mAuthor.Text = mQuote.Author;

            switch (mQuote.QuoteType)
            {
                case QuoteType.Happy:
                    view.SetBackgroundColor(Resources.GetColor(Resource.Color.happy_color));
                    break;
                case QuoteType.Love:
                    view.SetBackgroundColor(Resources.GetColor(Resource.Color.love_color));
                    break;
                case QuoteType.Life:
                    view.SetBackgroundColor(Resources.GetColor(Resource.Color.life_color));
                    break;
                default:
                    view.SetBackgroundColor(Resources.GetColor(Resource.Color.funny_color));
                    break;
            }
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            OverridePendingTransition(Resource.Animation.fade_in, Resource.Animation.fade_out);

        }
    }
}
//6i#GJzr&b6M3gKSHn3Z)