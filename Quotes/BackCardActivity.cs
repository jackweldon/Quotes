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
using Java.Security.Spec;
using Newtonsoft.Json;

namespace Quotes
{
    [Activity(Label = "Quote")]
    public class BackCardActivity : Activity
    {
        private QuoteModel mQuote;
        private TextView mQuoteText;
        private TextView mAuthor;
        public GestureDetector mGestureDetector;

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

            view.Touch += ViewOnTouch;
            mGestureDetector = new GestureDetector(this, new CardGestureListener(this));

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

        private void ViewOnTouch(object sender, View.TouchEventArgs touchEventArgs)
        {
            this.mGestureDetector.OnTouchEvent(touchEventArgs.Event);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            OverridePendingTransition(Resource.Animation.fade_in, Resource.Animation.fade_out);
        }


    }

    public class CardGestureListener : GestureDetector.SimpleOnGestureListener
    {

        public BackCardActivity mBackActivity;

        public CardGestureListener(BackCardActivity mBackActivity)
        {
            this.mBackActivity = mBackActivity;
        }

        public override bool OnDoubleTap(MotionEvent e)
        {
         
            mBackActivity.OnBackPressed();
            return base.OnDoubleTap(e);
        }

        public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            mBackActivity.OnBackPressed();
            return base.OnFling(e1, e2, velocityX, velocityY);
        }
    }
}
//6i#GJzr&b6M3gKSHn3Z)